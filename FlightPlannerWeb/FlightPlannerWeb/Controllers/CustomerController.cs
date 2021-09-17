using System;
using System.Collections.Generic;
using FlightPlannerWeb.Models;
using FlightPlannerWeb.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace FlightPlannerWeb.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(string search)
        {
            if (FlightStorage.SearchAirport(search).Count > 0)
            {
                return Ok(FlightStorage.SearchAirport(search));
            }

            return Ok();
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightById(int id)
        {
            if (FlightStorage.GetById(id) != null)
            {
                return Ok(FlightStorage.GetById(id));
            }

            return NotFound();
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlight(SearchFlight search)
        {
            if (search.From == search.To)
            {
                return BadRequest();
            }

            return Ok(FlightStorage.SearchFlight(search));
        }
    }
}
