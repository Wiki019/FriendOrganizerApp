using FriendOrganizer.UI.Data.Lookups;
using FriendOrganizer.UI.Event;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly IFriendLookupDataService _friendLookupService;
        private readonly IEventAggregator _eventAggregator;
        private NavigationItemViewModel _selectedItem;

        public ObservableCollection<NavigationItemViewModel> Friends { get; }

        public NavigationViewModel(IFriendLookupDataService friendLookupService, 
            IEventAggregator eventAggregator)
        {
            _friendLookupService = friendLookupService;
            _eventAggregator = eventAggregator;
            Friends = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterFriendSaveEvents>().Subscribe(AfterFriendSaved);
        }

        private void AfterFriendSaved(AfterFriendSavedEventArgs obj)
        {
            //foreach (var item in Friends)
            //{
            //    if (item.Id != obj.ID)
            //    {
            //        item.DisplayMember = obj.DisplayMember;
            //    }
            //}

            var lookupItem = Friends.Single(ob => ob.Id == obj.ID);
            lookupItem.DisplayMember = obj.DisplayMember;
        }

        public async Task LoadItemsAsync()
        {
            var lookups = await _friendLookupService.GetFriendLookupAsync();
            Friends.Clear();
            foreach (var lookup in lookups)
            {
                Friends.Add(new NavigationItemViewModel(lookup.Id, lookup.DisplayMember));
            }
        }

        public NavigationItemViewModel SelectedItemLookup
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if(_selectedItem != null)
                {
                    _eventAggregator.GetEvent<OpenFriendDataViewEvent>()
                        .Publish(_selectedItem.Id);
                }
            }
        }
    }
}
