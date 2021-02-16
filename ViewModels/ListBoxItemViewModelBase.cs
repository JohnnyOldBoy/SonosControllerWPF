using System;
using GalaSoft.MvvmLight;

namespace SonosController.ViewModels
{
    public abstract class ListBoxItemViewModelBase : ViewModelBase, IListBoxItemViewModel
    {
        private Int32 selectedIndex;

        private void OnSelectedIndexChanged(object sender, SelectedIndexChangedEventArgs e)
        {
            SelectedIndexChanged.Invoke(this, e);
        }


        public Int32 SelectedIndex
        {
            get => selectedIndex;
            set
            {
                if (selectedIndex != value)
                {
                    selectedIndex = value;
                    OnSelectedIndexChanged(this, new SelectedIndexChangedEventArgs(this));
                }
            }
        }

        public SelectedIndexChangedEventHandler SelectedIndexChanged { get; set; }
    }
}
