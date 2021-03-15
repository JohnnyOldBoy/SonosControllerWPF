using SonosController.ViewModels;
using System.Windows;

namespace SonosController
{
    /// <summary>
    /// Interaction logic for CreateStereoPair.xaml
    /// </summary>
    public partial class CreateStereoPairWindow : Window
    {
        private readonly CreateStereoPairViewModel _viewModel;

        public CreateStereoPairWindow()
        {
            InitializeComponent();
            _viewModel = new CreateStereoPairViewModel();
            DataContext = _viewModel;
        }
    }
}

