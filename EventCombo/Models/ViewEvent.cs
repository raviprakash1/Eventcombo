using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Models
{
    public class ViewEvent
    {
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
    }
}