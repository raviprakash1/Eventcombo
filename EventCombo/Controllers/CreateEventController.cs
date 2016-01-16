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
using Facebook;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace EventCombo.Controllers
{
   
    public class CreateEventController : Controller
    {
        string facebook_urlAuthorize_base = "https://graph.facebook.com/oauth/authorize";
        string facebook_urlGetToken_base = "https://graph.facebook.com/oauth/access_token";
        string facebook_AppID = "963347903739086";
        string facebook_AppSecret = "abe6b97c1d8b47d3545f0b429ad73cb0";
        EventComboEntities db = new EventComboEntities();
        // GET: Event
        [HttpPost]
        public ActionResult CreateEvent(EventCreation Model)
        {
            Session["Fromname"] = "events";
            return View();
        }

        public ActionResult CreateEvent()
        {

            if ((Session["AppId"] != null))
            {
                HomeController hmc = new HomeController();
                hmc.ControllerContext = new ControllerContext(this.Request.RequestContext, hmc);
                string usernme = hmc.getusername();
                if (string.IsNullOrEmpty(usernme))
                {
                    return RedirectToAction("Index", "Home");
                }
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
                    var Timezone = (from c in db.TimeZoneDetails orderby c.TimeZone_Id ascending select c).Distinct();
                    List<SelectListItem> Timezonelist = new List<SelectListItem>();
                    foreach (var item in Timezone)
                    {
                        Timezonelist.Add(new SelectListItem()
                        {
                            Text = item.TimeZone_Name.ToString(),
                            Value = item.TimeZone_Id.ToString()
                            //Selected = (item.TimeZone_Id.ToString().Trim() == timezone.Trim() ? true : false)

                        });

                    }
                    ViewBag.Timezonelist = Timezonelist;

                    ViewBag.CountryID = countryList;
                    ViewBag.EventType = EventType;
                    ViewBag.ddlEventCategory = EventCategory;

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
                return "Y";

            }
        }

        public long SaveEvent(EventCreation model)
        {
            long lEventId = 0;
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
                    ObjEC.ShowMap = model.ShowMap;
                    ObjEC.Parent_EventID = 0;
                    ObjEC.CreateDate = DateTime.Now;
                    //objEnt.Events.Add(ObjEC);
                    objEnt.Events.Add(ObjEC);
                    // Address info
                    if (model.AddressDetail != null)
                    {
                        Address ObjAdd = new Models.Address();
                        foreach (Address objA in model.AddressDetail)
                        {
                            if ((objA.VenueName != null && objA.VenueName.Trim() != "") || objA.ConsolidateAddress != "")
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
                    // Orgnizer
                    if (model.Orgnizer != null)
                    {
                        Event_Orgnizer_Detail objEOrg = new Event_Orgnizer_Detail();
                        foreach (Event_Orgnizer_Detail objOr in model.Orgnizer)
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
                    PublishEvent(lEventId);
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
                return lEventId;
            }
            return lEventId;
        }

        //[Route("{strEventDs}/{strEventId}", Name ="ViewEvent",Order=1),HttpGet]
        //public ActionResult ViewEvent(string strUrlData)
        //[Route("Event/{strEventDs}/{strEventId}", Name = "ViewEvent", Order = 1), HttpGet]
        public ActionResult ViewEvent(string strEventDs,string strEventId)
        {

            if ((Session["AppId"] != null))
            {
                HomeController hmc = new HomeController();
                hmc.ControllerContext = new ControllerContext(this.Request.RequestContext, hmc);
                string usernme = hmc.getusername();
                if (string.IsNullOrEmpty(usernme))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if(strEventId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //if (!strUrlData.Contains('౼'))
            //{
            //    return RedirectToAction("Index", "Home");

            //}
            ValidationMessageController vmc = new ValidationMessageController();

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

            string sDate_new = "", eDate_new="";
            string startday="", endday="", starttime="", endtime="";
            Session["Fromname"] = "events";
            CreateEventController objCE = new CreateEventController();
            var EventDetail = objCE.GetEventdetail(EventId);
            if (EventDetail == null) return null;
            var url = Url.RouteUrl("ViewEvent", new { strEventDs = Regex.Replace(EventDetail.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = EventDetail.EventID.ToString() });
            Session["ReturnUrl"] = "ViewEvent~" + url;

            var TopAddress = "";var Topvenue="";
            string organizername = "", fblink = "", twitterlink = "", organizerid = "",tickettype="",enablediscussion="",linkedin="";
            ViewEvent viewEvent = new ViewEvent();
            //EventDetails
            TempData["EventType"] = EventDetail.EventType.EventType1;
            
            var EvntCtgry = (from ev in db.EventCategories where ev.EventCategoryID == EventDetail.EventCategoryID select ev.EventCategory1).FirstOrDefault();
            var EvntSubCtgry = (from ev in db.EventSubCategories where ev.EventCategoryID == EventDetail.EventCategoryID && ev.EventSubCategoryID==EventDetail.EventSubCategoryID select ev.EventSubCategory1).FirstOrDefault();
            TempData["EventCategory"] = EvntCtgry;
            TempData["EventSubCategory"] = EvntSubCtgry;

            var OrganiserDetail = (from ev in db.Event_Orgnizer_Detail where ev.Orgnizer_Event_Id == EventId && ev.DefaultOrg == "Y" select ev).FirstOrDefault();
            var displaystarttime = EventDetail.DisplayStartTime;
            var displayendtime = EventDetail.DisplayEndTime;
            var EventDescription = EventDetail.EventDescription;
            var showtimezone = EventDetail.DisplayTimeZone;
            enablediscussion = EventDetail.EnableFBDiscussion;
            viewEvent.showTimezone = showtimezone;
            var timezone = "";
            var Timezonedetail = (from ev in db.TimeZoneDetails where ev.TimeZone_Id.ToString() == EventDetail.TimeZone select ev).FirstOrDefault();
            if (Timezonedetail != null)
            {
                 timezone = Timezonedetail.TimeZone_Name;
               
                //Timezone value
            
            }

          
      


            viewEvent.Timezone = timezone;
            viewEvent.enablediscussion = enablediscussion;
            viewEvent.showmaponevent = EventDetail.ShowMap;

            viewEvent.EventPrivacy = EventDetail.Private_Password;
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

            }
            var favCount = (from ev in db.EventFavourites where ev.eventId == EventId select ev).Count();
            var votecount = (from ev in db.EventVotes where ev.eventId == EventId select ev).Count();
            var eventype= (from ev in db.MultipleEvents where ev.EventID == EventId select ev).Count();
            //GetDateList
            var GetEventDate = db.GetEventDateList(EventId).ToList();
            ViewBag.DateList = GetEventDate;

            if (eventype > 0) {
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
                endday = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(eDate).ToString ();
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

            if ((displaystarttime == "Y"|| displaystarttime == "y") && (displayendtime == "Y"|| displayendtime == "y"))
            {
                viewEvent.DisplaydateRange = startday.ToString() + " " + sDate_new+ " " + starttime + "-" + endday.ToString() + " " + eDate_new + " " + endtime;

            }

            if ((displaystarttime=="N"|| displaystarttime == "n" ) && (displayendtime=="Y" || displayendtime == "Y"))
            {
                viewEvent.DisplaydateRange = startday.ToString() + " " + sDate_new +  "-" + endday.ToString() + " " + eDate_new + " " + endtime;

            }

            if ((displaystarttime == "N" || displaystarttime == "n") && (displayendtime == "n" || displayendtime == "N"))
            {
                viewEvent.DisplaydateRange = startday.ToString() + " " + sDate_new + "-" + endday.ToString() + " " + eDate_new ;

            }

            if ( (displayendtime == "N" || displayendtime == "n")&& (displaystarttime == "Y" || displaystarttime == "y"))
            {
                viewEvent.DisplaydateRange = startday.ToString() + " " + sDate_new + " " + starttime + "-" + endday.ToString() + " " + eDate_new;

            }

            if (!string.IsNullOrEmpty(eDate_new))
            {
                var enday = DateTime.Parse(eDate_new);
                var now = DateTime.Now;
                if (enday < now)
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
            viewEvent.typeofEvent = EventDetail.AddressStatus;
            viewEvent.Shareonfb= EventDetail.Private_ShareOnFB;
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
            viewEvent.Linkedinlin = linkedin;
            if (Session["AppId"] != null)
            {
                var userid = Session["AppId"].ToString();
               var count = (from r in db.EventFavourites where r.eventId == EventId && r.UserID == userid
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
            ViewBag.Images= GetImages(EventId);
            
            var cnt = GetImages(EventId).Count();
            TempData["ImageCount"] = cnt;
            var ticketsfree = (from r in db.Tickets where r.E_Id == EventId && r.TicketTypeID == 1 select r).Count();
            int imgcount = GetImages(EventId).Count();
            var ticketsPaid = (from r in db.Tickets where r.E_Id == EventId && r.TicketTypeID == 2 select r).Count();
            var ticketsDonation = (from r in db.Tickets where r.E_Id == EventId && r.TicketTypeID == 3 select r).Count();
            
            var itemsremainingInCart = (from o in db.Ticket_Quantity_Detail where o.TQD_Event_Id == EventId select o.TQD_Remaining_Quantity).Sum();


            if (ticketsfree>0 && ticketsPaid>0 && ticketsDonation>0)
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
            if(itemsremainingInCart == 0)
            {
                tickettype = "Sold Out";

            }
            viewEvent.Orderdetail = tickettype;
            return View(viewEvent);
        }


        public string GetOrgnizerDetailbyUser()
        {
            StringBuilder strHTML = new StringBuilder();
            string strtemp;
            string strUserId = Session["AppId"] != null ? Session["AppId"].ToString() : "";
            StringBuilder strDropDown = new StringBuilder();
            if (strUserId != "")
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    var MaxEventId = (from Org in objEnt.Event_Orgnizer_Detail
                                      where Org.UserId == strUserId
                                      select Org.Orgnizer_Event_Id).Max();

                    var EventOrg = (from Org in objEnt.Event_Orgnizer_Detail
                                    where Org.Orgnizer_Event_Id == MaxEventId
                                    select Org).ToList();

                    int i = 0;
                    foreach (Event_Orgnizer_Detail EOD in EventOrg)
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

                        strHTML.Append("<td style='display: none'><label id=OrgDes_");
                        strHTML.Append(i);
                        strHTML.Append(">");
                        strHTML.Append(EOD.Orgnizer_Desc);
                        strHTML.Append("</label></td>");

                        strHTML.Append("<td style='display: none'><label id=OrgFB_");
                        strHTML.Append(i);
                        strHTML.Append(">");
                        strHTML.Append(EOD.FBLink);
                        strHTML.Append("</label></td>");


                        strHTML.Append("<td style='display: none'><label id=OrgTw_");
                        strHTML.Append(i);
                        strHTML.Append(">");
                        strHTML.Append(EOD.Twitter);
                        strHTML.Append("</label></td>");
                        strHTML.Append("<td style='display: none'><label id=OrgLn_");
                        strHTML.Append(i);
                        strHTML.Append(">");
                        strHTML.Append(EOD.Linkedin);
                        strHTML.Append("</label></td>");
                        strtemp = "<td align='right'><i onclick='editOrgnizer(" + i + ")'; class='fa fa-pencil'></i> | <i onclick='DeleteOrgnizer(" + i + ");' class='fa fa-trash'></i></td>";
                        strHTML.Append(strtemp);
                        strHTML.Append("</tr>");

                        if (EOD.DefaultOrg == "Y")
                            strDropDown.Append("<option selected='selected' value=" + i.ToString() + " id=" + i.ToString() + ">" + EOD.Orgnizer_Name + "</option>");
                        else
                            strDropDown.Append("<option value=" + i.ToString() + " id=" + i.ToString() + ">" + EOD.Orgnizer_Name + "</option>");


                    }
                }
            }

            return strHTML.ToString() + "¶" + strDropDown.ToString();

        }

        public ActionResult ViewCreateEvent(string strUrlData)
        {
            
            ValidationMessageController vmc = new ValidationMessageController();
            string[] str = strUrlData.Split('౼');
            string strForView = "";
            string eventTitle = str[0].ToString();
            long EventId = Convert.ToInt64(str[1]);
            try
            {
                strForView = str[2].ToString();
            }
            catch (Exception)
            {
                strForView = "N";
            }
            //Session["Fromname"] = "events";
            TempData["ForViewOnly"] = strForView;
            EventId = vmc.GetLatestEventId(EventId);
            string sDate_new = "", eDate_new = "";
            string startday = "", endday = "", starttime = "", endtime = "";
            Session["Fromname"] = "events";
            var url = Url.Action("ViewEvent", "CreateEvent") + "?strUrlData=" + eventTitle.Trim() + "౼" + EventId + "౼N";
            // var url = Url.Action("ViewEvent", "CreateEvent")+ "?EventId="+ EventId+ "&eventTitle="+ eventTitle.Trim();
            Session["ReturnUrl"] = "ViewEvent~" + url;
            var TopAddress = ""; var Topvenue = "";
            string organizername = "", fblink = "", twitterlink = "", organizerid = "", tickettype = "", enablediscussion = "",Linkedin="";
            ViewEvent viewEvent = new ViewEvent();
            //EventDetails
            var EventDetail = GetEventdetail(EventId);
            TempData["EventType"] = EventDetail.EventType.EventType1;

            var EvntCtgry = (from ev in db.EventCategories where ev.EventCategoryID == EventDetail.EventCategoryID select ev.EventCategory1).FirstOrDefault();
            var EvntSubCtgry = (from ev in db.EventSubCategories where ev.EventCategoryID == EventDetail.EventCategoryID && ev.EventSubCategoryID == EventDetail.EventSubCategoryID select ev.EventSubCategory1).FirstOrDefault();
            TempData["EventCategory"] = EvntCtgry;
            TempData["EventSubCategory"] = EvntSubCtgry;

            var OrganiserDetail = (from ev in db.Event_Orgnizer_Detail where ev.Orgnizer_Event_Id == EventId && ev.DefaultOrg == "Y" select ev).FirstOrDefault();
            var displaystarttime = EventDetail.DisplayStartTime;
            var displayendtime = EventDetail.DisplayEndTime;
            var EventDescription = EventDetail.EventDescription;
            var showtimezone = EventDetail.DisplayTimeZone;
            enablediscussion = EventDetail.EnableFBDiscussion;
            viewEvent.showTimezone = showtimezone;
            var timezone = "";
            var Timezonedetail = (from ev in db.TimeZoneDetails where ev.TimeZone_Id.ToString() == EventDetail.TimeZone select ev).FirstOrDefault();
            if (Timezonedetail != null)
            {
                timezone = Timezonedetail.TimeZone_Name;

            }
            viewEvent.Timezone = timezone;
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
                Linkedin = OrganiserDetail.Linkedin;

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

                starttime = evschdetails.StartTime.ToUpper();
                endtime = evschdetails.EndTime.ToUpper();





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
            viewEvent.EventDescription = EventDescription;
            viewEvent.organizername = organizername;
            viewEvent.organizerid = organizerid;
            viewEvent.fblink = fblink;
            viewEvent.twitterlink = twitterlink;
            viewEvent.Linkedinlin = Linkedin;
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
       [HttpPost]
        public string savevote(string Eventid)
        {
            if (Session["AppId"] != null)
            {
                Session["ReturnUrl"] = Url.Action("ViewEvent", "CreateEvent", new { strUrlData = "a౼" + Eventid + "౼N" });
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
                Session["ReturnUrl"] = Url.Action("ViewEvent", "CreateEvent", new { strUrlData = "a౼" + Eventid + "౼N" });
                return "Y";

            }
        }
      [HttpPost]
        public string savefavourite(string Eventid,string type)
        {

            if (Session["AppId"] != null)
            {
                string url = Url.Action("ViewEvent", "CreateEvent", new { strUrlData = "a౼" + Eventid + "౼N" });
                Session["ReturnUrl"] = Url.Action("ViewEvent", "CreateEvent", new { strUrlData = "a౼" + Eventid + "౼N" });
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
                string url = Url.Action("ViewEvent", "CreateEvent", new { strUrlData = "a౼" + Eventid + "౼N" });
                Session["ReturnUrl"] = Url.Action("ViewEvent", "CreateEvent", new { strUrlData ="a౼"+ Eventid+"౼N"  });
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
              int i=  db.SaveChanges();
                return "saved";

            }
          
          
           

        }

        public string PublishEvent(long lEventId)
        {
            string strResult = "N";
            try
            {
                string strUserId = (Session["AppId"] != null ? Session["AppId"].ToString() : "");
                if (strUserId != "" && lEventId >0)
                {
                    using (EventComboEntities objEnt = new EventComboEntities())
                    {
                        objEnt.PublishEvent(lEventId, strUserId);
                    }
                    strResult = "Y";
                }
            }
            catch (Exception)
            {
                strResult ="N";
            }
            return strResult;
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

                        objEnt.SaveChanges();
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
                    objTLD.Locktime = DateTime.Now;
                    objTLD.TLD_Donate = objModel.TLD_Donate;
                    objTLD.TicketAmount = objModel.TicketAmount;
                    context.Ticket_Locked_Detail.Add(objTLD);


                }


                if (strResult == "Y")
                    context.SaveChanges();
                
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
            var checkpwd = (from obj in obje.Events where obj.EventID == id && obj.Private_Password.Trim() == password.Trim() select obj).Any();
            if(checkpwd==true)
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
            catch (Exception)
            {
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
                    var EvList = (from myRow in objEnt.Events
                                  select myRow).ToList().OrderByDescending(m => m.EventID);

                    strHtml.Append("<option value='0'>Select Any Event</option>");
                    foreach (var item in EvList)
                    {
                        if (item.EventTitle != null && item.EventTitle.Trim() != "")
                            strHtml.Append("<option>" + @Url.Action("ViewEvent", "ViewEvent", new { strEventDs = Regex.Replace(item.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", "") , strEventId = item.EventID.ToString() }) + "</option>");
                    }
                    return strHtml.ToString();
                }
            }
            catch (Exception ex)
            {
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
