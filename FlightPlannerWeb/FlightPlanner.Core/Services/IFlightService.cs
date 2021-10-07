﻿using System.Collections.Generic;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService: IEntityService<Flight>
    {
        Flight GetFlightById(int id);
        void DeleteFlight(int id);
        bool Exists(Flight flight);
        PageResult SearchFlight(SearchFlight data);
        List<Airport> SearchAirport(string keyword);
    }
}
