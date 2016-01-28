using EventCombo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace EventCombo.Controllers
{
    public class ManageEventController : Controller
    {
        // GET: ManageEvent
        EventComboEntities db = new EventComboEntities();
        public ActionResult Index(long Eventid,string type)
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
            if(transaction)
            {
                Mevent.Eventtransaction = "Y";
            }
            else
            {
                Mevent.Eventtransaction = "N";
            }
            //Trn

            Mevent.Eventid = Eventid;
            Mevent.Eventstatus = Edetails.EventStatus;
            Mevent.Eventtitle = Edetails.EventTitle;
            Mevent.EventAddress = TopAddress;
            Mevent.Eventdate = startday.ToString() + " " + sDate_new + " " + starttime;
            Mevent.Eventprivacy = Edetails.EventPrivacy;
            Session["Fromname"] = "events";
            ValidationMessageController vmc = new ValidationMessageController();
            vmc.ControllerContext = new ControllerContext(this.Request.RequestContext, vmc);
          
            if (type=="P")
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
                        Amount  = gc.Key.O_TotalAmount.ToString(),
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
                                    Date=o.O_OrderDateTime
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
                                orderby gc.Key.O_Order_Id descending,gc.Key.FirstName ascending
                                select new OrderAttendees()
                                {
                                    OrderId = gc.Key.O_Order_Id,
                                    Amount = gc.Key.O_TotalAmount.ToString(),
                                    Qty = gc.ToList().Sum(a => a.Qty).ToString(),
                                    Name = gc.Key.FirstName + " " + gc.Key.LastName,
                                    Date    =gc.Key.O_OrderDateTime.ToString()
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

            TempData["ForSale"] = GetSaleAmount(Eventid,"FORSALE");
            TempData["NETSale"] = GetSaleAmount(Eventid, "NETSALE");

            return View(Mevent);
        }

        public string GetAllTicketSale(long EventId)
        {
            StringBuilder strResult = new StringBuilder();
            using (EventComboEntities objEnt = new EventComboEntities())
            {
                //where myRow.E_Id == EventId
                var vTickets = (from myRow in objEnt.Tickets
                                select myRow).ToList();
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
                foreach (Ticket obj in vTickets)
                {
                    strResult.Append("<tr>");
                    strResult.Append("<td>"); strResult.Append(obj.T_name); strResult.Append("</td>");
                    strResult.Append("<td>"); strResult.Append(obj.Price); strResult.Append("</td>");

                    var vRemQty = (from myRow in objEnt.Ticket_Quantity_Detail
                                   where myRow.TQD_Event_Id == obj.E_Id && myRow.TQD_Ticket_Id == obj.T_Id
                                   select myRow).FirstOrDefault();
                    if (vRemQty != null)
                    {
                        dSoldQty = ((vRemQty.TQD_Quantity != null ? Convert.ToInt64(vRemQty.TQD_Quantity) :0) - (vRemQty.TQD_Remaining_Quantity != null ? Convert.ToInt64(vRemQty.TQD_Remaining_Quantity) : 0));
                        strResult.Append("<td>"); strResult.Append(dSoldQty.ToString() + "/" + (vRemQty.TQD_Quantity != null ? vRemQty.TQD_Quantity.ToString() : "0")); strResult.Append("</td>");
                    }
                    else
                    {
                        strResult.Append("<td>"); strResult.Append("0/0"); strResult.Append("</td>");
                    }
                    if (vRemQty != null &&  dSoldQty == vRemQty.TQD_Quantity)
                    {
                        strResult.Append("<td>"); strResult.Append("Sold Out"); strResult.Append("</td>");
                    }
                    else
                    {
                        strResult.Append("<td>"); strResult.Append("On Sale"); strResult.Append("</td>");
                    }
                    strResult.Append("<td>"); strResult.Append((obj.Sale_End_Date != null ?  obj.Sale_End_Date.ToString() :"")); strResult.Append("</td>");
                    strResult.Append("<td>"); strResult.Append(""); strResult.Append("</td>");
                    strResult.Append("</tr>");
                }
                strResult.Append("</tbody>");
                strResult.Append("</table>");
            }
            return strResult.ToString();
        }

        public string GetSaleAmount(long lEventId,string strAmtType)
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
                    strResult = Math.Round((vTotalAmt == null ? 0 : Convert.ToDouble(vTotalAmt)), 2).ToString("N",us);
                }
                else if (strAmtType == "NETSALE")
                {
                    var vEcFee = (from myRow in objEnt.Ticket_Purchased_Detail
                                  where myRow.TPD_Event_Id == lEventId
                                  select myRow.TPD_EC_Fee).Sum();

                    double dResult = Math.Round((vTotalAmt == null ? 0 : Convert.ToDouble(vTotalAmt)) - (vEcFee == null ? 0 : Convert.ToDouble(vEcFee)),2);
                    strResult = dResult.ToString("N", us);
                }
            }
            return strResult; 
        }
        public string SaveEventUrl(long lEventId,string strEventUrl)
        {
            string strResult = "N";
            try
            {
                if (CheckEventUrl(strEventUrl,lEventId) == "Y")
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
        public string CheckEventUrl(string strUserUrl,long EventId)
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
           
                using (var transaction=db.Database.BeginTransaction())
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

                    //db.Database.ExecuteSqlCommand("Delete from Event_Orgnizer_Detail where Orgnizer_Event_Id='" + eventid + "'");
                    //db.Database.ExecuteSqlCommand("Delete from Event_VariableDesc where Event_Id='" + eventid + "'");
                    //db.Database.ExecuteSqlCommand("Delete from EventImage where EventID='" + eventid + "'");
                    //db.Database.ExecuteSqlCommand("Delete from EventFavourite where eventId='" + eventid + "'");
                    //db.Database.ExecuteSqlCommand("Delete from Address where EventId='" + eventid + "'");
                    // db.Database.ExecuteSqlCommand("Delete from EventVenue where EventID='" + eventid + "'");
                    // db.Database.ExecuteSqlCommand("Delete from EventVote where eventId='" + eventid + "'");
                    //db.Database.ExecuteSqlCommand("Delete from MultipleEvent where EventID='" + eventid + "'");
                    //db.Database.ExecuteSqlCommand("Delete from Publish_Event_Detail where PE_Event_Id='" + eventid + "'");
                    //db.Database.ExecuteSqlCommand("Delete from Ticket_Quantity_Detail where TQD_Event_Id='" + eventid + "'");
                    //db.Database.ExecuteSqlCommand("Delete from Events_Hit where EventHit_EventId='" + eventid + "'");
                    //db.Database.ExecuteSqlCommand("Delete from Ticket where E_Id='" + eventid + "'");
                   // db.Database.ExecuteSqlCommand("Delete from Event where EventID='" + eventid + "'");




                   
                
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
                    dResult = Math.Round(((ltotalqty - lremqty) * 100) / ltotalqty,2);
                }
            }
            catch (Exception ex)
            {
                dResult = 0;
            }
            return dResult;
        }

        public double GetTicketQtyPer(long eventId,string strTicketType)
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
                    else {
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


        public long GetQuantity(long eventId,string strQtyType)
        {
            long lResult = 0;
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    if (strQtyType =="R")
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
                    string strQuery = "SELECT sum(TPD_Purchased_Qty) as SaleQty,Convert(date,O_OrderDateTime) AS orderDate FROM Ticket_Purchased_Detail LEFT JOIN Order_Detail_T On Ticket_Purchased_Detail.TPD_Order_Id = Order_Detail_T.O_Order_Id where isnull(TPD_Order_Id,'') !='' AND ISNULL(O_OrderDateTime,'') !='' AND TPD_Event_Id = " + eventId + " and COnvert(date,O_OrderDateTime) = convert(date,'" + dt + "') group by Convert(date,O_OrderDateTime) ";
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
            string strTitle="";
            using (EventComboEntities objEnt = new EventComboEntities())
            {
                var vEvent = (from myEnt in objEnt.Events where myEnt.EventID == Eventid select myEnt).FirstOrDefault();
                strTitle = "Copy of "  +  vEvent.EventTitle;
            }
            TempData["EventId"] = Eventid.ToString();
            return View();
        }

        public long SaveEvent(long Eventid,string strEventTitle)
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
                    ObjEC.EventTitle  = strEventTitle;
                    ObjEC.DisplayStartTime = vEvent.DisplayStartTime;
                    ObjEC.DisplayEndTime = vEvent.DisplayEndTime;
                    ObjEC.DisplayTimeZone = vEvent.DisplayTimeZone;
                    ObjEC.EventDescription = vEvent.EventDescription;
                    ObjEC.EventPrivacy = vEvent.EventPrivacy;
                    ObjEC.Private_ShareOnFB = vEvent.Private_ShareOnFB;
                    ObjEC.Private_GuestOnly = vEvent.Private_GuestOnly;
                    ObjEC.Private_Password = vEvent.Private_Password;
                    ObjEC.EventUrl = vEvent.EventUrl;
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
                            objEOrg.Orgnizer_Name = objOr.Orgnizer_Name;
                            objEOrg.Orgnizer_Desc = objOr.Orgnizer_Desc;
                            objEOrg.FBLink = objOr.FBLink;
                            objEOrg.Twitter = objOr.Twitter;
                            objEOrg.DefaultOrg = objOr.DefaultOrg;
                            objEOrg.Linkedin = objOr.Linkedin;
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
                    objCE.ControllerContext = new ControllerContext(this.Request.RequestContext,objCE);
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

    }
}