using FlightPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using FlightPlanner.CbContext;

namespace FlightPlanner.Storage
{
    public class AirportStorage
    {
        private readonly FlightPlannerDbContext _context;

        public AirportStorage(FlightPlannerDbContext context)
        {
            _context = context;
        }

        public void ClearAirports()
        {
            foreach (var value in _context.Airports)
                _context.Airports.Remove(value);

            _context.SaveChanges();
        }

        public List<Airport> FindFlight(string searchValue)
        {
            var airportValus = searchValue.Trim().ToLower();

            if (!String.IsNullOrEmpty(airportValus))
            {
                if (_context.Airports.Count() != 0)
                {
                    var airportX = _context.Airports.FirstOrDefault(f =>
                        f.AirportCode.ToLower().Contains(airportValus)
                        || f.Country.ToLower().Contains(airportValus)
                        || f.City.ToLower().Contains(airportValus));
                    List<Airport> listWithAirports = new List<Airport>();

                    listWithAirports.Add(airportX);

                    return listWithAirports;
                }

                return null;
            }

            return null;
        }
    }
}
