﻿using Devices;
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

namespace SonosController
{
    sealed class MainWindowViewModel : ViewModelBase
    {
        public ServiceUtils _serviceUtils;
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

        private ObservableCollection<ZonePlayerDetail> _zonePlayerDetailsView;

        public ObservableCollection<ZonePlayerDetail> ZonePlayerDetailsView
        {
            get => _zonePlayerDetailsView;
            set => _zonePlayerDetailsView = value;
        }

        public ICollectionView ZonePlayerDetailsViewCollection { get; }

        #endregion
        #region
        /// <summary>
        /// Rooms tab. A room is basically a single zone player or a groups of zone players, this will include stereo pairs.
        /// Devices such as Boost and Bridge are not normally visible here.
        /// /// </summary>

        private ObservableCollection<ZoneGroup> _zoneGroupCollection;
        public ObservableCollection<ZoneGroup> ZoneGroupCollection
        {
            get => _zoneGroupCollection;
            set
            {
                _zoneGroupCollection = value;
                RaisePropertyChanged("SelectedZoneGroup");
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

        public ObservableCollection<ZoneGroupViewModel> zoneGroupViewModels { get; } = new ObservableCollection<ZoneGroupViewModel>();
        #endregion
        /// <summary>
        /// Shared
        /// </summary>

        private ZonePlayers _zonePlayers;
        public ZonePlayers ZonePlayers
        {
            get => _zonePlayers;
            set => _zonePlayers = value;
        }

        public MainWindowViewModel()
        {
            _serviceUtils = new ServiceUtils();

            PropertyChanged += OnPropertyChangedHandler;
            #region
            // Devices
            ZonePlayers = _serviceUtils.GetZonePlayers();
            ZonePlayerCollection = new ObservableCollection<ZonePlayer>();
            foreach (ZonePlayer zonePlayer in ZonePlayers.ZonePlayersList)
            {
                ZonePlayerCollection.Add(zonePlayer);
            }

            List<ZonePlayerDetail> zonePlayerDetails = _serviceUtils.getPlayerDetails(ZonePlayers);
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
            if (ZonePlayers.ZonePlayersList.Any())
            {
                ZoneGroupTopology zoneGroupTopology =
                    _serviceUtils.GetZoneGroupTopology(ZonePlayers.ZonePlayersList.FirstOrDefault().PlayerIpAddress);
                ZoneGroupCollection = new ObservableCollection<ZoneGroup>();
                foreach (ZoneGroup zoneGroup in zoneGroupTopology.ZoneGroupList)
                {
                    ZoneGroupCollection.Add(zoneGroup);
                    ZoneGroupViewModel zoneGroupViewModel = new ZoneGroupViewModel(ZonePlayers, zoneGroup);
                    zoneGroupViewModels.Add(zoneGroupViewModel);
                }
            }
            SelectedZoneGroup = ZoneGroupCollection.FirstOrDefault();
            SelectedZoneGroup.IsSelected = true;

            //Queues
            List<QueueItem> queueItemList = new List<QueueItem>();
            foreach (ZoneGroup zoneGroup in ZoneGroupCollection)
            {
                ZonePlayer SelectedZoneGroupCoordinator = _serviceUtils.getPlayerByUUID(ZonePlayers, zoneGroup.ZoneGroupCoordinator);
                PlayerQueue playerQueue = _serviceUtils.getPlayerQueue(zoneGroup, SelectedZoneGroupCoordinator.PlayerIpAddress);
                
                if (playerQueue.QueueItems.Count == 0)
                {
                    QueueItem emptyQueueItem = new QueueItem();
                    emptyQueueItem.QiTitle = "Queue empty";
                    emptyQueueItem.ZoneGroupCoordinator = SelectedZoneGroupCoordinator.UUID;
                    queueItemList.Add(emptyQueueItem);
                }
                else
                {
                    foreach (QueueItem queueItem in playerQueue.QueueItems)
                    {
                        queueItemList.Add(queueItem);
                    }
                }
            }

            ZoneGroupQueueViewCollection = new ListCollectionView(queueItemList);

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
