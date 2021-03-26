using Devices;
using GalaSoft.MvvmLight;
using MusicData;
using Services;
using SonosController.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace SonosController
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {

        private readonly ServiceUtils _serviceUtils;
        #region
        /// <summary>
        /// Devices tab. A device is any Sonos product that can participate in a Sonos system, this includes all players -
        /// Play: 1, Play: 3, Play: 5, Beam etc and also non palyer devices such as Boost and Bridge.
        /// </summary>
        private ObservableCollection<ZonePlayer> _zonePlayerCollection;
        public ObservableCollection<ZonePlayer> ZonePlayerCollection
        {
            get { return _zonePlayerCollection; }
            set
            {
                _zonePlayerCollection = value;
                RaisePropertyChanged("SelctedZonePlayer");
            }
        }
        
        private ZonePlayer _selectedZonePlayer;
        public ZonePlayer SelectedZonePlayer
        {
            get => _selectedZonePlayer;
            set
            {
                _selectedZonePlayer = value;
                RaisePropertyChanged("SelectedZonePlayer");
            }
        }

        private ZonePlayer _selectedZoneGroupCoordinator;
        public ZonePlayer SelectedZoneGroupCoordinator
        {
            get => _selectedZoneGroupCoordinator;
            set
            {
                _selectedZoneGroupCoordinator = value;
                RaisePropertyChanged("SelectedZoneGroupCoordinator");
            }
        }

        private ObservableCollection<ZonePlayerDetail> _zonePlayerDetailsView;

        public ObservableCollection<ZonePlayerDetail> ZonePlayerDetailsView
        {
            get => _zonePlayerDetailsView;
            set
            {
                _zonePlayerDetailsView = value;
                RaisePropertyChanged("ZonePlayerDetailsView");
            }
        }

        public ICollectionView ZonePlayerDetailsViewCollection { get; }

        #endregion
        #region
        /// <summary>
        /// Rooms tab. A room is basically a single zone player or a groups of zone players, this will include stereo pairs.
        /// Devices such as Boost and Bridge are not normally visible here.
        /// /// </summary>

        private ObservableCollection<ZoneGroup> _zoneGroupViewModelCollection;
        public ObservableCollection<ZoneGroup> ZoneGroupViewModelCollection
        {
            get => _zoneGroupViewModelCollection;
            set
            {
                _zoneGroupViewModelCollection = value;
                RaisePropertyChanged("ZoneGroupViewModelCollection");
            }
        }

        private ZoneGroup _selectedZoneGroup;
        public ZoneGroup SelectedZoneGroup
        {
            get => _selectedZoneGroup;
            set
            {
                _selectedZoneGroup = value;
                RaisePropertyChanged("SelectedZoneGroup");
            }
        }

        public ICollectionView ZoneGroupQueueViewCollection { get; }

        public ObservableCollection<ZoneGroupViewModel> ZoneGroupViewModels { get; } = new ObservableCollection<ZoneGroupViewModel>();

        private ObservableCollection<StereoPairViewModel> _stereoPairViewModelsCollection;
        public ObservableCollection<StereoPairViewModel> StereoPairViewModelsCollection
        {
            get => _stereoPairViewModelsCollection;
            set
            {
                _stereoPairViewModelsCollection = value;
                RaisePropertyChanged("StereoPairViewModelsCollection");
            }
        }


        #endregion
        /// <summary>
        /// Shared
        /// </summary>

        private ZonePlayers _zonePlayers;
        public ZonePlayers ZonePlayers
        {
            get => _zonePlayers;
            set
            {
                _zonePlayers = value;
                RaisePropertyChanged("ZonePlayers");
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
                

            }
        }

        public MainWindowViewModel()
        {
            _serviceUtils = new ServiceUtils();

            CommandEx = new CommandEx
            {
                CanExecuteFunc = obj => true,
                ExecuteFunc = CommandExMethod
            };

            PropertyChanged += OnPropertyChangedHandler;
            #region
            // Devices
            ZonePlayersViewModel zonePlayersViewModel = new ZonePlayersViewModel();
            ZonePlayerCollection = zonePlayersViewModel.ZonePlayerCollection;

            List<ZonePlayerDetail> zonePlayerDetails = _serviceUtils.GetPlayerDetails(zonePlayersViewModel.ZonePlayers);
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
            #endregion
            #region
            // Rooms and group managemeent
            if (zonePlayersViewModel.ZonePlayers.ZonePlayersList.Any())
            {
                ZoneGroupTopologyViewModel zoneGroupTopologyViewModel = new ZoneGroupTopologyViewModel(zonePlayersViewModel);
                ZoneGroupViewModelCollection = zoneGroupTopologyViewModel.ZoneGroupCollection;
                StereoPairViewModelsCollection = zoneGroupTopologyViewModel.StereoPairViewModels;
                SelectedZoneGroup = ZoneGroupViewModelCollection.FirstOrDefault();
                SelectedZoneGroup.IsSelected = true;
                SelectedZoneGroupCoordinator = _serviceUtils.GetPlayerByUUID(zonePlayersViewModel.ZonePlayers, SelectedZoneGroup.ZoneGroupCoordinator);

                //Queues
                QueueViewModel queueViewModel = new QueueViewModel(this);
                ZoneGroupQueueViewCollection = new ListCollectionView(queueViewModel.QueueItemList)
                {
                    Filter = t =>
                    {
                        if (t is QueueItem queueItem)
                        {
                            if (queueItem.ZoneGroupCoordinator == SelectedZoneGroup.ZoneGroupCoordinator)
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                };
                ZoneGroupQueueViewCollection.Refresh();
            }
        }
        #endregion

        private void OnPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
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
                            if (queueItem.ZoneGroupCoordinator == SelectedZoneGroup.ZoneGroupCoordinator)
                            {
                                return true;
                            }
                        }
                        return false;
                    };
                    ZoneGroupQueueViewCollection.Refresh();
                }
            }
        }
    }
}
