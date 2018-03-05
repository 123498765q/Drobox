using API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace API.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=DataContext")
        {
            Database.SetInitializer(new DbInit());
            Database.Initialize(true);
        }

        public DbSet<Person> Persons { get; set; }
    }
}