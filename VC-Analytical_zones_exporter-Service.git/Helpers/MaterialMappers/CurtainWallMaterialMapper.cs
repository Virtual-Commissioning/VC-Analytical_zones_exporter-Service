using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VC_Analytical_zones_exporter_Service.Models.Zones.Analytical.Material;

namespace VC_Analytical_zones_exporter_Service.git.Helpers.MaterialMappers
{
    class CurtainWallMaterialMapper
    {
        public static List<SurfaceMaterial> MapAllCurtainWalls(FilteredElementCollector allWalls, Autodesk.Revit.DB.Document doc)
        {
            List<SurfaceMaterial> layerWallMaterials = new List<SurfaceMaterial>();

            foreach (Autodesk.Revit.DB.Wall wall in allWalls)
            {
                if (wall.WallType.Kind.ToString() != "Curtain") continue;
                string name = "CW_Mat_" + wall.Id.ToString();
                double? thickness = null;
                string readableName = wall.WallType.Name;
                int? roughness = null;
                double? thermalAbsorbtance = null;
                double? solarAbsorbtance = null;
                double? visibleAbsorbtance = null;
                double? conductivity = null;
                double? density = null;
                double? specificHeat = null;

                SurfaceMaterial layerWallMaterialToAdd = new SurfaceMaterial(readableName, name, roughness, thickness,
                    conductivity, density, specificHeat, thermalAbsorbtance,
                    solarAbsorbtance, visibleAbsorbtance);

                layerWallMaterials.Add(layerWallMaterialToAdd);
            }

            return layerWallMaterials;
        }
    }
}
