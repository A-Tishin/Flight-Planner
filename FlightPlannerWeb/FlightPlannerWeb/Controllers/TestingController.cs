using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlannerWeb.DbContext;
using FlightPlannerWeb.Storage;

namespace FlightPlannerWeb.Controllers
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

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _context.Airports.RemoveRange(_context.Airports);
            _context.Flights.RemoveRange(_context.Flights);
            _context.SaveChanges();
            return Ok();
        }
    }
}
