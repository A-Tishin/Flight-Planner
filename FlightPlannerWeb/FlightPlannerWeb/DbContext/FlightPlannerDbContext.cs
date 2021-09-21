using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlannerWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlannerWeb.DbContext
{
    public class FlightPlannerDbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public FlightPlannerDbContext(DbContextOptions<FlightPlannerDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}
