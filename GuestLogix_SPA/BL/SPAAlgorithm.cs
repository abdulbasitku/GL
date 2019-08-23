using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuestLogix_SPA.BL
{
    public class SPAAlgorithm
    {
        public string FindShortestPath(string origin, string destination)
        {
            string path = "";

            if (AppPersistentData.GuestLogixData.IsOriginExists(origin))
            {
                if (AppPersistentData.GuestLogixData.IsDestinationExists(destination))
                {
                    if (origin == destination)
                    {
                        path = "Origin & Destination are same";
                    }
                    else
                    {
                        path = FindDestination(origin, origin, destination);
                    }
                }
                else
                {
                    path = "INVALID Destination";
                }
            }
            else
            {
                path = "INVALID Origin";
            }

            return path;
        }

        private string FindDestination(string path, string sourcePoint, string destination)
        {
            if (AppPersistentData.GuestLogixData.IsDirectRouteExists(sourcePoint, destination))
            {
                return path + "->" + destination;
            }
            else
            {
                foreach (var connectingSource in AppPersistentData.GuestLogixData.GetOriginRoutes(sourcePoint))
                {
                    if (AppPersistentData.GuestLogixData.IsDirectRouteExists(connectingSource.Destination, destination))
                    {
                        return path + "->" + connectingSource.Destination + "->" + destination;
                    }
                }

                foreach (var connectingSource in AppPersistentData.GuestLogixData.GetOriginRoutes(sourcePoint))
                {
                    if (!path.Contains(connectingSource.Destination))
                    {
                        return FindDestination(path + "->" + connectingSource.Destination, connectingSource.Destination, destination);
                    }
                }
            }

            return "No Route Found";
        }
    }
}