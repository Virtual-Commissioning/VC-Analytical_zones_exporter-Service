using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Analysis;
using System.Collections.Generic;
using System.Linq;
using VC_Analytical_zones_exporter_Service.Helpers.GeometricOperations;
using VC_Analytical_zones_exporter_Service.Models.Geometry;
using VC_Analytical_zones_exporter_Service.Models.SubSurface;

namespace VC_Analytical_zones_exporter_Service.Helpers.ZoneMappers
{
    public class CurtainWallWindowSubSurfaceMapper
    {
        public static DoorAndWindow MapCurtainWallWindowSubSurfaces(EnergyAnalysisSurface energyAnalysisSurface, 
                                                                    EnergyAnalysisSpace energyAnalysisSpace, 
                                                                    Document doc,
                                                                    string surfConstructionId, string analyticalZoneId)
        {
            string name = "CW_Window_" + energyAnalysisSurface.Id.ToString();
            string subSurfType = "Window";
            string constructionName = surfConstructionId + "_Window";
            string hostSurfaceName = energyAnalysisSurface.Id.ToString();
            string outsideBCObj;
            
            if (energyAnalysisSurface.GetAdjacentAnalyticalSpace() != null)
            {
                
                if (energyAnalysisSurface.GetAnalyticalSpace().Id == energyAnalysisSpace.Id)
                {
                    outsideBCObj = energyAnalysisSurface.Id.ToString() + "_" + energyAnalysisSurface.GetAdjacentAnalyticalSpace().Id.ToString();
                }
                
                else
                {
                    outsideBCObj = energyAnalysisSurface.Id.ToString() + "_" + energyAnalysisSurface.GetAnalyticalSpace().Id.ToString();
                }
            }
            
            else
            {
                outsideBCObj = "";
            }

            string viewFactorToGround = "";
            string frameAndDividerName = "CW_Window_" + energyAnalysisSurface.Id.ToString() + "_Frame";
            int multiplier = 1;
            FrameAndDivider frameAndDivider = CurtainWallFrameAndDividerMapper.MapCWFrameAndDivider(energyAnalysisSurface);
            List<Coordinate> vertices = SurfaceGeometryMapper.MapSurfaceGeometry(energyAnalysisSurface, doc, analyticalZoneId);
            Coordinate planeCenter = new Coordinate(vertices.Average(p => p.X),
                            vertices.Average(p => p.Y), vertices.Average(p => p.Z));
            XYZ faceNormal = new XYZ();
            Document surfDoc = energyAnalysisSurface.Document;
            Application app = surfDoc.Application;
            Options opt = app.Create.NewGeometryOptions();
            GeometryElement geo = energyAnalysisSurface.get_Geometry(opt);
            foreach (GeometryObject obj in geo)
            {
                Solid solid = obj as Solid;
                if (null == solid) continue;
                foreach (Face face in solid.Faces)
                {
                    PlanarFace planarFace = face as PlanarFace;
                    faceNormal = -planarFace.FaceNormal;
                }
            }
            List<Coordinate> newVertices = new List<Coordinate>();
            foreach (Coordinate vertex in vertices)
            {
                Coordinate newVertex = MovePointTowardsPointV2.PointMover(vertex, planeCenter, 0.1, faceNormal);

                newVertices.Add(newVertex);
            }
            List<Coordinate> sortedVertices = SortPointsV2.PointSorter(newVertices, faceNormal);

            DoorAndWindow subSurface = new DoorAndWindow(name, 
                                                         subSurfType, 
                                                         constructionName, 
                                                         hostSurfaceName, 
                                                         outsideBCObj, 
                                                         viewFactorToGround,
                                                         frameAndDividerName, 
                                                         multiplier, 
                                                         sortedVertices, 
                                                         frameAndDivider);

            return subSurface;
        }

    }
}
