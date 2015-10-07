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

        public string SaveEvent(EventCreation model)
        {
            try
            {

           
            using (EventComboEntities objEnt = new EventComboEntities())
            {
                Event ObjEC = new Event();

                ObjEC.EventTypeID = model.EventTypeID;
                ObjEC.EventCategoryID = model.EventCategoryID;
                ObjEC.EventSubCategoryID = model.EventSubCategoryID;
                ObjEC.UserID = model.UserID;
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
                    ObjAdd.Name = objA.Name;
                    ObjAdd.State = objA.State;
                    ObjAdd.UserId = objA.UserId;
                    ObjAdd.VenueName = objA.VenueName;
                    ObjAdd.Zip = objA.Zip;
                    objEnt.Addresses.Add(ObjAdd);
                }

                objEnt.SaveChanges();

                //modelAdd.Address1 = model.Address1;
                //modelAdd.Address2 = model.Address2;
                //modelAdd.City = model.City;
                //modelAdd.CountryID = 1;
                //modelAdd.Name = model.Name;
                //modelAdd.State = model.State;
                //modelAdd.Zip = model.Zip;
                //modelEnt.Addresses.Add(modelAdd);
                //modelEnt.SaveChanges();
            }
            }
            catch (Exception ex)
            {
                return ex.InnerException.ToString();
            }
            return "Test";
        }

        //public void SaveTable(EventCreation model)
        //{


        //}

    }

}