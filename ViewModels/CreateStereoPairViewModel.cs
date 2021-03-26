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
        //public CreateStereoPairViewModel()

        public CreateStereoPairViewModel(MainWindowViewModel mainWindowViewModel)
        {

            localMainWindowViewModel = mainWindowViewModel;
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

        private List<string> zoneGroupMembersUuids = new List<string>();
        private string _leftUUID = string.Empty;
        private string _rightUUID = string.Empty;

        private MainWindowViewModel localMainWindowViewModel;
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
            _serviceUtils.CreateStereoPair(ZonePlayers, _leftUUID, _rightUUID);
            StereoPair stereoPair = new StereoPair()
            {
                LeftUUID = _leftUUID,
                RightUUID = _rightUUID,
                PairName = _serviceUtils.GetPlayerByUUID(ZonePlayers, _leftUUID).RoomName,
                ChannelMapSet =_leftUUID + ":LF,LF;" + _rightUUID + ":RF,RF"
            };
            StereoPairViewModel stereoPairViewModel = new StereoPairViewModel(localMainWindowViewModel);
            stereoPairViewModel.PairName = _serviceUtils.GetPlayerByUUID(ZonePlayers, _leftUUID).RoomName;
            stereoPairViewModel.StereoPair.Add(stereoPair);
            if (localMainWindowViewModel.StereoPairViewModelsCollection != null)
            {
                localMainWindowViewModel.StereoPairViewModelsCollection.Add(stereoPairViewModel);
            }
            else
            {
                localMainWindowViewModel.StereoPairViewModelsCollection = new ObservableCollection<StereoPairViewModel>();
                localMainWindowViewModel.StereoPairViewModelsCollection.Add(stereoPairViewModel);
            }
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
                //RaisePropertyChanged(nameof(IsCheckedCount));
            }
        }

        public void IsCheckedMethod(object parameter)
        {
            if (parameter as string != string.Empty)
            {
                string[] parameters = parameter.ToString().Split('|');

                bool isChecked = bool.Parse(parameters[0]);
                if (isChecked)
                {
                    {
                        _isCheckedCount += 1;
                        if (IsCheckedCount == 1)
                        {
                            _leftUUID = parameters[1];
                        }
                        else if (IsCheckedCount == 2)
                        {
                            _rightUUID = parameters[1];
                        }
                        else if (IsCheckedCount > 2)
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

        public event PropertyChangedEventHandler PropertyChanged;

        // NotifyPropertyChanged will raise the PropertyChanged event passing the
        // source property that is being updated.
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}