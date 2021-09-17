using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
