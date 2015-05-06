using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Linq;
using System.Web;

namespace StockPricePhoneApp
{
    public class Availability
    {
        //sd
        public int AvailabilityID { get; set; }
        public string IRFUNumber { get; set; }

        public string Name { get; set; }

        public string Opposition { get; set; }

        public string Location { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        public bool Available { get; set; }

        public override string ToString()
        {
            return "Opposition: " + this.Opposition + " Date: " + this.Date;
        }

    }
}