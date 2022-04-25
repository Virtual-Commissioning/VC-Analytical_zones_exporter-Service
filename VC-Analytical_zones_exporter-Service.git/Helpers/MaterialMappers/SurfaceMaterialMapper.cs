using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;
using VC_Analytical_zones_exporter_Service.git.Helpers.MaterialMappers;
using VC_Analytical_zones_exporter_Service.Models.Zones.Analytical.Material;

namespace VC_Analytical_zones_exporter_Service.Helpers.MaterialMappers
{
    class SurfaceMaterialMapper
    {
        public static List<Dictionary<string, SurfaceMaterial>> MapAllSurfaceMaterials(FilteredElementCollector allWalls,
                                                                               FilteredElementCollector allRoofs, 
                                                                               FilteredElementCollector allFloors, 
                                                                               Document doc)
        {
            List<Dictionary<string, SurfaceMaterial>> allSurfaceMaterials = new List<Dictionary<string, SurfaceMaterial>>();

            List<SurfaceMaterial> layerWallMaterials = WallMaterialMapper.MapAllWalls(allWalls, doc);
            if (layerWallMaterials.Count != 0)
            {
                foreach (SurfaceMaterial surfaceMaterial in layerWallMaterials)
                {
                    if (surfaceMaterial.ReadableName != "Air")
                    {
                        Dictionary<string, SurfaceMaterial> linkedSurfaceMaterial = new Dictionary<string, SurfaceMaterial>();
                        linkedSurfaceMaterial.Add(surfaceMaterial.Name, surfaceMaterial);
                        allSurfaceMaterials.Add(linkedSurfaceMaterial);
                    }
                }
            }

            List<SurfaceMaterial> layerCurtainWallMaterials = CurtainWallMaterialMapper.MapAllCurtainWalls(allWalls, doc);
            if (layerCurtainWallMaterials.Count != 0)
            {
                foreach (SurfaceMaterial surfaceMaterial in layerCurtainWallMaterials)
                {
                    if (surfaceMaterial.ReadableName != "Air")
                    {
                        Dictionary<string, SurfaceMaterial> linkedSurfaceMaterial = new Dictionary<string, SurfaceMaterial>();
                        linkedSurfaceMaterial.Add(surfaceMaterial.Name, surfaceMaterial);
                        allSurfaceMaterials.Add(linkedSurfaceMaterial);
                    }
                }
            }

            List<SurfaceMaterial> layerRoofMaterials = RoofMaterialMapper.MapAllRoofs(allRoofs, doc);
            if (layerRoofMaterials.Count != 0)
            {
                foreach (SurfaceMaterial surfaceMaterial in layerRoofMaterials)
                {
                    if (surfaceMaterial.Name != "Air")
                    {
                        Dictionary<string, SurfaceMaterial> linkedSurfaceMaterial = new Dictionary<string, SurfaceMaterial>();
                        linkedSurfaceMaterial.Add(surfaceMaterial.Name, surfaceMaterial);
                        allSurfaceMaterials.Add(linkedSurfaceMaterial);
                    }
                }
            }

            List<SurfaceMaterial> layerFloorMaterials = FloorMaterialMapper.MapAllFloors(allFloors, doc);
            if (layerFloorMaterials.Count != 0)
            {
                foreach (SurfaceMaterial surfaceMaterial in layerFloorMaterials)
                {
                    if (surfaceMaterial.Name != "Air")
                    {
                        Dictionary<string, SurfaceMaterial> linkedSurfaceMat = new Dictionary<string, SurfaceMaterial>();
                        linkedSurfaceMat.Add(surfaceMaterial.Name, surfaceMaterial);
                        allSurfaceMaterials.Add(linkedSurfaceMat);
                    }
                }
            }

            List<Dictionary<string, SurfaceMaterial>> filteredDictionary =
                allSurfaceMaterials.GroupBy(x => string.Join("", x.Select(i => string.Format("{0}{1}", i.Key, i.Value)))).Select(x => x.First()).ToList();

            return filteredDictionary;
        }
    }
}
