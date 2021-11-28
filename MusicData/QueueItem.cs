using System;
using System.Collections.Generic;

namespace MusicData
{
    [Serializable()]
    public class QueueItem
    {
        private string zoneGroupCoordinator = string.Empty;
        public string ZoneGroupCoordinator
        {
            get => zoneGroupCoordinator;
            set => zoneGroupCoordinator = value;
        }

        private string qiTitle = string.Empty;
        public string QiTitle 
        { 
            get => qiTitle; 
            set => qiTitle = value; 
        }

        private string qiArtist = string.Empty;
        public string QiArtist
        { 
            get => qiArtist; 
            set => qiArtist = value; 
        }

        private string qiAlbum = string.Empty;
        public string QiAlbum
        { 
            get => qiAlbum; 
            set => qiAlbum = value; 
        }

        private string qiAlbumArt = string.Empty;
        public string QiAlbumArt
        { 
            get => qiAlbumArt; 
            set => qiAlbumArt = value; 
        }

        private string qiUri = string.Empty;
        public string QiUri
        { 
            get => qiUri; 
            set => qiUri = value; 
        }
    }
}
