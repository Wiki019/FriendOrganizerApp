using FriendOrganizer.model;
using FriendOrganizer.UI.Data;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public INavigationViewModel NavigationViewModel { get; }
        public IFriendDataViewModel FriendDataViewModel { get; }

        public MainViewModel(INavigationViewModel navigationViewModel, IFriendDataViewModel friendDataViewModel)
        {
            NavigationViewModel = navigationViewModel;
            FriendDataViewModel = friendDataViewModel;
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadItemsAsync();
        }
    }
}
