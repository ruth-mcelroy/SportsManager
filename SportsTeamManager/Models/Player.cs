using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SportsTeamManager.Models
{
    public enum Position { Prop, Hooker, SecondRow, Flanker, No8, ScrumHalf, OutHalf, Centre, Wing, Fullback }
    public class Player
    {
        public int ID { get; set; }
        public string IRFUNumber { get; set; }
        public string Name { get; set; }
        public Position Position { get; set; }
    }

    public class PlayerDBContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
    }
}