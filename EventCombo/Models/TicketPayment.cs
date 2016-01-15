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
        public string shipaddress2 { get; set; }
        public string shipaddress1 { get; set; }
        public string shipcity { get; set; }
        public string shipzip { get; set; }
        public string shipstate { get; set; }
        public string shipcountry { get; set; }
        public string Savecarddetail { get; set; }
        public string Saveshipdetail { get; set; }
        public string sameshipbilldetail { get; set; }
        public string Ticketdeliveraddress { get; set; }
        public List<Ticket_Purchased_Detail> TPurchaseList { get; set; }
        public List<TicketBearer> NameList { get; set; }
        public string Ticketname { get; set; }
        public string URLTitle { get; set; }
        public string card_type { get; set; }
    }


    public class GetEventDateList
    {
        public string Dayofweek { get; set; }
        public string Datefrom { get; set; }
        public string type { get; set; }
        public string Time { get; set; }
        

    }

    public class Cardview
    {
        public string value { get; set; }
        public string text { get; set; }
    }


    public class Pdfgeneration
    {
        public string EventOrderNO { get; set; }
        public string EventLogo { get; set; }
        public string EventBarcodeId { get; set; }
        public string EventQrCode { get; set; }
        public string UserFirstNameID { get; set; }
        public string UserLastNameID { get; set; }
        public string EventImage { get; set; }
        public string EventTitleId { get; set; }
        public string EventStartDateID { get; set; }
        public string EventdayId { get; set; }
        public string EventStartTimeID { get; set; }
        public string EventVenueID { get; set; }
        public string Eventtype { get; set; }
        public string EventOrganiserName { get; set; }
        public string EventOrganiserEmail { get; set; }
        public string EventDescription { get; set; }
        public string Tickettype { get; set; }
        public string TicketOrderDateId { get; set; }
        public string TicketPrice { get; set; }
       public string Purchased_Qty { get; set; }



    }

}