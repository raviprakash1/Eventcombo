using System;
using System.Collections.Generic;
using System.Linq;
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
         
    }
}