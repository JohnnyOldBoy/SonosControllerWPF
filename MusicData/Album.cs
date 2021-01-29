using System;

namespace MusicData
{
    [Serializable()]
    public class Album
    {
        private string albumName = string.Empty;
        public string AlbumName
        {
            get => albumName;
            set => albumName = value;
        }

        private string albumArtist = string.Empty;
        public string AlbumArtist
        {
            get => albumArtist;
            set => albumArtist = value;
        }

        private string albumUrl = string.Empty;
        public string AlbumUrl
        {
            get => albumUrl;
            set => albumUrl = value;
        }

        private string albumId = string.Empty;
        public string AlbumId
        {
            get => albumId;
            set => albumId = value;
        }
    }
}
