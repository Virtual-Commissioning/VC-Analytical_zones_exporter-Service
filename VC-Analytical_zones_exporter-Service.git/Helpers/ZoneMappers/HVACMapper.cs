using VC_Analytical_zones_exporter_Service.Models.Zones.Analytical;

namespace VC_Analytical_zones_exporter_Service.Helpers.ZoneMappers
{
    public class HVACMapper
    {
        public static HVAC MapHVAC
            (string analyticalZoneId)
        {
            Thermostat thermostat = ThermostatMapper.MapThermostat(analyticalZoneId);
            AirLoadSystem airLoadSystem = AirLoadSystemMapper.MapAirLoadSystem(analyticalZoneId);
            HVAC hvac = new HVAC(thermostat, airLoadSystem);

            return hvac;
        }

    }
}
