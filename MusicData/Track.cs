using System;

namespace MusicData
{
    [Serializable()]
    public class Track
    {
        private string title = string.Empty;
        public string Title
        {
            get => title;
            set => title = value;
        }

        private string artistName = string.Empty;
        public string ArtistName
        {
            get => artistName;
            set => artistName = value;
        }

        private string genres;
        public string Genres
        {
            get => genres;
            set => genres = value;
        }

        private string albumId = string.Empty;
        public string AlbumId
        {
            get => albumId;
            set => albumId = value;
        }

        private uint trackNumber = 0;
        public uint TrackNumber
        {
            get => trackNumber;
            set => trackNumber = value;
        }

        private string trackUrl = string.Empty;
        public string TrackUrl
        {
            get => trackUrl;
            set => trackUrl = value;
        }
    }
}
