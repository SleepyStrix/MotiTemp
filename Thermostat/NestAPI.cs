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

        public static void GetNestData()
        {
            try
            {
                TokenModel tokenModel = JsonConvert.DeserializeObject<TokenModel>(Properties.Settings.Default.TokenJSON);
                Console.WriteLine("ACCESS TOKEN: " + tokenModel.access_token);
                string json = RequestNestJson(tokenModel, ConfigurationManager.AppSettings["Nest-JSON-Url"], 0);
                Console.WriteLine(json);
            }
            catch (Exception e)
            {
                Console.WriteLine("Getting Nest Data Failed with error: " + e.GetBaseException().ToString());
                throw e;
            }
        }

        private static string RequestNestJson(TokenModel tokenModel, string url, int redirectCount)
        {
            string json = "";
            Console.WriteLine("url: " + url);
            Console.WriteLine("redirect count: " + redirectCount);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.AllowAutoRedirect = false;
            req.ContentType = "application.json";
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
        }
    }
}
