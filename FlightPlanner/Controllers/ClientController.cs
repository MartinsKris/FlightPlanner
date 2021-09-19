using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlanner.Models;
using FlightPlanner.Storage;

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
            if (flight.from == flight.to)
            {
                return BadRequest();
            }

            var x = FlightStorage.FindFlight(flight);


            //if (FlightStorage.FindFlight(flight) == 0)
            //    return Ok(0);
            return Ok(x);
        }
    }
}
