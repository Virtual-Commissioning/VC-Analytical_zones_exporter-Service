using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using VC_Analytical_zones_exporter_Service.Helpers.BuildingMappers;
using VC_Analytical_zones_exporter_Service.Models;
using VC_Analytical_zones_exporter_Service.Models.Shading;
using VC_Analytical_zones_exporter_Service.Models.Site;

namespace VC_Analytical_zones_exporter_Service.Helpers
{
    class SiteMapper
    {
        public static Site MapSite(Document doc, FilteredElementCollector allSpaces, FilteredElementCollector allAnalyticalSurfaces,
            FilteredElementCollector allAnalyticalSpaces, FilteredElementCollector allAnalyticalSubSurfaces,
            FilteredElementCollector allMasses, FilteredElementCollector allWalls, ExternalCommandData commandData)
        {
            string name = "";
            string latitude = "";
            string longitude = "";
            string timeZone = "";
            string elevation = "";
            List<Dictionary<string, Building>> buildings = BuildingMapper.MapAllBuildings(doc, 
                                                                                          allSpaces, 
                                                                                          allAnalyticalSurfaces, 
                                                                                          allAnalyticalSpaces, 
                                                                                          allAnalyticalSubSurfaces, 
                                                                                          allMasses, 
                                                                                          allWalls, 
                                                                                          commandData);

            List<Dictionary<string, ShadingSite>> shadingSite = SiteShadingMapper.MapSiteShading(allMasses);
            Site site = new Site(name, latitude, longitude, timeZone, elevation, buildings, shadingSite);

            return site;
        }
    }
}
