using Autodesk.Revit.DB;
using System.Collections.Generic;
using VC_Analytical_zones_exporter_Service.Models.Zones.Analytical.Material;

namespace VC_Analytical_zones_exporter_Service.Helpers.MaterialMappers
{
    class CurtainWallWindowMaterialMapper
    {
        public static List<Dictionary<string, WindowMaterial>> MapAllCurtainWallWindows(FilteredElementCollector allWalls)
        {
            List<Dictionary<string, WindowMaterial>> windowMaterials = new List<Dictionary<string, WindowMaterial>>();

            foreach (Wall wall in allWalls)
            {
                if (wall.WallType.Kind.ToString() != "Curtain") continue;
                string name = "CW_Window_Mat_" + wall.Id.ToString();
                double? uFactor = null;
                double? solarHeatGain = null;
                double? visibleTransmittance = null;

                WindowMaterial windowMaterial = new WindowMaterial(name, uFactor, solarHeatGain, visibleTransmittance);
                Dictionary<string, WindowMaterial> linkedWindowMaterial = new Dictionary<string, WindowMaterial>();
                linkedWindowMaterial.Add(windowMaterial.Name, windowMaterial);
                windowMaterials.Add(linkedWindowMaterial);
            }
            return windowMaterials;
        }
    }
}
