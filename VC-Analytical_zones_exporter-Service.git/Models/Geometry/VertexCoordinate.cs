using System.Collections.Generic;

namespace VC_Analytical_zones_exporter_Service.Models.Geometry
{
    public class VertexCoordinates
    {
        public List<Coordinate> Vertices { get; set; }

        public VertexCoordinates(List<Coordinate> vertices)
        {
            Vertices = vertices;
        }
    }
}
