using System;
using System.Net;
using System.Net.Http;
using FlightPlannerWeb.Models;
using FlightPlannerWeb.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FlightPlannerWeb.Controllers
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
            {
                return NotFound();
            }

            return Ok(flight);
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(Flight flight)
        {
            if (!ModelState.IsValid || FlightStorage.CheckDestination(flight))
            {
                return BadRequest();
            }

            if (FlightStorage.IsUnique(flight))
            {
                return Created("", FlightStorage.AddFlight(flight));
            }
            
            return Conflict();
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
