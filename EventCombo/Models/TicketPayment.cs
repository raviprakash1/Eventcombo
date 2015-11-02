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
        public string AccFname { get; set; }
        public string  AccLname { get; set; }
        public string  AccEmail { get; set; }
        public string AccconfirmEmail { get; set; }
        public string Accpassword { get; set; }
        public string  Accconfirmpassword { get; set; }
        public string Accountphnno{ get; set; }
        public string AccCity { get; set; }
        public string  AccState { get; set; }
        public string Acczip { get; set; }
        public string Acccountry { get; set; }
        public string cardno { get; set; }
        public string expirydate{ get; set; }
    public string cvv { get; set; }
    public string billfname { get; set; }
    public string billLname { get; set; }
        public string billingphno { get; set; }
        public string billaddress1 { get; set; }
        public string billaddress2 { get; set; }
        public string billcity { get; set; }
        public string billzip { get; set; }
        public string billstate { get; set; }
        public string billcountry { get; set; }
        public string shipfname { get; set; }
        public string shipLname { get; set; }
        public string shipphno { get; set; }
        public string shipaddress1 { get; set; }
        public string shipcity { get; set; }
        public string shipzip { get; set; }
        public string shipstate { get; set; }
        public string shipcountry { get; set; }
       public List<TicketBearer> NameList { get; set; }
    }


    public class GetEventDateList
    {
        public string Dayofweek { get; set; }
        public string Datefrom { get; set; }
        public string type { get; set; }
        public string Time { get; set; }
        

    }
}