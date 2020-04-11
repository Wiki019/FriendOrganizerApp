using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public interface IFriendDataViewModel
    {
        Task LoadAsync(int friendId);
    }
}