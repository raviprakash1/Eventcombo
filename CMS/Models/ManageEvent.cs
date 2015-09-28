using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class ManageEvent
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Catogary { get; set; }
        public string subCatogary { get; set; }
        public string Date { get; set; }
        public string VenueName { get; set; }
        public string Venueaddress{ get; set; }
        public string Organisername { get; set; }
        public string Organiseremail { get; set; }
        public string TicketsSold { get; set; }
        public string Feature { get; set; }

    }
}