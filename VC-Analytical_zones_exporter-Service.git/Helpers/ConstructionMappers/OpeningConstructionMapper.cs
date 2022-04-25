using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VC_Analytical_zones_exporter_Service.Models.Construction;

namespace VC_Analytical_zones_exporter_Service.Helpers.ConstructionMappers
{
    public class OpeningConstructionMapper
    {
        public static List<Dictionary<string, OpeningConstruction>> MapAllOpeningConstructions(FilteredElementCollector allOpenings)
        {
            List<Dictionary<string, OpeningConstruction>> allOpeningConstructions = new List<Dictionary<string, OpeningConstruction>>();

            foreach (Opening opening in allOpenings)
            {
                string openingConstructionId = opening.Id.ToString();
                string airExchangeMethod = "";
                string airMixingChangesPerHour = "";
                string simpleMixingPerHour = "";
                OpeningConstruction openingConstructionToAdd = new OpeningConstruction(openingConstructionId, airExchangeMethod, airMixingChangesPerHour, simpleMixingPerHour);
                Dictionary<string, OpeningConstruction> linkedOpeningConstructions = new Dictionary<string, OpeningConstruction>();
                linkedOpeningConstructions.Add(openingConstructionId, openingConstructionToAdd);
                allOpeningConstructions.Add(linkedOpeningConstructions);
            }

            return allOpeningConstructions;
        }

    }
}
