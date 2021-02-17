using System;

namespace Devices
{
    public class ZonePlayerDetail
    {
        private string playerIpAddress = string.Empty;

        public string PlayerIpAddress 
        { 
            get => playerIpAddress; 
            set => playerIpAddress = value; 
        }

        private string detailName = string.Empty;
        public string DetailName 
        { 
            get => detailName; 
            set => detailName = value; 
        }

        private Object detailValue = string.Empty;
        public Object DetailValue 
        { 
            get => detailValue; 
            set => detailValue = value; 
        }
    }
}
