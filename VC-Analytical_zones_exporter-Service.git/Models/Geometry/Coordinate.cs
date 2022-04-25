using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VC_Analytical_zones_exporter_Service.Models.Geometry
{
    public class Coordinate
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Coordinate(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
