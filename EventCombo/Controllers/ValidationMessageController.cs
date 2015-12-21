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
                    lParentID = (from myEvt in db.Events
                                 where myEvt.Parent_EventID == lEvntId
                                 select myEvt.EventID).Count();
                    
                    if (lParentID >1)
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
     
        public string Index(string strFormName, string strFormTag)
       {
            string result = geterrorMessage(strFormName, strFormTag);
            return result;
        }

        private string geterrorMessage(string formname, string errortype)
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