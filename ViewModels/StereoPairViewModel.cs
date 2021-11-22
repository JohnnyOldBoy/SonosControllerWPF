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
            //stereoPairViewModels = _parentViewModel.StereoPairViewModels;
            SeparateStereoPair = new RelayCommand(SeparateSteroPairMethod);
        }

        private ZoneGroupTopologyViewModel _parentViewModel;
        //private ObservableCollection<StereoPairViewModel> stereoPairViewModels;
        private string _leftUUID = string.Empty;
        //private string _rightUUID = string.Empty;

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

        //private bool _separated = false;
        //public bool Separated 
        //{ 
        //    get => _separated;
        //    set
        //    {
        //        _separated = value;
        //        RaisePropertyChanged(nameof(Separated));
        //    }
        //}

        public ICommand SeparateStereoPair
        {
            get;
            private set;
        }

        private void SeparateSteroPairMethod()
        {
            StereoPair stereoPair = StereoPair[0];//Separated = true;
            ServiceUtils serviceUtils = new ServiceUtils();
            //MessageBox.Show(StereoPair[0].LeftUUID);
            
            string response = serviceUtils.SeparateStereoPair(StereoPair[0].LeftUUID, StereoPair[0].MasterPlayerIpAddress);

            int spIndex = -1;

            ObservableCollection<StereoPairViewModel> stereoPairViewModels = _parentViewModel.StereoPairViewModels;

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
