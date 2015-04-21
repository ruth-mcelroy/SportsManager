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



        public void CreateAvailable(Match m)           //Tried to do in constructor but match object not created yet
        {
            Context db = new Context();
            IEnumerable<Player> players = db.Players;

            foreach (Player p in players)                            //Not working. Want every time a match created creates an availability object for that match for each player
            {

                Availability a = new Availability{PlayerID = p.PlayerID, MatchID = m.MatchID};        //Just trying this. If on previous created object while creating this object. 
                db.Availabilities.Add(a);
            }

            db.SaveChanges();
        }



    }

}