using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SportsTeamManager.Models
{
    public class Availability
    {
        public int ID { get; set; }
        public Player Player { get; set; }              //Foreign key not working. Are dbContexts correct?
        public Match Match { get; set; }
        public bool Available { get; set; }

        public Availability(Player player, Match match)
        {
            Player = player;
            Match = match;
        }

        public Availability()
        {

        }
    }

    public class AvailabilityDBContext : DbContext
    {

        public AvailabilityDBContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<AvailabilityDBContext>(new DropCreateDatabaseAlways<AvailabilityDBContext>());      //Only for the moment, to be changed only development is complete
        }
        public DbSet<Availability> Available { get; set; }
    }
}