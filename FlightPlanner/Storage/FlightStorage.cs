using FlightPlanner.CbContext;
using FlightPlanner.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Storage
{
    public class FlightStorage
    {
        private static readonly object _objLock = new object();
        private readonly FlightPlannerDbContext _context;

        public FlightStorage(FlightPlannerDbContext context)
        {
            _context = context;
        }

        public Flight GetById(int id)
        {
            return _context.Flights.Include(a => a.To)
                .Include(a => a.From).SingleOrDefault(f => f.Id == id);
        }

        public void ClearFlights()
        {
            foreach (var value in _context.Flights)
                _context.Flights.Remove(value);

            _context.SaveChanges();
        }

        public Flight AddFlight(Flight flights)
        {
            lock (_objLock)
            {
                _context.Flights.Add(flights);
                _context.SaveChanges();

                return flights;
            }
        }

        public bool IsUniqueFlight(Flight flight)
        {
            lock (_objLock)
            {
                if (_context.Flights.Count() == 0)
                    return true;

                return (_context.Flights.FirstOrDefault(f =>
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

        public void DeleteFlight(int id)
        {
            lock (_objLock)
            {
                var flight = _context.Flights.Include(a => a.To)
                    .Include(a => a.From)
                    .FirstOrDefault(f => f.Id == id);

                if (flight != null)
                {
                    _context.Flights.Remove(_context.Flights.Include(a => a.From)
                        .Include(a => a.To).First(s => s.Id == id));
                    _context.SaveChanges();
                }
            }
        }

        public PageResults FindFlight(SearchFlights flight)
        {
            var flights = _context.Flights.SingleOrDefault(f =>
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
