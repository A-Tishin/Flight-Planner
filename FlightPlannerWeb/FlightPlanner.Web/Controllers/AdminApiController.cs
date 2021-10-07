using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Authorization;

namespace FlightPlanner.Web.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private static readonly object _locker = new();
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IValidator> _validators;
        public AdminApiController(IEnumerable<IValidator> validators, IFlightService flightService, IMapper mapper)
        {
            _validators = validators;
            _flightService = flightService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _flightService.GetFlightById(id);
            if (flight == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FlightResponse>(flight));
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(FlightRequest request)
        {
            lock (_locker)
            {
                if (!_validators.All(s => s.IsValid(request)))
                {
                    return BadRequest();
                }

                var flight = _mapper.Map<Flight>(request);
                if (_flightService.Exists(flight))
                {
                    return Conflict();
                }

                _flightService.Create(flight);
                return Created("", _mapper.Map<FlightResponse>(flight));
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            var flight = _flightService.GetFlightById(id);

            if (flight != null)
            {
                _flightService.DeleteFlight(id);
            }

            return Ok();
        }

    }
}
