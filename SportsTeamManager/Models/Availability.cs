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
        public int PlayerID { get; set; }           //Foreign Key for Player
        public Player Player { get; set; }           //Reference to Player   

        [ForeignKey("Match")]
        public int MatchID { get; set; }         //Foreign Key for Match      
        public Match Match { get; set; }        //Reference to Match


        public bool Available { get; set; }

        public Availability(int playerID, int matchID)
        {
            PlayerID = playerID;
            MatchID = matchID;
        }
        public Availability()
        { }


    }


}