using FlightPlanner.CbContext;
using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private static readonly object _objLock = new object();
        private readonly FlightPlannerDbContext _context;

        public AdminController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flightStorage = new FlightStorage(_context);
            var flight = flightStorage.GetById(id);

            if (flight == null)
                return NotFound();

            return Ok(flight);
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(Flight flight)
        {
            lock (_objLock)
            {
                var flightsStorage = new FlightStorage(_context);

                if (FlightStorage.NullValidation(flight) == false)
                    return BadRequest();

                if (flightsStorage.IsUniqueFlight(flight) == false)
                    return Conflict();

                var flights = flightsStorage.AddFlight(flight);

                return Created("", flights);
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            var flightStorage = new FlightStorage(_context);

            flightStorage.DeleteFlight(id);

            return Ok();
        }
    }
}
