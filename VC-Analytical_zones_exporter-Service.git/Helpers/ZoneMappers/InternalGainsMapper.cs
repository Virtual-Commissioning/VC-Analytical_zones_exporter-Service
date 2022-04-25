using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VC_Analytical_zones_exporter_Service.Helpers.ZoneMappers.InternalGainsMappers;
using VC_Analytical_zones_exporter_Service.Models.Zones.Analytical.InternalGains;

namespace VC_Analytical_zones_exporter_Service.git.Helpers.ZoneMappers
{
    public class InternalGainsMapper
    {
        public static InternalGains MapInternalGains
            (string analyticalZoneId)
        {
            List<People> people = PeopleMapper.MapPeople(analyticalZoneId);
            List<Lighting> lighting = LightingMapper.MapLighting(analyticalZoneId);
            List<Equipment> equipment = EquipmentMapper.MapEquipment(analyticalZoneId);

            InternalGains allInternalGains = new InternalGains(people, lighting, equipment);

            return allInternalGains;
        }

    }
}
