﻿using System;
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
    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public HomeController()
        {
        }
        EmsEntities db = new EmsEntities();
        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
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

        public ActionResult Login()
        {

            return View();
        }
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult Dashboard()
        {
            if ((Session["AppId"] != null))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");

            }

        }

        public ActionResult UserName()
        {
            string result = getusername();
            return Content(result);
        }
        public ActionResult UserImage()
        {
            string result = getuserImage();
            return Content(result);
        }

        private string getuserImage()
        {
            if ((Session["AppId"] != null))
            {
                string userid = Session["AppId"].ToString();
                var userImage = db.Profiles.Where(x => x.UserID == userid).Select(y => y.UserProfileImage).SingleOrDefault();
                return "http://eventcombo.kiwireader.com/Images/Profile/Profile_Images/imagepath/" + userImage;
            }
            else
            {
                return "";
            }
        }

        private string getusername()
        {
            if ((Session["AppId"] != null))
            {
                string userid = Session["AppId"].ToString();
                var userEmail = db.AspNetUsers.Where(x => x.Id == userid).Select(y => y.Email).SingleOrDefault();
                return userEmail;
            }
            else
            {
                return "";

            }
        }

        [HttpPost]
        [AllowAnonymous]
       public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    var User = UserManager.FindByEmail(model.Email.ToString());
                    var roleSuperAdmin = (from r in db.AspNetRoles  where r.Name.Contains("Super Admin")  select r).FirstOrDefault();
                    var users = db.AspNetUsers.Where(x => x.AspNetRoles.Select(y => y.Id ).Contains(roleSuperAdmin.Id)).ToList();
                    if (users.Find(x => x.Id == User.Id ) != null)
                    {
                        Session["AppId"] = User.Id;
                       var userprofile = db.Profiles.Where(x => x.UserID == User.Id).Select(y => y.UserProfileImage).SingleOrDefault();
                        if(!string.IsNullOrEmpty(userprofile))
                        {
                            ViewData["UserImage"] = "http://eventcombo.kiwireader.com/Images/Profile/Profile_Images/imagepathuserprofile/"+ userprofile;
                        }
                         ViewData["UserName"] = model.Email;
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        var roleAdmin = (from r in db.AspNetRoles where r.Name.Contains("Admin") select r).FirstOrDefault();
                        var usersAdmin = db.AspNetUsers.Where(x => x.AspNetRoles.Select(y => y.Id).Contains(roleAdmin.Id)).ToList();
                        if (users.Find(x => x.Id == User.Id) != null)
                        {
                            Session["AppId"] = User.Id;
                            var userprofile = db.Profiles.Where(x => x.UserID == User.Id).Select(y => y.UserProfileImage).SingleOrDefault();
                            if (!string.IsNullOrEmpty(userprofile))
                            {
                                ViewData["UserImage"] = "http://eventcombo.kiwireader.com/Images/Profile/Profile_Images/imagepathuserprofile/" + userprofile;

                            }
                            ViewData["UserImage"] = "";
                            ViewData["UserName"] = model.Email;
                            return RedirectToAction("Dashboard");
                        }
                        else
                        {
                            ModelState.AddModelError("", "You not authorized user");
                            return View(model);
                        }
                      
                    }
                   
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return View(model);
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);

            }
        }


        public bool isadmin(string Userid)
        {
            //using (EmsEntities objEntity = new EmsEntities())
            //{
            //    var modelmyaccount = (from cpd in objEntity.AspNetUsers
            //                          join pfd in objEntity.AspNetRoles
            //                          on cpd.Id equals pfd
            //                          where cpd.Id == userid
            //                          select cpd);
            //    if (modelmyaccount.FirstOrDefault() != null)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;

            //    }

            //}
            return false;
        }
    }
}