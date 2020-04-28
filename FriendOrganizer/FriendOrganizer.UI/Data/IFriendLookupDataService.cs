﻿using FriendOrganizer.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data
{
    public interface IFriendLookupDataService
    {
        Task<IEnumerable<NavigationItemViewModel>> GetFriendLookupAsync();
    }
}