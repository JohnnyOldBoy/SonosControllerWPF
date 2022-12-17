using Devices;
using GalaSoft.MvvmLight;
using Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;

namespace SonosController.ViewModels
{
    public class ZoneGroupViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ZoneGroupViewModel(ZonePlayers zonePlayers, ZoneGroup zoneGroup, XmlDocument sonosSystem)
        {
            _sonosSystem = sonosSystem;
            ZoneGroupCoordinator = _serviceUtils.GetPlayerByUUID(zonePlayers, zoneGroup.ZoneGroupCoordinator);
            ZoneGroupName = ZoneGroupCoordinator.RoomName;
            ZoneGroupMemberNames = zoneGroup.ZoneGroupMemberNames;
            ZoneGroupMembers = getZoneGroupMembers(zoneGroup);

            CommandEx = new CommandEx
            {
                CanExecuteFunc = obj => true,
                ExecuteFunc = CommandExMethod
            };
        }

        private XmlDocument _sonosSystem;

        private readonly ServiceUtils _serviceUtils = new ServiceUtils();

        private string _zoneGroupName = string.Empty;
        public string ZoneGroupName
        {
            get => _zoneGroupName;
            set
            {
                _zoneGroupName = value;
                RaisePropertyChanged(nameof(ZoneGroupName));
            }
        }

        private ZonePlayer _zoneGroupCoordinator;

        public ZonePlayer ZoneGroupCoordinator
        {
            get => _zoneGroupCoordinator;
            set
            {
                _zoneGroupCoordinator = value;
                RaisePropertyChanged(nameof(ZoneGroupCoordinator));
            }
        }

        private ObservableCollection<ZoneGroupMember> _zoneGroupMembers;
        public ObservableCollection<ZoneGroupMember> ZoneGroupMembers
        {
            get => _zoneGroupMembers;
            set
            {
                _zoneGroupMembers = value;
                RaisePropertyChanged(nameof(ZoneGroupMembers));
            }
        }

        private bool isSelected = false;
        public bool IsSelected
        {
            get => isSelected;
            set => isSelected = value;
        }

        private string zoneGroupMemberNames = string.Empty;
        public string ZoneGroupMemberNames
        {
            get => zoneGroupMemberNames;
            set => zoneGroupMemberNames = value;
        }

        public CommandEx CommandEx
        {
            get;
            set;
        }

        private ObservableCollection<ZoneGroupMember> getZoneGroupMembers(ZoneGroup zoneGroup)
        {
            ObservableCollection<ZoneGroupMember> zoneGroupMembers = new ObservableCollection<ZoneGroupMember>();

            //Add the group coordinator first
            ZoneGroupMember zoneGroupCoordinator = zoneGroup.ZoneGroupMemeberList.Find(x => x.IsCoordinator == true);
            zoneGroupMembers.Add(zoneGroupCoordinator);
            //Add the rest of the members that are not invisible
            foreach (ZoneGroupMember zoneGroupMember in zoneGroup.ZoneGroupMemeberList.FindAll(x => x.IsCoordinator == false).FindAll(x => x.Invisible == false))
            {
                zoneGroupMembers.Add(zoneGroupMember);
            }
            //Add the invisible members last
            foreach (ZoneGroupMember zoneGroupMember in zoneGroup.ZoneGroupMemeberList.FindAll(x => x.IsCoordinator == false).FindAll(x => x.Invisible == true))
            {
                zoneGroupMembers.Add(zoneGroupMember);
            }

            return zoneGroupMembers;
        }

        public void CommandExMethod(object parameter)
        {
            GroupManagementWindow groupManagementWindow = new GroupManagementWindow();
            groupManagementWindow.DataContext = new GroupManagementViewModel(1, _sonosSystem);
            groupManagementWindow.ShowDialog();
        }
    }
}