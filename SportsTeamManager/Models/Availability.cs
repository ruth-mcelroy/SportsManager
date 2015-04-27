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
        public virtual Player Player { get; set; }              

        [ForeignKey("Match")]
        public int MatchID { get; set; }
        public virtual Match Match { get; set; }


        public bool Available { get; set; }



        public Availability(int p, int m)      //Constructor called from CreateAvailable()
        {
            this.PlayerID = p;                   
            this.MatchID = m;
        }

        public Availability()      //Constructor called from CreateAvailable()
        {
        }


    }

}