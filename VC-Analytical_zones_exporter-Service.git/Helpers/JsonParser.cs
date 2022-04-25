using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using VC_Analytical_zones_exporter_Service.Models.Construction;
using VC_Analytical_zones_exporter_Service.Models.Site;
using VC_Analytical_zones_exporter_Service.Models.Zones.Analytical.Material;

namespace VC_Analytical_zones_exporter_Service.Helpers
{
    public class JsonParser
    {
        public static string ParseToJson(List<Materials> materialsToParse,
            List<Constructions> constructionsToParse, Dictionary<string, Site> siteToParse, string userId, string projectId)
        {
            string materials = ParseMaterialsToJson(materialsToParse);
            string constructions = ParseConstructionsToJson(constructionsToParse);
            string site = ParseSiteToJson(siteToParse);

            string userIdTag = "\"userID\": ";
            string projectIdTag = "\"projectID\": ";

            string jsonToWebApp = String.Concat("{",
                userIdTag,
                $"\"{userId}\"",
                ",",
                projectIdTag,
                $"\"{projectId}\"",
                ",",
                "\"Materials\":",
                materials,
                ",",
                "\"Constructions\":",
                constructions,
                ",",
                "\"BOT\":",
                site);

            return jsonToWebApp;
        }

        public static string ParseMaterialsToJson(List<Materials> materialsToParse)
        {
            return JsonConvert.SerializeObject(materialsToParse);
        }

        public static string ParseConstructionsToJson(List<Constructions> constructionsToParse)
        {
            return JsonConvert.SerializeObject(constructionsToParse);
        }

        public static string ParseSiteToJson(Dictionary<string, Site> siteToParse)
        {
            return JsonConvert.SerializeObject(siteToParse);
        }


    }
}
