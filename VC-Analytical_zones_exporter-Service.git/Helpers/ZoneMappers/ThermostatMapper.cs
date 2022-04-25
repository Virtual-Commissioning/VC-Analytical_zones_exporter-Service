using VC_Analytical_zones_exporter_Service.Models.Zones.Analytical;

namespace VC_Analytical_zones_exporter_Service.Helpers.ZoneMappers
{
    public class ThermostatMapper
    {
        public static Thermostat MapThermostat(string analyticalZoneId)
        {
            string id = "Zone" + analyticalZoneId + "_" + "Thermostat";
            string heatingSetpointSchedule = "";
            string constantHeatingSetpoint = "";
            string coolingSetpointSchedule = "";
            string constantCoolingSetpoint = "";

            Thermostat equipmentGains = new Thermostat(id, 
                                                       heatingSetpointSchedule, 
                                                       constantHeatingSetpoint,
                                                       coolingSetpointSchedule, 
                                                       constantCoolingSetpoint);
            return equipmentGains;
        }
    }
}
