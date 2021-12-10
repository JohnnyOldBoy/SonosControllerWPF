using System;
using System.Collections.Generic;

namespace Devices
{
    public class ZoneGroup : IComparable
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

        private string zoneGroupMemberNames = string.Empty;
        public string ZoneGroupMemberNames 
        { 
            get => zoneGroupMemberNames; 
            set => zoneGroupMemberNames = value; 
        }

        private bool isSelected = false;
        public bool IsSelected 
        { 
            get => isSelected; 
            set => isSelected = value; 
        }

        public int CompareTo(object obj)
        {
            ZoneGroup zoneGroupToCompare = obj as ZoneGroup;
            if (String.Compare(zoneGroupToCompare.ZoneGroupName, ZoneGroupName) < 0)
            {
                return 1;
            }
            if (String.Compare(zoneGroupToCompare.ZoneGroupName, ZoneGroupName) > 0)
            {
                return -1;
            }

            // The orders are equivalent.
            return 0;
        }
    }

}
