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
            this.Loaded += MainWindow_LoadedAsync;
        }

        private async void MainWindow_LoadedAsync(object sender, RoutedEventArgs e)
        {
            await _viewModelDataContext.LoadAsync();
        }
    }
}
