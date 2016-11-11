using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
using System.Globalization;
using System.Text.RegularExpressions;
using EventCombo.Utils;
using EventCombo.Service;
using NLog;

namespace EventCombo.Controllers
{
    public class OrganizerInfoController : BaseController
    {
        private IEventService _eService;
        private ILogger _logger;

        public OrganizerInfoController()
            : base()
        {
            _eService = new EventService(_factory, _mapper);
            _logger = LogManager.GetCurrentClassLogger();
        }
        EventComboEntities db = new EventComboEntities();

        public ActionResult OrganizerInfo(string organizerName, long organizerId)
        {

            Session["logo"] = "Organizer";
            Organizer_Master mst = (from x in db.Organizer_Master where x.Orgnizer_Id == organizerId select x).FirstOrDefault();

            mst.Imagepath = !string.IsNullOrEmpty(mst.Imagepath) ? mst.Imagepath : "/Images/default_org_image.jpg";
            mst.Organizer_Desc = Server.HtmlDecode(mst.Organizer_Desc);

            return View(mst);
        }

        [HttpGet]
        public JsonResult OrganizerEvent(bool isUpcomingEvent, long organizerId)
        {
            List<OrganizerEvnetViewModel> organizerEvents = new List<OrganizerEvnetViewModel>();
            var TopAddress = "";
            var Topvenue = "";
            string sDate_new = "", eDate_new = "";
            string startday = "", endday = "", starttime = "", endtime = "";
            DateTime dateTime = new DateTime();
            DateTime ENDATE = new DateTime();
            DateTime SDATEN = new DateTime();
            DateTime eDate = new DateTime();

            var OrganizerEvents = db.GetOrganizerEventid(organizerId).Select(x => x.Orgnizer_Event_Id).ToList();

            foreach (var item in OrganizerEvents)
            {
                OrganizerEvnetViewModel OrganizerEvnet = new OrganizerEvnetViewModel();
                EventCreation cms = new EventCreation();
                long? Eventid = item;
                var EventDetail = cms.GetEventdetail(Eventid ?? 0);

                var image = cms.GetImages(Eventid ?? 0).FirstOrDefault();
                if (image != null)
                {
                    OrganizerEvnet.EventImg = image;
                }
                else
                {
                    OrganizerEvnet.EventImg = "/Images/default_event_image.jpg";
                }
                var timezone = "";
                var Timezonedetail = (from t in db.TimeZoneDetails where t.TimeZone_Id.ToString() == EventDetail.TimeZone select t).FirstOrDefault();
                if (Timezonedetail != null)
                {
                    timezone = Timezonedetail.TimeZone;
                    TimeZoneInfo timeZoneInfo;
                    timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                    dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
                }
                var Addresstype = EventDetail.AddressStatus;
                if (Addresstype == "PastLocation")
                {
                    var evAdress = (from a in db.Addresses where a.AddressID == EventDetail.LastLocationAddress select a).FirstOrDefault();
                    if (evAdress != null)
                    {
                        TopAddress = evAdress.ConsolidateAddress;
                        Topvenue = evAdress.VenueName;
                    }
                }
                else
                {
                    var evAdress = (from a in db.Addresses where a.EventId == item select a).FirstOrDefault();
                    if (evAdress != null)
                    {
                        TopAddress = evAdress.ConsolidateAddress;
                        Topvenue = evAdress.VenueName;

                    }
                }
                OrganizerEvnet.Venue = TopAddress;
                DateTimeWithZone dtzstart, dzend, dtzCreated;
                var eventype = (from e in db.MultipleEvents where e.EventID == item select e).Count();
                if (eventype > 0)
                {
                    var evschdetails = (from e in db.MultipleEvents where e.EventID == item select e).FirstOrDefault();

                    if (Timezonedetail != null)
                    {
                        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Timezonedetail.TimeZone);
                        dtzstart = new DateTimeWithZone(Convert.ToDateTime(evschdetails.M_Startfrom), userTimeZone, true);
                        dzend = new DateTimeWithZone(Convert.ToDateTime(evschdetails.M_StartTo), userTimeZone, true);
                        dtzCreated = new DateTimeWithZone(DateTime.Now, userTimeZone, false);
                    }
                    else
                    {
                        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                        dtzstart = new DateTimeWithZone(Convert.ToDateTime(evschdetails.M_Startfrom), userTimeZone, true);
                        dzend = new DateTimeWithZone(Convert.ToDateTime(evschdetails.M_StartTo), userTimeZone, true);
                        dtzCreated = new DateTimeWithZone(DateTime.Now, userTimeZone, false);
                    }

                    dateTime = dtzCreated.LocalTime;
                    DateTime sDate = new DateTime();
                    sDate = dtzstart.LocalTime;
                    startday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(sDate).ToString();
                    sDate_new = sDate.ToString("MMM dd, yyyy");
                    eDate = dzend.LocalTime;
                    endday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(eDate).ToString();
                    eDate_new = eDate.ToString("MMM dd, yyyy");
                    starttime = sDate.ToString("h:mm tt").Trim().Replace(" ", ""); ; ;
                    endtime = eDate.ToString("h:mm tt").Trim().Replace(" ", ""); ;
                    ENDATE = dzend.LocalTime;
                    SDATEN = dtzstart.LocalTime;
                    OrganizerEvnet.DateOfEvent = startday + " " + sDate_new + " " + starttime + "-" + endday + " " + eDate_new + " " + endtime;

                }
                else
                {
                    var evschdetails = (from e in db.EventVenues where e.EventID == item select e).FirstOrDefault();
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
                        sDate_new = sDate.ToString("MMM dd, yyyy");
                        eDate = dzend.LocalTime;
                        endday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(eDate).ToString();
                        eDate_new = eDate.ToString("MMM dd, yyyy");
                        starttime = sDate.ToString("h:mm tt").Trim().Replace(" ", ""); ; ;
                        endtime = eDate.ToString("h:mm tt").Trim().Replace(" ", ""); ;
                        ENDATE = dzend.LocalTime;
                        SDATEN = dtzstart.LocalTime;
                        OrganizerEvnet.DateOfEvent = startday + " " + sDate_new + " " + starttime;
                    }
                }
                OrganizerEvnet.EventTitle = EventDetail.EventTitle;
                OrganizerEvnet.EventPrice = _eService.GetTicketPrice(EventDetail.EventID);
                OrganizerEvnet.EventType = EventDetail.EventType.EventType1;
                OrganizerEvnet.EventCat = EventDetail.EventCategory.EventCategory1;
                OrganizerEvnet.EventId = EventDetail.EventID;
                OrganizerEvnet.EventPrivacy = "Y";
                if (EventDetail.EventPrivacy.Trim().ToLower() == "private" && EventDetail.Private_ShareOnFB.Trim() == "N")
                {
                    OrganizerEvnet.EventPrivacy = "N";
                }
                OrganizerEvnet.EventLike = _eService.GetEventFavLikes(EventDetail.EventID, EventDetail.UserID);
                OrganizerEvnet.EventDate = SDATEN;
                OrganizerEvnet.EventPath = Url.Action("ViewEvent", "EventManagement", new { strEventDs = Regex.Replace(EventDetail.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = EventDetail.EventID.ToString() });
                if (!string.IsNullOrEmpty(eDate_new))
                {
                    if (eDate.Date < dateTime.Date && !isUpcomingEvent)
                    {
                        organizerEvents.Add(OrganizerEvnet);
                    }
                    else if (isUpcomingEvent)
                    {
                        organizerEvents.Add(OrganizerEvnet);
                    }
                }
                else if (!isUpcomingEvent)
                {
                    organizerEvents.Add(OrganizerEvnet);
                }
            }
            return Json(organizerEvents, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult Organiserlist(int OrganizerID,int lengthorg,string type)
        {
            var TopAddress = "";
            var Topvenue = "";
            string sDate_new = "", eDate_new = "";
            string startday = "", endday = "", starttime = "", endtime = "";
            DateTime ENDATE = new DateTime();
            DateTime SDATEN = new DateTime();
            var OrganizerEvents = (from x in db.Event_Orgnizer_Detail where x.OrganizerMaster_Id == OrganizerID select x.Orgnizer_Event_Id).ToList();
           var presentlist = new List<Organiserevent>();
           var pastlist=new List<Organiserevent>();
            foreach (var item in OrganizerEvents)
            {
                Organiserevent orgev = new Organiserevent();
                CreateEventController cms = new CreateEventController();

                var EventDetail = cms.GetEventdetail(item ?? 0);
                var image = cms.GetImages(item ?? 0).FirstOrDefault();
                if (image != null)
                {
                    orgev.FirstImage = image;
                }
                else
                {
                    orgev.FirstImage = "/Images/default_event_image.jpg";
                }
                int timeZoneID = Int32.Parse(EventDetail.TimeZone);
                TimeZoneDetail td = db.TimeZoneDetails.First(x => x.TimeZone_Id == timeZoneID);
                DateTimeWithZone dtzstart, dzend;
                DateTimeWithZone dtzCreated;
                DateTime dateTime = new DateTime();
                DateTime sDate = new DateTime();
                DateTime eDate = new DateTime();
                var timezone = "";
                var Timezonedetail = (from ev in db.TimeZoneDetails where ev.TimeZone_Id.ToString() == EventDetail.TimeZone select ev).FirstOrDefault();
                if (td != null)
                {
                    TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td.TimeZone);
                    dtzCreated = new DateTimeWithZone(DateTime.Now, userTimeZone, false);
                }
                else
                {
                    TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    dtzCreated = new DateTimeWithZone(DateTime.Now, userTimeZone, false);
                }
                dateTime = dtzCreated.LocalTime;

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
                    var evAdress = (from ev in db.Addresses where ev.EventId == item select ev).FirstOrDefault();
                    if (evAdress != null)
                    {
                        TopAddress = evAdress.ConsolidateAddress;
                        Topvenue = evAdress.VenueName;

                    }
                }
                orgev.Venue = TopAddress;
                var eventype = (from ev in db.MultipleEvents where ev.EventID == item select ev).Count();
                if (eventype > 0)
                {
                    var evschdetails = (from ev in db.MultipleEvents where ev.EventID == item select ev).FirstOrDefault();

                    if (td != null)
                    {
                        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td.TimeZone);
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

                    starttime = evschdetails.StartTime.ToUpper();
                    endtime = evschdetails.EndTime.ToUpper();
                    ENDATE = dzend.LocalTime;
                    SDATEN = dtzstart.LocalTime;
                    orgev.Dateofevent = startday + "," + sDate_new + "," + starttime + "-" + endday + "," + eDate_new + "," + endtime;

                }
                else
                {
                    var evschdetails = (from ev in db.EventVenues where ev.EventID == item select ev).FirstOrDefault();
                    if (evschdetails != null)
                    {

                        if (td != null)
                        {
                            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td.TimeZone);
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
                       

                        starttime = evschdetails.EventStartTime.ToString();
                        endtime = evschdetails.EventEndTime.ToString();
                        ENDATE = dzend.LocalTime;
                        SDATEN = dtzstart.LocalTime;
                        orgev.Dateofevent = startday + "," + sDate_new + "," + starttime;
                    }
                }
                orgev.Eventtitle = EventDetail.EventTitle;
                orgev.Dateofeventsort = SDATEN;
                orgev.eventpath = Url.Action("ViewEvent", "EventManagement", new { strEventDs = Regex.Replace(EventDetail.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = EventDetail.EventID.ToString() });

                if (!string.IsNullOrEmpty(eDate_new))
                {
                    

                   
                    if (eDate < dateTime)
                    {
                        pastlist.Add(orgev);

                    }
                    else
                    {
                        presentlist.Add(orgev);
                    }
                }
                else
                {
                    pastlist.Add(orgev);
                }


            }
          
            
           if(type=="Past")
            {
                if (pastlist != null)
                {
                    pastlist= pastlist.Take(lengthorg).OrderBy(x=>x.Dateofeventsort).ToList();
                }
                return PartialView("OrganiserPresentPartialView", pastlist);
            }
            else 
             {

                if (presentlist != null)
                {
                    presentlist= presentlist.Take(lengthorg).OrderBy(x => x.Dateofeventsort).ToList();
                }
                return PartialView("OrganiserPresentPartialView", presentlist);
            }

         

        }
    }
}