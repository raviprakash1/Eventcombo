using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
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
                    
                    objEnt.Events.Add(ObjEC);
                    //    objEnt.Events.Add(ObjEC)
                    Address ObjAdd = new Models.Address();
                    foreach (Address objA in model.AddressDetail)
                    {
                        ObjAdd.Address1 = objA.Address1;
                        ObjAdd.Address2 = objA.Address2;
                        ObjAdd.City = objA.City;
                        ObjAdd.CountryID = objA.CountryID;
                        ObjAdd.State = objA.State;
                        ObjAdd.UserId = strUserId;
                        ObjAdd.VenueName = objA.VenueName;
                        ObjAdd.Zip = objA.Zip;
                        ObjAdd.EventId = ObjEC.EventID;
                        ObjAdd.Name = "";
                        objEnt.Addresses.Add(ObjAdd);
                        
                    }

                    EventVenue objEventVenue = new EventVenue();
                    objEventVenue.EventID= ObjEC.EventID;
                    objEventVenue.EventStartDate = DateTime.Today;
                    objEventVenue.EventEndDate = DateTime.Today;
                    objEventVenue.EventStartTime =  TimeSpan.Parse("09:00");
                    objEventVenue.EventEndTime = TimeSpan.Parse("12:00");
                    objEnt.EventVenues.Add(objEventVenue);





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



       
        //public void SaveTable(EventCreation model)
        //{


        //}

    }

}