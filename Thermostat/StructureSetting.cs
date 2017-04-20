using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thermostat
{
    public class StructureSetting
    {
        public bool on; //if management by MotiTemp is turned on for this structure
        public int minutesUntilInactive = 5;
        public string structure_id;

        public StructureSetting(string structure_id)
        {
            this.structure_id = structure_id;
            this.on = false;
        }
    }
}
