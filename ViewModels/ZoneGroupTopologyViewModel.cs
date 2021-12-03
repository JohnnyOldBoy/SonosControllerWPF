using Devices;
using GalaSoft.MvvmLight;
using Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml;

namespace SonosController.ViewModels
{
    public class ZoneGroupTopologyViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ZoneGroupTopologyViewModel(XmlDocument doc, ZonePlayersViewModel zonePlayersViewModel, MainWindowViewModel mainWindowViewModel)
        {
            localMainWindowViewModel = mainWindowViewModel;
            _serviceUtils = localMainWindowViewModel._serviceUtils;
            ZoneGroupTopology zoneGroupTopology = new ZoneGroupTopology();
            zoneGroupTopology.ZoneGroupList = _serviceUtils.GetZoneGroups(doc);
            StereoPairs stereoPairs = new StereoPairs();
            zoneGroupTopology.StereoPairs = stereoPairs;
            zoneGroupTopology.StereoPairs.StereoPairsList = _serviceUtils.GetStereoPairs(doc);

            ZoneGroupViewModels = new ObservableCollection<ZoneGroupViewModel>();
            foreach (ZoneGroup zoneGroup in zoneGroupTopology.ZoneGroupList)
            {
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

        private MainWindowViewModel localMainWindowViewModel;
        ServiceUtils _serviceUtils;

        private ObservableCollection<ZoneGroupViewModel> _zoneGroupViewModels;
        public ObservableCollection<ZoneGroupViewModel> ZoneGroupViewModels
        { 
            get => _zoneGroupViewModels;
            set
            {
                _zoneGroupViewModels = value;
                RaisePropertyChanged(nameof(ZoneGroupViewModels));
            }
        }

        private ObservableCollection<StereoPairViewModel> _stereoPairViewModels;
        public ObservableCollection<StereoPairViewModel> StereoPairViewModels
        {
            get => _stereoPairViewModels;
            set
            {
                _stereoPairViewModels = value;
                RaisePropertyChanged(nameof(StereoPairViewModels));
            }
        }
    }
}
