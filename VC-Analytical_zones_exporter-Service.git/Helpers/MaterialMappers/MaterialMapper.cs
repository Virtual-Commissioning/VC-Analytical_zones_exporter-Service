using Autodesk.Revit.DB;
using System.Collections.Generic;
using VC_Analytical_zones_exporter_Service.git.Helpers.MaterialMappers;
using VC_Analytical_zones_exporter_Service.Models.Zones.Analytical.Material;

namespace VC_Analytical_zones_exporter_Service.Helpers.MaterialMappers
{
    public class MaterialMapper
    {
        public static List<Materials> MapAllMaterials(FilteredElementCollector allWalls,
            FilteredElementCollector allRoofs, FilteredElementCollector allFloors,
            FilteredElementCollector allDoors, FilteredElementCollector allWindows, Document doc)
        {
            List<Materials> allMaterials = new List<Materials>();

            List<Dictionary<string, SurfaceMaterial>> surfaceMaterials = SurfaceMaterialMapper.MapAllSurfaceMaterials(allWalls, allRoofs, allFloors, doc);
            List<Dictionary<string, AirGapMaterial>> airGapMaterials = AirGapMaterialMapper.MapAllMaterials(allWalls, allRoofs, allFloors, doc);
            List<Dictionary<string, DoorMaterial>> doorMaterials = DoorMaterialMapper.MapAllDoors(allDoors, doc);
            List<Dictionary<string, WindowMaterial>> windowMaterials = WindowMaterialMapper.MapAllWindows(allWindows, allWalls, doc);

            Materials materialsToAdd = new Materials(surfaceMaterials, airGapMaterials, doorMaterials, windowMaterials);

            allMaterials.Add(materialsToAdd);

            return allMaterials;
        }
    }
}
