using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SportsTeamManager.Models
{
    public class Availability
    {
        [Key]
        public int AvailabilityID { get; set; }

        [ForeignKey("Player")] 
        public int PlayerID { get; set; }
        public Player Player { get; set; }              //Foreign key not working. Are dbContexts correct?

        [ForeignKey("Match")] 
        public int MatchID { get; set; }
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
            Database.SetInitializer<AvailabilityDBContext>(new DropCreateDatabaseIfModelChanges<AvailabilityDBContext>());      //Only for the moment, to be changed only development is complete
        }
        public DbSet<Availability> Availabilitys { get; set; }
    }
}