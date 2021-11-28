using Devices;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SonosController.ViewModels
{
    public class StereoPairViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public StereoPairViewModel(ZoneGroupTopologyViewModel parentViewModel)
        {
            _parentViewModel = parentViewModel;
            SeparateStereoPair = new RelayCommand(SeparateSteroPairMethod);
        }

        private ZoneGroupTopologyViewModel _parentViewModel;

        private string _pairName = string.Empty;
        public string PairName 
        { 
            get => _pairName;
            set
            {
                _pairName = value; 
                RaisePropertyChanged(nameof(PairName));
            }
        }

        private ObservableCollection<StereoPair> _stereoPair = new ObservableCollection<StereoPair>();
        public ObservableCollection<StereoPair> StereoPair
        { 
            get => _stereoPair;
            set
            {
                _stereoPair = value; 
                RaisePropertyChanged(nameof(StereoPair));
            }
        }

        private ZonePlayers _zonePlayers;
        public ZonePlayers ZonePlayers 
        { 
            get => _zonePlayers; 
            set 
            { 
                _zonePlayers = value;
                RaisePropertyChanged(nameof(ZonePlayers));
            }
        }

        public ICommand SeparateStereoPair
        {
            get;
            private set;
        }

        private void SeparateSteroPairMethod()
        {
            StereoPair stereoPair = StereoPair[0];
            ServiceUtils serviceUtils = new ServiceUtils();
            
            string response = serviceUtils.SeparateStereoPair(stereoPair.LeftUUID, stereoPair.MasterPlayerIpAddress);

            int spIndex = -1;

            ObservableCollection<StereoPairViewModel> stereoPairViewModels = _parentViewModel.StereoPairViewModels;

            foreach (StereoPairViewModel stereoPairViewModel in stereoPairViewModels)
            {
                if (stereoPairViewModel.Equals(this))
                {
                    spIndex = stereoPairViewModels.IndexOf(stereoPairViewModel);
                }

            }
            if (spIndex != -1)
            {
                stereoPairViewModels.RemoveAt(spIndex);
                _parentViewModel.RaisePropertyChanged();
            }
        }
    }
}
