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
        public Dictionary<string, StructureModel> structures;
    }

    /// <summary>
    /// Parent of Nest devices. Does not represent a standalone JSON object.
    /// </summary>
    public class DeviceModel
    {
        public string name;
        public string name_long;
        public string device_id;
        public string structure_id;
    }

    public class DevicesModel
    {
        public Dictionary<string, ThermostatModel> thermostats;
        public Dictionary<string, CameraModel> cameras;
    }

    public class ThermostatModel : DeviceModel 
    {
        public bool is_using_emergency_heat;
        public string temperature_scale;
        public string hvac_mode;
        public string previous_hvac_mod;
    }

    public class CameraModel : DeviceModel
    {
        public CameraEventModel last_event;
    }

    public class CameraEventModel
    {
        public bool has_motion;
        public string end_time;
    }

    public class StructureModel
    {
        public string name;
        public string away;
        public List<String> thermostats;
        public List<String> cameras;
        public string structure_id;
    }

    
}
