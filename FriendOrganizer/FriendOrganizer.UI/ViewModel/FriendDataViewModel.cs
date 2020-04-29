using FriendOrganizer.model;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModel
{
    public class FriendDataViewModel : ViewModelBase, IFriendDataViewModel
    {
        private readonly IFriendDataService _friendDataService;
        private readonly IEventAggregator _eventAggregator;
        private FriendWrapper _friend;

        public FriendDataViewModel(IFriendDataService friendDataService,
            IEventAggregator eventAggregator)
        {
            _friendDataService = friendDataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenFriendDataViewEvent>()
                .Subscribe(SelectedFriendChangeAsync);

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        public FriendWrapper Friend
        {
            get { return _friend; }
            set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }


        public async Task LoadAsync(int friendId)
        {
            var friend = await _friendDataService.GetByIdAsync(friendId);

            Friend = new FriendWrapper(friend);
        }

        private async void OnSaveExecute()
        {
            await _friendDataService.SaveAsync(Friend.Model);
            _eventAggregator.GetEvent<AfterFriendSaveEvents>()
                .Publish(new AfterFriendSavedEventArgs
                {
                    ID = Friend.Id,
                    DisplayMember = $"{Friend.FirstName} {Friend.LastName}"
                });
        }

        private bool OnSaveCanExecute()
        {
            //TODO check if Friend is valid
            return true;
        }

        private async void SelectedFriendChangeAsync(int friendId)
        {
            await LoadAsync(friendId);
        }
    }
}
