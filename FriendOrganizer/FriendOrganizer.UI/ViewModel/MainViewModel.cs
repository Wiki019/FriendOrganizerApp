using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.View.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand CreateNewFriendCommand { get; }
        public INavigationViewModel NavigationViewModel { get; }

        private IFriendDataViewModel _friendDataViewModel;

        public IFriendDataViewModel FriendDataViewModel
        {
            get { return _friendDataViewModel; }
            set
            {
                _friendDataViewModel = value;
                OnPropertyChanged();
            }
        }

        private Func<IFriendDataViewModel> _friendDataViewModelCreator;

        private IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogeService;

        public MainViewModel(INavigationViewModel navigationViewModel,
            Func<IFriendDataViewModel> friendDataViewModelCreator,
            IEventAggregator eventAggregator, IMessageDialogService messageDialogeService)
        {
            _friendDataViewModelCreator = friendDataViewModelCreator;
            _eventAggregator = eventAggregator;
            _messageDialogeService = messageDialogeService;
            _eventAggregator.GetEvent<OpenFriendDataViewEvent>()
                .Subscribe(OnOpenFriendDataViewModel);

            _eventAggregator.GetEvent<AfterFriendDeletedEvent>()
               .Subscribe(AfterFriendDeleted);

            CreateNewFriendCommand = new DelegateCommand(OnCreateNewFriendExecute);

            NavigationViewModel = navigationViewModel;
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadItemsAsync();
        }

        private async void OnOpenFriendDataViewModel(int? friendId)
        {
            if (FriendDataViewModel != null && FriendDataViewModel.HasChanges)
            {
                var result = _messageDialogeService.ShowOkCancelDialog("You have made changes. Navigate away?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            FriendDataViewModel = _friendDataViewModelCreator();
            await FriendDataViewModel.LoadAsync(friendId);
        }

        private void OnCreateNewFriendExecute()
        {
            OnOpenFriendDataViewModel(null);
        }

        private void AfterFriendDeleted(int friendId)
        {
            FriendDataViewModel = null;
        }
    }
}
