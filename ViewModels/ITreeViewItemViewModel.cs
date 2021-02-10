using System;
using System.ComponentModel;
using SonosController.ViewModels;

namespace SonosControllerWPF.ViewModels
{
    public interface ITreeViewItemViewModel : INotifyPropertyChanged
    {
        bool IsSelected { get; set; }
        IsSelectedChangedEventHandler IsSelectedChanged { get; }
    }

    public delegate void IsSelectedChangedEventHandler(object sender, IsSelectedItemChangedEventArgs e);
}