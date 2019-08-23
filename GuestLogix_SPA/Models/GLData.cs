using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuestLogix_SPA.Models
{
    public class GLData
    {
        public List<Airline> Airlines { get; set; }
        public List<Airport> Airports { get; set; }
        public List<Route> Routes { get; set; }
        public Dictionary<string, List<Route>> OriginWithAllDestinations { get; set; }
        public List<Route> UniqueDestinations { get; set; }

        public GLData()
        {
            Airlines = new List<Airline>();
            Airports = new List<Airport>();
            Routes = new List<Route>();
            OriginWithAllDestinations = new Dictionary<string, List<Route>>();
            UniqueDestinations = new List<Route>();
        }
        

        public bool IsDirectRouteExists(string origin, string destination)
        {
            bool isExists = false;
            if (IsOriginExists(origin))
            {
                isExists = OriginWithAllDestinations[origin].Exists(item => item.Destination == destination);
            }

            return isExists;
        }

        public bool IsOriginExists(string origin)
        {
            return OriginWithAllDestinations.ContainsKey(origin);
        }

        public bool IsDestinationExists(string destination)
        {
            return UniqueDestinations.Exists(item=>item.Destination == destination);
        }

        public List<Route> GetOriginRoutes(string origin)
        {
            List<Route> originRoutes = new List<Route>();
            if (IsOriginExists(origin))
            {
                originRoutes = OriginWithAllDestinations[origin];
            }

            return originRoutes;
        }
    }
}