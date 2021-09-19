using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlanner.Models
{
    public class PageResults
    {
        public int page { get; set; }
        public int totalItems { get; set; }
        public virtual List<Flight> items { get; set; }
    }
}
