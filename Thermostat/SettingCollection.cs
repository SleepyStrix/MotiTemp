using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thermostat.NestModels;

namespace Thermostat
{
    public static class SettingCollection
    {
        public static List<StructureSetting> structureSettings = new List<StructureSetting>();

        public static void LoadSettings()
        {
            //SaveSettings();
            NestRootModel nestRoot = NestAPI.GetNestData();
            string json = Properties.Settings.Default.StructureSettings;
            Console.WriteLine(json);
            structureSettings = JsonConvert.DeserializeObject<List<StructureSetting>>(json);
            //make list if none is found
            if (structureSettings == null)
            {
                structureSettings = new List<StructureSetting>();
            }
            //add newly found strucures to settings
            foreach (StructureModel structure in nestRoot.structures.Values)
            {
                StructureSetting s;
                s = GetStructureSetting(structure.structure_id);
                if (s == null)
                {
                    structureSettings.Add(new StructureSetting(structure.structure_id));
                }
            }
            SaveSettings();
        }


        /// <summary>
        /// Get StructureSetting by structure_id
        /// </summary>
        /// <param name="structure_id"></param>
        /// <returns></returns>
        public static StructureSetting GetStructureSetting(string structure_id)
        {
            if (structureSettings != null)
            {
                foreach (StructureSetting s in structureSettings)
                {
                    if (s.structure_id.Equals(structure_id, StringComparison.OrdinalIgnoreCase))
                    {
                        return s;
                    }
                }
            }
            return null;
        }

        public static void SaveSettings()
        {
            Program.localConsole.WriteLine("Saving Settings");
            if (structureSettings != null)
            {
                string json = JsonConvert.SerializeObject(structureSettings);
                Program.localConsole.WriteLine(json);
                Properties.Settings.Default.StructureSettings = json;
                Properties.Settings.Default.Save();
            }
        }
    }
}
