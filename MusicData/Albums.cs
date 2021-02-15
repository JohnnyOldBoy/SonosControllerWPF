using System;
using System.Collections.Generic;

namespace MusicData
{
    [Serializable()]
    public class Albums
    {
        private List<Album> albumList = new List<Album>();
        public List<Album> AlbumList
        {
            get => albumList;
            set => albumList = value;
        }
    }
}
