using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CMS.Models;
    namespace CMS.Controllers
{
    public class ManageEventController : Controller
    {
        // GET: ManageEvent
        public ActionResult Index()
        {
            return View(GetAllEvents("","",""));
        }
        public List<EventCreation> GetAllEvents(string SearchStringFirstName, string SearchStringLastName, string SearchStringEmail)
        {
            string user = (Session["AppId"] != null ? Session["AppId"].ToString() : string.Empty);

            if (SearchStringFirstName == null) SearchStringFirstName = "";
            if (SearchStringLastName == null) SearchStringLastName = "";
            if (SearchStringEmail == null) SearchStringEmail = "";

            using (EmsEntities objEntity = new EmsEntities())
            {

                List<EventCreation> objEv = new List<EventCreation>();
                objEv = objEntity.GetEventListing().ToList();

                //var modelEvent = (from Ev in objEntity.Events
                //                  join EType in objEntity.EventTypes on Ev.EventTypeID equals EType.EventTypeID
                //                  select new Event
                //                  {
                //                      EventTitle = Ev.EventTitle,
                //                      EventCategoryID = Ev.EventCategoryID,
                //                      EventSubCategoryID = Ev.EventSubCategoryID,
                //                     // EventTypeName = EType.EventType1
                //                  }
                //                    );
                return objEv;
            }
            //if (!SearchStringFirstName.Equals(string.Empty))
            //    return modelUserTemp.Where(us => us.FirstName.ToLower().Contains(SearchStringFirstName.ToLower()) || us.LastName.ToLower().Contains(SearchStringLastName.ToLower())
            //    || us.EMail.ToLower().Contains(SearchStringEmail.ToLower())).ToList();
            //else

        


            
        }






    }
}