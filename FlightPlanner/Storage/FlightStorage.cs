using FlightPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Storage
{
    public static class FlightStorage
    {
        private static readonly object _objLock = new object();
        private static readonly List<Flight> _flights = new List<Flight>();

        public static Flight GetById(int id)
        {
            return _flights.SingleOrDefault(f => f.Id == id);
        }

        public static void ClearFlights()
        {
            _flights.Clear();
        }

        public static int? CheckForId(int id)
        {
            if (_flights.Count == 0)
                return 0;

            return _flights?.Find(v => v.Id == id)!?.Id;
        }

        public static Flight AddFlight(Flight flights)
        {
            lock (_objLock)
            {
                Random rand = new Random();

                flights.Id = Convert.ToInt32((DateTime.Now - DateTime.Parse("01/01/2002")).TotalSeconds) + rand.Next(1000000);

                if (CheckForId(flights.Id) == flights.Id)
                    flights.Id++;

                _flights.Add(flights);
                AirportStorage.AddAirport(flights);

                return flights;
            }
        }

        public static bool IsUniqueFlight(Flight flight)
        {
            lock (_objLock)
            {
                if (_flights.Count == 0)
                    return true;

                return (_flights.Find(f =>
                    f.DepartureTime == flight.DepartureTime && f.From.AirportCode == flight.From.AirportCode) == null);
            }
        }
        
        public static bool NullValidation(Flight flight)
        {
            lock (_objLock)
            {
                if (flight?.DepartureTime == null
                    || flight.From == null
                    || flight?.ArrivalTime == null
                    || flight.To == null)
                    return false;

                else if (String.IsNullOrEmpty(flight.From.AirportCode)
                         || String.IsNullOrEmpty(flight.From.City)
                         || String.IsNullOrEmpty(flight.From.Country)
                         || String.IsNullOrEmpty(flight.To.AirportCode)
                         || String.IsNullOrEmpty(flight.To.City)
                         || String.IsNullOrEmpty(flight.To.Country)
                         || String.IsNullOrEmpty(flight.Carrier))
                    return false;

                else if (flight.From.AirportCode.Trim().ToLower() == flight.To.AirportCode.Trim().ToLower())
                    return false;

                else if (Convert.ToDateTime(flight.DepartureTime) >= Convert.ToDateTime(flight.ArrivalTime))
                    return false;
                else
                    return true;
            }
        }

        public static void DeleteFlight(int id)
        {
            lock (_objLock)
            {
                var flightToRemove = _flights.Find(f => f.Id == id);
                _flights.Remove(flightToRemove);
            }
        }

        public static PageResults FindFlight(SearchFlights flight)
        {
            var flights = _flights.Find(f =>
                f.From.AirportCode == flight.From && f.To.AirportCode == flight.To 
                                                  && f.DepartureTime.Substring(0, 10) == flight.DepartureDate);

            List<int?> actingList = new List<int?>();
            actingList.Add(flights?.Id);

            PageResults value = new PageResults();
            value.Items = new List<Flight>();

            if (flights == null)
            {
                value.Page = 0;
                value.TotalItems = 0;
                var targetType = value.Items.Select(fx => new int()).ToList();
                targetType.Add(0);
            }
            else
            {
                value.Page = 0;
                value.TotalItems = actingList.Count;
                value.Items.Add(flights);
            }

            return value;
        }
    }
}
