using GalaSoft.MvvmLight;
using Devices;
using Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace SonosController
{
    sealed class MainWindowViewModel : ViewModelBase
    {
        public ServiceUtils _serviceUtils;

        private ObservableCollection<ZonePlayer> _zonePlayersCollection;
        public ObservableCollection<ZonePlayer> ZonePlayersCollection
        {
            get { return _zonePlayersCollection; }
            set { _zonePlayersCollection = value; }
        }

        private ObservableCollection<ZoneGroup> _zoneGroupCollection;
        public ObservableCollection<ZoneGroup> ZoneGroupCollection 
        { 
            get => _zoneGroupCollection; 
            set => _zoneGroupCollection = value; 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            _serviceUtils = new ServiceUtils();
            ZonePlayers zonePlayers = _serviceUtils.GetZonePlayers();
            ZonePlayersCollection = new ObservableCollection<ZonePlayer>();
            
            foreach (ZonePlayer zonePlayer in zonePlayers.ZonePlayersList)
            {
                ZonePlayersCollection.Add(zonePlayer);
            }
            if (zonePlayers.ZonePlayersList.Any())
            {
                ZoneGroupTopology zoneGroupTopology =
                    _serviceUtils.GetZoneGroupTopology(zonePlayers.ZonePlayersList[0].PlayerIpAddress);
                ZoneGroupCollection = new ObservableCollection<ZoneGroup>();
                foreach (ZoneGroup zoneGroup in zoneGroupTopology.ZoneGroupList)
                {
                    ZoneGroupCollection.Add(zoneGroup);
                }
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