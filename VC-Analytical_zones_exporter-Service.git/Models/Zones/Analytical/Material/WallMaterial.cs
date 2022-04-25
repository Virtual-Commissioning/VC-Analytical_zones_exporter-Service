using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VC_Analytical_zones_exporter_Service.Models.Zones.Analytical.Material
{
    public class WallMaterial
    {
        public List<WallMaterial> WallsInModel { get; set; } = new List<WallMaterial>();

        public void AddWall(WallMaterial wall)
        {
            if (!IsWallInList(wall))
            {
                WallsInModel.Add(wall);
            }
        }

        public bool IsWallInList(WallMaterial wall)
        {
            if (WallsInModel.Contains(wall))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
