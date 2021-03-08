using Devices;
using Services;
using System.Collections.ObjectModel;

namespace SonosController.ViewModels
{
    public class StereoPairViewModel
    {
        private string _pairName = string.Empty;

        public string PairName 
        { 
            get => _pairName; 
            set => _pairName = value; 
        }

        private ObservableCollection<StereoPair> _stereoPair = new ObservableCollection<StereoPair>();
        public ObservableCollection<StereoPair> StereoPair
        { 
            get => _stereoPair; 
            set => _stereoPair = value; 
        }
    }
}
 