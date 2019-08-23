using GuestLogix_SPA.BL;
using GuestLogix_SPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GuestLogix_SPA.Controllers
{
    public class SPAController : ApiController
    {
        // GET api/SPA?origin={origin}&destination={destination}
        public string Get(string origin,string destination)
        {
            SPAAlgorithm spaAlgo = new SPAAlgorithm();

            string path = spaAlgo.FindShortestPath(origin, destination);

            return path;
        }
    }
}
