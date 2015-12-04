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
        public long EventID { get; set; }
        public string EventTitle { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public Int64 TicketSold { get; set; }
        public Int64 TotalTicket { get; set; }
        public int EventCount { get; set; }
    }
}