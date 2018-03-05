using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace API.DataAccess
{
    public class DbInit : CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            context.Persons.Add(new Models.Person()
            {
                Name = "Petras",
                Age = 48
            });

            context.Persons.Add(new Models.Person()
            {
                Name = "Jonas",
                Age = 47
            });

            context.SaveChanges();
            base.Seed(context);
        }
    }
}