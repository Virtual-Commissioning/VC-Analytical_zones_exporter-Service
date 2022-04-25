using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
