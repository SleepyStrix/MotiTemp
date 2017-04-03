using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Thermostat
{
    public abstract class Setting
    {
        public DateTimeOffset timestamp;
        //public abstract void Load();
    }
}
