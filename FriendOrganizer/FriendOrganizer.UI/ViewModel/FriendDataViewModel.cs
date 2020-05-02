using FriendOrganizer.model;
using FriendOrganizer.UI.Data.Repositories;
using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.View.Services;
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
        private readonly IFriendRepository _friendRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private FriendWrapper _friend;

        public FriendDataViewModel(IFriendRepository friendRepository,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _friendRepository = friendRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
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

        public ICommand DeleteCommand { get; }

        private bool _hasChanges;

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if(_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }    
            }
        }


        public async Task LoadAsync(int? friendId)
        {
            var friend = friendId.HasValue? 
                await _friendRepository.GetByIdAsync(friendId.Value) :
                CreateNewFriend();

            Friend = new FriendWrapper(friend);
            Friend.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _friendRepository.HasChange();
                }
                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if(Friend.Id == 0)
            {
                //Little trick to trigger validaiton
                Friend.FirstName = "";
            }
        }

        private async void OnSaveExecute()
        {
            await _friendRepository.SaveAsync();
            HasChanges = _friendRepository.HasChange();
            _eventAggregator.GetEvent<AfterFriendSaveEvents>()
                .Publish(new AfterFriendSavedEventArgs
                {
                    ID = Friend.Id,
                    DisplayMember = $"{Friend.FirstName} {Friend.LastName}"
                });
        }

        private bool OnSaveCanExecute()
        {
            return Friend != null && !Friend.HasErrors && HasChanges;
        }

        private async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowYesNoDialog($"Do you really wabt ti delete the friend {Friend.FirstName} {Friend.LastName}?", 
                "Question");
            if(result == MessageDialogResult.Yes)
            {
                _friendRepository.Remove(Friend.Model);
                await _friendRepository.SaveAsync();
                _eventAggregator.GetEvent<AfterFriendDeletedEvent>()
                 .Publish(Friend.Id);
            }
        }

        private Friend CreateNewFriend()
        {
            var friend = new Friend();
            _friendRepository.Add(friend);
            return friend;
        }
    }
}
