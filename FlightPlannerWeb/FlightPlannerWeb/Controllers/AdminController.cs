using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using FlightPlannerWeb.DbContext;
using FlightPlannerWeb.Models;
using FlightPlannerWeb.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FlightPlannerWeb.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        public AdminController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
            
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
            List<Flight> flightList = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .ToList();

            if (!ModelState.IsValid || FlightStorage.CheckDestination(flight))
            {
                return BadRequest();
            }

            if (FlightStorage.IsUnique(flight, flightList))
            {
                _context.Flights.Add(flight);
                _context.SaveChanges();
                return Created("", flight);
            }

            return Conflict();
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            var flight = _context.Flights
                .Include(f => f.To)
                .Include(f => f.From)
                .SingleOrDefault(f => f.Id == id);

            if (flight != null)
            {
                _context.Airports.Remove(flight.To);
                _context.Airports.Remove(flight.From);
                _context.Flights.Remove(flight);
                _context.SaveChanges();
            }

            return Ok();
        }
    }
}
