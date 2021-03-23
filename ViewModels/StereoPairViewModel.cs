using Devices;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using Services;
using System.Windows;
using System.Windows.Input;
using System;

namespace SonosController.ViewModels
{
    public class StereoPairViewModel : ViewModelBase
    {
        public StereoPairViewModel()
        {
            SeparateSteroPair = new RelayCommand(SeparateSteroPairMethod);
        }

        private string leftUUID = string.Empty;
        private string rightUUID = string.Empty;
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
            //MessageBox.Show(response);
        }
    }
}
