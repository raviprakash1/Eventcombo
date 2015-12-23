using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;

namespace EventCombo.Controllers
{
    public class ValidationMessageController : Controller
    {
        // GET: ValidationMessage
        public long GetLatestEventId(long lEvntId)
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                var lParentID = (from myEvt in db.Events
                           where myEvt.EventID == lEvntId
                           select myEvt.Parent_EventID).FirstOrDefault();
                if (lParentID > 0)
                {
                    lParentID = (from myEvt in db.Events
                                   where myEvt.Parent_EventID == lParentID
                                   select myEvt.EventID).Max();
                }
                else
                {
                   int lCnt = (from myEvt in db.Events
                                 where myEvt.Parent_EventID == lEvntId
                                 select myEvt.EventID).Count();
                    
                    if (lCnt >0 )
                    {
                        lParentID = (from myEvt in db.Events
                                     where myEvt.Parent_EventID == lEvntId
                                     select myEvt.EventID).Max();
                    }
                }
                if ( lParentID == null || lParentID <=0) lParentID = lEvntId;

                return (long)lParentID;                    
            }
        }
<<<<<<< HEAD
        public Event GetSelectedEventDetail(string strGuid)
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                var EventId = (from MyE in db.Ticket_Locked_Detail where MyE.TLD_GUID == strGuid select MyE.TLD_Event_Id).FirstOrDefault();
                var MyEvent = (from MyEv in db.Events
                               where MyEv.EventID == EventId
                               select MyEv).FirstOrDefault();
                return MyEvent;
            }
        }

=======
     
>>>>>>> 6d8e87ad6388604d27512ee365768068feb2b70d
        public string Index(string strFormName, string strFormTag)
        {
            string result = geterrorMessage(strFormName, strFormTag);
            return result;
        }

        public string geterrorMessage(string formname, string errortype)
        {
            string message = "";
            try
            {
                using (EventComboEntities db = new EventComboEntities())
                {
                    message = (from cpd in db.Messages where cpd.Form_Name.Trim().ToLower() == formname.Trim().ToLower() && cpd.Form_Tag.Trim().ToLower() == errortype.Trim().ToLower()
                               select cpd.Message1).FirstOrDefault();


                }
                return message;
            }
            catch(Exception ex)
            {
                message = "There is some error.Please contact system administrator";
                return message;
            }

        }
    }
}