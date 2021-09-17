using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlannerWeb.Models
{
    public class PageResult
    {
        public int Page { get; set; } = 0;
        public int TotalItems { get; set; } = 0;
        public List<Flight> Items { get; set; } = new List<Flight>();
    }
}
