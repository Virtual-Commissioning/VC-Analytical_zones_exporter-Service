﻿using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;
using VC_Analytical_zones_exporter_Service.git.Helpers.ConstructionMappers;
using VC_Analytical_zones_exporter_Service.Helpers.ConstructionMappers;
using VC_Analytical_zones_exporter_Service.Models.Construction;

namespace VC_Analytical_zones_exporter_Service.Helpers.ConstructionMappers
{
    public class SurfaceConstructionMapper
    {
        public static List<Dictionary<string, SurfaceConstruction>> MapAllSurfaceConstructions(FilteredElementCollector allSpaces, FilteredElementCollector allWalls,
            FilteredElementCollector allRoofs, FilteredElementCollector allFloors, FilteredElementCollector allWindows,
            FilteredElementCollector allDoors, Document doc)
        {
            List<Dictionary<string, SurfaceConstruction>> allSurfaceConstructions = new List<Dictionary<string, SurfaceConstruction>>();
            List<Dictionary<string, SurfaceConstruction>> wallConstructions = WallConstructionMapper.MapAllWalls(allSpaces, doc);
            List<Dictionary<string, SurfaceConstruction>> roofConstructions = RoofConstructionMapper.MapAllRoofs(allRoofs, doc);
            List<Dictionary<string, SurfaceConstruction>> floorConstructions = FloorConstructionMapper.MapAllFloors(allFloors, doc);
            List<Dictionary<string, SurfaceConstruction>> windowConstructions = WindowConstructionMapper.MapAllWindows(allWindows, doc);
            List<Dictionary<string, SurfaceConstruction>> doorConstructions = DoorConstructionMapper.MapAllDoors(allDoors, doc);
            List<Dictionary<string, SurfaceConstruction>> curtainWallWindowConstructions = CurtainWallWindowConstructionMapper.MapAllCurtainWallWindows(allWalls);

            allSurfaceConstructions.AddRange(wallConstructions);
            allSurfaceConstructions.AddRange(roofConstructions);
            allSurfaceConstructions.AddRange(floorConstructions);
            allSurfaceConstructions.AddRange(windowConstructions);
            allSurfaceConstructions.AddRange(doorConstructions);
            allSurfaceConstructions.AddRange(curtainWallWindowConstructions);

            List<Dictionary<string, SurfaceConstruction>> filteredDictionary =
                allSurfaceConstructions.GroupBy(x => string.Join("", x.Select(i => string.Format("{0}{1}", i.Key, i.Value)))).Select(x => x.First()).ToList();

            return filteredDictionary;
        }
    }
}
