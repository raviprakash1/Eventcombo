using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Models
{
    public class GiveAwayViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string OrgName { get; set; }
        public string EventType { get; set; }
        public string EventFrequency { get; set; }
        public string EventLocation { get; set; }
        public string WhereHear { get; set; }
    }
}