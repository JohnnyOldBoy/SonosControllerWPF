using GalaSoft.MvvmLight;
using System;
using System.ComponentModel;

namespace SonosController.ViewModels
{
    public class GroupManagementViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public GroupManagementViewModel(int mode)
        {
            switch(mode)
            {
                case 0:
                    NewGroup();
                    break;
                case 1:
                    EditGroup();
                    break;
                default :
                    throw new IndexOutOfRangeException("Group management error");
            }
        }

        private void NewGroup()
        {
        
        }

        private void EditGroup()
        {

        }
    }
}
