using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Analysis;
using System.Collections.Generic;
using VC_Analytical_zones_exporter_Service.Models.SubSurface;
using Opening = VC_Analytical_zones_exporter_Service.Models.SubSurface.Opening;

namespace VC_Analytical_zones_exporter_Service.Helpers.ZoneMappers
{
    public class SubSurfaceMapper
    {
        public static SubSurfaceType MapSubSurfaces
            (EnergyAnalysisSurface energyAnalysisSurface, EnergyAnalysisSpace energyAnalysisSpace, Document doc,
            FilteredElementCollector allAnalyticalSubSurfaces, string analyticalZoneId, string constructionId)
        {
            List<DoorAndWindow> subSurfaces = DoorAndWindowSubSurfaceMapper.MapDoorAndWindowSubSurfaces(energyAnalysisSurface, energyAnalysisSpace, doc, allAnalyticalSubSurfaces, analyticalZoneId);
            List<Opening> subSurfaceOpenings = OpeningSubSurfaceMapper.MapOpeningSubSurfaces(energyAnalysisSurface, doc, allAnalyticalSubSurfaces, analyticalZoneId);

            List<Dictionary<string, DoorAndWindow>> allSubSurfDoors = new List<Dictionary<string, DoorAndWindow>>();
            List<Dictionary<string, DoorAndWindow>> allSubSurfWindows = new List<Dictionary<string, DoorAndWindow>>();
            List<Dictionary<string, Opening>> allSubSurfOpenings = new List<Dictionary<string, Opening>>();

            foreach (Opening subSurfOpening in subSurfaceOpenings)
            {
                Dictionary<string, Opening> linkedSubSurf = new Dictionary<string, Opening>();
                linkedSubSurf.Add(subSurfOpening.Name, subSurfOpening);
                allSubSurfOpenings.Add(linkedSubSurf);
            }
            foreach (DoorAndWindow subSurfDoorAndWindow in subSurfaces)
            {
                if (subSurfDoorAndWindow.Surface_Type == "Window")
                {
                    Dictionary<string, DoorAndWindow> linkedSubSurf = new Dictionary<string, DoorAndWindow>();
                    linkedSubSurf.Add(subSurfDoorAndWindow.Name, subSurfDoorAndWindow);
                    allSubSurfWindows.Add(linkedSubSurf);
                }
                else if (subSurfDoorAndWindow.Surface_Type == "Door")
                {
                    Dictionary<string, DoorAndWindow> linkedSubSurf = new Dictionary<string, DoorAndWindow>();
                    linkedSubSurf.Add(subSurfDoorAndWindow.Name, subSurfDoorAndWindow);
                    allSubSurfDoors.Add(linkedSubSurf);
                }
                else continue;
            }
            if (constructionId.Contains("CW_") == true)
            {
                DoorAndWindow curtainWallWindows = CurtainWallWindowSubSurfaceMapper.MapCurtainWallWindowSubSurfaces
                    (energyAnalysisSurface, energyAnalysisSpace, doc, constructionId, analyticalZoneId);
                Dictionary<string, DoorAndWindow> linkedSubSurf = new Dictionary<string, DoorAndWindow>();
                linkedSubSurf.Add(curtainWallWindows.Name, curtainWallWindows);
                allSubSurfWindows.Add(linkedSubSurf);
            }

            SubSurfaceType AllSubSurfaces = new SubSurfaceType(allSubSurfDoors, allSubSurfWindows, allSubSurfOpenings);

            return AllSubSurfaces;
        }

    }
}
