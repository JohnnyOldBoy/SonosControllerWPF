using Devices;
using Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SonosController
{
    sealed class mainWindowViewModel : INotifyPropertyChanged
    {
        public ServiceUtils serviceUtils;

        private ObservableCollection<ZonePlayer> _ZonePlayersOC;
        public ObservableCollection<ZonePlayer> ZonePlayersOC
        {
            get { return _ZonePlayersOC; }
            set { _ZonePlayersOC = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public mainWindowViewModel()
        {
            serviceUtils = new ServiceUtils();
            ZonePlayers zonePlayers = serviceUtils.GetZonePlayers();
            _ZonePlayersOC = new ObservableCollection<ZonePlayer>();
            
            foreach (ZonePlayer zonePlayer in zonePlayers.ZonePlayersList)
            {
                _ZonePlayersOC.Add(zonePlayer);
            }

            ZoneGroupTopology zoneGroupTopology = serviceUtils.GetZoneGroupTopology(zonePlayers.ZonePlayersList[0].PlayerIpAddress);
        }

        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}