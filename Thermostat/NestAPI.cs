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
using System.IO;

namespace Thermostat
{
    
    /// <summary>
    /// Handles all interfacing with the Nest API.
    /// </summary>
    public static class NestAPI
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
                    Properties.Settings.Default.Save();
                }
                Console.WriteLine("postResult: " + postResult);
            }
        }

        private static NestRootModel apiRoot;
        private static DateTimeOffset lastRetrieved;

        public static NestRootModel GetNestData()
        {
            //update data if it has been too long or if this is first retrieval.
            if (apiRoot == null || (DateTime.UtcNow - lastRetrieved).TotalSeconds > 120)
            {
                try
                {
                    TokenModel tokenModel = JsonConvert.DeserializeObject<TokenModel>(Properties.Settings.Default.TokenJSON);
                    Console.WriteLine("ACCESS TOKEN: " + tokenModel.access_token);
                    //string json = RequestNestJson(tokenModel, ConfigurationManager.AppSettings["Nest-JSON-Url"], 0);
                    string json = HitNestAPI(tokenModel, ConfigurationManager.AppSettings["Nest-JSON-Url"], "GET", null, 0);
                    apiRoot = JsonConvert.DeserializeObject<NestRootModel>(json);
                    lastRetrieved = DateTimeOffset.UtcNow; //update timestamp
                    Console.WriteLine(json);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Getting Nest Data Failed with error: " + e.GetBaseException().ToString());
                    throw e;
                }
            }
            return apiRoot;
        }

        /*private static string RequestNestJson(TokenModel tokenModel, string url, int redirectCount)
        {
            string json = "";
            Console.WriteLine("url: " + url);
            Console.WriteLine("redirect count: " + redirectCount);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.AllowAutoRedirect = false;
            req.ContentType = "application/json";
            req.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenModel.access_token;
            using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
            {
                HttpStatusCode statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.TemporaryRedirect)
                {
                    json = RequestNestJson(tokenModel, response.Headers[HttpResponseHeader.Location], ++redirectCount);
                }
                else
                {
                    //string redirectEndUrl = 
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            json = reader.ReadToEnd();
                            stream.Close();
                        }
                    }
                }
            }
            return json;
        }*/

        private static string HitNestAPI(TokenModel tokenModel, string url, string verb, string data, int redirectCount) {
            string result = "";
            Console.WriteLine("url: " + url);
            Console.WriteLine("redirect count: " + redirectCount);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = verb;
            req.AllowAutoRedirect = false;
            req.ContentType = "application/json";
            req.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenModel.access_token;
            if (data != null)
            {
                byte[] dataBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(data);
                req.ContentLength = dataBytes.Length;
                using (Stream stream = req.GetRequestStream())
                {
                    stream.Write(dataBytes, 0, dataBytes.Length);
                    stream.Close();
                }
            }
            using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
            {
                HttpStatusCode statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.TemporaryRedirect)
                {
                    result = HitNestAPI(tokenModel, response.Headers[HttpResponseHeader.Location], verb, data, ++redirectCount);
                }
                else
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            result = reader.ReadToEnd();
                            stream.Close();
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Set the target temparature of a thermostat.
        /// </summary>
        /// <param name="thermostat">Thermostat to modify</param>
        /// <param name="targetTemperatureF">Target temperature in degrees F</param>
        public static void SetTargetTemperature(ThermostatModel thermostat, int targetTemperatureF)
        {
            if (!thermostat.temperature_scale.Equals("F", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Incompatible thermostat, mst be Farenheit");
            }
            else
            {
                TokenModel tokenModel = JsonConvert.DeserializeObject<TokenModel>(Properties.Settings.Default.TokenJSON);
                string url = string.Format("https://developer-api.nest.com/devices/thermostats/{0}", thermostat.device_id);
                string data = @"{""target_temperature_f"": [target_temp]}";
                data = data.Replace("[target_temp]", targetTemperatureF.ToString());
                string result = HitNestAPI(tokenModel, url, "PUT", data, 0);
            }
        }
    }
}
