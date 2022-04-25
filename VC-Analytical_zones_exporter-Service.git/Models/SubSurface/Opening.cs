﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VC_Analytical_zones_exporter_Service.Models.Geometry;

namespace VC_Analytical_zones_exporter_Service.Models.SubSurface
{
    public class Opening
    {
        public string Name { get; set; }
        public string Surface_Type { get; set; } // If bool is needed, define types and use the SubSurfType class
        public string Construction_Name { get; set; }
        public string Zone_Name { get; set; }
        public string Outside_Boundary_Condition_Object { get; set; }
        public string Outside_Boundary_Condition { get; set; }
        public bool Sun_Exposure { get; set; }
        public bool Wind_Exposure { get; set; }
        public string View_Factor_to_Ground { get; set; }
        public List<Coordinate> VertexCoordinates { get; set; }

        public Opening(string id, string subSurfType, string constructionId, string zoneId,
            string outsideBCObj, string outsideBC, bool sunExposure, bool windExposure,
            string viewFactorToGround, List<Coordinate> vertices)
        {
            Name = id;
            Surface_Type = subSurfType;
            Construction_Name = constructionId;
            Zone_Name = zoneId;
            Outside_Boundary_Condition_Object = outsideBCObj;
            Outside_Boundary_Condition = outsideBC;
            Sun_Exposure = sunExposure;
            Wind_Exposure = windExposure;
            View_Factor_to_Ground = viewFactorToGround;
            VertexCoordinates = vertices;
        }
    }
}
