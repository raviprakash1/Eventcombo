﻿using System;
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
using System.Web.Script.Serialization;
using System.Net;
using EventCombo.Utils;
using System.Web.UI;
using System.Configuration;
using NLog;

namespace EventCombo.Controllers
{
    [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
    public class EditEventController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
      // GET: EditEvent
        EventComboEntities db = new EventComboEntities();
        public ActionResult EditEvent()
        {
            //  EventCreation objCr = GetEventDataEditing();
            EventCreation objCr = new EventCreation();
            if ((Session["AppId"] != null))
            {

                try {

                    MyAccount hmc = new MyAccount();
                    string usernme = hmc.getusername();
                    if (string.IsNullOrEmpty(usernme))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    Session["logo"] = "events";
                    Session["Fromname"] = "events";
                    var url = Url.Action("CreateEvent", "CreateEvent");
                    Session["ReturnUrl"] = "CreateEvent~" + url;

                    string defaultCountry = "";
                    using (EventComboEntities db = new EventComboEntities())
                    {
                        var countryQuery = (from c in db.Countries
                                            orderby c.Country1 ascending
                                            select c).Distinct();
                        List<SelectListItem> countryList = new List<SelectListItem>();
                        defaultCountry = "United States";

                        foreach (var item in countryQuery)
                        {
                            countryList.Add(new SelectListItem()
                            {
                                Text = item.Country1,
                                Value = item.CountryID.ToString(),
                                Selected = (item.CountryID.ToString().Trim() == defaultCountry.Trim() ? true : false)
                            });
                        }

                        var rows = (from myRow in db.EventTypes
                                    select myRow).ToList();
                        List<SelectListItem> EventType = new List<SelectListItem>();
                        EventType.Add(new SelectListItem()
                        {
                            Text = "Select",
                            Value = "0",
                            Selected = true
                        });
                        foreach (var item in rows)
                        {
                            if (objCr.EventTypeID == item.EventTypeID)
                            {
                                EventType.Add(new SelectListItem()
                                {
                                    Text = item.EventType1,
                                    Value = item.EventTypeID.ToString(),
                                    Selected = true

                                });
                            }
                            else
                            {
                                EventType.Add(new SelectListItem()
                                {
                                    Text = item.EventType1,
                                    Value = item.EventTypeID.ToString()
                                });
                            }
                        }


                        var EventCat = (from myRow in db.EventCategories
                                        select myRow).ToList();
                        List<SelectListItem> EventCategory = new List<SelectListItem>();
                        EventCategory.Add(new SelectListItem()
                        {
                            Text = "Select",
                            Value = "0",
                            Selected = true
                        });
                        foreach (var item in EventCat)
                        {
                            if (item.EventCategoryID == objCr.EventCategoryID)
                            {
                                EventCategory.Add(new SelectListItem()
                                {
                                    Text = item.EventCategory1,
                                    Value = item.EventCategoryID.ToString(),
                                    Selected = true
                                });
                            }
                            else
                            {
                                EventCategory.Add(new SelectListItem()
                                {
                                    Text = item.EventCategory1,
                                    Value = item.EventCategoryID.ToString()

                                });
                            }
                        }


                        ViewBag.CountryID = countryList;
                        ViewBag.EventType = EventType;
                        ViewBag.ddlEventCategory = EventCategory;

                    }


                    EventCreation ObjEV = new EventCreation();
                    ObjEV.EventTitle = "Form Editing";
                }catch(Exception ex)
                {
                  logger.Error("Exception during request processing", ex);
                }


                return View(objCr);
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }

      
        public ActionResult ModifyEvent(string Eventid)
        {
            EventCreation objCr = new EventCreation();
            if ((Session["AppId"] != null))
            {
                try {
                    var vRefferer = ControllerContext.HttpContext.Request.UrlReferrer;
                    if (vRefferer == null)
                    {
                        TempData["IsNewEvent"] = "N";
                    }
                    else
                    {
                        if (vRefferer.ToString().ToLower().Contains("createevent") == true)
                            TempData["IsNewEvent"] = "Y";
                        else if (vRefferer.ToString().ToLower().Contains("modifyevent") == true)
                            TempData["IsNewEvent"] = "M";
                        else
                            TempData["IsNewEvent"] = "N";
                    }

                    string LgUser = Session["AppId"].ToString();
                    using (EventComboEntities db = new EventComboEntities())
                    {
                        AspNetUser aspuser = db.AspNetUsers.First(i => i.Id == LgUser);

                        aspuser.LastLoginTime = System.DateTime.UtcNow;
                        db.SaveChanges();


                    }

                    long EventIid = 0;
                    string Isadmin = "N";
                    if (Eventid.Contains("ß"))
                    {
                        var res = Eventid.Split('ß');

                        EventIid = long.Parse(res[0]);
                        Isadmin = "Y";
                    }
                    else
                    {
                        EventIid = long.Parse(Eventid);
                    }

                    ValidationMessage vmc = new ValidationMessage();
                    EventIid = vmc.GetLatestEventId(EventIid);
                     objCr = GetEventDataEditing(EventIid);
                    if (objCr == null) return RedirectToAction("Index", "Home");

                    if (Session["AppId"].ToString().Trim() != objCr.UserID.Trim())
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    MyAccount hmc = new MyAccount();
                  
                    string usernme = hmc.getusername();
                    if (string.IsNullOrEmpty(usernme))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                 
                      Session["logo"] = "events";
                    Session["Fromname"] = "events";
                    var url = Url.Action("CreateEvent", "CreateEvent");
                    Session["ReturnUrl"] = "CreateEvent~" + url;
                   
                    string defaultCountry = "";
                    string timezone = objCr.TimeZone != null ? objCr.TimeZone : "31";
                    using (EventComboEntities db = new EventComboEntities())
                    {
                        var Timezone = (from c in db.TimeZoneDetails  select c).OrderBy(x => x.Timezone_order);
                        List<SelectListItem> Timezonelist = new List<SelectListItem>();
                       
                        foreach (var item in Timezone)
                        {
                            Timezonelist.Add(new SelectListItem()
                            {
                                Text = item.TimeZone_Name.ToString(),
                                Value = item.TimeZone_Id.ToString(),
                                Selected = (item.TimeZone_Id.ToString().Trim() == timezone.Trim() ? true : false)

                            });
                        }

                        var countryQuery = (from c in db.Countries
                                            orderby c.Country1 ascending
                                            select c).Distinct();
                        List<SelectListItem> countryList = new List<SelectListItem>();
                        defaultCountry = "United States";

                        foreach (var item in countryQuery)
                        {
                            countryList.Add(new SelectListItem()
                            {
                                Text = item.Country1,
                                Value = item.CountryID.ToString(),
                                Selected = (item.CountryID.ToString().Trim() == defaultCountry.Trim() ? true : false)
                            });
                        }

                        var rows = (from myRow in db.EventTypes
                                    where myRow.EventHide == "N" || string.IsNullOrEmpty(myRow.EventHide)
                                    orderby myRow.EventType1
                                    select myRow).ToList();
                        List<SelectListItem> EventType = new List<SelectListItem>();
                        EventType.Add(new SelectListItem()
                        {
                            Text = "Select",
                            Value = "0",
                            Selected = true
                        });
                        foreach (var item in rows)
                        {
                            if (objCr.EventTypeID == item.EventTypeID)
                            {
                                EventType.Add(new SelectListItem()
                                {
                                    Text = item.EventType1,
                                    Value = item.EventTypeID.ToString(),
                                    Selected = true

                                });
                            }
                            else
                            {
                                EventType.Add(new SelectListItem()
                                {
                                    Text = item.EventType1,
                                    Value = item.EventTypeID.ToString()
                                });
                            }
                        }


                        var EventCat = (from myRow in db.EventCategories
                                        orderby myRow.EventCategory1
                                        select myRow).ToList();
                        List<SelectListItem> EventCategory = new List<SelectListItem>();
                        EventCategory.Add(new SelectListItem()
                        {
                            Text = "Select",
                            Value = "0",
                            Selected = true
                        });
                        foreach (var item in EventCat)
                        {
                            if (item.EventCategoryID == objCr.EventCategoryID)
                            {
                                EventCategory.Add(new SelectListItem()
                                {
                                    Text = item.EventCategory1,
                                    Value = item.EventCategoryID.ToString(),
                                    Selected = true
                                });
                            }
                            else
                            {
                                EventCategory.Add(new SelectListItem()
                                {
                                    Text = item.EventCategory1,
                                    Value = item.EventCategoryID.ToString()

                                });
                            }
                        }


                        ViewBag.CountryID = countryList;
                        ViewBag.EventType = EventType;
                        ViewBag.ddlEventCategory = EventCategory;
                        ViewBag.Timezonelist = Timezonelist;
                        var Images = (from myEvent in db.EventImages
                                      where myEvent.EventID == EventIid
                                      select myEvent).ToList();

                        List<image> eList = new List<image>();
                        if (Images != null)
                        {
                            int count = Images.Count;
                            int k = 0;

                            foreach (EventImage Objimg in Images)
                            {

                                var path = "/Images/events/event_flyers/imagepath/" + Objimg.EventImageUrl;
                                image img = new image();
                                img.type = Objimg.ImageType;
                                img.name = Objimg.EventImageUrl;
                                img.file = path;
                                eList.Add(img);


                            }

                        }
                        var javaScriptSerializer = new
        System.Web.Script.Serialization.JavaScriptSerializer();
                        string jsonString = javaScriptSerializer.Serialize(eList);

                        ViewData["Image"] = jsonString.ToString();


                    }


                    EventCreation ObjEV = new EventCreation();
                    ObjEV.EventTitle = "Form Editing";
                    objCr.Isadmin = Isadmin;
                }catch(Exception ex)
                {
                  logger.Error("Exception during request processing", ex);
                }

                return View(objCr);
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }


        public string GetPreviousAddress(long lPreAddId)
        {
            string strUsers = (Session["AppId"] != null ? Session["AppId"].ToString() : "");

            StringBuilder strHtml = new StringBuilder();
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var PrevAdd = (from myRow in objEnt.Addresses
                                   where myRow.UserId == strUsers
                                   select myRow).ToList();

                    strHtml.Append("<option value='0'>Select Past Location</option>");
                    foreach (var item in PrevAdd)
                    {
                        if (item.AddressID == lPreAddId)
                        {
                            if (item.ConsolidateAddress != null && item.ConsolidateAddress.Trim() != "")
                                strHtml.Append("<option selected='selected' value=" + item.AddressID.ToString() + ">" + item.ConsolidateAddress + "</option>");
                        }
                        else
                        {
                            if (item.ConsolidateAddress != null && item.ConsolidateAddress.Trim() != "")
                                    strHtml.Append("<option value=" + item.AddressID.ToString() + ">" + item.ConsolidateAddress + "</option>");
                        }
                    }
                    return strHtml.ToString();
                }
            }
            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
              return strHtml.ToString();

            }


        }

        public string GetSubCat(long lECatId,long lSubCat)
        {

            StringBuilder strHtml = new StringBuilder();
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var EventCat = (from myRow in objEnt.EventSubCategories
                                    where myRow.EventCategoryID == lECatId
                                    select myRow).ToList();

                    //strHtml.Append("< option value =0 selected=true>Select</ option > ");
                    strHtml.Append("<option value=0>Select</option>");
                    foreach (var item in EventCat)
                    {
                        if (lSubCat == item.EventSubCategoryID)
                            strHtml.Append("<option selected='selected' value=" + item.EventSubCategoryID.ToString() + ">" + item.EventSubCategory1 + "</option>");
                        else
                            strHtml.Append("<option value=" + item.EventSubCategoryID.ToString() + ">" + item.EventSubCategory1 + "</option>");
                    }
                    return strHtml.ToString();
                }
            }
            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
              return strHtml.ToString();

            }


        }

     

        public string CheckEventUrl(string strUserUrl)
        {
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var Eventurl = (from myRow in objEnt.Events
                                    where myRow.EventUrl == strUserUrl
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
              logger.Error("Exception during request processing", ex);
              return "Y";

            }
        }

        public long SaveEvent(EventCreation model)
        {
            long lEventId = 0;
            string lat = "", lon = "";
            ViewEvent vc = new ViewEvent();
            DateTimeWithZone dtzstart, dtzend;
            DateTimeWithZone dtzCreated;
            try
            {
                string strUserId = (Session["AppId"] != null ? Session["AppId"].ToString() : "");


                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    Event ObjEC = new Event();
                    ObjEC.EventTypeID = model.EventTypeID;
                    ObjEC.EventCategoryID = model.EventCategoryID;
                    ObjEC.EventSubCategoryID = model.EventSubCategoryID;
                    ObjEC.UserID = strUserId;
                    ObjEC.EventTitle = model.EventTitle;
                    ObjEC.DisplayStartTime = model.DisplayStartTime;
                    ObjEC.DisplayEndTime = model.DisplayEndTime;
                    ObjEC.DisplayTimeZone = model.DisplayTimeZone;
                    ObjEC.EventDescription = model.EventDescription;
                    ObjEC.EventPrivacy = model.EventPrivacy;
                    ObjEC.Private_ShareOnFB = model.Private_ShareOnFB;
                    ObjEC.Private_GuestOnly = model.Private_GuestOnly;
                    ObjEC.Private_Password = model.Private_Password;
                    ObjEC.EventUrl = model.EventUrl;
                    ObjEC.PublishOnFB = model.PublishOnFB;
                    ObjEC.EventStatus = model.EventStatus;
                    ObjEC.IsMultipleEvent = model.IsMultipleEvent;
                    ObjEC.TimeZone = model.TimeZone;
                    ObjEC.DisplayStartTime = model.DisplayStartTime;
                    ObjEC.DisplayEndTime = model.DisplayEndTime;
                    ObjEC.DisplayTimeZone = model.DisplayTimeZone;
                    ObjEC.FBUrl = model.FBUrl;
                    ObjEC.TwitterUrl = model.TwitterUrl;
                    ObjEC.LastLocationAddress = model.LastLocationAddress;
                    ObjEC.AddressStatus = model.AddressStatus;
                    ObjEC.EnableFBDiscussion = model.EnableFBDiscussion;
                    ObjEC.Ticket_DAdress = model.Ticket_DAdress;
                    ObjEC.Ticket_showremain = model.Ticket_showremain;
                    ObjEC.Ticket_showvariable = model.Ticket_showvariable;
                    ObjEC.Ticket_variabledesc = model.Ticket_variabledesc;
                    ObjEC.Ticket_variabletype = model.Ticket_variabletype;
                    var Timezonedetail = (from ev in db.TimeZoneDetails where ev.TimeZone_Id.ToString() == model.TimeZone select ev).FirstOrDefault();
                    if (Timezonedetail != null)
                    {

                        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Timezonedetail.TimeZone);
                        
                        dtzCreated = new DateTimeWithZone(DateTime.Now, userTimeZone, false);
                    }
                    else
                    {
                        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                        dtzCreated = new DateTimeWithZone(DateTime.Now, userTimeZone, false);
                    }
                    ObjEC.CreateDate = dtzCreated.UniversalTime;
                    objEnt.Events.Add(ObjEC);
                    // Address info
                    if (model.AddressDetail != null)
                    {
                        Address ObjAdd = new Models.Address();
                        foreach (Address objA in model.AddressDetail)
                        {
                            if ((objA.VenueName != null && objA.VenueName.Trim() != "") || objA.ConsolidateAddress != "")
                            {
                                try
                                {
                                    var geocode = vc.Geocode(objA.ConsolidateAddress);
                                    lat = geocode.Latitude.ToString();
                                    lon = geocode.Longitude.ToString();
                                }
                                catch (Exception ex)
                                {
                                    lat = "";
                                    lon = "";

                                }

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
                                ObjAdd.Latitude = lat;
                                ObjAdd.Longitude = lon;
                                objEnt.Addresses.Add(ObjAdd);
                            }

                        }
                    }
                    // Event on Single Timing 
                    if (model.EventVenue != null)
                    {
                        EventVenue objEVenue = new EventVenue();
                        foreach (EventVenue objEv in model.EventVenue)
                        {
                            objEVenue = new EventVenue();
                            objEVenue.EventID = ObjEC.EventID;
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
                            //
                            objEVenue.E_Startdate = dtzstart.UniversalTime;
                            objEVenue.E_Enddate = dtzend.UniversalTime;
                            objEVenue.EventStartDate = objEv.EventStartDate;
                            objEVenue.EventEndDate = objEv.EventEndDate;
                            objEVenue.EventStartTime = objEv.EventStartTime;
                            objEVenue.EventEndTime = objEv.EventEndTime;
                            objEnt.EventVenues.Add(objEVenue);
                        }
                    }
                    // Event on Multiple timing 
                    if (model.MultipleEvents != null)
                    {
                        MultipleEvent objMEvents = new MultipleEvent();
                        foreach (MultipleEvent objME in model.MultipleEvents)
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
                    // Orgnizer
                    if (model.Orgnizer != null)
                    {
                        Organizer_Master objEOrg = new Organizer_Master();
                        Event_Orgnizer_Detail evntorg = new Event_Orgnizer_Detail();
                        var neworg = model.Orgnizer.Where(x => x.Orgnizer_Id == 0).Any();
                        if (neworg)
                        {
                            var defaultorg = model.Orgnizer.Where(x => x.Orgnizer_Id == 0 && x.DefaultOrg == "Y").Any();
                            if (defaultorg)
                            {
                                var orgnew = model.Orgnizer.Where(x => x.Orgnizer_Id == 0).ToList();
                                foreach (Organizer_Master objOr in orgnew)
                                {

                                    objEOrg = new Organizer_Master();
                                    // objEOrg.Orgnizer_Event_Id = ObjEC.EventID;
                                    objEOrg.Orgnizer_Name = objOr.Orgnizer_Name;
                                    objEOrg.Organizer_Desc = HttpUtility.UrlDecode(objOr.Organizer_Desc, System.Text.Encoding.Default);
                                    objEOrg.Organizer_FBLink = objOr.Organizer_FBLink;
                                    objEOrg.Organizer_Twitter = objOr.Organizer_Twitter;
                                    //objEOrg.DefaultOrg = objOr.DefaultOrg;
                                    objEOrg.Organizer_Linkedin = objOr.Organizer_Linkedin;
                                    objEOrg.UserId = strUserId;
                                    objEnt.Organizer_Master.Add(objEOrg);
                                    if (objOr.DefaultOrg == "Y")
                                    {
                                        evntorg = new Event_Orgnizer_Detail();
                                        evntorg.DefaultOrg = objOr.DefaultOrg;
                                        evntorg.Orgnizer_Id = objEOrg.Orgnizer_Id;
                                        evntorg.Orgnizer_Event_Id = ObjEC.EventID;
                                        evntorg.UserId = strUserId;
                                        objEnt.Event_Orgnizer_Detail.Add(evntorg);
                                    }



                                }
                            }
                            else
                            {
                                var orgnew = model.Orgnizer.Where(x => x.Orgnizer_Id == 0).ToList();
                                foreach (Organizer_Master objOr in orgnew)
                                {

                                    objEOrg = new Organizer_Master();
                                    // objEOrg.Orgnizer_Event_Id = ObjEC.EventID;
                                    objEOrg.Orgnizer_Name = objOr.Orgnizer_Name;
                                    objEOrg.Organizer_Desc = HttpUtility.UrlDecode(objOr.Organizer_Desc, System.Text.Encoding.Default);
                                    objEOrg.Organizer_FBLink = objOr.Organizer_FBLink;
                                    objEOrg.Organizer_Twitter = objOr.Organizer_Twitter;
                                    //objEOrg.DefaultOrg = objOr.DefaultOrg;
                                    objEOrg.Organizer_Linkedin = objOr.Organizer_Linkedin;
                                    objEOrg.UserId = strUserId;
                                    objEnt.Organizer_Master.Add(objEOrg);




                                }
                                var defaultorgold = model.Orgnizer.Where(x => x.DefaultOrg == "Y").ToList();
                                foreach (Organizer_Master objOr in defaultorgold)
                                {
                                    evntorg = new Event_Orgnizer_Detail();
                                    evntorg.DefaultOrg = objOr.DefaultOrg;
                                    evntorg.Orgnizer_Id = objOr.Orgnizer_Id;
                                    evntorg.Orgnizer_Event_Id = ObjEC.EventID;
                                    evntorg.UserId = strUserId;
                                    objEnt.Event_Orgnizer_Detail.Add(evntorg);
                                }
                                var editoldorg = model.Orgnizer.Where(x => x.EditOrg == "1").ToList();
                                if (editoldorg != null)
                                {
                                    foreach (Organizer_Master objOr in editoldorg)
                                    {
                                        string sql = @"update Organizer_Master set Orgnizer_Name={0},Organizer_Desc={1},Organizer_FBLink={2},Organizer_Twitter={3},Organizer_Linkedin={4} where Orgnizer_Id={5}";
                                        List<Object> sqlParamsList = new List<object>();
                                        sqlParamsList.Add(objOr.Orgnizer_Name);
                                        sqlParamsList.Add(HttpUtility.UrlDecode(objOr.Organizer_Desc, System.Text.Encoding.Default));
                                        sqlParamsList.Add(objOr.Organizer_FBLink);
                                        sqlParamsList.Add(objOr.Organizer_Twitter);
                                        sqlParamsList.Add(objOr.Organizer_Linkedin);
                                        sqlParamsList.Add(objOr.Orgnizer_Id);

                                        objEnt.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());
                                    }
                                }


                            }

                        }
                        else
                        {
                            var defaultorg = model.Orgnizer.Where(x => x.DefaultOrg == "Y").ToList();
                            foreach (Organizer_Master objOr in defaultorg)
                            {
                                evntorg = new Event_Orgnizer_Detail();
                                evntorg.DefaultOrg = objOr.DefaultOrg;
                                evntorg.Orgnizer_Id = objOr.Orgnizer_Id;
                                evntorg.Orgnizer_Event_Id = ObjEC.EventID;
                                evntorg.UserId = strUserId;
                                objEnt.Event_Orgnizer_Detail.Add(evntorg);
                            }

                            var editoldorg = model.Orgnizer.Where(x => x.EditOrg == "1").ToList();
                            if (editoldorg != null)
                            {
                                foreach (Organizer_Master objOr in editoldorg)
                                {
                                    string sql = @"update Organizer_Master set Orgnizer_Name={0},Organizer_Desc={1},Organizer_FBLink={2},Organizer_Twitter={3},Organizer_Linkedin={4} where Orgnizer_Id={5}";
                                    List<Object> sqlParamsList = new List<object>();
                                    sqlParamsList.Add(objOr.Orgnizer_Name);
                                    sqlParamsList.Add(HttpUtility.UrlDecode(objOr.Organizer_Desc, System.Text.Encoding.Default));
                                    sqlParamsList.Add(objOr.Organizer_FBLink);
                                    sqlParamsList.Add(objOr.Organizer_Twitter);
                                    sqlParamsList.Add(objOr.Organizer_Linkedin);
                                    sqlParamsList.Add(objOr.Orgnizer_Id);

                                    objEnt.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());
                                }
                            }
                        }
                    }
                    // Tickets

                    if (model.Ticket != null)
                    {
                        Ticket ticket = new Ticket();
                        foreach (Ticket tick in model.Ticket)
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


                    if (model.EventImage != null)
                    {
                        EventImage Image = new EventImage();
                        foreach (EventImage img in model.EventImage)
                        {
                            Image = new EventImage();
                            Image.EventID = ObjEC.EventID;
                            Image.EventImageUrl = img.EventImageUrl;
                            Image.ImageType = img.ImageType;
                            objEnt.EventImages.Add(Image);

                        }
                    }

                    if (model.EventVariable != null)
                    {
                        Event_VariableDesc var = new Event_VariableDesc();
                        foreach (Event_VariableDesc variable in model.EventVariable)
                        {
                            var = new Event_VariableDesc();
                            var.Event_Id = ObjEC.EventID;
                            var.VariableDesc = variable.VariableDesc;
                            var.Price = variable.Price;
                            objEnt.Event_VariableDesc.Add(var);

                        }
                    }

                    objEnt.SaveChanges();
                    lEventId = ObjEC.EventID;
                }
            }
            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
              return 0;
            }
            return lEventId;
        }


        #region EditingEvent

        
        public EventCreation GetEventDataEditing(long lEventId)
        {
            //EventCreation objEC = new EventCreation();

            using (EventComboEntities objEnt = new EventComboEntities())
            {
                var vEC = (from myEvent in objEnt.Events
                           where myEvent.EventID == lEventId
                           select new EventCreation
                           {
                               EventID = myEvent.EventID,
                               EventTypeID = myEvent.EventTypeID,
                               EventCategoryID = myEvent.EventCategoryID,
                               EventSubCategoryID = myEvent.EventSubCategoryID,
                               UserID = myEvent.UserID,
                               EventTitle = myEvent.EventTitle,
                               DisplayStartTime = myEvent.DisplayStartTime,
                               DisplayEndTime = myEvent.DisplayEndTime,
                               DisplayTimeZone = myEvent.DisplayTimeZone,
                               EventDescription = myEvent.EventDescription,
                               EventPrivacy = myEvent.EventPrivacy,
                               Private_ShareOnFB = myEvent.Private_ShareOnFB,
                               Private_GuestOnly = myEvent.Private_GuestOnly,
                               Private_Password = myEvent.Private_Password,
                               EventUrl = myEvent.EventUrl,
                               PublishOnFB = myEvent.PublishOnFB,
                               EventStatus = myEvent.EventStatus,
                               IsMultipleEvent = myEvent.IsMultipleEvent,
                               TimeZone = myEvent.TimeZone,
                               FBUrl = myEvent.FBUrl,
                               TwitterUrl = myEvent.TwitterUrl,
                               AddressStatus = myEvent.AddressStatus,
                               LastLocationAddress = myEvent.LastLocationAddress,
                               EnableFBDiscussion = myEvent.EnableFBDiscussion,
                               Ticket_DAdress = myEvent.Ticket_DAdress,
                               Ticket_showremain = myEvent.Ticket_showremain,
                               Ticket_showvariable = myEvent.Ticket_showvariable,
                               Ticket_variabledesc = myEvent.Ticket_variabledesc,
                               Ticket_variabletype = myEvent.Ticket_variabletype,
                               ModifyDate = (myEvent.ModifyDate.ToString().Trim() != "" ? myEvent.ModifyDate.ToString().Trim() : myEvent.CreateDate.ToString().Trim()) ,
                               ShowMap =myEvent.ShowMap,
                               Parent_EventID = myEvent.Parent_EventID
                           }
                        ).FirstOrDefault();
             
                var Timezonedetail = (from ev in db.TimeZoneDetails where ev.TimeZone_Id.ToString() == vEC.TimeZone select ev).FirstOrDefault();
                DateTimeWithZone dtzCreated;
                if (Timezonedetail != null)
                {


                    TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Timezonedetail.TimeZone);

                    dtzCreated = new DateTimeWithZone(Convert.ToDateTime(vEC.ModifyDate), userTimeZone,true);
                    //Timezone value

                }
                else
                {
                    TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    dtzCreated = new DateTimeWithZone(Convert.ToDateTime(vEC.ModifyDate), userTimeZone,true);
                }
                vEC.ModifyDate = "(Last Saved at " + dtzCreated.LocalTime + ")";
                return vEC;
            }
        }
        [HttpGet]
        public ActionResult GetEventChildData(string lEventId)
        {
            long EventIid = 0;
            string Isadmin = "N";
            if (lEventId.Contains("ß"))
            {
                var res = lEventId.Split('ß');

                EventIid = long.Parse(res[0]);
                Isadmin = "Y";
            }
            else
            {
                EventIid = long.Parse(lEventId);
            }
            GetEventData objJson = new GetEventData();
            using (EventComboEntities objEnt = new EventComboEntities())
            {
                try {
                    var Event = (from myEnt in objEnt.Events where myEnt.EventID == EventIid select myEnt).FirstOrDefault();

                    var ev = (from myEvent in objEnt.EventVenues
                              where myEvent.EventID == EventIid
                              select myEvent).FirstOrDefault();
                    int timeZoneID = Int32.Parse(Event.TimeZone);
                    TimeZoneDetail td = objEnt.TimeZoneDetails.First(x => x.TimeZone_Id == timeZoneID);
                    DateTimeWithZone dtzstart, dzend;
                    DateTimeWithZone dtzcreated;
                    if (ev != null)
                    {

                        if (td != null)
                        {
                            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td.TimeZone);
                            dtzstart = new DateTimeWithZone(Convert.ToDateTime(ev.E_Startdate), userTimeZone, true);
                            dzend = new DateTimeWithZone(Convert.ToDateTime(ev.E_Enddate), userTimeZone, true);
                        }
                        else
                        {
                            TimeZoneInfo userTimeZone =TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                            dtzstart = new DateTimeWithZone(Convert.ToDateTime(ev.E_Startdate), userTimeZone, true);
                            dzend = new DateTimeWithZone(Convert.ToDateTime(ev.E_Enddate), userTimeZone, true);
                        }
                        var start_date = dtzstart.LocalTime;
                        DateTime startdateOnly = start_date.Date;
                        var startdate = startdateOnly.ToString("MM/dd/yyyy");
                        var starttime = start_date.ToString("h:mm tt").ToLower().Trim().Replace(" ", ""); ;
                        var end_date = dzend.LocalTime;
                        DateTime enddateOnly = end_date.Date;
                        var enddate = enddateOnly.ToString("MM/dd/yyyy");
                        var endtime= end_date.ToString("h:mm tt").ToLower().Trim().Replace(" ", ""); ;
                        objJson.EventID = Event.EventID;
                        objJson.EventStartDate = startdate;
                        objJson.EventStartTime = starttime;
                        objJson.EventEndDate = enddate;
                        objJson.EventEndTime = endtime;
                        objJson.EventVenueID = ev.EventVenueID;

                        objJson.MultipleSchTime = "S";
                    }




                    var Mv = (from myEvent in objEnt.MultipleEvents
                              where myEvent.EventID == EventIid
                              select myEvent).FirstOrDefault();

                    if (Mv != null)
                    {

                        if (td != null)
                        {
                            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td.TimeZone);
                            dtzstart = new DateTimeWithZone(Convert.ToDateTime(Mv.M_Startfrom), userTimeZone, true);
                            dzend = new DateTimeWithZone(Convert.ToDateTime(Mv.M_StartTo), userTimeZone, true);
                        }
                        else
                        {
                            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                            dtzstart = new DateTimeWithZone(Convert.ToDateTime(Mv.M_Startfrom), userTimeZone, true);
                            dzend = new DateTimeWithZone(Convert.ToDateTime(Mv.M_StartTo), userTimeZone, true);
                        }
                        var start_date = dtzstart.LocalTime;
                        DateTime startdateOnly = start_date.Date;
                        var startdate = startdateOnly.ToString("MM/dd/yyyy");
                        var starttime = start_date.ToString("h:mm tt").ToLower().Trim();
                        var end_date = dzend.LocalTime;
                        DateTime enddateOnly = end_date.Date;
                        var enddate = enddateOnly.ToString("MM/dd/yyyy");
                        var endtime = end_date.ToString("h:mm tt").ToLower().Trim();
                        objJson.Frequency = Mv.Frequency;
                        objJson.WeeklyDay = Mv.WeeklyDay;
                        objJson.MonthlyDay = Mv.MonthlyDay;
                        objJson.MonthlyWeek = Mv.MonthlyWeek;
                        objJson.MonthlyWeekDays = Mv.MonthlyWeekDays;
                        objJson.StartingFrom = startdate;
                        objJson.StartingTo = enddate;
                        objJson.StartTime = starttime;
                        objJson.EndTime = endtime;
                        objJson.MultipleSchTime = "M";
                    }


                    var Addr = (from myEvent in objEnt.Addresses
                                where myEvent.EventId == EventIid
                                select myEvent).ToList();
                    StringBuilder strHTML = new StringBuilder();
                    int i = 0;
                    foreach (Address objAdd in Addr)
                    {
                        i = i + 1;
                        if (i == 1) { objJson.FirstAddress = objAdd.ConsolidateAddress; }
                        strHTML.Append("<tr>");
                        strHTML.Append("<td style='display: none'>");
                        strHTML.Append(i);
                        strHTML.Append("</td>");

                        strHTML.Append("<td style='display: none'>  <input type = 'hidden' id = VenueId" + i.ToString() + " value = '" + objAdd.AddressID + "' /></td>");
                        strHTML.Append("<td style='display: none'> <input type='text' name='' id=VenueName" + i.ToString() + " style='width: 100px;'  value='" + objAdd.VenueName + "' /></td>");
                        strHTML.Append("<td style='display: none'> <input type='text' name='' id=Address1" + i.ToString() + " style='width: 100px;'  value='" + objAdd.Address1 + "' /></td>");
                        strHTML.Append("<td style='display: none'> <input type='text' name='' id=Address2" + i.ToString() + " style='width: 100px;'  value='" + objAdd.Address2 + "' /></td>");
                        strHTML.Append("<td style='display: none'> <input type='text' name='' id=City" + i.ToString() + " style='width: 100px;'  value='" + objAdd.City + "' /></td>");
                        strHTML.Append("<td style='display: none'> <input type='text' name='' id=State" + i.ToString() + " style='width: 100px;'  value='" + objAdd.State + "' /></td>");
                        strHTML.Append("<td style='display: none'> <input type='text' name='' id=Zip" + i.ToString() + " style='width: 100px;'  value='" + objAdd.Zip + "' /></td>");
                        strHTML.Append("<td style='display: none'> <input type='text' name='' id=CountryID" + i.ToString() + " style='width: 100px;'  value='" + objAdd.CountryID + "' /></td>");
                        strHTML.Append("<td style='display: none'> <input type='text' name='' id=CID" + i.ToString() + " style='width: 100px;'  value='" + objAdd.CountryID + "' /></td>");
                        strHTML.Append("<td align='left'><label id=consolidate" + i.ToString() + ">" + objAdd.ConsolidateAddress + "</label></td>");
                        strHTML.Append("<td><div class='trigger mt5 ent_add'><a href = '#' onclick='editRow(" + i.ToString() + ");'><i class='fa fa-map-marker'></i> Edit</a><a href = '#' id='btAddDelete' onclick='DeleteTableRow(" + i.ToString() + ")'>Delete</a> </div> </td>");
                        strHTML.Append("</tr>");

                        //strHTML = strHTML + '<td style="display:none"> <input type="text" name=" " id="' + CellId + '" style="width:100px;" value="' + ColAry[i] + '" /></td>';
                    }
                    objJson.EventID = Event.EventID;
                    objJson.AddressStatus = Event.AddressStatus;
                    objJson.Addresses = strHTML.ToString();
                    var descripotino= new HtmlString(Server.HtmlDecode(Event.EventDescription));
                    objJson.EventDescription = descripotino.ToString();
                    objJson.EventStatus = Event.EventStatus;
                    objJson.timezone = Event.TimeZone;
                    //Tickets Section
                    long capacity = 00000;
                    decimal? percent = 0m;
                    decimal? amount = 0m;
                    var feestruct = (from db in objEnt.Fee_Structure select db).FirstOrDefault();
                    if (feestruct != null)
                    {
                        percent = (feestruct.FS_Percentage != null ? feestruct.FS_Percentage : 5);
                        amount = (feestruct.FS_Amount != null ? feestruct.FS_Amount : 0.99m);

                    }
                    else
                    {
                        percent = 5;
                        amount = 0.99m;

                    }
                    var Tick = (from myEvent in objEnt.Tickets
                                where myEvent.E_Id == EventIid
                                select myEvent).ToList();

                    StringBuilder strticketHtml = new StringBuilder();
                    int j = 0;
                    foreach (Ticket ObjTick in Tick)
                    {
                        var type = "";
                        var fee = "";
                        var ecfee = "";
                        var total = "";
                        var custdate = "0";
                        var Price = "";
                        var discount = "";
                        var customerfee = "";
                        var totaoln = "";
                        string ecfeepercentage = "", ecamount = "";
                        var startdate = String.Format("{0:MM/dd/yyyy}", ObjTick.Sale_Start_Date);
                        var enddate = String.Format("{0:MM/dd/yyyy}", ObjTick.Sale_End_Date);
                        var untilldate = String.Format("{0:MM/dd/yyyy}", ObjTick.Hide_Untill_Date);
                        var afterdate = String.Format("{0:MM/dd/yyyy}", ObjTick.Hide_After_Date);
                        capacity += ObjTick.Qty_Available;
                        if (ObjTick.Price != null)
                        {
                            Price = String.Format("{0:#,###,##0.00}", ObjTick.Price);

                        }
                        if (ObjTick.T_Discount != null)
                        {
                            discount = String.Format("{0:#,###,##0.00}", ObjTick.T_Discount);

                        }
                        if (ObjTick.TotalPrice != null)
                        {
                            totaoln = String.Format("{0:#,###,##0.00}", ObjTick.TotalPrice);
                        }
                        if (ObjTick.TicketTypeID == 1)
                        {
                            ecfee = "0";
                            fee = "0";
                            customerfee = "0";
                            type = "Free";
                            total = "0";
                            ecfeepercentage = "0";
                            ecamount = "0";
                        }
                        if (ObjTick.TicketTypeID == 2)
                        {

                            total = ObjTick.TotalPrice.ToString();
                            var ecfeenew = 0m;
                            if (ObjTick.EC_Fee != null)
                            {
                                ecfeenew = ((decimal.Parse(Price) * percent) / 100) + amount ?? 0;

                            }

                            ecfee = String.Format("{0:#,###,##0.00}", (ObjTick.EC_Fee != null ? ObjTick.EC_Fee : ecfeenew));

                            type = "Paid";
                        }
                        if (ObjTick.TicketTypeID == 3)
                        {


                            ecfee = "0";
                            fee = "0";
                            type = "Donate";
                        }
                        if (ObjTick.TicketTypeID != 1)
                        {
                            customerfee = String.Format("{0:#,###,##0.00}", (ObjTick.Customer_Fee != null ? ObjTick.Customer_Fee : 0));

                            ecfeepercentage = (ObjTick.T_Ecpercent != null ? ObjTick.T_Ecpercent.ToString() : percent.ToString());
                            ecamount = String.Format("{0:#,###,##0.00}", (ObjTick.T_EcAmount != null ? ObjTick.T_EcAmount : amount));
                        }
                        strticketHtml.Append("<div id='clonediv-" + j + "' class='ticket_haeding ev_ticket_haeding mt10 pb10' tabindex='-1'>");
                        strticketHtml.Append("<div class='col-sm-10 col-xs-12' ><div class='col-sm-1 text-center no_pad ev_row_mov'>");
                        strticketHtml.Append("<span class='ev_row_icn'><i class='fa fa-ellipsis-v'></i></span>");
                        strticketHtml.Append("<input type='hidden' id='id_ticket_id-" + j + "'  value='" + ObjTick.T_Id + "'/>");
                        strticketHtml.Append("<input type='hidden' id='id_ecfeeback_id-" + j + "'  value='" + ecfee + "'/>");
                        strticketHtml.Append("<input type='hidden' id='id_ecpercentback_id-" + j + "'  value='" + ecfeepercentage + "'/>");
                        strticketHtml.Append("<input type='hidden' id='id_ecammountback_id-" + j + "'  value='" + ecamount + "'/>");
                        strticketHtml.Append("<input type='hidden' id='id_order-" + j + "' value='" + ObjTick.T_order + "' />");
                        strticketHtml.Append("<input type='hidden' id='id_Tickettype-" + j + "' value=" + type + " />");
                        strticketHtml.Append("<input type='hidden' id=id_fee-" + j + " value=" + customerfee + " />");
                        strticketHtml.Append("<input type='hidden' id='id_total-" + j + "' value=" + total + " />");
                        strticketHtml.Append("<input type='hidden' id='id_customize-" + j + "' value=" + (ObjTick.T_Customize != null ? ObjTick.T_Customize : "0") + " />");

                        if (ObjTick.TicketTypeID == 2)
                        {
                            strticketHtml.Append("<input type='hidden' id='id_feetype-" + j + "' value='" + ObjTick.Fees_Type + "' />");
                        }
                        else
                        {
                            strticketHtml.Append("<input type='hidden' value='0' id='id_feetype-" + j + "' />");
                        }
                        strticketHtml.Append("<input type='text' value='0 id=id_totalamt-" + j + "' hidden /> ");
                        strticketHtml.Append("<input type='text' value='0' id='id_msg-" + j + "' hidden />");
                        strticketHtml.Append("</div>");
                        strticketHtml.Append("<div class='col-sm-3 no_pad ipad-50'>");
                        strticketHtml.Append("<div class='form-group'>");
                        strticketHtml.Append("<label class='col-sm-4 control-label ev_tickt_lebel xs-tkt-typ-pad-0'>Ticket</label>");
                        strticketHtml.Append("<div class='col-sm-8 xs-tkt-typ-pad-0'>");
                        if (ObjTick.TicketTypeID == 3)
                        {
                            strticketHtml.Append("<input type='text' class='form-control evnt_inp_cont chkvalidation' id='id_ticket_type-" + j + "' placeholder='Dontion' maxlength='256' title='Donation'  value='" + ObjTick.T_name + "'  onblur='checkvalidatetkt(this.id)' />");

                        }
                        else
                        {
                            strticketHtml.Append("<input type='text' class='form-control evnt_inp_cont chkvalidation' id='id_ticket_type-" + j + "' placeholder='Early Bird, RSVP...' maxlength='256' title='Give your ticket a name, like General Admission, Early Bird, RSVP, etc.'  value='" + ObjTick.T_name + "' />");

                        }
                        strticketHtml.Append("</div>");
                        strticketHtml.Append("</div>");
                        strticketHtml.Append("</div>");
                        strticketHtml.Append("<div class='col-sm-2 no_pad ipad-50'><div class='form-group'>");
                        strticketHtml.Append("<label class='col-sm-3 control-label ev_tickt_lebel xs-tkt-typ-pad-0'>Qty</label>");
                        strticketHtml.Append("<div class='col-sm-9 xs-tkt-typ-pad-0'>");
                        strticketHtml.Append("<input type='text' class='form-control evnt_inp_cont txtsum numbers' id='id_quantity_total-" + j + "' maxlength='6' placeholder='' onblur='changeqty(this.id)' oncopy='return false' oncut='return false' onpaste='return false' onkeypress='allownumber(this,event,this.id)' value='" + ObjTick.Qty_Available + "' />");
                        strticketHtml.Append("</div></div> </div>");
                        if (ObjTick.TicketTypeID == 2)
                        {
                            strticketHtml.Append("<div class='col-sm-3 no_pad ipad-50-ml10'><div class='form-group paidticket-" + j + "' id='id_paid-" + j + "'>");
                        }
                        else
                        {
                            strticketHtml.Append("<div class='col-sm-3 no_pad ipad-50-ml10'><div class='form-group paidticket-" + j + "' id='id_paid-" + j + "' style='display:none;'>");
                        }
                        strticketHtml.Append("<label class='col-sm-5 no_pad control-label ev_tickt_lebel xs-tkt-typ-pad-0'>Price $</label>");
                        strticketHtml.Append("<div class='col-sm-7 xs-tkt-typ-pad-0 ipad-pad-5'>");
                        if (ObjTick.TicketTypeID == 2)
                        {
                            strticketHtml.Append("<input type='text' class='form-control evnt_inp_cont numbers' id='id_cost-" + j + "' onkeypress='validatenumdec(this, event, this.id)' onkeyup='changefee(this.id, event)' onblur='tofixed(this.id)' oncopy='return false' oncut='return false' onpaste='return false'   placeholder='0.00' maxlength='9' value=" + Price + " />");
                        }
                        else
                        {
                            strticketHtml.Append("<input type='text' class='form-control evnt_inp_cont numbers' id='id_cost-" + j + "' onkeypress='validatenumdec(this, event, this.id)' onkeyup='changefee(this.id, event)' onblur='tofixed(this.id)' oncopy='return false' oncut='return false' onpaste='return false'   placeholder='0.00' maxlength='9' />");

                        }
                        strticketHtml.Append("</div></div>");
                        if (ObjTick.TicketTypeID == 2)
                        {
                            strticketHtml.Append("<div class='form-group normalticket-" + j + "' style='display:none; text-align:center;' id='id_normal-" + j + "'>");
                        }
                        else
                        {
                            strticketHtml.Append("<div class='form-group normalticket-" + j + "' style='display:block; text-align:center;' id='id_normal-" + j + "'>");

                        }
                        if (ObjTick.TicketTypeID == 2)
                        {
                            strticketHtml.Append("<label class='col-sm-4 no_pad control-label ev_tickt_lebel' id='id_paymenttype-" + j + "'/>");
                        }
                        else if (ObjTick.TicketTypeID == 1)
                        {
                            strticketHtml.Append("<label class='col-sm-4 no_pad control-label ev_tickt_lebel' id='id_paymenttype-" + j + "'>Free</label>");
                        }
                        else
                        {
                            strticketHtml.Append("<label class='col-sm-4 no_pad control-label ev_tickt_lebel' id='id_paymenttype-" + j + "'>Donation</label>");
                        }
                        strticketHtml.Append("</div>");
                        if (ObjTick.TicketTypeID == 2)
                        {
                            strticketHtml.Append("<div class='label-control lbl_bot_price paidticket-" + j + "' id='idlbuyertotal-" + j + "'>");
                        }
                        else
                        {
                            strticketHtml.Append("<div class='label-control lbl_bot_price paidticket-" + j + "' id='idlbuyertotal-" + j + "' style='display:none;'>");

                        }
                        strticketHtml.Append("<span class='buyer_tot'>Buyer(s) total: </span>");
                        if (ObjTick.TicketTypeID == 2)
                        {
                            strticketHtml.Append("<span class='buyer_price' id='id_buyerprice-" + j + "' >$" + totaoln + "</span>");
                        }
                        else
                        {
                            strticketHtml.Append("<span class='buyer_price' id='id_buyerprice-" + j + "' >$0.00 </span>");

                        }
                        strticketHtml.Append("<span class='evnt_toltip'><i class='fa fa-info-circle'></i></span>");
                        if (ObjTick.TicketTypeID == 2)
                        {
                            strticketHtml.Append("<div class='tooltip  xs-buyer-total' id='id_tooltip-" + j + "'>Ticket Price &nbsp; &nbsp; &nbsp; $" + Price + " <br /> Fee &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp;  &nbsp;  &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; $" + customerfee + " <br /> Buyer(s) Total &nbsp;&nbsp; &nbsp;  $" + totaoln + "</div>");

                        }
                        else
                        {
                            strticketHtml.Append("<div class='tooltip' id='id_tooltip-" + j + "'>Ticket Price &nbsp; &nbsp; &nbsp; $0.00 <br /> Fee &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp;  &nbsp;  &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; $0.00 <br /> Buyer(s) Total &nbsp;&nbsp; &nbsp;  $0.00</div>");

                        }
                        strticketHtml.Append("<label class='pull-right' style='color:red;display:none;' id='id_lblprice-" + j + "'>Please enter valid number</label>");
                        strticketHtml.Append("</div>  <div class='clearfix'></div></div><div class='col-sm-3 no_pad xs768det-del ipad-50-ml10'>");
                        if (ObjTick.TicketTypeID == 2)
                        {
                            strticketHtml.Append("<div class='form-group paidticket-" + j + "' id='id_Disc-" + j + "' style='display:block;'>");
                        }
                        else
                        {
                            strticketHtml.Append("<div class='form-group paidticket-" + j + "' id='id_Disc-" + j + "' style='display:none;'>");

                        }
                        strticketHtml.Append("<label class='col-sm-4 control-label ev_tickt_lebel xs-tkt-typ-pad-0 ipad-width-58 ipad-pl0'>Disc.$</label>");
                        strticketHtml.Append("<div class='col-sm-8 xs-tkt-typ-pad-0 ipad-pad-5'>");
                        strticketHtml.Append("<input type='text' class='form-control evnt_inp_cont numbers' placeholder='0' id='id_Discount-" + j + "' onkeypress='changefeetype(this, event, this.id)'  onblur='tofixed(this.id)' oncopy='return false' oncut='return false' onpaste='return false' maxlength='9' value='" + discount + "' />");
                        strticketHtml.Append("</div></div></div>");
                        strticketHtml.Append("<div class='clearfix'></div>");
                        if (Isadmin == "Y")
                        {
                            if (ObjTick.TicketTypeID == 1)
                            {
                                strticketHtml.Append("<div class='col-sm-12 col-xs-12 mt10 no_pad  adminvis' id='isadmin-" + j + "' style='display:none;'>");

                            }
                            else
                            {
                                strticketHtml.Append("<div class='col-sm-12 col-xs-12 mt10 no_pad  adminvis' id='isadmin-" + j + "' style='display:block;'>");

                            }

                        }
                        else
                        {
                            strticketHtml.Append("<div class='col-sm-12 col-xs-12 mt10 no_pad  adminvis' id='isadmin-" + j + "' style='display:none;'>");

                        }
                        strticketHtml.Append("<div class='col-sm-1 no_pad ev_row_mov'></div>");
                        strticketHtml.Append("<div class='col-sm-5 no_pad '>");
                        strticketHtml.Append("<div class='form-group'>");
                        strticketHtml.Append("<label class='col-sm-5 no_pad control-label ev_tickt_lebel xs-evnt-type-pad-0'>EC Fee</label>");
                        strticketHtml.Append("<label class='col-sm-1 no_pad control-label ev_tickt_lebel'>%</label>");
                        strticketHtml.Append("<div class='col-sm-6 xs-evnt-type-pad-0'><input type='text' class='form-control evnt_inp_cont' placeholder='' id='id_ecfeeper-" + j + "' value=" + ecfeepercentage + "  onkeypress='allownumber(this,event,this.id)' oncopy='return false' oncut='return false' onpaste='return false' onblur='changeinecfee(this.id)' maxlength=3  />");
                        strticketHtml.Append("  </div></div></div>");
                        strticketHtml.Append("<div class='col-sm-1 no_pad text-center ev_tickt_lebel'> + </div><div class='col-sm-2 no_pad'><div class='form-group'>");
                        strticketHtml.Append("<label class='col-sm-1 no_pad control-label ev_tickt_lebel'>$</label>");
                        strticketHtml.Append("<div class='col-sm-11 xs-evnt-type-pad-0'><input type='text' class='form-control evnt_inp_cont'  id='id_ecfeeamt-" + j + "' value=" + ecamount + " onkeypress='validatenumdec(this, event, this.id)' onblur='tofixed(this.id);changeinecfee(this.id)' oncopy='return false' oncut='return false' onpaste='return false'  maxlength=8 >");
                        strticketHtml.Append("</div> </div></div>");
                        strticketHtml.Append("<div class='col-sm-1 no_pad text-center ev_tickt_lebel'> =   </div>");
                        strticketHtml.Append("<div class='col-sm-2 no_pad'>");
                        strticketHtml.Append("<div class='form-group'> <label class='col-sm-6 no_pad control-label ev_tickt_lebel'>Total:</label>");
                        strticketHtml.Append(" <div class='col-sm-6 no_pad'>   <input type='hidden' id='hd_ecfee-" + j + "' value=" + ecfee + " />");
                        strticketHtml.Append("<label class='ev_tickt_lebel' id='id_ecfee-" + j + "'> " + ecfee + " </label>");
                        strticketHtml.Append("</div></div ></div >");
                        strticketHtml.Append(" <div class='clearfix'></div>");
                        strticketHtml.Append("<div class='col-sm-12 col-xs-12 no_pad mt10'>");
                        strticketHtml.Append("<div class='col-sm-1 no_pad ev_row_mov'></div>");
                        strticketHtml.Append("<div class='col-sm-6 no_pad'><div class='form-group'>");
                        strticketHtml.Append("<label class='col-sm-4 no_pad control-label ev_tickt_lebel xs-evnt-type-pad-0'>Cutomer Fee</label>");
                        strticketHtml.Append("<label class='col-sm-1 no_pad control-label ev_tickt_lebel'>$</label>");
                        strticketHtml.Append("<div class='col-sm-5 xs-evnt-type-pad-0'>");
                        strticketHtml.Append("<input type='text' class='form-control evnt_inp_cont'  id='id_customerfee-" + j + "' placeholder='' value=" + customerfee + " onkeypress='validatenumdec(this, event, this.id)' onblur='tofixed(this.id);reflectfeechange(this.id)' oncopy='return false' oncut='return false' onpaste='return false'  maxlength=8 />");
                        strticketHtml.Append("<input type='hidden'  id='hd_customerfee-" + j + "' value=" + customerfee + " />");
                        strticketHtml.Append("<input type='hidden'  id='hd_customchange-" + j + "' value=0 />");
                        strticketHtml.Append("</div></div ></div ></div ></div> </div>");
                        strticketHtml.Append("<div class='col-sm-2 col-xs-12 text-right'><div class='col-sm-1 no_pad evnt_sett_main xs768detail'>");
                        strticketHtml.Append("<div class='nav evnt_setting'>");
                        strticketHtml.Append("<span class='evnt_set ev_set_more' id='id_setting-" + j + "' onclick='showsettingdiv(this.id);'> Detail</span>");
                        strticketHtml.Append("<a class='btn ev_set_del_btn evnt_set' id='btndelete-" + j + "' type='button' href='#cnfrmdelete-" + j + "' data-toggle='modal'>");
                        strticketHtml.Append(" <i class='fa fa-trash'></i></a>");
                        strticketHtml.Append("</div></div></div><div class='clearfix'></div>");
                        strticketHtml.Append("<div class='tab-content' id='evnt_set-" + j + "'>");
                        strticketHtml.Append("<div class='col-sm-12 no_pad'><div class='modal-content evnt_set_panel'><div class='modal-body pb0'> <div class='col-sm-12 no_pad'>");
                        strticketHtml.Append("<div class='form-group'><label class='col-sm-2 control-label ev_tickt_lebel pl0'>Description</label><div class='col-sm-6 xs-tkt-typ-pad-0'>");
                        if (!string.IsNullOrEmpty(ObjTick.T_Desc))
                        {
                            strticketHtml.Append("<input type ='text' class='form-control evnt_inp_cont' id='id_descriptionclass-" + j + "' placeholder='Ticket description' maxlength='1000' value='" + ObjTick.T_Desc + "' />");
                        }
                        else
                        {
                            strticketHtml.Append("<input type ='text' class='form-control evnt_inp_cont' id='id_descriptionclass-" + j + "' placeholder='Ticket description' maxlength='1000'  />");

                        }
                        strticketHtml.Append(" </div> </div><div class='clearfix'></div><div class='form-group'><label class='label-control pl0 ev_tickt_lebel mt5'>");
                        if (ObjTick.Show_T_Desc == "0")
                        {
                            strticketHtml.Append("<input type='checkbox' class='' id='id_show_description-" + j + "'/>");
                        }
                        else
                        {
                            strticketHtml.Append("<input type='checkbox' class='' id='id_show_description-" + j + "' checked='checked'/>");

                        }
                        strticketHtml.Append("Show ticket description on event page</label></div><div class='clearfix'></div>");
                        if (ObjTick.TicketTypeID == 2)
                        {
                            strticketHtml.Append("<div class='form-group' id='id_feestruct-" + j + "' style='display:block;'>");
                        }
                        else
                        {
                            strticketHtml.Append("<div class='form-group' id='id_feestruct-" + j + "' style='display:none'>");

                        }
                        strticketHtml.Append("<label class='col-sm-1 control-label ev_tickt_lebel pl0'>Fees</label>");
                        strticketHtml.Append("<div class='col-sm-5'>");
                        if (ObjTick.Fees_Type == "0")
                        {
                            strticketHtml.Append("<select class='form-control ev_drop_label' id='id_include_ticket_fees-" + j + "' onchange='feechange(this.id)'>");

                            strticketHtml.Append("<option value='0' selected='selected' >customer pays the fees</option>");
                            strticketHtml.Append("<option value = '1'> I absorb the fee </option ></select > ");
                        }
                        else
                        {
                            strticketHtml.Append("<select class='form-control ev_drop_label' id='id_include_ticket_fees-" + j + "' onchange='feechange(this.id)'>");

                            strticketHtml.Append("<option value='0'>customer pays the fees</option>");
                            strticketHtml.Append("<option value='1' selected='selected'> I absorb the fee </option ></select > ");
                        }
                        strticketHtml.Append("</div></div>");
                        strticketHtml.Append("<div class='clearfix'></div><div class='form-group mt20'> <div class='col-sm-6 ev_pad_l0 xs-tkt-typ-pad-0'><div class='col-sm-12 no_pad'>");
                        strticketHtml.Append("<label class='label-control pl0 ev_tickt_lebel'>Ticket Sales Start</label>");
                        strticketHtml.Append("<div class='upload_help_icn pull-right' style='display:none;'><div class='tip'><img class='help_icon_hov' src='/Images/icon-question.gif' id='hovstart-" + j + "' onmouseover='showhover(this.id);' onmouseout='showhoverout(this.id)' />");
                        strticketHtml.Append(" <span class='upload_help_icn_inner' style='display:none;' id='help_start_inner-" + j + "''> Your Event's date and time need to be  selected in order to update sales start date  </span>  </div>  </div>");
                        strticketHtml.Append("</div><div class='col-sm-8 ev_pad_l0 mb5 xs_pad_0'>");
                        strticketHtml.Append("<input class='form-control event_time_str ev_tickt_input' placeholder='MM/DD/YYYY' id='id_salestart-" + j + "' onchange='changetext(this.id);' value='" + startdate + "' />");
                        strticketHtml.Append("</div><div class='col-sm-4 no_pad mb5 xs_pad_0'><input type='hidden' value='0' id='id_hdsaletimestart-" + j + "' value='" + ObjTick.Sale_Start_Time + "' />");
                        strticketHtml.Append("<input id='id_saletimestart-" + j + "' type='text' class='time_picker form-control ev_tickt_input mr0' placeholder='07:00pm' onchange='checkvalidtime(this.id)' value='" + ObjTick.Sale_Start_Time + "' />");
                        strticketHtml.Append("</div></div>");
                        strticketHtml.Append("<div class='col-sm-6 ev_pad_r0 xs_pad_0'><div class='col-sm-12 no_pad'><label class='label-control pl0 ev_tickt_lebel'>Ticket Sales End</label>");
                        strticketHtml.Append("<div class='upload_help_icn pull-right' style='display:none;'><div class='tip'><img class='help_icon_hov' src='/Images/icon-question.gif' id='hovend-" + j + "' onmouseover='showhover(this.id);' onmouseout='showhoverout(this.id)' />");
                        strticketHtml.Append(" <span class='upload_help_icn_inner' style='display:none;' id='help_end_inner-" + j + "''> Your Event's date and time need to be  selected in order to update sales end date  </span>  </div>  </div>");
                        strticketHtml.Append("</div><div class='col-sm-8 ev_pad_l0 mb5 xs-tkt-typ-pad-0'><input class='form-control event_time_str ev_tickt_input' placeholder='MM/DD/YYYY' id='id_saleend-" + j + "' onchange='checkvalidDate(this.id)' value='" + enddate + "' />");
                        strticketHtml.Append("</div><div class='col-sm-4 ev_pad_l0 mb5 xs-tkt-typ-pad-0'> <input type='hidden' value='0' id='id_hdsaletimeend-" + j + "' value='" + ObjTick.Sale_End_Time + "' />");
                        strticketHtml.Append("<input id='id_saletimeend-" + j + "' type='text' class='time_picker form-control ev_tickt_input mr0' placeholder='07:00pm' onchange='checkvalidtime(this.id) ' value='" + ObjTick.Sale_End_Time + "' />");
                        strticketHtml.Append("</div> </div></div><div class='clearfix'></div>");
                        strticketHtml.Append("<div class='form-group'><div class='col-sm-12 mt10 no_pad'>");
                        strticketHtml.Append("<label class='label-control pl0 ev_tickt_lebel mt5'>");
                        if (ObjTick.Hide_Ticket == "0")
                        {
                            strticketHtml.Append("<input type='checkbox' class='' id='id_ticketshidden-" + j + "' onclick='showdiv(this.id)' />");

                        }
                        else
                        {
                            strticketHtml.Append("<input type='checkbox' class='' id='id_ticketshidden-" + j + "' onclick='showdiv(this.id)' checked='checked' />");

                        }

                        strticketHtml.Append("Hide this ticket type</label></div>");
                        if (ObjTick.Auto_Hide_Sche == "1")
                        {
                            strticketHtml.Append("<label class='EvaddTkt lbl_bot_text' id='id_EvaddTkt-" + j + "' onclick='hideunhide(this.id);' style='display:none;'>Add ticket auto-hide schedule</label>");

                        }
                        else
                        {
                            strticketHtml.Append("<label class='EvaddTkt lbl_bot_text' id='id_EvaddTkt-" + j + "' onclick='hideunhide(this.id);'>Add ticket auto-hide schedule</label>");

                        }
                        if (ObjTick.Auto_Hide_Sche == "1")
                        {
                            strticketHtml.Append("<label class='RemvTkt label-control lbl_bot_text wd600' id='id_RemvTkt-" + j + "' onclick='hideunhide(this.id);' style='display:block;'>Remove ticket auto-hide schedule</label>");
                        }
                        else
                        {
                            strticketHtml.Append("<label class='RemvTkt label-control lbl_bot_text wd600' id='id_RemvTkt-" + j + "' onclick='hideunhide(this.id);' style='display:none;'>Remove ticket auto-hide schedule</label>");

                        }
                        if (ObjTick.Auto_Hide_Sche == "1")
                        {
                            strticketHtml.Append("<div class='CusdateCont evnt_cus_date_cont' id='id_CusdateCont-" + j + "' style='display:block;'>");
                        }
                        else
                        {
                            strticketHtml.Append("<div class='CusdateCont evnt_cus_date_cont' id='id_CusdateCont-" + j + "' style='display:none;'>");

                        }
                        strticketHtml.Append("<div class='col-sm-12'><label class='label-control ev_tickt_lebel fs-13' id='id_TktnotSale-" + j + "' onclick='hideunhide(this.id)'>");
                        if (ObjTick.Auto_Hide_Sche == "1")
                        {
                            if (ObjTick.T_AutoSechduleType == "0")
                            {
                                custdate = "1";
                                strticketHtml.Append("<input type='radio' id='id_auto_hide_type_0-" + j + "'  checked='checked' />");
                            }
                            else
                            {
                                strticketHtml.Append("<input type='radio' id='id_auto_hide_type_0-" + j + "'   />");

                            }
                        }
                        else
                        {
                            strticketHtml.Append("<input type='radio' id='id_auto_hide_type_0-" + j + "'   />");

                        }

                        strticketHtml.Append("When ticket is not on sale</label> </div><div class='clearfix'></div><div class='col-sm-12'><label class='label-control ev_tickt_lebel fs-13 mt5' id='id_CusdateTime-" + j + "' onclick='hideunhide(this.id)'>");

                        if (ObjTick.T_AutoSechduleType == "1")
                        {
                            strticketHtml.Append("<input type='radio' id='id_auto_hide_type_1-" + j + "' checked='checked' />");
                        }
                        else
                        {
                            strticketHtml.Append("<input type='radio' id='id_auto_hide_type_1-" + j + "'  />");

                        }
                        if (custdate == "0")
                        {
                            strticketHtml.Append("For custom date and time</label> </div> <div class='hide_unt_aft' id='id_HideuntDiv-" + j + "' style='display:block;'>");
                        }
                        else
                        {
                            strticketHtml.Append("For custom date and time</label> </div> <div class='hide_unt_aft' id='id_HideuntDiv-" + j + "' style='display:none;'>");

                        }
                        strticketHtml.Append("<div class='form-group mt10'><div class='col-sm-6 ev_pad_l0'><div class='col-sm-12 no_pad'>");
                        if (!string.IsNullOrEmpty(ObjTick.Hide_Untill_Date.ToString()) && !string.IsNullOrEmpty(ObjTick.Hide_Untill_Time))
                        {
                            strticketHtml.Append("<input type='checkbox' checked='checked'  id='id_hideuntil-" + j + "' onchange='enablehideafteruntil(this.id)' />");
                        }
                        else
                        {
                            strticketHtml.Append("<input type='checkbox' id='id_hideuntil-" + j + "' onchange='enablehideafteruntil(this.id)' />");

                        }
                        strticketHtml.Append("<label class='label-control ev_tickt_lebel'>Hide Untill</label>");
                        strticketHtml.Append("</div><div class='col-sm-8 ev_pad_l0'>");
                        if (!string.IsNullOrEmpty(ObjTick.Hide_Untill_Date.ToString()) && !string.IsNullOrEmpty(ObjTick.Hide_Untill_Time))
                        {
                            strticketHtml.Append("<input class='form-control event_time_str ev_tickt_input' placeholder='MM/DD/YYYY' id='id_auto_hide_until_Date-" + j + "' onchange='changetext(this.id);' value='" + untilldate + "' />");
                        }
                        else
                        {
                            strticketHtml.Append("<input class='form-control event_time_str ev_tickt_input' placeholder='MM/DD/YYYY' id='id_auto_hide_until_Date-" + j + "' onchange='changetext(this.id);' value='' readonly='readonly' />");

                        }
                        strticketHtml.Append("</div><div class='col-sm-4 no_pad'>");
                        strticketHtml.Append("<input type='hidden' value='0' id='id_hdautohideuntil-" + j + "' value='" + ObjTick.Hide_Untill_Time + "' />");
                        strticketHtml.Append("<input id ='id_auto_hide_until_time-" + j + "' type='text' class='time_picker form-control ev_tickt_input mr0' placeholder='07:00pm' onchange='checkvalidtime(this.id)' value='" + ObjTick.Hide_Untill_Time + "' />");
                        strticketHtml.Append("</div></div><div class='col-sm-6 ev_pad_r0'><div class='col-sm-12 no_pad'>");
                        if (!string.IsNullOrEmpty(ObjTick.Hide_After_Date.ToString()) && !string.IsNullOrEmpty(ObjTick.Hide_After_Time))
                        {
                            strticketHtml.Append("<input type='checkbox' checked='checked' id='id_hideafter-" + j + "' onchange='enablehideafteruntil(this.id)'/>");
                        }
                        else
                        {
                            strticketHtml.Append("<input type='checkbox' id='id_hideafter-" + j + "' onchange='enablehideafteruntil(this.id)'/>");

                        }
                        strticketHtml.Append("<label class='label-control ev_tickt_lebel'>Hide After</label>");
                        strticketHtml.Append("</div><div class='col-sm-8 ev_pad_l0'><input class='form-control event_time_str ev_tickt_input' placeholder='MM/DD/YYYY' id='id_auto_hide_after_Date-" + j + "' onchange='checkvalidDate(this.id)' value='" + afterdate + "' />");
                        strticketHtml.Append(" </div> <div class='col-sm-4 no_pad'><input type='hidden' value='0' id='id_hdautohideafter-" + j + "' value='" + ObjTick.Hide_After_Time + "' />");
                        strticketHtml.Append("<input id ='id_auto_hide_after_Time-" + j + "' type='text' class='time_picker form-control ev_tickt_input mr0' placeholder='07:00pm' onchange='checkvalidtime(this.id)' value='" + ObjTick.Hide_After_Time + "' />");
                        strticketHtml.Append("</div></div></div></div></div></div>");
                        if (ObjTick.TicketTypeID != 3)
                        {
                            strticketHtml.Append("<div class='clearfix donateshow-" + j + "' id='cleafix-" + j + "'></div><div class='form-group donateshow-" + j + "' id='maxmin-" + j + "'><label class='label-control pl0 ev_tickt_lebel wd600'>Tickets allowed per order</label>");
                            strticketHtml.Append("<div class='evnt_time_cont wd280 mr5_p'><input class='form-control evnt_inp_cont wd240 numbers' placeholder='0' id='id_min_ticket-" + j + "'  onkeypress='allownumber(this,event,this.id)' maxlength='6' oncopy='return false' oncut='return false' onpaste='return false' value='" + ObjTick.Min_T_Qty + "' />");
                            strticketHtml.Append("<label class='label-control lbl_bot_minim'>Minimum</label></div><div class='evnt_time_cont wd280'>");
                            strticketHtml.Append("<input class='form-control evnt_inp_cont wd240 numbers' placeholder='0' id='id_max_ticket-" + j + "'  onkeypress='allownumber(this,event,this.id)' maxlength='6' oncopy='return false' oncut='return false' onpaste='return false' value='" + ObjTick.Max_T_Qty + "'  />");
                            strticketHtml.Append("<label class='label-control lbl_bot_minim '>Maximum</label><label class='col-sm-12 no_pad control-label' style='color:red;display:none' id='id_lblmax-" + j + "' >Please enter a valid number</label>");
                            strticketHtml.Append(" </div></div>");
                        }
                        strticketHtml.Append("<div class='clearfix'></div><div class='form-group'><div class='col-sm-12 no_pad' style='display:none;'><label class='label-control pl0 ev_tickt_lebel mt5'>");
                        if (ObjTick.T_Disable == "1")
                        {
                            strticketHtml.Append("<input type='checkbox' class='' id='id_disableticket-" + j + "' checked='checked'/>");

                        }
                        else
                        {
                            strticketHtml.Append("<input type='checkbox' class='' id='id_disableticket-" + j + "'/>");

                        }
                        strticketHtml.Append("Disable Ticket </label> </div><div class='col-sm-12 no_pad'><label class='label-control pl0 ev_tickt_lebel mt5'>");
                        if (ObjTick.T_Mark_SoldOut == "1")
                        {
                            strticketHtml.Append("<input type='checkbox' class='' id='id_marksoldout-" + j + "' checked='checked' />");
                        }
                        else
                        {
                            strticketHtml.Append("<input type='checkbox' class='' id='id_marksoldout-" + j + "' />");

                        }
                        strticketHtml.Append("Mark Sold Out</label> </div>");
                        if (ObjTick.TicketTypeID != 3)
                        {
                            strticketHtml.Append("<div class='col-sm-12 no_pad donateshow - " + j + "' id='display - " + j + "'><label class='label-control pl0 ev_tickt_lebel mt5'>");
                            if (ObjTick.T_Displayremaining == "1")
                            {
                                strticketHtml.Append("<input type='checkbox' class='' id='id_displayremaining-" + j + "' checked='checked' />");
                            }
                            else
                            {
                                strticketHtml.Append("<input type='checkbox' class='' id='id_displayremaining-" + j + "'  />");

                            }
                            strticketHtml.Append("Display Remaining Tickets</label></div>");
                        }
                        strticketHtml.Append("</div> </div><div class='clearfix'></div><div class='modal-footer no_pad mt5'>");
                        strticketHtml.Append("<div class='col-sm-12 text-left mt10'><button class='EvntMinim ev_set_del_btn btn' id='id_Minimize-" + j + "'  onclick='minimize(this.id);' type='button'>");
                        strticketHtml.Append("<i class='fa fa-arrow-up'></i> Minimize Setting </button> </div></div> </div></div></div></div><div class='clearfix'></div>");

                        strticketHtml.Append("<div class='modal fade' id='cnfrmdelete-" + j + "'  tabindex='-1' role='dialog' aria-labelledby='myModalLabel' aria-hidden='true'>");
                        strticketHtml.Append("<div class='modal-dialog modal-sm confirm-msg ok-msg custom'><div class='modal-content'> <form id = 'contactForm' class='fullw1 add'>");
                        strticketHtml.Append("<div class='modal-body text-center pv50 txt-msg'> <h4 class='msg' >Are you sure you want to delete?</h4></div>");
                        strticketHtml.Append("<div class='modal-footer msg-action-btn ok'>");
                        strticketHtml.Append("<button type='button' class='btn btn-primary yes ok' id='btnyes-" + j + "' onclick='deletediv(this.id);' data-dismiss='modal'>Yes</button>");
                        strticketHtml.Append("<button type='button' class='btn btn-primary no' data-dismiss='modal'>No</button></div></form></div></div></div></div>");

                        j = j + 1;



                    }

                    objJson.Ticket = strticketHtml.ToString();

                    objJson.capacity = capacity;


                    var vardesc = (from myEvent in objEnt.Event_VariableDesc
                                   where myEvent.Event_Id == EventIid
                                   select myEvent).ToList();

                    StringBuilder strvariableHtml = new StringBuilder();
                    int k = 0;
                    foreach (Event_VariableDesc Objvardesc in vardesc)
                    {

                        var PRICE = String.Format("{0:#,###,##0.00}", Objvardesc.Price);

                        strvariableHtml.Append(" <div class='col-sm-12 no_pad' id='id_clonevariable-" + k + "' >");
                        strvariableHtml.Append("<div class='list -group-item ev_var_chrg_list'>");
                        strvariableHtml.Append(" <div class='form-group'>");
                        strvariableHtml.Append("<div class='col-sm-7 col-xs-7'><input type='hidden' id='id_varid-" + k + "' value='" + Objvardesc.Variable_Id + "'/>");
                        strvariableHtml.Append("<input class='form-control evnt_inp_cont' type='text' placeholder='Variable Charges Description " + (k + 1) + "' id='id_varsubdesc-" + k + "' maxlength='256' value='" + Objvardesc.VariableDesc + "' onblur='checkvalidatetkt(this.id)'     >");
                        strvariableHtml.Append(" </div><div class='col-sm-4 col-xs-4'><label class='col-sm-1 control-label ev_tickt_lebel'>$</label> <div class='col-sm-10 no_pad'>");
                        strvariableHtml.Append("<input class='form-control evnt_inp_cont' type='text' placeholder='0.00' id='id_varsubprice-" + k + "' maxlength='9'onkeypress='changefeetype(this, event, this.id);' onblur='tofixed(this.id);checkvalidatetkt(this.id);' oncopy='return false' oncut='return false' onpaste='return false' value=" + PRICE + ">");
                        strvariableHtml.Append("</div> </div><div class='col-sm-1 col-xs-1 no_pad text-right var_chrg_edt_main'> <button class='btn' type='button' id='btn_vardelete-" + k + "' onclick='deletevariable(this.id)'><i class='fa fa-times'></i></button>");
                        strvariableHtml.Append("</div> </div></div> </div>");

                        k = k + 1;
                    }

                    objJson.Variabledesc = strvariableHtml.ToString();
                }catch(Exception ex)
                {
                  logger.Error("Exception during request processing", ex);
                }

              
            }
            return Json(objJson, JsonRequestBehavior.AllowGet);
        }

        public long EditEventInfo(EventCreation model)
        {
            long lEventId = model.EventID;
            string lat = "", lon = "";
            ViewEvent vc = new ViewEvent();
            try
            {
                string strUserId = (Session["AppId"] != null ? Session["AppId"].ToString() : "");
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var vParentEvt = (from myEnt in objEnt.Events where myEnt.EventID == lEventId select myEnt.Parent_EventID).FirstOrDefault();
                    if (vParentEvt == 0) vParentEvt = lEventId;

                    //Event ObjEC = objEnt.Events.First(i => i.EventID == lEventId);
                    Event ObjEC = new Event();
                    ObjEC.Parent_EventID = vParentEvt;
                    ObjEC.EventTypeID = model.EventTypeID;
                    ObjEC.EventCategoryID = model.EventCategoryID;
                    ObjEC.EventSubCategoryID = model.EventSubCategoryID;
                    ObjEC.UserID = strUserId;
                    ObjEC.EventTitle = model.EventTitle;
                    ObjEC.DisplayStartTime = model.DisplayStartTime;
                    ObjEC.DisplayEndTime = model.DisplayEndTime;
                    ObjEC.DisplayTimeZone = model.DisplayTimeZone;
                    ObjEC.EventDescription = Server.HtmlEncode(model.EventDescription);
                    ObjEC.EventPrivacy = model.EventPrivacy;
                    ObjEC.Private_ShareOnFB = model.Private_ShareOnFB;
                    ObjEC.Private_GuestOnly = model.Private_GuestOnly;
                    ObjEC.Private_Password = model.Private_Password;
                    ObjEC.EventUrl = model.EventUrl;
                    ObjEC.PublishOnFB = model.PublishOnFB;
                    ObjEC.EventStatus = "Live";
                    ObjEC.IsMultipleEvent = model.IsMultipleEvent;
                    ObjEC.TimeZone = model.TimeZone;
                    ObjEC.DisplayStartTime = model.DisplayStartTime;
                    ObjEC.DisplayEndTime = model.DisplayEndTime;
                    ObjEC.DisplayTimeZone = model.DisplayTimeZone;
                    ObjEC.FBUrl = model.FBUrl;
                    ObjEC.TwitterUrl = model.TwitterUrl;
                    ObjEC.LastLocationAddress = model.LastLocationAddress;
                    ObjEC.AddressStatus = model.AddressStatus;
                    ObjEC.EnableFBDiscussion = model.EnableFBDiscussion;
                    ObjEC.Ticket_DAdress = model.Ticket_DAdress;
                    ObjEC.Ticket_showremain = model.Ticket_showremain;
                    ObjEC.Ticket_showvariable = model.Ticket_showvariable;
                    ObjEC.Ticket_variabledesc = model.Ticket_variabledesc;
                    ObjEC.Ticket_variabletype = model.Ticket_variabletype;
                    ObjEC.ShowMap = model.ShowMap;

                    var timezone = "";

                    DateTimeWithZone dtzCreated;

                    var Timezonedetail = (from ev in db.TimeZoneDetails where ev.TimeZone_Id.ToString() == model.TimeZone select ev).FirstOrDefault();
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
                    ObjEC.ModifyDate = dtzCreated.UniversalTime;
                   objEnt.Events.Add(ObjEC);
                    // Address info
                    if (model.AddressDetail != null)
                    {
                        Address ObjAdd = new Models.Address();
                        //  objEnt.Addresses.RemoveRange(objEnt.Addresses.Where(x => x.EventId == lEventId));
                        foreach (Address objA in model.AddressDetail)
                        {
                            if ((objA.VenueName != null && objA.VenueName.Trim() != "") || objA.ConsolidateAddress != "")
                            {

                                try
                                {
                                    var geocode = vc.Geocode(objA.ConsolidateAddress);
                                    lat = geocode.Latitude.ToString();
                                    lon = geocode.Longitude.ToString();
                                }
                                catch (Exception ex)
                                {
                                    lat = "";
                                    lon = "";

                                }
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
                                ObjAdd.Latitude = lat;
                                ObjAdd.Longitude = lon;
                                objEnt.Addresses.Add(ObjAdd);
                            }
                        }
                    }
                    DateTimeWithZone dtzstart, dtzend;
                    // Event on Single Timing 
                    if (model.EventVenue != null)
                    {
                        EventVenue objEVenue = new EventVenue();
                        foreach (EventVenue objEv in model.EventVenue)
                        {

                            //save utc


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
                            //
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
                    // Event on Multiple timing 
                    if (model.MultipleEvents != null)
                    {
                        MultipleEvent objMEvents = new MultipleEvent();
                        foreach (MultipleEvent objME in model.MultipleEvents)
                        {
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
                            ObjEC.MultipleEvents.Add(objMEvents);
                        }
                    }
                    // Orgnizer
                    if (model.Orgnizer != null)
                    {
                        Organizer_Master objEOrg = new Organizer_Master();
                        Event_Orgnizer_Detail evntorg = new Event_Orgnizer_Detail();
                        var neworg = model.Orgnizer.Where(x => x.Orgnizer_Id == 0).Any();
                        if (neworg)
                        {
                            var defaultorg = model.Orgnizer.Where(x => x.Orgnizer_Id == 0 && x.DefaultOrg == "Y").Any();
                            if (defaultorg)
                            {
                                var orgnew = model.Orgnizer.Where(x => x.Orgnizer_Id == 0).ToList();
                                foreach (Organizer_Master objOr in orgnew)
                                {

                                    objEOrg = new Organizer_Master();
                                    // objEOrg.Orgnizer_Event_Id = ObjEC.EventID;
                                    objEOrg.Orgnizer_Name = objOr.Orgnizer_Name;
                                    objEOrg.Organizer_Desc = Server.HtmlEncode(objOr.Organizer_Desc);
                                    objEOrg.Organizer_FBLink = objOr.Organizer_FBLink;
                                    objEOrg.Organizer_Twitter = objOr.Organizer_Twitter;
                                    //objEOrg.DefaultOrg = objOr.DefaultOrg;
                                    objEOrg.Organizer_Linkedin = objOr.Organizer_Linkedin;
                                    objEOrg.UserId = strUserId;
                                    objEOrg.Organizer_Status = "A";
                                    objEnt.Organizer_Master.Add(objEOrg);
                                    if (objOr.DefaultOrg == "Y")
                                    {
                                        evntorg = new Event_Orgnizer_Detail();
                                        evntorg.DefaultOrg = objOr.DefaultOrg;
                                        evntorg.OrganizerMaster_Id = objEOrg.Orgnizer_Id;
                                        evntorg.Orgnizer_Event_Id = ObjEC.EventID;
                                        evntorg.UserId = strUserId;
                                        objEnt.Event_Orgnizer_Detail.Add(evntorg);
                                    }



                                }
                            }
                            else
                            {
                                var orgnew = model.Orgnizer.Where(x => x.Orgnizer_Id == 0).ToList();
                                foreach (Organizer_Master objOr in orgnew)
                                {

                                    objEOrg = new Organizer_Master();
                                    // objEOrg.Orgnizer_Event_Id = ObjEC.EventID;
                                    objEOrg.Orgnizer_Name = objOr.Orgnizer_Name;
                                    objEOrg.Organizer_Desc = Server.HtmlEncode(objOr.Organizer_Desc);
                                    objEOrg.Organizer_FBLink = objOr.Organizer_FBLink;
                                    objEOrg.Organizer_Twitter = objOr.Organizer_Twitter;
                                    //objEOrg.DefaultOrg = objOr.DefaultOrg;
                                    objEOrg.Organizer_Linkedin = objOr.Organizer_Linkedin;
                                    objEOrg.UserId = strUserId;
                                    objEOrg.Organizer_Status = "A";
                                    objEnt.Organizer_Master.Add(objEOrg);




                                }
                                var defaultorgold = model.Orgnizer.Where(x => x.DefaultOrg == "Y").ToList();
                                foreach (Organizer_Master objOr in defaultorgold)
                                {
                                    evntorg = new Event_Orgnizer_Detail();
                                    evntorg.DefaultOrg = objOr.DefaultOrg;
                                    evntorg.OrganizerMaster_Id = objOr.Orgnizer_Id;
                                    evntorg.Orgnizer_Event_Id = ObjEC.EventID;
                                    evntorg.UserId = strUserId;
                                    objEnt.Event_Orgnizer_Detail.Add(evntorg);
                                }
                                var editoldorg = model.Orgnizer.Where(x => x.EditOrg == "1").ToList();
                                if (editoldorg != null)
                                {
                                    foreach (Organizer_Master objOr in editoldorg)
                                    {
                                        string sql = @"update Organizer_Master set Orgnizer_Name={0},Organizer_Desc={1},Organizer_FBLink={2},Organizer_Twitter={3},Organizer_Linkedin={4} where Orgnizer_Id={5}";
                                        List<Object> sqlParamsList = new List<object>();
                                        sqlParamsList.Add(objOr.Orgnizer_Name);
                                        sqlParamsList.Add(Server.HtmlEncode(objOr.Organizer_Desc));
                                        sqlParamsList.Add(objOr.Organizer_FBLink);
                                        sqlParamsList.Add(objOr.Organizer_Twitter);
                                        sqlParamsList.Add(objOr.Organizer_Linkedin);
                                        sqlParamsList.Add(objOr.Orgnizer_Id);

                                        objEnt.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());
                                    }
                                }


                            }

                        }
                        else
                        {

                            var defaultorg = model.Orgnizer.Where(x => x.DefaultOrg == "Y").ToList();
                            foreach (Organizer_Master objOr in defaultorg)
                            {
                                evntorg = new Event_Orgnizer_Detail();
                                evntorg.DefaultOrg = objOr.DefaultOrg;
                                evntorg.OrganizerMaster_Id = objOr.Orgnizer_Id;
                                evntorg.Orgnizer_Event_Id = ObjEC.EventID;
                                evntorg.UserId = strUserId;
                                objEnt.Event_Orgnizer_Detail.Add(evntorg);
                            }
                            var editoldorg = model.Orgnizer.Where(x => x.EditOrg == "1").ToList();
                            if (editoldorg != null)
                            {
                                foreach (Organizer_Master objOr in editoldorg)
                                {
                                    string sql = @"update Organizer_Master set Orgnizer_Name={0},Organizer_Desc={1},Organizer_FBLink={2},Organizer_Twitter={3},Organizer_Linkedin={4} where Orgnizer_Id={5}";
                                    List<Object> sqlParamsList = new List<object>();
                                    sqlParamsList.Add(objOr.Orgnizer_Name);
                                    sqlParamsList.Add(Server.HtmlEncode(objOr.Organizer_Desc));
                                    sqlParamsList.Add(objOr.Organizer_FBLink);
                                    sqlParamsList.Add(objOr.Organizer_Twitter);
                                    sqlParamsList.Add(objOr.Organizer_Linkedin);
                                    sqlParamsList.Add(objOr.Orgnizer_Id);

                                    objEnt.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());
                                }
                            }
                        }
                    }
                    // Tickets

                    string customize = "0";
                    var customizefee = false;
                    var customizeamt = false;
                    if (model.Ticket != null)
                    {
                        Ticket ticket = new Ticket();
                        // objEnt.Ticket_Quantity_Detail.RemoveRange(objEnt.Ticket_Quantity_Detail.Where(x => x.TQD_Event_Id == lEventId));
                        //  objEnt.Tickets.RemoveRange(objEnt.Tickets.Where(x => x.E_Id == lEventId));
                       var  ids = new List<long>();
                        foreach (Ticket tick in model.Ticket)
                        {

                            if (tick.T_Id == 0)
                            {
                                ticket = new Ticket();

                                ticket.T_Customize = "0";
                                var mainfee = (from db in objEnt.Fee_Structure select db).FirstOrDefault();
                                ticket.EC_Fee = tick.Customer_Fee;
                                ticket.T_Ecpercent = mainfee.FS_Percentage;
                                ticket.T_EcAmount = mainfee.FS_Amount;
                                if (tick.TicketTypeID == 2)
                                {
                                    ticket.Customer_Fee = tick.Customer_Fee;
                                }
                                else
                                {
                                    ticket.Customer_Fee = 0;
                                }

                            }
                            else
                            {
                                ticket = (from obj in objEnt.Tickets where obj.T_Id == tick.T_Id && obj.E_Id == lEventId select obj).FirstOrDefault();
                                customize = ticket.T_Customize;
                                if (tick.TicketTypeID == 2)
                                {
                                    ticket.Customer_Fee = tick.Customer_Fee;
                                }

                            }

                            //ids = new List<long>();
                            if (tick.Isadmin == "Y")
                            {
                                if (tick.TicketTypeID != 1)
                                {
                                    var mainfee = (from db in objEnt.Fee_Structure select db).FirstOrDefault();
                                    var oldamnt = 0m;

                                    if (tick.Fees_Type == "0")
                                    {
                                        oldamnt = ((((tick.Price * mainfee.FS_Percentage) / 100) + mainfee.FS_Amount) + tick.Price) ?? 0;
                                    }
                                    else
                                    {
                                        oldamnt = tick.Price ?? 0;

                                    }
                                    var oldamount = Decimal.Round(oldamnt, 2);

                                    //var customizefee = false;
                                    //var customizeamt = false;

                                    if (mainfee.FS_Percentage == tick.T_Ecpercent && mainfee.FS_Amount == tick.T_EcAmount)
                                    {
                                        customizefee = false;

                                    }
                                    else
                                    {
                                        customizefee = true;

                                    }
                                    if (oldamount == tick.TotalPrice)
                                    {
                                        customizeamt = false;
                                    }
                                    else
                                    {
                                        customizeamt = true;
                                    }

                                    if (customizefee == true || customizeamt == true)
                                    {
                                        tick.T_Customize = "1";
                                    }
                                    else
                                    {
                                        tick.T_Customize = "0";
                                    }
                                    if (tick.TicketTypeID == 3)
                                    {
                                        if(tick.Customer_Fee>0)
                                            {
                                            tick.T_Customize = "1";

                                        }
                                    }
                                    


                                        ticket.T_Customize = tick.T_Customize;

                                    ticket.T_EcAmount = tick.T_EcAmount;
                                    ticket.T_Ecpercent = tick.T_Ecpercent;
                                }

                            }

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
                            ticket.T_Displayremaining = tick.T_Displayremaining;
                            if (tick.Isadmin == "Y")
                            {
                                ticket.EC_Fee = tick.EC_Fee;
                                ticket.Customer_Fee = tick.Customer_Fee;
                            }
                            //ticket.EC_Fee = tick.EC_Fee;
                            //ticket.Customer_Fee = tick.Customer_Fee;
                            ticket.TotalPrice = tick.TotalPrice;
                            ticket.T_Discount = tick.T_Discount;
                            if (tick.T_Id == 0)
                            {
                                objEnt.Tickets.Add(ticket);
                            }
                            ids.Add(ticket.T_Id);
                            //ticket.T_EcAmount=tick.T_EcAmount;
                            //ticket.T_Ecpercent = tick.T_Ecpercent;
                            //ticket.T_Customize = tick.T_Customize;
                            //objEnt.Tickets.Add(ticket);
                        }
                    }
                  
                    if (model.EventImage != null)
                    {
                        EventImage Image = new EventImage();
                        foreach (EventImage img in model.EventImage)
                        {
                            Image = new EventImage();
                            Image.EventID = ObjEC.EventID;
                            Image.EventImageUrl = img.EventImageUrl;
                            Image.ImageType = img.ImageType;
                            objEnt.EventImages.Add(Image);
                        }
                    }

                    if (model.EventVariable != null)
                    {
                        Event_VariableDesc var = new Event_VariableDesc();
                        foreach (Event_VariableDesc variable in model.EventVariable)
                        {
                            var = new Event_VariableDesc();
                            var.Event_Id = ObjEC.EventID;
                            var.VariableDesc = variable.VariableDesc;
                            var.Price = variable.Price;
                            objEnt.Event_VariableDesc.Add(var);
                        }
                    }
                   objEnt.SaveChanges();
                    lEventId = ObjEC.EventID;
                    PublishEvent(lEventId);
                }
            }
            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
              return 0;
            }
            return lEventId;
        }


        public long Draftmodemodification(EventCreation model,string strDuplicate)
        {
            string lat = "", lon = "";
            ViewEvent vc = new ViewEvent();
            ValidationMessage vmc = new ValidationMessage();
            List<Email_Tag> EmailTag = new List<Email_Tag>();
            MyAccount ac = new MyAccount();
            EmailTag = ac.getTag();
            var Emailtemplate = ac.getEmail("new_event_notification_email");
            string to = "", from = "", cc = "", bcc = "", emailname = "", subjectn = "", bodyn = "";
            if (Session["AppId"] != null)
            {
                long lEventId = model.EventID;
                long Parent_EventID=0;
                List<long> ids = new List<long>();
                try
                {
                    if (strDuplicate == "Y")
                    {
                        CreateEventController objCE = new CreateEventController();
                        objCE.ControllerContext = new ControllerContext(this.Request.RequestContext, objCE);
                        model.EventTitle = "Copy of " + model.EventTitle;
                        model.EventUrl = "Copy of " + model.EventUrl;
                        lEventId = objCE.SaveEvent(model);
                        var userid = Session["AppId"].ToString();
                        string Organisername = "", Organiseremail = "", Organiserphn = ""; ;
                        var eventdetails = (from ev in db.Events where ev.EventID == lEventId select ev).FirstOrDefault();
                        var address = (from ev in db.Addresses where ev.EventId == lEventId select ev.ConsolidateAddress).FirstOrDefault();
                        address = !string.IsNullOrEmpty(address) ? address : ""; 
                        //if(eventdetails!=null && eventdetails.EventStatus=="Live")
                        //{ 
                        var OrganiserDetail = (from ev in db.Event_Orgnizer_Detail join pfd in db.Organizer_Master on ev.OrganizerMaster_Id equals pfd.Orgnizer_Id where ev.Orgnizer_Event_Id == lEventId && ev.DefaultOrg == "Y" select pfd).FirstOrDefault();
                        var Organiserdetail = db.Profiles.FirstOrDefault(i => i.UserID == OrganiserDetail.UserId);
                        var userdetail = db.Profiles.FirstOrDefault(i => i.UserID == userid);
                        if (Organiserdetail != null)
                        {
                            Organisername = !String.IsNullOrEmpty(OrganiserDetail.Orgnizer_Name) ? OrganiserDetail.Orgnizer_Name : Organiserdetail.FirstName != null ? Organiserdetail.FirstName : "";
                            Organiseremail = !String.IsNullOrEmpty(OrganiserDetail.Organizer_Email) ? OrganiserDetail.Organizer_Email : Organiserdetail.Email != null ? Organiserdetail.Email : "";
                            Organiserphn = !string.IsNullOrEmpty(OrganiserDetail.Organizer_Phoneno) ? " or call " + OrganiserDetail.Organizer_Phoneno : Organiserdetail.MainPhone != null ? " or call " + Organiserdetail.MainPhone : "";
                        }

                      
                   
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
                                            subjectn = subjectn.Replace("¶¶EventTitleId¶¶", model.EventTitle);

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
                                            bodyn = bodyn.Replace("¶¶EventTitleId¶¶", model.EventTitle);

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
                                            string strUrl = baseurl + Url.Action("ViewEvent", "EventManagement", new { strEventDs = System.Text.RegularExpressions.Regex.Replace(model.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = ValidationMessageController.GetParentEventId(lEventId).ToString() });
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
                        return lEventId;
                    }
                    string strUserId = (Session["AppId"] != null ? Session["AppId"].ToString() : "");
                    using (EventComboEntities objEnt = new EventComboEntities())
                    {
                        Event ObjEC = objEnt.Events.FirstOrDefault(i => i.EventID == lEventId);
                        if (ObjEC != null)
                        {
                            if (ObjEC.EventStatus == "Live")
                            {
                                var eventid = model.EventID;
                                model.EventID = vmc.GetLatestEventId(lEventId);
                                lEventId = EditsingleLiveInfo(model);
                                //lEventId = EditEventInfo(model);

                                Parent_EventID = vmc.GetParentEventId(lEventId);


                                var promocode = (from p in db.Promo_Code where p.PC_Eventid == eventid select p).Any();
                                if(promocode)
                                {
                                    string sql = @"update Promo_Code set PC_Eventid={0} where PC_Eventid={1}";
                                    List<Object> sqlParamsList = new List<object>();
                                    sqlParamsList.Add(lEventId);
                                    sqlParamsList.Add(eventid);
                                    objEnt.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());

                                }

                            }
                            else
                            {
                                objEnt.Publish_Event_Detail.RemoveRange(objEnt.Publish_Event_Detail.Where(x => x.PE_Event_Id == lEventId).ToList());
                                objEnt.Ticket_Quantity_Detail.RemoveRange(objEnt.Ticket_Quantity_Detail.Where(x => x.TQD_Event_Id == lEventId).ToList());
                                //  var vParentEvt = (from myEnt in objEnt.Events where myEnt.EventID == lEventId select myEnt.Parent_EventID).FirstOrDefault();
                                // if (vParentEvt == 0) vParentEvt = lEventId;
                                var addressstatus = model.AddressStatus;
                                if (model.AddressDetail == null)
                                {
                                    objEnt.Addresses.RemoveRange(objEnt.Addresses.Where(x => x.EventId == lEventId));
                                    if (addressstatus != "PastLocation" && addressstatus != "Online")
                                    {
                                        model.AddressStatus = "";
                                    }
                                }
                                // Event ObjEC = new Event();
                                ObjEC.Parent_EventID = 0;
                                ObjEC.EventTypeID = model.EventTypeID;
                                ObjEC.EventCategoryID = model.EventCategoryID;
                                ObjEC.EventSubCategoryID = model.EventSubCategoryID;
                                ObjEC.UserID = strUserId;
                                ObjEC.EventTitle = model.EventTitle;
                                ObjEC.DisplayStartTime = model.DisplayStartTime;
                                ObjEC.DisplayEndTime = model.DisplayEndTime;
                                ObjEC.DisplayTimeZone = model.DisplayTimeZone;
                                ObjEC.EventDescription = Server.HtmlEncode(model.EventDescription);
                                ObjEC.EventPrivacy = model.EventPrivacy;
                                ObjEC.Private_ShareOnFB = model.Private_ShareOnFB;
                                ObjEC.Private_GuestOnly = model.Private_GuestOnly;
                                ObjEC.Private_Password = model.Private_Password;
                                ObjEC.EventUrl = model.EventUrl;
                                ObjEC.PublishOnFB = model.PublishOnFB;
                                ObjEC.EventStatus = model.EventStatus;
                                ObjEC.IsMultipleEvent = model.IsMultipleEvent;
                                ObjEC.TimeZone = model.TimeZone;
                                ObjEC.DisplayStartTime = model.DisplayStartTime;
                                ObjEC.DisplayEndTime = model.DisplayEndTime;
                                ObjEC.DisplayTimeZone = model.DisplayTimeZone;
                                ObjEC.FBUrl = model.FBUrl;
                                ObjEC.TwitterUrl = model.TwitterUrl;
                                ObjEC.LastLocationAddress = model.LastLocationAddress;
                                ObjEC.AddressStatus = model.AddressStatus;
                                ObjEC.EnableFBDiscussion = model.EnableFBDiscussion;
                                ObjEC.Ticket_DAdress = model.Ticket_DAdress;
                                ObjEC.Ticket_showremain = model.Ticket_showremain;
                                ObjEC.Ticket_showvariable = model.Ticket_showvariable;
                                ObjEC.Ticket_variabledesc = model.Ticket_variabledesc;
                                ObjEC.Ticket_variabletype = model.Ticket_variabletype;
                                ObjEC.ShowMap = model.ShowMap;

                                var timezone = "";

                                DateTimeWithZone dtzCreated;
                                var Timezonedetail = (from ev in db.TimeZoneDetails where ev.TimeZone_Id.ToString() == model.TimeZone select ev).FirstOrDefault();
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

                                ObjEC.ModifyDate = dtzCreated.UniversalTime;
                                //objEnt.Events.Add(ObjEC);
                                // Address info
                                string adderess = "";
                                Address ObjAdd = new Models.Address();
                                // 
                                ids = new List<long>();

                                if (model.AddressStatus == "Online")
                                {
                                    objEnt.Addresses.RemoveRange(objEnt.Addresses.Where(x => x.EventId == lEventId));
                                }
                                else if (model.AddressStatus == "PastLocation")
                                {
                                    objEnt.Addresses.RemoveRange(objEnt.Addresses.Where(x => x.EventId == lEventId));
                                }
                                else
                                {
                                    if (model.AddressStatus == "Single")
                                    {
                                        objEnt.Addresses.RemoveRange(objEnt.Addresses.Where(x => x.EventId == lEventId));
                                    }
                                    if (model.AddressDetail != null)
                                    {
                                        foreach (Address objA in model.AddressDetail)
                                        {


                                            if ((objA.VenueName != null && objA.VenueName.Trim() != "") || (objA.ConsolidateAddress != "" && objA.ConsolidateAddress != null))
                                            {
                                                try
                                                {
                                                    var geocode = vc.Geocode(objA.ConsolidateAddress);
                                                    lat = geocode.Latitude.ToString();
                                                    lon = geocode.Longitude.ToString();
                                                }
                                                catch (Exception ex)
                                                {
                                                    lat = "";
                                                    lon = "";

                                                }
                                                if (objA.AddressID == 0)
                                                {
                                                    ObjAdd = new Models.Address();
                                                }
                                                else
                                                {
                                                    ObjAdd = (from obj in objEnt.Addresses where obj.AddressID == objA.AddressID && obj.EventId == lEventId select obj).FirstOrDefault();
                                                }
                                                ObjAdd.EventId = lEventId;
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
                                                ObjAdd.Latitude = lat;
                                                ObjAdd.Longitude = lon;
                                                if (objA.AddressID == 0)
                                                {
                                                    objEnt.Addresses.Add(ObjAdd);
                                                }
                                                adderess = objA.ConsolidateAddress;
                                                ids.Add(ObjAdd.AddressID);
                                            }

                                        }
                                        var results = objEnt.Addresses.Where(x => !ids.Contains(x.AddressID) && x.EventId == lEventId).ToList();
                                        if (results != null)
                                        {
                                            objEnt.Addresses.RemoveRange(results);
                                        }
                                    }
                                }

                                DateTimeWithZone dtzstart, dtzend;
                                DateTimeWithZone dtzCreatedstart, dtzCreatedend;

                                // Event on Single Timing 
                                if (model.EventVenue != null)
                                {
                                    //To be discuss  
                                    objEnt.MultipleEvents.RemoveRange(objEnt.MultipleEvents.Where(x => x.EventID == lEventId).ToList());
                                    objEnt.EventVenues.RemoveRange(objEnt.EventVenues.Where(x => x.EventID == lEventId).ToList());
                                    EventVenue objEVenue = new EventVenue();

                                    foreach (EventVenue objEv in model.EventVenue)
                                    {
                                        //save utc


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
                                        //

                                        objEVenue.EventID = lEventId;
                                        objEVenue.EventStartDate = objEv.EventStartDate;
                                        objEVenue.EventEndDate = objEv.EventEndDate;

                                        objEVenue.EventStartTime = objEv.EventStartTime;
                                        objEVenue.EventEndTime = objEv.EventEndTime;
                                        objEVenue.E_Startdate = dtzstart.UniversalTime;
                                        objEVenue.E_Enddate= dtzend.UniversalTime;
                                        objEnt.EventVenues.Add(objEVenue);


                                    }
                                }
                                // Event on Multiple timing 
                                if (model.MultipleEvents != null)
                                {
                                    objEnt.EventVenues.RemoveRange(objEnt.EventVenues.Where(x => x.EventID == lEventId).ToList());
                                    objEnt.MultipleEvents.RemoveRange(objEnt.MultipleEvents.Where(x => x.EventID == lEventId).ToList());

                                    MultipleEvent objMEvents = new MultipleEvent();
                                    foreach (MultipleEvent objME in model.MultipleEvents)
                                    {

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
                                        objMEvents.EventID = lEventId;
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

                                        ObjEC.MultipleEvents.Add(objMEvents);


                                    }
                                }
                                // Orgnizer
                                if (model.Orgnizer != null)
                                {
                                    Organizer_Master objEOrg = new Organizer_Master();
                                    Event_Orgnizer_Detail evntorg = new Event_Orgnizer_Detail();
                                    var results = objEnt.Event_Orgnizer_Detail.Where( x=>x.Orgnizer_Event_Id == lEventId).ToList();
                                    if (results != null)
                                    {
                                        objEnt.Event_Orgnizer_Detail.RemoveRange(results);
                                    }
                                    var neworg = model.Orgnizer.Where(x => x.Orgnizer_Id == 0).Any();
                                    if (neworg)
                                    {
                                        var defaultorg = model.Orgnizer.Where(x => x.Orgnizer_Id == 0 && x.DefaultOrg == "Y").Any();
                                        if (defaultorg)
                                        {
                                            var orgnew = model.Orgnizer.Where(x => x.Orgnizer_Id == 0).ToList();
                                            foreach (Organizer_Master objOr in orgnew)
                                            {

                                                objEOrg = new Organizer_Master();
                                                // objEOrg.Orgnizer_Event_Id = ObjEC.EventID;
                                                objEOrg.Orgnizer_Name = objOr.Orgnizer_Name;
                                                objEOrg.Organizer_Desc = Server.HtmlEncode(objOr.Organizer_Desc);
                                                objEOrg.Organizer_FBLink = objOr.Organizer_FBLink;
                                                objEOrg.Organizer_Twitter = objOr.Organizer_Twitter;
                                                //objEOrg.DefaultOrg = objOr.DefaultOrg;
                                                objEOrg.Organizer_Linkedin = objOr.Organizer_Linkedin;
                                                objEOrg.UserId = strUserId;
                                                objEOrg.Organizer_Status = "A";
                                                objEnt.Organizer_Master.Add(objEOrg);
                                                if (objOr.DefaultOrg == "Y")
                                                {
                                                    evntorg = new Event_Orgnizer_Detail();
                                                    evntorg.DefaultOrg = objOr.DefaultOrg;
                                                    evntorg.OrganizerMaster_Id = objEOrg.Orgnizer_Id;
                                                  
                                                    evntorg.Orgnizer_Event_Id = ObjEC.EventID;
                                                    evntorg.UserId = strUserId;
                                                    objEnt.Event_Orgnizer_Detail.Add(evntorg);
                                                }



                                            }
                                        }
                                        else
                                        {
                                            var orgnew = model.Orgnizer.Where(x => x.Orgnizer_Id == 0).ToList();
                                            foreach (Organizer_Master objOr in orgnew)
                                            {

                                                objEOrg = new Organizer_Master();
                                                // objEOrg.Orgnizer_Event_Id = ObjEC.EventID;
                                                objEOrg.Orgnizer_Name = objOr.Orgnizer_Name;
                                                objEOrg.Organizer_Desc = Server.HtmlEncode(objOr.Organizer_Desc);
                                                objEOrg.Organizer_FBLink = objOr.Organizer_FBLink;
                                                objEOrg.Organizer_Twitter = objOr.Organizer_Twitter;
                                                //objEOrg.DefaultOrg = objOr.DefaultOrg;
                                                objEOrg.Organizer_Linkedin = objOr.Organizer_Linkedin;
                                                objEOrg.UserId = strUserId;
                                                objEOrg.Organizer_Status = "A";
                                                objEnt.Organizer_Master.Add(objEOrg);




                                            }
                                            var defaultorgold = model.Orgnizer.Where(x => x.DefaultOrg == "Y").ToList();
                                            foreach (Organizer_Master objOr in defaultorgold)
                                            {
                                                evntorg = new Event_Orgnizer_Detail();
                                                evntorg.DefaultOrg = objOr.DefaultOrg;
                                                evntorg.OrganizerMaster_Id = objOr.Orgnizer_Id;
                                                evntorg.Orgnizer_Event_Id = ObjEC.EventID;
                                                evntorg.UserId = strUserId;
                                                objEnt.Event_Orgnizer_Detail.Add(evntorg);
                                            }
                                            var editoldorg = model.Orgnizer.Where(x => x.EditOrg == "1").ToList();
                                            if (editoldorg != null)
                                            {
                                                foreach (Organizer_Master objOr in editoldorg)
                                                {
                                                    string sql = @"update Organizer_Master set Orgnizer_Name={0},Organizer_Desc={1},Organizer_FBLink={2},Organizer_Twitter={3},Organizer_Linkedin={4} where Orgnizer_Id={5}";
                                                    List<Object> sqlParamsList = new List<object>();
                                                    sqlParamsList.Add(objOr.Orgnizer_Name);
                                                    sqlParamsList.Add(Server.HtmlEncode(objOr.Organizer_Desc));
                                                    sqlParamsList.Add(objOr.Organizer_FBLink);
                                                    sqlParamsList.Add(objOr.Organizer_Twitter);
                                                    sqlParamsList.Add(objOr.Organizer_Linkedin);
                                                    sqlParamsList.Add(objOr.Orgnizer_Id);

                                                    objEnt.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());
                                                }
                                            }



                                        }

                                    }
                                    else
                                    {
                                        var defaultorg = model.Orgnizer.Where(x => x.DefaultOrg == "Y").ToList();
                                        foreach (Organizer_Master objOr in defaultorg)
                                        {
                                            evntorg = new Event_Orgnizer_Detail();
                                            evntorg.DefaultOrg = objOr.DefaultOrg;
                                            evntorg.OrganizerMaster_Id = objOr.Orgnizer_Id;
                                            evntorg.Orgnizer_Event_Id = ObjEC.EventID;
                                            evntorg.UserId = strUserId;
                                            objEnt.Event_Orgnizer_Detail.Add(evntorg);
                                        }
                                        var editoldorg = model.Orgnizer.Where(x => x.EditOrg == "1").ToList();
                                        if (editoldorg != null)
                                        {
                                            foreach (Organizer_Master objOr in editoldorg)
                                            {
                                                string sql = @"update Organizer_Master set Orgnizer_Name={0},Organizer_Desc={1},Organizer_FBLink={2},Organizer_Twitter={3},Organizer_Linkedin={4} where Orgnizer_Id={5}";
                                                List<Object> sqlParamsList = new List<object>();
                                                sqlParamsList.Add(objOr.Orgnizer_Name);
                                                sqlParamsList.Add(Server.HtmlEncode(objOr.Organizer_Desc));
                                                sqlParamsList.Add(objOr.Organizer_FBLink);
                                                sqlParamsList.Add(objOr.Organizer_Twitter);
                                                sqlParamsList.Add(objOr.Organizer_Linkedin);
                                                sqlParamsList.Add(objOr.Orgnizer_Id);

                                                objEnt.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());
                                            }
                                        }
                                    }
                                }
                                // Tickets

                                string customize ="0";
                                var customizefee = false;
                                var customizeamt = false;

                                if (model.Ticket != null)
                                {
                                    Ticket ticket = new Ticket();
                                    ids = new List<long>();
                                    // objEnt.Ticket_Quantity_Detail.RemoveRange(objEnt.Ticket_Quantity_Detail.Where(x => x.TQD_Event_Id == lEventId));
                                    //  objEnt.Tickets.RemoveRange(objEnt.Tickets.Where(x => x.E_Id == lEventId));
                                    foreach (Ticket tick in model.Ticket)
                                    {
                                        if (tick.T_Id == 0)
                                        {
                                            ticket = new Ticket();
                                         
                                            ticket.T_Customize = "0";
                                            var mainfee = (from db in objEnt.Fee_Structure select db).FirstOrDefault();
                                            ticket.EC_Fee = tick.Customer_Fee;
                                            ticket.T_Ecpercent = mainfee.FS_Percentage;
                                            ticket.T_EcAmount = mainfee.FS_Amount;
                                            if (tick.TicketTypeID == 2)
                                            {
                                                ticket.Customer_Fee = tick.Customer_Fee;
                                            }
                                            else
                                            {
                                                ticket.Customer_Fee = 0;
                                            }

                                        }
                                        else
                                        {
                                            ticket = (from obj in objEnt.Tickets where obj.T_Id == tick.T_Id && obj.E_Id == lEventId select obj).FirstOrDefault();
                                            customize = ticket.T_Customize;
                                            if (tick.TicketTypeID == 2)
                                            {
                                                ticket.Customer_Fee = tick.Customer_Fee;
                                            }
                                        
                                        }
                                        if (tick.Isadmin == "Y")
                                        {
                                            if (tick.TicketTypeID != 1)
                                            {
                                                var mainfee = (from db in objEnt.Fee_Structure select db).FirstOrDefault();
                                                var oldamnt=0m;

                                                if (tick.Fees_Type == "0")
                                                {
                                                     oldamnt = ((((tick.Price * mainfee.FS_Percentage) / 100) + mainfee.FS_Amount) + tick.Price)??0;
                                                }
                                                else
                                                {
                                                     oldamnt = tick.Price??0 ;

                                                }
                                                var oldamount = Decimal.Round(oldamnt, 2);
                                           
                                              
                                                
                                                if (mainfee.FS_Percentage== tick.T_Ecpercent && mainfee.FS_Amount== tick.T_EcAmount)
                                                {
                                                    customizefee = false;
                                                   
                                                }
                                                else
                                                {
                                                    customizefee = true;
                                                   
                                                }
                                              if(oldamount==tick.TotalPrice)
                                                {
                                                    customizeamt = false;
                                                }
                                                else
                                                {
                                                    customizeamt = true;
                                                }

                                              if(customizefee==true || customizeamt == true)
                                                {
                                                    tick.T_Customize = "1";
                                                }
                                                else
                                                {
                                                    tick.T_Customize = "0";
                                                }
                                                if (tick.TicketTypeID == 3)
                                                {
                                                    if (tick.Customer_Fee > 0)
                                                    {
                                                        tick.T_Customize = "1";

                                                    }
                                                }


                                                ticket.T_Customize = tick.T_Customize;

                                                ticket.T_EcAmount = tick.T_EcAmount;
                                                ticket.T_Ecpercent = tick.T_Ecpercent;
                                            }

                                        }
                                        else
                                        {

                                        }
                                       
                                        ticket.E_Id = lEventId;
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
                                        ticket.T_Displayremaining = tick.T_Displayremaining;
                                        if (tick.Isadmin == "Y")
                                        {
                                            ticket.EC_Fee = tick.EC_Fee;
                                            ticket.Customer_Fee = tick.Customer_Fee;
                                        }
                                     
                                        ticket.TotalPrice = tick.TotalPrice;
                                        ticket.T_Discount = tick.T_Discount;
                                        if (tick.T_Id == 0)
                                        {
                                            objEnt.Tickets.Add(ticket);
                                        }
                                        ids.Add(ticket.T_Id);
                                    }


                                    //var ids = new HashSet<long>(objEnt.Tickets.Where(x => x.E_Id== lEventId).Select(x=>x.T_Id));

                                    var resultticks = objEnt.Tickets.Where(x => !ids.Contains(x.T_Id) && x.E_Id == lEventId).ToList();
                                  
                                    if (resultticks != null)
                                    {objEnt.Tickets.RemoveRange(resultticks);
                                    }


                                }

                                if (model.EventImage != null)
                                {
                                    EventImage Image = new EventImage();
                                    objEnt.EventImages.RemoveRange(objEnt.EventImages.Where(x => x.EventID == lEventId));
                                    foreach (EventImage img in model.EventImage)
                                    {
                                        Image = new EventImage();
                                        Image.EventID = lEventId;
                                        Image.EventImageUrl = img.EventImageUrl;
                                        Image.ImageType = img.ImageType;
                                        objEnt.EventImages.Add(Image);
                                    }
                                }
                                else
                                {
                                    objEnt.EventImages.RemoveRange(objEnt.EventImages.Where(x => x.EventID == lEventId));
                                }

                                if (model.EventVariable != null)
                                {
                                    Event_VariableDesc var = new Event_VariableDesc();
                                    ids = new List<long>();
                                    foreach (Event_VariableDesc variable in model.EventVariable)
                                    {
                                        if (variable.Variable_Id == 0)
                                        {
                                            var = new Event_VariableDesc();
                                        }
                                        else
                                        {
                                            var = (from obj in objEnt.Event_VariableDesc where obj.Variable_Id == variable.Variable_Id && obj.Event_Id == lEventId select obj).FirstOrDefault();

                                        }
                                        var.Event_Id = lEventId;
                                        var.VariableDesc = variable.VariableDesc;
                                        var.Price = variable.Price;
                                        if (variable.Variable_Id == 0)
                                        {
                                            objEnt.Event_VariableDesc.Add(var);

                                        }
                                        ids.Add(variable.Variable_Id);
                                    }
                                    var resultdesc = objEnt.Event_VariableDesc.Where(x => !ids.Contains(x.Variable_Id) && x.Event_Id == lEventId).ToList();
                                    if (resultdesc != null)
                                    {
                                        objEnt.Event_VariableDesc.RemoveRange(resultdesc);
                                    }

                                }
                                else
                                {
                                    objEnt.Event_VariableDesc.RemoveRange(objEnt.Event_VariableDesc.Where(x => x.Event_Id == lEventId));
                                }
                                objEnt.SaveChanges();
                                lEventId = ObjEC.EventID;

                                //if (ObjEC.Parent_EventID == null || ObjEC.Parent_EventID == 0)
                                //    Parent_EventID = lEventId;
                                //else
                                //    Parent_EventID = (long)ObjEC.Parent_EventID;
                                Parent_EventID = vmc.GetParentEventId(lEventId);
                                //Parent_EventID = (ObjEC.Parent_EventID ==null? lEventId : (long)ObjEC.Parent_EventID);

                                PublishEvent(lEventId);



                                string Organisername = "", Organiseremail = "", Organiserphn = ""; ;
                                var eventdetails = (from ev in db.Events where ev.EventID == lEventId select ev).FirstOrDefault();
                                if (eventdetails != null && eventdetails.EventStatus == "Live")
                                {
                                    var OrganiserDetail = (from ev in db.Event_Orgnizer_Detail join pfd in db.Organizer_Master on ev.OrganizerMaster_Id equals pfd.Orgnizer_Id where ev.Orgnizer_Event_Id == lEventId && ev.DefaultOrg == "Y" select pfd).FirstOrDefault();
                                    var Organiserdetail = db.Profiles.FirstOrDefault(i => i.UserID == OrganiserDetail.UserId);
                                    var userdetail = db.Profiles.FirstOrDefault(i => i.UserID == strUserId);

                                    if (Organiserdetail != null)
                                    {
                                        Organisername = !String.IsNullOrEmpty(OrganiserDetail.Orgnizer_Name) ? OrganiserDetail.Orgnizer_Name : Organiserdetail.FirstName != null ? Organiserdetail.FirstName : "";
                                        Organiseremail = !String.IsNullOrEmpty(OrganiserDetail.Organizer_Email) ? OrganiserDetail.Organizer_Email : Organiserdetail.Email != null ? Organiserdetail.Email : "";
                                        Organiserphn = !string.IsNullOrEmpty(OrganiserDetail.Organizer_Phoneno) ? " or call " + OrganiserDetail.Organizer_Phoneno : Organiserdetail.MainPhone != null ? " or call " + Organiserdetail.MainPhone : "";
                                    }

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
                                                        subjectn = subjectn.Replace("¶¶EventTitleId¶¶", model.EventTitle);

                                                    }
                                                    if (EmailTag[i].Tag_Name == "EventAddressID")
                                                    {
                                                        subjectn = subjectn.Replace("¶¶EventAddressID¶¶", adderess);

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
                                                        bodyn = bodyn.Replace("¶¶EventTitleId¶¶", model.EventTitle);

                                                    }
                                                    if (EmailTag[i].Tag_Name == "EventOrganiserEmail")
                                                    {
                                                        bodyn = bodyn.Replace("¶¶EventOrganiserEmail¶¶", Organiseremail);

                                                    }
                                                    if (EmailTag[i].Tag_Name == "EventAddressID")
                                                    {
                                                        bodyn = bodyn.Replace("¶¶EventAddressID¶¶", adderess);

                                                    }
                                                    if (EmailTag[i].Tag_Name == "EventOrganiserNumber")
                                                    {
                                                        bodyn = bodyn.Replace("¶¶EventOrganiserNumber¶¶", Organiserphn);

                                                    }

                                                }

                                            }
                                        }

                                        // ImageMapPath = Server.MapPath("..") + "/Images/Imagemap_"+EvtOrDetail.TPD_Order_Id+ ".png";
                                        //Mail 
                                        ac.SendHtmlFormattedEmail(to, from, subjectn, bodyn, cc, bcc, "", emailname);
                                        //Mail 
                                    }



                                }





                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                  logger.Error("Exception during request processing", ex);
                  return 0;
                }
                return Parent_EventID;
            }
            else
            {
                return -1;
            }
        }

        private long EditsingleLiveInfo(EventCreation model)
        {
            string lat = "", lon = "";
            List<long> ids = new List<long>();
            ViewEvent vc = new ViewEvent();
            var lEventId = model.EventID;
            using (EventComboEntities objEnt = new EventComboEntities())
            {
               
                Event ObjEC = objEnt.Events.FirstOrDefault(i => i.EventID == lEventId);
                if (ObjEC != null)
                {
                    string strUserId = (Session["AppId"] != null ? Session["AppId"].ToString() : "");
                    var addressstatus = model.AddressStatus;
                    if (model.AddressDetail == null)
                    {
                        objEnt.Addresses.RemoveRange(objEnt.Addresses.Where(x => x.EventId == lEventId));
                        if (addressstatus != "PastLocation" && addressstatus != "Online")
                        {
                            model.AddressStatus = "";
                        }
                    }
                    // Event ObjEC = new Event();
                    ObjEC.Parent_EventID = 0;
                    ObjEC.EventTypeID = model.EventTypeID;
                    ObjEC.EventCategoryID = model.EventCategoryID;
                    ObjEC.EventSubCategoryID = model.EventSubCategoryID;
                    ObjEC.UserID = strUserId;
                    ObjEC.EventTitle = model.EventTitle;
                    ObjEC.DisplayStartTime = model.DisplayStartTime;
                    ObjEC.DisplayEndTime = model.DisplayEndTime;
                    ObjEC.DisplayTimeZone = model.DisplayTimeZone;
                    ObjEC.EventDescription = Server.HtmlEncode(model.EventDescription);
                    ObjEC.EventPrivacy = model.EventPrivacy;
                    ObjEC.Private_ShareOnFB = model.Private_ShareOnFB;
                    ObjEC.Private_GuestOnly = model.Private_GuestOnly;
                    ObjEC.Private_Password = model.Private_Password;
                    ObjEC.EventUrl = model.EventUrl;
                    ObjEC.PublishOnFB = model.PublishOnFB;
                    ObjEC.EventStatus = "Live";
                    ObjEC.IsMultipleEvent = model.IsMultipleEvent;
                    ObjEC.TimeZone = model.TimeZone;
                    ObjEC.DisplayStartTime = model.DisplayStartTime;
                    ObjEC.DisplayEndTime = model.DisplayEndTime;
                    ObjEC.DisplayTimeZone = model.DisplayTimeZone;
                    ObjEC.FBUrl = model.FBUrl;
                    ObjEC.TwitterUrl = model.TwitterUrl;
                    ObjEC.LastLocationAddress = model.LastLocationAddress;
                    ObjEC.AddressStatus = model.AddressStatus;
                    ObjEC.EnableFBDiscussion = model.EnableFBDiscussion;
                    ObjEC.Ticket_DAdress = model.Ticket_DAdress;
                    ObjEC.Ticket_showremain = model.Ticket_showremain;
                    ObjEC.Ticket_showvariable = model.Ticket_showvariable;
                    ObjEC.Ticket_variabledesc = model.Ticket_variabledesc;
                    ObjEC.Ticket_variabletype = model.Ticket_variabletype;
                    ObjEC.ShowMap = model.ShowMap;

                    var timezone = "";

                    DateTimeWithZone dtzCreated;
                    var Timezonedetail = (from ev in db.TimeZoneDetails where ev.TimeZone_Id.ToString() == model.TimeZone select ev).FirstOrDefault();
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

                    ObjEC.ModifyDate = dtzCreated.UniversalTime;
                    //objEnt.Events.Add(ObjEC);
                    // Address info

                    Address ObjAdd = new Models.Address();
                    // 
                   

                    if (model.AddressStatus == "Online")
                    {
                        objEnt.Addresses.RemoveRange(objEnt.Addresses.Where(x => x.EventId == lEventId));
                    }
                    else if (model.AddressStatus == "PastLocation")
                    {
                        objEnt.Addresses.RemoveRange(objEnt.Addresses.Where(x => x.EventId == lEventId));
                    }
                    else
                    {
                      
                        if (model.AddressDetail != null)
                        {
                            foreach (Address objA in model.AddressDetail)
                            {


                                if ((objA.VenueName != null && objA.VenueName.Trim() != "") || (objA.ConsolidateAddress != "" && objA.ConsolidateAddress != null))
                                {
                                    try
                                    {
                                        var geocode = vc.Geocode(objA.ConsolidateAddress);
                                        lat = geocode.Latitude.ToString();
                                        lon = geocode.Longitude.ToString();
                                    }
                                    catch (Exception ex)
                                    {
                                        lat = "";
                                        lon = "";

                                    }
                                    ObjAdd= (from obj in objEnt.Addresses where  obj.EventId == lEventId select obj).FirstOrDefault();
                                    if (ObjAdd == null)
                                    {
                                        ObjAdd = new Address();
                                    }
                                    else
                                    { objA.AddressID = ObjAdd.AddressID; }
                                        
                                      
                                        ObjAdd.EventId = lEventId;
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
                                        ObjAdd.Latitude = lat;
                                        ObjAdd.Longitude = lon;

                                    if (objA.AddressID == 0)
                                    {
                                        objEnt.Addresses.Add(ObjAdd);
                                    }
                                }

                            }
                          }
                    }


                    DateTimeWithZone dtzstart, dtzend;
                    DateTimeWithZone dtzCreatedstart, dtzCreatedend;

                    // Event on Single Timing 
                    if (model.EventVenue != null)
                    {
                        //To be discuss  
                          EventVenue objEVenue = new EventVenue();

                        foreach (EventVenue objEv in model.EventVenue)
                        {
                            //save utc


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
                            //

                            objEVenue = (from obj in objEnt.EventVenues where  obj.EventID == lEventId select obj).FirstOrDefault();
                            
                            //objEVenue.EventID = lEventId;
                            objEVenue.EventStartDate = objEv.EventStartDate;
                            objEVenue.EventEndDate = objEv.EventEndDate;

                            objEVenue.EventStartTime = objEv.EventStartTime;
                            objEVenue.EventEndTime = objEv.EventEndTime;
                            objEVenue.E_Startdate = dtzstart.UniversalTime;
                            objEVenue.E_Enddate = dtzend.UniversalTime;
                           // objEnt.EventVenues.Add(objEVenue);


                        }
                    }

                    if (model.Orgnizer != null)
                    {
                        Organizer_Master objEOrg = new Organizer_Master();
                        Event_Orgnizer_Detail evntorg = new Event_Orgnizer_Detail();
                        var results = objEnt.Event_Orgnizer_Detail.Where(x => x.Orgnizer_Event_Id == lEventId).ToList();
                        if (results != null)
                        {
                            objEnt.Event_Orgnizer_Detail.RemoveRange(results);
                        }
                        var neworg = model.Orgnizer.Where(x => x.Orgnizer_Id == 0).Any();
                        if (neworg)
                        {
                            var defaultorg = model.Orgnizer.Where(x => x.Orgnizer_Id == 0 && x.DefaultOrg == "Y").Any();
                            if (defaultorg)
                            {
                                var orgnew = model.Orgnizer.Where(x => x.Orgnizer_Id == 0).ToList();
                                foreach (Organizer_Master objOr in orgnew)
                                {

                                    objEOrg = new Organizer_Master();
                                    // objEOrg.Orgnizer_Event_Id = ObjEC.EventID;
                                    objEOrg.Orgnizer_Name = objOr.Orgnizer_Name;
                                    objEOrg.Organizer_Desc = Server.HtmlEncode(objOr.Organizer_Desc);
                                    objEOrg.Organizer_FBLink = objOr.Organizer_FBLink;
                                    objEOrg.Organizer_Twitter = objOr.Organizer_Twitter;
                                    //objEOrg.DefaultOrg = objOr.DefaultOrg;
                                    objEOrg.Organizer_Linkedin = objOr.Organizer_Linkedin;
                                    objEOrg.UserId = strUserId;
                                    objEOrg.Organizer_Status = "A";
                                    objEnt.Organizer_Master.Add(objEOrg);
                                    if (objOr.DefaultOrg == "Y")
                                    {
                                        evntorg = new Event_Orgnizer_Detail();
                                        evntorg.DefaultOrg = objOr.DefaultOrg;
                                        evntorg.OrganizerMaster_Id = objEOrg.Orgnizer_Id;

                                        evntorg.Orgnizer_Event_Id = ObjEC.EventID;
                                        evntorg.UserId = strUserId;
                                        objEnt.Event_Orgnizer_Detail.Add(evntorg);
                                    }



                                }
                            }
                            else
                            {
                                var orgnew = model.Orgnizer.Where(x => x.Orgnizer_Id == 0).ToList();
                                foreach (Organizer_Master objOr in orgnew)
                                {

                                    objEOrg = new Organizer_Master();
                                    // objEOrg.Orgnizer_Event_Id = ObjEC.EventID;
                                    objEOrg.Orgnizer_Name = objOr.Orgnizer_Name;
                                    objEOrg.Organizer_Desc = Server.HtmlEncode(objOr.Organizer_Desc);
                                    objEOrg.Organizer_FBLink = objOr.Organizer_FBLink;
                                    objEOrg.Organizer_Twitter = objOr.Organizer_Twitter;
                                    //objEOrg.DefaultOrg = objOr.DefaultOrg;
                                    objEOrg.Organizer_Linkedin = objOr.Organizer_Linkedin;
                                    objEOrg.UserId = strUserId;
                                    objEOrg.Organizer_Status = "A";
                                    objEnt.Organizer_Master.Add(objEOrg);




                                }
                                var defaultorgold = model.Orgnizer.Where(x => x.DefaultOrg == "Y").ToList();
                                foreach (Organizer_Master objOr in defaultorgold)
                                {
                                    evntorg = new Event_Orgnizer_Detail();
                                    evntorg.DefaultOrg = objOr.DefaultOrg;
                                    evntorg.OrganizerMaster_Id = objOr.Orgnizer_Id;
                                    evntorg.Orgnizer_Event_Id = ObjEC.EventID;
                                    evntorg.UserId = strUserId;
                                    objEnt.Event_Orgnizer_Detail.Add(evntorg);
                                }
                                var editoldorg = model.Orgnizer.Where(x => x.EditOrg == "1").ToList();
                                if (editoldorg != null)
                                {
                                    foreach (Organizer_Master objOr in editoldorg)
                                    {
                                        string sql = @"update Organizer_Master set Orgnizer_Name={0},Organizer_Desc={1},Organizer_FBLink={2},Organizer_Twitter={3},Organizer_Linkedin={4} where Orgnizer_Id={5}";
                                        List<Object> sqlParamsList = new List<object>();
                                        sqlParamsList.Add(objOr.Orgnizer_Name);
                                        sqlParamsList.Add(Server.HtmlEncode(objOr.Organizer_Desc));
                                        sqlParamsList.Add(objOr.Organizer_FBLink);
                                        sqlParamsList.Add(objOr.Organizer_Twitter);
                                        sqlParamsList.Add(objOr.Organizer_Linkedin);
                                        sqlParamsList.Add(objOr.Orgnizer_Id);

                                        objEnt.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());
                                    }
                                }



                            }

                        }
                        else
                        {
                            var defaultorg = model.Orgnizer.Where(x => x.DefaultOrg == "Y").ToList();
                            foreach (Organizer_Master objOr in defaultorg)
                            {
                                evntorg = new Event_Orgnizer_Detail();
                                evntorg.DefaultOrg = objOr.DefaultOrg;
                                evntorg.OrganizerMaster_Id = objOr.Orgnizer_Id;
                                evntorg.Orgnizer_Event_Id = ObjEC.EventID;
                                evntorg.UserId = strUserId;
                                objEnt.Event_Orgnizer_Detail.Add(evntorg);
                            }
                            var editoldorg = model.Orgnizer.Where(x => x.EditOrg == "1").ToList();
                            if (editoldorg != null)
                            {
                                foreach (Organizer_Master objOr in editoldorg)
                                {
                                    string sql = @"update Organizer_Master set Orgnizer_Name={0},Organizer_Desc={1},Organizer_FBLink={2},Organizer_Twitter={3},Organizer_Linkedin={4} where Orgnizer_Id={5}";
                                    List<Object> sqlParamsList = new List<object>();
                                    sqlParamsList.Add(objOr.Orgnizer_Name);
                                    sqlParamsList.Add(Server.HtmlEncode(objOr.Organizer_Desc));
                                    sqlParamsList.Add(objOr.Organizer_FBLink);
                                    sqlParamsList.Add(objOr.Organizer_Twitter);
                                    sqlParamsList.Add(objOr.Organizer_Linkedin);
                                    sqlParamsList.Add(objOr.Orgnizer_Id);

                                    objEnt.Database.ExecuteSqlCommand(sql, sqlParamsList.ToArray());
                                }
                            }
                        }
                    }
                    // Tickets

                    string customize = "0";
                    var customizefee = false;
                    var customizeamt = false;

                    if (model.Ticket != null)
                    {
                        Ticket ticket = new Ticket();
                        ids = new List<long>();
                        // objEnt.Ticket_Quantity_Detail.RemoveRange(objEnt.Ticket_Quantity_Detail.Where(x => x.TQD_Event_Id == lEventId));
                        //  objEnt.Tickets.RemoveRange(objEnt.Tickets.Where(x => x.E_Id == lEventId));
                        foreach (Ticket tick in model.Ticket)
                        {
                            if (tick.T_Id == 0)
                            {
                                ticket = new Ticket();

                                ticket.T_Customize = "0";
                                var mainfee = (from db in objEnt.Fee_Structure select db).FirstOrDefault();
                                ticket.EC_Fee = tick.EC_Fee;
                                ticket.T_Ecpercent = mainfee.FS_Percentage;
                                ticket.T_EcAmount = mainfee.FS_Amount;
                                if (tick.TicketTypeID == 2)
                                {
                                    ticket.Customer_Fee = tick.Customer_Fee;
                                }
                                else
                                {
                                    ticket.Customer_Fee = 0;
                                }

                            }
                            else
                            {
                                ticket = (from obj in objEnt.Tickets where obj.T_Id == tick.T_Id && obj.E_Id == lEventId select obj).FirstOrDefault();
                                customize = ticket.T_Customize;
                                if (tick.TicketTypeID == 2)
                                {
                                    ticket.Customer_Fee = tick.Customer_Fee;
                                }

                            }
                            if (tick.Isadmin == "Y")
                            {
                                if (tick.TicketTypeID != 1)
                                {
                                    var mainfee = (from db in objEnt.Fee_Structure select db).FirstOrDefault();
                                    var oldamnt = 0m;

                                    if (tick.Fees_Type == "0")
                                    {
                                        oldamnt = ((((tick.Price * mainfee.FS_Percentage) / 100) + mainfee.FS_Amount) + tick.Price) ?? 0;
                                    }
                                    else
                                    {
                                        oldamnt = tick.Price ?? 0;

                                    }
                                    var oldamount = Decimal.Round(oldamnt, 2);



                                    if (mainfee.FS_Percentage == tick.T_Ecpercent && mainfee.FS_Amount == tick.T_EcAmount)
                                    {
                                        customizefee = false;

                                    }
                                    else
                                    {
                                        customizefee = true;

                                    }
                                    if (oldamount == tick.TotalPrice)
                                    {
                                        customizeamt = false;
                                    }
                                    else
                                    {
                                        customizeamt = true;
                                    }

                                    if (customizefee == true || customizeamt == true)
                                    {
                                        tick.T_Customize = "1";
                                    }
                                    else
                                    {
                                        tick.T_Customize = "0";
                                    }
                                    if (tick.TicketTypeID == 3)
                                    {
                                        if (tick.Customer_Fee > 0)
                                        {
                                            tick.T_Customize = "1";

                                        }
                                    }


                                    ticket.T_Customize = tick.T_Customize;

                                    ticket.T_EcAmount = tick.T_EcAmount;
                                    ticket.T_Ecpercent = tick.T_Ecpercent;
                                }

                            }
                            else
                            {

                            }

                            ticket.E_Id = lEventId;
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
                            ticket.T_Displayremaining = tick.T_Displayremaining;
                            if (tick.Isadmin == "Y")
                            {
                                ticket.EC_Fee = tick.EC_Fee;
                                ticket.Customer_Fee = tick.Customer_Fee;
                            }

                            ticket.TotalPrice = tick.TotalPrice;
                            ticket.T_Discount = tick.T_Discount;
                            if (tick.T_Id == 0)
                            {
                                objEnt.Tickets.Add(ticket);
                            }
                            ids.Add(ticket.T_Id);
                        }


                        //var ids = new HashSet<long>(objEnt.Tickets.Where(x => x.E_Id== lEventId).Select(x=>x.T_Id));

                        var resultticks = objEnt.Tickets.Where(x => !ids.Contains(x.T_Id) && x.E_Id == lEventId).ToList();
                        var resulttickqty = objEnt.Ticket_Quantity_Detail.Where(x => !ids.Contains(x.TQD_Ticket_Id ?? 0) && x.TQD_Event_Id == lEventId).ToList();
                        if(resulttickqty!=null)
                        {
                            objEnt.Ticket_Quantity_Detail.RemoveRange(resulttickqty);
                        }
                        if (resultticks != null)
                        {
                            objEnt.Tickets.RemoveRange(resultticks);
                        }


                    }

                    if (model.EventImage != null)
                    {
                        EventImage Image = new EventImage();
                        objEnt.EventImages.RemoveRange(objEnt.EventImages.Where(x => x.EventID == lEventId));
                        foreach (EventImage img in model.EventImage)
                        {
                            Image = new EventImage();
                            Image.EventID = lEventId;
                            Image.EventImageUrl = img.EventImageUrl;
                            Image.ImageType = img.ImageType;
                            objEnt.EventImages.Add(Image);
                        }
                    }
                    else
                    {
                        objEnt.EventImages.RemoveRange(objEnt.EventImages.Where(x => x.EventID == lEventId));
                    }

                    if (model.EventVariable != null)
                    {
                        Event_VariableDesc var = new Event_VariableDesc();
                        ids = new List<long>();
                        foreach (Event_VariableDesc variable in model.EventVariable)
                        {
                            if (variable.Variable_Id == 0)
                            {
                                var = new Event_VariableDesc();
                            }
                            else
                            {
                                var = (from obj in objEnt.Event_VariableDesc where obj.Variable_Id == variable.Variable_Id && obj.Event_Id == lEventId select obj).FirstOrDefault();

                            }
                            var.Event_Id = lEventId;
                            var.VariableDesc = variable.VariableDesc;
                            var.Price = variable.Price;
                            if (variable.Variable_Id == 0)
                            {
                                objEnt.Event_VariableDesc.Add(var);

                            }
                            ids.Add(variable.Variable_Id);
                        }
                        var resultdesc = objEnt.Event_VariableDesc.Where(x => !ids.Contains(x.Variable_Id) && x.Event_Id == lEventId).ToList();
                        if (resultdesc != null)
                        {
                            objEnt.Event_VariableDesc.RemoveRange(resultdesc);
                        }

                    }
                    else
                    {
                        objEnt.Event_VariableDesc.RemoveRange(objEnt.Event_VariableDesc.Where(x => x.Event_Id == lEventId));
                    }
                    objEnt.SaveChanges();
                    lEventId = ObjEC.EventID;
                    PublishSingleEvent(lEventId);
                }
            }

            return lEventId;
        }

        public string PublishEvent(long lEventId)
        {
            string strResult = "N";
            try
            {
                string strUserId = (Session["AppId"] != null ? Session["AppId"].ToString() : "");
                if (strUserId != "" && lEventId > 0)
                {
                    using (EventComboEntities objEnt = new EventComboEntities())
                    {
                        objEnt.PublishEvent(lEventId, strUserId);
                    }
                    strResult = "Y";
                }
            }
            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
              strResult = "N";
            }
            return strResult;
        }


        public string PublishSingleEvent(long lEventId)
        {
            string strResult = "N";
            try
            {
                string strUserId = (Session["AppId"] != null ? Session["AppId"].ToString() : "");
                if (strUserId != "" && lEventId > 0)
                {
                    using (EventComboEntities objEnt = new EventComboEntities())
                    {
                        objEnt.PublishSingleEvent(lEventId);
                    }
                    strResult = "Y";
                }
            }
            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
              strResult = "N";
            }
            return strResult;
        }
        public string GetOrgnizerDetail(long lEventId)
        {
            StringBuilder strHTML = new StringBuilder();
            string strtemp;
            StringBuilder strDropDown = new StringBuilder();
            using (EventComboEntities objEnt = new EventComboEntities())
            {
                try {
                    var EventOrgID = (from Org in objEnt.Event_Orgnizer_Detail
                                      where Org.Orgnizer_Event_Id == lEventId
                                      select Org.OrganizerMaster_Id).FirstOrDefault();
                    string strUserId = (Session["AppId"] != null ? Session["AppId"].ToString() : "");
                    var EventOrg = (from Org in objEnt.Organizer_Master
                                    where Org.UserId == strUserId && (Org.Orgnizer_Name ?? string.Empty) != string.Empty && Org.Organizer_Status == "A"
                                    select Org).ToList();
                    int i = 0;
                    foreach (Organizer_Master EOD in EventOrg)
                    {
                        i = i + 1;
                        strHTML.Append("<tr>");
                        strHTML.Append("<td style='display: none' width='92%'>");
                        strHTML.Append(i);
                        strHTML.Append("</td>");

                        strHTML.Append("<td width='92 %'><label id=OrgName_");
                        strHTML.Append(i);
                        strHTML.Append(">");
                        strHTML.Append(EOD.Orgnizer_Name);
                        strHTML.Append("</label></td>");
                        strHTML.Append("<td style='display: none'>");
                        strHTML.Append("<label id=OrgId_");
                        strHTML.Append(i);
                        strHTML.Append(">");
                        strHTML.Append(EOD.Orgnizer_Id);
                        strHTML.Append("</label>");
                        strHTML.Append("<label id=OrgEdit_");
                        strHTML.Append(i);
                        strHTML.Append(">");
                        strHTML.Append("0");
                        strHTML.Append("</label>");
                        strHTML.Append("<label id=OrgDes_");
                        strHTML.Append(i);
                        strHTML.Append(">");
                        strHTML.Append(new HtmlString(EOD.Organizer_Desc));
                        strHTML.Append("</label></td>");

                        strHTML.Append("<td style='display: none'><label id=OrgFB_");
                        strHTML.Append(i);
                        strHTML.Append(">");
                        strHTML.Append(EOD.Organizer_FBLink);
                        strHTML.Append("</label></td>");


                        strHTML.Append("<td style='display: none'><label id=OrgTw_");
                        strHTML.Append(i);
                        strHTML.Append(">");
                        strHTML.Append(EOD.Organizer_Twitter);
                        strHTML.Append("</label></td>");

                        strHTML.Append("<td style='display: none'><label id=OrgLn_");
                        strHTML.Append(i);
                        strHTML.Append(">");
                        strHTML.Append(EOD.Organizer_Linkedin);
                        strHTML.Append("</label></td>");
                        strtemp = "<td align='right'><i onclick='editOrgnizer(" + i + ")'; class='fa fa-pencil'></i> | <i onclick='DeleteOrgnizer(" + i + ");' class='fa fa-trash'></i></td>";
                        strHTML.Append(strtemp);
                        strHTML.Append("</tr>");

                        if (EOD.Orgnizer_Id == EventOrgID)
                            strDropDown.Append("<option selected='selected' value=" + i.ToString() + " id=" + i.ToString() + ">" + EOD.Orgnizer_Name + "</option>");
                        else
                            strDropDown.Append("<option value=" + i.ToString() + " id=" + i.ToString() + ">" + EOD.Orgnizer_Name + "</option>");
                    }
                }catch(Exception ex)
                {
                  logger.Error("Exception during request processing", ex);
                }
            }

            return strHTML.ToString() + "¶" + strDropDown.ToString();
            
        }

        #endregion
        public string deleteOrg(int id)
        {
           
            var msg = "";
            var userid = "";
            if (Session["AppId"] != null)
            {
                userid = Session["AppId"].ToString();
                using (EventComboEntities db = new EventComboEntities())
                {
                    Organizer_Master org = (from o in db.Organizer_Master where o.Orgnizer_Id == id && o.UserId == userid select o).FirstOrDefault();
                    org.Organizer_Status = "N";
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
            else
            {
                msg = "O";
            }
            return msg;
        }

        public ActionResult ViewEvent(long EventId, string eventTitle)
        {
            string sDate_new = "", eDate_new = "";
            string startday = "", endday = "", starttime = "", endtime = "";
            Session["Fromname"] = "events";
           
            var url = Url.Action("ViewEvent", "CreateEvent") + "?strUrlData=" + eventTitle.Trim()+ "౼" + EventId+ "౼N";
            Session["ReturnUrl"] = "ViewEvent~" + url;
            var TopAddress = ""; var Topvenue = "";
            string organizername = "", fblink = "", twitterlink = "", organizerid = "", tickettype = "";
            ViewEvent viewEvent = new ViewEvent();
            var EventDetail = GetEventdetail(EventId);
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
            var EventDescription = EventDetail.EventDescription;
            var showtimezone = EventDetail.DisplayTimeZone;
            viewEvent.showTimezone = showtimezone;
            var timezone = EventDetail.TimeZone;
            viewEvent.Timezone = timezone;
            //Address
            var evAdress = (from ev in db.Addresses where ev.EventId == EventId select ev).FirstOrDefault();
            if (evAdress != null)
            {
                TopAddress = evAdress.ConsolidateAddress;
                Topvenue = evAdress.VenueName;

            }

            //Organiser
            if (OrganiserDetail != null)
            {
                //organizername = OrganiserDetail.Orgnizer_Name;
                //fblink = OrganiserDetail.FBLink;
                //twitterlink = OrganiserDetail.Twitter;
                organizerid = OrganiserDetail.Orgnizer_Id.ToString();

            }
            var favCount = (from ev in db.EventFavourites where ev.eventId == EventId select ev).Count();
            var votecount = (from ev in db.EventVotes where ev.eventId == EventId select ev).Count();
            var eventype = (from ev in db.MultipleEvents where ev.EventID == EventId select ev).Count();
            //GetDateList
            var GetEventDate = db.GetEventDateList(EventId).ToList();
            ViewBag.DateList = GetEventDate;

            if (eventype > 0)
            {
                viewEvent.eventType = "Multiple";
                var evschdetails = (from ev in db.MultipleEvents where ev.EventID == EventId select ev).FirstOrDefault();
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

                starttime = evschdetails.StartTime;
                endtime = evschdetails.EndTime;





            }
            else
            {
                viewEvent.eventType = "single";
                var evschdetails = (from ev in db.EventVenues where ev.EventID == EventId select ev).FirstOrDefault();
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
            viewEvent.EventDescription = EventDescription;
            viewEvent.organizername = organizername;
            viewEvent.organizerid = organizerid;
            viewEvent.fblink = fblink;
            viewEvent.twitterlink = twitterlink;
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
            ViewBag.Images = GetImages(EventId);

            var ticketsfree = (from r in db.Tickets where r.E_Id == EventId && r.TicketTypeID == 1 select r).Count();
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
            if (ticketsfree > 0 && ticketsPaid <= 0 && ticketsDonation <= 0)
            {
                tickettype = "Register";

            }
            if (ticketsfree <= 0 && ticketsPaid <= 0 && ticketsDonation > 0)
            {
                tickettype = "Donate";

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

        public Event GetEventdetail(long eventId)
        {
            return ((from ev in db.Events where ev.EventID == eventId select ev).FirstOrDefault());
        }

        public List<string> GetImages(long EventId)
        {
            using (EventComboEntities db = new EventComboEntities())
            {

                return (from myRow in db.EventImages
                        where myRow.EventID == EventId
                        select "/Images/events/event_flyers/imagepath/" + myRow.EventImageUrl).ToList();


            }


        }
        public string savevote(string Eventid)
        {
            if (Session["AppId"] != null)
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    EventVote ObjEC = new EventVote();
                    ObjEC.eventId = long.Parse(Eventid);
                    ObjEC.UserID = Session["AppId"].ToString();
                    objEnt.EventVotes.Add(ObjEC);
                    objEnt.SaveChanges();
                }
                var voteCount = (from ev in db.EventVotes where ev.eventId.ToString() == Eventid select ev).Count();

                return voteCount.ToString();

            }
            else
            {
                Session["ReturnUrl"] = Url.Action("ViewEvent", "CreateEvent", new { EventId = Eventid });
                return "Y";

            }
        }

        public string savefavourite(string Eventid, string type)
        {

            if (Session["AppId"] != null)
            {
                if (type == "Y")
                {
                    using (EventComboEntities objEnt = new EventComboEntities())
                    {
                        EventFavourite ObjEC = new EventFavourite();
                        ObjEC.eventId = long.Parse(Eventid);
                        ObjEC.UserID = Session["AppId"].ToString();
                        objEnt.EventFavourites.Add(ObjEC);
                        objEnt.SaveChanges();
                    }
                    var favCount = (from ev in db.EventFavourites where ev.eventId.ToString() == Eventid select ev).Count();

                    return favCount.ToString();

                }
                else
                {
                    using (EventComboEntities objEnt = new EventComboEntities())
                    {
                        var userid = Session["AppId"].ToString().Trim();
                        objEnt.Database.ExecuteSqlCommand("Delete from EventFavourite where UserID='" + userid + "' AND eventId=" + Eventid + "");
                        objEnt.SaveChanges();
                    }
                    var favCount = (from ev in db.EventFavourites where ev.eventId.ToString() == Eventid select ev).Count();

                    return favCount.ToString();


                }



            }
            else
            {
                Session["ReturnUrl"] = Url.Action("ViewEvent", "CreateEvent", new { EventId = Eventid });
                return "Y";

            }
        }


        public FileResult Calendar(string beginDate, string endDate, string location, string subject, string description)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream) { AutoFlush = true })
                {
                    //HEADER
                    writer.WriteLine("BEGIN:VCALENDAR");
                    writer.WriteLine("PRODID:-//Bored code");
                    writer.WriteLine("VERSION:2.0");
                    writer.WriteLine("METHOD:PUBLISH");
                    writer.WriteLine("BEGIN:VEVENT");

                    //BODY
                    //beginDate.ToString("yyyyMMddTHHmmss")
                    writer.WriteLine("CLASS:PUBLIC");
                    writer.WriteLine("DTSTART:" + beginDate);
                    writer.WriteLine("DTEND:" + endDate);
                    writer.WriteLine("LOCATION:" + location);
                    writer.WriteLine("DESCRIPTION:" + description);
                    writer.WriteLine("SUMMARY:" + subject);
                    writer.WriteLine("UID:mail@boredcode.com");

                    //FOOTER
                    writer.WriteLine("END:VEVENT");
                    writer.WriteLine("END:VCALENDAR");

                    return File(stream.ToArray(), "text/calendar", "ical.ics");
                }
            }
        }

        public string saveorganizermsg(OrganizerMessages model)
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                Event_OrganizerMessages msg = new Event_OrganizerMessages();
                msg.Email = model.email;
                msg.Name = model.name;
                msg.EventId = long.Parse(model.EventId);
                msg.OrganizerId = long.Parse(model.organiserid);
                msg.Message = model.mesasges;
                if (Session["AppId"] != null)
                {
                    msg.Userid = Session["AppId"].ToString();
                }
                else
                {
                    msg.Userid = "";

                }

                db.Event_OrganizerMessages.Add(msg);
                try {
                    int i = db.SaveChanges();
                }catch(Exception ex)
                {
                  logger.Error("Exception during request processing", ex);
                }

                 return "saved";

            }




        }
        public JsonResult Checkticketstatus( string ticketid)
        {
            long str = 0;
            string strQuery = "SELECT isnull(sum(TPD_Purchased_Qty),0) as SaleQty FROM Ticket_Purchased_Detail a inner join  [Ticket_Quantity_Detail] b on a.TPD_TQD_Id=b.TQD_Id    where isnull(TPD_Order_Id,'') !=''  AND b.TQD_Ticket_Id = " + ticketid + "  ";
            var vEvent = db.Database.SqlQuery<long>(strQuery).FirstOrDefault();
            if(vEvent==0)
            {
                str = 0;
            }
            else
            {
                str = vEvent;
            }
            return Json(new { Ticketsale = str });
           


        }
    }
    public class image{
        public string type { get; set; }
        public string name { get; set; }
        public string file { get; set; }
    }
}
