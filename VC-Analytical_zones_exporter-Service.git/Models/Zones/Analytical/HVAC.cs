namespace VC_Analytical_zones_exporter_Service.Models.Zones.Analytical
{
    public class HVAC
    {
        public Thermostat Thermostat { get; set; }
        public AirLoadSystem IdealAirLoadsSystem { get; set; }

        public HVAC(Thermostat thermostat, AirLoadSystem airLoadSystem)
        {
            Thermostat = thermostat;
            IdealAirLoadsSystem = airLoadSystem;
        }
    }
}
