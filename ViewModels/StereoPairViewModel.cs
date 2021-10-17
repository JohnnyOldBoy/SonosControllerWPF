using Devices;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace SonosController.ViewModels
{
    public class StereoPairViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public StereoPairViewModel(ZoneGroupTopologyViewModel parentViewModel)
        {
            _parentViewModel = parentViewModel;
            SeparateSteroPair = new RelayCommand(SeparateSteroPairMethod);
            stereoPairViewModels = _parentViewModel.StereoPairViewModels;
        }

        public StereoPairViewModel(MainWindowViewModel parentViewModel)
        {
            SeparateSteroPair = new RelayCommand(SeparateSteroPairMethod);
            stereoPairViewModels = parentViewModel.StereoPairViewModelsCollection;
        }

        private ZoneGroupTopologyViewModel _parentViewModel;
        private ObservableCollection<StereoPairViewModel> stereoPairViewModels;
        private string _leftUUID = string.Empty;
        private string _rightUUID = string.Empty;
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

        public ICommand SeparateSteroPair
        {
            get;
            private set;
        }
        
        private void SeparateSteroPairMethod()
        {
            ServiceUtils serviceUtils = new ServiceUtils();
            string response = serviceUtils.SeparateStereoPair(ZonePlayers, StereoPair[0].LeftUUID);
            int spIndex = -1;

                foreach (StereoPairViewModel stereoPairViewModel in stereoPairViewModels)
                {
                    if (stereoPairViewModel._leftUUID == _leftUUID)
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
