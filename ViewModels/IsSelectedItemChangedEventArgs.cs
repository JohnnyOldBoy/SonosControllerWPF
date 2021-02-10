using System;
using SonosControllerWPF.ViewModels;

namespace SonosController.ViewModels
{
    public class IsSelectedItemChangedEventArgs : EventArgs
    {
        public ITreeViewItemViewModel Item { get; }

        public IsSelectedItemChangedEventArgs(ITreeViewItemViewModel item)
        {
            Item = item;
        }
    }
}
