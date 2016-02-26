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
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace EventCombo.Controllers
{
    [Authorize]
   
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        EventComboEntities db = new EventComboEntities();

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }


        public ActionResult CheckExistingEmail(string Email)
        {
            bool ifEmailExist = false;
            try
            {
                var Userid = UserManager.FindByEmail(Email);
                ifEmailExist = Email.Equals(Userid.Email) ? true : false;
                return Json(!ifEmailExist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult PurchasedTicket()
        {

            return View();
        }
        [HttpGet]
        public ActionResult PurchasedDeals()
        {

            return View();
        }
        [HttpGet]
        public ActionResult PaymentInformation()
        {
            return View();

        }
        [HttpGet]
        public ActionResult LiveEvents()
        {
            return View();

        }

        [HttpGet]
        public ActionResult PastEvents()
        {
            return View();

        }

        [HttpGet]
        public ActionResult EventHelp()
        {
            return View();

        }

        [HttpGet]
        public ActionResult OrganizerProfile()
        {
            return View();

        }
        [HttpGet]
        public ActionResult DealsDashboard()
        {

            return View();
        }
        [HttpGet]
        public ActionResult CurrentDeals()
        {

            return View();
        }
        [HttpGet]
        public ActionResult PastDeals()
        {

            return View();
        }
        [HttpGet]
        public ActionResult Feedback()
        {

            return View();
        }
        [HttpGet]
        public ActionResult Overview()
        {

            return View();
        }
        [HttpGet]
        public ActionResult ImpactReport()
        {

            return View();
        }
        [HttpGet]
        public ActionResult InvoiceHistory()
        {

            return View();
        }
        [HttpGet]
        public ActionResult Customers()
        {

            return View();
        }
        [HttpGet]
        public ActionResult DealsHelp()
        {

            return View();
        }
        [HttpGet]
        public ActionResult MerchantProfile()
        {

            return View();
        }

        public string getusername()
        {
            if ((Session["AppId"] != null))
            {
                string userid = Session["AppId"].ToString();
                var userEmail = db.AspNetUsers.Where(x => x.Id == userid).Select(y => y.Email).SingleOrDefault();
                if (userEmail != null)
                {
                    return userEmail.Substring(0, userEmail.IndexOf("@") + 1);
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
        [HttpGet]
    
        [Authorize]
        public ActionResult MyAccount()
        {
            string defaultCountry = "";
            HomeController hmc = new HomeController();
            hmc.ControllerContext = new ControllerContext(this.Request.RequestContext, hmc);
            Session["logo"] = "account";
            Session["Fromname"] = "account";
            string city = "", state = "", zipcode = "", country = "";
            if ((Session["AppId"] != null))
            {
              string usernme=  hmc.getusername();
                if(string.IsNullOrEmpty(usernme))
                {
                    return RedirectToAction("Index", "Home");
                }

                try {
                    using (WebClient client = new WebClient())
                    {
                        string ip = GetLanIPAddress().Replace("::ffff:", "");
                        var json = client.DownloadString("http://freegeoip.net/json/" + ip + "");
                        dynamic stuff = JsonConvert.DeserializeObject(json);
                        if (stuff != null)
                        {
                            city = stuff.city;
                            state = stuff.region_name;
                            zipcode = stuff.zip_code;
                            country = stuff.country_name;
                        }
                        else
                        {
                            city = "";
                            state = "";
                            zipcode = "";
                            country = "";

                        }
                    }
                }
                catch (Exception ex)
                {
                    city = "";
                    state = "";
                    zipcode = "";
                    country = "";

                }



                string userid = Session["AppId"].ToString();
                myAccount myacc = new myAccount();
                var accountdetail = GetLoginDetails(userid);
                if (accountdetail != null)
                {

                    myacc = accountdetail;
                }
                if (myacc != null)
                {

                    var x = new
                    {
                        type = myacc.contentype,
                        Name = myacc.UserProfileImage,
                        path = "a.jpg"
                    };

                    if (string.IsNullOrEmpty(myacc.Dateofbirth))
                    {
                        myacc.day = 1;
                        myacc.month = 1;
                        myacc.year = 1980;
                    }
                    else
                    {
                        string[] day = myacc.Dateofbirth.Split('-');

                        myacc.day = int.Parse(day[0].ToString());
                        myacc.month = int.Parse(day[1].ToString());
                        myacc.year = int.Parse(day[2].ToString());


                    }

                    if (string.IsNullOrEmpty(myacc.City))
                    {

                        if (!string.IsNullOrEmpty(city))
                        {
                            myacc.City = city;
                        }
                        else
                        {
                            myacc.City = "";

                        }


                    }

                    if (string.IsNullOrEmpty(myacc.State))
                    {
                        if (string.IsNullOrEmpty(state))
                        {
                            myacc.State = "";
                        }
                        else
                        {
                            myacc.State = state;
                        }
                    }

                    if (string.IsNullOrEmpty(myacc.Zip))
                    {
                        if (string.IsNullOrEmpty(zipcode))
                        {
                            myacc.Zip = "";
                        }
                        else
                        {
                            myacc.Zip = zipcode;
                        }
                    }
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    var results = js.Serialize(x);



                    if (string.IsNullOrEmpty(myacc.UserProfileImage))
                    {
                        myacc.editsave = "Save";
                        myacc.UserProfileImage = "image-drop2.gif";
                    }
                    else
                    {

                        myacc.ImagePath = "/Images/Profile/Profile_Images/imagepath/" + myacc.UserProfileImage;
                        myacc.editsave = "Edit";

                    }
                }
                else
                {
                    myacc = new myAccount();

                    myacc.editsave = "Save";
                    myacc.UserProfileImage = "image-drop2.gif";
                    myacc.WebsiteURL = "";
                    myacc.Password = "";

                }

                var countryQuery = (from c in db.Countries
                                    orderby c.Country1 ascending
                                    select c).Distinct();
                List<SelectListItem> countryList = new List<SelectListItem>();
                if (myacc != null)
                {
                    if (string.IsNullOrEmpty(myacc.Country))
                    {

                        defaultCountry = "United States";
                    }
                    else
                    {
                        defaultCountry = myacc.Country;

                    }


                }
                else
                {
                    if (!string.IsNullOrEmpty(country))
                    {

                        defaultCountry = "United States";
                    }
                    else
                    {
                        defaultCountry = country;

                    }

                }
                foreach (var item in countryQuery)
                {
                    countryList.Add(new SelectListItem()
                    {
                        Text = item.Country1,
                        Value = item.CountryID.ToString(),
                        Selected = (item.CountryID.ToString().Trim() == defaultCountry.Trim() ? true : false)
                    });
                }
                ViewBag.Country = countryList;

                return View(myacc);


            }
            else {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MyAccount(myAccount model, HttpPostedFileBase file)
        {
            string msg = "", errormessage = "", successmsg = "";
            int emailchange = 0,Pwdchange=0,mainchange=1;
            string to = "", from = "", cc = "", bcc = "", subjectn = "", emailname="";
            var bodyn = "";
            var Emailtemplate =new Email_Template();
            List<Email_Tag> EmailTag = new List<Email_Tag>();
            HomeController hmc = new HomeController();
            var validationresult = "";
            if (Session["AppId"] != null)
            {
                string Userid = Session["AppId"].ToString();
                string imagepresent = model.ImagePresent;

                ValidationMessageController vmc = new ValidationMessageController();
                validationresult = vmc.Index("", "");
                var accountdetail = GetLoginDetails(Userid);
                //check validation
                if (string.IsNullOrEmpty(model.Firstname))
                {
                    validationresult = vmc.Index("MyAccount", "MyAccountFnameRequiredUI");
                    ModelState.AddModelError("Error", validationresult);

                }
                if (!string.IsNullOrEmpty(model.ConfirmEmail))
                {
                    if (model.Email != model.ConfirmEmail)
                    {
                        validationresult = vmc.Index("MyAccount", "MyAccountEmailmatchValidationSy");
                        ModelState.AddModelError("Error", validationresult);

                    }
                }
                else
                {
                    if(model.Email !=accountdetail.Email )
                    {
                        validationresult = vmc.Index("MyAccount", "MyAccountEmailValidationUI");
                        ModelState.AddModelError("Error", validationresult);
                    }

                }
                if (!string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.ConfirmPassword))
                {
                    if (model.NewPassword != model.ConfirmPassword)
                    {
                        validationresult = vmc.Index("MyAccount", "MyAccountPwdmatchValidationSy");
                        ModelState.AddModelError("Error", validationresult);


                    }
                }
                if (!string.IsNullOrEmpty(model.ConfirmEmail) && !string.IsNullOrEmpty(model.Email) && string.IsNullOrEmpty(model.Password))
                {
                    validationresult = vmc.Index("MyAccount", "MyAccountPwdValidationSys");
                    ModelState.AddModelError("Error", validationresult);

                }
                    if (!string.IsNullOrEmpty(model.Password))
                   {

                    var user12 = UserManager.FindByEmail(accountdetail.PreviousEmail);
                    var result = UserManager.PasswordHasher.VerifyHashedPassword(user12.PasswordHash, model.Password);
              
                    if (result.ToString() != "Success")
                    {
                        validationresult = vmc.Index("MyAccount", "MyAccountPwdValidationSys");
                        ModelState.AddModelError("Error", validationresult);

                    }
                }

                if (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.ConfirmEmail))
                {
                    if (model.PreviousEmail != model.Email)
                    {
                        var user = UserManager.FindByEmail(model.Email);
                        if (user != null)
                        {
                            validationresult = vmc.Index("MyAccount", "MyAccountEmailAlreadyExistSY");
                            ModelState.AddModelError("Error", validationresult);
                        }
                    }
                }


                var userimage = model.userimage;

          

                if (ModelState.IsValid)
                {

                    using (EventComboEntities objEntity = new EventComboEntities())
                    {
                       // var ApplicationUser = objEntity.AspNetUsers.Find(Userid);
                        Profile profile = objEntity.Profiles.First(i => i.UserID == Userid);

                        if (userimage != null)
                        {
                            string[] images = userimage.Split('¶');
                            profile.UserProfileImage = images[0];
                            profile.ContentType = images[1];
                            model.UserProfileImage = images[0];
                            model.contentype = images[1];
                            model.ImagePath = "/Images/Profile/Profile_Images/imagepath/" + images[0];
                            model.editsave = "Edit";
                        }
                        else
                        {
                            model.editsave = "Save";
                            model.UserProfileImage = "image-drop2.gif";
                            profile.UserProfileImage = "";
                            profile.ContentType = "";
                        }
                        profile.FirstName = model.Firstname;
                        profile.LastName = model.Lastname;
                        profile.StreetAddressLine1 = model.StreetAddress1;
                        profile.StreetAddressLine2 = model.StreeAddress2;
                        profile.City = model.City;
                        profile.State = model.State;
                        profile.Zip = model.Zip;
                        profile.CountryID = byte.Parse(model.Country);
                        profile.MainPhone = model.MainPhone;
                        profile.SecondPhone = model.SecondPhone;
                        profile.WorkPhone = model.WorkPhone;
                        profile.WebsiteURL = model.WebsiteURL;
                        profile.Gender = model.Gender;
                        if (!string.IsNullOrEmpty(model.day.ToString()) && !string.IsNullOrEmpty(model.month.ToString()) && !string.IsNullOrEmpty(model.year.ToString()))
                        {
                            profile.DateofBirth = model.day.ToString() + "-" + model.month.ToString() + "-" + model.year.ToString();
                        }
                    if (!checkexternallogin(Userid))
                        {
                            if (!string.IsNullOrEmpty(model.ConfirmEmail) && !string.IsNullOrEmpty(model.Email) && model.PreviousEmail != model.Email)
                            {
                                profile.Email = model.Email;

                               

                            }
                        }
                        try
                        {
                            objEntity.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            string message = ex.Message;

                        }

                    }
                    using (EventComboEntities objEntity = new EventComboEntities())
                    {
                        AspNetUser aspuser = objEntity.AspNetUsers.First(i => i.Id == Userid);
                        if (!checkexternallogin(Userid))
                        {
                            if (!string.IsNullOrEmpty(model.ConfirmEmail) && !string.IsNullOrEmpty(model.Email) && model.PreviousEmail != model.Email)
                            {
                                aspuser.Email = model.Email;
                                aspuser.UserName = model.Email;


                                try
                                {
                                    objEntity.SaveChanges();
                                    emailchange = 1;
                                }
                                catch (Exception ex)
                                {
                                    string message = ex.Message;

                                }
                            }
                        }
                    }



                    if (!string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.ConfirmPassword))
                    {
                        var token = await UserManager.GeneratePasswordResetTokenAsync(Userid);

                        var result = await UserManager.ResetPasswordAsync(Userid, token, model.NewPassword);
                          Pwdchange = 1;
                     }
                    var email = "";
                    using (EventComboEntities objEntity = new EventComboEntities())
                    {
                        AspNetUser aspuser = objEntity.AspNetUsers.First(i => i.Id == Userid);
                        email = aspuser.Email;
                    }
                    string tag = "UserEmailID:" + email + "¶UserFirstNameID:" + model.Firstname;
                    if (emailchange==1 && Pwdchange==0)
                    {
                        successmsg =  vmc.Index("MyAccount", "MyAccountSuccessEmailSY");
                        
                       Emailtemplate = hmc.getEmail("acc_info_update");

                      
                        if (Emailtemplate != null)
                        {
                            if (!string.IsNullOrEmpty(Emailtemplate.To))
                            {


                                to = Emailtemplate.To;
                                if (to.Contains("¶¶UserEmailID¶¶"))
                                {
                                    to = to.Replace("¶¶UserEmailID¶¶", email);

                                }
                            }
                            if (!(string.IsNullOrEmpty(Emailtemplate.From)))
                            {
                                from = Emailtemplate.From;
                                if (from.Contains("¶¶UserEmailID¶¶"))
                                {
                                    from = from.Replace("¶¶UserEmailID¶¶", email);

                                }
                            }
                            else
                            {
                                from = "shweta.sindhu@kiwitech.com";

                            }

                            if (!(string.IsNullOrEmpty(Emailtemplate.From_Name)))
                            {
                                emailname = Emailtemplate.From_Name;
                            }
                            else
                            {
                                emailname = from;
                            }
                            if (!(string.IsNullOrEmpty(Emailtemplate.CC)))
                            {
                                cc = Emailtemplate.CC;
                                if (cc.Contains("¶¶UserEmailID¶¶"))
                                {
                                    cc = cc.Replace("¶¶UserEmailID¶¶", email);

                                }
                            }
                            if (!(string.IsNullOrEmpty(Emailtemplate.Bcc)))
                            {
                                bcc = Emailtemplate.Bcc;
                                if (bcc.Contains("¶¶UserEmailID¶¶"))
                                {
                                    bcc = bcc.Replace("¶¶UserEmailID¶¶", email);

                                }
                            }
                            if (!string.IsNullOrEmpty(Emailtemplate.Subject))
                            {


                                subjectn = Emailtemplate.Subject;


                            }
                            if (!string.IsNullOrEmpty(Emailtemplate.TemplateHtml))
                            {
                                bodyn = new MvcHtmlString(HttpUtility.HtmlDecode(Emailtemplate.TemplateHtml)).ToHtmlString();

                            }
                            hmc.SendHtmlFormattedEmail(to, from, subjectn, bodyn, cc, bcc, tag, emailname);
                        }
                    }
                    if (emailchange == 1 && Pwdchange == 1)
                    {
                        successmsg =  vmc.Index("MyAccount", "AccEmailPwdchangesys");

                        Emailtemplate = hmc.getEmail("acc_pwd_set");
                        if (Emailtemplate != null)
                        {
                            if (!string.IsNullOrEmpty(Emailtemplate.To))
                            {
                                to = Emailtemplate.To;
                                if (to.Contains("¶¶UserEmailID¶¶"))
                                {
                                    to = to.Replace("¶¶UserEmailID¶¶", email);

                                }
                            }
                            if (!(string.IsNullOrEmpty(Emailtemplate.From)))
                            {
                                from = Emailtemplate.From;
                                if (from.Contains("¶¶UserEmailID¶¶"))
                                {
                                    from = from.Replace("¶¶UserEmailID¶¶", email);

                                }
                            }
                            else
                            {
                                from = "shweta.sindhu@kiwitech.com";

                            }
                            if (!(string.IsNullOrEmpty(Emailtemplate.From_Name)))
                            {
                                emailname = Emailtemplate.From_Name;
                            }
                            else
                            {
                                emailname = from;
                            }
                            if (!(string.IsNullOrEmpty(Emailtemplate.CC)))
                            {
                                cc = Emailtemplate.CC;
                                if (cc.Contains("¶¶UserEmailID¶¶"))
                                {
                                    cc = from.Replace("¶¶UserEmailID¶¶", email);

                                }
                            }
                            if (!(string.IsNullOrEmpty(Emailtemplate.Bcc)))
                            {
                                bcc = Emailtemplate.Bcc;
                                if (bcc.Contains("¶¶UserEmailID¶¶"))
                                {
                                    bcc = from.Replace("¶¶UserEmailID¶¶", email);

                                }
                            }
                            if (!string.IsNullOrEmpty(Emailtemplate.Subject))
                            {


                                subjectn = Emailtemplate.Subject;


                            }
                            if (!string.IsNullOrEmpty(Emailtemplate.TemplateHtml))
                            {
                                bodyn = new MvcHtmlString(HttpUtility.HtmlDecode(Emailtemplate.TemplateHtml)).ToHtmlString();

                            }
                            hmc.SendHtmlFormattedEmail(to, from, subjectn, bodyn, cc, bcc, tag, emailname);
                        }
                    }
                    if (emailchange == 0 && Pwdchange == 1)
                    {
                        successmsg =  vmc.Index("MyAccount", "MyAccountSuccessPasswordSY") + successmsg;
                        Emailtemplate = hmc.getEmail("acc_pwd_set");
                        if (Emailtemplate != null)
                        {
                            if (!string.IsNullOrEmpty(Emailtemplate.To))
                            {
                                to = Emailtemplate.To;
                                if (to.Contains("¶¶UserEmailID¶¶"))
                                {
                                    to = to.Replace("¶¶UserEmailID¶¶", email);

                                }
                            }
                            if (!(string.IsNullOrEmpty(Emailtemplate.From)))
                            {
                                from = Emailtemplate.From;
                                if (from.Contains("¶¶UserEmailID¶¶"))
                                {
                                    from = from.Replace("¶¶UserEmailID¶¶", email);

                                }
                            }
                            else
                            {
                                from = "shweta.sindhu@kiwitech.com";

                            }
                            if (!(string.IsNullOrEmpty(Emailtemplate.From_Name)))
                            {
                                emailname = Emailtemplate.From_Name;
                            }
                            else
                            {
                                emailname = from;
                            }
                            if (!(string.IsNullOrEmpty(Emailtemplate.CC)))
                            {
                                cc = Emailtemplate.CC;
                                if (cc.Contains("¶¶UserEmailID¶¶"))
                                {
                                    cc = from.Replace("¶¶UserEmailID¶¶", email);

                                }
                            }
                            if (!(string.IsNullOrEmpty(Emailtemplate.Bcc)))
                            {
                                bcc = Emailtemplate.Bcc;
                                if (bcc.Contains("¶¶UserEmailID¶¶"))
                                {
                                    bcc = from.Replace("¶¶UserEmailID¶¶", email);

                                }
                            }
                            if (!string.IsNullOrEmpty(Emailtemplate.Subject))
                            {


                                subjectn = Emailtemplate.Subject;


                            }
                            if (!string.IsNullOrEmpty(Emailtemplate.TemplateHtml))
                            {
                                bodyn = new MvcHtmlString(HttpUtility.HtmlDecode(Emailtemplate.TemplateHtml)).ToHtmlString();

                            }
                            hmc.SendHtmlFormattedEmail(to, from, subjectn, bodyn, cc, bcc, tag, emailname);
                        }
                    }
                    if (emailchange == 0 && Pwdchange == 0)
                    {
                        successmsg = vmc.Index("MyAccount", "MyAccountSuccessInitSY");
                        Emailtemplate = hmc.getEmail("acc_info_update");


                        if (Emailtemplate != null)
                        {
                            if (!string.IsNullOrEmpty(Emailtemplate.To))
                            {


                                to = Emailtemplate.To;
                                if (to.Contains("¶¶UserEmailID¶¶"))
                                {
                                    to = to.Replace("¶¶UserEmailID¶¶", email);

                                }
                            }
                            if (!(string.IsNullOrEmpty(Emailtemplate.From)))
                            {
                                from = Emailtemplate.From;
                                if (from.Contains("¶¶UserEmailID¶¶"))
                                {
                                    from = from.Replace("¶¶UserEmailID¶¶", email);

                                }
                            }
                            else
                            {
                                from = "shweta.sindhu@kiwitech.com";

                            }
                            if (!(string.IsNullOrEmpty(Emailtemplate.From_Name)))
                            {
                                emailname = Emailtemplate.From_Name;
                            }
                            else
                            {
                                emailname = from;
                            }
                            if (!(string.IsNullOrEmpty(Emailtemplate.CC)))
                            {
                                cc = Emailtemplate.CC;
                                if (cc.Contains("¶¶UserEmailID¶¶"))
                                {
                                    cc = cc.Replace("¶¶UserEmailID¶¶", email);

                                }
                            }
                            if (!(string.IsNullOrEmpty(Emailtemplate.Bcc)))
                            {
                                bcc = Emailtemplate.Bcc;
                                if (bcc.Contains("¶¶UserEmailID¶¶"))
                                {
                                    bcc = bcc.Replace("¶¶UserEmailID¶¶", email);

                                }
                            }
                            if (!string.IsNullOrEmpty(Emailtemplate.Subject))
                            {


                                subjectn = Emailtemplate.Subject;


                            }
                            if (!string.IsNullOrEmpty(Emailtemplate.TemplateHtml))
                            {
                                bodyn = new MvcHtmlString(HttpUtility.HtmlDecode(Emailtemplate.TemplateHtml)).ToHtmlString();

                            }
                            hmc.SendHtmlFormattedEmail(to, from, subjectn, bodyn, cc, bcc, tag, emailname);
                        }
                    }


                    var countryQuery = (from c in db.Countries
                                        orderby c.Country1 ascending
                                        select c).Distinct();
                    List<SelectListItem> countryList = new List<SelectListItem>();
                    string defaultCountry = model.Country;
                    foreach (var item in countryQuery)
                    {
                        countryList.Add(new SelectListItem()
                        {
                            Text = item.Country1,
                            Value = item.CountryID.ToString(),
                            Selected = (item.CountryID.ToString().Trim() == defaultCountry.Trim() ? true : false)
                        });
                    }
                    ViewBag.Country = countryList;



                    ViewData["Message"] = successmsg;
                    return View(model);

                }



                var countryQuery12 = (from c in db.Countries
                                      orderby c.Country1 ascending
                                      select c).Distinct();
                List<SelectListItem> countryList12 = new List<SelectListItem>();
                string defaultCountry12 = model.Country;
                foreach (var item in countryQuery12)
                {
                    countryList12.Add(new SelectListItem()
                    {
                        Text = item.Country1,
                        Value = item.CountryID.ToString(),
                        Selected = (item.CountryID.ToString().Trim() == defaultCountry12.Trim() ? true : false)
                    });
                }
                ViewBag.Country = countryList12;
                if (string.IsNullOrEmpty(accountdetail.UserProfileImage))
                {
                    model.editsave = "Save";
                    model.UserProfileImage = "image-drop2.gif";
                }
                else
                {
                    model.UserProfileImage = accountdetail.UserProfileImage;
                    model.contentype = accountdetail.contentype;
                    model.ImagePath = "/Images/Profile/Profile_Images/imagepath/" + accountdetail.UserProfileImage;
                    model.editsave = "Edit";

                }

                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }

        }
        public ActionResult SaveUploadedFile()
        {
            if (Session["AppId"] != null)
            {
                Random rnd = new Random();
                var pathnew = "";
                var rndnumber = rnd.Next(1, 7);
                //string Name = Request.Form[1];
                //if (Request.Files.Count > 0)
                //{
                //    HttpPostedFileBase file = Request.Files[0];
                //}
                string Userid = Session["AppId"].ToString();
                bool isSavedSuccessfully = true;
                string fName = "";
                var NFilename = "";
                string content_type = "";
                try
                {
                    foreach (string fileName in Request.Files)
                    {
                        HttpPostedFileBase file = Request.Files[fileName];
                        //Save file content goes here
                        fName = file.FileName;
                        if (file != null && file.ContentLength > 0)
                        {
                            fName = file.FileName;
                            content_type = file.ContentType;
                            var originalDirectory = new DirectoryInfo(string.Format("{0}\\Images\\Profile\\Profile_Images", Server.MapPath(@"\")));

                            string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                            var fileName1 = Path.GetFileName(file.FileName);

                            //var format=    getImageFormat(fileName1);

                            bool isExists = System.IO.Directory.Exists(pathString);

                            if (!isExists)
                                System.IO.Directory.CreateDirectory(pathString);

                            var path = string.Format("{0}\\{1}", pathString, file.FileName);
                            var imageformat = getImageFormat(path);
                             NFilename = Userid.Trim() + "_ProfImage" + rndnumber + "." + imageformat;
                             pathnew = string.Format("{0}\\{1}", pathString, NFilename);
                            //using (EventComboEntities objEntity = new EventComboEntities())
                            //{
                            //    Profile profile = objEntity.Profiles.First(i => i.UserID == Userid);
                            //    profile.UserProfileImage = NFilename;
                            //    //profile.UserProfileImage = fName;
                            //    profile.ContentType = content_type;
                            //    objEntity.SaveChanges();
                            //}
                            // file.SaveAs(path);
                            HandleImageUpload(file, pathnew);
                        }

                    }

                }
                catch (Exception ex)
                {
                    isSavedSuccessfully = false;
                }


                if (isSavedSuccessfully)
                {
                    return Json(new { image_name = NFilename, image_type = content_type, image_path = pathnew });
                }
                else
                {
                    return Json(new { Message = "Error in saving file" });
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }
        protected byte[] getResizedImage(HttpPostedFileBase file, int width, int height)
        {
            System.IO.MemoryStream outStream = new System.IO.MemoryStream();
            try
            {
                Image imgIn = Image.FromStream(file.InputStream);
                string path = file.FileName;
                //Bitmap imgIn = new Bitmap(path);
                double y = imgIn.Height;
                double x = imgIn.Width;
                double factor = 1;
                if (width > 0)
                {
                    factor = width / x;
                }
                else if (height > 0)
                {
                    factor = height / y;
                }

                Bitmap imgOut = new Bitmap((int)(x * factor), (int)(y * factor));

                // Set DPI of image (xDpi, yDpi)
                imgOut.SetResolution(72, 72);
                Graphics g = Graphics.FromImage(imgOut);
                g.Clear(Color.White);
                g.DrawImage(imgIn, new Rectangle(0, 0, (int)(factor * x), (int)(factor * y)),
                  new Rectangle(0, 0, (int)x, (int)y), GraphicsUnit.Pixel);
                imgOut.Save(outStream, getImageFormat(path));

            }
            catch (Exception ex)
            {

            }
            return outStream.ToArray();
        }

        protected string getContentType(String path)
        {
            switch (Path.GetExtension(path))
            {
                case ".bmp": return "Image/bmp";
                case ".gif": return "Image/gif";
                case ".jpg": return "Image/jpeg";
                case ".png": return "Image/png";
                default: break;
            }
            return "";
        }

        protected ImageFormat getImageFormat(String path)
        {
            switch (Path.GetExtension(path))
            {
                case ".bmp": return ImageFormat.Bmp;
                case ".gif": return ImageFormat.Gif;
                case ".jpg": return ImageFormat.Jpeg;
                case ".png": return ImageFormat.Png;
                default: break;
            }
            return ImageFormat.Jpeg;
        }
        private Image RezizeImage(Image img, int maxWidth, int maxHeight)
        {
            if (img.Height < maxHeight && img.Width < maxWidth) return img;
            using (img)
            {
                Double xRatio = (double)img.Width / maxWidth;
                Double yRatio = (double)img.Height / maxHeight;
                Double ratio = Math.Max(xRatio, yRatio);
                int nnx = (int)Math.Floor(img.Width / ratio);
                int nny = (int)Math.Floor(img.Height / ratio);
                Bitmap cpy = new Bitmap(nnx, nny, PixelFormat.Format32bppArgb);
                cpy.SetResolution(72, 72);
                using (Graphics gr = Graphics.FromImage(cpy))
                {
                    gr.Clear(Color.Transparent);

                    // This is said to give best quality when resizing images
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    gr.DrawImage(img,
                        new Rectangle(0, 0, nnx, nny),
                        new Rectangle(0, 0, img.Width, img.Height),
                        GraphicsUnit.Pixel);
                }
                return cpy;
            }

        }

        private MemoryStream BytearrayToStream(byte[] arr)
        {
            return new MemoryStream(arr, 0, arr.Length);
        }

        private void HandleImageUpload(HttpPostedFileBase file, string path1)
        {//ProfileID_SequentialImage#
            Image img = RezizeImage(Image.FromStream(file.InputStream), 200, 200);
            string path = file.FileName;
            img.Save(path1, getImageFormat(path));
        }
      

        public myAccount GetLoginDetails(string userid)
        {

            using (EventComboEntities objEntity = new EventComboEntities())
            {
                var modelmyaccount = (from cpd in objEntity.AspNetUsers
                                      join pfd in objEntity.Profiles
                                      on cpd.Id equals pfd.UserID
                                      where cpd.Id == userid
                                      select new myAccount
                                      {
                                          Id = cpd.Id,
                                          Firstname = pfd.FirstName,
                                          Lastname = pfd.LastName,
                                          StreetAddress1 = pfd.StreetAddressLine1,
                                          StreeAddress2 = pfd.StreetAddressLine2,
                                          City = pfd.City,
                                          State = pfd.State,
                                          Zip = pfd.Zip,
                                          Country = pfd.CountryID.ToString(),
                                          MainPhone = pfd.MainPhone,
                                          SecondPhone = pfd.SecondPhone,
                                          WebsiteURL = pfd.WebsiteURL,
                                          UserProfileImage = pfd.UserProfileImage,
                                          Email = cpd.Email,
                                          Password = cpd.PasswordHash,
                                          contentype = pfd.ContentType,
                                          Dateofbirth = pfd.DateofBirth,
                                          Gender = pfd.Gender,
                                          PreviousEmail = cpd.Email,
                                          SendLatestdetails=pfd.SendCur_EventDetail
                                       
                                      });
                return modelmyaccount.FirstOrDefault();

            }
        }

        public bool checkexternallogin(string userid)
        {
            using (EventComboEntities objEntity = new EventComboEntities())
            {
                var modelmyaccount = (from cpd in objEntity.AspNetUsers
                                      join pfd in objEntity.AspNetUserLogins
                                      on cpd.Id equals pfd.UserId
                                      where cpd.Id == userid
                                      select cpd);
                if (modelmyaccount.FirstOrDefault() != null)
                {
                    return true;
                }
                else
                {
                    return false;

                }

            }

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

        //
        // GET: /Account/Login
        //[AllowAnonymous]
        //public ActionResult Login(string returnUrl)
        //{
        //    ViewBag.ReturnUrl = returnUrl;
        //    return View();
        //}

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
       
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {

          


            string url = null, city="", state="", zipcode="", country="";
        if(Session["ReturnUrl"]!=null)
            {
                url = Session["ReturnUrl"].ToString();
                if(url.Contains("~"))
                {
                    string[] urlnew = url.Split('~');
                    url = urlnew[1];
                }
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

            if (returnUrl != null && returnUrl.Trim() != "") result = SignInStatus.Success;


            switch (result)
            {
                case SignInStatus.Success:
                    var User = UserManager.FindByEmail(model.Email.ToString());

                    //var roleMemeber = (from r in db.AspNetRoles where r.Name.Contains("Member") select r).FirstOrDefault();
                    //var users = db.AspNetUsers.Where(x => x.AspNetRoles.Select(y => y.Id).Contains(roleMemeber.Id)).ToList();
                    //if (users.Find(x => x.Id == User.Id) != null)
                    //{
                    try
                    {
                        using (WebClient client = new WebClient())
                        {
                            string ip = GetLanIPAddress().Replace("::ffff:", "");


                            var json = client.DownloadString("http://freegeoip.net/json/" + ip + "");
                            dynamic stuff = JsonConvert.DeserializeObject(json);
                            if (stuff != null)
                            {
                                city = stuff.city;
                                state = stuff.region_name;
                                zipcode = stuff.zip_code;
                                country = stuff.country_name;
                            }
                            else
                            {
                                city = "";
                                state = "";
                                zipcode = "";
                                country = "";

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        city = "";
                        state = "";
                        zipcode = "";
                        country = "";

                    }
                    var status=  db.Profiles.Where(x => x.UserID == User.Id).Select(x => x.UserStatus).FirstOrDefault();
                    status = status != null ? status : "Y";
                    if (status == "Y" || status == "y")
                        {


                            Session["AppId"] = User.Id;

                            using (EventComboEntities db = new EventComboEntities())
                            {
                                AspNetUser aspuser = db.AspNetUsers.First(i => i.Id == User.Id);
                                aspuser.LoginStatus = "Y";
                               
                            Profile prof = db.Profiles.First(i => i.UserID == User.Id);
                            prof.Ipcity = city;
                            prof.Ipcountry = country;
                            prof.IpState = state;
                            db.SaveChanges();
                        }
                            return RedirectToLocal(url);
                        }else

                        {
                            ModelState.AddModelError("", "You not authorized user");
                            return RedirectToAction("Index", "Home");

                        }
                  

                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    // return View();
                    return RedirectToAction("Index", "Home");
            }
        }





     

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }
        public String GetLanIPAddress()
        {
            //The X-Forwarded-For (XFF) HTTP header field is a de facto standard for identifying the originating IP address of a
            //client connecting to a web server through an HTTP proxy or load balancer
            String ip = HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ip))
            {
                ip = HttpContext.Request.ServerVariables["REMOTE_ADDR"];
            }

            return ip;
        }






        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);

                var Userid = UserManager.FindByEmail(model.Email);


                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    using (EventComboEntities objEntity = new EventComboEntities())
                    {
                        Profile prof = new Profile();

                        prof.Email = model.Email;
                        //prof.City = model.city;
                        //String Country = model.Country;
                        //prof.CountryID = byte.Parse(model.Country);
                        //prof.FirstName = model.Firstname;
                        //prof.LastName = model.Lastname;
                        //prof.State = prof.State;
                        prof.UserID = Userid.Id.ToString();

                        objEntity.Profiles.Add(prof);
                        objEntity.SaveChanges();

                    }
                    Session["AppId"] = Userid.Id;


                    /// to send email////
                    /// 
                    var fromAddress = new MailAddress("ayush.rajput@kiwitech.com", "Shweta");
                    var toAddress = new MailAddress(model.Email, model.Firstname);
                    const string fromPassword = "ayush@123";
                    const string subject = "Thanku You";
                    const string body = "Confirmation Message";

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
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

                    return RedirectToAction("About", "Home");

                }
                AddErrors(result);
            }
            //var countryQuery = (from c in db.Countries
            //                    orderby c.Country1 ascending
            //                    select c).Distinct();
            //List<SelectListItem> countryList = new List<SelectListItem>();
            //string defaultCountry = "";
            //foreach (var item in countryQuery)
            //{
            //    countryList.Add(new SelectListItem()
            //    {
            //        Text = item.Country1,
            //        Value = item.CountryID.ToString(),
            //        Selected = (item.Country1.Trim() == defaultCountry.Trim() ? true : false)
            //    });
            //}
            //ViewBag.Country = countryList;
            // If we got this far, something failed, redisplay form
            return View();
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Password);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
       
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            string city = "", state = "", zipcode = "", country = "";
            try
            {
                using (WebClient client = new WebClient())
                {
                    string ip = GetLanIPAddress().Replace("::ffff:", "");


                    var json = client.DownloadString("http://freegeoip.net/json/" + ip + "");
                    dynamic stuff = JsonConvert.DeserializeObject(json);
                    if (stuff != null)
                    {
                        city = stuff.city;
                        state = stuff.region_name;
                        zipcode = stuff.zip_code;
                        country = stuff.country_name;
                    }
                    else
                    {
                        city = "";
                        state = "";
                        zipcode = "";
                        country = "";

                    }
                }
            }
            catch (Exception ex)
            {
                city = "";
                state = "";
                zipcode = "";
                country = "";

            }
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            string firstname = "", Lastnmae = "", Email = "";
            if (loginInfo == null)
            {
                //return RedirectToAction("Index", "Home");
                return View("LoginResult", new LoginResultViewModel(false, Url.Action("Index","Home")));
            }
            //return RedirectToAction("Index", "Home");
            

            // var info = await AuthenticationManager.GetExternalLoginInfoAsync();
            string url = null;
            if (Session["ReturnUrl"] != null)
            {
                url = Session["ReturnUrl"].ToString();
            }
            else
            {

                url = Url.Action("Index", "Home");
            }

            var user = UserManager.FindByEmail(loginInfo.Email);


            if (user == null)
            {
                // var user = new ApplicationUser { UserName = model.Email1, Email = model.Email1 };
                var user1 = new ApplicationUser { UserName = loginInfo.Email, Email = loginInfo.Email };
                var result1 = await UserManager.CreateAsync(user1);
                if (result1.Succeeded)
                {
                    result1 = await UserManager.AddLoginAsync(user1.Id, loginInfo.Login);
                    if (result1.Succeeded)
                    {
                        await SignInManager.SignInAsync(user1, isPersistent: false, rememberBrowser: false);
                        await this.UserManager.AddToRoleAsync(user1.Id, "Member");

                        Session["AppId"] = user1.Id;
                        using (EventComboEntities objEntity = new EventComboEntities())
                        {
                            User_Permission_Detail permdetail = new User_Permission_Detail();
                            for (int i = 1; i < 3; i++)
                            {

                                permdetail.UP_Permission_Id = i;
                                permdetail.UP_User_Id = user1.Id.ToString();
                                objEntity.User_Permission_Detail.Add(permdetail);
                                objEntity.SaveChanges();
                            }
                        }

                        var externalIdentity = HttpContext.GetOwinContext().Authentication.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);
                        if (loginInfo.Login.LoginProvider == "Facebook")
                        {

                            var name = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

                            var accesstoken = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == "urn:facebook:access_token").Value;
                            var client = new FacebookClient(accesstoken);
                            dynamic me = client.Get("me");
                            firstname = me.first_name;
                            Lastnmae = me.last_name;
                            Email = loginInfo.Email;

                        }
                        else if (loginInfo.Login.LoginProvider == "Google")
                        {

                            var emailClaim = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                            var lastNameClaim = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname);
                            var givenNameClaim = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);

                            Email = emailClaim.Value;
                            firstname = givenNameClaim.Value;
                            Lastnmae = lastNameClaim.Value;

                        }
                        bool getprofstatus = Getprofiledetails(user1.Id);
                        if (getprofstatus == false)
                        {
                            using (EventComboEntities objEntity = new EventComboEntities())
                            {
                                Profile prof = new Profile();

                                prof.FirstName = firstname;
                                prof.LastName = Lastnmae;
                                prof.Email = Email;
                                prof.UserID = user1.Id;
                                prof.Ipcountry = country;
                                prof.IpState = state;
                                prof.Ipcity = city;
                                prof.UserStatus = "Y";
                                objEntity.Profiles.Add(prof);

                             objEntity.SaveChanges();

                               
                           
                               

                                

                            }

                            using (EventComboEntities db = new EventComboEntities())
                            {
                                AspNetUser aspuser = db.AspNetUsers.First(i => i.Id == user1.Id);
                                aspuser.LoginStatus = "Y";
                                db.SaveChanges();

                            }

                        }
                       return View("LoginResult", new LoginResultViewModel(true, url));

                       //return RedirectToLocal(url);
                    }
                    else
                    {
                       return View("LoginResult", new LoginResultViewModel(false, Url.Action("Index", "Home")));

                       // return RedirectToAction("Index", "Home");

                    }
                }
                else
                {
                    return View("LoginResult", new LoginResultViewModel(false, Url.Action("Index", "Home")));

                  // return RedirectToAction("Index", "Home");

                }

            }
            else
            {
                bool getstatus = GetExternalLogindetails(user.Id, loginInfo.Login.LoginProvider);
                if (getstatus)
                {


                }
                else
                {
                    using (EventComboEntities objEntity = new EventComboEntities())
                    {
                        AspNetUserLogin prof = new AspNetUserLogin();

                        prof.LoginProvider = loginInfo.Login.LoginProvider;
                        prof.ProviderKey = loginInfo.Login.ProviderKey;
                        prof.UserId = user.Id;
                        

                        objEntity.AspNetUserLogins.Add(prof);
                        objEntity.SaveChanges();

                    }

                }

                var externalIdentity = HttpContext.GetOwinContext().Authentication.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);
                if (loginInfo.Login.LoginProvider == "Facebook")
                {

                    var name = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

                    var accesstoken = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == "urn:facebook:access_token").Value;
                    var client = new FacebookClient(accesstoken);
                    dynamic me = client.Get("me");
                    firstname = me.first_name;
                    Lastnmae = me.last_name;
                    Email = loginInfo.Email;

                }
                else if (loginInfo.Login.LoginProvider == "Google")
                {

                    var emailClaim = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                    var lastNameClaim = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname);
                    var givenNameClaim = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);

                    Email = emailClaim.Value;
                    firstname = givenNameClaim.Value;
                    Lastnmae = lastNameClaim.Value;
                    ////request profile image
                    //using (var webClient = new System.Net.WebClient())
                    //{
                    //    var json = webClient.DownloadString(apiRequestUri);
                    //    dynamic me2 = JsonConvert.DeserializeObject(json);
                    //    //userPicture = result.picture;
                    //}
                }
                bool getprofstatus = Getprofiledetails(user.Id);
                if (getprofstatus == false)
                {
                    using (EventComboEntities objEntity = new EventComboEntities())
                    {
                        Profile prof = new Profile();

                        prof.FirstName = firstname;
                        prof.LastName = Lastnmae;
                        prof.Email = Email;
                        prof.UserID = user.Id;

                        prof.Ipcountry = country;
                        prof.IpState = state;
                        prof.Ipcity = city;
                        objEntity.Profiles.Add(prof);
                        objEntity.SaveChanges();

                    }

                }
                var status = db.Profiles.Where(x => x.UserID == user.Id).Select(x => x.UserStatus).FirstOrDefault();
                status = status != null ? status : "Y";
                if (status == "Y" || status == "y")
                {
                    var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
                    Session["AppId"] = user.Id;
                    using (EventComboEntities db = new EventComboEntities())
                    {
                        AspNetUser aspuser = db.AspNetUsers.First(i => i.Id == user.Id);
                        aspuser.LoginStatus = "Y";


                        Profile prof = db.Profiles.First(i => i.UserID == user.Id);
                        prof.Ipcountry = country;
                        prof.IpState = state;
                        prof.Ipcity = city;

                        db.SaveChanges();


                    }
                    return View("LoginResult", new LoginResultViewModel(true, url));
                }
                else
                {
                    return View("LoginResult", new LoginResultViewModel(false, Url.Action("Index", "Home")));
                }
               

             




            }
        } 

        public bool Getprofiledetails(string id)
        {
            using (EventComboEntities objEntity = new EventComboEntities())
            {

                var modelmyaccount = (from cpd in objEntity.AspNetUsers
                                      join pfd in objEntity.Profiles
    on cpd.Id equals pfd.UserID
                                      where cpd.Id == id
                                      select new ExternalLogin
                                      {
                                          userid = cpd.Id,
                                          email = cpd.Email

                                      }).FirstOrDefault();


                if (modelmyaccount == null)
                {

                    return false;
                }
                else
                {
                    return true;

                }
            }
     }

        public bool GetExternalLogindetails(string userid, string loginprovider)
        {
            using (EventComboEntities objEntity = new EventComboEntities())
            {

                var modelmyaccount = (from cpd in objEntity.AspNetUsers
                                      join pfd in objEntity.AspNetUserLogins
    on cpd.Id equals pfd.UserId
                                      where cpd.Id == userid && pfd.LoginProvider == loginprovider
                                      select new ExternalLogin
                                      {
                                          userid = cpd.Id,
                                          email = cpd.Email

                                      }).FirstOrDefault();


                if (modelmyaccount == null)
                {

                    return false;
                }
                else
                {
                    return true;

                }


            }


        }

        public void FacebookLogin(string id)
        {

        }

        [AllowAnonymous]
        public ActionResult Confirm(string Email)
        {
            ViewBag.Email = Email; return View();
        }
        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(LoginViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
       
        public ActionResult LogOff()
        {
            try {
                if (Session["AppId"] != null)
                {

                    var userid = Session["AppId"].ToString();
                    using (EventComboEntities db = new EventComboEntities())
                    {
                        AspNetUser aspuser = db.AspNetUsers.FirstOrDefault(i => i.Id == userid);
                        if (aspuser != null)
                        {
                            aspuser.LoginStatus = "N";
                            db.SaveChanges();
                        }

                    }
                }
                AuthenticationManager.SignOut();
                Session["Fromname"] = null;
                Session["AppId"] = null;
                Session["ReturnUrl"] = null;
                Session.Abandon();
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            catch(Exception exe)
            {
                AuthenticationManager.SignOut();
                Session.Abandon();
                Session.Clear();
                return RedirectToAction("Index", "Home");

            }
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");

            }
            catch (Exception)
            {
                if (returnUrl.Trim() != string.Empty)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }                 
            }
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}