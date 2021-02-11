using MusicData;
using SonosController.ViewModels;

namespace SonosController.ViewModels
{
    public class AlbumViewModel : TreeViewItemViewModelBase
    {
        public AlbumViewModel(Album album)
        {
            Album = album;
        }
        public Album Album { get; }

        public string AlbumId
        {
            get => Album.AlbumId;
        }

        public string AlbumName
        {
            get => Album.AlbumName;
        }

    }
}
