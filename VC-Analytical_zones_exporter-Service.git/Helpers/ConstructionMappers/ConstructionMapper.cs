using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VC_Analytical_zones_exporter_Service.Models.Construction;

namespace VC_Analytical_zones_exporter_Service.Helpers.ConstructionMappers
{
    public class ConstructionMapper
    {
        public static List<Constructions> MapAllConstructions(FilteredElementCollector allWalls,
                                                              FilteredElementCollector allFloors, 
                                                              FilteredElementCollector allRoofs, 
                                                              FilteredElementCollector allSpaces,
                                                              FilteredElementCollector allDoors, 
                                                              FilteredElementCollector allWindows, 
                                                              FilteredElementCollector allOpenings, 
                                                              Document doc)
        {
            List<Constructions> allConstructions = new List<Constructions>();

            var surfaceConstructions = SurfaceConstructionMapper.MapAllSurfaceConstructions(allSpaces, allWalls, allRoofs, allFloors, allDoors, allWindows, doc);
            var openingConstructions = OpeningConstructionMapper.MapAllOpeningConstructions(allOpenings);
            var allConstructionsToAdd = new Constructions(surfaceConstructions, openingConstructions);
            allConstructions.Add(allConstructionsToAdd);

            return allConstructions;
        }
    }
}
