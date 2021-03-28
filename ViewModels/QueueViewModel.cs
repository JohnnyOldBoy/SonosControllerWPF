using Devices;
using GalaSoft.MvvmLight;
using MusicData;
using Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace SonosController.ViewModels
{
    public class QueueViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public QueueViewModel(MainWindowViewModel mainWindowViewModel)
        {
            ServiceUtils _serviceUtils = new ServiceUtils();
            localMainWindowViewModel = mainWindowViewModel;

            QueueItemList = new List<QueueItem>();
            foreach (ZoneGroupViewModel zoneGroupViewModel in localMainWindowViewModel.ZoneGroupViewModelCollection)
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
