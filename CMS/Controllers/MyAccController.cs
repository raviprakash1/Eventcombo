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
namespace CMS.Controllers
{
    public class MyAccController : Controller
    {
        EmsEntities db = new EmsEntities();
        // GET: MyAcc
        //public ActionResult MyAccount()
        //{

        //    return View();
        //}

        [HttpGet]
        [Authorize]
        public ActionResult MyAccount()
        {
            string defaultCountry = "";
            if ((Session["AppId"] != null))
            {
                var client = new WebClient();
                //string ip = GetLanIPAddress().Replace("::ffff:", "");
                //var json = client.DownloadString("http://freegeoip.net/json/" + ip + "");
                //dynamic stuff = JsonConvert.DeserializeObject(json);


                //ViewBag.Country = stuff.country_name;
                //ViewBag.city = stuff.city;
                //ViewBag.state =stuff.region_name;
                //ViewBag.PinCode = stuff.zip_code;


                string userid = "7bd93525-a288-43fd-84f8-40ec4b7c50bd";
                MyAccount myacc = new MyAccount();
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

                        myacc.day = int.Parse(day[0].ToString());
                        myacc.month = int.Parse(day[1].ToString());
                        myacc.year = int.Parse(day[2].ToString());


                    }

                    if (string.IsNullOrEmpty(accountdetail.City))
                    {
                        myacc.City = "";
                    }
                    else
                    {
                        myacc.City = accountdetail.City;
                    }
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

                return View(myacc);


            }
            else
            {
                return RedirectToAction("Index", "Home");
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
                                          PreviousEmail = cpd.Email
                                      });
                return modelmyaccount.FirstOrDefault();
            }
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MyAccount(MyAccount model, HttpPostedFileBase file)
        {
            string msg = "", errormessage = "";
            if (Session["AppId"] != null)
            {
                string Userid = Session["AppId"].ToString();
                var accountdetail = GetLoginDetails(Userid);
                if (string.IsNullOrEmpty(model.Firstname) && string.IsNullOrEmpty(model.Lastname))
                {

                    ModelState.AddModelError("Error", "Please provide first name and last name!");

                }
                if (!string.IsNullOrEmpty(model.ConfirmEmail))
                {
                    if (model.Email != model.ConfirmEmail)
                    {
                        ModelState.AddModelError("Error", "Email and email verification doesn't match!");

                    }
                }
                if (!string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.ConfirmPassword))
                {
                    if (model.NewPassword != model.ConfirmPassword)
                    {
                        ModelState.AddModelError("Error", "New password and confirm new password doesn't match!");


                    }
                }

                if (!string.IsNullOrEmpty(model.Password))
                {

                    //var user12 = UserManager.FindByEmail(accountdetail.PreviousEmail);
                    //var result = UserManager.PasswordHasher.VerifyHashedPassword(user12.PasswordHash, model.Password);
                    //if (result.ToString() != "Success")
                    //{
                    //    ModelState.AddModelError("Error", "Invalid current password!");

                    //}
                }

                if (!string.IsNullOrEmpty(model.Email))
                {
                    //var user = UserManager.FindByEmail(model.Email);
                    //if (user != null)
                    //{
                    //    ModelState.AddModelError("Error", "Confirm email already exist!");
                    //    //errormessage += "Confirm email already exist!! </br>";
                    //}
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
                        profile.Gender = model.Gender;
                        profile.DateofBirth = model.day.ToString() + "-" + model.month.ToString() + "-" + model.year.ToString();
                        if (!string.IsNullOrEmpty(model.ConfirmEmail) && !string.IsNullOrEmpty(model.Email))
                        {
                            profile.Email = model.Email;

                        }
                        objEntity.SaveChanges();
                    }
                    using (EmsEntities objEntity = new EmsEntities())
                    {
                        AspNetUser aspuser = objEntity.AspNetUsers.First(i => i.Id == Userid); ;
                        if (!string.IsNullOrEmpty(model.ConfirmEmail) && !string.IsNullOrEmpty(model.Email))
                        {
                            aspuser.Email = model.Email;
                            aspuser.UserName = model.Email;
                        }
                        objEntity.SaveChanges();
                    }



                    if (!string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.ConfirmPassword))
                    {
                        //var token = await UserManager.GeneratePasswordResetTokenAsync(Userid);

                        //var result = await UserManager.ResetPasswordAsync(Userid, token, model.NewPassword);

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
                    ViewData["Message"] = "Updated Successfully!!!!!";
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
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }

        }
    }
}