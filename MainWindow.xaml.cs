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
        private readonly MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            Left = SystemParameters.WorkArea.Left;
            Top = SystemParameters.WorkArea.Top;
            Width = SystemParameters.WorkArea.Width;
            Height = SystemParameters.WorkArea.Height;
            _viewModel = new MainWindowViewModel();
            // The DataContext serves as the starting point of Binding Paths
            DataContext = _viewModel;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void zonePlayersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ZonePlayer zonePlayer = _viewModel.ZonePlayersCollection[ZonePlayersList.SelectedIndex];
            
            DetailsView.ItemsSource = _viewModel._serviceUtils.getPlayerDetails(zonePlayer);
        }

        private void MusicLibraryBtn_Click(object sender, RoutedEventArgs e)
        {
            MusicLibraryWindow musicLibraryWindow = new MusicLibraryWindow();
            musicLibraryWindow.Show();
        }

    }
}
