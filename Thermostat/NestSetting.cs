using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thermostat
{
    public class NestSetting : Setting, ISaveable
    {
        public bool Save()
        {
            string json = JsonConvert.SerializeObject(this);
            Console.WriteLine("Saving: " + json);
            return false;
        }
        string oauthToken;
    }
}
