using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightPlanner.Models
{
    public class Airport
    {
        public string Country { get; set; }
        public string City { get; set; }

        [JsonPropertyName("airport")]
        public string AirportCode { get; set; }
    }
}
