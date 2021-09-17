using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;

namespace FlightPlanner.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetById(id);
            if (flight == null)
                return NotFound();

            return Ok(flight);
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(Flight flight)
        {
            if (FlightStorage.NullValidation(flight) == false)
                return BadRequest();

            if (FlightStorage.IsUniqueFlight(flight) == false)
                return Conflict();

            FlightStorage.AddFlight(flight);
                return Created("", flight);
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            FlightStorage.DeleteFlight(id);
            return Ok();
        }


    }
}
