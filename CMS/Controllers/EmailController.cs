using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Models;
namespace CMS.Controllers
{
    public class EmailController : Controller
    {
        EmsEntities db = new EmsEntities();
        // GET: Email
        public ActionResult Index(string templatetag,string templatename)
        {
            if (templatename == null)
            {
                templatename = "Welcome Template";
            }
            EmailTemplate obj = new EmailTemplate();
            obj = EmailData(templatetag);
            if(obj!=null)
            {
                return View(obj);

            }else
            {
                EmailTemplate obj1 = new EmailTemplate();
                obj1.Template_Name = templatename + "Template";
                return View(obj1);

            }
           
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Index(EmailTemplate model)
        {
            if (ModelState.IsValid)
            {

                EmailTemplate obj = new EmailTemplate();
                obj = EmailData(model.Template_Name);
                if (obj != null)
                {


                    using (EmsEntities objEntity = new EmsEntities())
                    {

                        Email_Template email = objEntity.Email_Template.First(i => i.Template_Name == model.emailtag);

                        email.Template_Name = model.Template_Name;
                        email.Subject = model.Subject;
                        email.To = model.To;
                        email.CC = model.CC;
                        email.Bcc = model.Bcc;
                        email.TemplateHtml = model.ckeditor1;


                        objEntity.SaveChanges();

                    }

                }
                else
                {
                    using (EmsEntities objEntity = new EmsEntities())
                    {
                        Email_Template email = new Email_Template();

                        email.Template_Name = model.Template_Name;
                        email.Subject = model.Subject;
                        email.To = model.To;
                        email.CC = model.CC;
                        email.Bcc = model.Bcc;
                        email.TemplateHtml = model.ckeditor1;
                        email.TemplateId = Guid.NewGuid().ToString ();
                        objEntity.Email_Template.Add(email);
                        objEntity.SaveChanges();
                    }


                }
                return View(model);
            }

                return View(model);
        }
        public EmailTemplate EmailData(string templatename)
        {

            using (EmsEntities objEntity = new EmsEntities())
            {
                var modelmyemail = (from cpd in objEntity.Email_Template
                                    where cpd.Template_Name.Equals(templatename)
                                    select new EmailTemplate
                                    {
                                        To = cpd.To,
                                        CC = cpd.CC,
                                        Bcc = cpd.Bcc,
                                        Subject = cpd.Subject,
                                        ckeditor1 = cpd.TemplateHtml,
                                        Template_Name=cpd.Template_Name ,
                                        Template_Id = cpd.TemplateId 

                                    }
                                    );
                                    
                        
                return modelmyemail.FirstOrDefault();

            }



        }

    }
}