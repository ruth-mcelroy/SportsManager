using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportsTeamManagerClient.Models
{
    public class Availability
    {
        [Key]
        public int AvailabilityID { get; set; }

        [Required]
        [DisplayName("IRFU Number")]
        [RegularExpression(@"[0-9]{8}", ErrorMessage = "That is not a valid IRFU Number")]
        public string IRFUNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Opposition { get; set; }

        [MaxLength(50)]
        public string Location { get; set; }

        [RegularExpression(@"^(([0-9])|([0-2][0-9])|([3][0-1])) (Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec) \d{4}$", ErrorMessage = "Please enter date in the format dd MMM yyyy")]
        public string Date { get; set; }

        [RegularExpression("^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Please enter the time in the format HH:mm")]
        public string Time { get; set; }

        public bool Available { get; set; }

    }
}