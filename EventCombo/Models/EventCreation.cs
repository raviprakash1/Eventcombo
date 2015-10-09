﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EventCombo.Models
{
    [Table("Event")]
    public class EventCreation
    {
        public long EventID { get; set; }
        public int EventTypeID { get; set; }
        public int EventCategoryID { get; set; }
        public int EventSubCategoryID { get; set; }
        public string UserID { get; set; }
        public string EventTitle { get; set; }
        public string DisplayStartTime { get; set; }
        public string DisplayEndTime { get; set; }
        public string DisplayTimeZone { get; set; }
        public string EventDescription { get; set; }
        public string EventPrivacy { get; set; }
        public string Private_ShareOnFB { get; set; }
        public string Private_GuestOnly { get; set; }
        public string Private_Password { get; set; }
        public string EventUrl { get; set; }
        public string PublishOnFB { get; set; }
        public string EventStatus { get; set; }
        public Address[] AddressDetail { get; set; }
    }
}