using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlannerWeb.Models
{
    public class PageResult
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public List<Flight> Items { get; set; }

        public PageResult(int page, int totalItems, List<Flight> items)
        {
            Page = page;
            TotalItems = totalItems;
            Items = items;
        }
    }
}
