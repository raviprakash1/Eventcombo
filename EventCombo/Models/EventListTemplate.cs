using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using EventCombo.Models;

namespace EventCombo.Models
{[Table("Event")]
    public class EventListTemplate
    {
        public string EventTitle { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public string TicketSold { get; set; }
        public string TotalTicket { get; set; }
        public int EventCount { get; set; }
    }
}