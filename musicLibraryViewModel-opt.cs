using GalaSoft.MvvmLight;
using Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using SonosController.ViewModels;

namespace SonosController
{
    sealed class MusicLibraryViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly MusicLibrary _musicLibrary = new MusicLibrary();

        private AlbumViewModel _selectedAlbum;
        public AlbumViewModel SelectedAlbum
        {
            get => _selectedAlbum;
            set
            {
                _selectedAlbum = value;
                RaisePropertyChanged(nameof(SelectedAlbum));
            }
        }

        private AlbumViewModel _selectedArtistSelectedAlbum;
        public AlbumViewModel SelectedArtistSelectedAlbum
        {
            get => _selectedArtistSelectedAlbum;
            set
            {
                _selectedArtistSelectedAlbum = value;
                RaisePropertyChanged(nameof(SelectedArtistSelectedAlbum));
            }
        }

        private string _selectedGenre;
        public string SelectedGenre
        {
            get => _selectedGenre;
            set
            {
                _selectedGenre = value;
                RaisePropertyChanged(nameof(SelectedGenre));
            }
        }

        public ObservableCollection<ArtistViewModel> Artists { get; } = new ObservableCollection<ArtistViewModel>();
        public ObservableCollection<TrackViewModel> Tracks { get; } = new ObservableCollection<TrackViewModel>();
        public ObservableCollection<AlbumViewModel> Albums { get; } = new ObservableCollection<AlbumViewModel>();
        public ObservableCollection<string> Genres { get; } = new ObservableCollection<string>();

        public ICollectionView TracksCollectionView { get; }

        public MusicLibraryViewModel()
        {
            try
            {
                LoadTracks();
                LoadAlbums();
                LoadArtists();
                LoadGenres();
                TracksCollectionView = new ListCollectionView(Tracks);
                SelectedAlbum = Albums.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Log the exception or show an error message to the user.
                Console.WriteLine($"Error loading music library: {ex.Message}");
            }
        }

        private void LoadTracks()
        {
            foreach (var track in _musicLibrary.TrackInfo.TrackList.OrderBy(s => _musicLibrary.GetAlbumTitle(s.AlbumId)).ThenBy(s => s.TrackNumber))
            {
                var trackViewModel = new TrackViewModel(track, _musicLibrary.GetAlbumTitle(track.AlbumId));
                Tracks.Add(trackViewModel);
            }
        }

        private void LoadAlbums()
        {
            foreach (var album in _musicLibrary.AlbumInfo.AlbumList.OrderBy(s => s.AlbumName))
            {
                Albums.Add(new AlbumViewModel(album));
            }
        }

        private void LoadArtists()
        {
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

        private void LoadGenres()
        {
            foreach (var genre in _musicLibrary.GenreInfo.GenreList.OrderBy(s => s))
            {
                Genres.Add(genre);
            }
        }

        private void IsSelectedItemChanged(object sender, IsSelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.Item.IsSelected)
                {
                    if (e.Item is ArtistViewModel artist)
                    {
                        TracksCollectionView.Filter = t =>
                        {
                            if (t is TrackViewModel trackDisplay)
                            {
                                if (trackDisplay.Track.ArtistName == artist.Artist.ArtistName)
                                {
                                    return true;
                                }
                            }

                            return false;
                        };
                    }
                    else if (e.Item is AlbumViewModel album)
                    {
                        TracksCollectionView.Filter = t =>
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


                TracksCollectionView.Refresh();
            }
            catch (Exception ex) { };


        }

        private void OnPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {

            if (e.PropertyName == nameof(SelectedAlbum))
            {
                AlbumTracksCollectionView.Refresh();
            }
            if (e.PropertyName == nameof(SelectedGenre))
            {
                GenreTracksCollectionView.Refresh();
            }
        }

    }
}

