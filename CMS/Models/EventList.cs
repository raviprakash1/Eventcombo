﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class EventList
    {
        public Nullable<long> Sno { get; set; }
        public string EventTitle { get; set; }
        public string UserID { get; set; }
        public long EventID { get; set; }
        public long EventCategoryID { get; set; }
        public string EventCategory { get; set; }
        public long EventTypeID { get; set; }
        public string EventType { get; set; }
        public Nullable<long> EventSubCategoryID { get; set; }
        public string EventSubCategory { get; set; }
        public string StartingFrom { get; set; }
        public string EventStartTime { get; set; }
        public string EventEndDate { get; set; }
        public string EventEndTime { get; set; }
        public Nullable<System.DateTime> E_Enddate { get; set; }
        public Nullable<System.DateTime> E_Startdate { get; set; }
        public string Orgnizer_Name { get; set; }
        public string Email { get; set; }
        public int Feature { get; set; }
        public string EventAddress { get; set; }
        public string VenueName { get; set; }
        public string TicketDetail { get; set; }
        public string Purchasedqty { get; set; }
        public string EventTiming { get; set; }
    }
}