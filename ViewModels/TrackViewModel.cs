using MusicData;
using System.Collections.ObjectModel;

namespace Services
{
    public class TrackViewModel
    {

        public TrackViewModel(Track track, string albumName)
        {
            Track = track;
            AlbumName = albumName;
            Genre = track.Genres[0];
        }
        public Track Track { get; }

        public string AlbumName { get; }

        public string Genre { get; }
    }
}
