using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
using System.Collections;
using System.Data;

namespace EventCombo.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        [HttpPost]
        public ActionResult EventCreation(EventCreation model)
        {



            return View();
        }

        public ActionResult EventCreation()
        {
            return View();
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
                    ObjEC.DisplayStartTime  = model.DisplayStartTime;
                    ObjEC.DisplayEndTime = model.DisplayEndTime;
                    ObjEC.DisplayTimeZone = model.DisplayTimeZone;
                    ObjEC.FBUrl = model.FBUrl;
                    ObjEC.TwitterUrl = model.TwitterUrl;

                    objEnt.Events.Add(ObjEC);
                    // Address info
                    if (model.AddressDetail != null)
                    {
                        Address ObjAdd = new Models.Address();
                        foreach (Address objA in model.AddressDetail)
                        {
                            ObjAdd.EventId = ObjEC.EventID;
                            ObjAdd.Address1 = objA.Address1;
                            ObjAdd.Address2 = objA.Address2;
                            ObjAdd.City = objA.City;
                            ObjAdd.CountryID = objA.CountryID;
                            ObjAdd.State = objA.State;
                            ObjAdd.UserId = strUserId;
                            ObjAdd.VenueName = objA.VenueName;
                            ObjAdd.Zip = objA.Zip;

                            ObjAdd.Name = "";
                            objEnt.Addresses.Add(ObjAdd);

                        }
                    }
 // Event on Single Timing 
                    if (model.EventVenue != null)
                    {
                        EventVenue objEVenue = new EventVenue();
                        foreach (EventVenue objEv in model.EventVenue)
                        {
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
                            objMEvents.EventID = ObjEC.EventID;
                            objMEvents.Frequency = objME.Frequency;
                            objMEvents.WeeklyDay = objME.WeeklyDay;
                            objMEvents.MonthlyDay = objME.MonthlyDay;
                            objMEvents.MonthlyWeek = objME.MonthlyWeek;
                            objMEvents.MonthlyWeekDays = objME.MonthlyWeekDays;
                            objMEvents.StartingFrom = objME.StartingFrom;
                            objMEvents.StartingTo = objME.StartingTo;
                            objMEvents.StartTime= objME.StartTime;
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
                            objEOrg.Orgnizer_Event_Id = ObjEC.EventID;
                            objEOrg.Orgnizer_Name = objOr.Orgnizer_Name ;
                            objEOrg.Orgnizer_Desc = objOr.Orgnizer_Desc;
                            objEnt.Event_Orgnizer_Detail.Add(objEOrg);
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


        //public string Dictionary<string,string> Test()
        //{


        //}
        public List<EventCategory> GetEventType()
        {
            List<EventCategory> objETL = new List<EventCategory>();
            //List<KeyValuePair<string, string>> objList = new List<KeyValuePair<string, string>>();
            try
            {
                

            using (EventComboEntities objEntity = new EventComboEntities())
            {
                    //IEnumerable<SelectListItem> EventTypeList = (from ET in objEntity.EventTypes select ET).AsEnumerable().Select(ET => new SelectListItem() { Text = ET.EventType1, Value = ET.EventTypeID.ToString() });

                    //SelectList objSL = new SelectList(EventTypeList, "Value", "Text");
                    var EventList = objEntity.EventCategories.SqlQuery("Select * from EventCategory").ToList();

                //var modelPerm = (from Perm in objEntity.EventTypes
                //                 select new EventType
                //                 {
                //                     EventTypeID = Perm.EventTypeID,
                //                     EventType1 = Perm.EventType1
                //                 }
                //            );

                    //JsonResult j = Json(modelPerm, JsonRequestBehavior.AllowGet);
                    //objETL = modelPerm.ToList();
                    return EventList;

                //return modelPerm.ToList();

                }
            }
            catch (Exception ex)
            {

                return objETL;
            }
        }



        //public void SaveTable(EventCreation model)
        //{


        //}

    }

}