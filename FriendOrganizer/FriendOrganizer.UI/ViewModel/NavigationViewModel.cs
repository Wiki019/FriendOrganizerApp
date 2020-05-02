using FriendOrganizer.UI.Data.Lookups;
using FriendOrganizer.UI.Event;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly IFriendLookupDataService _friendLookupService;
        private readonly IEventAggregator _eventAggregator;

        public ObservableCollection<NavigationItemViewModel> Friends { get; }

        public NavigationViewModel(IFriendLookupDataService friendLookupService, 
            IEventAggregator eventAggregator)
        {
            _friendLookupService = friendLookupService;
            _eventAggregator = eventAggregator;
            Friends = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterFriendSaveEvents>().Subscribe(AfterFriendSaved);
            _eventAggregator.GetEvent<AfterFriendDeletedEvent>().Subscribe(AfterFriendDeleted);
        }

        
        public async Task LoadItemsAsync()
        {
            var lookups = await _friendLookupService.GetFriendLookupAsync();
            Friends.Clear();
            foreach (var lookup in lookups)
            {
                Friends.Add(new NavigationItemViewModel(lookup.Id, lookup.DisplayMember, _eventAggregator));
            }
        }

        private void AfterFriendDeleted(int friendId)
        {
            var friend = Friends.SingleOrDefault(f => f.Id == friendId);
            if(friend != null)
            {
                Friends.Remove(friend);
            }
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

            var lookupItem = Friends.SingleOrDefault(ob => ob.Id == obj.ID);
            if (lookupItem == null)
            {
                Friends.Add(new NavigationItemViewModel(obj.ID, obj.DisplayMember, _eventAggregator));
            }
            else
            {
                lookupItem.DisplayMember = obj.DisplayMember;
            }
        }

    }
}
