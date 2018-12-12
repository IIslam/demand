using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DemandTool.MVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using DemandTool.Models;

namespace DemandTool.MVC.Context
{
    public class DefaultDBContext : DbContext
    {

        static DefaultDBContext()
        {
            Database.SetInitializer<DefaultDBContext>(new DefaultDBContextDbInitializer());
        }
        //public static DefaultDBContext Create()
        //{
        //    return new DefaultDBContext();
        //}
        public DefaultDBContext()
           : base("name=DemandDb")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }


        public DbSet<Demand> Demands { get; set; }
        public DbSet<DemandLog> DemandLogs { get; set; }

        public DbSet<DemandModel> DemandModels { get; set; }

        public DbSet<User> Users { get; set; }
        //  This method will be called after migrating to the latest version.

    }

    public class DefaultDBContextDbInitializer : CreateDatabaseIfNotExists<DefaultDBContext>
    {
        

        protected override void Seed(DefaultDBContext context)
        {
            User adminUser = new User()
            {
                Email = "admin@vodafone.com",
                FullName = "Admin",
                Id = 0,
                Mobile = "010",
                Password = "admin!1",
                Role = "admin",
                Username = "admin@vodafone.com"
            };
            context.Set<User>().Add(adminUser);
            context.SaveChanges();
            base.Seed(context);
        }
    }
}