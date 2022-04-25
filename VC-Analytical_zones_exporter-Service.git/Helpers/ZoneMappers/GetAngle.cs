using Autodesk.Revit.DB;
using System;
using VC_Analytical_zones_exporter_Service.Models.Geometry;

namespace VC_Analytical_zones_exporter_Service.Helpers.ZoneMappers
{
    public class GetAngle //Finds angle between three points
    {
        public static double AngleFinder(Coordinate p1, Coordinate p2, Coordinate centerPoint, XYZ faceNormal)
        {
            Coordinate a = new Coordinate(centerPoint.X - p1.X, centerPoint.Y - p1.Y, centerPoint.Z - p1.Z);
            Coordinate b = new Coordinate(centerPoint.X - p2.X, centerPoint.Y - p2.Y, centerPoint.Z - p2.Z);

            double dot, det;
            
            if (faceNormal.X == 1 || faceNormal.X == -1) // (Y, Z)
            {
                dot = a.Y * a.Z + b.Y * b.Z;
                det = a.Y * b.Z - b.Y * a.Z;
            }
            
            else if (faceNormal.Y == 1 || faceNormal.Y == -1) // (X, Z)
            {
                dot = a.X * a.Z + b.X * b.Z;
                det = a.X * b.Z - b.X * a.Z;
            }
            
            else // (X, Y)
            {
                dot = a.X * a.Y + b.X * b.Y;
                det = a.X * b.Y - b.X * a.Y;
            }

            double angle = Math.Atan2(det, dot) * 180 / Math.PI;
            return angle; //Returns an angle between 0 and 360
        }
    }
}
