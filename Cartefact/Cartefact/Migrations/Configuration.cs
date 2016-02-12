namespace Cartefact.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Cartefact.Classes.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Cartefact.Classes.Context context)
        {
            context.Roles.AddOrUpdate(
                r => r.RoleName,
                new Role { Id = Guid.NewGuid().ToString(), RoleName = "Admin" },
                new Role { Id = Guid.NewGuid().ToString(), RoleName = "User" }
            );
            context.SaveChanges();
            context.Statuses.AddOrUpdate(
                s => s.StatusName,
                new Status { Id = Guid.NewGuid().ToString(), StatusName = "Open" },
                new Status { Id = Guid.NewGuid().ToString(), StatusName = "Pending" },
                new Status { Id = Guid.NewGuid().ToString(), StatusName = "Closed" }
            );
            context.SaveChanges();
            context.Persons.AddOrUpdate(
                s => s.Nickname,
                new Person { Id = Guid.NewGuid().ToString(), Name = "Administrator", Nickname = "admin", Password = "admin", Role = context.Roles.First(r => r.RoleName == "Admin") },
                new Person { Id = Guid.NewGuid().ToString(), Name = "Regular User", Nickname = "user", Password = "user", Role = context.Roles.First(r => r.RoleName == "User") }
            );
            context.SaveChanges();
        }
    }
}
