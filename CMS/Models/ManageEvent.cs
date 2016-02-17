using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class ManageEvent
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Catogary { get; set; }
        public string subCatogary { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string VenueName { get; set; }
        public string Venueaddress{ get; set; }
        public string Venuecity { get; set; }
        public string VenueState { get; set; }
        public string VenueZip { get; set; }
        public string VenueCountry{ get; set; }
        public string NoLocation { get; set; }
        public string Organisername { get; set; }
        public string Organiseremail { get; set; }
        public string TicketsSold { get; set; }
        public string Feature { get; set; }

    }

    public class FeeSetting
    {
        public string percentage { get; set; }
        public string value { get; set; }
    }
}