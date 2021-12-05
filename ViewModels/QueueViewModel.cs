using Devices;
using GalaSoft.MvvmLight;
using MusicData;
using Services;
using System.Collections.Generic;
using System.ComponentModel;

namespace SonosController.ViewModels
{
    public class QueueViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public QueueViewModel(MainWindowViewModel mainWindowViewModel)
        {
            localMainWindowViewModel = mainWindowViewModel;
            _serviceUtils = localMainWindowViewModel._serviceUtils;

            QueueItemList = new List<QueueItem>();
            foreach (ZoneGroupViewModel zoneGroupViewModel in localMainWindowViewModel.ZoneGroupViewModels)
            {
                ZonePlayer SelectedZoneGroupCoordinator = _serviceUtils.GetPlayerByUUID(localMainWindowViewModel.ZonePlayersViewModel.ZonePlayers, zoneGroupViewModel.ZoneGroupCoordinator.UUID);
                PlayerQueue playerQueue = _serviceUtils.GetPlayerQueue(zoneGroupViewModel.ZoneGroupCoordinator.UUID, SelectedZoneGroupCoordinator.PlayerIpAddress);

                if (playerQueue.QueueItems.Count == 0)
                {
                    QueueItem emptyQueueItem = new QueueItem
                    {
                        QiTitle = "Queue empty",
                        ZoneGroupCoordinator = SelectedZoneGroupCoordinator.UUID
                    };
                    QueueItemList.Add(emptyQueueItem);
                }
                else
                {
                    foreach (QueueItem queueItem in playerQueue.QueueItems)
                    {
                        QueueItemList.Add(queueItem);
                    }
                }
            }
        }

        private MainWindowViewModel localMainWindowViewModel;
        private ServiceUtils _serviceUtils;

        private List<QueueItem> _queueItemList;
        public List<QueueItem> QueueItemList
        {
            get => _queueItemList;
            set
            {
                _queueItemList = value;
                RaisePropertyChanged(nameof(QueueItemList));
            }
        }
    }
}