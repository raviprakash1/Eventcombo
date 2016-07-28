using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Models;


namespace CMS.Controllers
{
    [CustomAuthorization("6")]
    public class EmailController : Controller
    {
        EmsEntities db = new EmsEntities();
        // GET: Email
        public ActionResult Index(string templatetag,string templatename)
        {
            if ((Session["UserID"] != null))
            {
                if (templatename == null)
                {
                    templatename = "Welcome";
                }
                ViewBag.EmailTags = db.Email_Tag.OrderBy(x=>x.Tag_Name).Select(x => x.Tag_Name).ToList();
                EmailTemplate obj = new EmailTemplate();
                obj = EmailData(templatetag);
                if (obj != null)
                {
                    obj.Template_Name = templatename + " Template";
                    obj.emailtag = templatetag;
                    return View(obj);

                }
                else
                {
                    EmailTemplate obj1 = new EmailTemplate();
                    obj1.Template_Name = templatename + " Template";
                    obj1.emailtag = templatetag;
                    return View(obj1);

                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Index(EmailTemplate model)
        {
            EmailTemplate obj = new EmailTemplate();
            var msg = "";
            if (ModelState.IsValid)
            {
                ViewBag.EmailTags = db.Email_Tag.Select(x=>x.Tag_Name).ToList();
             
                obj = EmailData(model.emailtag);
                if (obj != null)
                {


                    using (EmsEntities objEntity = new EmsEntities())
                    {

                        Email_Template email = objEntity.Email_Template.First(i => i.Template_Tag == model.emailtag);

                        email.Template_Name = model.Template_Name;
                        email.Template_Tag = model.emailtag;
                        email.Subject = model.Subject;
                        email.To = model.To;
                        email.From = model.From;
                        email.From_Name = model.FromName;
                        email.CC = model.CC;
                        email.Bcc = model.Bcc;
                        email.TemplateHtml =model.ckeditor1;


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
                        email.From = model.From;
                        email.TemplateHtml = model.ckeditor1;
                        email.TemplateId = Guid.NewGuid().ToString ();
                        email.Template_Name = model.Template_Name;
                        email.Template_Tag = model.emailtag;
                        objEntity.Email_Template.Add(email);
                        objEntity.SaveChanges();
                    }


                }
                obj = EmailData(model.emailtag);
                ValidationMessageController vmc = new ValidationMessageController();
                msg= vmc.Index("Email", "EmailSuccessSy");
                ViewData["Success"] = msg;
                return View(obj);

            }
            obj = EmailData(model.emailtag);
            return View(obj);

        }
        public EmailTemplate EmailData(string templatename)
        {

            using (EmsEntities objEntity = new EmsEntities())
            {
                var modelmyemail = (from cpd in objEntity.Email_Template
                                    where cpd.Template_Tag.Equals(templatename)
                                    select new EmailTemplate
                                    {
                                        To = cpd.To,
                                        CC = cpd.CC,
                                        Bcc = cpd.Bcc,
                                        From=cpd.From,
                                        FromName =cpd.From_Name,
                                        Subject = cpd.Subject,
                                        ckeditor1 = cpd.TemplateHtml,
                                        Template_Name=cpd.Template_Name ,
                                        Template_Id = cpd.TemplateId 

                                    }
                                    );
                                    
                        
                return modelmyemail.FirstOrDefault();

            }



        }




        public ActionResult Index1()
        {

            return View();

        }


    }
}