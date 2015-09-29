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
       
        public ActionResult Index(string formname,string errortype)
       {
            string result = geterrorMessage(formname, errortype);
            return Content(result);
        }

        private string geterrorMessage(string formname, string errortype)
        {
            string message = "";
            using (EventComboEntities db = new EventComboEntities())
            {
                  message = (from cpd in db.Messages  where cpd.Form_Name.Trim().ToLower()  == formname.Trim().ToLower() && cpd.Form_Tag.Trim().ToLower() == errortype.Trim().ToLower()
                             select cpd.Message1 ).SingleOrDefault(); 
            //    if (modelmyaccount.FirstOrDefault() != null)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;

                                      //    }

            }
            return message;

        }
    }
}