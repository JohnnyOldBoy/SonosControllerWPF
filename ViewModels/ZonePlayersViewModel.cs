using Devices;
using GalaSoft.MvvmLight;
using Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SonosController.ViewModels
{
    public class ZonePlayersViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ZonePlayersViewModel(string[] zonePlayerDescUrls)
        {
            ServiceUtils _serviceUtils = new ServiceUtils();
            _zonePlayers = _serviceUtils.GetZonePlayers(zonePlayerDescUrls);
            ZonePlayerCollection = new ObservableCollection<ZonePlayer>();
            foreach (ZonePlayer zonePlayer in _zonePlayers.ZonePlayersList)
            {
                ZonePlayerCollection.Add(zonePlayer);
            }

        }

        private ZonePlayers _zonePlayers;
        public ZonePlayers ZonePlayers
        {
            get => _zonePlayers;
            set
            {
                _zonePlayers = value;
                RaisePropertyChanged(nameof(ZonePlayers));
            }
        }

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
    }
}
