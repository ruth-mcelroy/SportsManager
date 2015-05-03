using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SportsTeamManager.Models
{
    public enum Competition { AIL1League,AIL2League, AICup, AIBowl, AIPlate, LeinsterCup}
    public class Match
    {
        
        [Key]
        public int MatchID { get; set; } 

        [Required]
        [MaxLength(50)]
        public string Opposition { get; set; }                  //Could be changed into list of possible teams. 

        [Required] 
        public DateTime TimeAndDate { get; set; }                     

        [RegularExpression(@"^(([0-9])|([0-2][0-9])|([3][0-1])) (Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec) \d{4}$", ErrorMessage = "Please enter date in the format dd MMM yyyy")]
        public string Date { get; set; }  //Format 01 Jan 2000

        [RegularExpression("^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Please enter the time in the format HH:mm")]
        public string Time { get; set; }    //Format 00:00

        public Competition Competition { get; set; }



        public void CreateAvailableNewMatch()                           //Creates availability objects for each player on the match object it calls.
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
            string newTimeString = this.Date + " " + this.Time;

                time = DateTime.ParseExact(newTimeString, "dd MMM yyyy HH:mm", CultureInfo.InvariantCulture);

                this.TimeAndDate = time;

           
        }

    }

}