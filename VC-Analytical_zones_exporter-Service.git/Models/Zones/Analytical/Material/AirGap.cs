namespace VC_Analytical_zones_exporter_Service.Models.Zones.Analytical.Material
{
    public class AirGapMaterial
    {
        public string Name { get; set; }
        public double? Thermal_Resistance { get; set; }

        public AirGapMaterial(string name, double? thermalResistance)
        {
            Name = name;
            Thermal_Resistance = thermalResistance;
        }
    }
}
