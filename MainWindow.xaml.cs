﻿
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            this.Cursor = Cursors.AppStarting;
            InitializeComponent();
            Left = SystemParameters.WorkArea.Left;
            Top = SystemParameters.WorkArea.Top;
            Width = SystemParameters.WorkArea.Width;
            Height = SystemParameters.WorkArea.Height;
            _viewModel = new MainWindowViewModel();
            // The DataContext serves as the starting point of Binding Paths
            DataContext = _viewModel;
            this.Cursor = Cursors.Arrow;

        }

    }
}