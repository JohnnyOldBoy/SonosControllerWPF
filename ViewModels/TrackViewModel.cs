using MusicData;
namespace SonosController
{
    public class TrackViewModel
    {

        public TrackViewModel(Track track, string albumName)
        {
            Track = track;
            AlbumName = albumName;
        }
        public Track Track { get; }

        public string AlbumName { get; }
    }
}
