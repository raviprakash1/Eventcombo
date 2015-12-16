using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Models;
namespace CMS.Controllers
{
    public class EventTypeController : Controller
    {
        // GET: EventType
        public ActionResult EventType()// Calling when we first hit controller.
        {
            EventType objET = new Models.EventType();
            EmsEntities objEntity = new EmsEntities();
            
                var modelPerm = (from EventType in objEntity.EventTypes
                                 orderby EventType.EventType1 ascending
                                 select EventType).ToList();
                
                //List<string> EventTypeList = new List<string>();
                //foreach (var item in modelPerm)
                //{                    
                //    EventTypeList.Add(item.EventType1);                    
                //}
                //ViewBag.EventType = EventTypeList;
                   
                return View(modelPerm);
            
        }
        public string DeleteType(long iType)
        {
            string strResult = string.Empty;
            try
            {
                EmsEntities objEntity = new EmsEntities();
                objEntity.Database.ExecuteSqlCommand("Delete from EventType where EventTypeID='" + iType + "' ");
                TempData["SuccessMessage"] = "Event Sub Category Deleted.";
                strResult = "Y";
            }
            catch (Exception ex)
            {
                strResult = "N";
            }

            return strResult;
        }
        public string AddType(string Type)
        {
            return "N";

        }



        [HttpPost]
        public ActionResult EventType(EventType et,string submit,string EventTypeListBox,string txtEventType)
        {
            var modelPerm=new List<EventType>();
            if (ModelState.IsValid)
            {
                string Add = submit;
                EventType objET = new Models.EventType();
                if(submit=="Add")
                {

                    using (EmsEntities objEntity = new EmsEntities())
                    {

                        bool IsEventExists = false;
                        try
                        {
                            var query = from EventType in objEntity.EventTypes
                                        where EventType.EventType1 == txtEventType
                                        select EventType;

                            foreach (EventType evnttyp in query)
                            {
                                IsEventExists = true;
                                TempData["SuccessMessage"] = "Event Type Already Exists.";

                            }
                            if (IsEventExists != true)
                            {
                                et.EventType1 = txtEventType;
                                objEntity.EventTypes.Add(et);
                                objEntity.SaveChanges();
                                TempData["SuccessMessage"] = "Event Type Created";
                            }
                        }
                        catch (Exception ex)
                        {
                            string message = ex.Message;

                        }
                        modelPerm = (from EventType in objEntity.EventTypes
                                     orderby EventType.EventType1 ascending
                                     select EventType).ToList();

                                             
                    }


                }



                //switch (submit)
                //{
                //    case "Add":

                                           
                //        break;

                //    case "Edit":
                //        using (EmsEntities objEntity = new EmsEntities())
                //        {
                //            var query = from EventType in objEntity.EventTypes
                //                        where EventType.EventType1== EventTypeListBox.Trim()
                //                        select EventType;
                            
                //            foreach (EventType evnttyp in query)
                //            {
                //                evnttyp.EventType1= et.EventType1;                                
                //            }
                            
                //            try
                //            {
                //                objEntity.SaveChanges();
                //            }

                //            catch (Exception ex)
                //            {
                //                string message = ex.Message;

                //            }
                //             modelPerm = (from EventType in objEntity.EventTypes
                //                             orderby EventType.EventType1 ascending
                //                             select EventType).ToList();

                //            List<string> EventTypeList = new List<string>();
                //            foreach (var item in modelPerm)
                //            {
                //                EventTypeList.Add(item.EventType1);
                //            }
                //            ViewBag.EventType = EventTypeList;
                //            TempData["SuccessMessage"] = "Event Type Edited.";
                //            ModelState.Clear();
                //            et = new Models.EventType();
                //        }
                //        break;

                //    case "Delete":
                //        using (EmsEntities objEntity = new EmsEntities())
                //        {
                //            try
                //            {
                //                objEntity.Database.ExecuteSqlCommand("Delete from EVENTTYPE where EventType='" + et.EventType1 + "'");
                //                TempData["SuccessMessage"] = "Event Type Deleted.";
                //            }
                //            catch (Exception ex)
                //            {
                //                TempData["SuccessMessage"] ="This Event Type is Already in Use.";                                
                //            }
                //             modelPerm = (from EventType in objEntity.EventTypes
                //                             orderby EventType.EventType1 ascending
                //                             select EventType).ToList();

                //            List<string> EventTypeList = new List<string>();
                //            foreach (var item in modelPerm)
                //            {
                //                EventTypeList.Add(item.EventType1);
                //            }
                //            ViewBag.EventType = EventTypeList;
                //            ModelState.Clear();
                //            et = new Models.EventType();
                //        }
                //        break;                        
                //    //case "Reset":
                //    //    using (EmsEntities objEntity = new EmsEntities())
                //    //    {                                                      
                //    //        var modelPerm = (from EventType in objEntity.EventTypes
                //    //                         orderby EventType.EventType1 ascending
                //    //                         select EventType).ToList();

                //    //        List<string> EventTypeList = new List<string>();
                //    //        foreach (var item in modelPerm)
                //    //        {
                //    //            EventTypeList.Add(item.EventType1);
                //    //        }
                //    //        ViewBag.EventType = EventTypeList;
                //    //    }
                //    //    break;
                //    default:
                //        throw new Exception();                        
                //}                
                return View(modelPerm);
            }
            else
            {
                EmsEntities db = new EmsEntities();
                modelPerm = (from EventType in db.EventTypes
                             orderby EventType.EventType1 ascending
                             select EventType).ToList();
                ModelState.AddModelError("", "Error in saving data");
                return View(modelPerm);
            }

        }   
    }
}