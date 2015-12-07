using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using EventCombo.Models;
using System.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Mail;
using System.IO;
using Facebook;
using System.Web.Security;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;

namespace EventCombo.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        EventComboEntities db = new EventComboEntities();
        public HomeController()
        {
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public string IsValid(string Email)
        {

            var user = UserManager.FindByEmail(Email);

            if (user == null)
            {
                return "NotFound";

            }
            else
            {
                return "Found";
            }


        }
        public string IsValidID(string Email,string Password)
        {

            var user = UserManager.FindByEmail(Email);

            if (user == null)
            {
                return "NotFound";

            }
            else
            {
             var   result = UserManager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, Password);

          if(result.ToString()=="Success")
                {
                    return "PasswordMatch";

                }
                else
                {
                    return "PasswordNotMatch";

                }
               
            }


        }
       

        public ActionResult DiscoverEvents()
        {
            if ((Session["AppId"] != null))
            {
                HomeController hmc = new HomeController();
                hmc.ControllerContext = new ControllerContext(this.Request.RequestContext, hmc);
                string usernme = hmc.getusername();
                if (string.IsNullOrEmpty(usernme))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            Session["Fromname"] = "DiscoverEvents";
            return View();

        }
        public ActionResult GetBuzz()
        {
            if ((Session["AppId"] != null))
            {
                HomeController hmc = new HomeController();
                hmc.ControllerContext = new ControllerContext(this.Request.RequestContext, hmc);
                string usernme = hmc.getusername();
                if (string.IsNullOrEmpty(usernme))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            Session["Fromname"] = "GetBuzz";
            ViewBag.ReturnUrl = "~/Home/GetBuzz";
            return View();

        }
        public ActionResult EventOraganizer()
        {
            if ((Session["AppId"] != null))
            {
                HomeController hmc = new HomeController();
                hmc.ControllerContext = new ControllerContext(this.Request.RequestContext, hmc);
                string usernme = hmc.getusername();
                if (string.IsNullOrEmpty(usernme))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            Session["Fromname"] = "EventOraganizer";
            return View();

        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ActionResult Index()
        {
            Session["Fromname"] = "Home";
            if(Session["AppId"]!=null)
            {
              string var=  getusername();
                if(string.IsNullOrEmpty(var))
                {
                    Session["AppId"] = null;
                }

            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult PasswordReset(string code)
        {
            Session["Fromname"] = "PasswordReset";
            if (code == null)
            {
              return  RedirectToAction("Index", "Home");

            }
            else
            {
                Session["code"] = code;
             return  View();

            }
        

        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PasswordReset(ResetPasswordViewModel model)
        {
            string code = "";
            var error = "";
            var success = "";
            Session["Fromname"] = "PasswordReset";
            ValidationMessageController vmc = new ValidationMessageController();
            if (model.Password!=model .ConfirmPassword)
            {
                error = vmc.Index("ResetPassword", "PwdResetPwdValidationSys");
                ViewData["Error"] = error;
                ModelState.AddModelError("Error", error);

            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (Session["code"] != null)
            {

                code = Session["code"].ToString();
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
            using (EventComboEntities objEntity = new EventComboEntities())
            {
                var user = (from cpd in objEntity.AspNetUsers
                            where cpd.Id.Trim() == code.Trim()
                            select new ExternalLogin
                            {
                                userid = cpd.Id,
                                email = cpd.Email

                            }).FirstOrDefault();


                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    return RedirectToAction("Index", "Home");
                }
            }
            var token = await UserManager.GeneratePasswordResetTokenAsync(code);

            var result = await UserManager.ResetPasswordAsync(code, token, model.Password);
           // var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                success = vmc.Index("ResetPassword", "PwdResetSuccessInitSY");
                ViewData["Message"] = success;
                return View();
            }
            AddErrors(result);
            return View();

        }
        [AllowAnonymous]
       public ActionResult ForgetPassword()
        {
            Session["Fromname"] = "ForgetPassword";
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(ForgetPassword model)
        {
            Session["Fromname"] = "ForgetPassword";
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = UserManager.FindByEmail(model.Email);
            var username = "";
            string id = user.Id;
            var profile = db.Profiles.Where(x => x.UserID == id).FirstOrDefault();
            if(profile!=null)
            {
                username = !string.IsNullOrEmpty(profile.FirstName)? profile.FirstName : "";

            }
          
            //string readFile = "";
           // using (StreamReader reader = new StreamReader(Server.MapPath("~/ForgotPassword.html")))
           // {
           //     readFile = reader.ReadToEnd();
           // }
           var url = Request.Url;
            var baseurl = url.GetLeftPart(UriPartial.Authority);
           string url1 = baseurl + Url.Action("PasswordReset", "Home") + "?code=" + id +" ";

           // string myString = "";
           // myString = readFile;
           // myString = myString.Replace("¶¶Email¶¶", model.Email);
           // myString = myString.Replace("¶¶Website¶¶", url1);

           // SendMail(model.Email, myString.ToString(), "The Eventcombo Team");

            /// to send email////
            /// 
            string to = "", from = "", cc = "", bcc = "", subjectn = "";
            var bodyn = "";
            List<Email_Tag> EmailTag = new List<Email_Tag>();
            EmailTag = getTag();

            var Emailtemplate = getEmail("email_lost_pwd");
            if (Emailtemplate != null)
            {
                if (!string.IsNullOrEmpty(Emailtemplate.To))
                {


                    to = Emailtemplate.To;
                    if (to.Contains("¶¶UserEmailID¶¶"))
                    {
                        to = to.Replace("¶¶UserEmailID¶¶", model.Email);

                    }
                }
                if (!(string.IsNullOrEmpty(Emailtemplate.From)))
                {
                    from = Emailtemplate.From;
                    if (from.Contains("¶¶UserEmailID¶¶"))
                    {
                        from = from.Replace("¶¶UserEmailID¶¶", model.Email);

                    }
                }
                else
                {
                    from = "shweta.sindhu@kiwitech.com";

                }
                if (!(string.IsNullOrEmpty(Emailtemplate.CC)))
                {
                    cc = Emailtemplate.CC;
                    if (cc.Contains("¶¶UserEmailID¶¶"))
                    {
                        cc = cc.Replace("¶¶UserEmailID¶¶", model.Email);

                    }
                }
                if (!(string.IsNullOrEmpty(Emailtemplate.Bcc)))
                {
                    bcc = Emailtemplate.Bcc;
                    if (bcc.Contains("¶¶UserEmailID¶¶"))
                    {
                        bcc = bcc.Replace("¶¶UserEmailID¶¶", model.Email);

                    }
                }
                if (!string.IsNullOrEmpty(Emailtemplate.Subject))
                {


                    subjectn = Emailtemplate.Subject;

                    for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
                    {

                        if (subjectn.Contains("¶¶" + EmailTag[i].Tag_Name.Trim() + "¶¶"))
                        {
                            if (EmailTag[i].Tag_Name == "UserEmailID")
                            {
                                subjectn = subjectn.Replace("¶¶UserEmailID¶¶", model.Email);

                            }
                            if (EmailTag[i].Tag_Name == "UserFirstNameID")
                            {
                                subjectn = subjectn.Replace("¶¶UserFirstNameID¶¶", username);

                            }

                        }

                    }
                }
                if (!string.IsNullOrEmpty(Emailtemplate.TemplateHtml))
                {
                    bodyn = new MvcHtmlString(HttpUtility.HtmlDecode(Emailtemplate.TemplateHtml)).ToHtmlString();
                    for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
                    {

                        if (bodyn.Contains("¶¶" + EmailTag[i].Tag_Name.Trim() + "¶¶"))
                        {
                            if (EmailTag[i].Tag_Name == "UserEmailID")
                            {
                                bodyn = bodyn.Replace("¶¶UserEmailID¶¶", model.Email);

                            }
                            if (EmailTag[i].Tag_Name == "UserFirstNameID")
                            {
                                bodyn = bodyn.Replace("¶¶UserFirstNameID¶¶", username);

                            }
                            if (EmailTag[i].Tag_Name == "ResetPwdUrl")
                            {
                                bodyn = bodyn.Replace("¶¶ResetPwdUrl¶¶", url1);

                            }

                        }

                    }
                }
                SendHtmlFormattedEmail(to, from, subjectn, bodyn,cc,bcc);
            }
            ValidationMessageController vmc = new ValidationMessageController();
           var msg= vmc.Index("ForgotPassword", "ForgotPwdSuccessInitSY");
            ViewData["Message"] = msg;
            return View();
        }


        public void SendMail(string toaddress,string messagebody,string messageSubject)
        {
            //var fromAddress = new MailAddress("shweta.sindhu@kiwitech.com", "Shweta");
            //var toAddress = new MailAddress(toaddress);
            //const string fromPassword = "Shweta1989";
            //const string address = "shweta.sindhu@kiwitech.com";
            string subject = messageSubject;
            string body = messagebody;

            //var smtp = new SmtpClient
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential(address, fromPassword)
            //};
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(toaddress));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["Host"];
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
                NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                smtp.Send(mailMessage);
            }
          

        }

        public ActionResult UserName()
        {
            string result = getusername();

            if(string.IsNullOrEmpty (result))
            { Session["AppId"] = null; }
            return Content(result);
        }
        public void setsessid(string id)
        {
            if (id == "discover")
            {



                Session["ReturnUrl"]= Url.Action("DiscoverEvents", "Home");

            }

            if (id == "GetBuzz")
            {
                Session["ReturnUrl"] = Url.Action("GetBuzz", "Home");
            
             

            }
         


        }
        public string checkid() {
            Session["ReturnUrl"] = "CreateEvent~" + Url.Action("CreateEvent", "CreateEvent");


            if (Session["AppId"] == null)
            {

                return "Y";

            }
            else
            {
                return "N";

            }
                }

        public List<Email_Tag> getTag()
        {
            var EmailTag = db.Email_Tag.ToList();
            return EmailTag;

        }
        public Email_Template getEmail(string template)
        {
           
            var userEmail = db.Email_Template.Where(x => x.Template_Tag == template).SingleOrDefault();
            
                return userEmail;
            

        }

        public string getusername()
        {
            if ((Session["AppId"] != null))
            {
                string userid = Session["AppId"].ToString();
                var userEmail = db.AspNetUsers.Where(x => x.Id == userid).Select(y => y.Email).SingleOrDefault();
                if (userEmail != null)
                {
                    return userEmail.Substring(0, userEmail.IndexOf("@")+1);
                }
                else
                {
                  
                    return "";
                }

            }
            else
            {
                
                return "";

            }
        }

      

        [HttpPost]
        [AllowAnonymous]
       
        public async Task<ActionResult> Signup(LoginViewModel model)
        {
           
            string url = null;
            if (Session["ReturnUrl"] != null)
            {
                url = Session["ReturnUrl"].ToString();
                if (url.Contains("~"))
                {
                    string[] urlnew = url.Split('~');
                    url = urlnew[1];
                }
            }
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);

                var Userid = UserManager.FindByEmail(model.Email);


                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    await this.UserManager.AddToRoleAsync(user.Id,"Member");

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    using (EventComboEntities objEntity = new EventComboEntities())
                    {
                        User_Permission_Detail permdetail = new User_Permission_Detail();
                        for (int i = 1; i < 3; i++)
                        {

                            permdetail.UP_Permission_Id = i;
                            permdetail.UP_User_Id = Userid.Id.ToString();
                            objEntity.User_Permission_Detail.Add(permdetail);
                            objEntity.SaveChanges();
                        }

                        Profile prof = new Profile();

                        prof.Email = model.Email;

                        prof.UserID = Userid.Id.ToString();
                        prof.UserStatus = "Y";

                        objEntity.Profiles.Add(prof);


                     

                        objEntity.SaveChanges();


                        using (EventComboEntities db = new EventComboEntities())
                        {
                            AspNetUser aspuser = db.AspNetUsers.First(i => i.Id == Userid.Id);

                            aspuser.LoginStatus = "Y";
                         
                            db.SaveChanges();

                        }
                        Session["AppId"] = Userid.Id;




                        /// to send email////
                        /// 
                        string to = "", from ="",cc="",bcc="",subjectn="";
                        var bodyn = "";
                        List<Email_Tag> EmailTag = new List<Email_Tag>();
                         EmailTag = getTag();
                       
                        var Emailtemplate = getEmail("email_welcome");
                        if (!string.IsNullOrEmpty(Emailtemplate.To))
                        {


                            to = Emailtemplate.To;
                            if (to.Contains("¶¶UserEmailID¶¶"))
                            {
                                to = to.Replace("¶¶UserEmailID¶¶", model.Email);

                            }
                        }
                        if (!(string.IsNullOrEmpty(Emailtemplate.From)))
                         {
                            from = Emailtemplate.From;
                            if (from.Contains("¶¶UserEmailID¶¶"))
                            {
                                from = from.Replace("¶¶UserEmailID¶¶", model.Email);

                            }
                        }
                        else
                        {
                            from = "shweta.sindhu@kiwitech.com";

                        }


                        if (!(string.IsNullOrEmpty(Emailtemplate.CC)))
                        {
                            cc = Emailtemplate.CC;
                            if (cc.Contains("¶¶UserEmailID¶¶"))
                            {
                                cc = cc.Replace("¶¶UserEmailID¶¶", model.Email);

                            }
                        }

                        if (!(string.IsNullOrEmpty(Emailtemplate.Bcc)))
                        {
                            bcc = Emailtemplate.Bcc;
                            if (bcc.Contains("¶¶UserEmailID¶¶"))
                            {
                                bcc = bcc.Replace("¶¶UserEmailID¶¶", model.Email);

                            }
                        }
                        if (!string.IsNullOrEmpty(Emailtemplate.Subject))
                        {


                            subjectn = Emailtemplate.Subject;

                            for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
                            {

                                if (subjectn.Contains("¶¶" + EmailTag[i].Tag_Name.Trim() + "¶¶"))
                                {
                                    if (EmailTag[i].Tag_Name == "UserEmailID")
                                    {
                                        subjectn= subjectn.Replace("¶¶UserEmailID¶¶", model.Email);

                                    }

                                }

                            }
                        }
                        if (!string.IsNullOrEmpty(Emailtemplate.TemplateHtml))
                        {
                            bodyn =  new  MvcHtmlString(HttpUtility.HtmlDecode(Emailtemplate.TemplateHtml)).ToHtmlString();
                            for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
                            {

                                if (bodyn.Contains("¶¶" + EmailTag[i].Tag_Name.Trim() + "¶¶"))
                                {
                                    if (EmailTag[i].Tag_Name == "UserEmailID")
                                    {
                                        bodyn= bodyn.Replace("¶¶UserEmailID¶¶", model.Email);

                                    }

                                }

                            }
                        }
                        SendHtmlFormattedEmail(to, from, subjectn, bodyn,cc,bcc);


                    }
                    //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                  //  await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToLocal(url);

                }
                AddErrors(result);
            }
           
            return RedirectToAction("Index", "Home");
        }

        public void SendHtmlFormattedEmail(string To,string from, string subject, string body,string cc,string bcc)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(from, from);
                mailMessage.Subject = subject;
                mailMessage.Body = body;    
                if (!string.IsNullOrEmpty(cc))
                {
                    mailMessage.CC.Add(cc);
                }
                if (!string.IsNullOrEmpty(bcc))
                {
                    mailMessage.Bcc.Add(bcc);
                }
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(To));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["Host"];
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
                NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                smtp.Send(mailMessage);
            }
        }
        public void SendHtmlFormattedEmail(string To, string from, string subject, string body, string cc, string bcc,MemoryStream attachment)
        {
            MailMessage mailMessage = new MailMessage();
            
                mailMessage.From = new MailAddress(from, from);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                if (!string.IsNullOrEmpty(cc))
                {
                    mailMessage.CC.Add(cc);
                }
                if (!string.IsNullOrEmpty(bcc))
                {
                    mailMessage.Bcc.Add(bcc);
                }
                if (attachment.Length != 0)
                {
                    System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(attachment, ct);
                    attach.ContentDisposition.FileName = "Ticket_EventCombo.pdf";
                    mailMessage.Attachments.Add(attach);
                }
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(To));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["Host"];
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
                NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                smtp.Send(mailMessage);
            
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }
  

}