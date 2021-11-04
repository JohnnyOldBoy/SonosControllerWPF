using Devices;
using GalaSoft.MvvmLight;
using MusicData;
using Services;
using SonosController.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

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

        public ICollectionView ZonePlayerDetailsViewCollection { get; }

        #endregion
        #region
        /// <summary>
        /// Rooms tab. A room is basically a single zone player or a groups of zone players, this will include stereo pairs.
        /// Devices such as Boost and Bridge are not normally visible here.
        /// /// </summary>

        private ObservableCollection<ZoneGroupViewModel> _zoneGroupViewModelCollection;
        public ObservableCollection<ZoneGroupViewModel> ZoneGroupViewModelCollection
        {
            get => _zoneGroupViewModelCollection;
            set
            {
                _zoneGroupViewModelCollection = value;
                RaisePropertyChanged(nameof(ZoneGroupViewModelCollection));
            }
        }

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

        public ICollectionView ZoneGroupQueueViewCollection { get; }

        private ObservableCollection<StereoPairViewModel> _stereoPairViewModelsCollection;
        public ObservableCollection<StereoPairViewModel> StereoPairViewModelsCollection
        {
            get => _stereoPairViewModelsCollection;
            set
            {
                _stereoPairViewModelsCollection = value;
                RaisePropertyChanged(nameof(StereoPairViewModelsCollection));
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
                    StereoPairViewModel stereoPairViewModel = new StereoPairViewModel(this);
                    stereoPairViewModel.PairName = _serviceUtils.GetPlayerByUUID(ZonePlayersViewModel.ZonePlayers, newCreateStereoPairViewModel.NewStereoPair.LeftUUID).RoomName;
                    stereoPairViewModel.StereoPair.Add(newCreateStereoPairViewModel.NewStereoPair);
                    if (StereoPairViewModelsCollection != null)
                    {
                        StereoPairViewModelsCollection.Add(stereoPairViewModel);
                    }
                    else
                    {
                        StereoPairViewModelsCollection = new ObservableCollection<StereoPairViewModel>();
                        StereoPairViewModelsCollection.Add(stereoPairViewModel);
                    }
                    ZoneGroupTopologyViewModel.StereoPairViewModels = StereoPairViewModelsCollection;
                }
            }
            if (parameter as string == "CreateStereoPair")
            {
                CreateStereoPairWindow createStereoPairWindow = new CreateStereoPairWindow();
                createStereoPairWindow.DataContext = new CreateStereoPairViewModel(this);
                createStereoPairWindow.ShowDialog();
                CreateStereoPairViewModel newCreateStereoPairViewModel = createStereoPairWindow.DataContext as CreateStereoPairViewModel;
                if (newCreateStereoPairViewModel.NewStereoPair != null)
                {
                    StereoPairViewModel stereoPairViewModel = new StereoPairViewModel(this);
                    stereoPairViewModel.PairName = _serviceUtils.GetPlayerByUUID(ZonePlayersViewModel.ZonePlayers, newCreateStereoPairViewModel.NewStereoPair.LeftUUID).RoomName;
                    stereoPairViewModel.StereoPair.Add(newCreateStereoPairViewModel.NewStereoPair);
                    if (StereoPairViewModelsCollection != null)
                    {
                        StereoPairViewModelsCollection.Add(stereoPairViewModel);
                    }
                    else
                    {
                        StereoPairViewModelsCollection = new ObservableCollection<StereoPairViewModel>();
                        StereoPairViewModelsCollection.Add(stereoPairViewModel);
                    }
                    ZoneGroupTopologyViewModel.StereoPairViewModels = StereoPairViewModelsCollection;
                }
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
            ZonePlayersViewModel = new ZonePlayersViewModel();
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
            #endregion
            #region
            // Rooms and group managemeent
            if (ZonePlayersViewModel.ZonePlayers.ZonePlayersList.Any())
            {
                ZoneGroupTopologyViewModel = new ZoneGroupTopologyViewModel(ZonePlayersViewModel);
                ZoneGroupViewModelCollection = ZoneGroupTopologyViewModel.ZoneGroupViewModels;
                StereoPairViewModelsCollection = ZoneGroupTopologyViewModel.StereoPairViewModels;
                SelectedZoneGroup = ZoneGroupViewModelCollection.FirstOrDefault();
                SelectedZoneGroup.IsSelected = true;
                SelectedZoneGroupCoordinator = _serviceUtils.GetPlayerByUUID(ZonePlayersViewModel.ZonePlayers, SelectedZoneGroup.ZoneGroupCoordinator.UUID);

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
            if (e.PropertyName == nameof(StereoPairViewModelsCollection))
            {
                if (StereoPairViewModelsCollection != null && ZoneGroupQueueViewCollection != null)
                {
                    ZoneGroupQueueViewCollection.Refresh();
                }
            }
        }
    }
}
