namespace VC_Analytical_zones_exporter_Service.Models.Surfaces
{
    public enum OutsideBoundaryConditionsEnum
    {
        Surface,
        Adiabatic,
        Zone,
        Outdoors,
        Ground
    }

    public class OutsideBoundaryConditions
    {
        public string OutsideBoundaryConditionsType { get; set; }

        public OutsideBoundaryConditions(string outsideBoundaryConditionsType)
        {
            OutsideBoundaryConditionsType = outsideBoundaryConditionsType;
        }
    }
}
