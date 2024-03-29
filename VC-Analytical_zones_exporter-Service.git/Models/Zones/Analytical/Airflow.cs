﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VC_Analytical_zones_exporter_Service.Models.Zones.Analytical
{
    public class AirFlow
    {
        public string Id { get; set; }
        public string Tag { get; set; }
        public string ZoneId { get; set; }
        public double Airflow { get; set; }
        public string CalculationMethod { get; set; }
        public double DesignFlowRate { get; set; }
        public double FlowRatePrArea { get; set; }
        public double FlowRatePrPerson { get; set; }
        public double AirChangesPrHour { get; set; }
        public string SourceZoneId { get; set; }
        public double DeltaTemperature { get; set; }

        public AirFlow(string id, string tag, string zoneId, double airflow, string calculationMethod, double designFlowRate, double flowRatePrArea, double flowRatePrPerson, double airChangesPrHour, string sourceZoneId, double deltaTemperature)
        {
            Id = id;
            Tag = tag;
            ZoneId = zoneId;
            Airflow = airflow;
            CalculationMethod = calculationMethod;
            DesignFlowRate = designFlowRate;
            FlowRatePrArea = flowRatePrArea;
            FlowRatePrPerson = flowRatePrPerson;
            AirChangesPrHour = airChangesPrHour;
            SourceZoneId = sourceZoneId;
            DeltaTemperature = deltaTemperature;
        }
    }
}
