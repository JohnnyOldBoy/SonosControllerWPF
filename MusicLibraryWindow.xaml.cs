using System.Windows;

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
    }
}
