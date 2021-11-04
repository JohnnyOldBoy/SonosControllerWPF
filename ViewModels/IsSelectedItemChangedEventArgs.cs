using System;

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
