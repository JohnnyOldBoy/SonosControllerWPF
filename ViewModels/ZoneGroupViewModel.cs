using Devices;
using Services;
using System.Collections.ObjectModel;

namespace SonosController.ViewModels
{
    public class ZoneGroupViewModel
    {
        public ZoneGroupViewModel(ZonePlayers zonePlayers, ZoneGroup zoneGroup)
        {
            ServiceUtils serviceUtils = new ServiceUtils();
            ZoneGroupCoordinator = serviceUtils.getPlayerByUUID(zonePlayers, zoneGroup.ZoneGroupCoordinator);
            ZoneGroupName = ZoneGroupCoordinator.RoomName;
            ZoneGroupMembers = getZoneGroupMembers(zoneGroup);
        }

        private string _zoneGroupName = string.Empty;
        public string ZoneGroupName 
        { 
            get => _zoneGroupName; 
            set => _zoneGroupName = value; 
        }

        private ZonePlayer _zoneGroupCoordinator;

        public ZonePlayer ZoneGroupCoordinator 
        { 
            get => _zoneGroupCoordinator; 
            set => _zoneGroupCoordinator = value; 
        }

        private ObservableCollection<ZoneGroupMember> _zoneGroupMembers;
        public ObservableCollection<ZoneGroupMember> ZoneGroupMembers 
        { 
            get => _zoneGroupMembers; 
            set => _zoneGroupMembers = value; 
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
    }
}
