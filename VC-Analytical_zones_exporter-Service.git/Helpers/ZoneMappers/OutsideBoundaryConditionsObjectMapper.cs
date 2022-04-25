using Autodesk.Revit.DB.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VC_Analytical_zones_exporter_Service.Helpers.ZoneMappers
{
    public class OutsideBoundaryConditionsObjectMapper
    {
        public static string MapOutsideBCObj(EnergyAnalysisSurface energyAnalysisSurface, 
                                             string outsideBC, 
                                             string surfaceAnalyticalSpaceId, 
                                             string analyticalZoneId)
        {
            
            string outsideBCObjType;
            
            if (outsideBC == "Zone")
            {
                outsideBCObjType = energyAnalysisSurface.GetAdjacentAnalyticalSpace().Id.ToString();
            }
            
            else if (outsideBC == "Surface")
            {
                
                if (surfaceAnalyticalSpaceId == analyticalZoneId)
                {
                    outsideBCObjType = energyAnalysisSurface.Id.ToString() + "_" + energyAnalysisSurface.GetAdjacentAnalyticalSpace().Id.ToString();
                }
                
                else
                {
                    outsideBCObjType = energyAnalysisSurface.Id.ToString() + "_" + energyAnalysisSurface.GetAnalyticalSpace().Id.ToString();
                }

            }
            
            else
            {
                outsideBCObjType = "";
            }

            return outsideBCObjType;
        }
    }
}
