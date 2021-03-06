﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public static bool CompareCurrentUser(long lEvntId,string strCurrentUser)
        {
            using (EventComboEntities objEnt = new EventComboEntities())
            {
                ValidationMessage vmc = new ValidationMessage();
                lEvntId = vmc.GetLatestEventId(lEvntId);
                var vEventUserId = (from myEvt in objEnt.Events where myEvt.EventID == lEvntId select myEvt.UserID).FirstOrDefault();
                if (vEventUserId == null) return false;
                if (strCurrentUser != vEventUserId.Trim())
                {
                    return false;
                }
                return true;
            }
        }
        public static bool UserOrganizerStatus(string strUserId)
        {
            using (EventComboEntities objEnt = new EventComboEntities())
            {
                if (HttpContext.Current.Session["AppId"] != null && strUserId == "")
                    strUserId = HttpContext.Current.Session["AppId"].ToString();

                var vUserOrgStatus = (from myUser in objEnt.Profiles where myUser.UserID == strUserId select myUser.Organiser).FirstOrDefault();
                if (vUserOrgStatus == null) return false;
                if (vUserOrgStatus == "Y")
                {
                    return true;
                }
                else { return false; }
            }
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
        public Nullable<int> TLD_PromoCodeId { get; set; }
        public Nullable<decimal> TLD_PromoCodeAmount { get; set; }
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
        public string EventDisplayAddress { get; set; }
        public double EventDistance { get; set; }

        public int EventFeature { get; set; }

        public DateTime FeatureDateTime { get; set; }

        public string EventPrivacy { get; set; }
        public int AddressStatus { get; set; }
        //public DiscoverEvent[] DiscoverEventList { get; set; }

    }
    public partial class Address
    {
        public string discoverdistance { get; set; }


    }

    public class EmailContent
    {
        public string To { get; set; }
        public string From { get; set; }
        [AllowHtml]
        public string Body { get; set; }

        public string Subject { get; set; }

        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Fromname { get; set; }
        

    }

    public partial class Event_Email_Invitation
    {
        public Event_Email_List[] EmailList { get; set; }
        public string EventTitle  {get;set;}

        public string EventDate { get; set; }
        public string EventOrgnizer { get; set; }
        public string EventAddress { get; set; }
        public string EventLat { get; set; }
        public string EventLong { get; set; }
        public string EventImg { get; set; }
        

    }
}