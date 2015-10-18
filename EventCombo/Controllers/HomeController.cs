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
           
            return View();

        }
        public ActionResult GetBuzz()
        {
            ViewBag.ReturnUrl = "~/Home/GetBuzz";
            return View();

        }
        public ActionResult EventOraganizer()
        {
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
          
            if (model.Password!=model .ConfirmPassword)
            {
                ViewData["Error"] = "Password and confirm password doesn't match!";
                ModelState.AddModelError("Error", "Password and confirm password doesn't match!");

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
                ViewData["Message"] = "Password reset successfully.";
                return View();
            }
            AddErrors(result);
            return View();

        }
        [AllowAnonymous]
       public ActionResult ForgetPassword()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(ForgetPassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = UserManager.FindByEmail(model.Email);
            string id = user.Id;
            string readFile = "";
            using (StreamReader reader = new StreamReader(Server.MapPath("~/ForgotPassword.html")))
            {
                readFile = reader.ReadToEnd();
            }
        
            string myString = "";
            myString = readFile;
            myString = myString.Replace("$$Email$$", model.Email);
            myString = myString.Replace("$$Website$$", "http://eventcombo.kiwireader.com/Home/PasswordReset?code=" + id + "'");

            SendMail(model.Email, myString.ToString(), "The Eventcombo Team");

            ViewData["Message"] = "Please check your email for password set link!!";
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
            Session["ReturnUrl"] =Url.Action("EventCreation", "Event");
            if (Session["AppId"] == null)
            {
                return "Y";

            }
            else
            {
                return "N";

            }
                }



        private string getusername()
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Signup(LoginViewModel model)
        {
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

                        objEntity.Profiles.Add(prof);
                        objEntity.SaveChanges();

                    }
                    Session["AppId"] = Userid.Id;


                    /// to send email////
                    /// 
                    var fromAddress = new MailAddress("shweta.sindhu@kiwitech.com", "Eventcombo");
                    var toAddress = new MailAddress(model.Email);
                    const string fromPassword = "Shweta1989";
                    const string address = "shweta.sindhu@kiwitech.com";
                    const string subject = "Thanku You";
                    const string body = "Confirmation Message";

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(address, fromPassword)
                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");

                }
                AddErrors(result);
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