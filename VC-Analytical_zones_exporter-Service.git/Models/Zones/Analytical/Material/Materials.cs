using System.Collections.Generic;

namespace VC_Analytical_zones_exporter_Service.Models.Zones.Analytical.Material
{
    public class Materials
    {
        public List<Dictionary<string, SurfaceMaterial>> SurfaceMaterials { get; set; }
        public List<Dictionary<string, AirGapMaterial>> AirGapMaterials { get; set; }
        public List<Dictionary<string, DoorMaterial>> DoorMaterials { get; set; }
        public List<Dictionary<string, WindowMaterial>> WindowMaterials { get; set; }

        public Materials(List<Dictionary<string, SurfaceMaterial>> surfaceMaterials, 
                         List<Dictionary<string, AirGapMaterial>> airGapMaterials,
                         List<Dictionary<string, DoorMaterial>> doorMaterials, 
                         List<Dictionary<string, WindowMaterial>> windowMaterials)
        {
            SurfaceMaterials = surfaceMaterials;
            AirGapMaterials = airGapMaterials;
            DoorMaterials = doorMaterials;
            WindowMaterials = windowMaterials;
        }
    }
}
