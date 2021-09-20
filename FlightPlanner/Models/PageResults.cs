using System.Collections.Generic;

namespace FlightPlanner.Models
{
    public class PageResults
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public virtual List<Flight> Items { get; set; }
    }
}
