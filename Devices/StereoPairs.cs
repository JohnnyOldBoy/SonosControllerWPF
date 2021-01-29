using System.Collections.Generic;

namespace Devices
{
    public class StereoPairs
    {
        List<StereoPair> stereoPairsList = new List<StereoPair>();

        public List<StereoPair> StereoPairsList 
        { 
            get => stereoPairsList; 
            set => stereoPairsList = value; 
        }
    }
}
