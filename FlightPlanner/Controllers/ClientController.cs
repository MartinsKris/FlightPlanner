using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [HttpGet]
        [Route("airports")]
        public IActionResult FindAirport(string search)
        {
            var airportValue = AirportStorage.FindFlight(search.Trim());

            if (airportValue != null)
                return Ok(airportValue);

            return NotFound();
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult SearchFlightById(int id)
        {
            var flight = FlightStorage.GetById(id);
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

            return Ok(FlightStorage.FindFlight(flight));
        }
    }
}
