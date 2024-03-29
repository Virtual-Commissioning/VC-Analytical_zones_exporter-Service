﻿using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Analysis;
using System;
using System.Collections.Generic;
using VC_Analytical_zones_exporter_Service.git.Helpers.ZoneMappers;
using VC_Analytical_zones_exporter_Service.git.Models.Zones.Analytical;
using VC_Analytical_zones_exporter_Service.Models.Shading;
using VC_Analytical_zones_exporter_Service.Models.Zones;
using VC_Analytical_zones_exporter_Service.Models.Zones.Analytical;
using VC_Analytical_zones_exporter_Service.Models.Zones.Analytical.InternalGains;

namespace VC_Analytical_zones_exporter_Service.Helpers.ZoneMappers
{
    public class ZoneMapper
    {
        public static List<Dictionary<string, Zone>> MapAllZones
            (FilteredElementCollector allSpaces, Document doc, FilteredElementCollector allAnalyticalSurfaces,
            FilteredElementCollector allAnalyticalSpaces, FilteredElementCollector allAnalyticalSubSurfaces)
        {
            List<Dictionary<string, Zone>> allZones = new List<Dictionary<string, Zone>>();
            int n = 0;
            List<EnergyAnalysisSpace> energyAnalysisSpaces = new List<EnergyAnalysisSpace>();
            foreach (EnergyAnalysisSpace space in allAnalyticalSpaces)
            {
                energyAnalysisSpaces.Add(space);
            }

            foreach (SpatialElement zone in allSpaces)
            {
                if (zone.Category.Name != "Spaces") continue;
                if (zone.Location == null) continue;
                //string tag = zone.Id.ToString();
                Autodesk.Revit.DB.Mechanical.Space associatedSpace = (Autodesk.Revit.DB.Mechanical.Space)zone;
                double x = 0;
                double y = 0;
                double z = 0;

                string zoneType;
                if (associatedSpace.SpaceType.ToString() == string.Empty)
                {
                    zoneType = "";
                }
                else
                {
                    zoneType = associatedSpace.SpaceType.ToString();
                }
                //string id = associatedSpace.Id.ToString();
                string analyticalZoneId = energyAnalysisSpaces[n].Id.ToString();
                double ceilingHeight = Math.Round(ImperialToMetricConverter.ConvertFromFeetToMeters(associatedSpace.UnboundedHeight), 3);
                double floorArea = Math.Round(ImperialToMetricConverter.ConvertFromSqFeetToSqMeters(associatedSpace.Area), 3);
                double zoneVolume = ceilingHeight * floorArea;
                string intConvAlg = "";
                string outConvAlg = "";
                bool includedInTotArea = true;

                List<Dictionary<string, Models.Surfaces.Surface>> surfaces = SurfaceMapper.MapSurfaces(analyticalZoneId, 
                                                                                                   doc, 
                                                                                                   allAnalyticalSurfaces, 
                                                                                                   allAnalyticalSubSurfaces, 
                                                                                                   energyAnalysisSpaces[n], 
                                                                                                   associatedSpace);

                InternalGains internalGains = InternalGainsMapper.MapInternalGains(analyticalZoneId);
                HVAC hvac = HVACMapper.MapHVAC(analyticalZoneId);
                Infiltration infiltration = InfiltrationMapper.MapInfiltration(analyticalZoneId);
                List<ShadingZone> shadingZone = ZoneShadingMapper.MapZoneShading(analyticalZoneId);
                var zoneToAdd = new Zone(analyticalZoneId,
                                     x,
                                     y,
                                     z,
                                     zoneType,
                                     ceilingHeight,
                                     floorArea,
                                     zoneVolume,
                                     intConvAlg,
                                     outConvAlg,
                                     includedInTotArea,
                                     surfaces,
                                     internalGains,
                                     hvac,
                                     infiltration,
                                     shadingZone);

                Dictionary<string, Zone> linkedZone = new Dictionary<string, Zone>();
                linkedZone.Add(analyticalZoneId, zoneToAdd);
                allZones.Add(linkedZone);

                n++;
            }

            return allZones;
        }
    }
}
