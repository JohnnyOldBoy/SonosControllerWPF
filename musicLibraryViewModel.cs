using GalaSoft.MvvmLight;
using Services;
using SonosController.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace SonosController
{
    sealed class MusicLibraryViewModel : ViewModelBase
    {
        private MusicLibrary _musicLibrary = new MusicLibrary();
        
        private AlbumViewModel _selectedAlbum;
        public AlbumViewModel SelectedAlbum
        {
            get => _selectedAlbum;
            set
            {
                _selectedAlbum = value;
                RaisePropertyChanged("SelectedAlbum");

            }
        }

        private AlbumViewModel _selectedArtistSelectedAlbum;
        public AlbumViewModel SelectedArtistSelectedAlbum
        {
            get => _selectedArtistSelectedAlbum;
            set
            {
                _selectedArtistSelectedAlbum = value;
                RaisePropertyChanged("SelectedArtistAlbum");

            }
        }

        public ObservableCollection<ArtistViewModel> Artists { get; } = new ObservableCollection<ArtistViewModel>();

        public ObservableCollection<TrackViewModel> Tracks { get; } = new ObservableCollection<TrackViewModel>();
        
        public ObservableCollection<AlbumViewModel> Albums { get; } = new ObservableCollection<AlbumViewModel>();
        
        public ICollectionView ArtistTracksCollectionView { get; }

        public ICollectionView AlbumTracksCollectionView { get; }

        public MusicLibraryViewModel()
        {

            foreach (var track in _musicLibrary.TrackInfo.TrackList.OrderBy(s => _musicLibrary.GetAlbumTitle(s.AlbumId)).ThenBy(s => s.TrackNumber))
            {
                var trackViewModel = new TrackViewModel(track, _musicLibrary.GetAlbumTitle(track.AlbumId));
                Tracks.Add(trackViewModel);
            }
            
            foreach (var album in _musicLibrary.AlbumInfo.AlbumList.OrderBy(s => s.AlbumName))
            {
                Albums.Add(new AlbumViewModel(album));
            }

            PropertyChanged += OnPropertyChangedHandler;


            ArtistTracksCollectionView = new ListCollectionView(Tracks);

            ArtistTracksCollectionView.Filter = t =>
            {
                if (t is TrackViewModel trackDisplay)
                {
                    if (trackDisplay.Track.AlbumId != "")
                    {
                        return false;
                    }
                }

                return false;
            };
            
            AlbumTracksCollectionView = new ListCollectionView(Tracks);

            SelectedAlbum = Albums.FirstOrDefault();
            AlbumTracksCollectionView.Filter = t =>
            {
                if (t is TrackViewModel trackDisplay)
                {
                    if (SelectedAlbum != null && trackDisplay.Track.AlbumId == SelectedAlbum.AlbumId)
                    {
                        return true;
                    }
                }
                return false;
            };

            foreach (var artist in _musicLibrary.ArtistInfo.ArtistList.OrderBy(s => s.ArtistName))
            {
                var artistDisplay = new ArtistViewModel(artist);

                foreach (var album in _musicLibrary.AlbumInfo.AlbumList.Where(a => artist.ArtistAlbums.Contains(a.AlbumId)))
                {
                    var albumViewModel = new AlbumViewModel(album);
                    albumViewModel.IsSelectedChanged += IsSelectedItemChanged;
                    artistDisplay.AlbumNamePairs.Add(albumViewModel);
                }

                artistDisplay.IsSelectedChanged += IsSelectedItemChanged;
                Artists.Add(artistDisplay);
            }
        }

        private void IsSelectedItemChanged(object sender, IsSelectedItemChangedEventArgs e)
        {
            if (e.Item.IsSelected)
            {
                if (e.Item is ArtistViewModel artist)
                {

                    ArtistTracksCollectionView.Filter = t =>
                    {
                        if (t is TrackViewModel trackDisplay)
                        {
                            if (trackDisplay.Track.ArtistName == artist.Artist.ArtistName)
                            {
                                return false; //To clear the selected album track grid if what is selected is not an album
                            }
                        }

                        return false;
                    };

                }
                else if (e.Item is AlbumViewModel album)
                {

                    ArtistTracksCollectionView.Filter = t =>
                    {
                        if (t is TrackViewModel trackDisplay)
                        {
                            if (trackDisplay.Track.AlbumId == album.AlbumId)
                            {
                                return true;
                            }
                        }

                        return false;
                    };
                }
            }
            ArtistTracksCollectionView.Refresh();
        }

        private void OnPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {

            if (e.PropertyName == nameof(SelectedAlbum))
            {
                AlbumTracksCollectionView.Refresh();
            }
        }
    }
}