using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Web.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingApiController : Controller
    {
        private readonly IDbServiceExtended _service;

        public TestingApiController(IDbServiceExtended service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _service.DeleteAll<Flight>();
            _service.DeleteAll<Airport>();

            return Ok();
        }
    }
}
