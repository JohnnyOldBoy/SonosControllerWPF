namespace Devices
{
    public class StereoPair
    {
        private string pairName = string.Empty;

        public string PairName 
        { 
            get => pairName; 
            set => pairName = value; 
        }

        private string channelMapSet = string.Empty;

        public string ChannelMapSet 
        { 
            get => channelMapSet; 
            set => channelMapSet = value; 
        }

        private string leftUUID = string.Empty;

        public string LeftUUID 
        { 
            get => leftUUID; 
            set => leftUUID = value; 
        }

        private string rightUUID = string.Empty;

        public string RightUUID
        {
            get => rightUUID;
            set => rightUUID = value;
        }

        private string _masterPlayerIpAddress;
        public string MasterPlayerIpAddress
        {
            get => _masterPlayerIpAddress;
            set => _masterPlayerIpAddress = value;
        }

    }
}
