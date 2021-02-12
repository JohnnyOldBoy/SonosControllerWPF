using GalaSoft.MvvmLight;

namespace SonosController.ViewModels
{
    public abstract class TreeViewItemViewModelBase : ViewModelBase, ITreeViewItemViewModel
    {
        private bool isSelected;

        private void OnIsSelectedChanged(object sender, IsSelectedItemChangedEventArgs e)
        {
            IsSelectedChanged.Invoke(this, e);
        }


        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnIsSelectedChanged(this, new IsSelectedItemChangedEventArgs(this));
                }
            }
        }

        public IsSelectedChangedEventHandler IsSelectedChanged { get; set; }
    }
}
