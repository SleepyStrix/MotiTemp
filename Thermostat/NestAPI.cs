using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using Thermostat.NestModels;
using Newtonsoft.Json;

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
                string url = ConfigurationManager.AppSettings["Nest-Token-Request-FullUrl"];
                url = url.Replace("{product_id}", ConfigurationManager.AppSettings["Nest-Product-Id"]);
                url = url.Replace("{product_secret}", ConfigurationManager.AppSettings["Nest-Product-Secret"]);
                url = url.Replace("{pin}", pin);
                webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                string postResult = webClient.UploadString(url, "");
                TokenModel tokenModel = JsonConvert.DeserializeObject<TokenModel>(postResult);
                if (string.IsNullOrWhiteSpace(tokenModel.access_token))
                {
                    throw new Exception("Token retireval failed");
                }
                else
                {
                    //save token json
                    Properties.Settings.Default.TokenJSON = postResult;
                }
                Console.WriteLine("postResult: " + postResult);
            }
        }
    }
}
