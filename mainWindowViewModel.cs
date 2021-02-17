using Devices;
using GalaSoft.MvvmLight;
using Services;
using SonosController.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace SonosController
{
    sealed class MainWindowViewModel : ViewModelBase
    {
        public ServiceUtils _serviceUtils;

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

        private ObservableCollection<ZoneGroup> _zoneGroupCollection;
        public ObservableCollection<ZoneGroup> ZoneGroupCollection 
        { 
            get => _zoneGroupCollection; 
            set => _zoneGroupCollection = value; 
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

        //public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            _serviceUtils = new ServiceUtils();

            PropertyChanged += OnPropertyChangedHandler;

            ZonePlayers zonePlayers = _serviceUtils.GetZonePlayers();
            ZonePlayerCollection = new ObservableCollection<ZonePlayer>();
            foreach (ZonePlayer zonePlayer in zonePlayers.ZonePlayersList)
            {
                ZonePlayerCollection.Add(zonePlayer);
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


            List<ZonePlayerDetail> zonePlayerDetails = _serviceUtils.getPlayerDetails(zonePlayers);
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

        }

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
        }


    }
}