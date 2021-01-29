using System;
using System.Collections.Generic;

namespace MusicData
{
    [Serializable()]
    public class Artists
    {
        private List<Artist> artistList = new List<Artist>();
        public List<Artist> ArtistList
        {
            get => artistList;
            set => artistList = value;
        }
    }
}
