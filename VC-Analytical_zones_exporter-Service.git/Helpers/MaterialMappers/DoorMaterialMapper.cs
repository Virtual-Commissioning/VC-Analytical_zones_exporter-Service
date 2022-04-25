using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using VC_Analytical_zones_exporter_Service.Models.Zones.Analytical.Material;

namespace VC_Analytical_zones_exporter_Service.Helpers.MaterialMappers
{
    class DoorMaterialMapper
    {
        public static List<Dictionary<string, DoorMaterial>> MapAllDoors(FilteredElementCollector allDoors, Autodesk.Revit.DB.Document doc)
        {
            List<Dictionary<string, DoorMaterial>> doorMaterials = new List<Dictionary<string, DoorMaterial>>();

            foreach (FamilyInstance door in allDoors)
            {
                ElementId doorSymbol = door.Symbol.Id;
                FamilySymbol doorInfo = doc.GetElement(doorSymbol) as FamilySymbol;
                string id = door.Id.ToString();
                int? roughness = null;
                double? thermalResistance = Math.Round(ImperialToMetricConverter.ConvertThermalResistanceImpToMet(doorInfo.GetThermalProperties().ThermalResistance), 3);
                double? thermalAbsorbtance = null;
                double? solarAbsorbtance = null;
                double? visibleTransmittance = null;
                
                DoorMaterial doorMaterial = new DoorMaterial(id, roughness, thermalResistance,
                    thermalAbsorbtance, solarAbsorbtance, visibleTransmittance);
                
                Dictionary<string, DoorMaterial> linkedDoorMaterial = new Dictionary<string, DoorMaterial>
                {
                    { doorMaterial.Name, doorMaterial }
                };

                doorMaterials.Add(linkedDoorMaterial);
            }
            return doorMaterials;
        }
    }
}
