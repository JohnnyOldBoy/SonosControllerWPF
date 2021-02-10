using System;
using MusicData;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using SonosController.ViewModels;

namespace SonosControllerWPF.ViewModels
{
    public class ArtistViewModel : TreeViewItemViewModelBase
    {
        private ObservableCollection<AlbumViewModel> albumNamePairs;

        public ArtistViewModel(Artist artist)
        {
            Artist = artist;
            AlbumNamePairs = new ObservableCollection<AlbumViewModel>();
        }

        private void OnIsSelectedChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }

        public Artist Artist { get; }


        public ObservableCollection<AlbumViewModel> AlbumNamePairs
        {
            get => albumNamePairs;
            set => Set(ref albumNamePairs, value);
        }

    }
}
