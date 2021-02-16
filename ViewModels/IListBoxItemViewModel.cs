using System;
using System.ComponentModel;

namespace SonosController.ViewModels
{
    public interface IListBoxItemViewModel : INotifyPropertyChanged
    {
        Int32 SelectedIndex { get; set; }
        SelectedIndexChangedEventHandler SelectedIndexChanged { get; }
    }

    public delegate void SelectedIndexChangedEventHandler(object sender, SelectedIndexChangedEventArgs e);
}