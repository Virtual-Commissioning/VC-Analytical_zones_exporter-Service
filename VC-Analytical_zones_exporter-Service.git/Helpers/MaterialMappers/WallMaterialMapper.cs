using System;
using System.Collections.Generic;

using Autodesk.Revit.DB;
using VC_Analytical_zones_exporter_Service.Helpers;
using VC_Analytical_zones_exporter_Service.Models.Zones.Analytical.Material;

namespace VC_Analytical_zones_exporter_Service.Helpers.MaterialMappers
{
    class WallMaterialMapper
    {
        public static List<SurfaceMaterial> MapAllWalls(FilteredElementCollector allWalls, Autodesk.Revit.DB.Document doc)
        {
            List<SurfaceMaterial> layerWallMaterials = new List<SurfaceMaterial>();

            foreach (Autodesk.Revit.DB.Wall wall in allWalls)
            {
                if (wall.WallType.Kind.ToString() == "Curtain") continue;
                CompoundStructure structure = wall.WallType.GetCompoundStructure();
                IList<CompoundStructureLayer> layers = structure.GetLayers();

                foreach (CompoundStructureLayer layer in layers)
                {
                    string id = layer.MaterialId.ToString();
                    double thickness;
                    if (layer.Width == 0)
                    {
                        thickness = 0.001;
                    }
                    else
                    {
                        thickness = Math.Round(ImperialToMetricConverter.ConvertFromFeetToMeters(layer.Width), 3);
                    }
                    string preName = id + "_" + thickness.ToString();
                    string name = preName.Replace(',', '.');
                    Material layerWallMaterial = doc.GetElement(layer.MaterialId) as Material;
                    string readableName = layerWallMaterial.Name;
                    int? roughness = null;
                    double? thermalAbsorbtance = null;
                    double? solarAbsorbtance = null;
                    double? visibleAbsorbtance = null;
                    // Getting thermal assets:
                    ElementId thermalAssetId = layerWallMaterial.ThermalAssetId;
                    PropertySetElement pse = doc.GetElement(thermalAssetId) as PropertySetElement;
                    if (pse == null) continue;
                    ThermalAsset asset = pse.GetThermalAsset();
                    double conductivity = Math.Round(ImperialToMetricConverter.ConvertThermalConductivityImpToMet(asset.ThermalConductivity), 3);
                    double density = Math.Round(ImperialToMetricConverter.ConvertDensityImpToMet(asset.Density), 3);
                    double specificHeat = Math.Round(ImperialToMetricConverter.ConvertSpecificHeatImpToMet(asset.SpecificHeat), 3);

                    SurfaceMaterial layerWallMaterialToAdd = new SurfaceMaterial(readableName, name, roughness, thickness,
                        conductivity, density, specificHeat, thermalAbsorbtance,
                        solarAbsorbtance, visibleAbsorbtance);

                    layerWallMaterials.Add(layerWallMaterialToAdd);
                }
            }

            return layerWallMaterials;

        }
    }
}
