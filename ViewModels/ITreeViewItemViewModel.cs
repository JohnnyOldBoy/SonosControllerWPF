using System;
using System.ComponentModel;

namespace SonosController.ViewModels
{
    public interface ITreeViewItemViewModel : INotifyPropertyChanged
    {
        bool IsSelected { get; set; }
        IsSelectedChangedEventHandler IsSelectedChanged { get; }
    }

    public delegate void IsSelectedChangedEventHandler(object sender, IsSelectedItemChangedEventArgs e);
}