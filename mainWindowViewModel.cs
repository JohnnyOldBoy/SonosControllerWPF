using GalaSoft.MvvmLight;
using Devices;
using Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace SonosController
{
    sealed class mainWindowViewModel : ViewModelBase
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
            if (zonePlayers.ZonePlayersList.Any())
            {
                ZoneGroupTopology zoneGroupTopology =
                    serviceUtils.GetZoneGroupTopology(zonePlayers.ZonePlayersList[0].PlayerIpAddress);
            }
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