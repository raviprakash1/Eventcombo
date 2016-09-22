using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Models
{
    public class ContactMessageViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Message { get; set; }
    }
}