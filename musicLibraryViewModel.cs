using Devices;
using Services;
using System.Collections.ObjectModel;
using MusicData;
using System.ComponentModel;
using System.Collections.Generic;

namespace SonosController
{
    sealed class musicLibraryViewModel : INotifyPropertyChanged
    {
        private MusicLibrary musicLibrary = new MusicLibrary();
        
        public MusicLibrary MusicLibrary
        {
            get { return musicLibrary; }
            set { musicLibrary = value; }
        }

        private List<string> _ArtistsList;

        public List<string> ArtistsList 
        { 
            get => _ArtistsList; 
            set => _ArtistsList = value; 
        }

        private ObservableCollection<TrackDisplay> _TracksOC;
        
        public ObservableCollection<TrackDisplay> TracksOC
        {
            get { return _TracksOC; }
            set { _TracksOC = value; }
        }

        private ObservableCollection<Album> _AlbumsOC;

        public ObservableCollection<Album> AlbumsOC
        {
            get { return _AlbumsOC; }
            set { _AlbumsOC = value; }
        }

        private ObservableCollection<string> _AlbumTracksOC;

        public ObservableCollection<string> AlbumTracksOC
        {
            get { return _AlbumTracksOC; }
            set { _AlbumTracksOC = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public musicLibraryViewModel()
        {
            Albums albums = musicLibrary.AlbumInfo;
            Artists artists = musicLibrary.ArtistInfo;
            Tracks tracks = musicLibrary.TrackInfo;
            
            _TracksOC = new ObservableCollection<TrackDisplay>();
            foreach (Track track in tracks.TrackList)
            {
                TrackDisplay trackDisplay = new TrackDisplay();
                trackDisplay.TrackNumber = track.TrackNumber;
                trackDisplay.Title = track.Title;
                trackDisplay.ArtistName = track.ArtistName;
                trackDisplay.Genres = string.Join(", ", track.Genres);
                trackDisplay.AlbumId = track.AlbumId;
                trackDisplay.AlbumName = albums.AlbumList.Find(x => x.AlbumId == track.AlbumId).AlbumName;
                trackDisplay.TrackUrl = track.TrackUrl;
                _TracksOC.Add(trackDisplay);
            }

            _ArtistsList = new List<string>();
            {
                foreach (Artist artist in artists.ArtistList)
                {
                    _ArtistsList.Add(artist.ArtistName);
                }
            }

            _AlbumsOC = new ObservableCollection<Album>();
            
            foreach (Album album in albums.AlbumList)
            {

                _AlbumsOC.Add(album);
            }

        }

        //protected void OnPropertyChange(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}


    }
}