using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VC_Analytical_zones_exporter_Service.Models.Zones.Analytical.Material
{
    public class WindowMaterial
    {
        public string Name { get; set; }
        public double? UFactor { get; set; }
        public double? Solar_Heat_Gain_Coefficient { get; set; }
        public double? Visible_Transmittance { get; set; }


        public WindowMaterial(string id, double? uFactor, double? solarHeatGain, double? visibleTransmittance)
        {
            Name = id;
            UFactor = uFactor;
            Solar_Heat_Gain_Coefficient = solarHeatGain;
            Visible_Transmittance = visibleTransmittance;
        }
    }
}
