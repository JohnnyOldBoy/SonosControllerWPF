﻿using System;
using System.Collections.Generic;
using System.Xml;

namespace Devices
{
    public class ZoneGroupTopology
    {

        private List<ZoneGroup> zoneGroupList = new List<ZoneGroup>();

        public List<ZoneGroup> ZoneGroupList
        {
            get => zoneGroupList;
            set => zoneGroupList = value;
        }

        private StereoPairs stereoPairs;
        
        public StereoPairs StereoPairs
        { 
            get => stereoPairs; 
            set => stereoPairs = value; 
        }

    }
}
