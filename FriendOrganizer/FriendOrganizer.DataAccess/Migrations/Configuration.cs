using FriendOrganizer.model;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace FriendOrganizer.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<FriendOrganizer.DataAccess.FriendOrganizerDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FriendOrganizer.DataAccess.FriendOrganizerDBContext context)
        {
            context.Friends.AddOrUpdate(f => f.FirstName,
                new Friend { FirstName = "Waqas", LastName = "Ahmad" },
            new Friend { FirstName = "Baby", LastName = "Hadid" },
            new Friend { FirstName = "Saima", LastName = "Iqbal" },
            new Friend { FirstName = "Mummy", LastName = "Daddy" },
            new Friend { FirstName = "Kanjos", LastName = "Rajpot" }
            );
        }
    }
}
