using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportsTeamManager.Models
{
    public class ClientAvailability
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DisplayName("IRFU Number")]
        [RegularExpression(@"[0-9]{8}", ErrorMessage = "That is not a valid IRFU Number")]
        public string IRFUNumber { get; set; }

        [Required]
        public int PlayerId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int MatchId { get; set; }



        [MaxLength(50)]
        public string Opposition { get; set; }

        public DateTime Time { get; set; }

        public bool Available { get; set; }

    }
}