using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VC_Analytical_zones_exporter_Service.Models.Construction;

namespace VC_Analytical_zones_exporter_Service.git.Helpers.ConstructionMappers
{
    class WindowConstructionMapper
    {
        public static List<Dictionary<string, SurfaceConstruction>> MapAllWindows(FilteredElementCollector allWindows, Autodesk.Revit.DB.Document doc)   //(Autodesk.Revit.DB.Document doc, Wall walls)    
        {
            List<Dictionary<string, SurfaceConstruction>> surfaceConstructions = new List<Dictionary<string, SurfaceConstruction>>();

            foreach (FamilyInstance window in allWindows)
            {
                string constructionId = window.Id.ToString();
                string materialId = window.Id.ToString();
                string layerId = "Layer1";

                List<Dictionary<string, string>> constructionLayers = new List<Dictionary<string, string>>();
                Dictionary<string, string> constructionLayerToAdd = new Dictionary<string, string>();
                constructionLayerToAdd.Add(layerId, materialId);
                constructionLayers.Add(constructionLayerToAdd);
                SurfaceConstruction surfaceConstructionToAdd = new SurfaceConstruction(constructionId, constructionLayers);

                Dictionary<string, SurfaceConstruction> linkedSurfaceConstruction = new Dictionary<string, SurfaceConstruction>
                {
                    { constructionId, surfaceConstructionToAdd }
                };

                surfaceConstructions.Add(linkedSurfaceConstruction);

            }

            return surfaceConstructions;
        }
    }
}
