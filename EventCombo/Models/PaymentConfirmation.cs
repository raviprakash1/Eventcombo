﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Models
{
    public class PaymentConfirmation
    {
        public string imgurl { get; set; }
        public string Tilte { get; set; }
        public string Username { get; set; }
        public string sendlatestdetails { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public string Email { get; set; }
        public string Eventid { get; set; }
        public string Organiserid { get; set; }
        public string Guestlist { get; internal set; }
        public string Orderdetail { get; set; }
        public string EventPrivacy { get; internal set; }
        public string Shareonfb { get; internal set; }
    }

    public class paymentdate
    {
        public string Address { get; set; }
        public string Datetime { get; set; }
        public string id { get; set; }
    }
}