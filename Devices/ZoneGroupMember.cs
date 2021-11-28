namespace Devices
{
    public class ZoneGroupMember
    {
        private string uUID = string.Empty;
        public string UUID
        {
            get => uUID;
            set => uUID = value;
        }

        private string zoneName = string.Empty;
        public string ZoneName
        {
            get => zoneName;
            set => zoneName = value;
        }

        private string channelMapSet = string.Empty;
        public string ChannelMapSet
        {
            get => channelMapSet;
            set => channelMapSet = value;
        }

        private bool isCoordinator = false;
        public bool IsCoordinator 
        { 
            get => isCoordinator; 
            set => isCoordinator = value; 
        }

        private bool invisible = false;
        public bool Invisible
        {
            get => invisible;
            set => invisible = value;
        }
    }
}
