using System;
using System.IO;
using Newtonsoft.Json;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Analysis;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;

using System.Collections.Generic;
using VC_Analytical_zones_exporter_Service.Models.Site;
using VC_Analytical_zones_exporter_Service.Models.Construction;
using VC_Analytical_zones_exporter_Service.Models.Zones.Analytical.Material;
using VC_Analytical_zones_exporter_Service.Helpers;
using VC_Analytical_zones_exporter_Service.Helpers.MaterialMappers;
using VC_Analytical_zones_exporter_Service.Helpers.ConstructionMappers;
using VC_Analytical_zones_exporter_Service.git.Helpers;

namespace AnalyticalZonesMapper
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class AnalyticalZonesSelector : IExternalCommand
    {
        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var doc = uiapp.ActiveUIDocument.Document;

            List<Materials> allMaterials = new List<Materials>();
            List<Constructions> allConstructions = new List<Constructions>();
            Dictionary<string, Site> bot = new Dictionary<string, Site>();

            var allSpaces = new FilteredElementCollector(doc).WherePasses(new ElementClassFilter(typeof(SpatialElement))); //New way to filter classes
            var allWalls = new FilteredElementCollector(doc).OfClass(typeof(Autodesk.Revit.DB.Wall));
            var allRoofs = new FilteredElementCollector(doc).WherePasses(new ElementClassFilter(typeof(RoofBase)));
            var allFloors = new FilteredElementCollector(doc).OfClass(typeof(Floor));
            var allDoors = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_Doors);
            var allWindows = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_Windows);
            var allOpenings = new FilteredElementCollector(doc).OfClass(typeof(Opening));
            var allAnalyticalSurfaces = new FilteredElementCollector(doc).OfClass(typeof(EnergyAnalysisSurface));
            var allAnalyticalSpaces = new FilteredElementCollector(doc).OfClass(typeof(EnergyAnalysisSpace));
            var allAnalyticalSubSurfaces = new FilteredElementCollector(doc).OfClass(typeof(EnergyAnalysisOpening));
            //var allMasses = new FilteredElementCollector(doc).WherePasses(new ElementCategoryFilter(BuiltInCategory.OST_Mass));
            ElementCategoryFilter massCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_Mass);
            ElementClassFilter familySymbolFilter = new ElementClassFilter(typeof(FamilyInstance));
            LogicalAndFilter logicalAndFiter = new LogicalAndFilter(massCategoryFilter, familySymbolFilter);
            FilteredElementCollector allMasses = new FilteredElementCollector(doc).WherePasses(logicalAndFiter);

            allMaterials = MaterialMapper.MapAllMaterials(allWalls, allRoofs, allFloors, allDoors, allWindows, doc);
            allConstructions = ConstructionMapper.MapAllConstructions(allWalls, allFloors, allRoofs, allSpaces, allDoors, allWindows, allOpenings, doc);
            bot.Add("Site", SiteMapper.MapSite(doc, allSpaces, allAnalyticalSurfaces, allAnalyticalSpaces, allAnalyticalSubSurfaces, allMasses, allWalls, commandData));

            //(string userId, string projectId, string url) = HelperFunctions.PromptToken();

            string userId = "something";
            string projectId = "test";
            string url = "something";

            string serializedJson = JsonParser.ParseToJson(allMaterials, allConstructions, bot, userId, projectId);

            HttpClientHelper.POSTData(serializedJson, url);

            return Result.Succeeded;
        }
    }
}
