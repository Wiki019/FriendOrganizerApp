using FriendOrganizer.model;
using FriendOrganizer.UI.Data;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public class FriendDataViewModel : ViewModelBase, IFriendDataViewModel
    {
        private readonly FriendDataService _friendDataService;
        private Friend _friend;

        public FriendDataViewModel(FriendDataService friendDataService)
        {
            _friendDataService = friendDataService;
        }

        public async Task LoadAsync(int friendId)
        {
            Friend = await _friendDataService.GetByIdAsync(friendId);
        }

        public Friend Friend
        {
            get { return _friend; }
            set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }
    }
}
