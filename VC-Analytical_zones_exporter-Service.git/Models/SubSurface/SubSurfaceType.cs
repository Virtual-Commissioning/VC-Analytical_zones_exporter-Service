using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VC_Analytical_zones_exporter_Service.Models.SubSurface
{
    public class SubSurfaceType
    {
        public List<Dictionary<string, DoorAndWindow>> Doors { get; set; }
        public List<Dictionary<string, DoorAndWindow>> Windows { get; set; }
        public List<Dictionary<string, Opening>> Openings { get; set; }

        public SubSurfaceType(List<Dictionary<string, DoorAndWindow>> subSurfaceDoor, 
                           List<Dictionary<string, DoorAndWindow>> subSurfaceWindow, 
                           List<Dictionary<string, Opening>> subSurfaceOpening)
        {
            Doors = subSurfaceDoor;
            Windows = subSurfaceWindow;
            Openings = subSurfaceOpening;
        }
    }
}
