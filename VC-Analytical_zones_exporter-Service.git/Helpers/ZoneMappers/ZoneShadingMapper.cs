using System.Collections.Generic;
using VC_Analytical_zones_exporter_Service.Models.Geometry;
using VC_Analytical_zones_exporter_Service.Models.Shading;

namespace VC_Analytical_zones_exporter_Service.Helpers.ZoneMappers
{
    public class ZoneShadingMapper
    {
        public static List<ShadingZone> MapZoneShading(string analyticalZoneId)
        {
            List<ShadingZone> shadingZones = new List<ShadingZone>();
            string id = "Zone" + analyticalZoneId + "_" + "Shading";
            string baseSurfId = "";
            string transmSchedule = "";
            VertexCoordinates vertexCoordinates = null;

            ShadingZone shadingZone = new ShadingZone(id, baseSurfId, transmSchedule, vertexCoordinates);
            shadingZones.Add(shadingZone);
            return shadingZones;
        }
    }
}
