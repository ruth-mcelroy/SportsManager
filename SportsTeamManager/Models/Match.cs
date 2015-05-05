using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Globalization;

namespace SportsTeamManager.Models
{
    public enum Competition { AIL1League,AIL2League, AICup, AIBowl, AIPlate, LeinsterCup}
    public class Match
    {
        [Key]
        public int MatchID { get; set; }

        [Required] 
        public string Opposition { get; set; }                   //Could be changed into list of possible teams. 

        [Required] 
        public DateTime TimeAndDate { get; set; }                      //Split this into date and time on view and edit

        public string DateStr { get; set; }  //Format 01 Jan 2000
        public string Time { get; set; }    //Format 00:00
        public Competition Competition { get; set; }



        public void CreateAvailable()                           //Creates availability objects for each player on the match object it calls.
        {
            Context db = new Context();
            IEnumerable<Player> players = db.Players;

            foreach (Player p in players)                            
            {

                Availability a = new Availability(p.PlayerID, this.MatchID);        
                db.Availabilities.Add(a);
            }

            db.SaveChanges();
        }

        public void UpdateTime()
        {

            DateTime time = new DateTime();
            time = DateTime.Parse(Time);
            this.TimeAndDate = time;
           
        }

    }

}