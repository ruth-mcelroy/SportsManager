using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;

namespace SportsTeamManager.Models
{
    public enum Competition { AIL1League,AIL2League, AICup, AIBowl, AIPlate, LeinsterCup}
    public class Match
    {
        [Key]
        public int MatchID { get; set; }

        [Required] 
        public string Opposition { get; set; }

        [Required] 
        public DateTime Time { get; set; }
        public Competition Competition { get; set; }




        public Match()
        {
            AvailabilityDBContext availabilityDB = new AvailabilityDBContext();
            Availability test = new Availability();

            using (PlayerDBContext playersDb = new PlayerDBContext())
            {

                IEnumerable<int> playersID = playersDb.Players.Select(p=>p.PlayerID );

                foreach (int p in playersID)                            //Not working. Want every time a match created creates an availability object for that match for each player
                { 
                    Availability a = new Availability(p, this.MatchID);
                    availabilityDB.Availabilitys.Add(a);
                    availabilityDB.SaveChanges();
                }
            }
        }
    }


    public class AvailabilityDBContext : DbContext
    {

        public AvailabilityDBContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<AvailabilityDBContext>(new CreateDatabaseIfNotExists<AvailabilityDBContext>());
        }
        public DbSet<Availability> Availabilitys { get; set; }

        public System.Data.Entity.DbSet<SportsTeamManager.Models.Match> Matches { get; set; }

        public System.Data.Entity.DbSet<SportsTeamManager.Models.Player> Players { get; set; }
    }

    public class MatchDBContext : DbContext
    {
        public MatchDBContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<MatchDBContext>(new CreateDatabaseIfNotExists<MatchDBContext>());      
        }
        public DbSet<Match> Matches { get; set; }
        public System.Data.Entity.DbSet<SportsTeamManager.Models.Availability> Availabilitys { get; set; }

    }
}