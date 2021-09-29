using FlightPlanner.CbContext;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;

        public TestingController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [Route("clear")]
        [HttpPost]
        public IActionResult Clear()
        {
            var flightStorage = new FlightStorage(_context);
            flightStorage.ClearFlights();

            var airportStorage = new AirportStorage(_context);
            airportStorage.ClearAirports();

            return Ok();
        }
    }
}
