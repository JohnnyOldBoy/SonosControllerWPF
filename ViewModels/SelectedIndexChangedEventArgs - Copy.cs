using System;

namespace SonosController.ViewModels
{
    public class SelectedIndexChangedEventArgs : EventArgs
    {
        public IListBoxItemViewModel Item { get; }

        public SelectedIndexChangedEventArgs(IListBoxItemViewModel item)
        {
            Item = item;
        }
    }
}
