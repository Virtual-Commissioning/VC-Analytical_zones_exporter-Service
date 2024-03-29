﻿using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VC_Analytical_zones_exporter_Service.Models.Geometry;

namespace VC_Analytical_zones_exporter_Service.Helpers.GeometricOperations
{
    public class SortPoints //Finds angle between three points
    {
        public static List<Coordinate> PointSorter(List<Coordinate> vertices, XYZ faceNormal)
        {
            List<Coordinate> sortedCoordinates = new List<Coordinate>();
            List<Coordinate> sorted = new List<Coordinate>();
            int normalX = (int)Math.Round(faceNormal.X);
            int normalY = (int)Math.Round(faceNormal.Y);
            int normalZ = (int)Math.Round(faceNormal.Z);
            
            if (normalX == 1)
            {
                sorted = vertices.OrderByDescending(p => p.Z).ThenBy(p => p.Y).ToList();
                sortedCoordinates.Add(sorted[0]);

                sorted = vertices.OrderBy(p => p.Z).ThenBy(p => p.Y).ToList();
                sortedCoordinates.Add(sorted[0]);

                sorted = vertices.OrderBy(p => p.Z).ThenByDescending(p => p.Y).ToList();
                sortedCoordinates.Add(sorted[0]);

                sorted = vertices.OrderByDescending(p => p.Z).ThenByDescending(p => p.Y).ToList();
                sortedCoordinates.Add(sorted[0]);

            }
            
            else if (normalX == -1)
            {
                sorted = vertices.OrderByDescending(p => p.Z).ThenByDescending(p => p.Y).ToList();
                sortedCoordinates.Add(sorted[0]);

                sorted = vertices.OrderBy(p => p.Z).ThenByDescending(p => p.Y).ToList();
                sortedCoordinates.Add(sorted[0]);

                sorted = vertices.OrderBy(p => p.Z).ThenBy(p => p.Y).ToList();
                sortedCoordinates.Add(sorted[0]);

                sorted = vertices.OrderByDescending(p => p.Z).ThenBy(p => p.Y).ToList();
                sortedCoordinates.Add(sorted[0]);
            }
            
            else if (normalY == 1)
            {
                sorted = vertices.OrderByDescending(p => p.Z).ThenBy(p => p.X).ToList();
                sortedCoordinates.Add(sorted[0]);

                sorted = vertices.OrderBy(p => p.Z).ThenBy(p => p.X).ToList();
                sortedCoordinates.Add(sorted[0]);

                sorted = vertices.OrderBy(p => p.Z).ThenByDescending(p => p.X).ToList();
                sortedCoordinates.Add(sorted[0]);

                sorted = vertices.OrderByDescending(p => p.Z).ThenByDescending(p => p.X).ToList();
                sortedCoordinates.Add(sorted[0]);
            }
            
            else if (normalY == -1)
            {
                sorted = vertices.OrderByDescending(p => p.Z).ThenByDescending(p => p.X).ToList();
                sortedCoordinates.Add(sorted[0]);

                sorted = vertices.OrderBy(p => p.Z).ThenByDescending(p => p.X).ToList();
                sortedCoordinates.Add(sorted[0]);

                sorted = vertices.OrderBy(p => p.Z).ThenBy(p => p.X).ToList();
                sortedCoordinates.Add(sorted[0]);

                sorted = vertices.OrderByDescending(p => p.Z).ThenBy(p => p.X).ToList();
                sortedCoordinates.Add(sorted[0]);
            }
            
            else if (normalZ == 1)
            {
                sorted = vertices.OrderByDescending(p => p.Y).ThenBy(p => p.X).ToList();
                sortedCoordinates.Add(sorted[0]);

                sorted = vertices.OrderBy(p => p.Y).ThenBy(p => p.X).ToList();
                sortedCoordinates.Add(sorted[0]);

                sorted = vertices.OrderBy(p => p.Y).ThenByDescending(p => p.X).ToList();
                sortedCoordinates.Add(sorted[0]);

                sorted = vertices.OrderByDescending(p => p.Y).ThenByDescending(p => p.X).ToList();
                sortedCoordinates.Add(sorted[0]);
            }
            
            else
            {
                sorted = vertices.OrderByDescending(p => p.Y).ThenBy(p => p.X).ToList();
                sortedCoordinates.Add(sorted[0]);

                sorted = vertices.OrderBy(p => p.Y).ThenBy(p => p.X).ToList();
                sortedCoordinates.Add(sorted[0]);

                sorted = vertices.OrderBy(p => p.Y).ThenByDescending(p => p.X).ToList();
                sortedCoordinates.Add(sorted[0]);

                sorted = vertices.OrderByDescending(p => p.Y).ThenByDescending(p => p.X).ToList();
                sortedCoordinates.Add(sorted[0]);
            }

            return sortedCoordinates;
        }
    }
}
