using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using VC_Analytical_zones_exporter_Service.Helpers.MaterialMappers;
using VC_Analytical_zones_exporter_Service.Models.Zones.Analytical.Material;

namespace VC_Analytical_zones_exporter_Service.git.Helpers.MaterialMappers
{
    class WindowMaterialMapper
    {
        public static List<Dictionary<string, WindowMaterial>> MapAllWindows(FilteredElementCollector allWindows, FilteredElementCollector allWalls, Autodesk.Revit.DB.Document doc)
        {
            List<Dictionary<string, WindowMaterial>> windowMaterials = new List<Dictionary<string, WindowMaterial>>();

            foreach (FamilyInstance window in allWindows)
            {
                ElementId windowSymbol = window.Symbol.Id;
                FamilySymbol windowInfo = doc.GetElement(windowSymbol) as FamilySymbol;

                string name = window.Id.ToString();
                double uFactor = Math.Round(1 / windowInfo.GetThermalProperties().ThermalResistance, 3);
                double solarHeatGain = Math.Round(windowInfo.GetThermalProperties().SolarHeatGainCoefficient, 3);
                double visibleTransmittance = Math.Round(windowInfo.GetThermalProperties().VisualLightTransmittance, 3);

                WindowMaterial windowMaterial = new WindowMaterial(name, uFactor, solarHeatGain, visibleTransmittance);
                
                Dictionary<string, WindowMaterial> linkedWindowMaterial = new Dictionary<string, WindowMaterial>
                {
                    { windowMaterial.Name, windowMaterial }
                };
                
                windowMaterials.Add(linkedWindowMaterial);

            }

            List<Dictionary<string, WindowMaterial>> curtainWallWindowMaterials = CurtainWallWindowMaterialMapper.MapAllCurtainWallWindows(allWalls);
            windowMaterials.AddRange(curtainWallWindowMaterials);

            return windowMaterials;
        }
    }
}
