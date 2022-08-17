using System.Collections.Generic;

namespace VC_Analytical_zones_exporter_Service.Models.Zones.Analytical.InternalGains
{
    public class InternalGains
    {
        public List<People> People { get; set; }
        public List<Lighting> Lights { get; set; }
        public List<Equipment> Equipment { get; set; }

        public InternalGains(List<People> people,
                             List<Lighting> lighting,
                             List<Equipment> equipment)
        {
            People = people;
            Lights = lighting;
            Equipment = equipment;
        }
    }
}
