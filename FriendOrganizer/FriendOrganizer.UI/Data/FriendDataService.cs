using FriendOrganizer.model;
using System.Collections.Generic;

namespace FriendOrganizer.UI.Data
{
    public class FriendDataService : IFriendDataService
    {
        public IEnumerable<Friend> GetAll()
        {
            // TODO: Load Data from real DataBase
            yield return new Friend { FirstName = "Waqas", LastName = "Ahmad" };
            yield return new Friend { FirstName = "Saima", LastName = "Iqbal" };
            yield return new Friend { FirstName = "Mummy", LastName = "Daddy" };
            yield return new Friend { FirstName = "Kanjos", LastName = "Rajpot" };
        }
    }
}
