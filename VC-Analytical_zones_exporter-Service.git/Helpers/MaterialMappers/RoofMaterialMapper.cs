﻿using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using VC_Analytical_zones_exporter_Service.Models.Zones.Analytical.Material;

namespace VC_Analytical_zones_exporter_Service.Helpers.MaterialMappers
{
    class RoofMaterialMapper
    {
        public static List<SurfaceMaterial> MapAllRoofs(FilteredElementCollector allRoofs, Autodesk.Revit.DB.Document doc)
        {
            List<SurfaceMaterial> layerRoofMaterials = new List<SurfaceMaterial>();

            foreach (RoofBase roof in allRoofs)
            {
                CompoundStructure structure = roof.RoofType.GetCompoundStructure();
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

                    //bool alreadyExists = layerRoofMaterials.Any(item => item.Name.ToString() == id && item.Thickness == thickness);
                    //if (alreadyExists == true) continue;
                    string preName = id + "_" + thickness.ToString();
                    string name = preName.Replace(',', '.');
                    Material layerRoofMaterial = doc.GetElement(layer.MaterialId) as Material;
                    string readableName = layerRoofMaterial.Name;
                    int? roughness = null;
                    double? thermalAbsorbtance = null;
                    double? solarAbsorbtance = null;
                    double? visibleAbsorbtance = null;
                    // Getting thermal assets:
                    ElementId thermalAssetId = layerRoofMaterial.ThermalAssetId;
                    PropertySetElement pse = doc.GetElement(thermalAssetId) as PropertySetElement;
                    if (pse == null) continue;
                    ThermalAsset asset = pse.GetThermalAsset();
                    double conductivity = Math.Round(ImperialToMetricConverter.ConvertThermalConductivityImpToMet(asset.ThermalConductivity), 3);
                    double density = Math.Round(ImperialToMetricConverter.ConvertDensityImpToMet(asset.Density), 3);
                    double specificHeat = Math.Round(ImperialToMetricConverter.ConvertSpecificHeatImpToMet(asset.SpecificHeat), 3);

                    SurfaceMaterial layerRoofMaterialToAdd = new SurfaceMaterial(readableName, name, roughness, thickness,
                        conductivity, density, specificHeat, thermalAbsorbtance,
                        solarAbsorbtance, visibleAbsorbtance);

                    layerRoofMaterials.Add(layerRoofMaterialToAdd);
                }
            }

            return layerRoofMaterials;

        }
    }
}
