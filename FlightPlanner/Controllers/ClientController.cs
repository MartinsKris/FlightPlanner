using FlightPlanner.CbContext;
using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;

        public ClientController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult FindAirport(string search)
        {
            var airportStorage = new AirportStorage(_context);
            var airportList= airportStorage.FindFlight(search);

            if (airportList == null)
                return NotFound();

            return Ok(airportList);
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult SearchFlightById(int id)
        {
            var flightStorage = new FlightStorage(_context);
            var flight = flightStorage.GetById(id);

            if (flight == null)
                return NotFound();

            return Ok(flight);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlight(SearchFlights flight)
        {
            if (flight.From == flight.To)
            {
                return BadRequest();
            }

            var flightStorage = new FlightStorage(_context);

            return Ok(flightStorage.FindFlight(flight));
        }
    }
}
