using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlannerWeb.Models
{
    public class Flight
    {
        public int? Id { get; set; }
        [Required]
        public string Carrier { get; set; }
        [Required]
        public Airport From { get; set; }
        [Required]
        public Airport To { get; set; }
        [Required]
        public string DepartureTime { get; set; }
        [Required]
        public string ArrivalTime { get; set; }
    }
}
