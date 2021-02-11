using GalaSoft.MvvmLight;
using MusicData;
using Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace SonosController
{
    sealed class musicLibraryViewModel : ViewModelBase
    {
        /// <summary>
        /// Creating the object specific to SonosControllerWPF from MusicData object instances created by Services.MusicLibrary
        /// </summary>
 
        // Get the Music Library
        private MusicLibrary musicLibrary = new MusicLibrary();
        
        // Local object to hold the selected album from the Albums tab
        private Album _SelectedAlbum;
        public Album SelectedAlbum
        {
            get => _SelectedAlbum;
            set
            {
                _SelectedAlbum = value;
                RaisePropertyChanged("SelectedAlbum");
            }
        }

        // Local object to hold the selected album from the Artists tab
        private AlbumInfo _SelectedArtistSelectedAlbum;
        public AlbumInfo SelectedArtistSelectedAlbum
        {
            get => _SelectedArtistSelectedAlbum;
            set
            {
                _SelectedArtistSelectedAlbum = value;
                RaisePropertyChanged("SelectedArtistAlbum");
            }
        }


        // Local object to hold Track info with additional data for display
        // purposes. Used to populate TracksView on the Tracks tab
        private ObservableCollection<TrackDisplay> _TracksOC;
        
        public ObservableCollection<TrackDisplay> TracksOC
        {
            get { return _TracksOC; }
            set
            {
                _TracksOC = value;
                RaisePropertyChanged(nameof(TracksOC));
            }
        }

        // Local object to holde the Observable Collection equivalent of
        // the Artists object instance. Used to populate ArtistsTreeView
        // on the Artists tab
        private ObservableCollection<ArtistDisplay> _ArtistsListOC;

        public ObservableCollection<ArtistDisplay> ArtistsListOC
        { 
            get => _ArtistsListOC;
            set { _ArtistsListOC = value;
                RaisePropertyChanged(nameof(ArtistsListOC));
            }
        }

        // Local object to holde the Observable Collection equivalent of
        // the Albums object instance. Used to populate AlbumsView on the
        // Albums tab
        private ObservableCollection<Album> _AlbumsOC;

        public ObservableCollection<Album> AlbumsOC
        {
            get { return _AlbumsOC; }
            set
            {
                _AlbumsOC = value;
                RaisePropertyChanged(nameof(AlbumsOC));
            }
        }

        // Local object to hold a set of tracks for a selected album in
        // AlbumsView on the Albums tab
        private ICollectionView _AlbumTracksCollectionView;
        public ICollectionView AlbumTracksCollectionView
        {
            get 
            {
                return _AlbumTracksCollectionView; 
            }
            set
            {
                _AlbumTracksCollectionView = value;
                RaisePropertyChanged(nameof(_AlbumTracksCollectionView));
            }
        }

        public musicLibraryViewModel()
        {
            // Get the Albums, Artists and Tracks objects from MusicLibrary
            Albums albums = musicLibrary.AlbumInfo;
            Artists artists = musicLibrary.ArtistInfo;
            Tracks tracks = musicLibrary.TrackInfo;

            // Build the underlying object for TracksView on the Tracks tab
            TracksOC = new ObservableCollection<TrackDisplay>();

            foreach (Track track in tracks.TrackList.OrderBy(s => musicLibrary.GetAlbumTitle(s.AlbumId)).ThenBy(s => s.TrackNumber))
            {
                TrackDisplay trackDisplay = new TrackDisplay();
                trackDisplay.Track = track;
                trackDisplay.AlbumName = musicLibrary.GetAlbumTitle(trackDisplay.Track.AlbumId);
                TracksOC.Add(trackDisplay);
            }

            // Build the underlying object for AlbumsView on the Albums tab
            _AlbumsOC = new ObservableCollection<Album>();

            foreach (Album album in albums.AlbumList.OrderBy(s => s.AlbumName))
            {

                _AlbumsOC.Add(album);
            }

            // Create and populate the underlying object for AlbumTracksView on the Albums tab
            AlbumTracksCollectionView = new ListCollectionView(TracksOC);
            SelectedAlbum = AlbumsOC[0];
            AlbumTracksCollectionView.Filter = t =>
            {
                if (t is TrackDisplay trackDisplay)
                {
                    if (trackDisplay.Track.AlbumId == SelectedAlbum.AlbumId)
                    {
                        return true;
                    }
                }

                return false;
            };

            // Create and populate the underlying object for ArtistsTreeView on the Artists tab
            _ArtistsListOC = new ObservableCollection<ArtistDisplay>();

            foreach (Artist artist in artists.ArtistList.OrderBy(s => s.ArtistName))
            {
                ArtistDisplay artistDisplay = new ArtistDisplay();
                artistDisplay.Artist = artist;
                foreach (string albumId in artist.ArtistAlbums)
                {
                    AlbumInfo albumInfo = new AlbumInfo();
                    albumInfo.AlbumId = albumId;
                    albumInfo.AlbumName = musicLibrary.GetAlbumTitle(albumId);
                    artistDisplay.AlbumNamePairs.Add(albumInfo);
                }
                ArtistsListOC.Add(artistDisplay);
            }

            PropertyChanged += OnPropertyChangedHandler;
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