using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thermostat.NestModels
{
    public class NestRootModel
    {
        public DevicesModel devices;
    }

    public class DevicesModel
    {
        public Dictionary<string, ThermostatModel> thermostats;
        public Dictionary<string, CameraModel> cameras;
    }

    public class ThermostatModel {
        public string name;
        public string name_long;
        public string where_id;
        public string device_id;
        public string structure_id;
        public bool is_using_emergency_heat;
        public string temperature_scale;
    }

    public class CameraModel
    {
        public string name;
        public string name_long;
        public string where_id;
        public string device_id;
        public string structure_id;
    }
}
