using FriendOrganizer.DataAccess;
using FriendOrganizer.model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly FriendOrganizerDBContext _context;

        public FriendRepository(FriendOrganizerDBContext context)
        {
            _context = context;
        }

        public async Task<Friend> GetByIdAsync(int friendID)
        {
            return await _context.Friends.SingleAsync(f => f.Id == friendID);
        }

        public bool HasChange()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
