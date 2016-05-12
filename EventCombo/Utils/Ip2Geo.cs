using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;

namespace EventCombo.Utils
{
    public class Ip2Geo
    {
        private string apiUrl = ConfigurationManager.AppSettings["IPInfoDBAPIUrl"];
        private string apiKey = ConfigurationManager.AppSettings["IPInfoDBAPIKey"];
        public GeoAddress GetAddress(string ipAddress)
        {
            GeoAddress gAddress = new GeoAddress(); 
            using (WebClient client = new WebClient())
            {
                var json = client.DownloadString(apiUrl + "?key=" + apiKey + "&ip=" + ipAddress + "&format=json");
                gAddress = JsonConvert.DeserializeObject<GeoAddress>(json);
                
            }
            return gAddress;
        }
    }

    public class GeoAddress
    {
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
        public string ipAddress { get; set; }
        public string countryCode { get; set; }
        public string countryName { get; set; }
        public string regionName { get; set; }
        public string cityName { get; set; }
        public string zipCode { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string timeZone { get; set; }
    }

}

