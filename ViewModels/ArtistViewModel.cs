using MusicData;
using System.Collections.ObjectModel;

namespace SonosController.ViewModels
{
    public class ArtistViewModel : TreeViewItemViewModelBase
    {
        private ObservableCollection<AlbumViewModel> albumNamePairs;

        public ArtistViewModel(Artist artist)
        {
            Artist = artist;
            AlbumNamePairs = new ObservableCollection<AlbumViewModel>();
        }

        public Artist Artist { get; }

        public ObservableCollection<AlbumViewModel> AlbumNamePairs
        {
            get => albumNamePairs;
            set => Set(ref albumNamePairs, value);
        }

    }
}
