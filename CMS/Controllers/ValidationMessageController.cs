using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Models;

namespace CMS.Controllers
{
    public class ValidationMessageController : Controller
    {
        EmsEntities db = new EmsEntities();
        // GET: ValidationMessage
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
                using (EmsEntities db = new EmsEntities())
                {
                    message = (from cpd in db.Messages
                               where cpd.Form_Name.Trim().ToLower() == formname.Trim().ToLower() && cpd.Form_Tag.Trim().ToLower() == errortype.Trim().ToLower()
                               select cpd.Message1).FirstOrDefault();


                }
                return message;
            }
            catch (Exception ex)
            {
                message = "There is some error.Please contact system administrator";
                return message;
            }

        }
    }
}