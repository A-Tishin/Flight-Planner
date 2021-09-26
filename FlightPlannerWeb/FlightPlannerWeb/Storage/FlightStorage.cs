using System;
using System.Collections.Generic;
using System.Linq;
using FlightPlannerWeb.DbContext;
using FlightPlannerWeb.Models;

namespace FlightPlannerWeb.Storage
{
    public static class FlightStorage
    {
        private static readonly object _locker = new ();

        public static bool IsUnique(Flight flight, List<Flight> FlightList)
        {
            bool isUnique = true;
            lock (_locker)
            {
                foreach (var t in FlightList)
                {
                    if (t.DepartureTime == flight.DepartureTime &&
                        t.ArrivalTime == flight.ArrivalTime &&
                        t.Carrier == flight.Carrier &&
                        t.From.AirportCode == flight.From.AirportCode)
                    {
                        isUnique = false;
                    }
                }
            }

            return isUnique;
        }

        public static bool CheckDestination(Flight flight)
        {
            lock (_locker)
            {
                DateTime depart = Convert.ToDateTime(flight.DepartureTime);
                DateTime arrival = Convert.ToDateTime(flight.ArrivalTime);
                var timeSpan = depart.Subtract(arrival).TotalSeconds;
                bool isValidData = flight.From.AirportCode.ToLower().Trim() == flight.To.AirportCode.ToLower().Trim() ||
                                   flight.DepartureTime == flight.ArrivalTime || timeSpan > 0;

                return isValidData;
            }
        }

        public static List<Airport> SearchAirport(string keyword, List<Flight> FlightList)
        {
            List<Airport> airList = new List<Airport>();
            foreach (Flight flights in FlightList)
            {
                if (flights.From.City.ToLower().Contains(keyword.ToLower().Trim()) ||
                    flights.From.Country.ToLower().Contains(keyword.ToLower().Trim()) ||
                    flights.From.AirportCode.ToLower().Contains(keyword.ToLower().Trim()) ||
                    flights.To.City.ToLower().Contains(keyword.ToLower().Trim()) ||
                    flights.To.Country.ToLower().Contains(keyword.ToLower().Trim()) ||
                    flights.To.AirportCode.ToLower().Contains(keyword.ToLower().Trim()))
                {
                    airList.Add(flights.From);
                    return airList;
                }
            }

            return airList;
        }

        public static PageResult SearchFlight(SearchFlight data, List<Flight> FlightList)
        {
            lock (_locker)
            {
                PageResult page = new PageResult(0, 0, new List<Flight>());
                foreach (Flight f in FlightList)
                {
                    var depDate = f.DepartureTime.Substring(0, 10);
                    if (data.From == f.From.AirportCode &&
                        data.To == f.To.AirportCode &&
                        data.DepartureDate == depDate)
                    {
                        page.Items.Add(f);
                        page.Page++;
                    }
                }

                page.TotalItems = page.Items.Count;
                return page;
            }
        }
    }
}
