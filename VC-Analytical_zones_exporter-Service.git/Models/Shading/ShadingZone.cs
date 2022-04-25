using VC_Analytical_zones_exporter_Service.Models.Geometry;

namespace VC_Analytical_zones_exporter_Service.Models.Shading
{
    public class ShadingZone
    {
        public string Name { get; set; }
        public string Base_Surface_Name { get; set; }
        public string Transmittance_Schedule_Name { get; set; }
        public VertexCoordinates VertexCoordinates { get; set; }

        public ShadingZone(string id, string baseSurfId, string transmSchedule, VertexCoordinates vertexCoordinates)
        {
            Name = id;
            Base_Surface_Name = baseSurfId;
            Transmittance_Schedule_Name = transmSchedule;
            VertexCoordinates = vertexCoordinates;
        }
    }
}
