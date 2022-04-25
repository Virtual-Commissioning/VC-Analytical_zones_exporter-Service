namespace VC_Analytical_zones_exporter_Service.Models.Surfaces
{
    public enum OutsideBoundaryConditionsObjectEnum
    {
        IntWallBetweenZones,
        IntWallInZone,
        IntFloor,
        ExtFloor,
        Roof,
        ExtWall
    }

    public class OutsideBoundaryConditionsObject
    {
        public string OutsideBoundaryConditionsObjectType { get; set; }

        public OutsideBoundaryConditionsObject(string outsideBoundaryConditionsObjectType)
        {
            OutsideBoundaryConditionsObjectType = outsideBoundaryConditionsObjectType;
        }
    }
}
