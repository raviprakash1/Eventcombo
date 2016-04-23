using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Models;


namespace CMS.Controllers
{
    public class MessagesController : Controller
    {
        // GET: Messages
        public ActionResult Index(string Form)
        {
            if ((Session["UserID"] == null))
            {
                return RedirectToAction("Login", "Home");
            }

            List<MessageTemplate> lstmsg = new List<MessageTemplate>();
            TempData["FormName"] = Form;
            lstmsg = MessageData(Form);
            return View(lstmsg);
        }
        [HttpPost]
        public ActionResult Index(ICollection<MessageTemplate> MessageTemplates)
        {
            if ((Session["UserID"] == null))
            {
                return RedirectToAction("Login", "Home");
            }

            string form_name = "";
            EmsEntities objEntity = new EmsEntities();
            foreach (MessageTemplate item in MessageTemplates)
            {
                Message msg = objEntity.Messages.First(i => i.Form_Tag == item.Form_Tag && i.Form_Name==item.Form_Name );
                form_name = item.Form_Name;
                if (msg!=null)
                {
                    msg.Message1 = item.Message;
                 

                }
                objEntity.SaveChanges();
            }
            //List<MessageTemplate> lstmsg = new List<MessageTemplate>();
            //lstmsg = MessageData(Form);
            List<MessageTemplate> lstmsg = new List<MessageTemplate>();
            lstmsg = MessageData(form_name);

            ValidationMessageController vmc = new ValidationMessageController();
             var successmsg = vmc.Index("Messages", "MessageSuccSY");
            ViewData["Saved"] = successmsg;
            TempData["FormName"] = form_name;
            return View(lstmsg);
        }

        private List<MessageTemplate> MessageData(string form)
        {
            using (EmsEntities objEntity = new EmsEntities())
            {
                var modelmyemail = (from cpd in objEntity.Messages
                                    where cpd.Form_Name.Equals(form)
                                    select new MessageTemplate
                                    {
                                        Form_Name = cpd.Form_Name,
                                        Form_Tag = cpd.Form_Tag,
                                        Message = cpd.Message1,
                                      

                                    }
                                    );


                return modelmyemail.ToList();

            }
        }
    }
}