using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlanner.Models;

namespace FlightPlanner
{
    interface IFlight
    {
        public int Id { get; set; }
        public Airport To { get; set; }
        public Airport From { get; set; }
        public string Carrier { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
