using Devices;
using Services;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace SonosController.ViewModels
{
    //Make sure your VIEWMODELS derive from ViewModelBase
    //Do the same with zonegroupviewmodel
    public class StereoPairViewModel : ViewModelBase
    {
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
    }
}
 