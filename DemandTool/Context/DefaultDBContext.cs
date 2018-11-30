using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DemandTool.MVC.Models;

namespace DemandTool.MVC.Context
{
    public class DefaultDBContext : DbContext
    {
        public DefaultDBContext()
           : base("name=DemandDb" )
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Demand> Demands { get; set; }
        public DbSet<DemandLog> DemandLogs { get; set; }

        public System.Data.Entity.DbSet<DemandTool.MVC.Models.DemandModel> DemandModels { get; set; }
    }
}