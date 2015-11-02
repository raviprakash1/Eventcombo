using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Models
{
    public class TicketPayment
    {
        public string Imageurl{get;set;}
        public string Title { get; set; }
        public string Tickettype { get; set; }
        public string Email { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string PhnNo { get; set; }
        public string Address { get; set; }
        public long EventId { get; set; }
    }


    public class GetEventDateList
    {
        public string Dayofweek { get; set; }
        public string Datefrom { get; set; }
        public string type { get; set; }
        public string Time { get; set; }
        

    }
}