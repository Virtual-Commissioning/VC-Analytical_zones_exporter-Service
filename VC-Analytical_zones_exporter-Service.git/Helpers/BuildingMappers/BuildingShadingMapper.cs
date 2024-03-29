﻿using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VC_Analytical_zones_exporter_Service.Models.Geometry;
using VC_Analytical_zones_exporter_Service.Models.Shading;

namespace VC_Analytical_zones_exporter_Service.Helpers.BuildingMappers
{
    public class BuildingShadingMapper
    {
        public static List<Dictionary<string, ShadingBuilding>> MapBuildingShading(FilteredElementCollector allSpaces, 
                                                                                   FilteredElementCollector allWalls,
                                                                                   Document doc, 
                                                                                   ExternalCommandData commandData)
        {
            List<Dictionary<string, ShadingBuilding>> buildingShadings = new List<Dictionary<string, ShadingBuilding>>();
            List<Wall> roomBoundingWalls = new List<Wall>();
            List<Wall> nonRoomBoundingWalls = new List<Wall>();
            foreach (Wall wall in allWalls)
            {
                nonRoomBoundingWalls.Add(wall);
            }
            foreach (SpatialElement space in allSpaces)
            {
                if (space.Category.Name == "Spaces") continue;

                IList<IList<BoundarySegment>> segments = space.GetBoundarySegments(new SpatialElementBoundaryOptions());

                foreach (IList<BoundarySegment> segmentList in segments)
                {
                    foreach (BoundarySegment boundarySegment in segmentList)
                    {
                        Wall wall = doc.GetElement(boundarySegment.ElementId) as Wall;
                        if (wall == null) continue;
                        roomBoundingWalls.Add(wall);
                    }
                }
            }
            nonRoomBoundingWalls.RemoveAll(x => roomBoundingWalls.Exists(y => x.Id.ToString() == y.Id.ToString()));
            foreach (Wall wall in nonRoomBoundingWalls)
            {
                string name = wall.Id.ToString();
                string transmSchedule = "";
                IList<Reference> sideFaces = HostObjectUtils.GetSideFaces(wall, ShellLayerType.Exterior);
                UIDocument uiDoc = commandData.Application.ActiveUIDocument;
                Face face = uiDoc.Document.GetElement(sideFaces[0]).GetGeometryObjectFromReference(sideFaces[0]) as Face;
                List<Coordinate> vertexCoordinates = ShadingGeometryMapper.MapShadingGeometry(face);

                ShadingBuilding shadingBuilding = new ShadingBuilding(name, transmSchedule, vertexCoordinates);
                Dictionary<string, ShadingBuilding> shadingElement = new Dictionary<string, ShadingBuilding>
                {
                    { name, shadingBuilding }
                };

                buildingShadings.Add(shadingElement);
            }

            return buildingShadings;
        }
    }
}
