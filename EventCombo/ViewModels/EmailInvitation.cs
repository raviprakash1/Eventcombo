﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace EventCombo.ViewModels
{
    public class EmailInvitation 
    {
        
        public Nullable<long> EventID { get; set; }
        public Nullable<long> NoOfRecipients { get; set; }
        public string Subject { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public Nullable<DateTime> SendOn { get; set; }

        public long I_Id   { get; set; }

    }

    public class ticketbox
    {

        public string ticketinfo { get; set; }
    }
      
}