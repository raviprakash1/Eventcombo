﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
using System.Globalization;
using System.Text.RegularExpressions;
using EventCombo.DAL;
using EventCombo.Service;

namespace EventCombo.Controllers
{
   
    public class EventConfirmationController : Controller
    {
        // GET: EventConfirmation
        EventComboEntities db = new EventComboEntities();
        ECImageService _iservice;
        
        public EventConfirmationController()
        {
          var mapper = AutomapperConfig.Config.CreateMapper();
          _iservice = new ECImageService(new EntityFrameworkUnitOfWorkFactory(new EventComboContextFactory()), mapper, new ECImageStorage(mapper));
        }

        public ActionResult EventConfirmation(string EventId)
        {
            if (Session["AppId"] != null)
            {
                var Eventdetails = db.Events.Where(x => x.EventID.ToString() == EventId.ToString()).FirstOrDefault();
                if (Eventdetails == null) return RedirectToAction("Index", "Home");
                if (Session["AppId"].ToString().Trim() != Eventdetails.UserID.Trim() || Eventdetails.EventStatus.ToUpper().Trim() == "SAVE")
                {
                    return RedirectToAction("Index", "Home");
                }

                if (!String.IsNullOrEmpty(Eventdetails.EventPrivacy) && (Eventdetails.EventPrivacy.ToUpper() == "PRIVATE") && (Eventdetails.Private_GuestOnly == "Y"))
                {
                  return RedirectToAction("CreateInvitations", "ManageEvent", new { lId = 0, lEvtId = Eventdetails.EventID, strMode = "C" });
                }

                MyAccount hmc = new MyAccount();
                string usernme = hmc.getusername();
                if (string.IsNullOrEmpty(usernme))
                {
                    return RedirectToAction("Index", "Home");
                }
                EventConfirmation cms = new EventConfirmation();
                var Image = db.EventImages.Where(x => x.EventID.ToString() == EventId.ToString()).FirstOrDefault();
                
                if ((Eventdetails.ECImageId ?? 0) > 0)
                {
                  var img = _iservice.GetImageById(Eventdetails.ECImageId ?? 0);
                  if ((img != null) && (System.IO.File.Exists(img.FilePath)))
                    cms.Imageurl = img.ImagePath;
                }

                cms.Imageurl = String.IsNullOrEmpty(cms.Imageurl) ? "/Images/default_event_image.jpg" : cms.Imageurl;

                cms.Title = Eventdetails.EventTitle.ToString();
                cms.urlTitle = Regex.Replace(Eventdetails.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""); ;
                var evid = long.Parse(EventId);
                var evAdress = (from ev in db.Addresses where ev.EventId == evid select ev).FirstOrDefault();

                var GetEventDate = db.GetEventDateList(evid).FirstOrDefault();
                //Date Section
                var eventype = (from ev in db.MultipleEvents where ev.EventID.ToString() == EventId select ev).Count();
                string startday = "", sDate_new = "", endday="", eDate_new="", starttime="", endtime="";



                if (eventype > 0)
                {
                 
                    var evschdetails = (from ev in db.MultipleEvents where ev.EventID.ToString() == EventId select ev).FirstOrDefault();
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





                }
                else
                {
                   
                    var evschdetails = (from ev in db.EventVenues where ev.EventID.ToString() == EventId select ev).FirstOrDefault();
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
                    }



                }



                //DAte


                cms.Startdate = startday.ToString() + " " + sDate_new + " " + starttime + "-" + endday.ToString() + " " + eDate_new + " " + endtime;
                cms.Address = (evAdress != null ? evAdress.ConsolidateAddress : "");
                var url = Request.Url;
                var baseurl = url.GetLeftPart(UriPartial.Authority);
                string title = Regex.Replace(cms.Title.Trim().Replace(" ", " - "), "[^ a - zA - Z0 - 9_ -] + ", "");
                cms.url = baseurl + Url.Action("ViewEvent", "EventManagement") + "?strEventDs= " + title + "&strEventId=" + EventId + "";
                cms.Descritption = Eventdetails.EventDescription;
                cms.EventId = EventId;
                return View(cms);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}