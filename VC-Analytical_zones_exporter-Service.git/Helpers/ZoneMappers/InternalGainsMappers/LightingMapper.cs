﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VC_Analytical_zones_exporter_Service.Models.Zones.Analytical.InternalGains;

namespace VC_Analytical_zones_exporter_Service.Helpers.ZoneMappers.InternalGainsMappers
{
    public class LightingMapper
    {
        public static List<Lighting> MapLighting(string analyticalZoneId)
        {
            List<Lighting> lighting = new List<Lighting>();
            string id = "Zone" + analyticalZoneId + "_" + "Lighting";
            string zoneId = analyticalZoneId;
            string lightingSchedule = "";
            string calculationMethod = "";
            double? lightingLevel = null;
            double? returnAirFraction = null;
            double? fractionRadiant = null;
            double? fractionVisible = null;
            double? fractionReplaceable = null;
            string endUseSubCategory = "";

            Lighting lightingGains = new Lighting(id, zoneId, lightingSchedule, calculationMethod,
                lightingLevel, returnAirFraction, fractionRadiant,
                fractionVisible, fractionReplaceable, endUseSubCategory);
            lighting.Add(lightingGains);
            return lighting;
        }
    }
}
