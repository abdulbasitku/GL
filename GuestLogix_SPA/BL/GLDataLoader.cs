using GuestLogix_SPA.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace GuestLogix_SPA.BL
{
    public class GLDataLoader
    {
        private string RootPath;
        private string AirlineCSVFilePath;
        private string AirportCSVFilePath;
        private string RoutesCSVFilePath;

        public GLDataLoader(string rootPath)
        {
            this.RootPath = rootPath;
            this.AirlineCSVFilePath = rootPath + @"\GL_Data\data\full\airlines.csv";
            //this.AirlineCSVFilePath = rootPath + @"\GL_Data\data\test\airlines.csv";

            this.AirportCSVFilePath = rootPath + @"\GL_Data\data\full\airports.csv";
            //this.AirportCSVFilePath = rootPath + @"\GL_Data\data\test\airports.csv";

            this.RoutesCSVFilePath = rootPath + @"\GL_Data\data\full\routes.csv";
            //this.RoutesCSVFilePath = rootPath + @"\GL_Data\data\test\routes.csv";

            LoadDataFromCSVFiles();
            GenerateKeyValueRoutesMap();
            SortOutUniqueDestinations();
        }

        private void LoadDataFromCSVFiles()
        {
            GLData guestLogixData = new GLData();
            
            using (var reader = new StreamReader(this.AirlineCSVFilePath))            
            {
                bool skipHeading = true;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (skipHeading == false)
                    {
                        var values = line.Split(',');

                        var airline = new Airline();
                        airline.Name = values[0];
                        airline.TwoDigitCode = values[1];
                        airline.ThreeDigitCode = values[2];
                        airline.Country = values[3];


                        guestLogixData.Airlines.Add(airline);
                    }
                    else
                    {
                        skipHeading = false;
                    }
                }
                reader.Close();
            }

            using (var reader = new StreamReader(this.AirportCSVFilePath))
            {
                bool skipHeading = true;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (skipHeading == false)
                    {
                        var values = line.Split(',');

                        var airport = new Airport();
                        airport.Name = values[0];
                        airport.City = values[1];
                        airport.Country = values[2];
                        airport.IATACode = values[3];
                        airport.Latitute = values[4];
                        airport.Longitude = values[5];


                        guestLogixData.Airports.Add(airport);
                    }
                    else
                    {
                        skipHeading = false;
                    }
                }
                reader.Close();
            }

            using (var reader = new StreamReader(this.RoutesCSVFilePath))
            {
                bool skipHeading = true;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (skipHeading == false)
                    {
                        var values = line.Split(',');

                        var route = new Models.Route();
                        route.AirlineID = values[0];
                        route.Origin = values[1];
                        route.Destination = values[2];


                        guestLogixData.Routes.Add(route);
                    }
                    else
                    {
                        skipHeading = false;
                    }
                }
                reader.Close();
            }

            AppPersistentData.GuestLogixData = guestLogixData;
        }

        private void GenerateKeyValueRoutesMap()
        {
            List<Models.Route> uniqueOrigins = AppPersistentData.GuestLogixData.Routes.GroupBy(item => new { item.Origin }).Select(item2 => item2.First()).ToList();
            foreach (var originRoute in uniqueOrigins)
            {
                List<Models.Route> allOriginRoutes = AppPersistentData.GuestLogixData.Routes.FindAll(item => item.Origin == originRoute.Origin);
                AppPersistentData.GuestLogixData.OriginWithAllDestinations.Add(originRoute.Origin, allOriginRoutes);
            }
        }

        private void SortOutUniqueDestinations()
        {
            List<Models.Route> uniqueDestinations = AppPersistentData.GuestLogixData.Routes.GroupBy(item => new { item.Destination }).Select(item2 => item2.First()).ToList();
            AppPersistentData.GuestLogixData.UniqueDestinations = uniqueDestinations;
        }
    }
}