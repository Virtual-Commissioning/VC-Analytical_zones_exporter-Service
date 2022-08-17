namespace VC_Analytical_zones_exporter_Service.Models.Zones.Analytical
{
    public class Thermostat
    {
        public string Name { get; set; }
        public string Heating_Setpoint_Schedule_Name { get; set; }
        public string Constant_Heating_Setpoint { get; set; }
        public string Cooling_Setpoint_Schedule_Name { get; set; }
        public string Constant_Cooling_Setpoint { get; set; }

        public Thermostat(string name, string heatingSetpointSchedule, string constantHeatingSetpoint,
                          string coolingSetpointSchedule, string constantCoolingSetpoint)
        {
            Name = name;
            Heating_Setpoint_Schedule_Name = heatingSetpointSchedule;
            Constant_Heating_Setpoint = constantHeatingSetpoint;
            Cooling_Setpoint_Schedule_Name = coolingSetpointSchedule;
            Constant_Cooling_Setpoint = constantCoolingSetpoint;
        }
    }
}
