﻿using System.Collections.Generic;
using VC_Analytical_zones_exporter_Service.Models.Shading;

namespace VC_Analytical_zones_exporter_Service.Models.Site
{
    public class Site
    {
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Time_Zone { get; set; }
        public string Elevation { get; set; }
        public List<Dictionary<string, Building>> Buildings { get; set; }
        public List<Dictionary<string, ShadingSite>> SiteShadings { get; set; }

        public Site(string name, string latitude,
                    string longitude, string timeZone,
                    string elevation, List<Dictionary<string, Building>> buildings,
                    List<Dictionary<string, ShadingSite>> shadingSite)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Time_Zone = timeZone;
            Elevation = elevation;
            Buildings = buildings;
            SiteShadings = shadingSite;
        }
    }
}
