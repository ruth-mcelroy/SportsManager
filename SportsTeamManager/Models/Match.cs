using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

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
            AvailabilityDBContext availDb = new AvailabilityDBContext();

            using (PlayerDBContext playersDb = new PlayerDBContext())
            {

                IEnumerable<Player> players = playersDb.Players;

                foreach (Player p in players)                            //Not working. Want every time a match created creates an availability object for that match for each player
                {
                    Availability a = new Availability(p.PlayerID, this.MatchID);        //Just trying this. If on previous created object while creating this object. 
                    availDb.Availabilitys.Add(a);
                }
            }
            availDb.SaveChanges();
        }
    }

    public class MatchDBContext : DbContext
    {
        public MatchDBContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<MatchDBContext>(new CreateDatabaseIfNotExists<MatchDBContext>());      
        }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Availability> Availabilitys { get; set; }
    }
}