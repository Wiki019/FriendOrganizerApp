using FriendOrganizer.DataAccess;
using FriendOrganizer.model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data
{
    public class FriendDataService : IFriendDataService
    {
        private readonly Func<FriendOrganizerDBContext> _contextCreator;

        public FriendDataService(Func<FriendOrganizerDBContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<Friend> GetByIdAsync(int friendID)
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Friends.AsNoTracking().SingleAsync(f => f.Id == friendID);
            }
        }

        public async Task SaveAsync(Friend friends)
        {
            using (var ctx = _contextCreator())
            {
                ctx.Friends.Attach(friends);
                ctx.Entry(friends).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
        }
    }
}
