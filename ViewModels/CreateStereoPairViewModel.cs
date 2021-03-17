using Devices;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SonosController.ViewModels
{
    public class CreateStereoPairViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private List<string> zoneGroupMembersUuids = new List<string>();

        public CreateStereoPairViewModel()
        {
            CreateSteroPair = new RelayCommand(CreateStereoPairMethod);
            IsChecked = new RelayCommand<object>(IsCheckedMethod);

            ZonePlayers = _serviceUtils.GetZonePlayers();
            ZonePlayerCollection = new ObservableCollection<ZonePlayer>();
            ZoneGroupTopology _zoneGroupTopology = _serviceUtils.GetZoneGroupTopology(ZonePlayers.ZonePlayersList[0].PlayerIpAddress);

            foreach (ZoneGroup zoneGroup in _zoneGroupTopology.ZoneGroupList)
            {
                foreach (ZoneGroupMember zoneGroupMember in zoneGroup.ZoneGroupMemeberList)
                {
                    zoneGroupMembersUuids.Add(zoneGroupMember.UUID);
                }
            }

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
            get => _zonePlayerCollection;
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

        private StereoPair _newStereoPair;
        public StereoPair NewStereoPair
        {
            get => _newStereoPair;
            set
            {
                _newStereoPair = value;
                RaisePropertyChanged(nameof(NewStereoPair));
            }
        }

        private bool isVisible(ZoneGroupTopology _zoneGroupTopology, string UUID)
        {
            bool deviceVisible = false;

            int zoneGroupMemberUuidPos = zoneGroupMembersUuids.IndexOf(UUID);
            if (zoneGroupMemberUuidPos > -1)
            {
                deviceVisible = true;
            }
            else
            {
                StereoPairs stereoPairs = _zoneGroupTopology.StereoPairs;
                if (stereoPairs != null)
                {
                    StereoPair stereoPair = stereoPairs.StereoPairsList.Find(x => x.LeftUUID == UUID || x.RightUUID == UUID);
                    if (stereoPair != null)
                    {
                        deviceVisible = false;
                    }
                }
            }

            return deviceVisible;
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

        //private bool _buttonIsEnabled = true;
        //public bool ButtonIsEnabled 
        //{
        //    get => _buttonIsEnabled;
        //    set 
        //    { 
        //            _buttonIsEnabled = value;
        //            RaisePropertyChanged(nameof(ButtonIsEnabled));
        //    }
        //}

        public void IsCheckedMethod(object parameter)
        {
            if (parameter as string != string.Empty)
            {
                string[] parameters = parameter.ToString().Split('|');

                bool isChecked = bool.Parse(parameters[0]);
                if (isChecked)
                {
                    {
                        if (IsCheckedCount == 0)
                        {
                            _newStereoPair = new StereoPair();
                            _newStereoPair.LeftUUID = parameters[1];
                            _newStereoPair.PairName = parameters[2];
                            _isCheckedCount += 1;
                        }
                        else if (IsCheckedCount == 1)
                        {
                            _newStereoPair.RightUUID = parameters[1];
                            _isCheckedCount += 1;
                        }

                        if (IsCheckedCount > 2)
                        {
                            MessageBox.Show("Please select a maximum of two players to pair");
                        }
                    }
                }
                else
                {
                    if (IsCheckedCount > 0)
                    {
                        _isCheckedCount -= 1;
                    }

                }
            }
        }
    }
}
