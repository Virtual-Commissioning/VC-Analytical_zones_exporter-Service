using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VC_Analytical_zones_exporter_Service.Models.Zones.Analytical.Material
{
    public class DoorMaterial
    {
        public string Name { get; set; }
        public int? Roughness { get; set; }
        public double? Thermal_Resistance { get; set; }
        public double? Thermal_Absorptance { get; set; }
        public double? Solar_Absorptance { get; set; }
        public double? Visible_Absorptance { get; set; }

        public DoorMaterial(string name, int? roughness, double? thermalResistance, double? thermalAbsorbtance,
            double? solarAbsorptance, double? visibleAbsorptance)
        {
            Name = name;
            Roughness = roughness;
            Thermal_Resistance = thermalResistance;
            Thermal_Absorptance = thermalAbsorbtance;
            Solar_Absorptance = solarAbsorptance;
            Visible_Absorptance = visibleAbsorptance;
        }
    }
}
