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


namespace EventCombo.Controllers
{
    public class EditEventController : Controller
    {
        // GET: EditEvent
        EventComboEntities db = new EventComboEntities();
        public ActionResult EditEvent()
        {
            //  EventCreation objCr = GetEventDataEditing();
            EventCreation objCr = new EventCreation();
            if ((Session["AppId"] != null))
            {
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
                                Selected=true

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
                                Selected=true
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


                return View(objCr);
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }


        public ActionResult ModifyEvent(long Eventid)
        {

            EventCreation objCr = GetEventDataEditing(Eventid);
            if ((Session["AppId"] != null))
            {
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

                    foreach (var item in PrevAdd)
                    {
                        if (item.AddressID == lPreAddId)
                            strHtml.Append("<option selected='selected' value=" + item.AddressID.ToString() + ">" + item.VenueName + "," + item.Address1 + "," + item.Address2 + "," + item.City + "," + item.Zip + "</option>");
                        else
                            strHtml.Append("<option value=" + item.AddressID.ToString() + ">" + item.VenueName + "," + item.Address1 + "," + item.Address2 + "," + item.City + "," + item.Zip + "</option>");
                    }
                    return strHtml.ToString();
                }
            }
            catch (Exception ex)
            {
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
                }
            }
            catch (Exception ex)
            {
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
                               ModifyDate =  "(Last Saved at " + (myEvent.ModifyDate.ToString().Trim() != "" ? myEvent.ModifyDate.ToString().Trim() : myEvent.CreateDate.ToString().Trim()) + ")" 
                           }
                        ).FirstOrDefault();
                return vEC;
            }
        }
        [HttpGet]
        public ActionResult GetEventChildData(long lEventId)
        {
            GetEventData objJson = new GetEventData();
            using (EventComboEntities objEnt = new EventComboEntities())
            {
                var Event = (from myEnt in objEnt.Events where myEnt.EventID == lEventId select myEnt).FirstOrDefault();
                var ev = (from myEvent in objEnt.EventVenues
                          where myEvent.EventID == lEventId
                          select myEvent).FirstOrDefault();
                if (ev != null)
                {
                    objJson.EventID = Event.EventID;
                    objJson.EventStartDate =  Convert.ToString(ev.EventStartDate);
                    objJson.EventStartTime = ev.EventStartTime;
                    objJson.EventEndDate = ev.EventEndDate;
                    objJson.EventEndTime = ev.EventEndTime;
                    objJson.AddressStatus = Event.AddressStatus;
                    objJson.EventID = Event.EventID;
                    objJson.MultipleSchTime = "S";
                }




                var Mv = (from myEvent in objEnt.MultipleEvents
                          where myEvent.EventID == lEventId
                          select myEvent).FirstOrDefault();

                if (Mv != null)
                {
                    objJson.Frequency = Mv.Frequency;
                    objJson.WeeklyDay = Mv.WeeklyDay;
                    objJson.MonthlyDay = Mv.MonthlyDay;
                    objJson.MonthlyWeek = Mv.MonthlyWeek;
                    objJson.MonthlyWeekDays = Mv.MonthlyWeekDays;
                    objJson.StartingFrom = Mv.StartingFrom;
                    objJson.StartingTo = Mv.StartingTo;
                    objJson.StartTime = Mv.StartTime;
                    objJson.EndTime = Mv.EndTime;
                    objJson.MultipleSchTime = "M";
                }


                var Addr = (from myEvent in objEnt.Addresses
                          where myEvent.EventId == lEventId
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

                    strHTML.Append("<td style='display: none'> <input type='text' name='' id=VenueName" + i.ToString() + " style='width: 100px;'  value='" + objAdd.VenueName + "' /></td>");
                    strHTML.Append("<td style='display: none'> <input type='text' name='' id=Address1" + i.ToString() + " style='width: 100px;'  value='" + objAdd.Address1 + "' /></td>");
                    strHTML.Append("<td style='display: none'> <input type='text' name='' id=Address2" + i.ToString() + " style='width: 100px;'  value='" + objAdd.Address2 + "' /></td>");
                    strHTML.Append("<td style='display: none'> <input type='text' name='' id=City" + i.ToString() + " style='width: 100px;'  value='" + objAdd.City + "' /></td>");
                    strHTML.Append("<td style='display: none'> <input type='text' name='' id=State" + i.ToString() + " style='width: 100px;'  value='" + objAdd.State + "' /></td>");
                    strHTML.Append("<td style='display: none'> <input type='text' name='' id=Zip" + i.ToString() + " style='width: 100px;'  value='" + objAdd.Zip + "' /></td>");
                    strHTML.Append("<td style='display: none'> <input type='text' name='' id=CountryID" + i.ToString() + " style='width: 100px;'  value='" + objAdd.CountryID + "' /></td>");
                    strHTML.Append("<td style='display: none'> <input type='text' name='' id=CID" + i.ToString() + " style='width: 100px;'  value='" + objAdd.CountryID + "' /></td>");
                    strHTML.Append("<td align='left'><label id=consolidate" + i.ToString() + ">" + objAdd.ConsolidateAddress +  "</label></td>");
                    strHTML.Append("<td><div class='trigger mt5 ent_add'><a href = '#' onclick='editRow("+i.ToString()+");'><i class='fa fa-map-marker'></i> Edit</a><a href = '#' id='btAddDelete' onclick='DeleteTableRow("+i.ToString()+")'>Delete</a> </div> </td>");
                    strHTML.Append("</tr>");

                    //strHTML = strHTML + '<td style="display:none"> <input type="text" name=" " id="' + CellId + '" style="width:100px;" value="' + ColAry[i] + '" /></td>';
                }

                objJson.Addresses = strHTML.ToString();
                objJson.EventDescription = Event.EventDescription;
                objJson.EventStatus = Event.EventStatus;
            }
            return Json(objJson, JsonRequestBehavior.AllowGet);
        }

        public long EditEventInfo(EventCreation model)
        {
            long lEventId = model.EventID;
            try
            {
                string strUserId = (Session["AppId"] != null ? Session["AppId"].ToString() : "");


                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    Event ObjEC = objEnt.Events.First(i => i.EventID == lEventId);
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
                    ObjEC.ModifyDate = DateTime.Now;
                    
                    // Address info
                    if (model.AddressStatus == "Online")
                    {
                        objEnt.Addresses.RemoveRange(objEnt.Addresses.Where(x => x.EventId == lEventId));
                    }
                    else if (model.AddressDetail != null)
                    {
                        Address ObjAdd = new Models.Address();
                        objEnt.Addresses.RemoveRange(objEnt.Addresses.Where(x => x.EventId == lEventId));
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
                        EventVenue objEVenue = objEnt.EventVenues.First(Ev => Ev.EventID == lEventId) ;
                        foreach (EventVenue objEv in model.EventVenue)
                        {
                            objEVenue.EventID = ObjEC.EventID;
                            objEVenue.EventStartDate = objEv.EventStartDate;
                            objEVenue.EventEndDate = objEv.EventEndDate;
                            objEVenue.EventStartTime = objEv.EventStartTime;
                            objEVenue.EventEndTime = objEv.EventEndTime;

                        }
                    }
                    // Event on Multiple timing 
                    if (model.MultipleEvents != null)
                    {
                        MultipleEvent objMEvents = objEnt.MultipleEvents.First(Ev => Ev.EventID == lEventId);
                        foreach (MultipleEvent objME in model.MultipleEvents)
                        {
                          
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
                            //ObjEC.MultipleEvents.Add(objMEvents);
                        }
                    }
                    // Orgnizer
                    if (model.Orgnizer != null)
                    {
                        Event_Orgnizer_Detail objEOrg = new Event_Orgnizer_Detail();
                        objEnt.Event_Orgnizer_Detail.RemoveRange(objEnt.Event_Orgnizer_Detail.Where(x => x.Orgnizer_Event_Id == lEventId));
                        foreach (Event_Orgnizer_Detail objOr in model.Orgnizer)
                        {
                            objEOrg = new Event_Orgnizer_Detail();
                            objEOrg.Orgnizer_Event_Id = ObjEC.EventID;
                            objEOrg.Orgnizer_Name = objOr.Orgnizer_Name;
                            objEOrg.Orgnizer_Desc = objOr.Orgnizer_Desc;
                            objEOrg.FBLink = objOr.FBLink;
                            objEOrg.Twitter = objOr.Twitter;
                            objEOrg.DefaultOrg = objOr.DefaultOrg;
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
                   // lEventId = ObjEC.EventID;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return lEventId;
        }
        public string GetOrgnizerDetail(long lEventId)
        {
            StringBuilder strHTML = new StringBuilder();
            string strtemp;
            StringBuilder strDropDown = new StringBuilder();
            using (EventComboEntities objEnt = new EventComboEntities())
            {
                var EventOrg = (from Org in objEnt.Event_Orgnizer_Detail
                                where Org.Orgnizer_Event_Id == lEventId
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
                    strtemp = "<td align='right'><i onclick='editOrgnizer(" + i + ")'; class='fa fa-pencil'></i> | <i onclick='DeleteOrgnizer(" + i + ");' class='fa fa-trash'></i></td>";
                    strHTML.Append(strtemp);
                    strHTML.Append("</tr>");

                    if (EOD.DefaultOrg == "Y")
                        strDropDown.Append("<option selected='selected' value=" + i.ToString() + " id=" + i.ToString() + ">" + EOD.Orgnizer_Name + "</option>");
                    else
                        strDropDown.Append("<option value=" + i.ToString() + " id=" + i.ToString() + ">" + EOD.Orgnizer_Name + "</option>");


                }
            }

            return strHTML.ToString() + "¶" + strDropDown.ToString();
            
        }

        #endregion


        public ActionResult ViewEvent(long EventId, string eventTitle)
        {
            string sDate_new = "", eDate_new = "";
            string startday = "", endday = "", starttime = "", endtime = "";
            Session["Fromname"] = "ViewEvent";
           
            var url = Url.Action("ViewEvent", "CreateEvent") + "?strUrlData=" + eventTitle.Trim()+ "౼" + EventId+ "౼N";
            Session["ReturnUrl"] = "ViewEvent~" + url;
            var TopAddress = ""; var Topvenue = "";
            string organizername = "", fblink = "", twitterlink = "", organizerid = "", tickettype = "";
            ViewEvent viewEvent = new ViewEvent();
            var EventDetail = GetEventdetail(EventId);
            var OrganiserDetail = (from ev in db.Event_Orgnizer_Detail where ev.Orgnizer_Event_Id == EventId && ev.DefaultOrg == "Y" select ev).FirstOrDefault();
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
                organizername = OrganiserDetail.Orgnizer_Name;
                fblink = OrganiserDetail.FBLink;
                twitterlink = OrganiserDetail.Twitter;
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
                int i = db.SaveChanges();
                return "saved";

            }




        }
    }
}