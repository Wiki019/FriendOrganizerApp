using FriendOrganizer.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data.Repositories
{
    public interface IFriendRepository
    {
        Task<Friend> GetByIdAsync(int friendID);
        Task SaveAsync();
        bool HasChange();
        void Add(Friend friend);
        void Remove(Friend model);
    }
}