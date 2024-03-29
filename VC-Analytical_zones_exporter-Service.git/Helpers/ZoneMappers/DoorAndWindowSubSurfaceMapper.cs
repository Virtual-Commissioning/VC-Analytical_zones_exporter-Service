﻿using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Analysis;
using System.Collections.Generic;
using VC_Analytical_zones_exporter_Service.Models.Geometry;
using VC_Analytical_zones_exporter_Service.Models.SubSurface;

namespace VC_Analytical_zones_exporter_Service.Helpers.ZoneMappers
{
    public class DoorAndWindowSubSurfaceMapper
    {
        public static List<DoorAndWindow> MapDoorAndWindowSubSurfaces
            (EnergyAnalysisSurface energyAnalysisSurface, EnergyAnalysisSpace energyAnalysisSpace, Document doc, FilteredElementCollector allAnalyticalSurfaces, string analyticalZoneId)
        {
            List<DoorAndWindow> allSubSurfaces = new List<DoorAndWindow>();

            EnergyAnalysisSurface analyticalSurface = doc.GetElement(energyAnalysisSurface.Id) as EnergyAnalysisSurface;
            IList<EnergyAnalysisOpening> subSurfaces = analyticalSurface.GetAnalyticalOpenings();
            foreach (EnergyAnalysisOpening opening in subSurfaces)
            {
                if (opening.OpeningType.ToString() == "Air") continue;
                string name;
                if (opening.OpeningType.ToString() == "Door")
                {
                    if (energyAnalysisSurface.GetAdjacentAnalyticalSpace() != null)
                    {
                        if (energyAnalysisSurface.GetAnalyticalSpace().Id == energyAnalysisSpace.Id)
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
                }
                else
                {
                    name = opening.Id.ToString();
                }
                string subSurfType = opening.OpeningType.ToString();
                Coordinate openingCenter = SubSurfaceCenterMapper.MapSubSurface(opening, doc);
                string constructionId = GetAssociatedOpeningConstruction.MapAssociatedOpeningConstruction(openingCenter, doc, opening);
                string hostSurfId;
                if (energyAnalysisSurface.SurfaceType.ToString() == "InteriorWall" ||
                    energyAnalysisSurface.SurfaceType.ToString() == "InteriorFloor")
                {
                    if (energyAnalysisSurface.GetAnalyticalSpace().Id == energyAnalysisSpace.Id)
                    {
                        hostSurfId = energyAnalysisSurface.Id.ToString() + "_" + energyAnalysisSurface.GetAnalyticalSpace().Id.ToString();
                    }
                    else
                    {
                        hostSurfId = energyAnalysisSurface.Id.ToString() + "_" + energyAnalysisSurface.GetAdjacentAnalyticalSpace().Id.ToString();
                    }
                }
                else
                {
                    hostSurfId = energyAnalysisSurface.Id.ToString();
                }
                string outsideBCObj;
                if (energyAnalysisSurface.GetAdjacentAnalyticalSpace() != null)
                {
                    if (energyAnalysisSurface.GetAnalyticalSpace().Id == energyAnalysisSpace.Id)
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
                string viewFactorToGround = "";
                int multiplier = 1;
                FrameAndDivider frameAndDivider = FrameAndDividerMapper.MapFrameAndDivider(opening);
                string frameAndDividerName = frameAndDivider.Name;
                List<Coordinate> vertices = SubSurfaceGeometryMapper.MapSubSurfaceGeometry(opening, doc, energyAnalysisSurface, analyticalZoneId);
                
                DoorAndWindow subSurfaceToAdd = new DoorAndWindow(name, 
                                                                  subSurfType, 
                                                                  constructionId, 
                                                                  hostSurfId, 
                                                                  outsideBCObj, 
                                                                  viewFactorToGround,
                                                                  frameAndDividerName, 
                                                                  multiplier, 
                                                                  vertices, 
                                                                  frameAndDivider);

                allSubSurfaces.Add(subSurfaceToAdd);
            }


            return allSubSurfaces;
        }

    }
}
