using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SportsTeamManager.Models
{
    public class Context : DbContext
    {
        public Context()
            : base("DefaultConnection")
        {
            Database.SetInitializer<Context>(new CreateDatabaseIfNotExists<Context>());     
        }
        public System.Data.Entity.DbSet<SportsTeamManager.Models.Availability> Availabilities { get; set; }

        public System.Data.Entity.DbSet<SportsTeamManager.Models.Match> Matches { get; set; }

        public System.Data.Entity.DbSet<SportsTeamManager.Models.Player> Players { get; set; }

    }
}