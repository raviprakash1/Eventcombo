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
            if ((Session["UserID"] != null))
            {
                LeftMenu Menu;
                List<LeftMenu> MenuList = new List<LeftMenu>();

                string UserId = (Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
                var UserRole = db.Database.SqlQuery<string>("Select RoleId from AspNetUserRoles where UserId=@UserId", new System.Data.SqlClient.SqlParameter("@UserId", UserId)).SingleOrDefault();
                if (UserRole != null)
                {
                    if (Convert.ToInt16(UserRole) == 1)
                    {
                        var Permission = (from c in db.Permission_Detail
                                          select c);
                        foreach (var item in Permission)
                        {
                            Menu = new LeftMenu();
                            Menu.PermissionDescription = item.Permission_Desc;
                            Menu.PermissionId = item.Permission_Id;
                            MenuList.Add(Menu);
                        }
                    }
                    else
                    {
                        var Permission = (from c in db.User_Permission_Detail
                                          join p in db.Permission_Detail on c.UP_Permission_Id equals p.Permission_Id
                                          where c.UP_User_Id == UserId
                                          select new { c, p });
                        foreach (var item in Permission)
                        {
                            Menu = new LeftMenu();
                            Menu.PermissionDescription = item.p.Permission_Desc;
                            Menu.PermissionId = item.p.Permission_Id;
                            MenuList.Add(Menu);
                        }
                    }
                }
                return View(MenuList);
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

        private string ResolveServerUrl(string serverUrl, bool forceHttps)
        {
          if (serverUrl.IndexOf("://") > -1)
            return serverUrl;

          string newUrl = serverUrl;
          Uri originalUri = System.Web.HttpContext.Current.Request.Url;
          newUrl = (forceHttps ? "https" : originalUri.Scheme) +
              "://" + originalUri.Authority + newUrl;
          return newUrl;
        }


        private string getuserImage()
        {
            if ((Session["UserID"] != null))
            {
                string userid = Session["UserID"].ToString();
                var userImage = db.Profiles.Where(x => x.UserID == userid).Select(y => y.UserProfileImage).SingleOrDefault();
                if (userImage != null)
                {
                  return ResolveServerUrl(VirtualPathUtility.ToAbsolute(Url.Content("~/Images/Profile/Profile_Images/imagepath/" + userImage)), false); 
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

        private string getusername()
        {
            if ((Session["UserID"] != null))
            {
                string userid = Session["UserID"].ToString();
                var userEmail = db.AspNetUsers.Where(x => x.Id == userid).Select(y => y.Email).SingleOrDefault();
                if(userEmail!=null)
                {
                  return  userEmail.Substring(0, userEmail.IndexOf("@") + 1);
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
                    var roleSuperAdmin = (from r in db.AspNetRoles  where r.Id.Equals("1")  select r).FirstOrDefault();
                    var users = db.AspNetUsers.Where(x => x.AspNetRoles.Select(y => y.Id ).Contains(roleSuperAdmin.Id)).ToList();
                    var status = db.Profiles.Where(x => x.UserID == User.Id).Select(x => x.UserStatus).FirstOrDefault();
                    if (users.Find(x => x.Id == User.Id ) != null)
                    {
                     
                        if (status == "Y" || status == "y")
                        {

                            Session["UserID"] = User.Id;
                            using (EmsEntities db = new EmsEntities())
                            {
                                AspNetUser aspuser = db.AspNetUsers.First(i => i.Id == User.Id);
                                aspuser.LoginStatus = "Y";
                                aspuser.LastLoginTime = System.DateTime.UtcNow;
                                db.SaveChanges();

                            }
                            return RedirectToAction("Dashboard");
                        }
                        else
                        {
                            ModelState.AddModelError("", "You are not an authorized user!!");
                            return View(model);
                        }
                    }
                    else
                    {
                        var roleAdmin = (from r in db.AspNetRoles where r.Id.Equals("2") select r).FirstOrDefault();
                        var usersAdmin = db.AspNetUsers.Where(x => x.AspNetRoles.Select(y => y.Id).Contains(roleAdmin.Id)).ToList();
                        if (usersAdmin.Find(x => x.Id == User.Id) != null)
                        {
                            if (status == "Y" || status == "y")
                            {

                                Session["UserID"] = User.Id;
                                using (EmsEntities db = new EmsEntities())
                                {
                                    AspNetUser aspuser = db.AspNetUsers.First(i => i.Id == User.Id);
                                    aspuser.LoginStatus = "Y";
                                    aspuser.LastLoginTime = System.DateTime.UtcNow;
                                    db.SaveChanges();

                                }
                                return RedirectToAction("Dashboard");
                            }
                            else
                            {
                                ModelState.AddModelError("", "You are not an authorized user!!");
                                return View(model);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "You are not an authorized user!!");
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

        public ActionResult LeftMenu()
        {
            LeftMenu Menu;
            List<LeftMenu> MenuList = new List<LeftMenu>();

            string UserId = (Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
            var UserRole = db.Database.SqlQuery<string>("Select RoleId from AspNetUserRoles where UserId=@UserId", new System.Data.SqlClient.SqlParameter("@UserId", UserId)).SingleOrDefault();
            if (UserRole != null)
            {
                if (Convert.ToInt16(UserRole) == 1)
                {
                    var Permission = (from c in db.Permission_Detail
                                      select c);
                    foreach (var item in Permission)
                    {
                        Menu = new LeftMenu();
                        Menu.PermissionDescription = item.Permission_Desc;
                        Menu.PermissionId = item.Permission_Id;
                        MenuList.Add(Menu);
                    }
                }
                else
                {
                    var Permission = (from c in db.User_Permission_Detail
                                      join p in db.Permission_Detail on c.UP_Permission_Id equals p.Permission_Id
                                      where c.UP_User_Id == UserId
                                      select new { c, p });
                    foreach (var item in Permission)
                    {
                        Menu = new LeftMenu();
                        Menu.PermissionDescription = item.p.Permission_Desc;
                        Menu.PermissionId = item.p.Permission_Id;
                        MenuList.Add(Menu);
                    }
                }
            }
            return PartialView("LeftMenu", MenuList);
        }

    }
}