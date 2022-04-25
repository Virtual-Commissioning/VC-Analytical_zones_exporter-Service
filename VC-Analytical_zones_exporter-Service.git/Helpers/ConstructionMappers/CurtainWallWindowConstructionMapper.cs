using Autodesk.Revit.DB;
using System.Collections.Generic;
using VC_Analytical_zones_exporter_Service.Models.Construction;

namespace VC_Analytical_zones_exporter_Service.Helpers.ConstructionMappers
{
    class CurtainWallWindowConstructionMapper
    {
        public static List<Dictionary<string, SurfaceConstruction>> MapAllCurtainWallWindows(FilteredElementCollector allWalls)
        {
            List<Dictionary<string, SurfaceConstruction>> surfaceConstructions = new List<Dictionary<string, SurfaceConstruction>>();

            foreach (Wall wall in allWalls)
            {
                if (wall.WallType.Kind.ToString() != "Curtain") continue;
                string constructionId = "CW_" + wall.Id.ToString() + "_Window";
                string materialId = "CW_Window_Mat_" + wall.Id.ToString();
                string layerId = "Layer1";

                List<Dictionary<string, string>> constructionLayers = new List<Dictionary<string, string>>();
                Dictionary<string, string> constructionLayerToAdd = new Dictionary<string, string>();
                constructionLayerToAdd.Add(layerId, materialId);
                constructionLayers.Add(constructionLayerToAdd);
                SurfaceConstruction surfaceConstructionToAdd = new SurfaceConstruction(constructionId, constructionLayers);

                Dictionary<string, SurfaceConstruction> linkedSurfaceConstruction = new Dictionary<string, SurfaceConstruction>();
                linkedSurfaceConstruction.Add(constructionId, surfaceConstructionToAdd);
                surfaceConstructions.Add(linkedSurfaceConstruction);

            }

            return surfaceConstructions;
        }
    }
}
