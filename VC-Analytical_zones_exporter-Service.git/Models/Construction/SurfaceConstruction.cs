using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VC_Analytical_zones_exporter_Service.Models.Construction
{
    public class SurfaceConstruction
    {
        public string Name { get; set; }
        public List<Dictionary<string, string>> Layers { get; set; }

        public SurfaceConstruction(string name, 
                                   List<Dictionary<string, string>> layers)
        {
            Name = name;
            Layers = layers;
        }
    }
}
