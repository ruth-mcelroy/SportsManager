using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SportsTeamManager.Models
{
    public enum Position { Prop, Hooker, [Display(Name = "Second Row")] SecondRow, Flanker, [Display(Name = "Number 8")] No8, [Display(Name = "Scrum Half")] ScrumHalf, [Display(Name = "Out Half")] OutHalf, Centre, Wing, Fullback, None }      //Only some of the display names need to be cahnged
    public class Player
    {
        [Key]
        public int PlayerID { get; set; }
        
        [Required] 
        [DisplayName ("IRFU Number")]

        [RegularExpression(@"[0-9]{8}", ErrorMessage = "That is not a valid IRFU Number")]     //IRFU number is exactly 8 digits
        public string IRFUNumber { get; set; }      //Rugby registration number unique and known by each player

        [Required] 
        [MaxLength (50)]
        public string Name { get; set; }

        public Position Position { get; set; }



        public void CreateAvailableNewPlayer()                           //Creates availability objects for each player on the match object it calls.
        {
            Context db = new Context();
            IEnumerable<Match> matches = db.Matches;

            foreach (Match m in matches)
            {

                Availability a = new Availability(this.PlayerID, m.MatchID);
                db.Availabilities.Add(a);
            }

            db.SaveChanges();
        }


    }

}