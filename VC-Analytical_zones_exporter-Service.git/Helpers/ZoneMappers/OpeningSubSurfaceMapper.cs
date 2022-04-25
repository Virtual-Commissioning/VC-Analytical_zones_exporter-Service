using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VC_Analytical_zones_exporter_Service.Models.Geometry;
using Opening = VC_Analytical_zones_exporter_Service.Models.SubSurface.Opening;

namespace VC_Analytical_zones_exporter_Service.Helpers.ZoneMappers
{
    public class OpeningSubSurfaceMapper
    {
        public static List<Opening> MapOpeningSubSurfaces(EnergyAnalysisSurface energyAnalysisSurface, 
                                                                 Document doc, 
                                                                 FilteredElementCollector allAnalyticalSurfaces, 
                                                                 string analyticalZoneId)
        {
            List<Opening> allSubSurfaces = new List<Opening>();

            EnergyAnalysisSurface analyticalSurface = doc.GetElement(energyAnalysisSurface.Id) as EnergyAnalysisSurface;
            IList<EnergyAnalysisOpening> subSurfaces = analyticalSurface.GetAnalyticalOpenings();
            foreach (EnergyAnalysisOpening opening in subSurfaces)
            {
                if (opening.OpeningType.ToString() != "Air") continue;
                string name;
                if (energyAnalysisSurface.GetAdjacentAnalyticalSpace() != null)
                {
                    if (energyAnalysisSurface.GetAnalyticalSpace().Id.ToString() == analyticalZoneId)
                    {
                        name = opening.Id.ToString() + "_" + energyAnalysisSurface.GetAnalyticalSpace().Id.ToString();
                    }
                    else
                    {
                        name = opening.Id.ToString() + "_" + energyAnalysisSurface.GetAdjacentAnalyticalSpace().Id.ToString();
                    }
                }
                else
                {
                    name = opening.Id.ToString();
                }
                string subSurfType = "Wall";
                Coordinate openingCenter = SubSurfaceCenterMapper.MapSubSurface(opening, doc);
                string constructionId = GetAssociatedOpeningConstruction.MapAssociatedOpeningConstruction(openingCenter, doc, opening);
                string zoneId = analyticalSurface.GetAdjacentAnalyticalSpace().Id.ToString();
                string outsideBCObj;
                if (energyAnalysisSurface.GetAdjacentAnalyticalSpace() != null)
                {
                    if (energyAnalysisSurface.GetAnalyticalSpace().Id.ToString() == analyticalZoneId)
                    {
                        outsideBCObj = opening.Id.ToString() + "_" + energyAnalysisSurface.GetAdjacentAnalyticalSpace().Id.ToString();
                    }
                    else
                    {
                        outsideBCObj = opening.Id.ToString() + "_" + energyAnalysisSurface.GetAnalyticalSpace().Id.ToString();
                    }
                }
                else
                {
                    outsideBCObj = "";
                }
                string outsideBC = "Surface";
                bool sunExposure;
                bool windExposure;
                if (analyticalSurface.GetAnalyticalOpenings().Count > 0 && analyticalSurface.SurfaceType.ToString() == "ExteriorWall")
                {
                    sunExposure = true;
                    windExposure = true;
                }
                else
                {
                    sunExposure = false;
                    windExposure = false;
                }
                string viewFactorToGround = "";
                List<Coordinate> vertices = SubSurfaceGeometryMapper.MapSubSurfaceGeometry(opening, doc, energyAnalysisSurface, analyticalZoneId);
                Opening subSurfaceToAdd = new Opening(name, 
                                                      subSurfType, 
                                                      constructionId, 
                                                      zoneId, 
                                                      outsideBCObj, 
                                                      outsideBC, 
                                                      sunExposure, 
                                                      windExposure, 
                                                      viewFactorToGround, 
                                                      vertices);

                allSubSurfaces.Add(subSurfaceToAdd);
            }


            return allSubSurfaces;
        }

    }
}
