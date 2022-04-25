using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;
using VC_Analytical_zones_exporter_Service.Models.Zones.Analytical.Material;

namespace VC_Analytical_zones_exporter_Service.Helpers.MaterialMappers
{
    public class AirGapMaterialMapper
    {
        public static List<Dictionary<string, AirGapMaterial>> MapAllMaterials(FilteredElementCollector allWalls,
                                                                               FilteredElementCollector allRoofs, 
                                                                               FilteredElementCollector allFloors, 
                                                                               Document doc)
        {
            List<Dictionary<string, AirGapMaterial>> airGapMaterials = new List<Dictionary<string, AirGapMaterial>>();

            List<SurfaceMaterial> layerWallMaterials = WallMaterialMapper.MapAllWalls(allWalls, doc);
            foreach (SurfaceMaterial surfaceMaterial in layerWallMaterials)
            {
                if (surfaceMaterial.ReadableName == "Air")
                {
                    Dictionary<string, AirGapMaterial> linkedSurfaceMaterial = new Dictionary<string, AirGapMaterial>();
                    string name = surfaceMaterial.Name;
                    double? thermalResistance = 1 / surfaceMaterial.Conductivity;
                    AirGapMaterial AirGapMaterial = new AirGapMaterial(name, thermalResistance);
                    linkedSurfaceMaterial.Add(surfaceMaterial.Name, AirGapMaterial);
                    airGapMaterials.Add(linkedSurfaceMaterial);
                }
            }

            List<SurfaceMaterial> layerRoofMaterials = RoofMaterialMapper.MapAllRoofs(allRoofs, doc);
            foreach (SurfaceMaterial surfaceMaterial in layerRoofMaterials)
            {
                if (surfaceMaterial.ReadableName == "Air")
                {
                    Dictionary<string, AirGapMaterial> linkedSurfaceMaterial = new Dictionary<string, AirGapMaterial>();
                    string name = surfaceMaterial.Name;
                    double? thermalResistance = 1 / surfaceMaterial.Conductivity;
                    AirGapMaterial airGapMat = new AirGapMaterial(name, thermalResistance);
                    linkedSurfaceMaterial.Add(surfaceMaterial.Name, airGapMat);
                    airGapMaterials.Add(linkedSurfaceMaterial);
                }
            }

            List<SurfaceMaterial> layerFloorMaterials = FloorMaterialMapper.MapAllFloors(allFloors, doc);
            foreach (SurfaceMaterial surfaceMaterial in layerFloorMaterials)
            {
                if (surfaceMaterial.ReadableName == "Air")
                {
                    Dictionary<string, AirGapMaterial> linkedSurfaceMaterial = new Dictionary<string, AirGapMaterial>();
                    string name = surfaceMaterial.Name;
                    double? thermalResistance = 1 / surfaceMaterial.Conductivity;
                    AirGapMaterial airGapMat = new AirGapMaterial(name, thermalResistance);
                    linkedSurfaceMaterial.Add(surfaceMaterial.Name, airGapMat);
                    airGapMaterials.Add(linkedSurfaceMaterial);
                }
            }

            List<Dictionary<string, AirGapMaterial>> filteredDictionary =
                airGapMaterials.GroupBy(x => string.Join("", x.Select(i => string.Format("{0}{1}", i.Key, i.Value)))).Select(x => x.First()).ToList();
            List<Dictionary<string, AirGapMaterial>> airGapMaterial = new List<Dictionary<string, AirGapMaterial>>();

            return filteredDictionary;
        }
    }
}
