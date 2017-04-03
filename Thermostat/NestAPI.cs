using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;
using System.Net;

namespace Thermostat
{
    /// <summary>
    /// Handles all interfacing with the Nest API.
    /// </summary>
    public class NestAPI
    {
        public static string GetPinUrl()
        {
            return ConfigurationManager.AppSettings["Nest-Pin-Url"];
        }

        public static void RequestToken(string pin)
        {
            using (WebClient webClient = new WebClient())
            {
                string url = ConfigurationManager.AppSettings["Nest-Token-Request-Endpoint"];
                url += "?client_id=" + ConfigurationManager.AppSettings["Nest-Product-Id"];
                url += "&client_secret=" + ConfigurationManager.AppSettings["Nest-Product-Secret"];
                url += "&code=" + pin;
                url += "&grant_type=authorization";
                webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            }
        }
    }
}
