using GalaSoft.MvvmLight;
using MusicData;
using Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace SonosController
{
    sealed class musicLibraryViewModel : ViewModelBase
    {
        private MusicLibrary musicLibrary = new MusicLibrary();
        
        public MusicLibrary MusicLibrary
        {
            get { return musicLibrary; }
            set { musicLibrary = value; }
        }

        private Album selectedAlbum;
        public Album SelectedAlbum
        {
            get => selectedAlbum;
            set
            {
                selectedAlbum = value;
                RaisePropertyChanged("SelectedAlbum");

            }
        }

        private List<ArtistDisplay> _ArtistsList;

        public List<ArtistDisplay> ArtistsList 
        { 
            get => _ArtistsList; 
            set => _ArtistsList = value;
        }

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

        private ObservableCollection<string> _AlbumTracksOC;

        public ObservableCollection<string> AlbumTracksOC
        {
            get
            {
                return _AlbumTracksOC; 
            }
            set
            {
                _AlbumTracksOC = value;
                RaisePropertyChanged(nameof(AlbumTracksOC));
            }
        }

        private ICollectionView _tracksCollectionView;
        public ICollectionView TracksCollectionView
        {
            get { return _tracksCollectionView; }

            set
            {
                _tracksCollectionView = value;
                RaisePropertyChanged(nameof(TracksCollectionView));
            }
        }

        public musicLibraryViewModel()
        {
            Albums albums = musicLibrary.AlbumInfo;
            Artists artists = musicLibrary.ArtistInfo;
            Tracks tracks = musicLibrary.TrackInfo;

            TracksOC = new ObservableCollection<TrackDisplay>();
            foreach (Track track in tracks.TrackList.OrderBy(s => musicLibrary.GetAlbumTitle(s.AlbumId)).ThenBy(s => s.TrackNumber))
            {
                TrackDisplay trackDisplay = new TrackDisplay();
                trackDisplay.Track = track;
                trackDisplay.AlbumName = musicLibrary.GetAlbumTitle(trackDisplay.Track.AlbumId);
                TracksOC.Add(trackDisplay);
            }

            _AlbumsOC = new ObservableCollection<Album>();

            foreach (Album album in albums.AlbumList.OrderBy(s => s.AlbumName))
            {

                _AlbumsOC.Add(album);
            }

            PropertyChanged += OnPropertyChangedHandler;


            TracksCollectionView = new ListCollectionView(TracksOC);
            SelectedAlbum = AlbumsOC[0];
            TracksCollectionView.Filter = t =>
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

            _ArtistsList = new List<ArtistDisplay>();

            foreach (Artist artist in artists.ArtistList.OrderBy(s => s.ArtistName))
            {
                List<string> albumNames = new List<string>();
                ArtistDisplay artistDisplay = new ArtistDisplay();
                artistDisplay.Artist = artist;
                foreach (string albumId in artist.ArtistAlbums)
                {
                    string albumName = musicLibrary.GetAlbumTitle(albumId);
                    albumNames.Add(albumName);
                }
                albumNames.Sort();
                artistDisplay.AlbumNames = albumNames;
                ArtistsList.Add(artistDisplay);
            }

        }

        private void OnPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedAlbum))
            {
                TracksCollectionView.Refresh();
            }
        }
    }
}