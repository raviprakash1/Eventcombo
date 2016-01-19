using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
using System.Globalization;

namespace EventCombo.Controllers
{
    public class ManageEventController : Controller
    {
        // GET: ManageEvent
        EventComboEntities db = new EventComboEntities();
        public ActionResult Index(long Eventid)
        {
            var TopAddress = ""; var Topvenue = "";var Dayofweek = "";
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
            var chkdate=  (from ev in db.MultipleEvents where ev.EventID == Eventid select ev).Any();
            if(chkdate)
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

          

            Mevent.Eventstatus = Edetails.EventStatus;
            Mevent.Eventtitle = Edetails.EventTitle;
            Mevent.EventAddress = TopAddress;
            Mevent.Eventdate = startday.ToString() + " " + sDate_new + " " + starttime;
            Session["Fromname"] = "events";
            return View(Mevent);
        }
    }
}