using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VC_Analytical_zones_exporter_Service.Models.Construction
{
    public class OpeningConstruction
    {
        public string Name { get; set; }
        public string Air_Exchange_Method { get; set; }
        public string Simple_Mixing_Air_Changes_per_Hour { get; set; }
        public string Simple_Mixing_Schedule_Name { get; set; }

        public OpeningConstruction(string name, string airExchangeMethod, string simpleMixingAirChangesPerHour, string simpleMixingScheduleName)
        {
            Name = name;
            Air_Exchange_Method = airExchangeMethod;
            Simple_Mixing_Air_Changes_per_Hour = simpleMixingAirChangesPerHour;
            Simple_Mixing_Schedule_Name = simpleMixingScheduleName;
        }
    }
}
