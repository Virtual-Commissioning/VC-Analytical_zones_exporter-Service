using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VC_Analytical_zones_exporter_Service.Models.Surfaces
{
    public enum SurfaceType
    {
        Wall,
        Floor,
        Ceiling,
        Roof
    }
    public class Type
    {
        public SurfaceType SurfaceType { get; set; }

    }
}
