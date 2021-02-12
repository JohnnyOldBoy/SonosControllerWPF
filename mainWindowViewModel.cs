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

#pragma warning disable CS0108 // 'mainWindowViewModel.PropertyChanged' hides inherited member 'ObservableObject.PropertyChanged'. Use the new keyword if hiding was intended.
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0108 // 'mainWindowViewModel.PropertyChanged' hides inherited member 'ObservableObject.PropertyChanged'. Use the new keyword if hiding was intended.

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

#pragma warning disable CS0628 // 'mainWindowViewModel.OnPropertyChange(string)': new protected member declared in sealed type
        protected void OnPropertyChange(string propertyName)
#pragma warning restore CS0628 // 'mainWindowViewModel.OnPropertyChange(string)': new protected member declared in sealed type
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}