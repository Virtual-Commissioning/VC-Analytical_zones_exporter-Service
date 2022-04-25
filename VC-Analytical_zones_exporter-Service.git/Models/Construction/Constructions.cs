using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VC_Analytical_zones_exporter_Service.Models.Construction
{
    public class Constructions
    {
        public List<Dictionary<string, SurfaceConstruction>> SurfaceConstructions { get; set; }
        public List<Dictionary<string, OpeningConstruction>> Construction_AirBoundary { get; set; }

        public Constructions(List<Dictionary<string, SurfaceConstruction>> surfaceConstructions, List<Dictionary<string, OpeningConstruction>> openingConstructions)
        {
            SurfaceConstructions = surfaceConstructions;
            Construction_AirBoundary = openingConstructions;
        }
    }
}
