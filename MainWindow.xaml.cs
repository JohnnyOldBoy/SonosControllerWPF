using System.Windows;
using System.Windows.Controls;
using Devices;
using Services;

namespace SonosController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private readonly mainWindowViewModel _viewModel;
        //private ServiceUtils serviceUtils = new ServiceUtils();

        public MainWindow()
        {
            InitializeComponent();
            Left = SystemParameters.WorkArea.Left;
            Top = SystemParameters.WorkArea.Top;
            Width = SystemParameters.WorkArea.Width;
            Height = SystemParameters.WorkArea.Height;
            _viewModel = new mainWindowViewModel();
            // The DataContext serves as the starting point of Binding Paths
            DataContext = _viewModel;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void zonePlayersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ZonePlayer zonePlayer = _viewModel.ZonePlayersOC[zonePlayersList.SelectedIndex];
            
            DetailsView.ItemsSource = _viewModel.serviceUtils.getPlayerDetails(zonePlayer);
        }

        private void MusicLibraryBtn_Click(object sender, RoutedEventArgs e)
        {
            MusicLibraryWindow musicLibraryWindow = new MusicLibraryWindow();
            musicLibraryWindow.Show();
        }

    }
}
