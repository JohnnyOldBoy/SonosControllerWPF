using MusicData;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SonosController
{
    /// <summary>
    /// Interaction logic for MusicLibrary.xaml
    /// </summary>
    public partial class MusicLibraryWindow : Window
    {
        private readonly musicLibraryViewModel _viewModel;

        public MusicLibraryWindow()
        {
            InitializeComponent();
            Left = SystemParameters.WorkArea.Left;
            Top = SystemParameters.WorkArea.Top;
            Width = SystemParameters.WorkArea.Width;
            Height = SystemParameters.WorkArea.Height;
            _viewModel = new musicLibraryViewModel();
            // The DataContext serves as the starting point of Binding Paths
            DataContext = _viewModel;
        }

        private void TracksView_Loaded(object sender, RoutedEventArgs e)
        {
            if (TracksView.Columns.Count > 0)
            {
                TracksView.Columns[0].Header = "Track Number";
                TracksView.Columns[1].Header = "Title";
                TracksView.Columns[2].Header = "Artist";
                TracksView.Columns[3].Header = "Genre";
                TracksView.Columns[4].Header = "Album";
                TracksView.Columns[5].Visibility = Visibility.Hidden;
                TracksView.Columns[6].Visibility = Visibility.Hidden;
            }
        }

        private void AlbumsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<Track> AlbumTracksOC = new ObservableCollection<Track>();
            ListCollectionView AlbumTracksCV;
            Album album =(Album)AlbumsView.CurrentItem;
            string albumId = album.AlbumId;
            Tracks tracks = _viewModel.MusicLibrary.TrackInfo;
            List<Track> albumTrackList = tracks.TrackList.FindAll(x => x.AlbumId == albumId);
            if (albumTrackList.Count > 0)
            {
                foreach (Track track in albumTrackList)
                {
                    AlbumTracksOC.Add(track);
                }
                AlbumTracksCV = new ListCollectionView(AlbumTracksOC);
                AlbumTracksCV.SortDescriptions.Add(new SortDescription("TrackNumber", ListSortDirection.Ascending));
                AlbumTracksView.ItemsSource = AlbumTracksCV;

                AlbumTracksView.Columns[0].Header = "Title";
                AlbumTracksView.Columns[1].Header = "Artist";
                AlbumTracksView.Columns[2].Header = "Genre";
                AlbumTracksView.Columns[4].Header = "Track Number";

                AlbumTracksView.Columns[4].DisplayIndex = 0;


                AlbumTracksView.Columns[3].Visibility = Visibility.Hidden;
                AlbumTracksView.Columns[5].Visibility = Visibility.Hidden;
            }
            else
            {
                AlbumTracksView.ItemsSource = null;
            }
        }

        private void AlbumsView_Loaded_1(object sender, RoutedEventArgs e)
        {
            AlbumsView.Columns[0].Header = "Album";
            AlbumsView.Columns[1].Header = "Artist";
            AlbumsView.Columns[2].Visibility = Visibility.Hidden;
            AlbumsView.Columns[3].Visibility = Visibility.Hidden;
        }

        private void ArtistTreeView_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
