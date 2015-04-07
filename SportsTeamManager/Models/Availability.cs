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
        public TeamMember Player { get; set; }
        public Match Match { get; set; }
        public bool Available { get; set; }
    }

    public class AvailabilityDBContext : DbContext
    {
        public DbSet<Availability> Matches { get; set; }
    }
}