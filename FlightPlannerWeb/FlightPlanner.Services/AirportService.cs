using System.Collections.Generic;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightDbContext context) : base(context)
        { }

        public List<Airport> SearchAirport(string keyword)
        {
            List<Airport> response = new List<Airport>();
            foreach (Airport airport in _context.Airports)
            {
                if (airport.City.ToLower().Contains(keyword.ToLower().Trim()) ||
                    airport.Country.ToLower().Contains(keyword.ToLower().Trim()) ||
                    airport.AirportCode.ToLower().Contains(keyword.ToLower().Trim()) ||
                    airport.City.ToLower().Contains(keyword.ToLower().Trim()) ||
                    airport.Country.ToLower().Contains(keyword.ToLower().Trim()) ||
                    airport.AirportCode.ToLower().Contains(keyword.ToLower().Trim()))
                {
                    response.Add(airport);
                }
            }

            return response;
        }
    }
}