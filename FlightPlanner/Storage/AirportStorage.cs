using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlanner.Models;

namespace FlightPlanner.Storage
{
    public class AirportStorage
    {
        private static List<Airport> _airports = new List<Airport>();

        public static void AddAirport(Flight flight)
        {
            if (_airports.Count != 0)
            {
                if (_airports.SingleOrDefault(a =>
                        a.AirportCode == flight.From.AirportCode)!?.AirportCode !=
                    flight.From.AirportCode)
                {
                    _airports.Add(flight.From);

                }
                else if (_airports.SingleOrDefault(a =>
                             a.AirportCode == flight.To.AirportCode)!?.AirportCode !=
                         flight.To.AirportCode)
                {
                    _airports.Add(flight.To);
                }
            }
            else
            {
                _airports.Add(flight.From);
                _airports.Add(flight.To);
            }

        }

        public static void ClearAirports()
        {
            _airports.Clear();
        }

        public static List<Airport> FindFlight(string searchValue)
        {
            if (!String.IsNullOrEmpty(searchValue))
                if (_airports.Count != 0)
                {
                    var airportX = _airports.SingleOrDefault(f =>
                        f.AirportCode.ToString().ToLower().Contains(searchValue.ToLower())
                        || f.Country.ToString().ToLower().Contains(searchValue.ToLower())
                        || f.City.ToString().ToLower().Contains(searchValue.ToLower()));
                    List<Airport> listWithAirports = new List<Airport>();
                    listWithAirports.Add(airportX);

                    return listWithAirports;
                }
                else
                    return null;

            else
                return null;
            
        }
    }
}
