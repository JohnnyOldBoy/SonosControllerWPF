using System.Collections.Generic;

namespace Devices
{
    public class ZonePlayers
    {
        private List<ZonePlayer> zonePlayersList = new List<ZonePlayer>();

        public List<ZonePlayer> ZonePlayersList
        {
            get => zonePlayersList;
            set => zonePlayersList = value;
        }
    }
}
