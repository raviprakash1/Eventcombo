using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Models
{
    public class EventConfirmation
    {
        public string Imageurl { get; set; }
        public string Startdate { get; set; }
        public string Time { get; set; }
        public string Title { get; set; }
        public string Venuename { get; set; }
        public string Address { get; set; }
        public string url { get; set; }
        public string Descritption { get; set; }

        public string EventId { get; set; }

        public string urlTitle { get; set; }
    }
}