using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Models
{
    public class CommanClasses
    {
        public string GetMessage(string strFormName, string strFormTag)
        {
            string result = "";
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    result = (from myRow in objEnt.Messages
                              where myRow.Form_Name == strFormName && myRow.Form_Tag == strFormTag
                              select myRow.Message1).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                result = "There is some error.Please contact system administrator";
            }
            return result;
        }


        public static decimal ConvertToNumeric(string strNumeric)
        {
            decimal dResult = 0;
            try
            {
                dResult = Convert.ToDecimal(strNumeric);
            }
            catch (Exception)
            {
                dResult = 0;
            }
            return dResult;
        }

        public static long ConvertToLong(string strLong)
        {
            long dResult = 0;
            try
            {
                dResult = Convert.ToInt64(strLong);
            }
            catch (Exception)
            {
                dResult = 0;
            }
            return dResult;
        }
    }


    public partial class Ticket_Locked_Detail
    {
        public Ticket_Locked_Detail[] TLD_List { get; set; }
    }

    public class Ticket_Locked_Detail_List
    {
        public long TLD_Id { get; set; }
        public string TLD_User_Id { get; set; }
        public Nullable<long> TLD_Locked_Qty { get; set; }
        public Nullable<long> TLD_Event_Id { get; set; }
        public Nullable<long> TLD_TQD_Id { get; set; }
        public Nullable<System.DateTime> Locktime { get; set; }
        public string TLD_GUID { get; set; }
        public Nullable<decimal> TLD_Donate { get; set; }
        public Nullable<decimal> TicketAmount { get; set; }
    }


    public class SaleTickets
    {
        public long SaleQty { get; set; }
        public DateTime orderDate { get; set; }

    }
    public class DiscoverEvent
    {
        public string EventTitle { get; set; }
        public string EventAddress { get; set; }
        public string EventTimings { get; set; }
        public string EventCat { get; set; }
        public string EventType { get; set; }
        public string PriceLable { get; set; }
        public string EventImage { get; set; }
        public long EventTypeId { get; set; }
        public long EventCatId { get; set; }
        public long EventId { get; set; }
        public DateTime EventDate { get; set; }
        public string EventLike { get; set; }

        //public DiscoverEvent[] DiscoverEventList { get; set; }

    }
    public partial class Address
    {
        public string discoverdistance { get; set; }
    }
}