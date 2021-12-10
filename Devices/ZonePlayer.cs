using System;

namespace Devices
{
    public class ZonePlayer : System.IComparable
    {
        private string roomName;
        public string RoomName
        {
            get => roomName;
            set => roomName = value;
        }

        private string modelName;
        public string ModelName
        {
            get => modelName;
            set => modelName = value;
        }

        private string softwareVersion;
        public string SoftwareVersion
        {
            get => softwareVersion;
            set => softwareVersion = value;
        }

        private string swGen;
        public string SWGen
        {
            get => swGen;
            set => swGen = value;
        }

        private string hardwareVersion;
        public string HardwareVersion
        {
            get => hardwareVersion;
            set => hardwareVersion = value;
        }

        private string serialNum;
        public string SerialNum
        {
            get => serialNum;
            set => serialNum = value;
        }

        private string macAddress;
        public string MACAddress
        {
            get => macAddress;
            set => macAddress = value;
        }

        private string uUID;
        public string UUID
        {
            get => uUID;
            set => uUID = value;
        }

        private string playerIpAddress;
        public string PlayerIpAddress
        {
            get => playerIpAddress;
            set => playerIpAddress = value;
        }

        private string iconURL;
        public string IconURL
        {
            get => iconURL;
            set => iconURL = value;
        }

        public int CompareTo(object obj)
        {
            ZonePlayer zonePlayerToCompare = obj as ZonePlayer;
            if (String.Compare(zonePlayerToCompare.RoomName, RoomName) < 0)
            {
                return 1;
            }
            if (String.Compare(zonePlayerToCompare.RoomName, RoomName) > 0)
            {
                return -1;
            }

            // The orders are equivalent.
            return 0;
        }
    }
}
