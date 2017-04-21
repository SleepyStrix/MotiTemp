using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thermostat.NestModels;


namespace Thermostat
{
    public static class NestMain
    {
        public static void TurnEcoModeOff(ThermostatModel thermostat)
        {
            NestAPI.SetThermostatHvacMode(thermostat, thermostat.previous_hvac_mod);
        }

        public static void TurnEcoModeOn(ThermostatModel thermostat)
        {
            NestAPI.SetThermostatHvacMode(thermostat, "eco");
        } 

        public static DateTimeOffset? GetTimeOfLastMotion(string structure_id)
        {
            NestRootModel nestData = NestAPI.GetNestData();
            List<CameraModel> cameras = nestData.devices.cameras.Values.ToList();
            DateTimeOffset? lastTime = null;
            foreach (CameraModel cam in cameras)
            {
                if (cam.structure_id != null && 
                    cam.structure_id.Equals(structure_id, StringComparison.OrdinalIgnoreCase) &&
                    cam.last_event != null && cam.last_event.has_motion)
                {
                    DateTimeOffset end_time = DateTimeOffset.Parse(cam.last_event.end_time);
                    if (lastTime == null)
                    {
                        lastTime = end_time;
                    }
                    else if (end_time > lastTime)
                    {
                        lastTime = end_time;
                    }
                }
            }
            Console.WriteLine(lastTime.ToString());
            return lastTime;
        }

        /// <summary>
        /// Main functionality
        /// </summary>
        public static void UpdateThemostatModes()
        {
            Program.utcConsole.WriteLine("Updating Thermostats");
            NestRootModel data = NestAPI.GetNestData();
            //for all structures
            foreach (StructureModel structure in data.structures.Values)
            {
                Program.utcConsole.WriteLine("Trying structure:" + structure.structure_id);
                //get settings for this structure
                StructureSetting structureSetting = SettingCollection.GetStructureSetting(structure.structure_id);
                //if MotiTemp is on for this home and user is home
                if (structureSetting.on && structure.away.Equals("home", StringComparison.OrdinalIgnoreCase))
                {
                    DateTimeOffset? lastMotion = GetTimeOfLastMotion(structure.structure_id);
                    if (lastMotion != null)
                    {
                        double timeSinceMotion = (DateTimeOffset.UtcNow - (DateTimeOffset)lastMotion).TotalMinutes;
                        int minTime = structureSetting.minutesUntilInactive;
                        //if it has been at least 1 minute since last motion and less that 135 min
                        if (timeSinceMotion >= minTime && timeSinceMotion < 135)
                        {
                            Program.utcConsole.WriteLine("Setting eco mode on");
                            //set all thermostats in this structure to eco mode
                            List<ThermostatModel> thermostats = GetThermostats(structure.structure_id);
                            foreach (ThermostatModel thermostat in thermostats)
                            {
                                TurnEcoModeOn(thermostat);
                            }
                        }
                    }
                }
            }
        }

        public static List<CameraModel> GetCameras(string structure_id)
        {
            List<CameraModel> result = new List<CameraModel>();
            NestRootModel data = NestAPI.GetNestData();
            foreach (CameraModel cam in data.devices.cameras.Values.ToList())
            {
                if (cam.structure_id != null && cam.structure_id.Equals(structure_id, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(cam);
                }
            }
            return result;
        }

        public static List<ThermostatModel> GetThermostats(string structure_id)
        {
            List<ThermostatModel> result = new List<ThermostatModel>();
            NestRootModel data = NestAPI.GetNestData();
            foreach (ThermostatModel thermostat in data.devices.thermostats.Values.ToList())
            {
                if (thermostat.structure_id != null && thermostat.structure_id.Equals(structure_id, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(thermostat);
                }
            }
            return result;
        }
    }
}
