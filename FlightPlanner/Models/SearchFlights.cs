using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlanner.Models
{
    public class SearchFlights
    {
        public string from { get; set; }
        public string to { get; set; }
        public string departureDate { get; set; }

        public SearchFlights( string from, string to, string departureDate)
        {
            this.from = from;
            this.to = to;
            //this.departureDate = departureDate.ToString("yyyyMMdd");
            this.departureDate = departureDate;
        }
    }
}
