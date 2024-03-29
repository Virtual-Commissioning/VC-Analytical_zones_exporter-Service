﻿using System.Collections.Generic;
using VC_Analytical_zones_exporter_Service.Models.Geometry;

namespace VC_Analytical_zones_exporter_Service.Models.Shading
{
    public class ShadingBuilding
    {
        public string Name { get; set; }
        public string Transmittance_Schedule_Name { get; set; }
        public List<Coordinate> VertexCoordinates { get; set; }

        public ShadingBuilding(string name, string transmSchedule, List<Coordinate> vertexCoordinates)
        {
            Name = name;
            Transmittance_Schedule_Name = transmSchedule;
            VertexCoordinates = vertexCoordinates;
        }
    }
}
