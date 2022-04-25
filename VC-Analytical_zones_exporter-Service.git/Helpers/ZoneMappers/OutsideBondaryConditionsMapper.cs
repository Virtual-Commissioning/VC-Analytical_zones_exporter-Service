using Autodesk.Revit.DB.Analysis;
using System.Linq;
using VC_Analytical_zones_exporter_Service.Models.Surfaces;

namespace VC_Analytical_zones_exporter_Service.Helpers.ZoneMappers
{
    public class OutsideBoundaryConditionsMapper
    {
        public static string MapOutsideBC(EnergyAnalysisSurface energyAnalysisSurface)
        {
            string outsideBCType;
            if ((energyAnalysisSurface.SurfaceType.ToString() == "InteriorWall" && energyAnalysisSurface.GetAnalyticalOpenings().Count == 0) ||
                energyAnalysisSurface.SurfaceType.ToString() == "InteriorFloor")
            {
                outsideBCType = OutsideBoundaryConditionsEnum.Surface.ToString();
            }
            else if (energyAnalysisSurface.SurfaceType.ToString() == "InteriorWall" &&
                energyAnalysisSurface.GetAnalyticalOpenings().Count > 0)
            {
                outsideBCType = OutsideBoundaryConditionsEnum.Surface.ToString();
            }
            else if (energyAnalysisSurface.SurfaceType.ToString() == "ExteriorWall" ||
                energyAnalysisSurface.SurfaceType.ToString() == "ExteriorFloor" ||
                energyAnalysisSurface.SurfaceType.ToString() == "Roof")
            {
                outsideBCType = OutsideBoundaryConditionsEnum.Outdoors.ToString();
            }
            else if (energyAnalysisSurface.SurfaceType.ToString() == "Ceiling")
            {
                outsideBCType = OutsideBoundaryConditionsEnum.Adiabatic.ToString();
            }
            else if (energyAnalysisSurface.SurfaceType.ToString() == "Underground")
            {
                outsideBCType = OutsideBoundaryConditionsEnum.Ground.ToString();
            }
            else
            {
                outsideBCType = null;
            }

            return outsideBCType;
        }
    }
}
