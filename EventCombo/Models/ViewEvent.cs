using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace EventCombo.Models
{
     public class ViewEvent
    {
        private const string ServiceUri = "http://maps.googleapis.com/maps/api/geocode/xml?address={0}&region=be&sensor=false";

        public List<EventImage> Images { get; set; }
        public string Title { get; set; }
        public string Favourite { get; set; }
        public string Vote { get; set; }
        public string eventId { get; set; }
        public string hdEventFav { get; set; }
        public string hdEventvote { get; set; }
        public string TopAddress { get; set; }
        public string TopVenue { get; set; }
        public string eventType { get; set; }
        public string DisplaydateRange { get; set; }
        public string EventDescription { get; set; }
        public string organizername { get; set; }
        public string fblink { get; set; }
        public string twitterlink { get; set; }
        public string organizerid { get; set; }
        public string Orderdetail { get; set; }
        public string Timezone { get; set; }
        public string showTimezone { get; set; }
        public string showstarttime { get; set; }
        public string showendtime { get; set; }
        public string Shareonfb { get; set; }
        public string typeofEvent { get; set; }
        public string enablediscussion { get; set; }
        public string showmaponevent { get; set; }
        public string Linkedinlin { get;  set; }
        public string EventPrivacy { get; set; }
        public string EventCancel { get; set; }
        public string Orgevents { get; internal set; }

        public Coordinates Geocode(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException("address");

            string requestUriString = string.Format(ServiceUri, Uri.EscapeDataString(address));

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestUriString);

            try
            {
                WebResponse response = request.GetResponse();

                XDocument xdoc = XDocument.Load(response.GetResponseStream());

                // Verify the GeocodeResponse status
                string status = xdoc.Element("GeocodeResponse").Element("status").Value;
                ValidateGeocodeResponseStatus(status, address);

                XElement locationElement = xdoc.Element("GeocodeResponse").Element("result").Element("geometry").Element("location");
                double latitude = (double)locationElement.Element("lat");
                double longitude = (double)locationElement.Element("lng");

                return new Coordinates(latitude, longitude);
            }
            catch (WebException ex)
            {
                switch (ex.Status)
                {
                    case WebExceptionStatus.NameResolutionFailure:
                        throw new Exception("The Google Maps geocoding service appears to be offline.", ex);
                    default:
                        throw;
                }
            }


        }

        private void ValidateGeocodeResponseStatus(string status, string address)
        {
            switch (status)
            {
                case "ZERO_RESULTS":
                    string message = string.Format("No coordinates found for address \"{0}\".", address);
                    throw new Exception(message);
                case "OVER_QUERY_LIMIT":
                    throw new Exception("Over Query Limit");
                case "OK":
                    break;
                default:
                    throw new Exception("Unkown status code: " + status + ".");
            }
        }
        
        
    }

    public partial class Promo_Code
    {
        public string Eventitle{ get; set; }
        public List<Ticket> Ticketdata { get; set; }
    }
    public class Coordinates
        {
            public Coordinates(double latitude, double longitude)
            {
                Latitude = latitude;
                Longitude = longitude;
            }

            public double Latitude { get; private set; }

            public double Longitude { get; private set; }
        }
}