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
using System.Text.RegularExpressions;
using Facebook;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using EventCombo.Utils;
using System.Data.Entity.SqlServer;
using System.Web.UI;
using System.Configuration;
using EventCombo.ViewModels;
using NLog;

namespace EventCombo.Controllers
{
    [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
    public class CreateEventController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        string facebook_urlAuthorize_base = "https://graph.facebook.com/oauth/authorize";
        string facebook_urlGetToken_base = "https://graph.facebook.com/oauth/access_token";
        string facebook_AppID = "963347903739086";
        string facebook_AppSecret = "abe6b97c1d8b47d3545f0b429ad73cb0";
        EventComboEntities db = new EventComboEntities();
        // GET: Event
        [HttpPost]
        public ActionResult CreateEvent(EventCreation Model)
        {
            Session["logo"] = "events";
            Session["Fromname"] = "events";
            return View();
        }

        public string GetOrderText( long EventId)
        {
            string tickettype = "";
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
            if (ticketsfree > 0 && ticketsPaid <= 0 && ticketsDonation > 0)
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
            return tickettype;
        }
        public ActionResult UserOrgnizerStatus()
        {
            return View();
        }

        public ActionResult CreateEvent()
        {

            if ((Session["AppId"] != null))
            {
                try {

                    string User = Session["AppId"].ToString();
                    if (CommanClasses.UserOrganizerStatus(User) == false)
                    {
                        //string str = Url.Action("UserOrgnizerStatus", "UserOrgnizerStatus");
                        //string str = Url.Action("Index", "Home", new { lat = CookieStore.GetCookie("Lat"), lng = CookieStore.GetCookie("Long"), page = "1", strParm="Y" });
                        return RedirectToAction("Index", "Home", new { lat = CookieStore.GetCookie("Lat"), lng = CookieStore.GetCookie("Long"), page = "1", strParm = "Y" });
                    }
                    using (EventComboEntities db = new EventComboEntities())
                    {
                        AspNetUser aspuser = db.AspNetUsers.First(i => i.Id == User);

                        aspuser.LastLoginTime = System.DateTime.UtcNow;
                        db.SaveChanges();


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

                    var rows = (from myRow in db.EventTypes where myRow.EventHide=="N" || string.IsNullOrEmpty(myRow.EventHide)
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
                        EventType.Add(new SelectListItem()
                        {
                            Text = item.EventType1,
                            Value = item.EventTypeID.ToString(),
                        });
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
                        EventCategory.Add(new SelectListItem()
                        {
                            Text = item.EventCategory1,
                            Value = item.EventCategoryID.ToString(),
                        });
                    }
                    var Timezone = (from c in db.TimeZoneDetails  select c).OrderBy(x=>x.Timezone_order);
                    List<SelectListItem> Timezonelist = new List<SelectListItem>();
                    foreach (var item in Timezone)
                    {
                        Timezonelist.Add(new SelectListItem()
                        {
                            Text = item.TimeZone_Name.ToString(),
                            Value = item.TimeZone_Id.ToString(),
                            Selected = (item.TimeZone_Id.ToString().Trim() == "31" ? true : false)

                        });

                    }
                    ViewBag.Timezonelist = Timezonelist;

                    ViewBag.CountryID = countryList;
                    ViewBag.EventType = EventType;
                    ViewBag.ddlEventCategory = EventCategory;

                }
                }
                catch (Exception ex)
                {
                  logger.Error("Exception during request processing", ex);
                }

                //EventCreation ObjEV = new EventCreation();
                //ObjEV.EventTitle = "Form Editing";


                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }


        public string GetPreviousAddress()
        {
            string strUsers = (Session["AppId"] != null ? Session["AppId"].ToString() : "");
            bool bflag = false;
            StringBuilder strHtml = new StringBuilder();
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var PrevAdd = (from myRow in objEnt.Addresses
                                   where myRow.UserId == strUsers
                                   select myRow).ToList();

                    //strHtml.Append("< option value =0 selected=true>Select</ option > ");
                    //strHtml.Append("<option value=" + item.AddressID.ToString() + ">" + item.VenueName + "," + item.Address1 + "," + item.Address2 + "," + item.City + "," + item.Zip + "</option>");
                    strHtml.Append("<option value='0'>Select Past Location</option>");
                    foreach (var item in PrevAdd)
                    {
                        bflag = true;
                        if (item.ConsolidateAddress != null && item.ConsolidateAddress.Trim() != "")
                            strHtml.Append("<option value=" + item.AddressID.ToString() + ">" + item.ConsolidateAddress + "</option>");
                    }
                    if (bflag == false) return ""; else return strHtml.ToString();
                }
            }
            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
              return strHtml.ToString();

            }


        }

        public string GetPreviousAddressForEditing(long lEid)
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

                    var PrevAddID = (from myRow in objEnt.Events
                                     where myRow.EventID == lEid
                                     select myRow.LastLocationAddress).FirstOrDefault();

                    //strHtml.Append("< option value =0 selected=true>Select</ option > ");
                    //strHtml.Append("<option value=" + item.AddressID.ToString() + ">" + item.VenueName + "," + item.Address1 + "," + item.Address2 + "," + item.City + "," + item.Zip + "</option>");
                    strHtml.Append("<option value='0'>Select Past Location</option>");
                    foreach (var item in PrevAdd)
                    {
                        if (item.ConsolidateAddress != null && item.ConsolidateAddress.Trim() != "")
                        {
                            if (PrevAddID == item.AddressID)
                                strHtml.Append("<option selected='selected' value=" + item.AddressID.ToString() + ">" + item.ConsolidateAddress + "</option>");
                            else
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
        public string GetSubCat(long lECatId)
        {

            StringBuilder strHtml = new StringBuilder();
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var EventCat = (from myRow in objEnt.EventSubCategories
                                    where myRow.EventCategoryID == lECatId  
                                    orderby myRow.EventSubCategory1
                                    select myRow).ToList();

                    //strHtml.Append("< option value =0 selected=true>Select</ option > ");
                    strHtml.Append("<option value=0>Select</option>");
                    foreach (var item in EventCat)
                        strHtml.Append("<option value=" + item.EventSubCategoryID.ToString() + ">" + item.EventSubCategory1 + "</option>");

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
            long lEventId = 0; string strEventTitle = "";
            string lat="", lon="";
            ViewEvent vc = new ViewEvent();
            EventCreation obj = new EventCreation();
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
                    Event ObjEC = new Event();
                    strEventTitle = model.EventTitle;
                    ObjEC.EventTypeID = model.EventTypeID;
                    ObjEC.EventCategoryID = model.EventCategoryID;
                    ObjEC.EventSubCategoryID = model.EventSubCategoryID;
                    ObjEC.UserID = strUserId;
                    ObjEC.EventTitle = model.EventTitle;
                    ObjEC.DisplayStartTime = model.DisplayStartTime;
                    ObjEC.DisplayEndTime = model.DisplayEndTime;
                    ObjEC.DisplayTimeZone = model.DisplayTimeZone;
                    ObjEC.EventDescription =Server.HtmlEncode(model.EventDescription);
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
                    ObjEC.Parent_EventID = 0;
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
                    ObjEC.CreateDate = dtzCreated.UniversalTime;
                    ObjEC.EventCancel = "N";
                    //objEnt.Events.Add(ObjEC);
                    objEnt.Events.Add(ObjEC);
                    // Address info
                    string address = "";
                   
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

                                address = objA.ConsolidateAddress;
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
                            objEVenue = new EventVenue();
                            objEVenue.EventID = ObjEC.EventID;

                            //save utc
                           
                          
                            if (Timezonedetail != null)
                            {
                                TimeZoneInfo userTimeZone =TimeZoneInfo.FindSystemTimeZoneById(Timezonedetail.TimeZone);
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(objEv.EventStartDate+" "+objEv.EventStartTime), userTimeZone);
                                dtzend = new DateTimeWithZone(Convert.ToDateTime(objEv.EventEndDate + " " + objEv.EventEndTime), userTimeZone);


                            }
                            else
                            {
                                TimeZoneInfo userTimeZone =TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
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
                        var neworg=model.Orgnizer.Where(x => x.Orgnizer_Id == 0).Any();
                        if (neworg)
                        {

                            var defaultorg = model.Orgnizer.Where(x => x.Orgnizer_Id == 0 && x.DefaultOrg=="Y").Any();
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
                            ticket.T_Displayremaining = tick.T_Displayremaining;
                            ticket.T_Customize = "0";
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
                    obj.PublishEvent(lEventId);

                    string Organisername = "", Organiseremail = "", Organiserphn = ""; ;
                    var eventdetails= (from ev in db.Events  where ev.EventID == lEventId  select ev).FirstOrDefault();
                    //if (eventdetails != null && eventdetails.EventStatus == "Live")
                    //{
                        var OrganiserDetail = (from ev in db.Event_Orgnizer_Detail join pfd in db.Organizer_Master on ev.OrganizerMaster_Id equals pfd.Orgnizer_Id where ev.Orgnizer_Event_Id == lEventId && ev.DefaultOrg == "Y" select pfd).FirstOrDefault();
                    var Organiserdetail = db.Profiles.FirstOrDefault(i => i.UserID == OrganiserDetail.UserId);
                    var userdetail = db.Profiles.FirstOrDefault(i => i.UserID == strUserId);
                    if (Organiserdetail != null)
                    {
                        Organisername = !String.IsNullOrEmpty(OrganiserDetail.Orgnizer_Name) ? OrganiserDetail.Orgnizer_Name : Organiserdetail.FirstName != null ? Organiserdetail.FirstName : "";
                        Organiseremail = !String.IsNullOrEmpty(OrganiserDetail.Organizer_Email) ? OrganiserDetail.Organizer_Email : Organiserdetail.Email != null ? Organiserdetail.Email : "";
                        Organiserphn = !string.IsNullOrEmpty(OrganiserDetail.Organizer_Phoneno) ? " or call "+ OrganiserDetail.Organizer_Phoneno : Organiserdetail.MainPhone != null ? " or call " + Organiserdetail.MainPhone : "";
                    }
                   
                    List<Email_Tag> EmailTag = new List<Email_Tag>();
                    MyAccount ac = new MyAccount();
                        EmailTag = ac.getTag();
                        var Emailtemplate  = ac.getEmail("new_event_notification_email");
                    string to = "", from="", cc="", bcc="", emailname="", subjectn="", bodyn="";
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
                                from = ConfigurationManager.AppSettings.Get("DefaultEmail"); 

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
                                            string strUrl = baseurl + Url.Action("ViewEvent", "EventManagement", new { strEventDs = System.Text.RegularExpressions.Regex.Replace(strEventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = ValidationMessageController.GetParentEventId(lEventId).ToString() });
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



                    //}


                    //if (strIsLive == "Y")
                    //    UpdateEventStatus(lEventId.ToString());
                }
                //if (model.DuplicateEvent == "Y")
                //{
                //    model.DuplicateEvent = "N";
                //    SaveEvent(model);
                //}
            }
            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
              return lEventId;
            }
            return lEventId;
        }






        





        //[Route("{strEventDs}/{strEventId}", Name ="ViewEvent",Order=1),HttpGet]
        //public ActionResult ViewEvent(string strUrlData)
        //[Route("Event/{strEventDs}/{strEventId}", Name = "ViewEvent", Order = 1), HttpGet]
        //public ActionResult ViewEvent(string strEventDs,string strEventId)
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
        //    if(strEventId == null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    string startday = "", endday = "", starttime = "", endtime = "";
        //    DateTimeWithZone dtzstart, dzend, dtznewstart;
        //    DateTimeWithZone dtz;
        //    DateTimeWithZone dtzCreated;
        //    //if (!strUrlData.Contains('౼'))
        //    //{
        //    //    return RedirectToAction("Index", "Home");

        //    //}
        //    ValidationMessageController vmc = new ValidationMessageController();

        //    //string[] str = strUrlData.Split('౼');
        //    //string strForView = "";
        //    //string eventTitle = str[0].ToString();

        //    //long EventId =  vmc.GetLatestEventId(Convert.ToInt64(str[1]));

        //    //try
        //    //{
        //    //    strForView = str[2].ToString();
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    strForView = "N";
        //    //}
        //    long EventId = (strEventId != "" ? Convert.ToInt64(strEventId) : 0);
        //    EventId = vmc.GetLatestEventId(EventId);
        //    TempData["ForViewOnly"] = "N";

        //    string sDate_new = "", eDate_new="";
        //    DateTime dateTime = new DateTime();
        //    DateTime eDate = new DateTime();
        //    DateTime sDate = new DateTime();
        //    Session["logo"] = "events";
        //    Session["Fromname"] = "events";
        //    CreateEventController objCE = new CreateEventController();
        //    var EventDetail = objCE.GetEventdetail(EventId);
        //    if (EventDetail == null) return null;
        //    var url = Url.RouteUrl("ViewEvent", new { strEventDs = Regex.Replace(EventDetail.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = EventDetail.EventID.ToString() });
        //    Session["ReturnUrl"] = "ViewEvent~" + url;

        //    var TopAddress = "";var Topvenue="";
        //    string organizername = "", fblink = "", twitterlink = "", organizerid = "",tickettype="",enablediscussion="",linkedin="";
        //    ViewEvent viewEvent = new ViewEvent();
        //    //EventDetails
        //    var Evnttype = (from ev in db.EventTypes where ev.EventTypeID == EventDetail.EventTypeID select ev.EventType1).FirstOrDefault();

        //    TempData["EventType"] = Evnttype;
            
        //    var EvntCtgry = (from ev in db.EventCategories where ev.EventCategoryID == EventDetail.EventCategoryID select ev.EventCategory1).FirstOrDefault();
        //    var EvntSubCtgry = (from ev in db.EventSubCategories where ev.EventCategoryID == EventDetail.EventCategoryID && ev.EventSubCategoryID==EventDetail.EventSubCategoryID select ev.EventSubCategory1).FirstOrDefault();
        //    TempData["EventCategory"] = EvntCtgry;
        //    TempData["EventSubCategory"] = EvntSubCtgry;

        //    var OrganiserDetail = (from ev in db.Event_Orgnizer_Detail
        //                           join pfd in db.Organizer_Master on ev.Orgnizer_Id equals pfd.Orgnizer_Id
        //                           where ev.Orgnizer_Event_Id == EventId && ev.DefaultOrg == "Y"
        //                           select new
        //                           {
        //                               Orgnizer_Name = pfd.Orgnizer_Name,
        //                               FBLink = pfd.Organizer_FBLink,
        //                               Twitter = pfd.Organizer_Twitter,
        //                               Linkedin = pfd.Organizer_Linkedin,
        //                               Orgnizer_Id = pfd.Orgnizer_Id

        //                           }).FirstOrDefault();
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
        //         timezone = Timezonedetail.TimeZone_Name;
               
        //        //Timezone value
            
        //    }

          
      


        //    viewEvent.Timezone = timezone;
        //    viewEvent.enablediscussion = enablediscussion;
        //    viewEvent.showmaponevent = EventDetail.ShowMap;

        //    viewEvent.EventPrivacy = EventDetail.Private_Password;
        //    //Address
        //    var Addresstype = EventDetail.AddressStatus;
        //    if (Addresstype == "PastLocation")
        //    {
        //        var evAdress = (from ev in db.Addresses where ev.AddressID == EventDetail.LastLocationAddress select ev).FirstOrDefault();
        //        if (evAdress != null)
        //        {
        //            TopAddress = evAdress.ConsolidateAddress;
        //            Topvenue = evAdress.VenueName;
        //        }
        //    }
        //    else
        //    {
        //        var evAdress = (from ev in db.Addresses where ev.EventId == EventId select ev).FirstOrDefault();
        //        if (evAdress != null)
        //        {
        //            TopAddress = evAdress.ConsolidateAddress;
        //            Topvenue = evAdress.VenueName;

        //        }
        //    }

        //    //Organiser
        //    if (OrganiserDetail != null)
        //    {
        //        //organizername = OrganiserDetail.Orgnizer_Name;
        //        //fblink = OrganiserDetail.FBLink;
        //        //twitterlink = OrganiserDetail.Twitter;
        //        organizerid = OrganiserDetail.Orgnizer_Id.ToString();
        //        //linkedin = OrganiserDetail.Linkedin;

        //    }
        //    var favCount = (from ev in db.EventFavourites where ev.eventId == EventId select ev).Count();
        //    var votecount = (from ev in db.EventVotes where ev.eventId == EventId select ev).Count();
        //    var eventype= (from ev in db.MultipleEvents where ev.EventID == EventId select ev).Count();
        //    //GetDateList
        //    var GetEventDate = db.GetEventDateList(EventId).ToList();
        //    List<listevent> lstevent = new List<listevent>();
        //    foreach (var item in GetEventDate)
        //    {
        //        listevent lst = new listevent();
        //        if (Timezonedetail != null)
        //        {


        //            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Timezonedetail.TimeZone);
        //            dtznewstart = new DateTimeWithZone(Convert.ToDateTime(item.Datefrom), userTimeZone, true);

        //            //Timezone value

        //        }
        //        else
        //        {
        //            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        //            dtznewstart = new DateTimeWithZone(Convert.ToDateTime(item.Datefrom), userTimeZone, true);
        //        }
        //        var datenew = dtznewstart.LocalTime;
        //        var endnewday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(datenew).ToString();
        //        var eDatnew = datenew.ToString("MMM dd, yyyy");
        //        var startnewtime = datenew.ToString("h:mm tt").ToLower().Trim().Replace(" ", ""); ;
        //        lst.Dayofweek = endnewday;
        //        lst.Datefrom = eDatnew;
        //        lst.Time = startnewtime;
        //        lstevent.Add(lst);
        //    }
        //    ViewBag.DateList = lstevent;

        //    if (eventype > 0)
        //    {
        //        viewEvent.eventType = "Multiple";
        //        var evschdetails = (from ev in db.MultipleEvents where ev.EventID == EventId select ev).FirstOrDefault();

        //        if (Timezonedetail != null)
        //        {
        //            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Timezonedetail.TimeZone);
        //            dtzstart = new DateTimeWithZone(Convert.ToDateTime(evschdetails.M_Startfrom), userTimeZone, true);
        //            dzend = new DateTimeWithZone(Convert.ToDateTime(evschdetails.M_StartTo), userTimeZone, true);
        //        }
        //        else
        //        {
        //            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        //            dtzstart = new DateTimeWithZone(Convert.ToDateTime(evschdetails.M_Startfrom), userTimeZone, true);
        //            dzend = new DateTimeWithZone(Convert.ToDateTime(evschdetails.M_StartTo), userTimeZone, true);
        //        }





        //        sDate = dtzstart.LocalTime;
        //        startday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(sDate).ToString();

        //        sDate_new = sDate.ToString("MMM dd, yyyy");



        //        eDate = dzend.LocalTime;
        //        endday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(eDate).ToString();
        //        eDate_new = eDate.ToString("MMM dd, yyyy");
        //        starttime = sDate.ToString("h:mm tt").ToLower().Trim().Replace(" ", ""); ;
        //        endtime = eDate.ToString("h:mm tt").ToLower().Trim().Replace(" ", ""); ;






        //    }
        //    else
        //    {
        //        viewEvent.eventType = "single";
        //        var evschdetails = (from ev in db.EventVenues where ev.EventID == EventId select ev).FirstOrDefault();
        //        if (evschdetails != null)
        //        {
        //            if (Timezonedetail != null)
        //            {
        //                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Timezonedetail.TimeZone);
        //                dtzstart = new DateTimeWithZone(Convert.ToDateTime(evschdetails.E_Startdate), userTimeZone, true);
        //                dzend = new DateTimeWithZone(Convert.ToDateTime(evschdetails.E_Enddate), userTimeZone, true);
        //            }
        //            else
        //            {
        //                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        //                dtzstart = new DateTimeWithZone(Convert.ToDateTime(evschdetails.E_Startdate), userTimeZone, true);
        //                dzend = new DateTimeWithZone(Convert.ToDateTime(evschdetails.E_Enddate), userTimeZone, true);
        //            }





        //            sDate = dtzstart.LocalTime;
        //            startday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(sDate).ToString();
        //            sDate_new = sDate.ToString("MMM dd,yyyy");



        //            eDate = dzend.LocalTime;
        //            endday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(eDate).ToString();
        //            eDate_new = eDate.ToString("MMM dd,yyyy");


        //            starttime = sDate.ToString("h:mm tt").ToLower().Trim().Replace(" ", ""); ;
        //            endtime = eDate.ToString("h:mm tt").ToLower().Trim().Replace(" ", ""); ;

        //        }



        //    }

        //    if ((displaystarttime == "Y"|| displaystarttime == "y") && (displayendtime == "Y"|| displayendtime == "y"))
        //    {
        //        viewEvent.DisplaydateRange = startday.ToString() + " " + sDate_new+ " " + starttime + "-" + endday.ToString() + " " + eDate_new + " " + endtime;

        //    }

        //    if ((displaystarttime=="N"|| displaystarttime == "n" ) && (displayendtime=="Y" || displayendtime == "Y"))
        //    {
        //        viewEvent.DisplaydateRange = startday.ToString() + " " + sDate_new +  "-" + endday.ToString() + " " + eDate_new + " " + endtime;

        //    }

        //    if ((displaystarttime == "N" || displaystarttime == "n") && (displayendtime == "n" || displayendtime == "N"))
        //    {
        //        viewEvent.DisplaydateRange = startday.ToString() + " " + sDate_new + "-" + endday.ToString() + " " + eDate_new ;

        //    }

        //    if ( (displayendtime == "N" || displayendtime == "n")&& (displaystarttime == "Y" || displaystarttime == "y"))
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
        //    viewEvent.Shareonfb= EventDetail.Private_ShareOnFB;
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
        //       var count = (from r in db.EventFavourites where r.eventId == EventId && r.UserID == userid
        //                    select r).Count();
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
        //    ViewBag.Images= GetImages(EventId);
            
        //    var cnt = GetImages(EventId).Count();
        //    TempData["ImageCount"] = cnt;
        //    var ticketsfree = (from r in db.Tickets where r.E_Id == EventId && r.TicketTypeID == 1 select r).Count();
        //    int imgcount = GetImages(EventId).Count();
        //    var ticketsPaid = (from r in db.Tickets where r.E_Id == EventId && r.TicketTypeID == 2 select r).Count();
        //    var ticketsDonation = (from r in db.Tickets where r.E_Id == EventId && r.TicketTypeID == 3 select r).Count();
            
        //    var itemsremainingInCart = (from o in db.Ticket_Quantity_Detail where o.TQD_Event_Id == EventId select o.TQD_Remaining_Quantity).Sum();


        //    if (ticketsfree>0 && ticketsPaid>0 && ticketsDonation>0)
        //    {
        //        tickettype = "Order Now";

        //    }
        //    if (ticketsfree <= 0 && ticketsPaid > 0 && ticketsDonation <= 0)
        //    {
        //        tickettype = "Order Now";

        //    }
        //    if (ticketsfree <= 0 && ticketsPaid > 0 && ticketsDonation > 0)
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
        //    if(itemsremainingInCart == 0)
        //    {
        //        tickettype = "Sold Out";

        //    }
        //    viewEvent.Orderdetail = tickettype;
        //    return View(viewEvent);
        //}


        public string GetOrgnizerDetailbyUser()
        {
            StringBuilder strHTML = new StringBuilder();
            string strtemp;
            long MaxEventId = 0;
            string strUserId = Session["AppId"] != null ? Session["AppId"].ToString() : "";
            StringBuilder strDropDown = new StringBuilder();
            if (strUserId != "")
            {
                try {
                    using (EventComboEntities objEnt = new EventComboEntities())
                    {
                        var OrgEventId = (from Org in objEnt.Event_Orgnizer_Detail
                                          where Org.UserId == strUserId
                                          select Org.Orgnizer_Id).Any();
                        if (OrgEventId)
                        {
                            MaxEventId = db.Event_Orgnizer_Detail.Where(x => x.Orgnizer_Event_Id == (db.Event_Orgnizer_Detail.Where(y => y.UserId == strUserId).Max(d => d.Orgnizer_Event_Id) ?? 0)).Select(x => x.OrganizerMaster_Id).FirstOrDefault();
                        }


                        var EventOrg = (from Org in objEnt.Organizer_Master
                                        where Org.UserId == strUserId && (Org.Orgnizer_Name ?? string.Empty) != string.Empty && Org.Organizer_Status == "A"
                                        select Org).OrderBy(x => x.Orgnizer_Name).ToList();

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

                            if (MaxEventId != 0)
                            {


                                if (EOD.Orgnizer_Id == MaxEventId)
                                    strDropDown.Append("<option selected='selected' value=" + i.ToString() + " id=" + i.ToString() + ">" + EOD.Orgnizer_Name + "</option>");
                                else
                                    strDropDown.Append("<option value=" + i.ToString() + " id=" + i.ToString() + ">" + EOD.Orgnizer_Name + "</option>");

                            }
                            else
                            {
                                strDropDown.Append("<option value=" + i.ToString() + " id=" + i.ToString() + ">" + EOD.Orgnizer_Name + "</option>");
                            }

                        }
                    }
                }catch(Exception ex)
                {
                  logger.Error("Exception during request processing", ex);
                }
            }

            return strHTML.ToString() + "¶" + strDropDown.ToString();

        }

        public ActionResult ViewCreateEvent(string strUrlData)
        {
            
            ValidationMessage vmc = new ValidationMessage();
            ViewEvent viewEvent = new ViewEvent();
            DateTimeWithZone dtzstart, dzend, dtznewstart;
            DateTimeWithZone dtz;
            DateTimeWithZone dtzCreated;
            string[] str = strUrlData.Split('౼');
            string strForView = "";
            string eventTitle = str[0].ToString();
            
            long EventId = (str[1] != "" ? Convert.ToInt64(Convert.ToInt64(str[1])) : 0);
            EventId = vmc.GetLatestEventId(EventId);
          
            try
            {
                strForView = str[2].ToString();
            }
            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
              strForView = "N";
            }
            //Session["Fromname"] = "events";
            TempData["ForViewOnly"] = strForView;
            try {
                EventId = vmc.GetLatestEventId(EventId);
                string sDate_new = "", eDate_new = "";
                string startday = "", endday = "", starttime = "", endtime = "";
                Session["logo"] = "events";
                Session["Fromname"] = "events";
                var url = Url.Action("ViewEvent", "CreateEvent") + "?strUrlData=" + eventTitle.Trim() + "౼" + EventId + "౼N";
                // var url = Url.Action("ViewEvent", "CreateEvent")+ "?EventId="+ EventId+ "&eventTitle="+ eventTitle.Trim();
                Session["ReturnUrl"] = "ViewEvent~" + url;
                var TopAddress = ""; var Topvenue = "";
                string organizername = "", fblink = "", twitterlink = "", organizerid = "", tickettype = "", enablediscussion = "",  linkedin = "", orgevents=""; 
               
                //EventDetails
                var EventDetail = GetEventdetail(EventId);
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
               // var EventDescription = EventDetail.EventDescription;
                var showtimezone = EventDetail.DisplayTimeZone;
                enablediscussion = EventDetail.EnableFBDiscussion;
                viewEvent.showTimezone = showtimezone;
                var timezone = "";
                var timezonedet=DateTimeWithZone.Timezonedetail(EventId);
                viewEvent.Timezone = timezonedet;
                viewEvent.enablediscussion = enablediscussion;
                viewEvent.showmaponevent = EventDetail.ShowMap;
                viewEvent.eventId = EventId.ToString();
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
                    var exceptionList = db.EventVenues.Where(x => SqlFunctions.DateDiff("s", x.E_Startdate, DateTime.UtcNow) > 0).Select(e => e.EventID).ToList();
                    var exceptionList1 = db.MultipleEvents.Where(x => SqlFunctions.DateDiff("s", x.M_StartTo, DateTime.UtcNow) > 0).Select(e => e.EventID);
                    var Organizerevents = db.GetOrganizerEventid(OrganiserDetail.Orgnizer_Id).ToList();


                    orgevents = (from x in Organizerevents where x.OrganizerMaster_Id == OrganiserDetail.Orgnizer_Id && !exceptionList.Contains(x.Orgnizer_Event_Id ?? 0) && !exceptionList1.Contains(x.Orgnizer_Event_Id ?? 0) select x).Count().ToString();


                }
                var favCount = (from ev in db.EventFavourites where ev.eventId == EventId select ev).Count();
                var votecount = (from ev in db.EventVotes where ev.eventId == EventId select ev).Count();
                var eventype = (from ev in db.MultipleEvents where ev.EventID == EventId select ev).Count();
                //GetDateList
                List<listevent> lstevent = new List<listevent>();
                var GetEventDate = db.GetEventDateList(EventId).ToList();
                foreach (var item in GetEventDate)
                {
                    listevent lst = new listevent();
                  
                    TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezonedet);
                    dtznewstart = new DateTimeWithZone(Convert.ToDateTime(item.Datefrom), userTimeZone, true);
                    var datenew = dtznewstart.LocalTime;
                    var endnewday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(datenew).ToString();
                    var eDatnew = datenew.ToString("MMM dd, yyyy");
                    var startnewtime = datenew.ToString("h:mm tt").ToLower().Trim().Replace(" ", ""); ;
                    lst.Dayofweek = endnewday;
                    lst.Datefrom = eDatnew;
                    lst.Time = startnewtime;
                    lstevent.Add(lst);
                }
                ViewBag.DateList = lstevent;

                if (eventype > 0)
                {
                    viewEvent.eventType = "Multiple";
                    var evschdetails = (from ev in db.MultipleEvents where ev.EventID == EventId select ev).FirstOrDefault();

                    TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezonedet);
                    dtzstart = new DateTimeWithZone(Convert.ToDateTime(evschdetails.M_Startfrom), userTimeZone, true);
                    dzend = new DateTimeWithZone(Convert.ToDateTime(evschdetails.M_StartTo), userTimeZone, true);

                   
                    DateTime sDate = new DateTime();
                    sDate = dtzstart.LocalTime;
                    startday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(sDate).ToString();

                    sDate_new = sDate.ToString("MMM dd, yyyy");
                   

                    DateTime eDate = new DateTime();
                    eDate = dzend.LocalTime;
                    endday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(eDate).ToString();
                    eDate_new = eDate.ToString("MMM dd, yyyy");

                    starttime = evschdetails.StartTime.ToUpper();
                    endtime = evschdetails.EndTime.ToUpper();





                }
                else
                {
                    viewEvent.eventType = "single";
                    var evschdetails = (from ev in db.EventVenues where ev.EventID == EventId select ev).FirstOrDefault();
                    if (evschdetails != null)
                    {

                        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezonedet);
                        dtzstart = new DateTimeWithZone(Convert.ToDateTime(evschdetails.E_Startdate), userTimeZone, true);
                        dzend = new DateTimeWithZone(Convert.ToDateTime(evschdetails.E_Enddate), userTimeZone, true);
                      
                       
                            DateTime sDate = new DateTime();
                            sDate = dtzstart.LocalTime;
                            startday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(sDate).ToString();
                            sDate_new = sDate.ToString("MMM dd,yyyy");
                       
                      
                            DateTime eDate = new DateTime();
                            eDate = dzend.LocalTime;
                            endday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(eDate).ToString();
                            eDate_new = eDate.ToString("MMM dd,yyyy");
                     

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
                if (eDate_new == "")
                {
                    TempData["ExpiredEvent"] = vmc.Index("ViewEvent", "ViewEventExpiredSy");
                    TempData["ForViewOnly"] = "Y";
                }
                else
                {
                    var enday = DateTime.Parse(eDate_new);
                    var now = DateTime.Now;
                    if (enday < now)
                    {

                        TempData["ExpiredEvent"] = vmc.Index("ViewEvent", "ViewEventExpiredSy");
                        TempData["ForViewOnly"] = "Y";
                    }
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
                viewEvent.EventPrivacy = EventDetail.EventPrivacy;
                viewEvent.PrivatePassword = EventDetail.Private_Password;
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
                var cnt = GetImages(EventId).Count();
                TempData["ImageCount"] = cnt;
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
                if (ticketsfree > 0 && ticketsPaid > 0 && ticketsDonation <= 0)
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
            }catch(Exception ex)
            {
              logger.Error("Exception during request processing", ex);
            }
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
                             orderby myRow.EventImageID
                             select "/Images/events/event_flyers/imagepath/" + myRow.EventImageUrl ).ToList();



            }
               

        }
       [HttpPost]
        public string savevote(string Eventid)
        {
            var EventDetail = GetEventdetail(long.Parse(Eventid));
            if (Session["AppId"] != null)
            {
              
                Session["ReturnUrl"] = Url.RouteUrl("ViewEvent", new { strEventDs = Regex.Replace(EventDetail.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = EventDetail.EventID.ToString() });
                // Url.Action("ViewEvent", "CreateEvent", new { strUrlData = "a౼" + Eventid + "౼N" });
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
            else {
                Session["ReturnUrl"] = Url.RouteUrl("ViewEvent", new { strEventDs = Regex.Replace(EventDetail.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = EventDetail.EventID.ToString() });
                // Url.Action("ViewEvent", "CreateEvent", new { strUrlData = "a౼" + Eventid + "౼N" });
                return "Y";

            }
        }
      [HttpPost]
        public string savefavourite(string Eventid,string type)
        {
            var EventDetail = GetEventdetail(long.Parse(Eventid));
            if (Session["AppId"] != null)
            {
                string url = Url.RouteUrl("ViewEvent", new { strEventDs = Regex.Replace(EventDetail.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = EventDetail.EventID.ToString() });
                //Url.Action("ViewEvent", "CreateEvent", new { strUrlData = "a౼" + Eventid + "౼N" });
                Session["ReturnUrl"] = Url.RouteUrl("ViewEvent", new { strEventDs = Regex.Replace(EventDetail.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = EventDetail.EventID.ToString() });
                //Url.Action("ViewEvent", "CreateEvent", new { strUrlData = "a౼" + Eventid + "౼N" });
                if (type=="Y")
                {
                    using (EventComboEntities objEnt = new EventComboEntities())
                    {
                        EventFavourite ObjEC = new EventFavourite();
                        ObjEC.eventId =long.Parse(Eventid);
                        ObjEC.UserID = Session["AppId"].ToString();
                        objEnt.EventFavourites.Add(ObjEC);
                        objEnt.SaveChanges();
                    }
                    var favCount = (from ev in db.EventFavourites where ev.eventId.ToString () == Eventid select ev).Count();

                    return favCount.ToString ();

                }
                else
                {
                    using (EventComboEntities objEnt = new EventComboEntities())
                    {
                        var userid = Session["AppId"].ToString().Trim();
                        objEnt.Database.ExecuteSqlCommand("Delete from EventFavourite where UserID='" + userid + "' AND eventId="+ Eventid + "");
                        objEnt.SaveChanges();
                    }
                    var favCount = (from ev in db.EventFavourites where ev.eventId.ToString() == Eventid select ev).Count();

                    return favCount.ToString();


                }

                

            }
            else
            {
                string url = Url.RouteUrl("ViewEvent", new { strEventDs = Regex.Replace(EventDetail.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = EventDetail.EventID.ToString() });
                //Url.Action("ViewEvent", "CreateEvent", new { strUrlData = "a౼" + Eventid + "౼N" });
                Session["ReturnUrl"] = Url.RouteUrl("ViewEvent", new { strEventDs = Regex.Replace(EventDetail.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = EventDetail.EventID.ToString() });
                //Url.Action("ViewEvent", "CreateEvent", new { strUrlData ="a౼"+ Eventid+"౼N"  });
                return "Y";

            }
        }


        [HttpPost]
        public string Discoversavefavourite(string Eventid, string type,string strUrl)
        {
            var EventDetail = GetEventdetail(long.Parse(Eventid));
            if (Session["AppId"] != null)
            {
                var url = Request.Url;
                var baseurl = url.GetLeftPart(UriPartial.Authority);
                strUrl = strUrl.Replace(baseurl, "");
              //  Session["ReturnUrl"] = "DiscoverEvent~" + strUrl;
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    long? lEventid = (Eventid != "" ? Convert.ToInt64(Eventid) : 0);
                    string strUserId = Session["AppId"].ToString();
                    var vfav = (from ev in db.EventFavourites where ev.eventId == lEventid && ev.UserID == strUserId select ev.UserID).FirstOrDefault();
                    if (vfav != null && vfav.Trim() != "")
                    {
                        var userid = Session["AppId"].ToString().Trim();
                        objEnt.Database.ExecuteSqlCommand("Delete from EventFavourite where UserID='" + userid + "' AND eventId=" + Eventid + "");
                        objEnt.SaveChanges();
                        return "D";
                    }
                    else
                    {
                        EventFavourite ObjEC = new EventFavourite();
                        ObjEC.eventId = long.Parse(Eventid);
                        ObjEC.UserID = Session["AppId"].ToString();
                        objEnt.EventFavourites.Add(ObjEC);
                        objEnt.SaveChanges();
                        return "I";
                    }
                }
                
            }
            else
            {
                //string url = strUrl;
                var url = Request.Url;
                var baseurl = url.GetLeftPart(UriPartial.Authority);
                strUrl = strUrl.Replace(baseurl, "");

                Session["ReturnUrl"] = Eventid.ToString() + "~" + strUrl;
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
                msg.PhoneNo = model.PhoneNo;
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
                    Email.SendToOrganizer(msg.MessageId);
                }catch(Exception ex)
                {
                  logger.Error("Exception during request processing", ex);
                }
                return "saved";
            }
        }

     
        public string UpdateEventStatus(string strEventId)
        {
            string strResult = "N";
            try
            {
                string strUserId = (Session["AppId"] != null ? Session["AppId"].ToString() : "");
                if (strUserId != "" && strEventId != "")
                {
                    using (EventComboEntities objEnt = new EventComboEntities())
                    {
                        long lEvent = Convert.ToInt64(strEventId);
                        Event objEvt = objEnt.Events.First(i => i.EventID == lEvent);
                        objEvt.EventStatus = "Live";
                        try {
                            objEnt.SaveChanges();
                        }catch (Exception ex)
                        {
                          logger.Error("Exception during request processing", ex);
                        }
                    }
                    strResult = "Y";
                }
            }
            catch (Exception ex)
            {
                strResult = "N";
            }
            return strResult;
        }
        #region DisplayTickets
        public string GetTicketDetail(string Eventid)
        {
            string strTicket = "";
                
           // if (Session["AppId"] != null)
          //  {
                if (Eventid.Trim() != "")
                {
                    using (EventComboEntities objEnt = new EventComboEntities())
                    {
                        strTicket = objEnt.GetTicketListing(Convert.ToInt64(Eventid)).Single();
                    }
                }
          //  }
            return strTicket;
        }

        public string LockTickets(Ticket_Locked_Detail objLocked)
        {
            

            string strResult = "Y";
            string strGuid = Guid.NewGuid().ToString();
            Session["TicketLockedId"] = strGuid;
            string strUsers = (Session["AppId"] != null ? Session["AppId"].ToString() : "");
            using (var context = new EventComboEntities())
            {
                Ticket_Locked_Detail objTLD = new Ticket_Locked_Detail();
                foreach (Ticket_Locked_Detail objModel in objLocked.TLD_List)
                {
                    var vEvtState = (from myevent in context.Events where myevent.EventID == objModel.TLD_Event_Id select myevent.EventStatus).FirstOrDefault();
                    if (vEvtState.Trim() != "Live") return "NOTLIVE";

                    var vRemQty = (from PQty in context.Ticket_Quantity_Detail where PQty.TQD_Id == objModel.TLD_TQD_Id select PQty.TQD_Remaining_Quantity).SingleOrDefault();
                    var vLockQty = (from PQty in context.Ticket_Locked_Detail where PQty.TLD_TQD_Id == objModel.TLD_TQD_Id select PQty.TLD_Locked_Qty).Sum();
                    vLockQty = (vLockQty != null ? vLockQty : 0);
                    if (vRemQty < (vLockQty + objModel.TLD_Locked_Qty))
                    {
                        strResult = "N";
                        break;
                    }

                    objTLD = new Ticket_Locked_Detail();
                    objTLD.TLD_Locked_Qty = objModel.TLD_Locked_Qty;
                    objTLD.TLD_TQD_Id  = objModel.TLD_TQD_Id;
                    objTLD.TLD_Event_Id = objModel.TLD_Event_Id;
                    objTLD.TLD_User_Id = strUsers;
                    objTLD.TLD_GUID = strGuid;
                    objTLD.Locktime = DateTime.UtcNow;
                    objTLD.TLD_Donate = objModel.TLD_Donate;
                    objTLD.TicketAmount = objModel.TicketAmount;
                    context.Ticket_Locked_Detail.Add(objTLD);


                }


                if (strResult == "Y")
                {
                    try {
                        context.SaveChanges();
                    }catch(Exception ex)
                    {
                      logger.Error("Exception during request processing", ex);
                    }
                }
                
            }
            return strResult;
        }


        public string GetSelectedTickets(Ticket_Locked_Detail objLocked)
        {

            string strGuid = (Session["TicketLockedId"] != null ? Session["TicketLockedId"].ToString() : "");
            StringBuilder strIds = new StringBuilder();
            if (strGuid == "")
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var LockTicket = (from LT in objEnt.Ticket_Locked_Detail
                                      where LT.TLD_GUID == strGuid
                                      select LT);
                    foreach (Ticket_Locked_Detail TLD in LockTicket)
                    {
                        strIds.Append(TLD.TLD_TQD_Id.ToString() + ",");
                    }
                }
            }
            return strIds.ToString();
        }

        #endregion

public string Checkpassword(string password ,long id)
        {
            EventComboEntities obje = new EventComboEntities();
            string strresult = "";
            var pwd = (from obj in obje.Events where obj.EventID == id  select obj.Private_Password).FirstOrDefault();
            var i = String.Compare(pwd, password);
            if(i == 0)
            {
                strresult = "Y";
            }
            else
            {
                strresult = "N";
            }

            return strresult;
        }

        public void InsertFriendlist(string accesstoken)
        {

            FacebookClient client = new FacebookClient(accesstoken);
            var friendListData = client.Get("/me/friends");
            JObject friendListJson = JObject.Parse(friendListData.ToString());


            List<FbUser> fbUsers = new List<FbUser>();
            foreach (var friend in friendListJson["data"].Children())
            {
                FbUser fbUser = new FbUser();
                fbUser.Id = friend["id"].ToString().Replace("\"", "");
                fbUser.Name = friend["name"].ToString().Replace("\"", "");
                fbUsers.Add(fbUser);
            }
        }

        public Friends friendlist(string accesstoken)
        {
          Friends frnd=  Facebook_ListFriends(accesstoken);
            return frnd;
        }

        private Friends Facebook_ListFriends(string pAccessToken)
        {
            string username = "me";
            string dataType = "friends";
            string urlGetFriends = "https://graph.facebook.com/" + username + "/" + dataType + "?access_token=" + pAccessToken+ "&fields=name,picture,id";
            string jsonFriends = RequestResponse(urlGetFriends);
            //if (jsonFriends == "")
            //{
            //    Response.Write("<br /><br />urlGetFriends have problems");
            //    return;
            //}
            Friends friends = JsonConvert.DeserializeObject<Friends>(jsonFriends); //we write the Friends class later

            return friends;

        }
        private string RequestResponse(string pUrl)
        {
            HttpWebRequest webRequest = System.Net.WebRequest.Create(pUrl) as HttpWebRequest;
            webRequest.Method = "GET";
            webRequest.ServicePoint.Expect100Continue = false;
            webRequest.Timeout = 20000;

            Stream responseStream = null;
            StreamReader responseReader = null;
            string responseData = "";
            try
            {
                WebResponse webResponse = webRequest.GetResponse();
                responseStream = webResponse.GetResponseStream();
                responseReader = new StreamReader(responseStream);
                responseData = responseReader.ReadToEnd();
            }
            catch (Exception exc)
            {
              logger.Error("Exception during request processing", exc);
              Response.Write("<br /><br />ERROR : " + exc.Message);
            }
            finally
            {
                if (responseStream != null)
                {
                    responseStream.Close();
                    responseReader.Close();
                }
            }

            return responseData;
        }

        public string ReleaseTickets()
        {
            string strResult = "";
            try
            {
                string strLockedId = (Session["TicketLockedId"] != null ? Session["TicketLockedId"].ToString() : "");
                CommanClasses objC = new CommanClasses();
                using (var context = new EventComboEntities())
                {
                    context.Database.ExecuteSqlCommand("DELETE FROM Ticket_Locked_Detail WHERE TLD_GUID ='" + strLockedId + "'");
                    strResult = objC.GetMessage("TicketPayment", "TenMinWindowExpires");
                }
            }
            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
              strResult = "There is some Problem.";
            }
            return strResult;
        }

        public string GetEventURL()
        {
            StringBuilder strHtml = new StringBuilder();
            try
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var EvList = (from myRow in objEnt.v_RetrieveEventid
                                  select myRow).ToList().OrderByDescending(m => m.EventID);

                    strHtml.Append("<option value='0'>Select Any Event</option>");
                    foreach (var item in EvList)
                    {
                        if (item.EventTitle != null && item.EventTitle.Trim() != "")
                          strHtml.Append("<option>" + @Url.Action("ViewEvent", "EventManagement", new { strEventDs = Regex.Replace(item.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = item.EventID.ToString() }) + "</option>");
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














    }

       

    }

public class Friends
{
    public List<Friend> data { get; set; }
}

public class FbUser
    {
       public String Id { set; get; }
        public String Name { set; get; }
    }

public class Friend
{
    public string id { get; set; }
    public string name { get; set; }
}
