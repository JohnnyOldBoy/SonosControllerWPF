using Devices;
using GalaSoft.MvvmLight;
using Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace SonosController.ViewModels
{
    public class ZoneGroupTopologyViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ZoneGroupTopologyViewModel(ZonePlayersViewModel zonePlayersViewModel)
        {
            ZoneGroupTopology zoneGroupTopology = 
                _serviceUtils.GetZoneGroupTopology(zonePlayersViewModel.ZonePlayers.ZonePlayersList.FirstOrDefault().PlayerIpAddress);
            ZoneGroupViewModels = new ObservableCollection<ZoneGroupViewModel>();
            //ZoneGroupCollection = new ObservableCollection<ZoneGroup>();
            foreach (ZoneGroup zoneGroup in zoneGroupTopology.ZoneGroupList)
            {
                //ZoneGroupCollection.Add(zoneGroup);
                ZoneGroupViewModel zoneGroupViewModel = new ZoneGroupViewModel(zonePlayersViewModel.ZonePlayers, zoneGroup);
                ZoneGroupViewModels.Add(zoneGroupViewModel);
            }
            if (zoneGroupTopology.StereoPairs.StereoPairsList.Count > 0)
            {
                StereoPairViewModels = new ObservableCollection<StereoPairViewModel>();
                foreach (StereoPair stereoPair in zoneGroupTopology.StereoPairs.StereoPairsList)
                {
                    StereoPairViewModel stereoPairViewModel = new StereoPairViewModel(this)
                    {
                        PairName = stereoPair.PairName
                    };
                    stereoPairViewModel.StereoPair.Add(stereoPair);
                    stereoPairViewModel.ZonePlayers = zonePlayersViewModel.ZonePlayers;
                    StereoPairViewModels.Add(stereoPairViewModel);
                }

            }
        }
        ServiceUtils _serviceUtils = new ServiceUtils();

        private ObservableCollection<ZoneGroupViewModel> _zoneGroupViewModels;
        public ObservableCollection<ZoneGroupViewModel> ZoneGroupViewModels
        { 
            get => _zoneGroupViewModels;
            set
            {
                _zoneGroupViewModels = value;
                RaisePropertyChanged("ZoneGroupViewModels");
            }
        }

        //private ObservableCollection<ZoneGroup> _zoneGroupCollection;
        //public ObservableCollection<ZoneGroup> ZoneGroupCollection
        //{
        //    get => _zoneGroupCollection;
        //    set
        //    {
        //        _zoneGroupCollection = value;
        //        RaisePropertyChanged("SelectedZoneGroup");
        //    }
        //}

        private ObservableCollection<StereoPairViewModel> _stereoPairViewModels;
        public ObservableCollection<StereoPairViewModel> StereoPairViewModels
        {
            get => _stereoPairViewModels;
            set
            {
                _stereoPairViewModels = value;
                RaisePropertyChanged("StereoPairViewModels");
            }
        }
    }
}
