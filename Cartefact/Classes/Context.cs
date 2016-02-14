using Cartefact.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cartefact.Classes
{
    public class Context : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Rental> Rentals { get; set; }
    }
}