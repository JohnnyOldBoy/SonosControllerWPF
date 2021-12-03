using Devices;
using GalaSoft.MvvmLight;
using MusicData;
using Services;
using SonosController.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Xml;

namespace SonosController
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {

        public readonly ServiceUtils _serviceUtils;
        #region
        /// <summary>
        /// Devices tab. A device is any Sonos product that can participate in a Sonos system, this includes all players -
        /// Play: 1, Play: 3, Play: 5, Beam etc. and also non player devices such as Boost and Bridge.
        /// </summary>
        /// 
        private ObservableCollection<ZonePlayer> _zonePlayerCollection;
        public ObservableCollection<ZonePlayer> ZonePlayerCollection
        {
            get { return _zonePlayerCollection; }
            set
            {
                _zonePlayerCollection = value;
                RaisePropertyChanged(nameof(ZonePlayerCollection));
            }
        }

        private ZonePlayer _selectedZonePlayer;
        public ZonePlayer SelectedZonePlayer
        {
            get => _selectedZonePlayer;
            set
            {
                _selectedZonePlayer = value;
                RaisePropertyChanged(nameof(SelectedZonePlayer));
            }
        }

        private ZonePlayer _selectedZoneGroupCoordinator;
        public ZonePlayer SelectedZoneGroupCoordinator
        {
            get => _selectedZoneGroupCoordinator;
            set
            {
                _selectedZoneGroupCoordinator = value;
                RaisePropertyChanged(nameof(SelectedZoneGroupCoordinator));
            }
        }

        public ICollectionView ZonePlayerDetailsViewCollection { get; set; }

        #endregion
        #region
        /// <summary>
        /// Rooms tab. A room is basically a single zone player or a groups of zone players, this will include stereo pairs.
        /// Devices such as Boost and Bridge are not normally visible here.
        /// /// </summary>

        private ObservableCollection<ZoneGroupViewModel> _zoneGroupViewModels;
        public ObservableCollection<ZoneGroupViewModel> ZoneGroupViewModels
        {
            get => _zoneGroupViewModels;
            set
            {
                _zoneGroupViewModels = value;
                RaisePropertyChanged(nameof(ZoneGroupViewModels));
            }
        }

        public ICollectionView ZoneGroupQueueViewCollection { get; set; }

        private ZoneGroupViewModel _selectedZoneGroup;
        public ZoneGroupViewModel SelectedZoneGroup
        {
            get => _selectedZoneGroup;
            set
            {
                _selectedZoneGroup = value;
                RaisePropertyChanged(nameof(SelectedZoneGroup));
            }
        }

        public ICollectionView ZoneGroupViewModelsCollection { get; set; }

        private ObservableCollection<StereoPairViewModel> _stereoPairViewModels;
        public ObservableCollection<StereoPairViewModel> StereoPairViewModels
        {
            get => _stereoPairViewModels;
            set
            {
                _stereoPairViewModels = value;
                RaisePropertyChanged(nameof(StereoPairViewModels));
            }
        }

        #endregion
        /// <summary>
        /// Shared
        /// </summary>

        private ZoneGroupTopologyViewModel _zoneGroupTopologyViewModel;
        public ZoneGroupTopologyViewModel ZoneGroupTopologyViewModel
        {
            get => _zoneGroupTopologyViewModel;
            set
            {
                _zoneGroupTopologyViewModel = value;
                RaisePropertyChanged(nameof(ZoneGroupTopologyViewModel));
            }
        }

        public ZonePlayersViewModel ZonePlayersViewModel;
        private ZonePlayersViewModel _zonePlayersViewModel
        {
            get => _zonePlayersViewModel;
            set
            {
                _zonePlayersViewModel = value;
                RaisePropertyChanged(nameof(ZonePlayersViewModel));
            }
        }

        public CommandEx CommandEx
        {
            get;
            set;
        }

        public void CommandExMethod(object parameter)
        {
            if (parameter as string == "ViewMusicLibrary")
            {
                MusicLibraryWindow musicLibraryWindow = new MusicLibraryWindow();
                musicLibraryWindow.Show();
            }
            if (parameter as string == "CreateStereoPair")
            {
                CreateStereoPairWindow createStereoPairWindow = new CreateStereoPairWindow();
                createStereoPairWindow.DataContext = new CreateStereoPairViewModel(this);
                createStereoPairWindow.ShowDialog();
                CreateStereoPairViewModel newCreateStereoPairViewModel = createStereoPairWindow.DataContext as CreateStereoPairViewModel;
                if (newCreateStereoPairViewModel.NewStereoPair != null)
                {
                    StereoPairViewModel stereoPairViewModel = new StereoPairViewModel(ZoneGroupTopologyViewModel);
                    ZonePlayer masterZonePlayer = _serviceUtils.GetPlayerByUUID(ZonePlayersViewModel.ZonePlayers, newCreateStereoPairViewModel.NewStereoPair.LeftUUID);
                    stereoPairViewModel.PairName = masterZonePlayer.RoomName;
                    stereoPairViewModel.StereoPair.Add(newCreateStereoPairViewModel.NewStereoPair);
                    stereoPairViewModel.StereoPair[0].MasterPlayerIpAddress = masterZonePlayer.PlayerIpAddress;
                    if (StereoPairViewModels != null)
                    {
                        StereoPairViewModels.Add(stereoPairViewModel);
                    }
                    else
                    {
                        StereoPairViewModels = new ObservableCollection<StereoPairViewModel>();
                        StereoPairViewModels.Add(stereoPairViewModel);
                    }
                    ZoneGroupTopologyViewModel.StereoPairViewModels = StereoPairViewModels;
                }
            }
            if (parameter as string == "GroupManagementNew")
            {
                GroupManagementWindow groupManagementWindow = new GroupManagementWindow();
                groupManagementWindow.DataContext = new GroupManagementViewModel(0);
                groupManagementWindow.ShowDialog();
            }
        }

        XmlDocument _sonosSystem;
        public XmlDocument SonosSystem 
        { 
            get => _sonosSystem; 
            set => _sonosSystem = value; 
        }

        string playerIpAddress = string.Empty;

        public MainWindowViewModel()
        {
            _serviceUtils = new ServiceUtils();

            CommandEx = new CommandEx
            {
                CanExecuteFunc = obj => true,
                ExecuteFunc = CommandExMethod
            };

            PropertyChanged += OnPropertyChangedHandler;

            //Get a zone player Ip address using iPnP, only one is required
            playerIpAddress = _serviceUtils.GetPlayerIPAdress();

            #region
            // Devices
            GetDevices();

            #endregion
            #region
            // Rooms and group managemeent
            GetCurrentTopology();
            StereoPairViewModels = ZoneGroupTopologyViewModel.StereoPairViewModels;
            SelectedZoneGroup = ZoneGroupViewModels.FirstOrDefault();
            SelectedZoneGroup.IsSelected = true;
            SelectedZoneGroupCoordinator = _serviceUtils.GetPlayerByUUID(ZonePlayersViewModel.ZonePlayers, SelectedZoneGroup.ZoneGroupCoordinator.UUID);
            #endregion

            #region
            // Queues
            GetQueues();
            #endregion
        }

        private void GetDevices()
        {
            //Get the Zone Group Topology XML from using the IP address found above
            //The whole system can be obtained from this
            SonosSystem = _serviceUtils.GetSonosSystem(playerIpAddress);

            string[] playerDescLocations = _serviceUtils.GetPlayerDescLocations(SonosSystem);

            // Devices

            ZonePlayersViewModel = new ZonePlayersViewModel(playerDescLocations);
            ZonePlayerCollection = ZonePlayersViewModel.ZonePlayerCollection;

            List<ZonePlayerDetail> zonePlayerDetails = _serviceUtils.GetPlayerDetails(ZonePlayersViewModel.ZonePlayers);
            ZonePlayerDetailsViewCollection = new ListCollectionView(zonePlayerDetails);
            SelectedZonePlayer = ZonePlayerCollection.FirstOrDefault();
            if (SelectedZonePlayer != null)
            {
                ZonePlayerDetailsViewCollection.Filter = t =>
                {
                    if (t is ZonePlayerDetail zonePlayerDetail)
                    {
                        if (zonePlayerDetail.PlayerIpAddress == SelectedZonePlayer.PlayerIpAddress)
                        {
                            return true;
                        }
                    }
                    return false;
                };
                ZonePlayerDetailsViewCollection.Refresh();
            }
        }

        private void GetCurrentTopology()
        {
            ZoneGroupTopologyViewModel = new ZoneGroupTopologyViewModel(SonosSystem, ZonePlayersViewModel);
            if (ZonePlayersViewModel.ZonePlayers.ZonePlayersList.Any())
            {
                ZoneGroupViewModels = ZoneGroupTopologyViewModel.ZoneGroupViewModels;
                ZoneGroupViewModelsCollection = new ListCollectionView(ZoneGroupViewModels);
                if (SelectedZonePlayer != null)
                {
                    ZonePlayerDetailsViewCollection.Filter = t =>
                    {
                        if (t is ZonePlayerDetail zonePlayerDetail)
                        {
                            if (zonePlayerDetail.PlayerIpAddress == SelectedZonePlayer.PlayerIpAddress)
                            {
                                return true;
                            }
                        }
                        return false;
                    };
                    ZonePlayerDetailsViewCollection.Refresh();
                }

                //StereoPairViewModels = ZoneGroupTopologyViewModel.StereoPairViewModels;
                //SelectedZoneGroup = ZoneGroupViewModels.FirstOrDefault();
                //SelectedZoneGroup.IsSelected = true;
                //SelectedZoneGroupCoordinator = _serviceUtils.GetPlayerByUUID(ZonePlayersViewModel.ZonePlayers, SelectedZoneGroup.ZoneGroupCoordinator.UUID);
            }
       }

        private void GetQueues()
        {
            //Queues
            QueueViewModel queueViewModel = new QueueViewModel(this);
            ZoneGroupQueueViewCollection = new ListCollectionView(queueViewModel.QueueItemList)
            {
                Filter = t =>
                {
                    if (t is QueueItem queueItem)
                    {
                        if (queueItem.ZoneGroupCoordinator == SelectedZoneGroup.ZoneGroupCoordinator.UUID)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            };
            ZoneGroupQueueViewCollection.Refresh();
        }

        private void OnPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            //MessageBox.Show(e.PropertyName);
            if (e.PropertyName == nameof(SelectedZonePlayer))
            {
                ZonePlayerDetailsViewCollection.Filter = t =>
                {
                    if (t is ZonePlayerDetail zonePlayerDetail)
                    {
                        if (zonePlayerDetail.PlayerIpAddress == SelectedZonePlayer.PlayerIpAddress)
                        {
                            return true;
                        }
                    }
                    return false;
                };
                ZonePlayerDetailsViewCollection.Refresh();
            }
            if (e.PropertyName == nameof(SelectedZoneGroup))
            {
                if (SelectedZoneGroup != null && ZoneGroupQueueViewCollection != null)
                {
                    ZoneGroupQueueViewCollection.Filter = t =>
                    {
                        if (t is QueueItem queueItem)
                        {
                            if (queueItem.ZoneGroupCoordinator == SelectedZoneGroup.ZoneGroupCoordinator.UUID)
                            {
                                return true;
                            }
                        }
                        return false;
                    };
                    ZoneGroupQueueViewCollection.Refresh();
                }
            }
            if (e.PropertyName == nameof(StereoPairViewModels))
            {
                if (StereoPairViewModels != null)
                {
                    GetCurrentTopology();
                    ZoneGroupViewModelsCollection.Refresh();
                }
            }
            if (e.PropertyName == nameof(StereoPairViewModel))
            {
                MessageBox.Show("Separated");
                ZoneGroupViewModelsCollection.Refresh();
            }
        }
    }
}