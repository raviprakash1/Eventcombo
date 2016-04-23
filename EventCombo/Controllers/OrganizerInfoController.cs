using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
using System.Globalization;
using System.Text.RegularExpressions;
using EventCombo.Utils;

namespace EventCombo.Controllers
{
    public class OrganizerInfoController : Controller
    {
        EventComboEntities db = new EventComboEntities();
        // GET: OrganizerInfo
        public ActionResult Index(int id, long eventid)
        {
            var TopAddress = "";
            var Topvenue = "";
            Session["logo"] = "Organizer";
            string sDate_new = "", eDate_new = "";
            string startday = "", endday = "", starttime = "", endtime = "";
            DateTime ENDATE = new DateTime();
            DateTime SDATEN = new DateTime();
            DateTime eDate = new DateTime();
            DateTime dateTime = new DateTime();
            Organizer_Master mst = (from x in db.Organizer_Master where x.Orgnizer_Id == id select x).FirstOrDefault();

            mst.Imagepath = !string.IsNullOrEmpty(mst.Imagepath) ? mst.Imagepath : "Images/default_org_image.jpg";
            mst.Eventid = eventid;
            mst.Organizer_Desc = Server.HtmlDecode(mst.Organizer_Desc);
            var OrganizerEvents =  db.GetOrganizerEventid(id).Select(x=>x.Orgnizer_Event_Id).ToList();
            mst.pastevent = new List<Organiserevent>();
            mst.presentevent = new List<Organiserevent>();

            foreach (var item in OrganizerEvents)
            {
                Organiserevent orgev = new Organiserevent();
                CreateEventController cms = new CreateEventController();
                long ?Eventid = item;
                var EventDetail = cms.GetEventdetail(Eventid??0);
               
                var image = cms.GetImages(Eventid ?? 0).FirstOrDefault();
                if (image != null)
                {
                    orgev.FirstImage = image;
                }
                else
                {
                    orgev.FirstImage = "/Images/default_event_image.jpg";
                }

               
                var timezone = "";
                var Timezonedetail = (from ev in db.TimeZoneDetails where ev.TimeZone_Id.ToString() == EventDetail.TimeZone select ev).FirstOrDefault();
                if (Timezonedetail != null)
                {
                    timezone = Timezonedetail.TimeZone;
                    TimeZoneInfo timeZoneInfo;


                    timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                    dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
                    //Timezone value

                }

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
                DateTimeWithZone dtzstart, dzend, dtzCreated;
                var eventype = (from ev in db.MultipleEvents where ev.EventID == item select ev).Count();
                if (eventype > 0)
                {
                    var evschdetails = (from ev in db.MultipleEvents where ev.EventID == item select ev).FirstOrDefault();
                    
                   
                 
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
                    SDATEN= dtzstart.LocalTime;
                    orgev.Dateofevent = startday + "," + sDate_new + "," + starttime + "-" + endday + "," + eDate_new + "," + endtime;

                }
                else
                {
                    var evschdetails = (from ev in db.EventVenues where ev.EventID == item select ev).FirstOrDefault();
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
                        orgev.Dateofevent = startday + "," + sDate_new + "," + starttime ;


                    }

                  
                }
                orgev.Eventtitle = EventDetail.EventTitle;
                orgev.Dateofeventsort = SDATEN;
                orgev.eventpath = Url.Action("ViewEvent", "ViewEvent", new { strEventDs = Regex.Replace(EventDetail.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = EventDetail.EventID.ToString() });
                if (!string.IsNullOrEmpty(eDate_new))
                {
                  
                    if (eDate < dateTime)
                    {

                        mst.pastevent.Add(orgev);
                    }
                    else
                    {
                        mst.presentevent.Add(orgev);
                    }
                }
                else
                {
                    mst.pastevent.Add(orgev);
                }


            }
            mst.pasteventcount = mst.pastevent.Count();
            mst.presentevtcount = mst.presentevent.Count();
            mst.maxsetcount = 20;
            if (mst.presentevent != null)
            {
                mst.presentevent= mst.presentevent.Take(mst.maxsetcount).OrderBy(x => x.Dateofeventsort).ToList().ToList();
            }
            if (mst.pastevent != null)
            {
                mst.pastevent= mst.pastevent.Take(mst.maxsetcount).OrderBy(x => x.Dateofeventsort).ToList().ToList();
            }


            return View(mst);
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
                orgev.eventpath = Url.Action("ViewEvent", "ViewEvent", new { strEventDs = Regex.Replace(EventDetail.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = EventDetail.EventID.ToString() });

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