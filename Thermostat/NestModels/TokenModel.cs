using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Thermostat.NestModels
{
    public class TokenModel : ISaveable
    {
        public string access_token;
        public int expires_in;

        public DateTimeOffset timestamp;

        public void Save() 
        {
            timestamp = DateTimeOffset.UtcNow;
            string json = JsonConvert.SerializeObject(this);
            Properties.Settings.Default.TokenJSON = json;
            Properties.Settings.Default.Save();
        }
    }
}
