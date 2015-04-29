using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTeamManagerClient.Models
{
    public class Availability
    {


        public int ID { get; set; }
        public string Name { get; set; }
        public int MatchId { get; set; }
        public string Opposition { get; set; }
        public DateTime Time { get; set; }
        public bool Available { get; set; }

    }
}
