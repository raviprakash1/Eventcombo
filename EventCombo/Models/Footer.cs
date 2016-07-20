using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Models
{
    public class Footer
    {
        public List<BusinessPage> BusinessPages { get; set; }
        public List<EventType> EventTypes { get; set; }
        public List<City> Cities { get; set; }
    }
}