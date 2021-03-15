using Devices;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace SonosController.ViewModels
{
    public class CreateStereoPairViewModel : ViewModelBase
    {
        public CreateStereoPairViewModel()
        {
            CreateSteroPair = new RelayCommand(CreateStereoPairMethod);
            IsChecked = new RelayCommand<object>(IsCheckedMethod);

            ZonePlayers = _serviceUtils.GetZonePlayers();
            ZonePlayerCollection = new ObservableCollection<ZonePlayer>();
            ZoneGroupTopology _zoneGroupTopology = _serviceUtils.GetZoneGroupTopology(ZonePlayers.ZonePlayersList[0].PlayerIpAddress);

            foreach (ZonePlayer zonePlayer in ZonePlayers.ZonePlayersList)
            {
                if (isVisible(_zoneGroupTopology, zonePlayer.UUID))
                {
                    ZonePlayerCollection.Add(zonePlayer);
                }
            }

        }
        private readonly ServiceUtils _serviceUtils = new ServiceUtils();

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

        private ZonePlayers _zonePlayers;
        public ZonePlayers ZonePlayers
        {
            get => _zonePlayers;
            set => _zonePlayers = value;
        }

        private bool isVisible(ZoneGroupTopology _zoneGroupTopology, string UUID)
        {
            StereoPairs stereoPairs = _zoneGroupTopology.StereoPairs;
            if (stereoPairs != null)
            {
                foreach (StereoPair stereoPair in stereoPairs.StereoPairsList)
                {
                    if (stereoPair.LeftUUID == UUID || stereoPair.RightUUID == UUID)
                    {
                        return false;
                        break;
                    }
                }
            }

            List<ZoneGroup> zoneGroupList = _zoneGroupTopology.ZoneGroupList;
            foreach (ZoneGroup zoneGroup in zoneGroupList)
            {
                foreach (ZoneGroupMember zoneGroupMember in zoneGroup.ZoneGroupMemeberList)
                {
                    if (zoneGroupMember.UUID == UUID && zoneGroupMember.Invisible)
                    {
                        return false;
                        break;
                    }
                }
            }
            return true;
        }

        public ICommand CreateSteroPair
        {
            get;
            private set;
        }

        public void CreateStereoPairMethod()
        {
            MessageBox.Show("Hello Billy");
        }

        public ICommand IsChecked
        {
            get;
            private set;
        }

        private int _isCheckedCount = 0;
        public int IsCheckedCount
        {
            get => _isCheckedCount;
            set
            {
                _isCheckedCount = value;
                RaisePropertyChanged(nameof(IsCheckedCount));
            }
        }

        public void IsCheckedMethod(object parameter)
        {
            if ((bool)parameter == true)
            {
                MessageBox.Show("True");
            }
            else
            {
                MessageBox.Show("False");
            }
        }
    }
}
