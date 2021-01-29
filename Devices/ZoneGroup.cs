using System.Collections.Generic;

namespace Devices
{
    public class ZoneGroup
    {

        private string zoneGroupCoordinator = string.Empty;
        public string ZoneGroupCoordinator
        {
            get => zoneGroupCoordinator;
            set => zoneGroupCoordinator = value;
        }
   
        private string zoneGroupId = string.Empty;
        public string ZoneGroupId
        {
            get => zoneGroupId;
            set => zoneGroupId = value;
        }

        private string zoneGroupName = string.Empty;
        public string ZoneGroupName
        {
            get => zoneGroupName;
            set => zoneGroupName = value;
        }

        private List<ZoneGroupMember> zoneGroupMemeberList = new List<ZoneGroupMember>();
        public List<ZoneGroupMember> ZoneGroupMemeberList
        {
            get => zoneGroupMemeberList;
            set => zoneGroupMemeberList = value;
        }

    }

}
