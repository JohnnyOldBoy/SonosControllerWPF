﻿using Devices;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace SonosController.ViewModels
{
    public class StereoPairViewModel : ViewModelBase
    {
        public StereoPairViewModel()
        {
            SeparateSteroPair = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(SeparateSteroPairMethod);
        }

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

        public ICommand SeparateSteroPair
        {
            get;
            private set;
        }

        private void SeparateSteroPairMethod()
        {
            MessageBox.Show("You Have Clicked the button");
        }
    }
}
 