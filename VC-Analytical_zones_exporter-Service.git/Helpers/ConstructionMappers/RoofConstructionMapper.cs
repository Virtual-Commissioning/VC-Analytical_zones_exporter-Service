﻿using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using VC_Analytical_zones_exporter_Service.Models.Construction;

namespace VC_Analytical_zones_exporter_Service.Helpers.ConstructionMappers
{
    class RoofConstructionMapper
    {
        public static List<Dictionary<string, SurfaceConstruction>> MapAllRoofs(FilteredElementCollector allRoofs, 
                                                                                Autodesk.Revit.DB.Document doc)
        {
            List<Dictionary<string, SurfaceConstruction>> surfaceConstructions = new List<Dictionary<string, SurfaceConstruction>>();

            foreach (RoofBase roof in allRoofs)
            {
                string constructionId = roof.Id.ToString();

                CompoundStructure structure = roof.RoofType.GetCompoundStructure();
                IList<CompoundStructureLayer> layers = structure.GetLayers();

                List<Dictionary<string, string>> constructionLayers = new List<Dictionary<string, string>>();
                
                foreach (CompoundStructureLayer layer in layers)
                {
                    //Filtering away materials without thermal properties
                    Material layerMaterial = doc.GetElement(layer.MaterialId) as Material;
                    ElementId thermalAssetId = layerMaterial.ThermalAssetId;
                    PropertySetElement pse = doc.GetElement(thermalAssetId) as PropertySetElement;
                    if (pse == null) continue;

                    int layerId = layer.LayerId + 1;
                    string layerName = "Layer" + layerId.ToString();
                    double thickness;
                    
                    if (layer.Width == 0)
                    {
                        thickness = 0.001;
                    }
                    
                    else
                    {
                        thickness = Math.Round(ImperialToMetricConverter.ConvertFromFeetToMeters(layer.Width), 3);
                    }
                    
                    string preMaterialId = layer.MaterialId.ToString() + "_" + thickness;
                    string materialId = preMaterialId.Replace(',', '.');
                    
                    Dictionary<string, string> layerValues = new Dictionary<string, string>
                    {
                        { layerName, materialId }
                    };

                    constructionLayers.Add(layerValues);

                    SurfaceConstruction surfaceConstructionToAdd = new SurfaceConstruction(constructionId, constructionLayers);
                    
                    Dictionary<string, SurfaceConstruction> linkedSurfaceConstruction = new Dictionary<string, SurfaceConstruction>
                    {
                        { constructionId, surfaceConstructionToAdd }
                    };

                    surfaceConstructions.Add(linkedSurfaceConstruction);
                }
            }

            return surfaceConstructions;

        }
    }
}
