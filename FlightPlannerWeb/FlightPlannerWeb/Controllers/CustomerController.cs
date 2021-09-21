using System;
using System.Collections.Generic;
using System.Linq;
using FlightPlannerWeb.DbContext;
using FlightPlannerWeb.Models;
using FlightPlannerWeb.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlannerWeb.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        public CustomerController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(string search)
        {
            List<Flight> flightList = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .ToList();

            if (FlightStorage.SearchAirport(search, flightList).Count > 0)
            {
                return Ok(FlightStorage.SearchAirport(search, flightList));
            }

            return Ok();
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightById(int id)
        {
            var flight = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);

            if (flight != null)
            {
                return Ok(flight);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlight(SearchFlight search)
        {
            List<Flight> flightList = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .ToList();

            if (search.From == search.To)
            {
                return BadRequest();
            }

            return Ok(FlightStorage.SearchFlight(search, flightList));
        }
    }
}
