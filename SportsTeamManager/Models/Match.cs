using System;
using System.Data.Entity;

namespace SportsTeamManager.Models
{
    public enum Competition { AIL1League,AIL2League, AICup, AIBowl, AIPlate, LeinsterCup}
    public class Match
    {
        public int ID { get; set; }
        public string Opposition { get; set; }
        public DateTime Time { get; set; }
        public Competition Competition { get; set; }
    }

    public class MatchDBContext : DbContext
    {
        public DbSet<Match> Matches { get; set; }
    }
}