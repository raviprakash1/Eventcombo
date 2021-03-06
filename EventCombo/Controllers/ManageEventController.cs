﻿using EventCombo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.Entity.SqlServer;
using PagedList;
using EventCombo.ViewModels;
using System.Net.Mail;
using System.Configuration;
using EventCombo.Utils;
using System.Web.UI;
using NLog;
using EventCombo.DAL;
using EventCombo.Service;


namespace EventCombo.Controllers
{

    [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
    public class ManageEventController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
      
        // GET: ManageEvent
        EventComboEntities db = new EventComboEntities();

        IECImageService _iservice;
        IEventService _eservice;
        ITicketsService _tservice;

        public ManageEventController()
        {
          var mapper = AutomapperConfig.Config.CreateMapper();
          var factory = new EntityFrameworkUnitOfWorkFactory(new EventComboContextFactory());
          _iservice = new ECImageService(factory, mapper, new ECImageStorage(mapper));
          _eservice = new EventService(factory, mapper);
          _tservice = new TicketService(factory, mapper, new DBAccessService(factory, mapper), this);
        }

        private string GetNewInvitationCode()
        {
          var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
          return System.Convert.ToBase64String(plainTextBytes);
        }

        [Authorize]
        public ActionResult Index(long Eventlid = 0, string type = "")
        {
            if (Session["AppId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string User = Session["AppId"].ToString();
            using (EventComboEntities db = new EventComboEntities())
            {
                AspNetUser aspuser = db.AspNetUsers.First(i => i.Id == User);

                aspuser.LastLoginTime = System.DateTime.UtcNow;
                db.SaveChanges();
            }
            if (Eventlid == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            ValidationMessage vmc = new ValidationMessage();
            var Eventid = vmc.GetLatestEventId(Eventlid);
            if (CommanClasses.CompareCurrentUser(Eventid, Session["AppId"].ToString().Trim()) == false) return RedirectToAction("Index", "Home");

            var TopAddress = ""; var Topvenue = "";
            string sDate_new = "", eDate_new = "";
            string startday = "", endday = "", starttime = "", endtime = "";
            ManageEvent Mevent = new ManageEvent();
            //Getting event details
            EventCreation createevent = new EventCreation();

            var Edetails = createevent.GetEventdetail(Eventid);
            //Getting event details
            //Get Address detail
            var Discountcode = (from x in db.Promo_Code where x.PC_Eventid == Eventid select x).Count();
            var Addresstype = Edetails.AddressStatus;


            if (Addresstype == "PastLocation")
            {
                var evAdress = (from ev in db.Addresses where ev.AddressID == Edetails.LastLocationAddress select ev).FirstOrDefault();
                if (evAdress != null)
                {
                    TopAddress = evAdress.ConsolidateAddress;
                    Topvenue = evAdress.VenueName;
                }
            }
            else
            {
                var evAdress = (from ev in db.Addresses where ev.EventId == Eventid select ev).FirstOrDefault();
                if (evAdress != null)
                {
                    TopAddress = evAdress.ConsolidateAddress;
                    Topvenue = evAdress.VenueName;

                }
            }
            //Get Address detail
            //Get Event Date
            var Timezonedetail = (from ev in db.TimeZoneDetails where ev.TimeZone_Id.ToString() == Edetails.TimeZone select ev).FirstOrDefault();
            DateTimeWithZone dtzstart, dzend, dtznewstart, dtzCreated;
            DateTime ENDATE = new DateTime();
            var chkdate = (from ev in db.MultipleEvents where ev.EventID == Eventid select ev).Any();
            if (chkdate)
            {
                var evschdetails = (from ev in db.MultipleEvents where ev.EventID == Eventid select ev).FirstOrDefault();

                if (Timezonedetail != null)
                {
                    TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Timezonedetail.TimeZone);
                    dtzstart = new DateTimeWithZone(Convert.ToDateTime(evschdetails.M_Startfrom), userTimeZone, true);
                    dzend = new DateTimeWithZone(Convert.ToDateTime(evschdetails.M_StartTo), userTimeZone, true);
                }
                else
                {
                    TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    dtzstart = new DateTimeWithZone(Convert.ToDateTime(evschdetails.M_Startfrom), userTimeZone, true);
                    dzend = new DateTimeWithZone(Convert.ToDateTime(evschdetails.M_StartTo), userTimeZone, true);
                }



                DateTime sDate = new DateTime();
                sDate = dtzstart.LocalTime;
                startday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(sDate).ToString();

                sDate_new = sDate.ToString("MMM dd, yyyy");


                DateTime eDate = new DateTime();
                eDate = dzend.LocalTime;
                endday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(eDate).ToString();
                eDate_new = eDate.ToString("MMM dd, yyyy");

                starttime = sDate.ToString("h:mm tt").ToLower().Trim().Replace(" ", ""); ;
                endtime = eDate.ToString("h:mm tt").ToLower().Trim().Replace(" ", ""); ;
                ENDATE = dzend.LocalTime;

            }
            else
            {
                var evschdetails = (from ev in db.EventVenues where ev.EventID == Eventid select ev).FirstOrDefault();
                if (evschdetails != null)
                {

                    if (Timezonedetail != null)
                    {
                        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Timezonedetail.TimeZone);
                        dtzstart = new DateTimeWithZone(Convert.ToDateTime(evschdetails.E_Startdate), userTimeZone, true);
                        dzend = new DateTimeWithZone(Convert.ToDateTime(evschdetails.E_Enddate), userTimeZone, true);
                    }
                    else
                    {
                        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                        dtzstart = new DateTimeWithZone(Convert.ToDateTime(evschdetails.E_Startdate), userTimeZone, true);
                        dzend = new DateTimeWithZone(Convert.ToDateTime(evschdetails.E_Enddate), userTimeZone, true);
                    }


                    DateTime sDate = new DateTime();
                    sDate = dtzstart.LocalTime;
                    startday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(sDate).ToString();
                    sDate_new = sDate.ToString("MMM dd,yyyy");


                    DateTime eDate = new DateTime();
                    eDate = dzend.LocalTime;
                    endday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(eDate).ToString();
                    eDate_new = eDate.ToString("MMM dd,yyyy");


                    starttime = sDate.ToString("h:mm tt").ToLower().Trim().Replace(" ", ""); ;
                    endtime = eDate.ToString("h:mm tt").ToLower().Trim().Replace(" ", ""); ;
                    ENDATE = dzend.LocalTime;
                }
            }
            var timezone = "";
            DateTime dateTime = new DateTime();

            if (Timezonedetail != null)
            {
                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Timezonedetail.TimeZone);
                dtzCreated = new DateTimeWithZone(DateTime.Now, userTimeZone, false);


                //Timezone value

            }
            else
            {
                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                dtzCreated = new DateTimeWithZone(DateTime.Now, userTimeZone, false);
            }

            dateTime = dtzCreated.LocalTime;
            if (!string.IsNullOrEmpty(eDate_new))
            {

                if (ENDATE < dateTime)
                {

                    Mevent.EventExpired = "Y";
                }
                else
                {
                    Mevent.EventExpired = "N";
                }
            }
            else
            {
                Mevent.EventExpired = "Y";
            }
            //Get Event Date
            //Trn
            var transaction = db.Ticket_Purchased_Detail.Any(i => i.TPD_Event_Id == Eventid);
            if (transaction)
            {
                Mevent.Eventtransaction = "Y";
            }
            else
            {
                Mevent.Eventtransaction = "N";
            }
            //Trn
            var url = Request.Url;
            var baseurl = url.GetLeftPart(UriPartial.Authority);
            string title = Regex.Replace(Edetails.EventTitle.Trim().Replace(" ", " - "), "[^ a - zA - Z0 - 9_ -] + ", "");
            var urldb = GetEventURL(Eventlid);
            if (urldb.Contains("/"))
            {
                Mevent.url = baseurl + urldb;
            }
            else
            {
                Mevent.url = Request.Url.Scheme + System.Uri.SchemeDelimiter + urldb + "." + Request.Url.Host.Replace("www.", "").Replace("WWW.", "") + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
            }

            Mevent.Descritption = Edetails.EventDescription;
            Mevent.Eventid = Eventid;
            Mevent.Eventstatus = Edetails.EventStatus;
            Mevent.Eventtitle = Edetails.EventTitle;
            Mevent.EventAddress = TopAddress;
            Mevent.Eventcancel = Edetails.EventCancel;
            Mevent.Eventdate = startday.ToString() + " " + sDate_new + " " + starttime;
            Mevent.Eventprivacy = Edetails.EventPrivacy;
            Mevent.EventHits = GetEventTotalHits(Eventid);
            Mevent.DiscountCode = Discountcode;
            Session["logo"] = "events";
            Session["Fromname"] = "myevents";



            if (type == "P")
            {
                TempData["Success"] = vmc.Index("ManageEvent", "MEPublisheventSucc");
            }
            else
            {
                TempData["Success"] = null;
            }
            OrderAttendees CO = new OrderAttendees();
            var Order = (from o in db.EventTicket_View
                         where o.EventID == Eventid && o.IsManualOrder == false
                         select new OrderAttendees()
                         {
                             OrderId = o.OrderId,
                             Amount = ((o.OrderStateId ?? 0) == 3 || (o.OrderStateId ?? 0) == 2 ? 0 : (o.PaidAmount ?? 0) + (o.VariableAmount ?? 0)).ToString(),
                             NetAmount = ((o.PaidAmount ?? 0) + (o.VariableAmount ?? 0) - ((o.OrderStateId ?? 0) == 3 ? o.PaidAmount + (o.VariableAmount ?? 0) - o.ECFeePerTicket * (o.PurchasedQuantity ?? 0) - o.MerchantFeePerTicket * (o.PurchasedQuantity ?? 0) : 0) - ((o.OrderStateId ?? 0) == 2 ? o.PaidAmount + (o.VariableAmount ?? 0) - o.ECFeePerTicket * (o.PurchasedQuantity ?? 0) - o.MerchantFeePerTicket * (o.PurchasedQuantity ?? 0) : 0)).ToString(),
                             Qty = (o.PurchasedQuantity ?? 0).ToString(),
                             Name = o.FirstName + " " + o.LastName,
                             Date = o.O_OrderDateTime.ToString(),
                             Status = o.OrderStateName
                         }).Distinct().Take(3).ToList();

            Mevent.Order = Order;
            Mevent.Attendess = Order;

            DateTime dt = DateTime.Today.AddDays(-30);
            StringBuilder strDates = new StringBuilder();
            StringBuilder strSaleQty = new StringBuilder();
            long lHitCount = 0;
            for (int i = 1; dt <= DateTime.Today; i++)
            {
                if (strDates.ToString().Equals(""))
                    strDates.Append(dt.ToString("MM/dd"));
                else
                    strDates.Append("," + dt.ToString("MM/dd"));

                lHitCount = GetEventHitDayCount(Eventid, dt);
                strDates.Append("-");
                if (lHitCount > 0)
                {
                    strDates.Append(lHitCount.ToString());
                }
                else
                {
                    strDates.Append(" ");
                }


                if (strSaleQty.ToString().Equals(""))
                    strSaleQty.Append(dt.ToString("MM/dd"));
                else
                    strSaleQty.Append("," + dt.ToString("MM/dd"));

                SaleTickets objSale = GetTicketSalebyEvent(Eventid, dt);
                strSaleQty.Append("-");
                if (objSale != null)
                {
                    strSaleQty.Append(objSale.SaleQty);
                }
                else
                {
                    strSaleQty.Append(" ");
                }
                dt = dt.AddDays(1);
            }

            CultureInfo us = new CultureInfo("en-US");
            var eInfo = _tservice.GetEventSummaryCalculation(Eventid, FilterByOrderType.Regular);

            TempData["EventHits"] = strDates.ToString();
            TempData["SaleQty"] = strSaleQty.ToString();
            TempData["TicketSalePer"] = GetSalePer(Eventid);

            TempData["RemQty"] = (eInfo != null ? eInfo.TicketQuantity : 0);
            TempData["TotalQty"] = GetQuantity(Eventid, "T");
            TempData["PaidTicket"] = GetTicketQtyPer(Eventid, "P");
            TempData["FreeTicket"] = GetTicketQtyPer(Eventid, "F");
            TempData["EventUrl"] = GetEventURL(Eventid);

            TempData["ForSale"] = Math.Round((eInfo != null ? eInfo.Price : 0), 2).ToString("N", us);
            TempData["NETSale"] = Math.Round((eInfo != null ? eInfo.PriceNet : 0), 2).ToString("N", us);

            return View(Mevent);
        }

        public string GetEventHitsChart(string strDurataion, long lEventId)
        {
            DateTime dt = new DateTime();
            StringBuilder strDates = new StringBuilder();
            StringBuilder strSaleQty = new StringBuilder();
            long lHitCount = 0;

            if (strDurataion == "Week" || strDurataion == "Month")
            {
                if (strDurataion == "Week")
                    dt = DateTime.Today.AddDays(-6);
                else
                    dt = DateTime.Today.AddDays(-30);

                for (int i = 1; dt <= DateTime.Today; i++)
                {
                    if (strDates.ToString().Equals(""))
                        strDates.Append(dt.ToString("MM/dd"));
                    else
                        strDates.Append("," + dt.ToString("MM/dd"));

                    lHitCount = GetEventHitDayCount(lEventId, dt);
                    strDates.Append("-");
                    if (lHitCount > 0)
                    {
                        strDates.Append(lHitCount.ToString());
                    }
                    else
                    {
                        strDates.Append(" ");
                    }
                    dt = dt.AddDays(1);
                }
            }
            else if (strDurataion == "Year")
            {
                dt = DateTime.Today.AddMonths(-11);

                for (int i = 1; i <= 12; i++)
                {
                    if (strDates.ToString().Equals(""))
                        strDates.Append(dt.ToString("MM/yy"));
                    else
                        strDates.Append("," + dt.ToString("MM/yy"));

                    lHitCount = GetEventHitDayCount(lEventId, dt.Month, dt.Year);
                    strDates.Append("-");
                    if (lHitCount > 0)
                    {
                        strDates.Append(lHitCount.ToString());
                    }
                    else
                    {
                        strDates.Append(" ");
                    }
                    dt = dt.AddMonths(1);
                }
            }
            else if (strDurataion == "Day")
            {

                dt = DateTime.Now.AddHours(-23);

                for (int i = 1; i <= 24; i++)
                {
                    if (strDates.ToString().Equals(""))
                        strDates.Append(dt.Hour.ToString());
                    else
                        strDates.Append("," + dt.Hour.ToString());

                    lHitCount = GetEventHitDayCount(lEventId, dt, dt.Hour);
                    strDates.Append("-");
                    if (lHitCount > 0)
                    {
                        strDates.Append(lHitCount.ToString());
                    }
                    else
                    {
                        strDates.Append(" ");
                    }
                    dt = dt.AddHours(1);
                }
            }


            return strDates.ToString();
        }



        public string GetEventSaleChart(string strDurataion, long lEventId)
        {
            DateTime dt = new DateTime();
            StringBuilder strSaleQty = new StringBuilder();
            long lHitCount = 0;

            if (strDurataion == "Week" || strDurataion == "Month")
            {
                if (strDurataion == "Week")
                    dt = DateTime.Today.AddDays(-6);
                else
                    dt = DateTime.Today.AddDays(-30);

                for (int i = 1; dt <= DateTime.Today; i++)
                {

                    if (strSaleQty.ToString().Equals(""))
                        strSaleQty.Append(dt.ToString("MM/dd"));
                    else
                        strSaleQty.Append("," + dt.ToString("MM/dd"));

                    SaleTickets objSale = GetTicketSalebyEvent(lEventId, dt);
                    strSaleQty.Append("-");
                    if (objSale != null)
                    {
                        strSaleQty.Append(objSale.SaleQty);
                    }
                    else
                    {
                        strSaleQty.Append(" ");
                    }
                    dt = dt.AddDays(1);
                }
            }
            else if (strDurataion == "Year")
            {
                dt = DateTime.Today.AddMonths(-11);

                for (int i = 1; i <= 12; i++)
                {
                    if (strSaleQty.ToString().Equals(""))
                        strSaleQty.Append(dt.ToString("MM/yy"));
                    else
                        strSaleQty.Append("," + dt.ToString("MM/yy"));

                    SaleTickets objSale = GetTicketSalebyEventYear(lEventId, dt.Month, dt.Year);
                    //SaleTickets objSale = new SaleTickets();
                    strSaleQty.Append("-");
                    if (objSale != null)
                    {
                        strSaleQty.Append(objSale.SaleQty);
                    }
                    else
                    {
                        strSaleQty.Append(" ");
                    }
                    dt = dt.AddMonths(1);
                }
            }
            else if (strDurataion == "Day")
            {
                dt = DateTime.Now.AddHours(-23);
                for (int i = 1; i <= 24; i++)
                {
                    if (strSaleQty.ToString().Equals(""))
                        strSaleQty.Append(dt.Hour.ToString());
                    else
                        strSaleQty.Append("," + dt.Hour.ToString());

                    SaleTickets objSale = GetTicketSalebyEventDay(lEventId, dt, dt.Hour);
                    //SaleTickets objSale = new SaleTickets();
                    strSaleQty.Append("-");
                    if (objSale != null)
                    {
                        strSaleQty.Append(objSale.SaleQty);
                    }
                    else
                    {
                        strSaleQty.Append(" ");
                    }
                    dt = dt.AddHours(1);
                }
            }


            return strSaleQty.ToString();
        }

        [Authorize]
        public ActionResult EmailInvitations(long eventId, string tab, string sortOrder, int? page)
        {
            if (Session["AppId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string LgUser = Session["AppId"].ToString();
            using (EventComboEntities db = new EventComboEntities())
            {
                AspNetUser aspuser = db.AspNetUsers.First(i => i.Id == LgUser);

                aspuser.LastLoginTime = System.DateTime.UtcNow;
                db.SaveChanges();


            }
            if (CommanClasses.CompareCurrentUser(eventId, Session["AppId"].ToString().Trim()) == false) return RedirectToAction("Index", "Home");

            ViewBag.CurrentSort = (sortOrder ?? "subject");
            ValidationMessageController vmc = new ValidationMessageController();
            eventId = vmc.GetLatestEventId(eventId);
            long levtId = ValidationMessageController.GetParentEventId(eventId);
            ViewBag.tab = tab;
            ViewBag.EventId = eventId;
            Session["Fromname"] = "emailinvitation";
            Session["logo"] = "events";
            using (EventComboEntities objEnt = new EventComboEntities())
            {
                var invitations = from invite_list in objEnt.Event_Email_List
                                  group invite_list by invite_list.L_I_Id
                                  into result1
                                  join invites in objEnt.Event_Email_Invitation on result1.FirstOrDefault().L_I_Id equals invites.I_Id
                                  where invites.I_Event_Id == levtId && (invites.I_Mode == "S" || invites.I_Mode == "N")
                                  orderby invites.I_ModifyDate
                                  select new EmailInvitation
                                  {
                                      EventID = invites.I_Event_Id,
                                      Subject = invites.I_SubjectLine,
                                      SendOn = invites.I_ScheduleDate,
                                      CreatedOn = invites.I_CreateDate,
                                      I_Id = invites.I_Id,
                                      NoOfRecipients = result1.Count()
                                  };
                ViewBag.scheduledCount = invitations.Count();
                switch (sortOrder)
                {
                    case "subject_desc":
                        invitations = invitations.OrderByDescending(s => s.Subject);
                        break;
                    case "created_date":
                        invitations = invitations.OrderBy(s => s.CreatedOn);
                        break;
                    case "created_date_desc":
                        invitations = invitations.OrderByDescending(s => s.CreatedOn);
                        break;
                    case "send_date":
                        invitations = invitations.OrderBy(s => s.SendOn);
                        break;
                    case "send_date_desc":
                        invitations = invitations.OrderByDescending(s => s.SendOn);
                        break;
                    case "recipient":
                        invitations = invitations.OrderBy(s => s.NoOfRecipients);
                        break;
                    case "recipient_desc":
                        invitations = invitations.OrderByDescending(s => s.NoOfRecipients);
                        break;
                    default:
                        invitations = invitations.OrderBy(s => s.SendOn);
                        break;
                }

                int pageSize = 10;
                int pageNumber = (page ?? 1);
                ViewBag.scheduled = invitations.ToPagedList(pageNumber, pageSize);

                //return View(invitations.ToPagedList(pageNumber, pageSize));
            }

            using (EventComboEntities objEnt = new EventComboEntities())
            {
                var invitations = from invite_list in objEnt.Event_Email_List
                                  group invite_list by invite_list.L_I_Id
                                  into result1
                                  join invites in objEnt.Event_Email_Invitation on result1.FirstOrDefault().L_I_Id equals invites.I_Id
                                  where invites.I_Event_Id == levtId && invites.I_Mode == "D"
                                  orderby invites.I_ModifyDate
                                  select new EmailInvitation
                                  {
                                      EventID = invites.I_Event_Id,
                                      Subject = invites.I_SubjectLine,
                                      SendOn = invites.I_ScheduleDate,
                                      CreatedOn = invites.I_CreateDate,
                                      I_Id = invites.I_Id,
                                      NoOfRecipients = result1.Count()
                                  };

                ViewBag.draftCount = invitations.Count();
                switch (sortOrder)
                {
                    case "subject_desc":
                        invitations = invitations.OrderByDescending(s => s.Subject);
                        break;
                    case "created_date":
                        invitations = invitations.OrderBy(s => s.CreatedOn);
                        break;
                    case "created_date_desc":
                        invitations = invitations.OrderByDescending(s => s.CreatedOn);
                        break;
                    case "send_date":
                        invitations = invitations.OrderBy(s => s.SendOn);
                        break;
                    case "send_date_desc":
                        invitations = invitations.OrderByDescending(s => s.SendOn);
                        break;
                    case "recipient":
                        invitations = invitations.OrderBy(s => s.NoOfRecipients);
                        break;
                    case "recipient_desc":
                        invitations = invitations.OrderByDescending(s => s.NoOfRecipients);
                        break;
                    default:
                        invitations = invitations.OrderBy(s => s.SendOn);
                        break;
                }

                int pageSize = 10;
                int pageNumber = (page ?? 1);
                ViewBag.draft = invitations.ToPagedList(pageNumber, pageSize);

                //return View(invitations.ToPagedList(pageNumber, pageSize));
            }


            return View();
        }

        public string DeleteInvitation(long lId)
        {
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    objEnt.Event_Email_List.RemoveRange(objEnt.Event_Email_List.Where(x => x.L_I_Id == lId));
                    objEnt.Event_Email_Invitation.Remove(objEnt.Event_Email_Invitation.Where(x => x.I_Id == lId).FirstOrDefault());
                    objEnt.SaveChanges();
                    return "D";

                }
            }
            catch (Exception ex)
            {
                return "E";
            }
        }
        public string GetAllTicketSale(long EventId)
        {
            StringBuilder strResult = new StringBuilder();
            using (EventComboEntities objEnt = new EventComboEntities())
            {
                var vEvent = (from myRow in objEnt.Events
                              where myRow.EventID == EventId
                              select myRow).FirstOrDefault();

                var timezone = "";
                DateTime dateTime = new DateTime();
                var Timezonedetail = (from ev in db.TimeZoneDetails where ev.TimeZone_Id.ToString() == vEvent.TimeZone select ev).FirstOrDefault();
                if (Timezonedetail != null)
                {
                    timezone = Timezonedetail.TimeZone;
                    TimeZoneInfo timeZoneInfo;


                    timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                    dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
                    //Timezone value

                }

                var vTickets = (from myRow in objEnt.Tickets
                                where myRow.E_Id == EventId
                                select myRow).ToList().OrderBy(y => y.T_name);
                long dSoldQty = 0;
                strResult.Append("<table id='tbSaleTicket' class='crt_event_list_tabl sales_by_tkt_tab'>");
                strResult.Append("<thead>");
                strResult.Append("<tr>");
                strResult.Append("<th>Ticket Type</th>");
                strResult.Append("<th>Price</th>");
                strResult.Append("<th>Sold</th>");
                strResult.Append("<th>Status</th>");
                strResult.Append("<th>End Sales</th>");
                strResult.Append("<th>View Sale</th>");
                strResult.Append("</tr>");
                strResult.Append("</thead>");
                strResult.Append("<tbody>");
                DateTime dtHideuntil = new DateTime(); DateTime dtHideafter = new DateTime();
                string strHideUntil; string strHideUntilTime;
                string strHideAfter; string strHideAfterTime;
                foreach (Ticket obj in vTickets)
                {
                    strResult.Append("<tr>");
                    strResult.Append("<td>"); strResult.Append(obj.T_name); strResult.Append("</td>");
                    if (obj.TicketTypeID == 1)
                    {
                        strResult.Append("<td>"); strResult.Append("FREE"); strResult.Append("</td>");
                    }
                    else
                    {
                        if (obj.Price != null && obj.Price > 0)
                        {
                            strResult.Append("<td>"); strResult.Append(obj.Price); strResult.Append("</td>");
                        }
                        else
                        {
                            strResult.Append("<td>"); strResult.Append("-"); strResult.Append("</td>");
                        }
                    }

                    var vRemQty = (from myRow in objEnt.Ticket_Quantity_Detail
                                   where myRow.TQD_Event_Id == EventId && myRow.TQD_Ticket_Id == obj.T_Id
                                   select myRow).FirstOrDefault();
                    if (vRemQty != null)
                    {
                        dSoldQty = ((vRemQty.TQD_Quantity != null ? Convert.ToInt64(vRemQty.TQD_Quantity) : 0) - (vRemQty.TQD_Remaining_Quantity != null ? Convert.ToInt64(vRemQty.TQD_Remaining_Quantity) : 0));
                        strResult.Append("<td>"); strResult.Append(dSoldQty.ToString() + "/" + (vRemQty.TQD_Quantity != null ? vRemQty.TQD_Quantity.ToString() : "0")); strResult.Append("</td>");
                    }
                    else
                    {
                        strResult.Append("<td>"); strResult.Append("0/0"); strResult.Append("</td>");
                    }

                    strHideUntil = (obj.Hide_Untill_Date != null ? (obj.Hide_Untill_Date ?? default(DateTime)).ToString("d") : "");
                    strHideUntilTime = (obj.Hide_Untill_Time != null ? obj.Hide_Untill_Time.ToString() : "");

                    strHideAfter = (obj.Hide_After_Date != null ? (obj.Hide_After_Date ?? default(DateTime)).ToString("d") : "");
                    strHideAfterTime = (obj.Hide_After_Time != null ? obj.Hide_After_Time.ToString() : "");

                    if (!strHideUntil.Equals(string.Empty))
                    {
                        dtHideuntil = DateTime.Parse(strHideUntil + " " + strHideUntilTime);
                    }

                    if (!strHideAfter.Equals(string.Empty))
                    {
                        dtHideafter = DateTime.Parse(strHideAfter + " " + strHideAfterTime);
                    }


                    if (vRemQty != null && dSoldQty == vRemQty.TQD_Quantity)
                    {
                        strResult.Append("<td>"); strResult.Append("Sold Out"); strResult.Append("</td>");
                    }
                    else if (strHideUntil != "" && dateTime <= dtHideuntil)
                    {
                        strResult.Append("<td>"); strResult.Append("Hidden"); strResult.Append("</td>");
                    }
                    else if (strHideAfter != "" && dateTime > dtHideafter)
                    {
                        strResult.Append("<td>"); strResult.Append("Hidden"); strResult.Append("</td>");
                    }
                    else
                    {
                        strResult.Append("<td>"); strResult.Append("On Sale"); strResult.Append("</td>");
                    }
                    strResult.Append("<td>"); strResult.Append((obj.Sale_End_Date != null ? obj.Sale_End_Date.ToString() : "-")); strResult.Append("</td>");
                    strResult.Append("<td>"); strResult.Append("-"); strResult.Append("</td>");
                    strResult.Append("</tr>");
                }
                strResult.Append("</tbody>");
                strResult.Append("</table>");
            }
            return strResult.ToString();
        }

        public string SaveEventUrl(long lEventId, string strEventUrl)
        {
            string strResult = "N";
            try
            {
                if (CheckEventUrl(strEventUrl, lEventId) == "Y")
                {
                    strResult = "N";
                }
                else
                {
                    strResult = "Y";
                    using (EventComboEntities objEnt = new EventComboEntities())
                    {

                        Event objEvt = objEnt.Events.First(i => i.EventID == lEventId);
                        objEvt.EventUrl = strEventUrl;
                        objEnt.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                strResult = "N";
            }
            return strResult;
        }
        public string CheckEventUrl(string strUserUrl, long EventId)
        {
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var Eventurl = (from myRow in objEnt.Events
                                    where myRow.EventUrl == strUserUrl && myRow.EventID != EventId
                                    select myRow).SingleOrDefault();

                    if (Eventurl == null) return "N";
                    if (Eventurl.ToString().Equals(string.Empty))
                        return "N"; // Not Exists
                    else
                        return "Y"; // Exists
                }
            }
            catch (Exception ex)
            {
                return "Y";

            }
        }
        public string GetEventURL(long lEventId)
        {
            string strResult = "";
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var vEvent = (from myRow in objEnt.Events
                                  where myRow.EventID == lEventId
                                  select myRow).FirstOrDefault();


                    if (vEvent.EventUrl != null && vEvent.EventUrl.Trim() != string.Empty)
                    {
                        strResult = vEvent.EventUrl.Trim();
                    }
                    else
                    {
                      strResult = @Url.Action("ViewEvent", "EventManagement", new { strEventDs = Regex.Replace(vEvent.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = vEvent.EventID.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                return strResult = "";
            }
            return strResult;
        }
        public string DeleteEvent(long eventid)
        {
            string msg = "";

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Event_Orgnizer_Detail.RemoveRange(db.Event_Orgnizer_Detail.Where(x => x.Orgnizer_Event_Id == eventid).ToList());
                    db.Event_VariableDesc.RemoveRange(db.Event_VariableDesc.Where(x => x.Event_Id == eventid).ToList());
                    db.EventImages.RemoveRange(db.EventImages.Where(x => x.EventID == eventid).ToList());
                    db.EventFavourites.RemoveRange(db.EventFavourites.Where(x => x.eventId == eventid).ToList());
                    db.Addresses.RemoveRange(db.Addresses.Where(x => x.EventId == eventid).ToList());
                    db.EventVenues.RemoveRange(db.EventVenues.Where(x => x.EventID == eventid).ToList());
                    db.EventVotes.RemoveRange(db.EventVotes.Where(x => x.eventId == eventid).ToList());
                    db.MultipleEvents.RemoveRange(db.MultipleEvents.Where(x => x.EventID == eventid).ToList());
                    db.Publish_Event_Detail.RemoveRange(db.Publish_Event_Detail.Where(x => x.PE_Event_Id == eventid).ToList());
                    db.Ticket_Quantity_Detail.RemoveRange(db.Ticket_Quantity_Detail.Where(x => x.TQD_Event_Id == eventid).ToList());
                    db.Events_Hit.RemoveRange(db.Events_Hit.Where(x => x.EventHit_EventId == eventid).ToList());
                    db.Tickets.RemoveRange(db.Tickets.Where(x => x.E_Id == eventid).ToList());
                    db.Events.RemoveRange(db.Events.Where(x => x.EventID == eventid).ToList());

                    db.SaveChanges();
                    transaction.Commit();
                    msg = "Y";
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    msg = "N";
                }
            }

            return msg;
        }

        public long GetEventHitDayCount(long eventId, DateTime dt)
        {
            long lResult = 0;
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    //var vEvent = objEnt.Events_Hit.SqlQuery("Select EventHit_Id from Events_Hit").Count();
                    var vEvent = objEnt.Database.SqlQuery<long>("Select EventHit_Id from Events_Hit where EventHit_EventId = " + eventId + " and  convert(date,EventHitDatetime) = convert(date,'" + dt + "')").Count();
                    //var vEvent = (from myEnt in objEnt.Events_Hit where myEnt.EventHit_EventId == eventId && myEnt.EventHitDateTime == dt  select myEnt.EventHit_Id).Count();
                    lResult = vEvent;
                }
            }
            catch (Exception ex)
            {
                lResult = 0;
            }

            return lResult;
        }
        public long GetEventHitDayCount(long eventId, int iMonth, int iYear)
        {
            long lResult = 0;
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {

                    var vEvent = objEnt.Database.SqlQuery<long>("Select EventHit_Id from Events_Hit where EventHit_EventId = " + eventId + " and Month(convert(date,EventHitDatetime)) = " + iMonth.ToString() + " And Year(convert(date,EventHitDatetime)) = " + iYear.ToString()).Count();

                    lResult = vEvent;
                }
            }
            catch (Exception ex)
            {
                lResult = 0;
            }

            return lResult;
        }
        public long GetEventHitDayCount(long eventId, DateTime dt, int iHour)
        {
            long lResult = 0;
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var vEvent = objEnt.Database.SqlQuery<long>("Select EventHit_Id from Events_Hit where EventHit_EventId = " + eventId + " and  convert(date,EventHitDatetime) = convert(date,'" + dt + "') And datepart(hour,EventHitDateTime) = " + iHour.ToString()).Count();

                    lResult = vEvent;
                }
            }
            catch (Exception ex)
            {
                lResult = 0;
            }

            return lResult;
        }
        public long GetEventTotalHits(long eventId)
        {
            long lResult = 0;
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var vEvent = objEnt.Database.SqlQuery<long>("Select EventHit_Id from Events_Hit where EventHit_EventId = " + eventId).Count();
                    lResult = vEvent;
                }
            }
            catch (Exception ex)
            {
                lResult = 0;
            }
            return lResult;
        }
        public double GetSalePer(long eventId)
        {
            double dResult = 0;
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var vtotalqty = (from myRow in objEnt.Ticket_Quantity_Detail where myRow.TQD_Event_Id == eventId select myRow.TQD_Quantity).Sum();
                    var vremqty = (from myRow in objEnt.Ticket_Quantity_Detail where myRow.TQD_Event_Id == eventId select myRow.TQD_Remaining_Quantity).Sum();
                    double ltotalqty = (vtotalqty != null ? Convert.ToInt64(vtotalqty) : 0);
                    double lremqty = (vremqty != null ? Convert.ToInt64(vremqty) : 0);
                    dResult = Math.Round(((ltotalqty - lremqty) * 100) / ltotalqty, 2);
                }
            }
            catch (Exception ex)
            {
                dResult = 0;
            }
            return dResult;
        }

        public double GetTicketQtyPer(long eventId, string strTicketType)
        {
            double dResult = 0;
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    if (strTicketType == "P")
                    {
                        var vtotalqty = objEnt.Database.SqlQuery<long>("SELECT sum(TQD_Quantity) TQty From (Ticket_Quantity_Detail TQD LEFT JOIN  Ticket T on TQD.TQD_Ticket_Id = T.T_Id)  where TQD_Event_Id = " + eventId + " and T.TicketTypeID =2").FirstOrDefault();
                        var vremqty = objEnt.Database.SqlQuery<long>("SELECT sum(TQD_Remaining_Quantity) TRQty From (Ticket_Quantity_Detail TQD LEFT JOIN  Ticket T on TQD.TQD_Ticket_Id = T.T_Id)  where TQD_Event_Id = " + eventId + " and T.TicketTypeID = 2").FirstOrDefault();
                        dResult = ((vtotalqty - vremqty) * 100) / vtotalqty;
                    }
                    else
                    {
                        var vtotalqty = objEnt.Database.SqlQuery<long>("SELECT sum(TQD_Quantity) TQty From (Ticket_Quantity_Detail TQD LEFT JOIN  Ticket T on TQD.TQD_Ticket_Id = T.T_Id)  where TQD_Event_Id = " + eventId + " and T.TicketTypeID = 1").FirstOrDefault();
                        var vremqty = objEnt.Database.SqlQuery<long>("SELECT sum(TQD_Remaining_Quantity) TRQty From (Ticket_Quantity_Detail TQD LEFT JOIN  Ticket T on TQD.TQD_Ticket_Id = T.T_Id)  where TQD_Event_Id = " + eventId + " and T.TicketTypeID = 1").FirstOrDefault();
                        dResult = ((vtotalqty - vremqty) * 100) / vtotalqty;
                    }

                }
            }
            catch (Exception ex)
            {
                dResult = 0;
            }
            return dResult;
        }


        public long GetQuantity(long eventId, string strQtyType)
        {
            long lResult = 0;
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    if (strQtyType == "R")
                    {
                        var vtotalqty = (from myRow in objEnt.Ticket_Quantity_Detail where myRow.TQD_Event_Id == eventId select myRow.TQD_Quantity).Sum();
                        var vremqty = (from myRow in objEnt.Ticket_Quantity_Detail where myRow.TQD_Event_Id == eventId select myRow.TQD_Remaining_Quantity).Sum();
                        long ltotalqty = (vtotalqty != null ? Convert.ToInt64(vtotalqty) : 0);
                        long lremqty = (vremqty != null ? Convert.ToInt64(vremqty) : 0);
                        lResult = (ltotalqty - lremqty);
                    }
                    else
                    {
                        //var vtotalty = (from myRow in objEnt.Ticket_Quantity_Detail where myRow.TQD_Event_Id == eventId select myRow.TQD_Quantity).Sum();
                        //lResult = (vtotalty != null ? Convert.ToInt64(vtotalty) : 0);
                        var vtotalty = (from myRow in objEnt.Tickets where myRow.E_Id == eventId select myRow.Qty_Available).Sum();
                        lResult = vtotalty;
                    }
                }
            }
            catch (Exception ex)
            {
                lResult = 0;
            }
            return lResult;
        }


        public SaleTickets GetTicketSalebyEvent(long eventId, DateTime dt)
        {
            SaleTickets objResult = new SaleTickets();
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    //var vEvent = objEnt.Events_Hit.SqlQuery("Select EventHit_Id from Events_Hit").Count();
                    var ticketid = (from v in db.Tickets where v.E_Id == eventId select v.T_Id).ToList();
                    string joined = string.Join(",", ticketid.ToArray());

                    string strQuery = "SELECT sum(TPD_Purchased_Qty) as SaleQty,Convert(date,O_OrderDateTime) AS orderDate FROM Ticket_Purchased_Detail a inner join  [Ticket_Quantity_Detail] b on a.TPD_TQD_Id=b.TQD_Id  LEFT JOIN Order_Detail_T On a.TPD_Order_Id = Order_Detail_T.O_Order_Id where isnull(TPD_Order_Id,'') !='' AND ISNULL(O_OrderDateTime,'') !='' AND b.TQD_Ticket_Id in (" + joined + ") and COnvert(date,O_OrderDateTime) = convert(date,'" + dt + "') group by Convert(date,O_OrderDateTime) ";
                    var vEvent = objEnt.Database.SqlQuery<SaleTickets>(strQuery).FirstOrDefault();
                    //var vEvent = (from myEnt in objEnt.Events_Hit where myEnt.EventHit_EventId == eventId && myEnt.EventHitDateTime == dt  select myEnt.EventHit_Id).Count();
                    objResult = vEvent;
                }
            }
            catch (Exception ex)
            {
                return objResult;
            }

            return objResult;
        }
        public SaleTickets GetTicketSalebyEventYear(long eventId, int iMonth, int iYear)
        {
            SaleTickets objResult = new SaleTickets();
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {

                    //   var vEvent = objEnt.Database.SqlQuery<long>("Select EventHit_Id from Events_Hit where EventHit_EventId = " + eventId + " and Month(convert(date,EventHitDatetime)) = " + iMonth.ToString() + " And Year(convert(date,EventHitDatetime)) = " + iYear.ToString()).Count();

                    var ticketid = (from v in db.Tickets where v.E_Id == eventId select v.T_Id).ToList();
                    string joined = string.Join(",", ticketid.ToArray());


                    string strQuery = "SELECT sum(TPD_Purchased_Qty) as SaleQty,Convert(date,O_OrderDateTime) AS orderDate FROM Ticket_Purchased_Detail a inner join  [Ticket_Quantity_Detail] b on a.TPD_TQD_Id=b.TQD_Id LEFT JOIN Order_Detail_T On a.TPD_Order_Id = Order_Detail_T.O_Order_Id where isnull(TPD_Order_Id,'') !='' AND ISNULL(O_OrderDateTime,'') !='' AND b.TQD_Ticket_Id in (" + joined + ") and Month(Convert(date,O_OrderDateTime)) = " + iMonth.ToString() + " and Year(Convert(date,O_OrderDateTime)) = " + iYear.ToString() + " group by Convert(date,O_OrderDateTime) ";
                    var vEvent = objEnt.Database.SqlQuery<SaleTickets>(strQuery).FirstOrDefault();

                    objResult = vEvent;
                }
            }
            catch (Exception ex)
            {
                return objResult;
            }

            return objResult;
        }


        public SaleTickets GetTicketSalebyEventDay(long eventId, DateTime dt, int iHour)
        {
            SaleTickets objResult = new SaleTickets();
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var ticketid = (from v in db.Tickets where v.E_Id == eventId select v.T_Id).ToList();
                    string joined = string.Join(",", ticketid.ToArray());
                    string strQuery = "SELECT sum(TPD_Purchased_Qty) as SaleQty,Convert(date,O_OrderDateTime) AS orderDate FROM Ticket_Purchased_Detail a inner join  [Ticket_Quantity_Detail] b on a.TPD_TQD_Id=b.TQD_Id LEFT JOIN Order_Detail_T On a.TPD_Order_Id = Order_Detail_T.O_Order_Id where isnull(TPD_Order_Id,'') !='' AND ISNULL(O_OrderDateTime,'') !='' AND  b.TQD_Ticket_Id in (" + joined + ") and COnvert(date,O_OrderDateTime) = convert(date,'" + dt + "')  And datepart(hour,O_OrderDateTime) = " + iHour.ToString() + " group by Convert(date,O_OrderDateTime) ";

                    var vEvent = objEnt.Database.SqlQuery<SaleTickets>(strQuery).FirstOrDefault();

                    objResult = vEvent;
                }
            }
            catch (Exception ex)
            {
                return objResult;
            }

            return objResult;
        }

        public string PublishUnpublishEvent(string Tag, long id)
        {
            string result = "";
            try
            {
                Event objEvt = db.Events.First(i => i.EventID == id);
                if (Tag == "P")
                {

                    objEvt.EventStatus = "Live";

                    result = "Y";
                }

                if (Tag == "U")
                {
                    var transaction = db.Ticket_Purchased_Detail.Any(i => i.TPD_Event_Id == id);
                    if (transaction)
                    {
                        result = "T";
                    }
                    else
                    {
                        objEvt.EventStatus = "Save";
                        result = "Y";
                    }
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                result = "N";
            }
            return result;
        }

        public ActionResult CopyEvent(long Eventid)
        {
            string strTitle = "";
            using (EventComboEntities objEnt = new EventComboEntities())
            {
                var vEvent = (from myEnt in objEnt.Events where myEnt.EventID == Eventid select myEnt).FirstOrDefault();
                strTitle = "Copy of " + vEvent.EventTitle;
            }
            TempData["Title"] = strTitle;
            TempData["EventId"] = Eventid.ToString();
            return View();
        }

        public long SaveEvent(long Eventid, string strEventTitle)
        {
            try
            {
                //PayPalRedirect redirect = PayPal.ExpressCheckout(new PayPalOrder { Amount = 50 });

                //Session["token"] = redirect.Token;
                //Response.Redirect(redirect.Url);

                //RedirectResult(redirect.Url);

                string strUserId = (Session["AppId"] != null ? Session["AppId"].ToString() : "");
                EventComboEntities objEntDup = new EventComboEntities();

                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var vEvent = (from myEnt in objEnt.Events where myEnt.EventID == Eventid select myEnt).FirstOrDefault();
                    if (strEventTitle.Trim().Equals("")) strEventTitle = vEvent.EventTitle;
                    Event ObjEC = new Event();
                    var Timezonedetail = (from ev in db.TimeZoneDetails where ev.TimeZone_Id.ToString() == vEvent.TimeZone select ev).FirstOrDefault();

                    ObjEC.EventTypeID = vEvent.EventTypeID;
                    ObjEC.EventCategoryID = vEvent.EventCategoryID;
                    ObjEC.EventSubCategoryID = vEvent.EventSubCategoryID;
                    ObjEC.UserID = strUserId;
                    ObjEC.EventTitle = strEventTitle;
                    ObjEC.DisplayStartTime = vEvent.DisplayStartTime;
                    ObjEC.DisplayEndTime = vEvent.DisplayEndTime;
                    ObjEC.DisplayTimeZone = vEvent.DisplayTimeZone;
                    ObjEC.EventDescription = vEvent.EventDescription;
                    ObjEC.EventPrivacy = vEvent.EventPrivacy;
                    ObjEC.Private_ShareOnFB = vEvent.Private_ShareOnFB;
                    ObjEC.Private_GuestOnly = vEvent.Private_GuestOnly;
                    ObjEC.Private_Password = vEvent.Private_Password;
                    if (vEvent.EventUrl != null && vEvent.EventUrl.Trim() != string.Empty)
                        ObjEC.EventUrl = (vEvent.EventUrl != null ? "copyof" + vEvent.EventUrl : "");
                    else
                        ObjEC.EventUrl = "";

                    ObjEC.PublishOnFB = vEvent.PublishOnFB;
                    ObjEC.IsMultipleEvent = vEvent.IsMultipleEvent;
                    ObjEC.TimeZone = vEvent.TimeZone;
                    ObjEC.DisplayStartTime = vEvent.DisplayStartTime;
                    ObjEC.DisplayEndTime = vEvent.DisplayEndTime;
                    ObjEC.DisplayTimeZone = vEvent.DisplayTimeZone;
                    ObjEC.FBUrl = vEvent.FBUrl;
                    ObjEC.TwitterUrl = vEvent.TwitterUrl;
                    ObjEC.LastLocationAddress = vEvent.LastLocationAddress;
                    ObjEC.AddressStatus = vEvent.AddressStatus;
                    ObjEC.EnableFBDiscussion = vEvent.EnableFBDiscussion;
                    ObjEC.Ticket_DAdress = vEvent.Ticket_DAdress;
                    ObjEC.Ticket_showremain = vEvent.Ticket_showremain;
                    ObjEC.Ticket_showvariable = vEvent.Ticket_showvariable;
                    ObjEC.Ticket_variabledesc = vEvent.Ticket_variabledesc;
                    ObjEC.Ticket_variabletype = vEvent.Ticket_variabletype;
                    ObjEC.ShowMap = vEvent.ShowMap;
                    ObjEC.Parent_EventID = 0;
                    DateTimeWithZone dtzstart, dtzend, dtzCreated;
                    if (Timezonedetail != null)
                    {

                        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Timezonedetail.TimeZone);
                        //Timezone value
                        dtzCreated = new DateTimeWithZone(DateTime.Now, userTimeZone, false);
                    }
                    else
                    {
                        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                        dtzCreated = new DateTimeWithZone(DateTime.Now, userTimeZone, false);
                    }
                    ObjEC.CreateDate = dtzCreated.UniversalTime;
                    ObjEC.EventStatus = "Save";
                    objEnt.Events.Add(ObjEC);
                    string address = "";
                    var vAddress = (from myEnt in objEnt.Addresses where myEnt.EventId == Eventid select myEnt).ToList();
                    if (vAddress != null)
                    {
                        Models.Address ObjAdd = new Models.Address();
                        foreach (Address objA in vAddress)
                        {
                            ObjAdd = new Models.Address();
                            ObjAdd.EventId = ObjEC.EventID;
                            ObjAdd.Address1 = objA.Address1 == null ? "" : objA.Address1;
                            ObjAdd.Address2 = objA.Address2 == null ? "" : objA.Address2;
                            ObjAdd.City = objA.City == null ? "" : objA.City;
                            ObjAdd.CountryID = objA.CountryID;
                            ObjAdd.State = objA.State == null ? "" : objA.State;
                            ObjAdd.UserId = strUserId;
                            ObjAdd.VenueName = objA.VenueName;
                            ObjAdd.Zip = objA.Zip == null ? "" : objA.Zip;
                            ObjAdd.ConsolidateAddress = objA.ConsolidateAddress;
                            ObjAdd.Name = "";
                            objEnt.Addresses.Add(ObjAdd);
                            address= objA.ConsolidateAddress;
                        }
                    }

                    var vEventVenue = (from myEnt in objEnt.EventVenues where myEnt.EventID == Eventid select myEnt).ToList();
                    if (vEventVenue != null)
                    {
                        EventVenue objEVenue = new EventVenue();
                        foreach (EventVenue objEv in vEventVenue)
                        {
                            objEVenue = new EventVenue();
                            if (Timezonedetail != null)
                            {
                                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Timezonedetail.TimeZone);
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(objEv.EventStartDate + " " + objEv.EventStartTime), userTimeZone);
                                dtzend = new DateTimeWithZone(Convert.ToDateTime(objEv.EventEndDate + " " + objEv.EventEndTime), userTimeZone);


                            }
                            else
                            {
                                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(objEv.EventStartDate + " " + objEv.EventStartTime), userTimeZone);
                                dtzend = new DateTimeWithZone(Convert.ToDateTime(objEv.EventEndDate + " " + objEv.EventEndTime), userTimeZone);

                            }

                            objEVenue.EventID = ObjEC.EventID;
                            objEVenue.EventStartDate = objEv.EventStartDate;
                            objEVenue.EventEndDate = objEv.EventEndDate;
                            objEVenue.EventStartTime = objEv.EventStartTime;
                            objEVenue.EventEndTime = objEv.EventEndTime;
                            objEVenue.E_Startdate = dtzstart.UniversalTime;
                            objEVenue.E_Enddate = dtzend.UniversalTime;
                            objEnt.EventVenues.Add(objEVenue);
                        }
                    }

                    var vEventAddress = (from myEnt in objEnt.MultipleEvents where myEnt.EventID == Eventid select myEnt).ToList();
                    if (vEventAddress != null)
                    {
                        MultipleEvent objMEvents = new MultipleEvent();
                        foreach (MultipleEvent objME in vEventAddress)
                        {


                            objMEvents = new MultipleEvent();

                            if (Timezonedetail != null)
                            {
                                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Timezonedetail.TimeZone);
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(objME.StartingFrom + " " + objME.StartTime), userTimeZone);
                                dtzend = new DateTimeWithZone(Convert.ToDateTime(objME.StartingTo + " " + objME.EndTime), userTimeZone);


                            }
                            else
                            {
                                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(objME.StartingFrom + " " + objME.StartTime), userTimeZone);
                                dtzend = new DateTimeWithZone(Convert.ToDateTime(objME.StartingTo + " " + objME.EndTime), userTimeZone);

                            }
                            objMEvents.EventID = ObjEC.EventID;
                            objMEvents.Frequency = objME.Frequency;
                            objMEvents.WeeklyDay = objME.WeeklyDay;
                            objMEvents.MonthlyDay = objME.MonthlyDay;
                            objMEvents.MonthlyWeek = objME.MonthlyWeek;
                            objMEvents.MonthlyWeekDays = objME.MonthlyWeekDays;
                            objMEvents.StartingFrom = objME.StartingFrom;
                            objMEvents.StartingTo = objME.StartingTo;
                            objMEvents.StartTime = objME.StartTime;
                            objMEvents.EndTime = objME.EndTime;
                            objMEvents.M_Startfrom = dtzstart.UniversalTime;
                            objMEvents.M_StartTo = dtzend.UniversalTime;
                            objEnt.MultipleEvents.Add(objMEvents);
                        }
                    }


                    var vOrgnizer = (from myEnt in objEnt.Event_Orgnizer_Detail where myEnt.Orgnizer_Event_Id == Eventid select myEnt).ToList();
                    if (vOrgnizer != null)
                    {
                        Event_Orgnizer_Detail objEOrg = new Event_Orgnizer_Detail();
                        foreach (Event_Orgnizer_Detail objOr in vOrgnizer)
                        {
                            objEOrg = new Event_Orgnizer_Detail();
                            objEOrg.Orgnizer_Event_Id = ObjEC.EventID;

                            objEOrg.DefaultOrg = objOr.DefaultOrg;
                            objEOrg.OrganizerMaster_Id = objOr.OrganizerMaster_Id;

                            objEOrg.UserId = strUserId;
                            objEnt.Event_Orgnizer_Detail.Add(objEOrg);
                        }
                    }

                    var vTicket = (from myEnt in objEnt.Tickets where myEnt.E_Id == Eventid select myEnt).ToList();
                    if (vTicket != null)
                    {
                        Ticket ticket = new Ticket();
                        foreach (Ticket tick in vTicket)
                        {
                            ticket = new Ticket();
                            ticket.E_Id = ObjEC.EventID;
                            ticket.TicketTypeID = tick.TicketTypeID;
                            ticket.T_name = tick.T_name;
                            ticket.Qty_Available = tick.Qty_Available;
                            ticket.Price = tick.Price;
                            ticket.T_Desc = tick.T_Desc;
                            ticket.TicketTypeID = tick.TicketTypeID;
                            ticket.T_order = tick.T_order;
                            ticket.Show_T_Desc = tick.Show_T_Desc;
                            ticket.Fees_Type = tick.Fees_Type;
                            ticket.Sale_Start_Date = tick.Sale_Start_Date;
                            ticket.Sale_Start_Time = tick.Sale_Start_Time;
                            ticket.Sale_End_Date = tick.Sale_End_Date;
                            ticket.Sale_End_Time = tick.Sale_End_Time;
                            ticket.Hide_Ticket = tick.Hide_Ticket;
                            ticket.Auto_Hide_Sche = tick.Auto_Hide_Sche;
                            ticket.T_AutoSechduleType = tick.T_AutoSechduleType;
                            ticket.Hide_Untill_Date = tick.Hide_Untill_Date;
                            ticket.Hide_Untill_Time = tick.Hide_Untill_Time;
                            ticket.Hide_After_Date = tick.Hide_After_Date;
                            ticket.Hide_After_Time = tick.Hide_After_Time;
                            ticket.Min_T_Qty = tick.Min_T_Qty;
                            ticket.Max_T_Qty = tick.Max_T_Qty;
                            ticket.T_Disable = tick.T_Disable;
                            ticket.T_Mark_SoldOut = tick.T_Mark_SoldOut;
                            ticket.EC_Fee = tick.EC_Fee;
                            ticket.Customer_Fee = tick.Customer_Fee;
                            ticket.TotalPrice = tick.TotalPrice;
                            ticket.T_Discount = tick.T_Discount;
                            objEnt.Tickets.Add(ticket);
                        }
                    }



                    var vImage = (from myEnt in objEnt.EventImages where myEnt.EventID == Eventid select myEnt).ToList();
                    if (vImage != null)
                    {
                        EventImage Image = new EventImage();
                        foreach (EventImage img in vImage)
                        {
                            Image = new EventImage();
                            Image.EventID = ObjEC.EventID;
                            Image.EventImageUrl = img.EventImageUrl;
                            Image.ImageType = img.ImageType;
                            objEnt.EventImages.Add(Image);
                        }
                    }


                    var vVariable = (from myEnt in objEnt.Event_VariableDesc where myEnt.Event_Id == Eventid select myEnt).ToList();
                    if (vVariable != null)
                    {
                        Event_VariableDesc var = new Event_VariableDesc();
                        foreach (Event_VariableDesc variable in vVariable)
                        {
                            var = new Event_VariableDesc();
                            var.Event_Id = ObjEC.EventID;
                            var.VariableDesc = variable.VariableDesc;
                            var.Price = variable.Price;
                            objEnt.Event_VariableDesc.Add(var);
                        }
                    }
                    objEnt.SaveChanges();
                    Eventid = (from myEvt in objEnt.Events select myEvt.EventID).Max();
                    EventCreation objCE = new EventCreation();

                    objCE.PublishEvent(Eventid);


                    //send mail

                    string Organisername = "", Organiseremail = "", Organiserphn = ""; ;
                    var eventdetails = (from ev in db.Events where ev.EventID == Eventid select ev).FirstOrDefault();
                    //if(eventdetails!=null && eventdetails.EventStatus=="Live")
                    //{ 
                    var OrganiserDetail = (from ev in db.Event_Orgnizer_Detail join pfd in db.Organizer_Master on ev.OrganizerMaster_Id equals pfd.Orgnizer_Id where ev.Orgnizer_Event_Id == Eventid && ev.DefaultOrg == "Y" select pfd).FirstOrDefault();
                    var Organiserdetail = db.Profiles.FirstOrDefault(i => i.UserID == OrganiserDetail.UserId);
                    var userdetail = db.Profiles.FirstOrDefault(i => i.UserID == strUserId);
                    if (Organiserdetail != null)
                    {
                        Organisername = !String.IsNullOrEmpty(OrganiserDetail.Orgnizer_Name) ? OrganiserDetail.Orgnizer_Name : Organiserdetail.FirstName != null ? Organiserdetail.FirstName : "";
                        Organiseremail = !String.IsNullOrEmpty(OrganiserDetail.Organizer_Email) ? OrganiserDetail.Organizer_Email : Organiserdetail.Email != null ? Organiserdetail.Email : "";
                        Organiserphn = !string.IsNullOrEmpty(OrganiserDetail.Organizer_Phoneno) ? " or call " + OrganiserDetail.Organizer_Phoneno : Organiserdetail.MainPhone != null ? " or call " + Organiserdetail.MainPhone : "";
                    }

                    List<Email_Tag> EmailTag = new List<Email_Tag>();
                    MyAccount ac = new MyAccount();
                    EmailTag = ac.getTag();
                    var Emailtemplate = ac.getEmail("new_event_notification_email");
                    string to = "", from = "", cc = "", bcc = "", emailname = "", subjectn = "", bodyn = "";
                    if (Emailtemplate != null)
                    {
                        if (!string.IsNullOrEmpty(Emailtemplate.To))
                        {


                            to = Emailtemplate.To;
                            if (to.Contains("¶¶UserEmailID¶¶"))
                            {
                                to = to.Replace("¶¶UserEmailID¶¶", userdetail.Email);

                            }
                        }
                        if (!(string.IsNullOrEmpty(Emailtemplate.From)))
                        {
                            from = Emailtemplate.From;
                            if (from.Contains("¶¶UserEmailID¶¶"))
                            {
                                from = from.Replace("¶¶UserEmailID¶¶", userdetail.Email);

                            }

                        }
                        else
                        {
                            from = ConfigurationManager.AppSettings.Get("DefaultEmail"); //ConfigurationManager.AppSettings.Get("UserName");

                        }
                        if (!(string.IsNullOrEmpty(Emailtemplate.CC)))
                        {
                            cc = Emailtemplate.CC;
                            if (cc.Contains("¶¶UserEmailID¶¶"))
                            {
                                cc = cc.Replace("¶¶UserEmailID¶¶", userdetail.Email);

                            }
                        }
                        if (!(string.IsNullOrEmpty(Emailtemplate.Bcc)))
                        {
                            bcc = Emailtemplate.Bcc;
                            if (bcc.Contains("¶¶UserEmailID¶¶"))
                            {
                                bcc = bcc.Replace("¶¶UserEmailID¶¶", userdetail.Email);

                            }
                        }
                        if (!(string.IsNullOrEmpty(Emailtemplate.From_Name)))
                        {
                            emailname = Emailtemplate.From_Name;
                        }
                        else
                        {
                            emailname = from;
                        }
                        if (!string.IsNullOrEmpty(Emailtemplate.Subject))
                        {


                            subjectn = Emailtemplate.Subject;

                            for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
                            {

                                if (subjectn.Contains("¶¶" + EmailTag[i].Tag_Name.Trim() + "¶¶"))
                                {
                                    if (EmailTag[i].Tag_Name == "EventOrganiserName")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventOrganiserName¶¶", Organisername);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventTitleId")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventTitleId¶¶", strEventTitle);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventAddressID")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventAddressID¶¶", address);

                                    }

                                    // All tags




                                }

                            }
                        }



                        if (!string.IsNullOrEmpty(Emailtemplate.TemplateHtml))
                        {
                            bodyn = new MvcHtmlString(HttpUtility.HtmlDecode(Emailtemplate.TemplateHtml)).ToHtmlString();
                            for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
                            {

                                if (bodyn.Contains("¶¶" + EmailTag[i].Tag_Name.Trim() + "¶¶"))
                                {
                                    if (EmailTag[i].Tag_Name == "EventOrganiserName")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventOrganiserName¶¶", Organisername);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventTitleId")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventTitleId¶¶", strEventTitle);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventOrganiserEmail")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventOrganiserEmail¶¶", Organiseremail);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventAddressID")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventAddressID¶¶", address);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventOrganiserNumber")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventOrganiserNumber¶¶", Organiserphn);

                                    }
                                    if (EmailTag[i].Tag_Name == "DiscoverEventurl")
                                    {
                                        var url = Request.Url;
                                        var baseurl = url.GetLeftPart(UriPartial.Authority);
                                        string strUrl = baseurl + Url.Action("ViewEvent", "EventManagement", new { strEventDs = System.Text.RegularExpressions.Regex.Replace(strEventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = ValidationMessageController.GetParentEventId(Eventid).ToString() });
                                        bodyn = bodyn.Replace("¶¶DiscoverEventurl¶¶", strUrl);
                                    }
                                }
                            }
                        }

                        // ImageMapPath = Server.MapPath("..") + "/Images/Imagemap_"+EvtOrDetail.TPD_Order_Id+ ".png";
                        //Mail 
                        ac.SendHtmlFormattedEmail(to, from, subjectn, bodyn, cc, bcc, "", emailname);
                        //Mail 
                    }


                    //




                }
            }
            catch (Exception ex)
            {
                return Eventid;
            }
            return Eventid;
        }

        public void SetScrollTemp()
        {
            TempData["Scroll"] = "PrivaPub";
        }




        public string CancelEvent(long eventid)
        {
            string msg = "";

            try
            {
                Event objEvt = db.Events.First(i => i.EventID == eventid);
                objEvt.EventCancel = "Y";
                db.SaveChanges();
                msg = "Y";
            }
            catch (Exception ex)
            {
                msg = "N";
            }

            return msg;

        }
        [Authorize]
        public ActionResult PromotionalCodes(long Eventlid = 0, string strPageIndex = "page", string searchquery = "")
        {
            if (Session["AppId"] != null)
            {
                if (Eventlid == 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                string LgUser = Session["AppId"].ToString();
                using (EventComboEntities db = new EventComboEntities())
                {
                    AspNetUser aspuser = db.AspNetUsers.First(i => i.Id == LgUser);

                    aspuser.LastLoginTime = System.DateTime.UtcNow;
                    db.SaveChanges();


                }


                ValidationMessage vmc = new ValidationMessage();
                DateTimeWithZone dtzCreated;
                var Eventid = vmc.GetLatestEventId(Eventlid);
                if (CommanClasses.CompareCurrentUser(Eventid, Session["AppId"].ToString().Trim()) == false) return RedirectToAction("Index", "Home");
                Session["Fromname"] = "promotional";
                Session["logo"] = "events";
                showPromocode sc = new showPromocode();
                sc.Eventid = Eventid;
                int pageSize = 20;
                int pageIndex = 1;
                if (strPageIndex != null && strPageIndex != string.Empty && strPageIndex != "page")
                    pageIndex = Convert.ToInt32(strPageIndex);
                List<Promocode> ls = new List<Promocode>();
                EventCreation cms = new EventCreation();
                var Timezonedetail = DateTimeWithZone.Timezonedetail(Eventlid);

                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Timezonedetail);
                dtzCreated = new DateTimeWithZone(DateTime.Now, userTimeZone, false);
                var Eventdetail = cms.GetEventdetail(Eventid);
                var Discountcode = (from x in db.Promo_Code where x.PC_Eventid == Eventid select x).Count();
                sc.Eventtitle = Eventdetail.EventTitle;
                var lstpromo = new List<Promo_Code>();
                if (!string.IsNullOrWhiteSpace(searchquery))
                {
                    lstpromo = (from x in db.Promo_Code
                                orderby x.SavedDate descending
                                where x.PC_Eventid == Eventid && x.PC_Code.Contains(searchquery)
                                select x).ToList();

                }
                else
                {
                    lstpromo = (from x in db.Promo_Code
                                orderby x.SavedDate descending
                                where x.PC_Eventid == Eventid
                                select x).ToList();

                }
                foreach (var item in lstpromo)
                {
                    Promocode pr = new Promocode();
                    pr.Amount = (item.PC_Amount != null || item.PC_Percentage != null) ? (item.PC_Amount != null ? "$" + item.PC_Amount.ToString() : item.PC_Percentage + "%") : "-";
                    pr.code = item.PC_Code;
                    var starttype = "";
                    DateTimeWithZone dtzstart, dtzend;
                    DateTime dtstart = new DateTime();
                    DateTime dtnow = new DateTime();
                    if (item.PC_Startdatetype == "0")
                    {
                        dtzstart = new DateTimeWithZone(Convert.ToDateTime(item.P_Startdate), userTimeZone, true);

                        dtstart = dtzstart.LocalTime;
                        dtnow = dtzCreated.LocalTime;
                        int result = DateTime.Compare(dtstart, dtnow);
                        if (result == 0)
                        {
                            starttype = "Started";
                        }
                        else
                        {
                            starttype = dtstart.ToString("MM-dd-yyyy hh:mm tt");
                        }
                    }
                    else
                    {
                        starttype = item.PC_Start + " before event";
                    }
                    pr.Start = starttype;
                    pr.End = (item.Pc_Enddatetype != null && item.Pc_Enddatetype == "1") ? item.PC_End + " before event" : new DateTimeWithZone(Convert.ToDateTime(item.P_Enddate), userTimeZone, true).LocalTime.ToString("MM-dd-yyyy hh:mm tt");
                    pr.Limit = item.PC_Uses != null ? item.PC_Uses.ToString() : "No Limit";
                    pr.PCID = item.PC_id;
                    pr.Orderpromo = (from v in db.Order_Detail_T where v.O_PromoCodeId == item.PC_id select v).Count();
                    ls.Add(pr);
                }
                double dPageCount = ls.Count;
                ViewData["countlist"] = ls.Count;
                double dTotalPages = dPageCount / pageSize;
                int lTotalPages = (ls.Count / pageSize);
                if (dTotalPages.ToString().Contains(".") == true)
                    lTotalPages = lTotalPages + 1;
                sc.Promocode = ls.ToPagedList(pageIndex, pageSize);

                TempData["TotalPages"] = lTotalPages;
                sc.searchquery = searchquery;
                sc.discountcode = Discountcode;
                TempData["PageIndex"] = (strPageIndex.ToLower() == "page" ? "1" : strPageIndex);
                return View(sc);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        public ActionResult CreatePromotionalCodes(long Eventlid = 0, long Promocode = 0)
        {
            if (Session["AppId"] != null)
            {
                if (Eventlid == 0)
                {
                    return RedirectToAction("Index", "Home");
                }

                string LgUser = Session["AppId"].ToString();
                using (EventComboEntities db = new EventComboEntities())
                {
                    AspNetUser aspuser = db.AspNetUsers.First(i => i.Id == LgUser);

                    aspuser.LastLoginTime = System.DateTime.UtcNow;
                    db.SaveChanges();


                }

                ValidationMessage vmc = new ValidationMessage();
                DateTimeWithZone dtzstart, dzend, dtzpcstart, dtzCreated, dtzpcend;
                //DateTimeWithZone tz = new DateTimeWithZone();
                var Eventid = vmc.GetLatestEventId(Eventlid);
                if (CommanClasses.CompareCurrentUser(Eventid, Session["AppId"].ToString().Trim()) == false) return RedirectToAction("Index", "Home");

                Promo_Code pm = new Promo_Code();
                var ttype = 0;
                DateTime end_date = new DateTime();
                EventCreation cms = new EventCreation();
                DateTime now = new DateTime();
                Session["Fromname"] = "promotional";
                Session["logo"] = "events";

                string startdate = "", enddate = "";

                var Eventdetail = cms.GetEventdetail(Eventid);
                pm.Eventitle = Eventdetail.EventTitle;
                pm.Eventitle = Eventdetail.EventTitle;
                var url = Request.Url;
                var baseurl = url.GetLeftPart(UriPartial.Authority);
                var urldb = GetEventURL(Eventlid);

                var Discountcode = (from x in db.Promo_Code where x.PC_Eventid == Eventid select x).Count();
                pm.discountcode = Discountcode;
                pm.PC_Eventid = Eventid;
                var startdatesave = "";
                var singleevnt = (from x in db.EventVenues where x.EventID == Eventid select x).Any();
                var Timezonedetail = DateTimeWithZone.Timezonedetail(Eventlid);

                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Timezonedetail);
                dtzCreated = new DateTimeWithZone(DateTime.Now, userTimeZone, false);
                now = dtzCreated.LocalTime;
                if (singleevnt)
                {
                    var y = (from x in db.EventVenues where x.EventID == Eventid select x).FirstOrDefault();

                    dtzstart = new DateTimeWithZone(Convert.ToDateTime(y.E_Startdate), userTimeZone, true);
                    dzend = new DateTimeWithZone(Convert.ToDateTime(y.E_Enddate), userTimeZone, true);
                    end_date = dtzstart.LocalTime;
                    startdatesave = dtzstart.LocalTime.ToString("MM-dd-yyyy hh:mm tt");

                }
                else
                {
                    var y = (from x in db.MultipleEvents where x.EventID == Eventid select x).FirstOrDefault();

                    dtzstart = new DateTimeWithZone(Convert.ToDateTime(y.M_Startfrom), userTimeZone, true);
                    dzend = new DateTimeWithZone(Convert.ToDateTime(y.M_StartTo), userTimeZone, true);
                    end_date = dtzstart.LocalTime;
                    startdatesave = dtzstart.LocalTime.ToString("MM-dd-yyyy hh:mm tt");

                }
                if (Promocode == 0)
                {
                    pm.Ticketdata = (from x in db.Tickets where x.E_Id == Eventid select x).ToList();
                    foreach (var item in pm.Ticketdata)
                    {
                        if (item.TicketTypeID == 2)
                        {
                            ttype = 1;
                        }
                    }
                    pm.ticketype = ttype;
                    pm.Formtype = "S";

                    startdate = now.ToString("MM-dd-yyyy hh:mm tt");

                    pm.orderrow = false;
                    pm.PC_Start = startdate;
                    pm.PC_End = startdatesave;
                    pm.startdatesave = end_date.ToString();
                    pm.PC_id = 0;
                    pm.PC_URL = baseurl + "/" + urldb + "?discount=Example";
                    pm.Pc_Enddatetype = "0";
                    pm.PC_Startdatetype = "0";
                    if (end_date < now)
                    {
                        pm.startdays = "0 Days 0 Hrs 0 Min";

                    }
                    else
                    {
                        TimeSpan span = (end_date - now);
                        pm.startdays = span.Days.ToString() + " Days " + span.Hours.ToString() + " Hrs " + span.Minutes.ToString() + " Min";

                    }
                    pm.enddays = "0 Days 0 Hrs 0 Min";
                }
                else
                {


                    var Orderdetail = (from x in db.Order_Detail_T where x.O_PromoCodeId == Promocode select x).Any();
                    var p = (from x in db.Promo_Code where x.PC_id == Promocode select x).FirstOrDefault();
                    pm.orderrow = Orderdetail;
                    pm.PC_Code = p.PC_Code;
                    pm.PC_id = p.PC_id;
                    pm.PC_Uses = p.PC_Uses;
                    pm.Ticketdata = (from x in db.Tickets where x.E_Id == Eventid select x).ToList();
                    foreach (var item in pm.Ticketdata)
                    {
                        if (item.TicketTypeID == 2)
                        {
                            ttype = 1;
                        }
                    }
                    pm.ticketype = ttype;
                    pm.startdatesave = end_date.ToString();
                    if (p.PC_Startdatetype != null && p.PC_Startdatetype == "1")
                    {
                        pm.startdays = p.PC_Start;
                        string[] words = p.PC_Start.Split(' ');
                        if (end_date < now)
                        {
                            pm.PC_Start = now.ToString("MM-dd-yyyy hh:mm tt");
                        }
                        else
                        {
                            var datenew = end_date.AddDays(-double.Parse(words[0]));
                            datenew = datenew.AddHours(-double.Parse(words[2]));

                            datenew = datenew.AddMinutes(-double.Parse(words[4]));
                            pm.PC_Start = datenew.ToString("MM-dd-yyyy hh:mm tt");
                        }


                    }
                    else
                    {
                        dtzpcstart = new DateTimeWithZone(Convert.ToDateTime(p.P_Startdate), userTimeZone, true);
                        pm.PC_Start = dtzpcstart.LocalTime.ToString("MM-dd-yyyy hh:mm tt"); ;

                        pm.startdays = "0 Days 0 Hrs 0 Min";
                    }
                    if (p.Pc_Enddatetype != null && p.Pc_Enddatetype == "1")
                    {

                        pm.enddays = p.PC_End;
                        string[] words = p.PC_End.Split(' ');
                        var datenew = end_date.AddDays(-double.Parse(words[0]));
                        datenew = datenew.AddHours(-double.Parse(words[2]));

                        datenew = datenew.AddMinutes(-double.Parse(words[4]));
                        pm.PC_End = datenew.ToString("MM-dd-yyyy hh:mm tt"); ;
                    }
                    else
                    {
                        dtzpcend = new DateTimeWithZone(Convert.ToDateTime(p.P_Enddate), userTimeZone, true);
                        pm.PC_End = dtzpcend.LocalTime.ToString("MM-dd-yyyy hh:mm tt"); ;
                        pm.enddays = "0 Days 0 Hrs 0 Min";
                    }
                    pm.PC_Amount = p.PC_Amount != null ? p.PC_Amount : p.PC_Percentage;
                    pm.Amounttype = p.PC_Amount != null ? "A" : "P";
                    pm.PC_Percentage = p.PC_Percentage;
                    pm.Formtype = "E";
                    pm.PC_Apply = p.PC_Apply;

                    pm.PC_URL = baseurl + urldb + "?discount=" + p.PC_Code;
                    pm.PC_Startdatetype = p.PC_Startdatetype != null ? p.PC_Startdatetype : "0";
                    pm.Pc_Enddatetype = p.Pc_Enddatetype != null ? p.Pc_Enddatetype : "0";
                }

                return View(pm);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [Authorize]
        [HttpPost]
        public ActionResult CreatePromotionalCodes(HttpPostedFileBase file, Promo_Code model)
        {
            var msg = "";
            var containsspecial = 0;
            var Invalidrepeatcode = "";
            DateTimeWithZone dtz, dtzCreated;
            var timezoneid = DateTimeWithZone.Timezonedetail(model.PC_Eventid);
            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezoneid);
            dtzCreated = new DateTimeWithZone(DateTime.Now, userTimeZone, false);
            try
            {
                using (EventComboEntities db = new EventComboEntities())
                {
                    if (model.Formtype == "E")
                    {
                        if (model.PC_Code == null)
                        {
                            Promo_Code org = (from x in db.Promo_Code where x.PC_id == model.PC_id select x).FirstOrDefault();
                            org.PC_Eventid = model.PC_Eventid;
                            org.PC_Type = model.PC_Type;
                            // org.PC_Code = model.PC_Code;

                            if (model.Discount_Type == "A")
                            {
                                org.PC_Amount = model.PC_Amount;
                                org.PC_Percentage = null;
                            }
                            else
                            {
                                org.PC_Percentage = model.PC_Amount;
                                org.PC_Amount = null;
                            }


                            org.PC_Uses = model.PC_Uses;
                            org.PC_Startdatetype = model.PC_Startdatetype;
                            if (model.PC_Startdatetype == "1")
                            {
                                org.PC_Start = model.startdays;
                            }
                            else
                            {

                                dtz = new DateTimeWithZone(Convert.ToDateTime(model.PC_Start), userTimeZone);

                                org.P_Startdate = dtz.UniversalTime;
                            }
                            org.Pc_Enddatetype = model.Pc_Enddatetype;
                            if (model.Pc_Enddatetype == "1")
                            {
                                org.PC_End = model.enddays;
                            }
                            else
                            {
                                dtz = new DateTimeWithZone(Convert.ToDateTime(model.PC_End), userTimeZone);


                                org.P_Enddate = dtz.UniversalTime;
                            }

                            org.PC_Apply = model.PC_Apply;
                            org.PC_Eventid = model.PC_Eventid;
                            org.SavedDate = dtzCreated.UniversalTime;





                            try
                            {
                                int i = db.SaveChanges();
                                msg = "S";

                            }
                            catch (Exception ex)
                            {
                              logger.Error("Exception during request processing", ex);
                              msg = "N";
                            }
                        }
                        else
                        {

                            if (Regex.IsMatch(model.PC_Code.ToString(), "^[a-zA-Z0-9@_,-]+$"))
                            {
                                if (model.PC_Code.Length > 15)
                                {
                                    if (!string.IsNullOrEmpty(model.PC_Code))
                                    {
                                        if (Invalidrepeatcode == "")
                                        {
                                            Invalidrepeatcode += model.PC_Code;
                                        }
                                        else
                                        {
                                            Invalidrepeatcode += "," + model.PC_Code;
                                        }
                                    }

                                }
                                else
                                {

                                    var ifany = (from v in db.Promo_Code where v.PC_Code.Trim().ToLower() == model.PC_Code.Trim().ToLower() && v.PC_id != model.PC_id && v.PC_Eventid == model.PC_Eventid select v).Any();
                                    if (ifany)
                                    {
                                        if (!string.IsNullOrEmpty(model.PC_Code))
                                        {
                                            if (Invalidrepeatcode == "")
                                            {
                                                Invalidrepeatcode += model.PC_Code;
                                            }
                                            else
                                            {
                                                Invalidrepeatcode += "," + model.PC_Code;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        Promo_Code org = (from x in db.Promo_Code where x.PC_id == model.PC_id select x).FirstOrDefault();
                                        org.PC_Eventid = model.PC_Eventid;
                                        org.PC_Type = model.PC_Type;
                                        org.PC_Code = model.PC_Code;

                                        if (model.Discount_Type == "A")
                                        {
                                            org.PC_Amount = model.PC_Amount;
                                            org.PC_Percentage = null;
                                        }
                                        else
                                        {
                                            org.PC_Percentage = model.PC_Amount;
                                            org.PC_Amount = null;
                                        }


                                        org.PC_Uses = model.PC_Uses;
                                        org.PC_Startdatetype = model.PC_Startdatetype;
                                        if (model.PC_Startdatetype == "1")
                                        {
                                            org.PC_Start = model.startdays;
                                        }
                                        else
                                        {

                                            dtz = new DateTimeWithZone(Convert.ToDateTime(model.PC_Start), userTimeZone);

                                            org.P_Startdate = dtz.UniversalTime;
                                        }
                                        org.Pc_Enddatetype = model.Pc_Enddatetype;
                                        if (model.Pc_Enddatetype == "1")
                                        {
                                            org.PC_End = model.enddays;
                                        }
                                        else
                                        {
                                            dtz = new DateTimeWithZone(Convert.ToDateTime(model.PC_End), userTimeZone);


                                            org.P_Enddate = dtz.UniversalTime;
                                        }

                                        org.PC_Apply = model.PC_Apply;
                                        org.PC_Eventid = model.PC_Eventid;
                                        org.SavedDate = dtzCreated.UniversalTime;





                                        try
                                        {
                                            int i = db.SaveChanges();
                                            msg = "S";

                                        }
                                        catch (Exception ex)
                                        {
                                          logger.Error("Exception during request processing", ex);
                                          msg = "N";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                containsspecial++;
                                if (!string.IsNullOrEmpty(model.PC_Code))
                                {
                                    if (Invalidrepeatcode == "")
                                    {
                                        Invalidrepeatcode += model.PC_Code;
                                    }
                                    else
                                    {
                                        Invalidrepeatcode += "," + model.PC_Code;
                                    }
                                }

                            }
                        }
                    }
                    else
                    {


                        if (!string.IsNullOrWhiteSpace(model.PC_Code))
                        {
                            if (Regex.IsMatch(model.PC_Code.ToString(), "^[a-zA-Z0-9@_,-]+$"))
                            {
                                if (model.PC_Code.Length > 15)
                                {
                                    if (!string.IsNullOrEmpty(model.PC_Code))
                                    {
                                        if (Invalidrepeatcode == "")
                                        {
                                            Invalidrepeatcode += model.PC_Code;
                                        }
                                        else
                                        {
                                            Invalidrepeatcode += "," + model.PC_Code;
                                        }
                                    }

                                }
                                else {
                                    var ifany = (from v in db.Promo_Code where v.PC_Code.Trim().ToLower() == model.PC_Code.Trim().ToLower() && v.PC_Eventid == model.PC_Eventid select v).Any();
                                    if (ifany)
                                    {
                                        if (Invalidrepeatcode == "")
                                        {
                                            Invalidrepeatcode += model.PC_Code;
                                        }
                                        else
                                        {
                                            Invalidrepeatcode += "," + model.PC_Code;
                                        }
                                    }
                                    else
                                    {
                                        Promo_Code org = new Promo_Code();
                                        org.PC_Eventid = model.PC_Eventid;
                                        org.PC_Type = model.PC_Type;
                                        org.PC_Code = model.PC_Code;
                                        if (model.Discount_Type == "A")
                                        {
                                            org.PC_Amount = model.PC_Amount;
                                        }
                                        else
                                        {
                                            org.PC_Percentage = model.PC_Amount;

                                        }


                                        org.PC_Uses = model.PC_Uses;
                                        org.PC_Startdatetype = model.PC_Startdatetype;
                                        if (model.PC_Startdatetype == "1")
                                        {
                                            org.PC_Start = model.startdays;
                                        }
                                        else
                                        {
                                            dtz = new DateTimeWithZone(Convert.ToDateTime(model.PC_Start), userTimeZone);
                                            org.P_Startdate = dtz.UniversalTime;
                                        }

                                        org.Pc_Enddatetype = model.Pc_Enddatetype;
                                        if (model.Pc_Enddatetype == "1")
                                        {
                                            org.PC_End = model.enddays;
                                        }
                                        else
                                        {
                                            dtz = new DateTimeWithZone(Convert.ToDateTime(model.PC_End), userTimeZone);
                                            org.P_Enddate = dtz.UniversalTime;
                                        }
                                        org.PC_Apply = model.PC_Apply;
                                        org.PC_Eventid = model.PC_Eventid;

                                        org.SavedDate = dtzCreated.UniversalTime;



                                        db.Promo_Code.Add(org);
                                        try
                                        {
                                            int i = db.SaveChanges();
                                            msg = "S";

                                        }
                                        catch (Exception ex)
                                        {
                                            msg = "N";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (Invalidrepeatcode == "")
                                {
                                    Invalidrepeatcode += model.PC_Code;
                                }
                                else
                                {
                                    Invalidrepeatcode += "," + model.PC_Code;
                                }
                                containsspecial++;
                            }
                        }
                        else
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                var fileName = Path.GetFileName(file.FileName);
                                string ext = System.IO.Path.GetExtension(file.FileName);
                                string[] allowedExtenstions = new string[] { ".txt", ".csv" };
                                if (allowedExtenstions.Contains(ext))
                                {
                                    StreamReader csvreader = new StreamReader(file.InputStream);
                                    while (!csvreader.EndOfStream)
                                    {
                                        var line = csvreader.ReadLine();
                                        if (line.Contains(','))
                                        {
                                            var innerlines = line.Split(',');
                                            foreach (var item in innerlines)
                                            {
                                                if (Regex.IsMatch(item.ToString(), "^[a-zA-Z0-9@_,-]+$"))
                                                {
                                                    if (item.Length > 15)
                                                    {
                                                        if (!string.IsNullOrEmpty(item))
                                                        {
                                                            if (Invalidrepeatcode == "")
                                                            {
                                                                Invalidrepeatcode += item;
                                                            }
                                                            else
                                                            {
                                                                Invalidrepeatcode += "," + item;
                                                            }
                                                        }

                                                    }
                                                    else
                                                    {
                                                        var ifany = (from v in db.Promo_Code where v.PC_Code.Trim().ToLower() == item.Trim().ToLower() && v.PC_Eventid == model.PC_Eventid select v).Any();
                                                        if (ifany)
                                                        {
                                                            Invalidrepeatcode += item.Trim();
                                                        }
                                                        else
                                                        {
                                                            Promo_Code org = new Promo_Code();
                                                            org.PC_Eventid = model.PC_Eventid;
                                                            org.PC_Type = model.PC_Type;
                                                            org.PC_Code = item.Trim();
                                                            if (model.Discount_Type == "A")
                                                            {
                                                                org.PC_Amount = model.PC_Amount;
                                                            }
                                                            else
                                                            {
                                                                org.PC_Percentage = model.PC_Amount;

                                                            }


                                                            org.PC_Uses = model.PC_Uses;
                                                            org.PC_Startdatetype = model.PC_Startdatetype;
                                                            if (model.PC_Startdatetype == "1")
                                                            {
                                                                org.PC_Start = model.startdays;
                                                            }
                                                            else
                                                            {
                                                                dtz = new DateTimeWithZone(Convert.ToDateTime(model.PC_Start), userTimeZone);
                                                                org.P_Startdate = dtz.UniversalTime;
                                                            }
                                                            org.Pc_Enddatetype = model.Pc_Enddatetype;
                                                            if (model.Pc_Enddatetype == "1")
                                                            {
                                                                org.PC_End = model.enddays;
                                                            }
                                                            else
                                                            {
                                                                dtz = new DateTimeWithZone(Convert.ToDateTime(model.PC_End), userTimeZone);
                                                                org.P_Enddate = dtz.UniversalTime;
                                                            }
                                                            org.PC_Apply = model.PC_Apply;
                                                            org.PC_Eventid = model.PC_Eventid;
                                                            org.SavedDate = dtzCreated.UniversalTime;




                                                            db.Promo_Code.Add(org);
                                                            try
                                                            {
                                                                int i = db.SaveChanges();
                                                                msg = "S";

                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                msg = "N";
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (!string.IsNullOrEmpty(item))
                                                    {
                                                        if (Invalidrepeatcode == "")
                                                        {
                                                            Invalidrepeatcode += item.Trim();
                                                        }
                                                        else
                                                        {
                                                            Invalidrepeatcode += "," + item.Trim();
                                                        }
                                                    }

                                                    containsspecial++;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (Regex.IsMatch(line.ToString(), "^[a-zA-Z0-9@_,-]+$"))
                                            {

                                                if (line.Length > 15)
                                                {
                                                    if (!string.IsNullOrEmpty(line))
                                                    {
                                                        if (Invalidrepeatcode == "")
                                                        {
                                                            Invalidrepeatcode += line;
                                                        }
                                                        else
                                                        {
                                                            Invalidrepeatcode += "," + line;
                                                        }
                                                    }

                                                }
                                                else
                                                {
                                                    var ifany = (from v in db.Promo_Code where v.PC_Code.Trim().ToLower() == line.Trim().ToLower() && v.PC_Eventid == model.PC_Eventid select v).Any();
                                                    if (ifany)
                                                    {
                                                        if (!string.IsNullOrEmpty(line))
                                                        {
                                                            if (Invalidrepeatcode == "")
                                                            {
                                                                Invalidrepeatcode += line.Trim();
                                                            }
                                                            else
                                                            {
                                                                Invalidrepeatcode += "," + line.Trim();
                                                            }
                                                        }

                                                    }
                                                    else
                                                    {
                                                        Promo_Code org = new Promo_Code();
                                                        org.PC_Eventid = model.PC_Eventid;
                                                        org.PC_Type = model.PC_Type;
                                                        org.PC_Code = line;
                                                        if (model.Discount_Type == "A")
                                                        {
                                                            org.PC_Amount = model.PC_Amount;
                                                        }
                                                        else
                                                        {
                                                            org.PC_Percentage = model.PC_Amount;

                                                        }


                                                        org.PC_Uses = model.PC_Uses;
                                                        org.PC_Startdatetype = model.PC_Startdatetype;
                                                        if (model.PC_Startdatetype == "1")
                                                        {
                                                            org.PC_Start = model.startdays;
                                                        }
                                                        else
                                                        {
                                                            dtz = new DateTimeWithZone(Convert.ToDateTime(model.PC_Start), userTimeZone);
                                                            org.P_Startdate = dtz.UniversalTime;
                                                        }
                                                        org.Pc_Enddatetype = model.Pc_Enddatetype;
                                                        if (model.Pc_Enddatetype == "1")
                                                        {
                                                            org.PC_End = model.enddays;
                                                        }
                                                        else
                                                        {
                                                            dtz = new DateTimeWithZone(Convert.ToDateTime(model.PC_End), userTimeZone);

                                                            org.P_Enddate = dtz.UniversalTime;
                                                        }
                                                        org.PC_Apply = model.PC_Apply;
                                                        org.PC_Eventid = model.PC_Eventid;


                                                        org.SavedDate = dtzCreated.UniversalTime;


                                                        db.Promo_Code.Add(org);
                                                        try
                                                        {
                                                            int i = db.SaveChanges();
                                                            msg = "S";

                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            msg = "N";
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(line))
                                                {
                                                    if (Invalidrepeatcode == "")
                                                    {
                                                        Invalidrepeatcode += line.Trim();
                                                    }
                                                    else
                                                    {
                                                        Invalidrepeatcode += "," + line.Trim();
                                                    }
                                                }
                                                containsspecial++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
            }

            if (!string.IsNullOrWhiteSpace(Invalidrepeatcode))
            {
                TempData["Invalidcode"] = Invalidrepeatcode;

                return RedirectToAction("CreatePromotionalCodes", "ManageEvent", new { Eventlid = ValidationMessageController.GetParentEventId(model.PC_Eventid) });
            }
            else
            {
                return RedirectToAction("PromotionalCodes", "ManageEvent", new { Eventlid = ValidationMessageController.GetParentEventId(model.PC_Eventid) });


            }



        }

        public string SavePromocode(Promo_Code model)
        {
            var msg = "";
            if (Session["AppId"] != null)
            {
                using (EventComboEntities db = new EventComboEntities())
                {
                    Promo_Code org = new Promo_Code();
                    org.PC_Eventid = model.PC_Eventid;
                    org.PC_Type = model.PC_Type;
                    org.PC_Code = model.PC_Code;
                    org.PC_Amount = model.PC_Amount;
                    org.PC_Percentage = model.PC_Percentage;

                    org.PC_Uses = model.PC_Uses;
                    org.PC_Start = model.PC_Start;
                    org.PC_End = model.PC_End;
                    org.PC_Apply = model.PC_Apply;
                    org.PC_Eventid = model.PC_Eventid;





                    db.Promo_Code.Add(org);
                    try
                    {
                        int i = db.SaveChanges();
                        msg = "S";

                    }
                    catch (Exception ex)
                    {
                        msg = "N";
                    }
                }

                return "s";
            }
            else
            {
                return "o";

            }
        }

        public string datechange(string date, string type, string starttype, string typeofs)
        {
            string str = "";
            DateTime endadte = DateTime.Parse(starttype);

            if (typeofs == "s")
            {
                if (type == "m")
                {

                    string[] words = date.Split(' ');
                    if (words[0] == "0" && words[2] == "0" && words[4] == "0")
                    {
                        str = DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt");
                    }
                    else
                    {
                        var datenew = endadte.AddDays(-double.Parse(words[0]));
                        datenew = datenew.AddHours(-double.Parse(words[2]));

                        datenew = datenew.AddMinutes(-double.Parse(words[4]));
                        str = datenew.ToString("MM-dd-yyyy hh:mm:ss tt");
                    }
                }
                if (type == "d")
                {
                    DateTime startdate = DateTime.Parse(date);
                    if (endadte < startdate)
                    {
                        str = "0 Days " + "0 Hrs " + "0 Min";
                    }
                    else
                    {


                        TimeSpan span = (endadte - startdate);
                        str = span.Days.ToString() + " Days " + span.Hours.ToString() + " Hrs " + span.Minutes.ToString() + " Min";
                    }

                }
            }
            else
            {
                if (type == "m")
                {

                    string[] words = date.Split(' ');
                    if (words[0] == "0" && words[2] == "0" && words[4] == "0")
                    {
                        str = endadte.ToString("MM-dd-yyyy hh:mm:ss tt");
                    }
                    else
                    {
                        var datenew = endadte.AddDays(-double.Parse(words[0]));
                        datenew = datenew.AddHours(-double.Parse(words[2]));

                        datenew = datenew.AddMinutes(-double.Parse(words[4]));
                        str = datenew.ToString("MM-dd-yyyy hh:mm:ss tt");
                    }
                }
                if (type == "d")
                {
                    DateTime startdate = DateTime.Parse(date);
                    if (endadte < startdate)
                    {
                        str = "0 Days " + "0 Hrs " + "0 Min";
                    }
                    else
                    {


                        TimeSpan span = (endadte - startdate);
                        str = span.Days.ToString() + " Days " + span.Hours.ToString() + " Hrs " + span.Minutes.ToString() + " Min";
                    }

                }
            }
            return str;
        }
        public string DeletePromocode(long promocode)
        {
            try
            {
                var countpru = (from x in db.Ticket_Purchased_Detail where x.TPD_PromoCodeID == promocode select x).Count();
                var countlock = (from x in db.Ticket_Locked_Detail where x.TLD_PromoCodeId == promocode select x).Count();
                if (countpru > 0 || countlock > 0)
                {
                    return "N";
                }
                else
                {
                    Promo_Code prof = db.Promo_Code.Where(i => i.PC_id == promocode).FirstOrDefault();
                    db.Promo_Code.Remove(prof);
                    db.SaveChanges();
                    return "D";

                }






            }
            catch (Exception ex)
            {
                return "E";

            }

        }


        #region EmailInvitations

        [Authorize]
        public ActionResult CreateInvitations(long lId, long lEvtId, string strMode)
        {
            if (Session["AppId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            string LgUser = Session["AppId"].ToString();
            using (EventComboEntities db = new EventComboEntities())
            {
                AspNetUser aspuser = db.AspNetUsers.First(i => i.Id == LgUser);
                aspuser.LastLoginTime = System.DateTime.UtcNow;
                db.SaveChanges();
            }
            if (CommanClasses.CompareCurrentUser(lEvtId, Session["AppId"].ToString().Trim()) == false) return RedirectToAction("Index", "Home");


            //strMode If comes from Event Live Then need to set as 'E' otherwise set as 'C'
            Event_Email_Invitation objEEI = new Event_Email_Invitation();
            int iElistCnt = 0;
            string strPassword = "";
            string strDateTime = "";
            string strViewEvent = "";
            string strViewEventShort = "";
            string strOrgnizerUrl = "";
            int privateType = 0;
            Session["logo"] = "events";
            Session["Fromname"] = "Invitation";
            string strOrderText = "Attend";
            using (EventComboEntities objEnt = new EventComboEntities())
            {
                var vObj = (from EEI in objEnt.Event_Email_Invitation where EEI.I_Id == lId select EEI).FirstOrDefault();
                if (vObj != null)
                {
                    objEEI = vObj;


                    //Kannan Start
                    Event eventForTimeZone = objEnt.Events.First(i => i.EventID == objEEI.I_Event_Id);
                    int timeZoneID = Int32.Parse(eventForTimeZone.TimeZone);
                    TimeZoneDetail td = objEnt.TimeZoneDetails.First(i => i.TimeZone_Id == timeZoneID);
                    DateTimeWithZone dtz;
                    if (td != null)
                    {
                        TimeZoneInfo userTimeZone =
                        TimeZoneInfo.FindSystemTimeZoneById(td.TimeZone);
                        dtz = new DateTimeWithZone(Convert.ToDateTime(objEEI.I_ScheduleDate), userTimeZone, true);

                    }
                    else
                    {
                        TimeZoneInfo userTimeZone =
                        TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                        dtz = new DateTimeWithZone(Convert.ToDateTime(objEEI.I_ScheduleDate), userTimeZone, true);
                    }
                    //Kannan End



                    if (objEEI.I_ScheduleDate != null)
                        objEEI.I_ScheduleDate = dtz.LocalTime; // DateTime.SpecifyKind(Convert.ToDateTime(objEEI.I_ScheduleDate), DateTimeKind.Local);
                }
                else
                {
                  objEEI.Code = GetNewInvitationCode();
                }
                iElistCnt = (objEEI.Event_Email_List != null ? objEEI.Event_Email_List.Count() : 0);
                // lEvtId = (objEEI.I_Event_Id != null ? Convert.ToInt64(objEEI.I_Event_Id):0);

                if (lEvtId > 0)
                {
                    var vEvent = (from myEvent in objEnt.Events where myEvent.EventID == lEvtId select myEvent).FirstOrDefault();
                    if (vEvent != null)
                    {
                        strPassword = (vEvent.Private_Password != null ? vEvent.Private_Password.Trim() : "");
                        objEEI.EventTitle = vEvent.EventTitle;
                        var vDatetime = (from myDt in objEnt.EventVenues where myDt.EventID == lEvtId select myDt).FirstOrDefault();
                        if (vDatetime == null)
                        {
                            var vMultiDateTime = (from myDt in objEnt.MultipleEvents where myDt.EventID == lEvtId select myDt).FirstOrDefault();
                            if (vMultiDateTime != null)
                            {
                                //strDateTime = Convert.ToDateTime(vMultiDateTime.StartingFrom).ToString("ddd MMM dd, yyyy") + "," + vMultiDateTime.StartTime.ToString() + "(" + vMultiDateTime.Frequency + ")";
                                strDateTime = Convert.ToDateTime(vMultiDateTime.StartingFrom).ToString("ddd MMM dd, yyyy") + "," + vMultiDateTime.StartTime.ToString() + " - " + Convert.ToDateTime(vMultiDateTime.StartingTo).ToString("ddd MMM dd, yyyy") + "," + vMultiDateTime.EndTime.ToString();
                            }
                        }
                        else
                        {
                            strDateTime = Convert.ToDateTime(vDatetime.EventStartDate).ToString("ddd MMM dd, yyyy") + "," + vDatetime.EventStartTime.ToString();
                        }
                        if (vEvent.TimeZone != null)
                        {
                            int iTimezone = Convert.ToInt32(vEvent.TimeZone);
                            var vTimeZone = (from tmz in objEnt.TimeZoneDetails where tmz.TimeZone_Id == iTimezone select tmz.TimeZone_Name).FirstOrDefault();
                            objEEI.EventDate = strDateTime + "(" + vTimeZone + ")";
                        }
                        else
                        {
                            objEEI.EventDate = strDateTime;
                        }
                        var vOrgnizer = (from Ord in objEnt.Event_Orgnizer_Detail join orm in objEnt.Organizer_Master on Ord.OrganizerMaster_Id equals orm.Orgnizer_Id where Ord.Orgnizer_Event_Id == lEvtId select orm).FirstOrDefault();
                        objEEI.EventOrgnizer = (vOrgnizer != null ? vOrgnizer.Orgnizer_Name : "");
                        var vAddress = (from eAdd in objEnt.Addresses where eAdd.EventId == lEvtId select eAdd).FirstOrDefault();
                        if (vAddress != null)
                        {
                            if (vAddress.ConsolidateAddress != null && vAddress.ConsolidateAddress.Trim() != "")
                                objEEI.EventAddress = vAddress.ConsolidateAddress;
                            else
                                objEEI.EventAddress = vAddress.VenueName + " " + vAddress.Address1 + " " + vAddress.Address2 + " " + vAddress.City + " " + vAddress.State + " " + vAddress.Zip;

                            objEEI.EventLat = vAddress.Latitude;
                            objEEI.EventLong = vAddress.Longitude;
                        }

                        CreateEventController objEv = new CreateEventController();
                        strOrderText = objEv.GetOrderText(lEvtId);

                        var url = Request.Url;
                        var baseurl = url.GetLeftPart(UriPartial.Authority);


                        strViewEvent = baseurl + Url.Action("ViewEvent", "EventManagement", new 
                          { 
                            strEventDs = System.Text.RegularExpressions.Regex.Replace(vEvent.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), 
                            strEventId = ValidationMessageController.GetParentEventId(lEvtId).ToString(),
                            InviteId = objEEI.Code
                          });
                        strViewEventShort = baseurl + Url.Action("ViewEvent", "EventManagement", new
                        {
                          strEventDs = System.Text.RegularExpressions.Regex.Replace(vEvent.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""),
                          strEventId = ValidationMessageController.GetParentEventId(lEvtId).ToString()
                        });
                        strOrgnizerUrl = "";
                        if (vOrgnizer != null)
                            strOrgnizerUrl = baseurl + Url.Action("Index", "OrganizerInfo", new { id = vOrgnizer.Orgnizer_Id, eventid = lEvtId });

                    }

                    string strImageUrl = String.Empty;
                    if ((vEvent.ECImageId ?? 0) > 0)
                    {
                      var img = _iservice.GetImageById(vEvent.ECImageId ?? 0);
                      if ((img != null) && (System.IO.File.Exists(img.FilePath)))
                        strImageUrl = img.ImagePath;
                    }
                    objEEI.EventImg = String.IsNullOrEmpty(strImageUrl) ? "/Images/default_event_image.jpg" : strImageUrl;

                    if (!String.IsNullOrEmpty(vEvent.EventPrivacy) && (vEvent.EventPrivacy.ToUpper() == "PRIVATE"))
                    {
                      if (!String.IsNullOrEmpty(vEvent.Private_Password))
                        privateType = 2;
                      else
                        privateType = 1;
                      if (vEvent.Private_GuestOnly == "Y")
                        privateType += 2;
                    }
                }
            }

            var vurl = Request.Url;
            var vbaseurl = vurl.GetLeftPart(UriPartial.Authority);
            TempData["baseurl"] = vbaseurl;
            TempData["OrderText"] = strOrderText;
            TempData["OrgnizerUrl"] = strOrgnizerUrl;
            TempData["ViewEventUrl"] = strViewEventShort;
            TempData["InviteUrl"] = strViewEvent;
            TempData["Lat"] = (objEEI.EventLat != null ? objEEI.EventLat.Trim() : "");
            TempData["Long"] = (objEEI.EventLong != null ? objEEI.EventLong.Trim() : "");
            TempData["lId"] = lId;
            TempData["EmailListCount"] = iElistCnt;
            TempData["Eventid"] = lEvtId;
            TempData["PPassword"] = strPassword;
            TempData["EventIMode"] = strMode;
            TempData["InviteId"] = objEEI.Code;
            TempData["PrivacyType"] = privateType;
            ViewBag.Title = "Create Email Invite for " + objEEI.EventTitle + " | Eventcombo";
            return View(objEEI);
        }

        public void SendHtmlFormattedEmail(EmailContent model)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(model.From, model.Fromname);


            mailMessage.Subject = model.Subject;
            mailMessage.Body = model.Body;
            if (!string.IsNullOrEmpty(model.Cc))
            {
                mailMessage.CC.Add(model.Cc);
            }
            if (!string.IsNullOrEmpty(model.Bcc))
            {
                mailMessage.Bcc.Add(model.Bcc);
            }

            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(new MailAddress(model.To));

            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["Host"];
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            string s = ConfigurationManager.AppSettings["UserName"];
            if (!String.IsNullOrEmpty(s))
            {
                NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
                NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
            }
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            smtp.Send(mailMessage);

        }

        [HttpPost]
        public long SaveInvitation(Event_Email_Invitation Model)
        {
            long lResult = 0;
            try
            {
                string strUserId = (Session["AppId"] != null ? Session["AppId"].ToString() : "");
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    if (Model.I_Id <= 0)
                    {
                        Event_Email_Invitation objEInt = new Event_Email_Invitation();
                        objEInt.I_SenderName = Model.I_SenderName;
                        objEInt.I_SubjectLine = Model.I_SubjectLine;
                        objEInt.Code = Model.Code;
                        objEInt.I_Event_Id = ValidationMessageController.GetParentEventId((Model.I_Event_Id != null ? Convert.ToInt64(Model.I_Event_Id) : 0));


                        //Kannan Start
                        Event eventForTimeZone = objEnt.Events.First(i => i.EventID == Model.I_Event_Id);
                        int timeZoneID = Int32.Parse(eventForTimeZone.TimeZone);

                        TimeZoneDetail td = objEnt.TimeZoneDetails.First(i => i.TimeZone_Id == timeZoneID);
                        DateTimeWithZone dtz;
                        DateTimeWithZone dtzCreated;
                        if (td != null)
                        {
                            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td.TimeZone);
                            dtz = new DateTimeWithZone(Convert.ToDateTime(Model.I_ScheduleDate), userTimeZone);
                            dtzCreated = new DateTimeWithZone(DateTime.Now, userTimeZone, false);

                        }
                        else
                        {
                            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                            dtz = new DateTimeWithZone(Convert.ToDateTime(Model.I_ScheduleDate), userTimeZone);
                            dtzCreated = new DateTimeWithZone(DateTime.Now, userTimeZone, false);
                        }
                        //Kannan End

                        objEInt.I_EmailContent = Model.I_EmailContent;
                        if (Model.I_Mode == "N")
                        {
                            objEInt.I_ScheduleDate = DateTime.Now;
                        }
                        else {
                            if (Model.I_ScheduleDate != null)
                                objEInt.I_ScheduleDate = dtz.UniversalTime; // DateTime.SpecifyKind(Convert.ToDateTime(Model.I_ScheduleDate), DateTimeKind.Utc);
                            else
                                objEInt.I_ScheduleDate = Model.I_ScheduleDate;
                        }


                        objEInt.I_EditableContent = Model.I_EditableContent;
                        objEInt.I_Mode = Model.I_Mode;
                        objEInt.I_CreateDate = dtzCreated.UniversalTime; //DateTime.Now;
                        if (Model.EmailList != null)
                        {
                            Event_Email_List objEList = new Event_Email_List();
                            foreach (Event_Email_List objEv in Model.EmailList)
                            {
                                objEList = new Event_Email_List();
                                objEList.L_I_Id = objEInt.I_Id;
                                objEList.L_EmailId = objEv.L_EmailId;
                                objEnt.Event_Email_List.Add(objEList);
                            }
                        }
                        objEnt.Event_Email_Invitation.Add(objEInt);
                        objEnt.SaveChanges();
                        lResult = objEInt.I_Id;
                    }
                    else
                    {
                        Event_Email_Invitation objEInt = objEnt.Event_Email_Invitation.First(i => i.I_Id == Model.I_Id);
                        objEInt.I_SenderName = Model.I_SenderName;
                        objEInt.I_SubjectLine = Model.I_SubjectLine;
                        objEInt.I_Event_Id = ValidationMessageController.GetParentEventId((Model.I_Event_Id != null ? Convert.ToInt64(Model.I_Event_Id) : 0));
                        objEInt.I_EmailContent = Model.I_EmailContent;
                        //Kannan Start
                        Event eventForTimeZone = objEnt.Events.First(i => i.EventID == Model.I_Event_Id);
                        int timeZoneID = Int32.Parse(eventForTimeZone.TimeZone);
                        TimeZoneDetail td = objEnt.TimeZoneDetails.First(i => i.TimeZone_Id == timeZoneID);
                        DateTimeWithZone dtz;
                        DateTimeWithZone dtzCreated;
                        if (td != null)
                        {
                            TimeZoneInfo userTimeZone =
                            TimeZoneInfo.FindSystemTimeZoneById(td.TimeZone);
                            dtz = new DateTimeWithZone(Convert.ToDateTime(Model.I_ScheduleDate), userTimeZone);
                            dtzCreated = new DateTimeWithZone(DateTime.Now, userTimeZone, false);
                        }
                        else
                        {
                            TimeZoneInfo userTimeZone =
                            TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                            dtz = new DateTimeWithZone(Convert.ToDateTime(Model.I_ScheduleDate), userTimeZone);
                            dtzCreated = new DateTimeWithZone(DateTime.Now, userTimeZone, false);
                        }
                        //Kannan End


                        if (Model.I_ScheduleDate != null)
                            objEInt.I_ScheduleDate = dtz.UniversalTime; // DateTime.SpecifyKind(Convert.ToDateTime(Model.I_ScheduleDate), DateTimeKind.Utc);
                        else
                            objEInt.I_ScheduleDate = Model.I_ScheduleDate;
                        objEInt.I_EditableContent = Model.I_EditableContent;
                        objEInt.I_Mode = Model.I_Mode;
                        objEInt.I_ModifyDate = dtzCreated.UniversalTime; //DateTime.Now;
                        objEnt.Event_Email_List.RemoveRange(objEnt.Event_Email_List.Where(x => x.L_I_Id == Model.I_Id));
                        if (Model.EmailList != null)
                        {
                            Event_Email_List objEList = new Event_Email_List();
                            foreach (Event_Email_List objEv in Model.EmailList)
                            {
                                objEList = new Event_Email_List();
                                objEList.L_I_Id = Model.I_Id;
                                objEList.L_EmailId = objEv.L_EmailId.Trim();
                                objEnt.Event_Email_List.Add(objEList);
                            }
                        }
                        objEnt.SaveChanges();
                        lResult = objEInt.I_Id;

                    }
                    if (Model.I_Mode.Trim() == "N")
                    {
                        EmailContent objEC = new EmailContent();
                        foreach (Event_Email_List objEv in Model.EmailList)
                        {
                            objEC = new EmailContent();
                            objEC.To = objEv.L_EmailId.Trim();
                            objEC.From = ConfigurationManager.AppSettings.Get("DefaultEmail"); //ConfigurationManager.AppSettings.Get("UserName");
                            objEC.Body = Model.I_EmailContent;
                            objEC.Subject = Model.I_SubjectLine;
                            objEC.Cc = "";
                            objEC.Bcc = "";
                            objEC.Fromname = Model.I_SenderName;
                            SendHtmlFormattedEmail(objEC);
                        }
                    }
                }
                return lResult;
            }
            catch (Exception ex)
            {
                return lResult;
            }

        }

        public string CopyInvitation(long l_Id)
        {
            try
            {
                string strUserId = (Session["AppId"] != null ? Session["AppId"].ToString() : "");
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    Event_Email_Invitation objEInt = new Event_Email_Invitation();
                    var Model = (from Int in objEnt.Event_Email_Invitation where Int.I_Id == l_Id select Int).FirstOrDefault();
                    if (Model != null)
                    {
                        objEInt.I_SenderName = Model.I_SenderName;
                        objEInt.I_SubjectLine = Model.I_SubjectLine;
                        objEInt.I_Event_Id = Model.I_Event_Id;
                        objEInt.I_EmailContent = Model.I_EmailContent;
                        objEInt.Code = GetNewInvitationCode();
                        if (Model.I_ScheduleDate != null)
                            objEInt.I_ScheduleDate = DateTime.SpecifyKind(Convert.ToDateTime(Model.I_ScheduleDate), DateTimeKind.Utc);
                        else
                            objEInt.I_ScheduleDate = Model.I_ScheduleDate;

                        objEInt.I_EditableContent = Model.I_EditableContent;
                        objEInt.I_Mode = Model.I_Mode;
                        objEInt.I_CreateDate = DateTime.Now;
                        if (Model.EmailList != null)
                        {
                            Event_Email_List objEList = new Event_Email_List();
                            var vEmailList = (from Int in objEnt.Event_Email_List where Int.L_I_Id == l_Id select Int).ToList();
                            foreach (Event_Email_List objEv in vEmailList)
                            {
                                objEList = new Event_Email_List();
                                objEList.L_I_Id = objEInt.I_Id;
                                objEList.L_EmailId = objEv.L_EmailId;
                                objEnt.Event_Email_List.Add(objEList);
                            }
                        }
                        objEnt.Event_Email_Invitation.Add(objEInt);
                        objEnt.SaveChanges();
                        return "Y";
                    }
                }
                return "Y";
            }
            catch (Exception ex)
            {
                return "E";
            }

        }

        public PartialViewResult Sidenav(long eventid, string CurrentItem)
        {

            Sidenav ss = new ViewModels.Sidenav();
            EventCreation cms = new EventCreation();

            var Eventdetails = cms.GetEventdetail(eventid);
            var Discountcode = (from x in db.Promo_Code where x.PC_Eventid == eventid select x).Count();
            ss.Eventtitle = Eventdetails.EventTitle;
            ss.EventId = eventid;
            ss.DiscountCode = Discountcode;
            ss.CurrentItem = CurrentItem;
            return PartialView("SideNavPartialView", ss);
        }

    }
    #endregion


}
