using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FlightPlannerWeb.Models
{
    public class Airport
    {
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }

        [Required]
        [JsonPropertyName("airport")]
        public string AirportCode { get; set; }
    }
}
