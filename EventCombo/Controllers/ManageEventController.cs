using EventCombo.Models;
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

namespace EventCombo.Controllers
{

    [OutputCacheAttribute(VaryByParam = "None", Duration = 0, NoStore = true)]
    public class ManageEventController : Controller
    {
        // GET: ManageEvent
        EventComboEntities db = new EventComboEntities();

        [Authorize]
        public ActionResult Index(long Eventid, string type)
        {
            var TopAddress = ""; var Topvenue = "";
            string sDate_new = "", eDate_new = "";
            string startday = "", endday = "", starttime = "", endtime = "";
            ManageEvent Mevent = new ManageEvent();
            //Getting event details
            CreateEventController createevent = new CreateEventController();
            createevent.ControllerContext = new ControllerContext(this.Request.RequestContext, createevent);
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
            DateTime ENDATE = new DateTime();
            var chkdate = (from ev in db.MultipleEvents where ev.EventID == Eventid select ev).Any();
            if (chkdate)
            {
                var evschdetails = (from ev in db.MultipleEvents where ev.EventID == Eventid select ev).FirstOrDefault();
                var startdate = (evschdetails.StartingFrom);
                DateTime sDate = new DateTime();
                sDate = DateTime.Parse(startdate);
                startday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(sDate).ToString();

                sDate_new = sDate.ToString("MMM dd, yyyy");
                var enddate = evschdetails.StartingTo;

                DateTime eDate = new DateTime();
                eDate = DateTime.Parse(enddate);
                endday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(eDate).ToString();
                eDate_new = eDate.ToString("MMM dd, yyyy");

                starttime = evschdetails.StartTime.ToUpper();
                endtime = evschdetails.EndTime.ToUpper();
                ENDATE = DateTime.Parse(enddate + " " + endtime);

            }
            else
            {
                var evschdetails = (from ev in db.EventVenues where ev.EventID == Eventid select ev).FirstOrDefault();
                if (evschdetails != null)
                {
                    var startdate = (evschdetails.EventStartDate);
                    if (startdate != null)
                    {
                        DateTime sDate = new DateTime();
                        sDate = DateTime.Parse(startdate.ToString());
                        startday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(sDate).ToString();
                        sDate_new = sDate.ToString("MMM dd,yyyy");
                    }
                    var enddate = evschdetails.EventEndDate;
                    if (enddate != null)
                    {
                        DateTime eDate = new DateTime();
                        eDate = DateTime.Parse(enddate.ToString());
                        endday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(eDate).ToString();
                        eDate_new = eDate.ToString("MMM dd,yyyy");
                    }

                    starttime = evschdetails.EventStartTime.ToString();
                    endtime = evschdetails.EventEndTime.ToString();
                    ENDATE = DateTime.Parse(enddate + " " + endtime);
                }
            }
            var timezone = "";
            DateTime dateTime = new DateTime();
            var Timezonedetail = (from ev in db.TimeZoneDetails where ev.TimeZone_Id.ToString() == Edetails.TimeZone select ev).FirstOrDefault();
            if (Timezonedetail != null)
            {
                timezone = Timezonedetail.TimeZone;
                TimeZoneInfo timeZoneInfo;


                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
                //Timezone value

            }
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
            var urldb = GetEventURL(Eventid);
            if (urldb.Contains("/"))
            {
                Mevent.url = baseurl + urldb;
            }
            else
            {
                Mevent.url = baseurl + "/ev/" + urldb;
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
            ValidationMessageController vmc = new ValidationMessageController();
            vmc.ControllerContext = new ControllerContext(this.Request.RequestContext, vmc);

            if (type == "P")
            {
                TempData["Success"] = vmc.Index("ManageEvent", "MEPublisheventSucc");
            }
            else
            {
                TempData["Success"] = null;
            }
            OrderAttendees CO = new OrderAttendees();
            Mevent.Order = (from o in db.Order_Detail_T
                            join p in db.Ticket_Purchased_Detail on o.O_Order_Id equals p.TPD_Order_Id
                            join a in db.Profiles on p.TPD_User_Id equals a.UserID
                            where p.TPD_Event_Id == Eventid
                            group new
                            {
                                OrderId = o.O_Order_Id,
                                Price = o.O_TotalAmount,
                                Qty = p.TPD_Purchased_Qty,
                                Name = a.FirstName + " " + a.LastName,
                                Date = o.O_OrderDateTime
                            }
                            by new
                            {
                                o.O_Order_Id,
                                o.O_TotalAmount,
                                p.TPD_Purchased_Qty,
                                a.FirstName,
                                a.LastName,
                                o.O_OrderDateTime
                            } into gc
                            orderby gc.Key.O_Order_Id descending
                            select new OrderAttendees()
                            {
                                OrderId = gc.Key.O_Order_Id,
                                Amount = gc.Key.O_TotalAmount.ToString(),
                                Qty = gc.ToList().Sum(a => a.Qty).ToString(),
                                Name = gc.Key.FirstName + " " + gc.Key.LastName,
                                Date = gc.Key.O_OrderDateTime.ToString()
                            }).Take(3).ToList();



            Mevent.Attendess = (from o in db.Order_Detail_T
                                join p in db.Ticket_Purchased_Detail on o.O_Order_Id equals p.TPD_Order_Id
                                join a in db.Profiles on p.TPD_User_Id equals a.UserID
                                where p.TPD_Event_Id == Eventid
                                group new
                                {
                                    OrderId = o.O_Order_Id,
                                    Price = o.O_TotalAmount,
                                    Qty = p.TPD_Purchased_Qty,
                                    Name = a.FirstName + " " + a.LastName,
                                    Date = o.O_OrderDateTime
                                }
                                by new
                                {
                                    o.O_Order_Id,
                                    o.O_TotalAmount,
                                    p.TPD_Purchased_Qty,
                                    a.FirstName,
                                    a.LastName,
                                    o.O_OrderDateTime
                                } into gc
                                orderby gc.Key.O_Order_Id descending, gc.Key.FirstName ascending
                                select new OrderAttendees()
                                {
                                    OrderId = gc.Key.O_Order_Id,
                                    Amount = gc.Key.O_TotalAmount.ToString(),
                                    Qty = gc.ToList().Sum(a => a.Qty).ToString(),
                                    Name = gc.Key.FirstName + " " + gc.Key.LastName,
                                    Date = gc.Key.O_OrderDateTime.ToString()
                                }).Take(3).ToList();


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
            TempData["EventHits"] = strDates.ToString();
            TempData["SaleQty"] = strSaleQty.ToString();
            TempData["TicketSalePer"] = GetSalePer(Eventid);

            TempData["RemQty"] = GetQuantity(Eventid, "R");
            TempData["TotalQty"] = GetQuantity(Eventid, "T");
            TempData["PaidTicket"] = GetTicketQtyPer(Eventid, "P");
            TempData["FreeTicket"] = GetTicketQtyPer(Eventid, "F");
            TempData["EventUrl"] = GetEventURL(Eventid);

            TempData["ForSale"] = GetSaleAmount(Eventid, "FORSALE");
            TempData["NETSale"] = GetSaleAmount(Eventid, "NETSALE");

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
        public ActionResult EmailInvitations(long EventId, int? page)
        {
            StringBuilder strResult = new StringBuilder();
            using (EventComboEntities objEnt = new EventComboEntities())
            {
                
                var invitations = from invite_list in objEnt.Event_Email_List
                                  group invite_list by invite_list.L_I_Id into result1
                                  join invites in objEnt.Event_Email_Invitation on result1.FirstOrDefault().L_I_Id equals invites.I_Id
                                  orderby invites.I_ModifyDate
                                  select new EmailInvitation
                                  {
                                      EventID = invites.I_Event_Id,
                                      Subject = invites.I_SubjectLine,
                                      SendOn = invites.I_ScheduleDate,
                                      CreatedOn = invites.I_CreateDate,
                                      NoOfRecipients = result1.Count()
                                  };



                int pageSize = 9;
                int pageNumber = (page ?? 1);
                return View(invitations.ToPagedList(pageNumber, pageSize));
            }

            return View();
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
                strResult.Append("<table id='tbSaleTicket' class='table ft_black table - bordered mb0'>");
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
                        strResult.Append("<td>"); strResult.Append(obj.Price); strResult.Append("</td>");
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

                    strHideUntil = (obj.Hide_Untill_Date != null ? obj.Hide_Untill_Date.ToString() : "");
                    strHideUntilTime = (obj.Hide_Untill_Time != null ? obj.Hide_Untill_Time.ToString() : "");

                    strHideAfter = (obj.Hide_After_Date != null ? obj.Hide_After_Date.ToString() : "");
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
                    strResult.Append("<td>"); strResult.Append((obj.Sale_End_Date != null ? obj.Sale_End_Date.ToString() : "")); strResult.Append("</td>");
                    strResult.Append("<td>"); strResult.Append(""); strResult.Append("</td>");
                    strResult.Append("</tr>");
                }
                strResult.Append("</tbody>");
                strResult.Append("</table>");
            }
            return strResult.ToString();
        }

        public string GetSaleAmount(long lEventId, string strAmtType)
        {
            string strResult = "";
            CultureInfo us = new CultureInfo("en-US");
            using (EventComboEntities objEnt = new EventComboEntities())
            {
                var vTotalAmt = (from myRow in objEnt.Ticket_Purchased_Detail
                                 where myRow.TPD_Event_Id == lEventId
                                 select myRow.TPD_Amount).Sum();

                if (strAmtType == "FORSALE")
                {
                    strResult = Math.Round((vTotalAmt == null ? 0 : Convert.ToDouble(vTotalAmt)), 2).ToString("N", us);
                }
                else if (strAmtType == "NETSALE")
                {
                    var vEcFee = (from myRow in objEnt.Ticket_Purchased_Detail
                                  where myRow.TPD_Event_Id == lEventId
                                  select myRow.TPD_EC_Fee).Sum();

                    double dResult = Math.Round((vTotalAmt == null ? 0 : Convert.ToDouble(vTotalAmt)) - (vEcFee == null ? 0 : Convert.ToDouble(vEcFee)), 2);
                    strResult = dResult.ToString("N", us);
                }
            }
            return strResult;
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
                        strResult = @Url.Action("ViewEvent", "ViewEvent", new { strEventDs = Regex.Replace(vEvent.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = vEvent.EventID.ToString() });
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
                        var vtotalqty = objEnt.Database.SqlQuery<long>("SELECT sum(TQD_Quantity) TQty From (Ticket_Quantity_Detail TQD LEFT JOIN  Ticket T on TQD.TQD_Ticket_Id = T.T_Id)  where TQD_Event_Id = " + eventId + " and T.TicketTypeID in (1,2)").FirstOrDefault();
                        var vremqty = objEnt.Database.SqlQuery<long>("SELECT sum(TQD_Remaining_Quantity) TRQty From (Ticket_Quantity_Detail TQD LEFT JOIN  Ticket T on TQD.TQD_Ticket_Id = T.T_Id)  where TQD_Event_Id = " + eventId + " and T.TicketTypeID in (1,2)").FirstOrDefault();
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
                        var vtotalty = (from myRow in objEnt.Ticket_Quantity_Detail where myRow.TQD_Event_Id == eventId select myRow.TQD_Quantity).Sum();
                        lResult = (vtotalty != null ? Convert.ToInt64(vtotalty) : 0);
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
<<<<<<< HEAD
                    var ticketid = (from v in db.Tickets where v.E_Id == eventId select v.T_Id).ToList();
                    string joined = string.Join(",", ticketid.ToArray());
=======
>>>>>>> 76bd1279bada0011e46eaaa1f2faeaac4a094057

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
                    //vEvent.EventTitle = strEventTitle;
                    //vEvent.EventStatus = "Save";
                    //vEvent.EventID = 0;
                    //objEnt.Events.Add(vEvent);
                    if (strEventTitle.Trim().Equals("")) strEventTitle = vEvent.EventTitle;
                    Event ObjEC = new Event();
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
                    ObjEC.CreateDate = DateTime.Now;
                    ObjEC.EventStatus = "Save";
                    objEnt.Events.Add(ObjEC);

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
                        }
                        //foreach (Address objAdd in vAddress)
                        //{
                        //    objAdd.EventId = ObjEC.EventID;
                        //    objEnt.Addresses.Add(objAdd);
                        //}
                    }
                    var vEventVenue = (from myEnt in objEnt.EventVenues where myEnt.EventID == Eventid select myEnt).ToList();
                    if (vEventVenue != null)
                    {
                        EventVenue objEVenue = new EventVenue();
                        foreach (EventVenue objEv in vEventVenue)
                        {
                            objEVenue = new EventVenue();
                            objEVenue.EventID = ObjEC.EventID;
                            objEVenue.EventStartDate = objEv.EventStartDate;
                            objEVenue.EventEndDate = objEv.EventEndDate;
                            objEVenue.EventStartTime = objEv.EventStartTime;
                            objEVenue.EventEndTime = objEv.EventEndTime;
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
                    CreateEventController objCE = new CreateEventController();
                    objCE.ControllerContext = new ControllerContext(this.Request.RequestContext, objCE);
                    objCE.PublishEvent(Eventid);
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
        public ActionResult PromotionalCodes(long Eventid, string strPageIndex = "page", string searchquery = "")
        {
            if (Session["AppId"] != null)
            {
                showPromocode sc = new showPromocode();
                sc.Eventid = Eventid;
                int pageSize = 20;
                int pageIndex = 1;
                if (strPageIndex != null && strPageIndex != string.Empty && strPageIndex != "page")
                    pageIndex = Convert.ToInt32(strPageIndex);
                List<Promocode> ls = new List<Promocode>();
                CreateEventController cms = new CreateEventController();
                var Eventdetail = cms.GetEventdetail(Eventid);
                var Discountcode = (from x in db.Promo_Code where x.PC_Eventid == Eventid select x).Count();
                sc.Eventtitle = Eventdetail.EventTitle;
                if (!string.IsNullOrWhiteSpace(searchquery))
                {
                    ls = (from x in db.Promo_Code
                          orderby x.SavedDate descending
                          where x.PC_Eventid == Eventid && x.PC_Code.Contains(searchquery)
                          select new Promocode
                          {
                              code = x.PC_Code,
                              Amount = (x.PC_Amount != null || x.PC_Percentage != null) ? (x.PC_Amount != null ? "$" + x.PC_Amount.ToString() : x.PC_Percentage + "%") : "-",
                              Start = (x.PC_Startdatetype != null && x.PC_Startdatetype == "1") ? x.PC_Start + " before event" : SqlFunctions.DateDiff("s", x.PC_Start, DateTime.Now) == 0 ? "Started" : x.PC_Start,
                              End = (x.Pc_Enddatetype != null && x.Pc_Enddatetype == "1") ? x.PC_End + " before event" : x.PC_End,
                              Limit = x.PC_Uses != null ? x.PC_Uses.ToString() : "No Limit",
                              PCID = x.PC_id,



                          }).ToList();

                }
                else
                {
                    ls = (from x in db.Promo_Code
                          orderby x.SavedDate descending
                          where x.PC_Eventid == Eventid
                          select new Promocode
                          {
                              code = x.PC_Code,
                              Amount = (x.PC_Amount != null || x.PC_Percentage != null) ? (x.PC_Amount != null ? "$" + x.PC_Amount.ToString() : x.PC_Percentage + "%") : "-",
                              Start = (x.PC_Startdatetype != null && x.PC_Startdatetype == "1") ? x.PC_Start + " before event" : SqlFunctions.DateDiff("s", x.PC_Start, DateTime.Now) == 0 ? "Started" : x.PC_Start,
                              End = (x.Pc_Enddatetype != null && x.Pc_Enddatetype == "1") ? x.PC_End + " before event" : x.PC_End,
                              Limit = x.PC_Uses != null ? x.PC_Uses : "No Limit",
                              PCID = x.PC_id


                          }).ToList();

                }
                double dPageCount = ls.Count;
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
        public ActionResult CreatePromotionalCodes(long Eventid, long Promocode = 0)
        {
            if (Session["AppId"] != null)
            {
                Promo_Code pm = new Promo_Code();
                var ttype = 0;
                DateTime end_date = new DateTime();
                CreateEventController cms = new CreateEventController();
                string startdate = "", enddate = "";
                var Eventdetail = cms.GetEventdetail(Eventid);
                pm.Eventitle = Eventdetail.EventTitle;
                pm.Eventitle = Eventdetail.EventTitle;
                var url = Request.Url;
                var baseurl = url.GetLeftPart(UriPartial.Authority);
                var urldb = GetEventURL(Eventid);

                var Discountcode = (from x in db.Promo_Code where x.PC_Eventid == Eventid select x).Count();
                pm.discountcode = Discountcode;
                pm.PC_Eventid = Eventid;
                var startdatesave = "";
                var singleevnt = (from x in db.EventVenues where x.EventID == Eventid select x).Any();
                if (singleevnt)
                {
                    var y = (from x in db.EventVenues where x.EventID == Eventid select x).FirstOrDefault();
                    end_date = DateTime.Parse(y.EventStartDate + " " + y.EventEndTime);
                    startdatesave = DateTime.Parse(y.EventStartDate + " " + y.EventEndTime).ToString("MM-dd-yyyy hh:mm:ss tt");

                }
                else
                {
                    var y = (from x in db.MultipleEvents where x.EventID == Eventid select x).FirstOrDefault();
                    end_date = DateTime.Parse(y.StartingFrom + " " + y.StartTime);
                    startdatesave = DateTime.Parse(y.StartingFrom + " " + y.StartTime).ToString("MM-dd-yyyy hh:mm:ss tt");

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

                    startdate = DateTime.UtcNow.ToString("MM-dd-yyyy hh:mm:ss tt");


                    pm.PC_Start = startdate;
                    pm.PC_End = startdatesave;
                    pm.startdatesave = end_date.ToString();
                    pm.PC_id = 0;
                    pm.PC_URL = baseurl + urldb + "?discount=Example";
                    pm.Pc_Enddatetype = "0";
                    pm.PC_Startdatetype = "0";
                    if (end_date < DateTime.Now)
                    {
                        pm.startdays = "0 Days 0 Hrs 0 Min";

                    }
                    else
                    {
                        TimeSpan span = (end_date - DateTime.Now);
                        pm.startdays = span.Days.ToString() + " Days " + span.Hours.ToString() + " Hrs " + span.Minutes.ToString() + " Min";

                    }
                    pm.enddays = "0 Days 0 Hrs 0 Min";
                }
                else
                {
                    var p = (from x in db.Promo_Code where x.PC_id == Promocode select x).FirstOrDefault();
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
                        if (end_date < DateTime.Now)
                        {
                            pm.PC_Start = DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt");
                        }
                        else
                        {
                            var datenew = end_date.AddDays(-double.Parse(words[0]));
                            datenew = datenew.AddHours(-double.Parse(words[2]));

                            datenew = datenew.AddMinutes(-double.Parse(words[4]));
                            pm.PC_Start = datenew.ToString("MM-dd-yyyy hh:mm:ss tt");
                        }


                    }
                    else
                    {
                        pm.PC_Start = p.PC_Start;

                        pm.startdays = "0 Days 0 Hrs 0 Min";
                    }
                    if (p.Pc_Enddatetype != null && p.Pc_Enddatetype == "1")
                    {

                        pm.enddays = p.PC_End;
                        string[] words = p.PC_End.Split(' ');
                        var datenew = end_date.AddDays(-double.Parse(words[0]));
                        datenew = datenew.AddHours(-double.Parse(words[2]));

                        datenew = datenew.AddMinutes(-double.Parse(words[4]));
                        pm.PC_End = datenew.ToString("MM-dd-yyyy hh:mm:ss tt"); ;
                    }
                    else
                    {

                        pm.PC_End = p.PC_End;
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
            using (EventComboEntities db = new EventComboEntities())
            {
                if (model.Formtype == "E")
                {

                    if (Regex.IsMatch(model.PC_Code.ToString(), "^[a-zA-Z0-9@_,-]+$"))
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
                                org.PC_Start = model.PC_Start;
                            }
                            org.Pc_Enddatetype = model.Pc_Enddatetype;
                            if (model.Pc_Enddatetype == "1")
                            {
                                org.PC_End = model.enddays;
                            }
                            else
                            {
                                org.PC_End = model.PC_End;
                            }

                            org.PC_Apply = model.PC_Apply;
                            org.PC_Eventid = model.PC_Eventid;
                            org.SavedDate = DateTime.Now;





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
                else
                {


                    if (!string.IsNullOrWhiteSpace(model.PC_Code))
                    {
                        if (Regex.IsMatch(model.PC_Code.ToString(), "^[a-zA-Z0-9@_,-]+$"))
                        {
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
                                    org.PC_Start = model.PC_Start;
                                }

                                org.Pc_Enddatetype = model.Pc_Enddatetype;
                                if (model.Pc_Enddatetype == "1")
                                {
                                    org.PC_End = model.enddays;
                                }
                                else
                                {
                                    org.PC_End = model.PC_End;
                                }
                                org.PC_Apply = model.PC_Apply;
                                org.PC_Eventid = model.PC_Eventid;

                                org.SavedDate = DateTime.Now;



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
                                                        org.PC_Start = model.PC_Start;
                                                    }
                                                    org.Pc_Enddatetype = model.Pc_Enddatetype;
                                                    if (model.Pc_Enddatetype == "1")
                                                    {
                                                        org.PC_End = model.enddays;
                                                    }
                                                    else
                                                    {
                                                        org.PC_End = model.PC_End;
                                                    }
                                                    org.PC_Apply = model.PC_Apply;
                                                    org.PC_Eventid = model.PC_Eventid;
                                                    org.SavedDate = DateTime.Now;




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
                                                    org.PC_Start = model.PC_Start;
                                                }
                                                org.Pc_Enddatetype = model.Pc_Enddatetype;
                                                if (model.Pc_Enddatetype == "1")
                                                {
                                                    org.PC_End = model.enddays;
                                                }
                                                else
                                                {
                                                    org.PC_End = model.PC_End;
                                                }
                                                org.PC_Apply = model.PC_Apply;
                                                org.PC_Eventid = model.PC_Eventid;


                                                org.SavedDate = DateTime.Now;


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

            if (!string.IsNullOrWhiteSpace(Invalidrepeatcode))
            {
                TempData["Invalidcode"] = Invalidrepeatcode;

                return RedirectToAction("CreatePromotionalCodes", "ManageEvent", new { Eventid = model.PC_Eventid });
            }
            else
            {
                return RedirectToAction("PromotionalCodes", "ManageEvent", new { Eventid = model.PC_Eventid });


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




                Promo_Code prof = db.Promo_Code.Where(i => i.PC_id == promocode).FirstOrDefault();
                db.Promo_Code.Remove(prof);
                db.SaveChanges();




                return "D";

            }
            catch (Exception ex)
            {
                return "E";

            }

        }


        #region EmailInvitations
        //[Authorize]
        //public ActionResult EmailInvitations()
        //{

        //    return View();
        //}

        [Authorize]
        public ActionResult CreateInvitations()
        {

            return View();
        }



    }
    #endregion


}