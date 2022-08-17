namespace VC_Analytical_zones_exporter_Service.Models.Surfaces
{
    public enum SurfaceType
    {
        Wall,
        Floor,
        Ceiling,
        Roof
    }
    public class Type
    {
        public SurfaceType SurfaceType { get; set; }

    }
}
