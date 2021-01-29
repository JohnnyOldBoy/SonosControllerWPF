using System;
using System.Collections.Generic;

namespace MusicData
{
    [Serializable()]
    public class Artist
    {
        private string artistName = string.Empty;
        public string ArtistName
        {
            get => artistName;
            set => artistName = value;
        }

        private List<string> artistAlbums = new List<string>();
        public List<string> ArtistAlbums
        {
            get => artistAlbums;
            set => artistAlbums = value;
        }

    }
}
