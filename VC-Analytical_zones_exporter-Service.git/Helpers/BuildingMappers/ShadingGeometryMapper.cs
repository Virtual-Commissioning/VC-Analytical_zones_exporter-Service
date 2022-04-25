using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VC_Analytical_zones_exporter_Service.Helpers;
using VC_Analytical_zones_exporter_Service.Helpers.GeometricOperations;
using VC_Analytical_zones_exporter_Service.Models.Geometry;

namespace VC_Analytical_zones_exporter_Service.Helpers.BuildingMappers
{
    public class ShadingGeometryMapper
    {
        public static List<Coordinate> MapShadingGeometry(Face face)
        {
            List<Coordinate> vertices = new List<Coordinate>();
            EdgeArray loop = face.EdgeLoops.get_Item(0);
            PlanarFace planarFace = face as PlanarFace;
            XYZ faceNormal = planarFace.FaceNormal;

            foreach (Autodesk.Revit.DB.Edge vertex in loop)
            {
                IList<XYZ> edgePts = vertex.Tessellate();
                double x = Math.Round(ImperialToMetricConverter.ConvertFromFeetToMeters(edgePts[0].X), 3);
                double y = Math.Round(ImperialToMetricConverter.ConvertFromFeetToMeters(edgePts[0].Y), 3);
                double z = Math.Round(ImperialToMetricConverter.ConvertFromFeetToMeters(edgePts[0].Z), 3);
                Coordinate point = new Coordinate(x, y, z);

                vertices.Add(point);
            }
            List<Coordinate> newVertices = new List<Coordinate>();
            foreach (Coordinate coord in vertices)
            {
                if (!newVertices.Where(x => x.X == coord.X && x.Y == coord.Y && x.Z == coord.Z).Any())
                {
                    newVertices.Add(coord);
                }
            }
            List<Coordinate> sortedVertices = SortPointsV2.PointSorter(newVertices, faceNormal);
            return sortedVertices;
        }
    }
}
