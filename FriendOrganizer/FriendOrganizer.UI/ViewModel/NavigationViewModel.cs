
using FriendOrganizer.model;
using FriendOrganizer.UI.Data;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IFriendLookupDataService _friendLookupService;
        private LookupItem _selectedItem;

        public ObservableCollection<LookupItem> Friends { get; }

        public NavigationViewModel(IFriendLookupDataService friendLookupService)
        {
            _friendLookupService = friendLookupService;
            Friends = new ObservableCollection<LookupItem>();
        }

        public async Task LoadItemsAsync()
        {
            var lookups = await _friendLookupService.GetFriendLookupAsync();
            Friends.Clear();
            foreach (var lookup in lookups)
            {
                Friends.Add(lookup);
            }
        }

        public LookupItem SelectedItemLookup
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }
    }
}
