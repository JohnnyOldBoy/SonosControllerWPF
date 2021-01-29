using System;
using System.Collections.Generic;

namespace MusicData
{
    [Serializable()]
    public class Tracks
    {
        private List<Track> trackList = new List<Track>();
        public List<Track> TrackList
        {
            get => trackList;
            set => trackList = value;
        }
    }
}

