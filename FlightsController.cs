using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YourProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightsController : ControllerBase
    {
        private static readonly List<Flight> flights = new List<Flight>
        {
            new Flight { Origin = "MZL", Destination = "JFK", DepartureTime = DateTime.UtcNow, ArrivalTime = DateTime.UtcNow.AddHours(5), Price = 1000.0},
            new Flight { Origin = "JFK", Destination = "BCN", DepartureTime = DateTime.UtcNow.AddHours(6), ArrivalTime = DateTime.UtcNow.AddHours(12), Price = 1200.0},
        };

        [HttpGet("unique")]
        public IActionResult GetUniqueFlights(string origin, string destination)
        {
            var uniqueFlights = flights.FindAll(flight => flight.Origin == origin && flight.Destination == destination);
            return Ok(uniqueFlights);
        }

        [HttpGet("Multiple")]
        public IActionResult GetMultipleFlights(string origin, string destination)
        {
            var multipleFlights = FindMultipleFlights(origin, destination);
            return Ok(multipleFlights);
        }

        [HttpGet("multiple-return")]
        public IActionResult GetMultipleReturnFlights(string origin, string destination)
        {
            var multipleReturnFlights = FindMultipleReturnFlights(origin, destination);
            return Ok(multipleReturnFlights);
        }

        private List<Flight> FindMultipleFlights(string origin, string destination)
        {
            var startingFlights = flights.FindAll(flight => flight.Origin == origin);
            var endingFlights = flights.FindAll(flight => flight.Destination == destination);
            var multipleFlights = new List<Flight>();

            foreach (var startingFlight in startingFlights)
            {
                foreach (var endingFlight in endingFlights)
                {
                    if (startingFlight.Destination == endingFlight.Origin)
                    {
                        multipleFlights.Add(startingFlight);
                        multipleFlights.Add(endingFlight);
                    }
                }
            }

            return multipleFlights;
        }

        private List<Flight> FindMultipleReturnFlights(string origin, string destination)
        {
            var startingFlights = flights.FindAll(flight => flight.Origin == origin);
            var endingFlights = flights.FindAll(flight => flight.Destination == destination);
            var multipleReturnFlights = new List<Flight>();

            foreach (var startingFlight in startingFlights)
            {
                foreach (var endingFlight in endingFlights)
                {
                    if (startingFlight.Destination == endingFlight.Origin && endingFlight.Destination == destination)
                    {
                        multipleReturnFlights.Add(startingFlight);
                        multipleReturnFlights.Add(endingFlight);
                    }
                }
            }

            return multipleReturnFlights;
        }
    }

    public class Flight
    {
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public double Price { get; set; }
    }
}
