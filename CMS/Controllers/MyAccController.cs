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
using CMS.Models;
using System.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Mail;
using System.IO;

using System.Web.Security;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Configuration;

namespace CMS.Controllers
{
    public class MyAccController : Controller
    {
        EmsEntities db = new EmsEntities();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
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
        // GET: MyAcc
        //public ActionResult MyAccount()
        //{

        //    return View();
        //}


        [HttpGet]
        [Authorize]
        public ActionResult MyAccount(string UserId)
        {
            string defaultCountry = "";

            if ((Session["UserID"] != null))
            {
                var client = new WebClient();
                //string ip = GetLanIPAddress().Replace("::ffff:", "");
                //var json = client.DownloadString("http://freegeoip.net/json/" + ip + "");
                //dynamic stuff = JsonConvert.DeserializeObject(json);


                //ViewBag.Country = stuff.country_name;
                //ViewBag.city = stuff.city;
                //ViewBag.state =stuff.region_name;
                //ViewBag.PinCode = stuff.zip_code;


                string userid = UserId;
                MyAccount myacc = new MyAccount();
                myacc.Id = userid;
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
                    string city = accountdetail.City;
                    if (string.IsNullOrEmpty(myacc.Dateofbirth))
                    {
                        myacc.day = 1;
                        myacc.month = 1;
                        myacc.year = 1980;
                    }
                    else
                    {
                        string[] day = myacc.Dateofbirth.Split('-');

                        myacc.day = day[0].ToString().Trim() != string.Empty ? int.Parse(day[0].ToString()) : 1;
                        myacc.month = day[1].ToString().Trim() != string.Empty ? int.Parse(day[1].ToString()) : 1;
                        myacc.year = day[2].ToString().Trim() != string.Empty ? int.Parse(day[2].ToString()) : 1;


                    }
                   
                  var ans=  db.Database.SqlQuery<string>("select RoleId from AspNetUserRoles where Userid=@p0",userid).FirstOrDefault();
                  
                    if(ans=="2")
                    {
                        myacc.Designation = "Admin";
                    }
                    if (ans == "1")
                    {
                        myacc.Designation = "Super Admin";
                    }
                    if (ans == "3")
                    {
                        var countperrm = db.Permission_Detail.Where(i => i.Permission_Category == "APP").Count();
                        var countuserperm = db.User_Permission_Detail.Where(i => i.UP_User_Id == userid).Count();

                        if(countuserperm < countperrm)
                        {
                            myacc.Designation = "Member-Limited";
                            ans = "4";
                        }
                        else
                        {
                            myacc.Designation = "Member";
                        }
                    }
                    myacc.role = ans;
                    if (string.IsNullOrEmpty(accountdetail.City))
                    {
                        myacc.City = "";
                    }
                    else
                    {
                        myacc.City = accountdetail.City;
                    }
                    myacc.Email = accountdetail.Email;

                    string state = accountdetail.State;
                    if (string.IsNullOrEmpty(state))
                    {
                        myacc.State = "";
                    }
                    else
                    {
                        myacc.State = state;
                    }
                    string zipcode = accountdetail.ZipCode;
                    if (string.IsNullOrEmpty(zipcode))
                    {
                        myacc.ZipCode = "";
                    }
                    else
                    {
                        myacc.ZipCode = zipcode;
                    }
                    var ticketpurchased = db.Database.SqlQuery<Int64>("select isnull(sum(TPD_Purchased_Qty),0) from Ticket_Purchased_Detail  where TPD_User_Id=@p0", UserId).FirstOrDefault();
                    myacc.TicketPurchased = ticketpurchased;
                    //JavaScriptSerializer js = new JavaScriptSerializer();
                    //var results = js.Serialize(x);



                    //if (string.IsNullOrEmpty(myacc.UserProfileImage))
                    //{
                    //    myacc.editsave = "Save";
                    //    myacc.UserProfileImage = "image-drop2.gif";
                    //}
                    //else
                    //{

                    //    myacc.ImagePath = "/Images/Profile/Profile_Images/imagepath/" + myacc.UserProfileImage;
                    //    myacc.editsave = "Edit";

                    //}
                }
                else
                {
                    myacc = new MyAccount();

                    //myacc.editsave = "Save";
                    //myacc.UserProfileImage = "image-drop2.gif";
                    myacc.WebsiteURL = "";
                    myacc.CurrentPassword = "";

                }
                //string COuntryname = stuff.country_name;
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
                    defaultCountry = "United States";

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
                //var DesignQuery = (from c in db.AspNetRoles
                //                   where c.Id!="1"
                //                    orderby c.Id ascending
                //                    select c ).Distinct();
                //List<SelectListItem> DesignationList = new List<SelectListItem>();
                //foreach (var item in DesignQuery)
                //{
                //    DesignationList.Add(new SelectListItem()
                //    {
                //        Text = item.Name,
                //        Value = item.Id ,
                //        Selected=(item.Id==myacc.Designation? true :false)
                //    });

                //}
                //DesignationList.Add(new SelectListItem()
                //{
                //    Text = "Memeber-Limited",
                //    Value = "4",
                //    Selected = ("4" == myacc.Designation ? true : false)

                //});
                //ViewBag.Desig = DesignationList;
                return View(myacc);


            }
            else
            {
                Session.Abandon();
                Session.Clear();
                return RedirectToActionPermanent("Login", "Home");
            }

        }

        public MyAccount GetLoginDetails(string userid)
        {

            using (EmsEntities objEntity = new EmsEntities())
            {
                var modelmyaccount = (from cpd in objEntity.AspNetUsers
                                      join pfd in objEntity.Profiles
                                            on cpd.Id equals pfd.UserID
                                      where cpd.Id == userid
                                      select new MyAccount
                                      {
                                          Id = cpd.Id,
                                          Firstname = pfd.FirstName,
                                          Lastname = pfd.LastName,
                                          StreetAddress1 = pfd.StreetAddressLine1,
                                          StreetAddress2 = pfd.StreetAddressLine2,
                                          City = pfd.City,
                                          State = pfd.State,
                                          ZipCode = pfd.Zip,
                                          Country = pfd.CountryID.ToString(),
                                          MainPhone = pfd.MainPhone,
                                          SecondPhone = pfd.SecondPhone,
                                          WebsiteURL = pfd.WebsiteURL,
                                          UserProfileImage = pfd.UserProfileImage,
                                          contentype = pfd.ContentType,
                                          Dateofbirth = pfd.DateofBirth,
                                          Gender = pfd.Gender,
                                          PreviousEmail = cpd.Email,
                                          Email = cpd.Email,
                                          WorkPhone = pfd.WorkPhone,
                                          Password=cpd.PasswordHash
                                      });
                return modelmyaccount.FirstOrDefault();
            }
        }
        [HttpPost]
        public async Task<ActionResult> MyAccount(MyAccount model)
        {
            string msg = "", errormessage = "", successmsg = "";
            //if (Session["AppId"] != null)
            //{
            ValidationMessageController vmc = new ValidationMessageController();
            var validationresult = "";
            string Userid = model.Id;
            var accountdetail = GetLoginDetails(Userid);
            if (string.IsNullOrEmpty(model.Firstname) )
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
            if (!string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.ConfirmPassword))
            {
                if (model.NewPassword != model.ConfirmPassword)
                {
                    validationresult = vmc.Index("MyAccount", "MyAccountPwdmatchValidationSy");
                    ModelState.AddModelError("Error", validationresult);


                }
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
            if (model.PreviousEmail != model.Email)
            {
                var user = UserManager.FindByEmail(model.Email);
                if (user != null)
                {
                    validationresult = vmc.Index("MyAccount", "MyAccountEmailAlreadyExistSY");
                    ModelState.AddModelError("Error", validationresult);
                }
            }



            if (ModelState.IsValid)
            {

                using (EmsEntities objEntity = new EmsEntities())
                {
                    var ApplicationUser = objEntity.AspNetUsers.Find(Userid);
                    Profile profile = objEntity.Profiles.First(i => i.UserID == Userid);
                    profile.FirstName = model.Firstname;
                    profile.LastName = model.Lastname;
                    profile.StreetAddressLine1 = model.StreetAddress1;
                    profile.StreetAddressLine2 = model.StreetAddress2;
                    profile.City = model.City;
                    profile.State = model.State;
                    profile.Zip = model.ZipCode;
                    profile.CountryID = byte.Parse(model.Country);
                    profile.MainPhone = model.MainPhone;
                    profile.SecondPhone = model.SecondPhone;
                    profile.WorkPhone = model.WorkPhone;
                    profile.WebsiteURL = model.WebsiteURL;
                    //profile.UserProfileImage = model.UserProfileImage;
                    //profile.Gender = model.Gender;
                    //if(!string.IsNullOrEmpty(model.day.ToString ()) && !string.IsNullOrEmpty(model.month.ToString()) && !string.IsNullOrEmpty(model.year.ToString()))
                    //{
                    //    profile.DateofBirth = model.day.ToString() + "-" + model.month.ToString() + "-" + model.year.ToString();
                    //}
                       
                    if (!checkexternallogin(Userid))
                    {
                        if (!string.IsNullOrEmpty(model.Email) && model.PreviousEmail != model.Email)
                        {
                            profile.Email = model.Email;

                        }
                    }
                    objEntity.SaveChanges();
                }
                using (EmsEntities objEntity = new EmsEntities())
                {
                    AspNetUser aspuser = objEntity.AspNetUsers.First(i => i.Id == Userid); ;
                    if (!checkexternallogin(Userid))
                    {
                        if (!string.IsNullOrEmpty(model.Email) && model.PreviousEmail != model.Email)
                        {
                            aspuser.Email = model.Email;
                            aspuser.UserName = model.Email;
                        }
                        objEntity.SaveChanges();
                    }
                }



                if (!string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.ConfirmPassword))
                {
                    var token = await UserManager.GeneratePasswordResetTokenAsync(model.Id);

                    var result = await UserManager.ResetPasswordAsync(model.Id, token, model.NewPassword);
                    successmsg = vmc.Index("MyAccount", "MyAccountSuccessPasswordSY") + successmsg;
                    string to = "", from = "", cc = "", bcc = "", subjectn = "", emailname="";
                    var bodyn = "";
                    List<Email_Tag> EmailTag = new List<Email_Tag>();
                    HomeController hmc = new HomeController();
                    EmailTag = getTag();

                    var Emailtemplate =getEmail("acc_pwd_set");
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
                                        subjectn = subjectn.Replace("¶¶UserFirstNameID¶¶", model.Firstname);

                                    }
                                   

                                    if (EmailTag[i].Tag_Name == "UserLastNameID")
                                    {
                                        subjectn = subjectn.Replace("¶¶UserLastNameID¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventNameID")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventNameID¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventStartDateID")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventStartDateID¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventEndDateID")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventEndDateID¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventStartTimeID")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventStartTimeID¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventEndTimeID")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventEndTimeID¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventVenueID")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventVenueID¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventAddressID")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventAddressID¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "TicketOrderNumberID")
                                    {
                                        subjectn = subjectn.Replace("¶¶TicketOrderNumberID¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "DealOrderNumberID")
                                    {
                                        subjectn = subjectn.Replace("¶¶DealOrderNumberID¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "ResetPwdUrl")
                                    {
                                        subjectn = subjectn.Replace("¶¶ResetPwdUrl¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventOrderNO")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventOrderNO¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventBarcodeId")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventBarcodeId¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventTitleId")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventTitleId¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventImageId")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventImageId¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "Tickettype")
                                    {
                                        subjectn = subjectn.Replace("¶¶Tickettype¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "TicketPrice")
                                    {
                                        subjectn = subjectn.Replace("¶¶TicketPrice¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "TicketOrderDateId")
                                    {
                                        subjectn = subjectn.Replace("¶¶TicketOrderDateId¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventTimeZone")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventTimeZone¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "Ticketname")
                                    {
                                        subjectn = subjectn.Replace("¶¶Ticketname¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventQrCode")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventQrCode¶¶", "");

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
                                        bodyn = bodyn.Replace("¶¶UserFirstNameID¶¶", model.Firstname);

                                    }
                                  

                                    if (EmailTag[i].Tag_Name == "UserLastNameID")
                                    {
                                        bodyn = bodyn.Replace("¶¶UserLastNameID¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventNameID")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventNameID¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventStartDateID")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventStartDateID¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventEndDateID")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventEndDateID¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventStartTimeID")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventStartTimeID¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventEndTimeID")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventEndTimeID¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventVenueID")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventVenueID¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventAddressID")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventAddressID¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "TicketOrderNumberID")
                                    {
                                        bodyn = bodyn.Replace("¶¶TicketOrderNumberID¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "DealOrderNumberID")
                                    {
                                        bodyn = bodyn.Replace("¶¶DealOrderNumberID¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "ResetPwdUrl")
                                    {
                                        bodyn = bodyn.Replace("¶¶ResetPwdUrl¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventOrderNO")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventOrderNO¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventBarcodeId")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventBarcodeId¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventTitleId")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventTitleId¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventImageId")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventImageId¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "Tickettype")
                                    {
                                        bodyn = bodyn.Replace("¶¶Tickettype¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "TicketPrice")
                                    {
                                        bodyn = bodyn.Replace("¶¶TicketPrice¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "TicketOrderDateId")
                                    {
                                        bodyn = bodyn.Replace("¶¶TicketOrderDateId¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventTimeZone")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventTimeZone¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "Ticketname")
                                    {
                                        bodyn = bodyn.Replace("¶¶Ticketname¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventQrCode")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventQrCode¶¶", "");

                                    }


                                }

                            }
                        }
                        SendHtmlFormattedEmail(to, from, cc,bcc,subjectn, bodyn, emailname);
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

                //if (string.IsNullOrEmpty(accountdetail.UserProfileImage))
                //{
                //    model.editsave = "Save";
                //    model.UserProfileImage = "image-drop2.gif";
                //}
                //else
                //{
                //    model.UserProfileImage = accountdetail.UserProfileImage;
                //    model.contentype = accountdetail.contentype;
                //    model.ImagePath = "/Images/Profile/Profile_Images/imagepath/" + accountdetail.UserProfileImage;
                //    model.editsave = "Edit";

                //}
                if (successmsg != "")
                {
                    successmsg = vmc.Index("MyAccount", "MyAccountSuccessInitSY") + "," + successmsg;
                }
                else
                {
                    successmsg = vmc.Index("MyAccount", "MyAccountSuccessInitSY");
                }
                TempData["SuccessMessage"] = successmsg;
                var ans = db.Database.SqlQuery<string>("select RoleId from AspNetUserRoles where Userid=@p0", model.Id).FirstOrDefault();

                if (ans == "2")
                {
                    model.Designation = "Admin";
                }
                if (ans == "1")
                {
                    model.Designation = "Super Admin";
                }
                if (ans == "3")
                {
                    var countperrm = db.Permission_Detail.Where(i => i.Permission_Category == "APP").Count();
                    var countuserperm = db.User_Permission_Detail.Where(i => i.UP_User_Id == model.Id).Count();

                    if (countuserperm < countperrm)
                    {
                        model.Designation = "Member-Limited";
                        ans = "4";
                    }
                    else
                    {
                        model.Designation = "Member";
                    }
                }
                model.role = ans;
                var ticketpurchased = db.Database.SqlQuery<Int64>("select isnull(sum(TPD_Purchased_Qty),0) from Ticket_Purchased_Detail  where TPD_User_Id=@p0", Userid).FirstOrDefault();
                model.TicketPurchased = ticketpurchased;
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
            //if (string.IsNullOrEmpty(accountdetail.UserProfileImage))
            //{
            //    model.editsave = "Save";
            //    model.UserProfileImage = "image-drop2.gif";
            //}
            //else
            //{
            //    model.UserProfileImage = accountdetail.UserProfileImage;
            //    model.contentype = accountdetail.contentype;
            //    model.ImagePath = "/Images/Profile/Profile_Images/imagepath/" + accountdetail.UserProfileImage;
            //    model.editsave = "Edit";

            //}

            return View(model);
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Home");

            //}

        }

        public bool checkexternallogin(string userid)
        {
            using (EmsEntities objEntity = new EmsEntities())
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
        public List<Email_Tag> getTag()
        {
            var EmailTag = db.Email_Tag.ToList();
            return EmailTag;

        }
        public Email_Template getEmail(string template)
        {

            var userEmail = db.Email_Template.Where(x => x.Template_Name == template).SingleOrDefault();

            return userEmail;


        }
        public void SendHtmlFormattedEmail(string To, string from,string cc,string bcc, string subject, string body,string emailname)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(from, emailname);
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


    }
}