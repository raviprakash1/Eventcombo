using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
using System.Collections;
using System.Data;
using System.Text;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data.Entity.SqlServer;
using EventCombo.Utils;

namespace EventCombo.Controllers
{
    //[RoutePrefix("event")]
    //[RouteArea("Event", AreaPrefix = "E")]
    //[RoutePrefix("Ev")]
  
    public class ViewEventController : Controller
    {
        EventComboEntities db = new EventComboEntities();
        // GET: ViewEvent
        //[Route("{strUrlData}", Name = "ViewEvent",Order =1)]
        //[OutputCache(NoStore =true,Duration =0,VaryByParam ="None") ]

        //[Route("{strEventDs?}-{strEventId?}", Name = "ViewEvent"), HttpGet]

        public ActionResult ViewEvent(string strEventDs, string strEventId)
        {
            MyAccount hmc = new MyAccount();
            if ((Session["AppId"] != null))
            {
               
                string usernme = hmc.getusername();
                if (string.IsNullOrEmpty(usernme))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (strEventId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //if (!strUrlData.Contains('౼'))
            //{
            //    return RedirectToAction("Index", "Home");

            //}
            ValidationMessage vmc = new ValidationMessage();

            //string[] str = strUrlData.Split('౼');
            //string strForView = "";
            //string eventTitle = str[0].ToString();

            //long EventId =  vmc.GetLatestEventId(Convert.ToInt64(str[1]));

            //try
            //{
            //    strForView = str[2].ToString();
            //}
            //catch (Exception)
            //{
            //    strForView = "N";
            //}
            long EventId = (strEventId != "" ? Convert.ToInt64(strEventId) : 0);
            EventId = vmc.GetLatestEventId(EventId);
            TempData["ForViewOnly"] = "N";
            DateTime eDate = new DateTime();
            EventHit(EventId);



            string sDate_new = "", eDate_new = "", orgevents="";
            string startday = "", endday = "", starttime = "", endtime = "";
            Session["Fromname"] = "events";
            Session["logo"] = "events";
            EventCreation objCE = new EventCreation();
            var EventDetail = objCE.GetEventdetail(EventId);
            if (EventDetail == null) return null;
            var url = Url.RouteUrl("ViewEvent", new { strEventDs = Regex.Replace(EventDetail.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = EventDetail.EventID.ToString() });
            Session["ReturnUrl"] = "ViewEvent~" + url;

            var TopAddress = ""; var Topvenue = "";
            string organizername = "", fblink = "", twitterlink = "", organizerid = "", tickettype = "", enablediscussion = "", linkedin = "";
            ViewEvent viewEvent = new ViewEvent();
            //EventDetails
            var Evnttype = (from ev in db.EventTypes where ev.EventTypeID == EventDetail.EventTypeID select ev.EventType1).FirstOrDefault();

            TempData["EventType"] = Evnttype;

            var EvntCtgry = (from ev in db.EventCategories where ev.EventCategoryID == EventDetail.EventCategoryID select ev.EventCategory1).FirstOrDefault();
            var EvntSubCtgry = (from ev in db.EventSubCategories where ev.EventCategoryID == EventDetail.EventCategoryID && ev.EventSubCategoryID == EventDetail.EventSubCategoryID select ev.EventSubCategory1).FirstOrDefault();
            TempData["EventCategory"] = EvntCtgry;
            TempData["EventSubCategory"] = EvntSubCtgry;

            var OrganiserDetail = (from ev in db.Event_Orgnizer_Detail
                                   join pfd in db.Organizer_Master on ev.OrganizerMaster_Id equals pfd.Orgnizer_Id
                                   where ev.Orgnizer_Event_Id == EventId && ev.DefaultOrg == "Y"
                                   select new
                                   {
                                       Orgnizer_Name = pfd.Orgnizer_Name,
                                       FBLink = pfd.Organizer_FBLink,
                                       Twitter = pfd.Organizer_Twitter,
                                       Linkedin = pfd.Organizer_Linkedin,
                                       Orgnizer_Id = pfd.Orgnizer_Id

                                   }).FirstOrDefault();
            var displaystarttime = EventDetail.DisplayStartTime;
            var displayendtime = EventDetail.DisplayEndTime;
            var htmldisplay = new HtmlString(Server.HtmlDecode(EventDetail.EventDescription));
            var EventDescription = htmldisplay;
            var showtimezone = EventDetail.DisplayTimeZone;
            enablediscussion = EventDetail.EnableFBDiscussion;
            viewEvent.showTimezone = showtimezone;
            var timezone = "";
            DateTime dateTime= new DateTime();
            DateTime sDate = new DateTime();
            var Timezonedetail = (from ev in db.TimeZoneDetails where ev.TimeZone_Id.ToString() == EventDetail.TimeZone select ev).FirstOrDefault();
            DateTimeWithZone dtzstart, dzend, dtznewstart;
            DateTimeWithZone dtz;
            DateTimeWithZone dtzCreated;
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
            timezone = Timezonedetail.TimeZone_Name;

            viewEvent.EventCancel = EventDetail.EventCancel;
            viewEvent.Timezone = timezone;
            viewEvent.enablediscussion = enablediscussion;
            viewEvent.showmaponevent = EventDetail.ShowMap;
            viewEvent.EventPrivacy = EventDetail.EventPrivacy;
            viewEvent.PrivatePassword = EventDetail.Private_Password;
            //Address
            var Addresstype = EventDetail.AddressStatus;
            if (Addresstype == "PastLocation")
            {
                var evAdress = (from ev in db.Addresses where ev.AddressID == EventDetail.LastLocationAddress select ev).FirstOrDefault();
                if (evAdress != null)
                {
                    TopAddress = evAdress.ConsolidateAddress;
                    Topvenue = evAdress.VenueName;
                }
            }
            else
            {
                var evAdress = (from ev in db.Addresses where ev.EventId == EventId select ev).FirstOrDefault();
                if (evAdress != null)
                {
                    TopAddress = evAdress.ConsolidateAddress;
                    Topvenue = evAdress.VenueName;

                }
            }

            //Organiser
            if (OrganiserDetail != null)
            {
                organizername = OrganiserDetail.Orgnizer_Name;
                fblink = OrganiserDetail.FBLink;
                twitterlink = OrganiserDetail.Twitter;
                organizerid = OrganiserDetail.Orgnizer_Id.ToString();
               linkedin = OrganiserDetail.Linkedin;
                var exceptionList = db.EventVenues.Where(x=> SqlFunctions.DateDiff("s", x.E_Startdate , DateTime.UtcNow) > 0 ).Select(e => e.EventID).ToList();
                var exceptionList1 = db.MultipleEvents.Where(x => SqlFunctions.DateDiff("s", x.M_StartTo , DateTime.UtcNow) > 0 ).Select(e => e.EventID);
                var Organizerevents = db.GetOrganizerEventid(OrganiserDetail.Orgnizer_Id) .ToList() ;


                orgevents = (from x in Organizerevents where x.OrganizerMaster_Id == OrganiserDetail.Orgnizer_Id && !exceptionList.Contains(x.Orgnizer_Event_Id??0) && !exceptionList1.Contains(x.Orgnizer_Event_Id ?? 0)  select x).Count().ToString();


            }
            var favCount = (from ev in db.EventFavourites where ev.eventId == EventId select ev).Count();
            var votecount = (from ev in db.EventVotes where ev.eventId == EventId select ev).Count();
            var eventype = (from ev in db.MultipleEvents where ev.EventID == EventId select ev).Count();
            //GetDateList
            var GetEventDate = db.GetEventDateList(EventId).ToList();
            List<listevent> lstevent = new List<listevent>();
            foreach (var item in GetEventDate)
            {
               listevent lst = new listevent();
                if (Timezonedetail != null)
                {


                    TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Timezonedetail.TimeZone);
                    dtznewstart = new DateTimeWithZone(Convert.ToDateTime(item.Datefrom), userTimeZone, true);

                    //Timezone value

                }
                else
                {
                    TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    dtznewstart = new DateTimeWithZone(Convert.ToDateTime(item.Datefrom), userTimeZone, true);
                }
             var datenew= dtznewstart.LocalTime;
               var  endnewday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(datenew).ToString();
               var eDatnew = datenew.ToString("MMM dd, yyyy");
               var startnewtime = datenew.ToString("h:mm tt").ToLower().Trim().Replace(" ", ""); ;
                lst.Dayofweek = endnewday;
                lst.Datefrom = eDatnew;
                lst.Time = startnewtime;
                lstevent.Add(lst);
            }
            ViewBag.DateList = lstevent;
            DateTime ENDATE = new DateTime();
            if (eventype > 0)
            {
                viewEvent.eventType = "Multiple";
                var evschdetails = (from ev in db.MultipleEvents where ev.EventID == EventId select ev).FirstOrDefault();

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



              
             
                sDate = dtzstart.LocalTime;
                startday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(sDate).ToString();

                sDate_new = sDate.ToString("MMM dd, yyyy");
              

              
                eDate = dzend.LocalTime;
                endday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(eDate).ToString();
                eDate_new = eDate.ToString("MMM dd, yyyy");
                 starttime = sDate.ToString("h:mm tt").ToLower().Trim().Replace(" ", ""); ;
                 endtime = eDate.ToString("h:mm tt").ToLower().Trim().Replace(" ", ""); ;
              
               




            }
            else
            {
                viewEvent.eventType = "single";
                var evschdetails = (from ev in db.EventVenues where ev.EventID == EventId select ev).FirstOrDefault();
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


                   
                   
                      
                        sDate = dtzstart.LocalTime;
                        startday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(sDate).ToString();
                        sDate_new = sDate.ToString("MMM dd,yyyy");
                  
                
                  
                        eDate = dzend.LocalTime;
                        endday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(eDate).ToString();
                        eDate_new = eDate.ToString("MMM dd,yyyy");


                    starttime = sDate.ToString("h:mm tt").ToLower().Trim().Replace(" ", ""); ;
                    endtime = eDate.ToString("h:mm tt").ToLower().Trim().Replace(" ", ""); ;

                }



            }

            if ((displaystarttime == "Y" || displaystarttime == "y") && (displayendtime == "Y" || displayendtime == "y"))
            {
                viewEvent.DisplaydateRange = startday.ToString() + " " + sDate_new + " " + starttime + "-" + endday.ToString() + " " + eDate_new + " " + endtime;

            }

            if ((displaystarttime == "N" || displaystarttime == "n") && (displayendtime == "Y" || displayendtime == "Y"))
            {
                viewEvent.DisplaydateRange = startday.ToString() + " " + sDate_new + "-" + endday.ToString() + " " + eDate_new + " " + endtime;

            }

            if ((displaystarttime == "N" || displaystarttime == "n") && (displayendtime == "n" || displayendtime == "N"))
            {
                viewEvent.DisplaydateRange = startday.ToString() + " " + sDate_new + "-" + endday.ToString() + " " + eDate_new;

            }

            if ((displayendtime == "N" || displayendtime == "n") && (displaystarttime == "Y" || displaystarttime == "y"))
            {
                viewEvent.DisplaydateRange = startday.ToString() + " " + sDate_new + " " + starttime + "-" + endday.ToString() + " " + eDate_new;

            }
           
            if (!string.IsNullOrEmpty(eDate_new))
            {
                var enday = DateTime.Parse(eDate_new);
             
                var now = DateTime.Now;
                if (eDate < dateTime)
                {

                    TempData["ExpiredEvent"] = vmc.Index("ViewEvent", "ViewEventExpiredSy");
                    TempData["ForViewOnly"] = "Y";
                }
            }
            else
            {
                TempData["ExpiredEvent"] = vmc.Index("ViewEvent", "ViewEventExpiredSy");
                TempData["ForViewOnly"] = "Y";
            }
            if (EventDetail.EventCancel=="Y")
            {
                TempData["EventCancel"] = "This event is canceled";
             }
            viewEvent.typeofEvent = EventDetail.AddressStatus;
            viewEvent.Shareonfb = EventDetail.Private_ShareOnFB;
            viewEvent.showstarttime = displaystarttime;
            viewEvent.showendtime = displayendtime;
            viewEvent.TopAddress = TopAddress;
            viewEvent.Favourite = favCount.ToString();
            viewEvent.Vote = votecount.ToString();
            viewEvent.Title = EventDetail.EventTitle;
            viewEvent.eventId = EventDetail.EventID.ToString();
            viewEvent.TopVenue = Topvenue;
            viewEvent.EventDescription = EventDescription.ToString(); 
            viewEvent.organizername = organizername;
            viewEvent.organizerid = organizerid;
            viewEvent.fblink = fblink;
            viewEvent.twitterlink = twitterlink;
            viewEvent.Linkedinlin = linkedin;
            viewEvent.Orgevents = orgevents;
            if (Session["AppId"] != null)
            {
                var userid = Session["AppId"].ToString();
                var count = (from r in db.EventFavourites
                             where r.eventId == EventId && r.UserID == userid
                             select r).Count();
                if (count > 0)
                {
                    viewEvent.hdEventFav = "Y";
                }
                else { viewEvent.hdEventFav = "N"; }


            }
            else
            {

                viewEvent.hdEventFav = "N";

            }
            if (Session["AppId"] != null)
            {
                var userid = Session["AppId"].ToString();
                var count = (from r in db.EventVotes
                             where r.eventId == EventId && r.UserID == userid
                             select r).Count();
                if (count > 0)
                {
                    viewEvent.hdEventvote = "Y";
                }
                else { viewEvent.hdEventvote = "N"; }


            }
            else
            {

                viewEvent.hdEventvote = "N";

            }
            
            ViewBag.Images = objCE.GetImages(EventId);

            var cnt = objCE.GetImages(EventId).Count();
            TempData["ImageCount"] = cnt;
            var ticketsfree = (from r in db.Tickets where r.E_Id == EventId && r.TicketTypeID == 1 select r).Count();
            int imgcount = objCE.GetImages(EventId).Count();
            var ticketsPaid = (from r in db.Tickets where r.E_Id == EventId && r.TicketTypeID == 2 select r).Count();
            var ticketsDonation = (from r in db.Tickets where r.E_Id == EventId && r.TicketTypeID == 3 select r).Count();

            var itemsremainingInCart = (from o in db.Ticket_Quantity_Detail where o.TQD_Event_Id == EventId select o.TQD_Remaining_Quantity).Sum();


            if (ticketsfree > 0 && ticketsPaid > 0 && ticketsDonation > 0)
            {
                tickettype = "Order Now";

            }
            if (ticketsfree <= 0 && ticketsPaid > 0 && ticketsDonation <= 0)
            {
                tickettype = "Order Now";

            }
            if (ticketsfree <= 0 && ticketsPaid > 0 && ticketsDonation > 0)
            {
                tickettype = "Order Now";

            }
            if (ticketsfree > 0 && ticketsPaid > 0 && ticketsDonation <= 0)
            {
                tickettype = "Order Now";

            }
            if (ticketsfree <= 0 && ticketsPaid > 0 && ticketsDonation > 0)
            {
                tickettype = "Order Now";

            }
            if (ticketsfree > 0 && ticketsPaid <= 0 && ticketsDonation <= 0)
            {
                tickettype = "Register";

            }
            if (ticketsfree <= 0 && ticketsPaid <= 0 && ticketsDonation > 0)
            {
                tickettype = "Donate";

            }
            if (ticketsfree >0 && ticketsPaid <= 0 && ticketsDonation > 0)
            {
                tickettype = "Register";

            }
            if (ticketsfree <= 0 && ticketsPaid <= 0 && ticketsDonation <= 0)
            {
                tickettype = "Get Tickets";

            }
            if (itemsremainingInCart == 0)
            {
                tickettype = "Sold Out";

            }
            viewEvent.Orderdetail = tickettype;
            return View(viewEvent);
        }

     

     
        public void EventHit(long EventId)
        {
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    //var EventH = (from myRow in objEnt.Events_Hit
                    //                where myRow.EventHit_EventId == EventId
                    //                select myRow).FirstOrDefault();
                    //if (EventH == null)
                    //{
                        Events_Hit objEHI = new Events_Hit();
                        objEHI.EventHit_EventId = EventId;
                        objEHI.EventHitDateTime = DateTime.Now;
                        objEHI.EventHit_Hits = 1;
                        
                        objEnt.Events_Hit.Add(objEHI);
                    //}
                    //else
                    //{
                    //    decimal? dEventHit = EventH.EventHit_Hits + 1;
                    //    Events_Hit objEH = objEnt.Events_Hit.First(h => h.EventHit_EventId == EventId);
                    //    objEH.EventHit_Hits = dEventHit;
                    //}
                    objEnt.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }


        }
        public void Nullurlsession()
        {
            Session["ReturnUrl"] = null;
        }
        //public ActionResult ViewEvent(string strEventDs, string strEventId)
        //{
        //    if ((Session["AppId"] != null))
        //    {
        //        HomeController hmc = new HomeController();
        //        hmc.ControllerContext = new ControllerContext(this.Request.RequestContext, hmc);
        //        string usernme = hmc.getusername();
        //        if (string.IsNullOrEmpty(usernme))
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //    }
        //    if (strEventId == null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    //if (!strUrlData.Contains('౼'))
        //    //{
        //    //    return RedirectToAction("Index", "Home");

        //    //}
        //    ValidationMessageController vmc = new ValidationMessageController();
        //    //string[] str = strUrlData.Split('౼');
        //    //string strForView = "";
        //    //string eventTitle = str[0].ToString();

        //    //long EventId = vmc.GetLatestEventId(Convert.ToInt64(str[1]));
        //    long EventId = (strEventId != "" ? Convert.ToInt64(strEventId) : 0);
        //    EventId = vmc.GetLatestEventId(EventId);
        //    //try
        //    //{
        //    //    strForView = str[2].ToString();
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    strForView = "N";
        //    //}

        //    TempData["ForViewOnly"] = "N";

        //    string sDate_new = "", eDate_new = "";
        //    string startday = "", endday = "", starttime = "", endtime = "";
        //    Session["Fromname"] = "events";
        //    //var url = Url.Action("ViewEvent", "ViewEvent") + "?strUrlData=" + eventTitle.Trim() + "౼" + EventId + "౼N";
        //    var TopAddress = ""; var Topvenue = "";
        //    string organizername = "", fblink = "", twitterlink = "", organizerid = "", tickettype = "", enablediscussion = "", linkedin = "";
        //    ViewEvent viewEvent = new ViewEvent();
        //    //EventDetails
        //    CreateEventController objCE = new CreateEventController();
        //    var EventDetail = objCE.GetEventdetail(EventId);
        //    if (EventDetail == null) return null;
        //    var url = Url.RouteUrl("ViewEvent", new { strEventDs = Regex.Replace(EventDetail.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", "") , strEventId = EventDetail.EventID.ToString()});
        //    Session["ReturnUrl"] = "ViewEvent~" + url;

        //    TempData["EventType"] = EventDetail.EventType.EventType1;

        //    var EvntCtgry = (from ev in db.EventCategories where ev.EventCategoryID == EventDetail.EventCategoryID select ev.EventCategory1).FirstOrDefault();
        //    var EvntSubCtgry = (from ev in db.EventSubCategories where ev.EventCategoryID == EventDetail.EventCategoryID && ev.EventSubCategoryID == EventDetail.EventSubCategoryID select ev.EventSubCategory1).FirstOrDefault();
        //    TempData["EventCategory"] = EvntCtgry;
        //    TempData["EventSubCategory"] = EvntSubCtgry;

        //    var OrganiserDetail = (from ev in db.Event_Orgnizer_Detail where ev.Orgnizer_Event_Id == EventId && ev.DefaultOrg == "Y" select ev).FirstOrDefault();
        //    var displaystarttime = EventDetail.DisplayStartTime;
        //    var displayendtime = EventDetail.DisplayEndTime;
        //    var EventDescription = EventDetail.EventDescription;
        //    var showtimezone = EventDetail.DisplayTimeZone;
        //    enablediscussion = EventDetail.EnableFBDiscussion;
        //    viewEvent.showTimezone = showtimezone;
        //    var timezone = "";
        //    var Timezonedetail = (from ev in db.TimeZoneDetails where ev.TimeZone_Id.ToString() == EventDetail.TimeZone select ev).FirstOrDefault();
        //    if (Timezonedetail != null)
        //    {
        //        timezone = Timezonedetail.TimeZone_Name;

        //    }
        //    viewEvent.Timezone = timezone;
        //    viewEvent.enablediscussion = enablediscussion;
        //    viewEvent.showmaponevent = EventDetail.ShowMap;
        //    viewEvent.EventPrivacy = EventDetail.EventPrivacy;
        //    //Address
        //    var evAdress = (from ev in db.Addresses where ev.EventId == EventId select ev).FirstOrDefault();
        //    if (evAdress != null)
        //    {
        //        TopAddress = evAdress.ConsolidateAddress;
        //        Topvenue = evAdress.VenueName;

        //    }

        //    //Organiser
        //    if (OrganiserDetail != null)
        //    {
        //        organizername = OrganiserDetail.Orgnizer_Name;
        //        fblink = OrganiserDetail.FBLink;
        //        twitterlink = OrganiserDetail.Twitter;
        //        organizerid = OrganiserDetail.Orgnizer_Id.ToString();
        //        linkedin = OrganiserDetail.Linkedin;

        //    }
        //    var favCount = (from ev in db.EventFavourites where ev.eventId == EventId select ev).Count();
        //    var votecount = (from ev in db.EventVotes where ev.eventId == EventId select ev).Count();
        //    var eventype = (from ev in db.MultipleEvents where ev.EventID == EventId select ev).Count();
        //    //GetDateList
        //    var GetEventDate = db.GetEventDateList(EventId).ToList();
        //    ViewBag.DateList = GetEventDate;

        //    if (eventype > 0)
        //    {
        //        viewEvent.eventType = "Multiple";
        //        var evschdetails = (from ev in db.MultipleEvents where ev.EventID == EventId select ev).FirstOrDefault();
        //        var startdate = (evschdetails.StartingFrom);
        //        DateTime sDate = new DateTime();
        //        sDate = DateTime.Parse(startdate);
        //        startday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(sDate).ToString();

        //        sDate_new = sDate.ToString("MMM dd, yyyy");
        //        var enddate = evschdetails.StartingTo;

        //        DateTime eDate = new DateTime();
        //        eDate = DateTime.Parse(enddate);
        //        endday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(eDate).ToString();
        //        eDate_new = eDate.ToString("MMM dd, yyyy");

        //        starttime = evschdetails.StartTime.ToUpper();
        //        endtime = evschdetails.EndTime.ToUpper();





        //    }
        //    else
        //    {
        //        viewEvent.eventType = "single";
        //        var evschdetails = (from ev in db.EventVenues where ev.EventID == EventId select ev).FirstOrDefault();
        //        if (evschdetails != null)
        //        {
        //            var startdate = (evschdetails.EventStartDate);
        //            if (startdate != null)
        //            {
        //                DateTime sDate = new DateTime();
        //                sDate = DateTime.Parse(startdate.ToString());
        //                startday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(sDate).ToString();
        //                sDate_new = sDate.ToString("MMM dd,yyyy");
        //            }
        //            var enddate = evschdetails.EventEndDate;
        //            if (enddate != null)
        //            {
        //                DateTime eDate = new DateTime();
        //                eDate = DateTime.Parse(enddate.ToString());
        //                endday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(eDate).ToString();
        //                eDate_new = eDate.ToString("MMM dd,yyyy");
        //            }

        //            starttime = evschdetails.EventStartTime.ToString();
        //            endtime = evschdetails.EventEndTime.ToString();
        //        }



        //    }

        //    if ((displaystarttime == "Y" || displaystarttime == "y") && (displayendtime == "Y" || displayendtime == "y"))
        //    {
        //        viewEvent.DisplaydateRange = startday.ToString() + " " + sDate_new + " " + starttime + "-" + endday.ToString() + " " + eDate_new + " " + endtime;

        //    }

        //    if ((displaystarttime == "N" || displaystarttime == "n") && (displayendtime == "Y" || displayendtime == "Y"))
        //    {
        //        viewEvent.DisplaydateRange = startday.ToString() + " " + sDate_new + "-" + endday.ToString() + " " + eDate_new + " " + endtime;

        //    }

        //    if ((displaystarttime == "N" || displaystarttime == "n") && (displayendtime == "n" || displayendtime == "N"))
        //    {
        //        viewEvent.DisplaydateRange = startday.ToString() + " " + sDate_new + "-" + endday.ToString() + " " + eDate_new;

        //    }

        //    if ((displayendtime == "N" || displayendtime == "n") && (displaystarttime == "Y" || displaystarttime == "y"))
        //    {
        //        viewEvent.DisplaydateRange = startday.ToString() + " " + sDate_new + " " + starttime + "-" + endday.ToString() + " " + eDate_new;

        //    }

        //    if (!string.IsNullOrEmpty(eDate_new))
        //    {
        //        var enday = DateTime.Parse(eDate_new);
        //        var now = DateTime.Now;
        //        if (enday < now)
        //        {

        //            TempData["ExpiredEvent"] = vmc.Index("ViewEvent", "ViewEventExpiredSy");
        //            TempData["ForViewOnly"] = "Y";
        //        }
        //    }
        //    else
        //    {
        //        TempData["ExpiredEvent"] = vmc.Index("ViewEvent", "ViewEventExpiredSy");
        //        TempData["ForViewOnly"] = "Y";
        //    }
        //    viewEvent.typeofEvent = EventDetail.AddressStatus;
        //    viewEvent.Shareonfb = EventDetail.Private_ShareOnFB;
        //    viewEvent.showstarttime = displaystarttime;
        //    viewEvent.showendtime = displayendtime;
        //    viewEvent.TopAddress = TopAddress;
        //    viewEvent.Favourite = favCount.ToString();
        //    viewEvent.Vote = votecount.ToString();
        //    viewEvent.Title = EventDetail.EventTitle;
        //    viewEvent.eventId = EventDetail.EventID.ToString();
        //    viewEvent.TopVenue = Topvenue;
        //    viewEvent.EventDescription = EventDescription;
        //    viewEvent.organizername = organizername;
        //    viewEvent.organizerid = organizerid;
        //    viewEvent.fblink = fblink;
        //    viewEvent.twitterlink = twitterlink;
        //    viewEvent.Linkedinlin = linkedin;
        //    if (Session["AppId"] != null)
        //    {
        //        var userid = Session["AppId"].ToString();
        //        var count = (from r in db.EventFavourites
        //                     where r.eventId == EventId && r.UserID == userid
        //                     select r).Count();
        //        if (count > 0)
        //        {
        //            viewEvent.hdEventFav = "Y";
        //        }
        //        else { viewEvent.hdEventFav = "N"; }


        //    }
        //    else
        //    {

        //        viewEvent.hdEventFav = "N";

        //    }
        //    if (Session["AppId"] != null)
        //    {
        //        var userid = Session["AppId"].ToString();
        //        var count = (from r in db.EventVotes
        //                     where r.eventId == EventId && r.UserID == userid
        //                     select r).Count();
        //        if (count > 0)
        //        {
        //            viewEvent.hdEventvote = "Y";
        //        }
        //        else { viewEvent.hdEventvote = "N"; }


        //    }
        //    else
        //    {

        //        viewEvent.hdEventvote = "N";

        //    }
        //    ViewBag.Images = objCE.GetImages(EventId);

        //    var ticketsfree = (from r in db.Tickets where r.E_Id == EventId && r.TicketTypeID == 1 select r).Count();
        //    var ticketsPaid = (from r in db.Tickets where r.E_Id == EventId && r.TicketTypeID == 2 select r).Count();
        //    var ticketsDonation = (from r in db.Tickets where r.E_Id == EventId && r.TicketTypeID == 3 select r).Count();

        //    var itemsremainingInCart = (from o in db.Ticket_Quantity_Detail where o.TQD_Event_Id == EventId select o.TQD_Remaining_Quantity).Sum();


        //    if (ticketsfree > 0 && ticketsPaid > 0 && ticketsDonation > 0)
        //    {
        //        tickettype = "Order Now";

        //    }
        //    if (ticketsfree <= 0 && ticketsPaid > 0 && ticketsDonation <= 0)
        //    {
        //        tickettype = "Order Now";

        //    }
        //    if (ticketsfree > 0 && ticketsPaid > 0 && ticketsDonation <= 0)
        //    {
        //        tickettype = "Order Now";

        //    }
        //    if (ticketsfree > 0 && ticketsPaid <= 0 && ticketsDonation <= 0)
        //    {
        //        tickettype = "Register";

        //    }
        //    if (ticketsfree <= 0 && ticketsPaid <= 0 && ticketsDonation > 0)
        //    {
        //        tickettype = "Donate";

        //    }
        //    if (ticketsfree <= 0 && ticketsPaid <= 0 && ticketsDonation <= 0)
        //    {
        //        tickettype = "Get Tickets";

        //    }
        //    if (itemsremainingInCart == 0)
        //    {
        //        tickettype = "Sold Out";

        //    }
        //    viewEvent.Orderdetail = tickettype;
        //    return View(viewEvent);
        //}
    }
}