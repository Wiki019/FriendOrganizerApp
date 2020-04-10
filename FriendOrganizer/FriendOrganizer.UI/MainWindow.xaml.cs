using FriendOrganizer.UI.ViewModel;
using System;
using System.Windows;

namespace FriendOrganizer.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModelDataContext;

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModelDataContext = viewModel;
            DataContext = _viewModelDataContext;
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModelDataContext.Load();
        }
    }
}
