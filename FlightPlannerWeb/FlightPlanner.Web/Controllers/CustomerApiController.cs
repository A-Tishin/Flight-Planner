using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly ISearchValidator _searchValidator;
        private readonly IAirportService _airportService;

        public CustomerApiController(IAirportService airportService,
            ISearchValidator searchValidator, IFlightService flightService, IMapper mapper)
        {
            _airportService = airportService;
            _searchValidator = searchValidator;
            _flightService = flightService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightById(int id)
        {
            var flight = _flightService.GetFlightById(id);

            if (flight != null)
            {
                return Ok(_mapper.Map<FlightResponse>(flight));
            }

            return NotFound();
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlight(SearchFlight search)
        {
            if (_searchValidator.IsValid(search))
            {
                return Ok(_flightService.SearchFlight(search));
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(string search)
        {
            return Ok(_mapper.Map<AirportResponse[]>(_airportService.SearchAirport(search)));
        }
    }
}
