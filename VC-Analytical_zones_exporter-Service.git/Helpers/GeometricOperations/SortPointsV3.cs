﻿using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using VC_Analytical_zones_exporter_Service.Helpers.ZoneMappers;
using VC_Analytical_zones_exporter_Service.Models.Geometry;

namespace VC_Analytical_zones_exporter_Service.Helpers.GeometricOperations
{
    public class SortPointsV3 //Attempt on sorting vertices based on angles. Does not work completely, but think this is the optimal way to solve the sorting.
    {
        public static List<Coordinate> PointSorter(List<Coordinate> vertices, XYZ faceNormal)
        {
            Coordinate centerPoint = new Coordinate(vertices.Average(p => p.X), vertices.Average(p => p.Y), vertices.Average(p => p.Z));
            List<Coordinate> sortedCoordinates = new List<Coordinate>();
            List<Coordinate> q1_1 = new List<Coordinate>(); // Dependent on the location of the point in the top left corner
            List<Coordinate> q1_2 = new List<Coordinate>(); // Dependent on the location of the point in the top left corner
            List<Coordinate> q2 = new List<Coordinate>();
            List<Coordinate> q3 = new List<Coordinate>();
            List<Coordinate> q4 = new List<Coordinate>();
            int normalX = (int)Math.Round(faceNormal.X);
            int normalY = (int)Math.Round(faceNormal.Y);
            int normalZ = (int)Math.Round(faceNormal.Z);
            
            if (normalX == 1 || normalX == -1)
            {
                vertices = vertices.OrderBy(p => p.Y).ThenByDescending(p => p.Z).ToList();
                sortedCoordinates.Add(vertices[0]);
                vertices.Remove(vertices[0]);
                vertices = vertices.OrderBy(p => GetAngle.AngleFinder(sortedCoordinates[0], p, centerPoint, faceNormal)).ToList();
                sortedCoordinates.AddRange(vertices);
                
                if (normalX == -1)
                {
                    sortedCoordinates.Reverse();
                }
            }
            
            else if (normalY == 1 || normalY == -1)
            {
                vertices = vertices.OrderBy(p => p.X).ThenByDescending(p => p.Z).ToList();
                sortedCoordinates.Add(vertices[0]);
                vertices.Remove(vertices[0]);
                vertices = vertices.OrderBy(p => GetAngle.AngleFinder(sortedCoordinates[0], p, centerPoint, faceNormal)).ToList();
                sortedCoordinates.AddRange(vertices);
                
                if (normalY == -1)
                {
                    sortedCoordinates.Reverse();
                }
            }
            
            else
            {
                vertices = vertices.OrderBy(p => p.X).ThenByDescending(p => p.Y).ToList();
                sortedCoordinates.Add(vertices[0]);
                vertices.Remove(vertices[0]);
                vertices = vertices.OrderBy(p => GetAngle.AngleFinder(sortedCoordinates[0], p, centerPoint, faceNormal)).ToList();
                sortedCoordinates.AddRange(vertices);
                
                if (normalZ == -1)
                {
                    sortedCoordinates.Reverse();
                }
            }

            return sortedCoordinates;
        }
    }
}
