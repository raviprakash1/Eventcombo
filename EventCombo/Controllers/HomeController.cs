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
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Microsoft.Owin.Security.OAuth;
using PagedList;

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
        [AllowAnonymous]
        public async Task<string> FacebookLogin(string id, string email)
        {
            //current login details 
            string city = "", state = "", zipcode = "", country = "";
            var Strfirstname = "";
            var Strlastnmae = "";
            var Stremail = "";
            var fbuserid = "";


            AccountController acc = new AccountController();
            try
            {
                using (WebClient wbclient = new WebClient())
                {
                    string ip = GetLanIPAddress().Replace("::ffff:", "");


                    var json = wbclient.DownloadString("http://freegeoip.net/json/" + ip + "");
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

            //

            //Redirect url
            string url = null;
            if (Session["ReturnUrl"] != null)
            {
                url = Session["ReturnUrl"].ToString();
            }
            else
            {

                url = Url.Action("Index", "Home");
            }
            //

            //Fb userdetail
            var client = new FacebookClient(id);

            dynamic me = client.Get("me?fields=first_name,last_name,id,email");
            Strfirstname = me.first_name;
            Strlastnmae = me.last_name;
            Stremail = me.email;
            fbuserid = me.id;

            string HitURL = string.Format("https://graph.facebook.com/me?access_token={0}", id);

            HttpClient clienthd = new HttpClient();
            Uri uri = new Uri(HitURL);
            HttpResponseMessage response = await clienthd.GetAsync(uri);
            ClaimsIdentity identity = null;
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic iObj = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, fbuserid, ClaimValueTypes.String, "Facebook", "Facebook"));
            }
            Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var LoginProvider = providerKeyClaim.Issuer;
            var ProviderKey = providerKeyClaim.Value;
            var UserName = identity.FindFirstValue(ClaimTypes.Name);
            //

            var user = UserManager.FindByEmail(email);
            UserLoginInfo login = new UserLoginInfo(LoginProvider, ProviderKey);
            if (user == null)
            {
                var user1 = new ApplicationUser { UserName = email, Email = email };
                var result1 = await UserManager.CreateAsync(user1);


                if (result1.Succeeded)
                {
                    result1 = await UserManager.AddLoginAsync(user1.Id, login);
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

                            bool getprofstatus = acc.Getprofiledetails(user1.Id);
                            if (getprofstatus == false)
                            {
                                Profile prof = new Profile();

                                prof.FirstName = Strfirstname;
                                prof.LastName = Strlastnmae;
                                prof.Email = email;
                                prof.UserID = user1.Id;
                                prof.Ipcountry = country;
                                prof.IpState = state;
                                prof.Ipcity = city;
                                prof.UserStatus = "Y";

                                objEntity.Profiles.Add(prof);


                                AspNetUser aspuser = db.AspNetUsers.First(i => i.Id == user1.Id);
                                aspuser.LoginStatus = "Y";


                                objEntity.SaveChanges();
                                var externalIdentity = HttpContext.GetOwinContext().Authentication.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);

                            }
                        }

                        return url;
                    }
                    else
                    {
                        return Url.Action("Index", "Home");
                    }
                }
                else
                {
                    return Url.Action("Index", "Home");
                }
            }
            else
            {
                bool getstatus = acc.GetExternalLogindetails(user.Id, LoginProvider);
                if (!getstatus)
                {
                    using (EventComboEntities objEntity = new EventComboEntities())
                    {
                        AspNetUserLogin prof = new AspNetUserLogin();

                        prof.LoginProvider = LoginProvider;
                        prof.ProviderKey = ProviderKey;
                        prof.UserId = user.Id;


                        objEntity.AspNetUserLogins.Add(prof);
                        objEntity.SaveChanges();

                    }
                }

                bool getprofstatus = acc.Getprofiledetails(user.Id);
                using (EventComboEntities objEntity = new EventComboEntities())
                {

                    if (getprofstatus == false)
                    {

                        Profile prof = new Profile();
                        prof.FirstName = Strfirstname;
                        prof.LastName = Strlastnmae;
                        prof.Email = email;
                        prof.UserID = user.Id;
                        objEntity.Profiles.Add(prof);


                    }
                    else
                    {
                        Profile prof = db.Profiles.First(i => i.UserID == user.Id);
                        prof.Ipcountry = country;
                        prof.IpState = state;
                        prof.Ipcity = city;
                    }

                    AspNetUser aspuser = db.AspNetUsers.First(i => i.Id == user.Id);
                    aspuser.LoginStatus = "Y";

                    objEntity.SaveChanges();
                }
                var usernew = new ApplicationUser { UserName = email, Email = email };
                ExternalLoginInfo exterlogin = new ExternalLoginInfo();
                var externalIdentity = HttpContext.GetOwinContext().Authentication.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);

                exterlogin.DefaultUserName = Strfirstname;
                exterlogin.Email = email;
                exterlogin.Login = login;
                exterlogin.ExternalIdentity = identity;
                var status = db.Profiles.Where(x => x.UserID == user.Id).Select(x => x.UserStatus).FirstOrDefault();
                status = status != null ? status : "Y";
                if (status == "Y" || status == "y")
                {
                    await SignInManager.SignInAsync(user, true, true);
                    //var result = await SignInManager.ExternalSignInAsync(exterlogin, isPersistent: false);
                    Session["AppId"] = user.Id;
                    return url;
                }
                else
                {
                    return Url.Action("Index", "Home");
                }
            }









        }
        public string IsValidID(string Email, string Password)
        {

            var user = UserManager.FindByEmail(Email);

            if (user == null)
            {
                return "NotFound";

            }
            else
            {
                var result = UserManager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, Password);

                if (result.ToString() == "Success")
                {
                    return "PasswordMatch";

                }
                else
                {
                    return "PasswordNotMatch";

                }

            }


        }


        public ActionResult DiscoverEvents(string strEt, string strEc, string strPrice, string strPageIndex, string strLat, string strLong, string strSort, string strDateFilter,string strTextSearch)
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
            string strUrl = @Url.RouteUrl("EvType", new { strEt = strEt, strEc = strEc, strPrice = strPrice, strPageIndex = strPageIndex, strLat = strLat, strLong = strLong, strSort = strSort, strDateFilter = strDateFilter });

            Session["ReturnUrl"] = "DiscoverEvent~" + strUrl;

            if (strPageIndex == null) strPageIndex = "page";
            if (strDateFilter == null) strDateFilter = "none";
            int pageSize = 15;
            int pageIndex = 1;
            if (strPageIndex != null && strPageIndex != string.Empty && strPageIndex != "page")
                pageIndex = Convert.ToInt32(strPageIndex);


            if (strEt == null || strEt == "~" || strEt == "evt") strEt = string.Empty;
            if (strEc == null || strEc == "~" || strEc == "evc") strEc = string.Empty;

            Session["Fromname"] = "DiscoverEvents";
            string strNearLat = "";
            string strNearLong = "";

            List<DiscoverEvent> objDiscEvt = GetDiscoverEventListing(strEt, strEc, strPrice, strLat, strLong, strSort, strDateFilter, ref strNearLat, ref strNearLong,strTextSearch);
            double dPageCount = objDiscEvt.Count;
            double dTotalPages = dPageCount / pageSize;
            int lTotalPages = (objDiscEvt.Count / pageSize);
            if (dTotalPages.ToString().Contains(".") == true)
                lTotalPages = lTotalPages + 1;

            ViewBag.DisEvnt = objDiscEvt.ToPagedList(pageIndex, pageSize);
            ViewBag.Eventtype = GetDiscoverEventType(strEt);
            ViewBag.EventCat = GetDiscoverEventCategory(strEc);
            if (strEt != null && strEt != string.Empty)
                ViewBag.ETSelected = GetEventTypeSelected(strEt);
            else
                ViewBag.ETSelected = null;

            if (strEc != null && strEc != string.Empty)
                ViewBag.ECatSelected = GetEventCategorySelected(strEc);
            else
                ViewBag.ECatSelected = null;

            TempData["ETypeSelected"] = strEt;
            TempData["ECatSelected"] = strEc;
            TempData["TotalPages"] = lTotalPages;
            TempData["tLat"] = strLat;
            TempData["tLng"] = strLong;
            TempData["tSort"] = strSort;
            TempData["tDateFilter"] = strDateFilter;
            TempData["NearLat"] = strNearLat;
            TempData["NearLong"] = strNearLong;
            TempData["PageIndex"] = (strPageIndex.ToLower() == "page" ? "1" : strPageIndex);
            ViewData["tempPrice"] = (strPrice != null ? strPrice.ToUpper() : "ALL");
            return View();
        }

        public List<EventType> GetDiscoverEventType(string strSelectedTypes)
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                if (strSelectedTypes != null && strSelectedTypes != string.Empty)
                {
                    //List<EventType> objETSelected = GetEventTypeSelected(strSelectedTypes);
                    string[] str = strSelectedTypes.Split(',');
                    long[] iAry = Array.ConvertAll(str, s => long.Parse(s));
                    List<EventType> objETRest = db.EventTypes.Where(e => e.EventHide != "Y" && !iAry.Contains(e.EventTypeID)).OrderBy(m => m.EventType1).ToList();
                    return objETRest;

                }
                else
                {
                    return db.EventTypes.Where(e => e.EventHide != "Y").OrderBy(m => m.EventType1).ToList();
                }
                //if (strSelectedTypes != null && strSelectedTypes != string.Empty)
                //{
                //    var vEt = db.EventTypes.SqlQuery("select * From EventType where EventHide != 'Y' and EventTypeID in (" + strSelectedTypes + ")").Concat(db.EventTypes.SqlQuery("select * From EventType where EventHide != 'Y' and EventTypeID not in (" + strSelectedTypes + ") "));
                //    return vEt.ToList();
                //}
                //else
                //{
                //var vEtype = (from et in db.EventTypes where et.EventHide != "Y" select et).ToList().OrderBy(m => m.EventType1).ToList();
                //if (strSelectedTypes != null && strSelectedTypes != string.Empty)
                //{
                //    List<EventType> objETSelected = new List<EventType>();
                //    List<EventType> objETRest = new List<EventType>();
                //    foreach (EventType ev in vEtype)
                //    {
                //        if (strSelectedTypes.Contains(ev.EventTypeID.ToString()) == true)
                //        {
                //            objETSelected.Add(ev);
                //        }
                //        else
                //        {
                //            objETRest.Add(ev);
                //        }
                //    }
                //    return objETSelected.Concat(objETRest).ToList();
                //}
                //else {

                //    return vEtype;
                //}
            }
        }


        public List<EventType> GetEventTypeSelected(string strSelectedTypes)
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                string[] str = strSelectedTypes.Split(',');
                long[] iAry = Array.ConvertAll(str, s => long.Parse(s));
                return db.EventTypes.Where(e => e.EventHide != "Y" && iAry.Contains(e.EventTypeID)).ToList();
            }
        }
        public List<EventCategory> GetEventCategorySelected(string strSelectedCat)
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                string[] str = strSelectedCat.Split(',');
                long[] iAry = Array.ConvertAll(str, s => long.Parse(s));
                return db.EventCategories.Where(e => iAry.Contains(e.EventCategoryID)).ToList();
            }
        }
        public List<EventCategory> GetDiscoverEventCategory(string strCat)
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                if (strCat != null && strCat != string.Empty)
                {
                    //List<EventType> objETSelected = GetEventTypeSelected(strSelectedTypes);
                    string[] str = strCat.Split(',');
                    long[] iAry = Array.ConvertAll(str, s => long.Parse(s));
                    List<EventCategory> objEcat = db.EventCategories.Where(e => !iAry.Contains(e.EventCategoryID)).OrderBy(m => m.EventCategory1).ToList();
                    return objEcat;

                }
                else
                {
                    return db.EventCategories.OrderBy(m => m.EventCategory1).ToList();
                }
                //var vEventCategory = (from et in db.EventCategories select et).ToList().OrderBy(m => m.EventCategory1);
                //return vEventCategory.ToList();
            }
        }
        public string GetDiscoverEventCategorybyId(long lid)
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                var vEventCategory = (from et in db.EventCategories where et.EventCategoryID == lid select et.EventCategory1).FirstOrDefault();
                return vEventCategory;
            }
        }
        //public string GetDiscoverEventTimings(long lEvent)
        //{

        //    using (EventComboEntities db = new EventComboEntities())
        //    {
        //        var vEventCategory = (from et in db.EventCategories where et.EventCategoryID == lid select et.EventCategory1).FirstOrDefault();
        //        return vEventCategory;
        //    }

        //}


        public string GetDiscoverEventTypebyId(long lid)
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                var vEventType = (from et in db.EventTypes where et.EventTypeID == lid select et.EventType1).FirstOrDefault();
                return vEventType;
            }
        }

        public string GetDiscoverEventFavLikes(long lEvId, string strUserId)
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                var vfav = (from ev in db.EventFavourites where ev.eventId == lEvId && ev.UserID == strUserId select ev.UserID).FirstOrDefault();
                if (vfav != null && vfav.Trim() != "") return "I";
                else return "D";
            }
        }
        public double GetDiscoverEventLatLongDis(double dUserLat, double dUserLong, double dAddLat, double dAddLong)
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                var vDistance = db.GetLantLongDistance(dUserLat, dUserLong, dAddLat, dAddLong).FirstOrDefault();
                if (vDistance != null) return Convert.ToDouble(vDistance);
                else return 0;
            }
        }
        public string GetPriceLabel(long lEventId)
        {
            string strResult = "";

            using (EventComboEntities db = new EventComboEntities())
            {
                var vFree = (from et in db.Tickets where et.E_Id == lEventId && et.TicketTypeID == 1 select et).FirstOrDefault();
                var vDonate = (from et in db.Tickets where et.E_Id == lEventId && et.TicketTypeID == 3 select et).FirstOrDefault();
                var vMinPrice = (from et in db.Tickets where et.E_Id == lEventId && et.TicketTypeID == 2 select et.TotalPrice).Min();
                var vMaxPrice = (from et in db.Tickets where et.E_Id == lEventId && et.TicketTypeID == 2 select et.TotalPrice).Max();

                if (vMaxPrice == null) vMaxPrice = 0;
                if (vMinPrice == null) vMinPrice = 0;
                if (vFree != null && vDonate == null && vMaxPrice == 0 && vMinPrice == 0)
                {
                    strResult = "FREE";
                }
                else if (vFree == null && vDonate != null && vMaxPrice == 0 && vMinPrice == 0)
                {
                    strResult = "DONATE";
                }
                else if (vFree == null && vDonate == null && vMaxPrice == vMinPrice)
                {
                    strResult = "$" + vMaxPrice.ToString();
                }
                else if (vFree == null && vDonate == null && vMaxPrice > vMinPrice)
                {
                    strResult = "$" + vMinPrice.ToString() + " - $" + vMaxPrice.ToString();
                }
                else if (vFree != null && vDonate == null && vMaxPrice > 0)
                {
                    strResult = "$0 - $" + vMaxPrice.ToString();
                }
                else if (vFree != null && vDonate != null && vMaxPrice > 0)
                {
                    strResult = "$0 - $" + vMaxPrice.ToString();
                }
                else if (vFree == null && vDonate != null && vMaxPrice > vMinPrice)
                {
                    strResult = "$" + vMinPrice.ToString() + " - $" + vMaxPrice.ToString();
                }
                else if (vFree != null && vDonate != null && vMaxPrice <= 0)
                {
                    strResult = "FREE";
                }

            }
            return strResult;
        }
        public List<DiscoverEvent> GetDiscoverEventListing(string strEventTypeId, string strEventCatId, string strPrice, string strLat, string strLong, string strSort, string strDateFilter, ref string strNearLat, ref string strNearLong,string strTextSearch)
        {

            List<DiscoverEvent> lsDisEvt = new List<DiscoverEvent>();
            using (EventComboEntities db = new EventComboEntities())
            {
                StringBuilder sbQuery = new StringBuilder();
                if (strEventTypeId == null || strEventTypeId == "~") strEventTypeId = string.Empty;
                if (strEventCatId == null || strEventCatId == "~") strEventCatId = string.Empty;
                if (strPrice == null || strPrice == "~") strPrice = "ALL";
                if (strTextSearch == null) strTextSearch="";
                //var vValue = db.Addresses.SqlQuery("select dbo.distance(28.6139, 77.2090, Latitude, Longitude) discoverdistance,* from Address").Where(m=> (m.discoverdistance!= null ? Convert.ToInt64(m.discoverdistance) : 21)<=20).ToList();
                //var vValue = db.Addresses.SqlQuery("select dbo.distance(28.6139, 77.2090, Latitude, Longitude) discoverdistance,* from Address").ToList();
                //select dbo.distance(28.6139, 77.2090, Latitude, Longitude) dis from Address
                string strEventIds = "";
                strEventIds = db.GetLantLong(strLat, strLong).FirstOrDefault();
                //foreach (Address objAdd in vValue)
                //{
                //    if (objAdd.discoverdistance != null && Convert.ToInt32(objAdd.discoverdistance) <=20)
                //    {
                //        if (strEventIds == "")
                //        {
                //            strEventIds = objAdd.EventId.ToString();
                //        }
                //        else
                //        {
                //            strEventIds = strEventIds + "," + objAdd.EventId.ToString();
                //        }
                //    }
                //}

                DiscoverEvent objDisEv = new DiscoverEvent();


                //sbQuery.Append("Select * from Event where EventID in (" + strEventIds + ")");

                if (strEventIds.Trim() != "")
                {
                    //sbQuery.Append("Select * from Event where EventStatus = 'Live' and isnull(Parent_EventID,0) = 0");
                    sbQuery.Append("Select * from Event where EventStatus = 'Live' "); // No need of Parent Event check,  as we already filter address and address table only have latest event id's
                    if (strEventTypeId.Trim() != string.Empty)
                        sbQuery.Append(" AND EventTypeID in (" + strEventTypeId + ")");

                    if (strEventCatId.Trim() != string.Empty)
                        sbQuery.Append(" AND EventCategoryID in (" + strEventCatId + ")");

                    //if (strTextSearch.Trim() != string.Empty)
                    //{
                    //    sbQuery.Append(" AND EventTitle like %" + strTextSearch + "%");
                    //    sbQuery.Append(" AND EventTitle like %" + strTextSearch + "%");
                    //}

                    if (strPrice.ToUpper() == "FREE")
                    {
                        sbQuery.Append(" AND EventID in (select E_Id from Ticket where TicketTypeID in (1,3) and E_Id in ( " + strEventIds + "))");
                    }
                    else if (strPrice.ToUpper() == "PAID")
                    {
                        sbQuery.Append(" AND EventID in (select E_Id from Ticket where TicketTypeID in (2,3) and E_Id in ( " + strEventIds + "))");
                    }
                    else
                    {
                        sbQuery.Append(" and EventID in (" + strEventIds + ")");
                    }

                    var vEventList = db.Events.SqlQuery(sbQuery.ToString()).ToList();
                    CreateEventController objCEv = new CreateEventController();

                    string strImageUrl = "";
                    ValidationMessageController vmc = new ValidationMessageController();
                    string strUserId = "";
                    if (Session["AppId"] != null && Session["AppId"].ToString() != string.Empty) strUserId = Session["AppId"].ToString();
                    bool bflag = true;
                    foreach (Event objEv in vEventList)
                    {
                        long lEventId = vmc.GetLatestEventId(objEv.EventID);
                        //if (strPrice.ToUpper() == "FREE")
                        //{
                        //    var vTicket = db.Tickets.Where(ev => ev.TicketTypeID == 1).FirstOrDefault();
                        //    if (vTicket == null) continue;
                        //}
                        //if (strPrice.ToUpper() == "PAID")
                        //{

                        //    var vTicket = db.Tickets.Where(ev => ev.TicketTypeID == 2).FirstOrDefault();
                        //    var vDontion = db.Tickets.Where(ev => ev.TicketTypeID == 3).FirstOrDefault();
                        //    if (vTicket == null && vDontion == null) continue;
                        //}



                        strImageUrl = objCEv.GetImages(lEventId).FirstOrDefault();

                        if (strImageUrl != null && strImageUrl != "")
                        {
                            if (!System.IO.File.Exists(Server.MapPath(strImageUrl)))
                                strImageUrl = "/Images/default_event_image.jpg";
                        }
                        else
                            strImageUrl = "/Images/default_event_image.jpg";

                        objDisEv = new DiscoverEvent();
                        objDisEv.EventId = lEventId;
                        objDisEv.EventTitle = objEv.EventTitle;
                        if (objDisEv.EventTitle.Length > 57)
                        {
                            objDisEv.EventTitle = objDisEv.EventTitle.Substring(0, 54) + "...";
                        }
                        objDisEv.EventImage = strImageUrl;
                        objDisEv.EventCat = GetDiscoverEventCategorybyId(objEv.EventCategoryID);
                        if (objDisEv.EventCat.Length > 20)
                        {
                            objDisEv.EventCat = objDisEv.EventCat.Substring(0, 16) + "...";
                        }
                        objDisEv.EventType = GetDiscoverEventTypebyId(objEv.EventTypeID);
                        if (objDisEv.EventType.Length > 20)
                        {
                            objDisEv.EventType = objDisEv.EventType.Substring(0, 16) + "...";
                        }


                        objDisEv.EventCatId = objEv.EventCategoryID;
                        objDisEv.EventTypeId = objEv.EventTypeID;
                        objDisEv.PriceLable = GetPriceLabel(lEventId);
                        objDisEv.EventLike = GetDiscoverEventFavLikes(lEventId, strUserId);
                        var vAddress = objEv.Addresses.FirstOrDefault();
                        objDisEv.EventDistance = GetDiscoverEventLatLongDis(Convert.ToDouble(strLat), Convert.ToDouble(strLong), Convert.ToDouble(vAddress.Latitude), Convert.ToDouble(vAddress.Longitude));
                        if (vAddress != null)
                        {
                            if (vAddress.ConsolidateAddress.Trim() != string.Empty)
                            {
                                objDisEv.EventAddress = vAddress.ConsolidateAddress;
                            }
                            else
                            {
                                objDisEv.EventAddress = vAddress.VenueName.Trim() + " " + vAddress.Address1.Trim() + " " + vAddress.Address2.Trim() + " " + vAddress.City.Trim() + " " + vAddress.Zip;
                            }
                            objDisEv.EventDisplayAddress = objDisEv.EventAddress;
                        }
                        if (objDisEv.EventAddress.Length > 140)
                        {
                            objDisEv.EventAddress = objDisEv.EventAddress.Substring(0, 135) + "...";
                        }

                        if (bflag == true)
                        {
                            strNearLat = vAddress.Latitude;
                            strNearLong = vAddress.Longitude;
                            bflag = false;
                        }

                        var vTimings = objEv.EventVenues.FirstOrDefault();
                        if (vTimings != null)
                        {
                            objDisEv.EventTimings = Convert.ToDateTime(vTimings.EventStartDate).ToString("ddd MMM dd, yyyy") + " " + vTimings.EventStartTime;
                            objDisEv.EventDate = (vTimings.EventStartDate != null ? Convert.ToDateTime(vTimings.EventStartDate + " " + vTimings.EventStartTime) : DateTime.Now);
                        }
                        else
                        {
                            long lCont = 0;
                            lCont = (from evt in db.Publish_Event_Detail where evt.PE_Event_Id == lEventId select evt.PE_Id).Count();
                            long? lMin = 0; long? lMax = 0;
                            if (lCont > 0)
                            {
                                lMin = (from evt in db.Publish_Event_Detail where evt.PE_Event_Id == lEventId select evt.PE_Id).Min();
                                lMax = (from evt in db.Publish_Event_Detail where evt.PE_Event_Id == lEventId select evt.PE_Id).Max();
                            }


                            string strTiming = "";
                            if (lMin != null && lMin > 0)
                            {
                                var vMin = (from evt in db.Publish_Event_Detail where evt.PE_Id == lMin select evt).FirstOrDefault();
                                strTiming = vMin.PE_Scheduled_Date + " " + vMin.PE_Start_Time;
                                objDisEv.EventDate = (vMin.PE_Scheduled_Date != null ? Convert.ToDateTime(vMin.PE_Scheduled_Date) : DateTime.Now);
                            }
                            if (lMax != null && lMax > 0)
                            {
                                var vMax = (from evt in db.Publish_Event_Detail where evt.PE_Id == lMax select evt).FirstOrDefault();
                                strTiming = strTiming + " - " + vMax.PE_Scheduled_Date + " " + vMax.PE_End_Time;
                            }
                            objDisEv.EventTimings = strTiming;
                        }

                        lsDisEvt.Add(objDisEv);
                    }
                    if (strSort == "dat") lsDisEvt = lsDisEvt.OrderBy(m => m.EventDate).ToList();
                    else lsDisEvt = lsDisEvt.OrderBy(m => m.EventDistance).ToList();
                    if (strTextSearch.Trim() != string.Empty)
                    {
                        lsDisEvt = lsDisEvt.Where(m => m.EventTitle.Contains(strTextSearch) || m.EventCat.Contains(strTextSearch) || m.EventDisplayAddress.Contains(strTextSearch)).ToList();
                    }
                    try
                    {
                        if (strDateFilter == "today") lsDisEvt = lsDisEvt.Where(m => m.EventDate >= DateTime.Now && m.EventDate.Date == DateTime.Today.Date).ToList();
                        if (strDateFilter == "tomorrow") lsDisEvt = lsDisEvt.Where(m => m.EventDate.Date == DateTime.Today.AddDays(1).Date).ToList();
                        if (strDateFilter == "thisweek")
                        {
                            int iday = (int)DateTime.Now.DayOfWeek;
                            int iLen = (7 - iday) + 1;
                            string strDates = "";
                            DateTime[] dt = new DateTime[iLen];
                            for (int i = 0; i < iLen; i++)
                            {
                                if (i == 0)
                                    dt[i] = DateTime.Now.Date;
                                else
                                    dt[i] = dt[i - 1].AddDays(1).Date;
                            }
                            lsDisEvt = lsDisEvt.Where(m => dt.Contains(m.EventDate.Date)).ToList();
                        }
                        if (strDateFilter == "thisweekend")
                        {
                            int iday = (int)DateTime.Now.DayOfWeek;
                            int iLen = (7 - iday) + 1;
                            int iTemp = iLen;
                            string strDates = "";
                            if (iLen > 3) iTemp = 3;
                            DateTime[] dt = new DateTime[iTemp];
                            DateTime dttemp = DateTime.Now.AddDays(-1);
                            iTemp = 0;
                            for (int i = iday; i <= 7; i++)
                            {
                                dttemp = dttemp.AddDays(1);
                                if (i == 5)
                                    dt[iTemp] = dttemp.Date;
                                else if (i > 5)
                                {
                                    if (iday > 5)
                                    {
                                        dt[iTemp] = dt[iTemp].AddDays(1).Date;
                                    }
                                    else
                                    {
                                        iTemp = iTemp + 1;
                                        dt[iTemp] = dt[iTemp - 1].AddDays(1).Date;
                                    }
                                }
                            }
                            lsDisEvt = lsDisEvt.Where(m => dt.Contains(m.EventDate.Date)).ToList();
                        }
                        if (strDateFilter == "nextweek")
                        {
                            int iday = (int)DateTime.Now.DayOfWeek;
                            DateTime dtnow = DateTime.Now;
                            dtnow = dtnow.AddDays(7 - iday);
                            DateTime[] dt = new DateTime[7];

                            for (int i = 0; i <= 6; i++)
                            {
                                if (i == 0)
                                    dt[i] = dtnow.AddDays(1).Date;
                                else
                                    dt[i] = dt[i - 1].AddDays(1).Date;
                            }

                            lsDisEvt = lsDisEvt.Where(m => dt.Contains(m.EventDate.Date)).ToList();
                        }

                        if (strDateFilter == "thismonth")
                        {
                            int iMonthdays = (int)DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                            int idays = (int)DateTime.Now.Day;
                            int iRemDays = (iMonthdays - idays) + 1;
                            DateTime dtnow = DateTime.Now.AddDays(-1);
                            DateTime[] dt = new DateTime[iRemDays];
                            for (int i = 0; i < iRemDays; i++)
                            {
                                if (i == 0)
                                    dt[i] = dtnow.AddDays(1).Date;
                                else
                                    dt[i] = dt[i - 1].AddDays(1).Date;
                            }

                            lsDisEvt = lsDisEvt.Where(m => dt.Contains(m.EventDate.Date)).ToList();
                        }

                        string[] str = strDateFilter.Split('@');
                        if (str.Length > 0 && str[0].ToLower().Trim() == "custom")
                        {
                            if (str[1].Trim() != "" && str[2].Trim() != "")
                            {

                                DateTime dtFrom = Convert.ToDateTime(str[1].ToString());
                                DateTime dtTo = Convert.ToDateTime(str[2].ToString());
                                lsDisEvt = lsDisEvt.Where(m => m.EventDate.Date >= dtFrom.Date && m.EventDate.Date <= dtTo.Date).ToList();
                            }
                            else if (str[1].Trim() != "")
                            {
                                DateTime dtFrom = Convert.ToDateTime(str[1]);
                                lsDisEvt = lsDisEvt.Where(m => m.EventDate.Date >= dtFrom.Date).ToList();
                            }
                            else if (str[2].Trim() != "")
                            {
                                DateTime dtTo = Convert.ToDateTime(str[2]);
                                lsDisEvt = lsDisEvt.Where(m => m.EventDate.Date <= dtTo.Date).ToList();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        lsDisEvt = null;
                    }
                }
                return lsDisEvt;
            }
        }


        public List<DiscoverEvent> GetHomePageEventListing(string strEventTypeId, string strEventCatId, string strPrice, string strLat, string strLong, string strSort, string strDateFilter, ref string strNearLat, ref string strNearLong)
        {

            List<DiscoverEvent> lsDisEvt = new List<DiscoverEvent>();
            using (EventComboEntities db = new EventComboEntities())
            {
                StringBuilder sbQuery = new StringBuilder();
                if (strEventTypeId == null || strEventTypeId == "~") strEventTypeId = string.Empty;
                if (strEventCatId == null || strEventCatId == "~") strEventCatId = string.Empty;
                if (strPrice == null || strPrice == "~") strPrice = "ALL";
                string strEventIds = "";
                strEventIds = db.GetLantLong(strLat, strLong).FirstOrDefault();
                DiscoverEvent objDisEv = new DiscoverEvent();

                if (strEventIds.Trim() != "")
                {
                    sbQuery.Append("Select * from Event where EventStatus = 'Live' and isnull(Parent_EventID,0) = 0");
                    sbQuery.Append(" and EventID in (" + strEventIds + ")");

                    var vEventList = db.Events.SqlQuery(sbQuery.ToString()).ToList();
                    CreateEventController objCEv = new CreateEventController();
                    string strImageUrl = "";
                    ValidationMessageController vmc = new ValidationMessageController();
                    string strUserId = "";
                    if (Session["AppId"] != null && Session["AppId"].ToString() != string.Empty) strUserId = Session["AppId"].ToString();
                    bool bflag = true;
                    foreach (Event objEv in vEventList)
                    {
                        long lEventId = vmc.GetLatestEventId(objEv.EventID);
                        strImageUrl = objCEv.GetImages(lEventId).FirstOrDefault();

                        if (strImageUrl != null && strImageUrl != "")
                        {
                            if (!System.IO.File.Exists(Server.MapPath(strImageUrl)))
                                strImageUrl = "/Images/default_event_image.jpg";
                        }
                        else
                            strImageUrl = "/Images/default_event_image.jpg";

                        objDisEv = new DiscoverEvent();
                        objDisEv.EventId = lEventId;
                        objDisEv.EventTitle = objEv.EventTitle;
                        if (objDisEv.EventTitle.Length > 57)
                        {
                            objDisEv.EventTitle = objDisEv.EventTitle.Substring(0, 54) + "...";
                        }
                        objDisEv.EventImage = strImageUrl;
                        objDisEv.EventCat = GetDiscoverEventCategorybyId(objEv.EventCategoryID);
                        if (objDisEv.EventCat.Length > 20)
                        {
                            objDisEv.EventCat = objDisEv.EventCat.Substring(0, 16) + "...";
                        }
                        objDisEv.EventType = GetDiscoverEventTypebyId(objEv.EventTypeID);
                        if (objDisEv.EventType.Length > 20)
                        {
                            objDisEv.EventType = objDisEv.EventType.Substring(0, 16) + "...";
                        }


                        objDisEv.EventCatId = objEv.EventCategoryID;
                        objDisEv.EventTypeId = objEv.EventTypeID;
                        objDisEv.PriceLable = GetPriceLabel(lEventId);
                        objDisEv.EventLike = GetDiscoverEventFavLikes(lEventId, strUserId);
                        objDisEv.EventFeature = (objEv.Feature != null ? Convert.ToInt16(objEv.Feature) : 10); // 10 - becz if feature is null then that event have to show at last according to feature sorting 
                        var vAddress = objEv.Addresses.FirstOrDefault();
                        objDisEv.EventDistance = GetDiscoverEventLatLongDis(Convert.ToDouble(strLat), Convert.ToDouble(strLong), Convert.ToDouble(vAddress.Latitude), Convert.ToDouble(vAddress.Longitude));
                        if (vAddress != null)
                        {
                            if (vAddress.ConsolidateAddress.Trim() != string.Empty)
                            {
                                objDisEv.EventAddress = vAddress.ConsolidateAddress;
                            }
                            else
                            {
                                objDisEv.EventAddress = vAddress.VenueName.Trim() + " " + vAddress.Address1.Trim() + " " + vAddress.Address2.Trim() + " " + vAddress.City.Trim() + " " + vAddress.Zip;
                            }
                            objDisEv.EventDisplayAddress = objDisEv.EventAddress;
                        }
                        if (objDisEv.EventAddress.Length > 140)
                        {
                            objDisEv.EventAddress = objDisEv.EventAddress.Substring(0, 135) + "...";
                        }

                        if (bflag == true)
                        {
                            strNearLat = vAddress.Latitude;
                            strNearLong = vAddress.Longitude;
                            bflag = false;
                        }

                        var vTimings = objEv.EventVenues.FirstOrDefault();
                        if (vTimings != null)
                        {
                            objDisEv.EventTimings = Convert.ToDateTime(vTimings.EventStartDate).ToString("ddd MMM dd, yyyy") + " " + vTimings.EventStartTime;
                            objDisEv.EventDate = (vTimings.EventStartDate != null ? Convert.ToDateTime(vTimings.EventStartDate + " " + vTimings.EventStartTime) : DateTime.Now);
                        }
                        else
                        {
                            long lCont = 0;
                            lCont = (from evt in db.Publish_Event_Detail where evt.PE_Event_Id == lEventId select evt.PE_Id).Count();
                            long? lMin = 0; long? lMax = 0;
                            if (lCont > 0)
                            {
                                lMin = (from evt in db.Publish_Event_Detail where evt.PE_Event_Id == lEventId select evt.PE_Id).Min();
                                lMax = (from evt in db.Publish_Event_Detail where evt.PE_Event_Id == lEventId select evt.PE_Id).Max();
                            }


                            string strTiming = "";
                            if (lMin != null && lMin > 0)
                            {
                                var vMin = (from evt in db.Publish_Event_Detail where evt.PE_Id == lMin select evt).FirstOrDefault();
                                strTiming = vMin.PE_Scheduled_Date + " " + vMin.PE_Start_Time;
                                objDisEv.EventDate = (vMin.PE_Scheduled_Date != null ? Convert.ToDateTime(vMin.PE_Scheduled_Date) : DateTime.Now);
                            }
                            if (lMax != null && lMax > 0)
                            {
                                var vMax = (from evt in db.Publish_Event_Detail where evt.PE_Id == lMax select evt).FirstOrDefault();
                                strTiming = strTiming + " - " + vMax.PE_Scheduled_Date + " " + vMax.PE_End_Time;
                            }
                            objDisEv.EventTimings = strTiming;
                        }

                        lsDisEvt.Add(objDisEv);
                    }
                    lsDisEvt = lsDisEvt.OrderBy(m => m.EventDistance).ToList().OrderBy(m => m.EventFeature).ToList();
                }
                return lsDisEvt;
            }
        }

        public ActionResult DiscoverEventsTiles()
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
            if (Session["AppId"] != null)
            {
                string var = getusername();
                if (string.IsNullOrEmpty(var))
                {
                    Session["AppId"] = null;
                }
            }
     
            return View();

        }


        public ActionResult HomeEventList(string strPageIndex, string strLat, string strLong)
        {

            int pageSize = 15;
            int pageIndex = 1;
            if (strPageIndex != null && strPageIndex != string.Empty && strPageIndex != "page")
                pageIndex = Convert.ToInt32(strPageIndex);
            string strNearLat = "";
            string strNearLong = "";
            List<DiscoverEvent> objDiscEvt = GetHomePageEventListing("", "", "all", strLat, strLong, "rel", "none", ref strNearLat, ref strNearLong);
            double dPageCount = objDiscEvt.Count;
            double dTotalPages = dPageCount / pageSize;
            int lTotalPages = (objDiscEvt.Count / pageSize);
            if (dTotalPages.ToString().Contains(".") == true)
                lTotalPages = lTotalPages + 1;
            ViewBag.DisEvnt = objDiscEvt.ToPagedList(pageIndex, pageSize);

            TempData["TotalPages"] = lTotalPages;
            TempData["tLat"] = strLat;
            TempData["tLng"] = strLong;
            TempData["NearLat"] = strNearLat;
            TempData["NearLong"] = strNearLong;
            TempData["PageIndex"] = (strPageIndex.ToLower() == "page" ? "1" : strPageIndex);
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
                return RedirectToAction("Index", "Home");

            }
            else
            {
                Session["code"] = code;
                return View();

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
            if (model.Password != model.ConfirmPassword)
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
            if (profile != null)
            {
                username = !string.IsNullOrEmpty(profile.FirstName) ? profile.FirstName : "";

            }

            //string readFile = "";
            // using (StreamReader reader = new StreamReader(Server.MapPath("~/ForgotPassword.html")))
            // {
            //     readFile = reader.ReadToEnd();
            // }
            var url = Request.Url;
            var baseurl = url.GetLeftPart(UriPartial.Authority);
            string url1 = baseurl + Url.Action("PasswordReset", "Home") + "?code=" + id + " ";

            // string myString = "";
            // myString = readFile;
            // myString = myString.Replace("¶¶Email¶¶", model.Email);
            // myString = myString.Replace("¶¶Website¶¶", url1);

            // SendMail(model.Email, myString.ToString(), "The Eventcombo Team");

            /// to send email////
            /// 
            string to = "", from = "", cc = "", bcc = "", subjectn = "", emailname = "";
            var bodyn = "";
            List<Email_Tag> EmailTag = new List<Email_Tag>();
            EmailTag = getTag();

            var Emailtemplate = getEmail("email_lost_pwd");
            if (Emailtemplate != null)
            {

                string tag = "UserEmailID:" + model.Email + "¶" + "UserFirstNameID:" + username + "ResetPwdUrl:" + url1;
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
                if (!(string.IsNullOrEmpty(Emailtemplate.From_Name)))
                {
                    emailname = Emailtemplate.From_Name;
                }
                else
                {
                    emailname = from;
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
                            // All tags




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
                                bodyn = subjectn.Replace("¶¶EventAddressID¶¶", "");

                            }
                            if (EmailTag[i].Tag_Name == "TicketOrderNumberID")
                            {
                                bodyn = bodyn.Replace("¶¶TicketOrderNumberID¶¶", "");

                            }
                            if (EmailTag[i].Tag_Name == "DealOrderNumberID")
                            {
                                bodyn = bodyn.Replace("¶¶DealOrderNumberID¶¶", "");

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
                                bodyn = subjectn.Replace("¶¶TicketOrderDateId¶¶", "");

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
                SendHtmlFormattedEmail(to, from, subjectn, bodyn, cc, bcc, tag, emailname);
            }
            ValidationMessageController vmc = new ValidationMessageController();
            var msg = vmc.Index("ForgotPassword", "ForgotPwdSuccessInitSY");
            ViewData["Message"] = msg;
            return View();
        }

        public JsonResult Getuserdetails(string Email)
        {
            string message = "";

            var user = (from Org in db.Profiles
                        join pfd in db.AspNetUsers on Org.UserID equals pfd.Id
                        where pfd.Email == Email
                        select Org).FirstOrDefault();
            if (user!=null)
            {
                message = "F";
                return Json(new { Message = message,Fname= user.FirstName,Lname=user.LastName });
            }
            else
            {
                message = "N";

                return Json(new { Message = message, Fname = "", Lname = ""});
            }
           

        }
        public void SendMail(string toaddress, string messagebody, string messageSubject)
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

            if (string.IsNullOrEmpty(result))
            { Session["AppId"] = null; }
            return Content(result);
        }
        public void setsessid(string id)
        {
            if (id == "discover")
            {



                Session["ReturnUrl"] = Url.Action("DiscoverEvents", "Home");

            }

            if (id == "GetBuzz")
            {
                Session["ReturnUrl"] = Url.Action("GetBuzz", "Home");



            }



        }
        public string checkid()
        {
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



        [HttpPost]
        [AllowAnonymous]

        public async Task<ActionResult> Signup(LoginViewModel model)
        {
            string city = "", state = "", country = "", zipcode = "";
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
                String url1 = String.Empty;
                var Userid = UserManager.FindByEmail(model.Email);


                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    await this.UserManager.AddToRoleAsync(user.Id, "Member");

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
                        Profile prof = new Profile();

                        prof.Email = model.Email;

                        prof.UserID = Userid.Id.ToString();
                        prof.UserStatus = "Y";
                        prof.Ipcountry = country;
                        prof.IpState = state;
                        prof.Ipcity = city;
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
                        string to = "", from = "", cc = "", bcc = "", subjectn = "", emailname = "";
                        var bodyn = "";
                        List<Email_Tag> EmailTag = new List<Email_Tag>();
                        EmailTag = getTag();
                        string tag = "UserEmailID:" + model.Email;
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
                        if (!string.IsNullOrEmpty(Emailtemplate.Subject))
                        {


                            subjectn = Emailtemplate.Subject;


                        }
                        if (!string.IsNullOrEmpty(Emailtemplate.TemplateHtml))
                        {
                            bodyn = new MvcHtmlString(HttpUtility.HtmlDecode(Emailtemplate.TemplateHtml)).ToHtmlString();





                        }
                        SendHtmlFormattedEmail(to, from, subjectn, bodyn, cc, bcc, tag, emailname);


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
        public String GetLanIPAddress()
        {
            //The X-Forwarded-For (XFF) HTTP header field is a de facto standard for identifying the originating IP address of a
            //client connecting to a web server through an HTTP proxy or load balancer
            string strIpAddress;
            strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (strIpAddress == null)
            {
                strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            return strIpAddress;
        }
        public void SendHtmlFormattedEmail(string To, string from, string subject, string body, string cc, string bcc, string tags, string emailname)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(from, emailname);
                string[] arr = tags.Split('¶');
                int length = arr.Length;
                List<Email_Tag> EmailTag = new List<Email_Tag>();
                EmailTag = getTag();

                if (!string.IsNullOrEmpty(subject) && subject != null)
                {


                    for (int j = 0; j < length; j++)
                    {
                        for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
                        {
                            string[] arrtag = arr[j].Split(':');
                            if (arrtag[0] == EmailTag[i].Tag_Name)
                            {
                                if (subject.Contains(EmailTag[i].Tag_Name))
                                {
                                    subject = subject.Replace("¶¶" + EmailTag[i].Tag_Name + "¶¶", arrtag[1]);
                                }
                            }
                        }
                    }
                    for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
                    {
                        if (subject.Contains(EmailTag[i].Tag_Name))
                        {
                            subject = subject.Replace("¶¶" + EmailTag[i].Tag_Name + "¶¶", "");
                        }
                    }



                }
                if (body != null && !string.IsNullOrEmpty(body))
                {
                    for (int j = 0; j < length; j++)
                    {
                        for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
                        {

                            string[] arrtag = arr[j].Split(':');
                            if (arrtag[0] == EmailTag[i].Tag_Name)
                            {
                                if (body.Contains(EmailTag[i].Tag_Name))
                                {
                                    body = body.Replace("¶¶" + EmailTag[i].Tag_Name + "¶¶", arrtag[1]);
                                }
                            }
                        }
                    }
                    for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
                    {
                        if (body.Contains(EmailTag[i].Tag_Name))
                        {
                            body = body.Replace("¶¶" + EmailTag[i].Tag_Name + "¶¶", "");
                        }
                    }

                }
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
        public void SendHtmlFormattedEmail(string To, string from, string subject, string body, string cc, string bcc, MemoryStream attachment, string emailname, string qrimage, string brcode, List<TicketBearer> GuestList)
        {
            MailMessage mailMessage = new MailMessage();

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
            if (attachment != null)
            {
                if (attachment.Length != 0)
                {
                    System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(attachment, ct);
                    attach.ContentDisposition.FileName = "Ticket_EventCombo.pdf";
                    mailMessage.Attachments.Add(attach);
                }
            }
            mailMessage.IsBodyHtml = true;
            //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
            //mailMessage.AlternateViews.Add(htmlView);

            //Add Image
            //LinkedResource theEmailImage = new LinkedResource(ImageMapPath);
            //theEmailImage.ContentId = "myeventmapImageID";
            //htmlView.LinkedResources.Add(theEmailImage);


            ////LinkedResource theQrImage = new LinkedResource(qrimage);

            ////theQrImage.ContentId = "myQrcodeImageID";
            ////htmlView.LinkedResources.Add(theQrImage);


            ////LinkedResource thebarImage = new LinkedResource(brcode);

            ////thebarImage.ContentId = "myBarcodeImageID";
            ////htmlView.LinkedResources.Add(thebarImage);

            //LinkedResource theeventImage = new LinkedResource(Imageevent);

            //theeventImage.ContentId = "myeventImageID";
            //htmlView.LinkedResources.Add(theeventImage);


            mailMessage.To.Add(new MailAddress(To));
            if (GuestList != null)
            {
                foreach (var item in GuestList)
                {
                    mailMessage.To.Add(new MailAddress(item.Email, item.Name));
                }
            }
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