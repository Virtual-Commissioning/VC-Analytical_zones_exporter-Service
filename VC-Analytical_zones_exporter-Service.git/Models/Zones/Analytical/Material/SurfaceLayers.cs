using System.Collections.Generic;
using VC_Analytical_zones_exporter_Service.Models.Surfaces;

namespace VC_Analytical_zones_exporter_Service.Models.Zones.Analytical.Material
{
    public class SurfaceLayers
    {
        public List<Surface> SurfaceLayer { get; set; }

        public SurfaceLayers(List<Surface> surfaceLayer)
        {
            SurfaceLayer = surfaceLayer;
        }
    }
}
