﻿using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Analysis;
using Autodesk.Revit.DB.Mechanical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VC_Analytical_zones_exporter_Service.Helpers.GeometricOperations;
using VC_Analytical_zones_exporter_Service.Models.Geometry;
using VC_Analytical_zones_exporter_Service.Models.SubSurface;

namespace VC_Analytical_zones_exporter_Service.Helpers.ZoneMappers
{
    public class SurfaceMapper
    {
        public static List<Dictionary<string, Models.Surfaces.Surface>> MapSurfaces(string analyticalZoneId, 
                                                                                    Document doc, 
                                                                                    FilteredElementCollector allAnalyticalSurfaces,
                                                                                    FilteredElementCollector allAnalyticalSubSurfaces, 
                                                                                    EnergyAnalysisSpace energyAnalysisSpace,
                                                                                    Space associatedSpace)
        {
            List<Dictionary<string, Models.Surfaces.Surface>> allSurfaces = new List<Dictionary<string, Models.Surfaces.Surface>>();

            foreach (EnergyAnalysisSurface energyAnalysisSurface in allAnalyticalSurfaces)
            {
                if (energyAnalysisSurface.GetAnalyticalSpace() == null) continue;

                string surfaceAnalyticalSpaceId = energyAnalysisSurface.GetAnalyticalSpace().Id.ToString();
                string surfaceAdjacentAnalyticalSpaceId;

                if (energyAnalysisSurface.GetAdjacentAnalyticalSpace() != null)
                {
                    surfaceAdjacentAnalyticalSpaceId = energyAnalysisSurface.GetAdjacentAnalyticalSpace().Id.ToString();
                }

                else { surfaceAdjacentAnalyticalSpaceId = ""; }
                
                if (surfaceAnalyticalSpaceId == analyticalZoneId || surfaceAdjacentAnalyticalSpaceId == analyticalZoneId) //Checking if the analytical surface belongs to analytical zone
                {
                    string id;
                    
                    if (surfaceAnalyticalSpaceId == analyticalZoneId)
                    {
                        
                        if (energyAnalysisSurface.SurfaceType.ToString() == "InteriorWall" ||
                        energyAnalysisSurface.SurfaceType.ToString() == "InteriorFloor")
                        {
                            id = energyAnalysisSurface.Id.ToString() + "_" + energyAnalysisSurface.GetAnalyticalSpace().Id.ToString();
                        }

                        else
                        {
                            id = energyAnalysisSurface.Id.ToString();
                        }
                    }
                    
                    else
                    {
                        
                        if (energyAnalysisSurface.SurfaceType.ToString() == "InteriorWall" ||
                        energyAnalysisSurface.SurfaceType.ToString() == "InteriorFloor")
                        {
                            id = energyAnalysisSurface.Id.ToString() + "_" + energyAnalysisSurface.GetAdjacentAnalyticalSpace().Id.ToString();
                        }
                        
                        else
                        {
                            id = energyAnalysisSurface.Id.ToString();
                        }
                    }

                    Coordinate planeCenter = PlaneCenterMapper.MapPlaneCenter(energyAnalysisSurface, doc);
                    string constructionId;
                    
                    if (surfaceAnalyticalSpaceId == analyticalZoneId)
                    {
                        constructionId = GetAssociatedConstruction.MapAssociatedConstruction(planeCenter, doc, energyAnalysisSurface);
                    }
                    
                    else
                    {
                        constructionId = GetAssociatedConstruction.MapAssociatedConstruction(planeCenter, doc, energyAnalysisSurface) + "_Adjacent";
                    }
                    
                    string surfType;
                    
                    if (energyAnalysisSurface.SurfaceType.ToString() == "InteriorWall" ||
                        energyAnalysisSurface.SurfaceType.ToString() == "ExteriorWall")
                    {
                        surfType = "Wall";
                    }
                    
                    else if (energyAnalysisSurface.SurfaceType.ToString() == "ExteriorFloor")
                    {
                        surfType = "Floor";
                    }
                    
                    else if (energyAnalysisSurface.SurfaceType.ToString() == "InteriorFloor")
                    {
                        
                        if (surfaceAdjacentAnalyticalSpaceId != analyticalZoneId)
                        {
                            surfType = "Ceiling";
                        }
                        
                        else
                        {
                            surfType = "Floor";
                        }
                    }
                    
                    else
                    {
                        surfType = energyAnalysisSurface.SurfaceType.ToString();
                    }
                    string zoneTag = analyticalZoneId;
                    string outsideBC = OutsideBoundaryConditionsMapper.MapOutsideBC(energyAnalysisSurface);
                    string outsideBCObj = OutsideBoundaryConditionsObjectMapper.MapOutsideBCObj(energyAnalysisSurface, outsideBC, surfaceAnalyticalSpaceId, analyticalZoneId);
                    bool sunExposure;
                    bool windExposure;
                    
                    if ((energyAnalysisSurface.SurfaceType.ToString() == "ExteriorWall") || (energyAnalysisSurface.SurfaceType.ToString() == "Roof"))
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
                    List<Coordinate> vertexCoordinates = SurfaceGeometryMapper.MapSurfaceGeometry(energyAnalysisSurface, doc, analyticalZoneId);
                    SubSurfaceType subSurfType = SubSurfaceMapper.MapSubSurfaces(energyAnalysisSurface, 
                                                                                 energyAnalysisSpace, 
                                                                                 doc, 
                                                                                 allAnalyticalSubSurfaces, 
                                                                                 analyticalZoneId, 
                                                                                 constructionId);

                    List<Coordinate> subSurfaceVertices = new List<Coordinate>();
                    if (energyAnalysisSurface.GetAnalyticalOpenings().Count > 0)
                    {
                        foreach (EnergyAnalysisOpening opening in energyAnalysisSurface.GetAnalyticalOpenings())
                        {
                            List<Coordinate> coordinates = SubSurfaceGeometryMapper.MapSubSurfaceGeometry(opening, doc, energyAnalysisSurface, analyticalZoneId);
                            subSurfaceVertices.AddRange(coordinates);
                        }
                    }
                    vertexCoordinates.RemoveAll(p1 => subSurfaceVertices.Exists(p2 => p2.X == p1.X && p2.Y == p1.Y && p2.Z == p1.Z + 0.00001));
                    
                    //Finding face normal
                    Document surfDoc = energyAnalysisSurface.Document;
                    Application app = surfDoc.Application;
                    Options opt = app.Create.NewGeometryOptions();
                    GeometryElement geo = energyAnalysisSurface.get_Geometry(opt);
                    XYZ faceNormal = new XYZ();
                    
                    foreach (GeometryObject obj in geo)
                    {
                        Solid solid = obj as Solid;
                        
                        if (null != solid)
                        {
                            
                            foreach (Face face in solid.Faces)
                            {
                                PlanarFace planarFace = face as PlanarFace;
                                
                                if (energyAnalysisSurface.GetAnalyticalSpace().Id.ToString() == analyticalZoneId)
                                { faceNormal = -planarFace.FaceNormal; }
                                
                                else
                                { faceNormal = planarFace.FaceNormal; }
                            }
                        }
                    }
                    
                    if (surfType == "Floor" || surfType == "Roof" || surfType == "Ceiling")
                    {
                        faceNormal = -faceNormal;
                    }
                    
                    List<Coordinate> sortedVertices = SortPointsV2.PointSorter(vertexCoordinates, faceNormal);

                    Models.Surfaces.Surface surface = new Models.Surfaces.Surface(id, surfType,
                        constructionId, zoneTag, outsideBC, outsideBCObj, sunExposure, windExposure,
                        viewFactorToGround, sortedVertices, subSurfType);

                    Dictionary<string, Models.Surfaces.Surface> linkedSurfaces = new Dictionary<string, Models.Surfaces.Surface>();
                    linkedSurfaces.Add(id, surface);

                    allSurfaces.Add(linkedSurfaces);
                }
                
                else continue;

            }

            return allSurfaces;
        }

    }
}
