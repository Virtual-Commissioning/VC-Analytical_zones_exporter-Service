using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VC_Analytical_zones_exporter_Service.git.Helpers
{
    public class HttpClientHelper
    {
        private static HttpClient _httpClient = new HttpClient();

        public static bool POSTData(object json, string url)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(json), System.Text.Encoding.UTF8))
            {

                HttpResponseMessage result = _httpClient.PutAsync(url, content).Result;

                if (result.StatusCode == System.Net.HttpStatusCode.Created)
                    return true;
                string returnValue = result.Content.ReadAsStringAsync().Result;
                throw new Exception($"Failed to POST data: ({result.StatusCode}): {returnValue}");
            }
        }
    }
}
