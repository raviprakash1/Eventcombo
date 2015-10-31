﻿using System;
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
    }
}