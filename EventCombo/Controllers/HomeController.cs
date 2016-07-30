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
using EventCombo.Utils;
using EventCombo.ViewModels;
using NLog;

namespace EventCombo.Controllers
{

    public class HomeController : Controller
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
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


            MyAccount acc = new MyAccount();
            try
            {

                Ip2Geo ip2Geo = new Ip2Geo();
                GeoAddress geoAddress = ip2Geo.GetAddress(ClientIPAddress.GetLanIPAddress(Request));
                city = geoAddress.cityName;
                country = geoAddress.countryName;
                zipcode = geoAddress.zipCode;
                state = geoAddress.regionName;
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
                                prof.Organiser = "Y";

                                objEntity.Profiles.Add(prof);


                                AspNetUser aspuser = db.AspNetUsers.First(i => i.Id == user1.Id);
                                aspuser.LoginStatus = "Y";
                                aspuser.LastLoginTime = System.DateTime.UtcNow;

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
                    aspuser.LastLoginTime = System.DateTime.UtcNow;
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
        public string Discoversavefavourite(long Eventid, string strUrl, string strType = "")
        {

            if (Session["AppId"] != null)
            {
                using (EventComboEntities objEnt = new EventComboEntities())
                {
                    long? lEventid = (Eventid != 0 ? Convert.ToInt64(Eventid) : 0);
                    string strUserId = Session["AppId"].ToString();
                    var vfav = (from ev in db.EventFavourites where ev.eventId == lEventid && ev.UserID == strUserId select ev.UserID).FirstOrDefault();
                    if (vfav != null && vfav.Trim() != "")
                    {
                        if (strType == "")
                        {
                            var userid = Session["AppId"].ToString().Trim();
                            objEnt.Database.ExecuteSqlCommand("Delete from EventFavourite where UserID='" + userid + "' AND eventId=" + Eventid + "");
                            objEnt.SaveChanges();
                        }
                        return "D";

                    }
                    else
                    {
                        EventFavourite ObjEC = new EventFavourite();
                        ObjEC.eventId = lEventid;
                        ObjEC.UserID = Session["AppId"].ToString();
                        objEnt.EventFavourites.Add(ObjEC);
                        objEnt.SaveChanges();
                        return "I";
                    }
                }

            }
            else
            {
                //string url = strUrl;
                var url = Request.Url;
                var baseurl = url.GetLeftPart(UriPartial.Authority);
                strUrl = strUrl.Replace(baseurl, "");

                Session["ReturnUrl"] = Eventid.ToString() + "~" + strUrl;
                return "Y";

            }
        }

        public ActionResult DiscoverEvents(string strEt, string strEc, string strPrice, string strPageIndex, string strLat, string strLong, string strSort, string strDateFilter, string strTextSearch)
        {
            if (strLat == "lat")
            {
                try
                {
                    Ip2Geo ip2Geo = new Ip2Geo();
                    GeoAddress geoAddress = ip2Geo.GetAddress(ClientIPAddress.GetLanIPAddress(Request));
                    strLat = geoAddress.latitude;
                    strLong = geoAddress.longitude;
                    if (string.IsNullOrEmpty(strLat) || strLat == "0")
                    {
                        strLat = "28.6139";
                        strLong = "77.2090";
                    }
                }
                catch (Exception)
                {
                    strLat = "28.6139";
                    strLong = "77.2090";
                }
            }

            MyAccount hmc = new MyAccount();
            if ((Session["AppId"] != null))
            {
                string User = Session["AppId"].ToString();
                using (EventComboEntities db = new EventComboEntities())
                {
                    AspNetUser aspuser = db.AspNetUsers.First(i => i.Id == User);

                    aspuser.LastLoginTime = System.DateTime.UtcNow;
                    db.SaveChanges();


                }

                string usernme = hmc.getusername();
                if (string.IsNullOrEmpty(usernme))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            string strUrl = @Url.RouteUrl("EvType", new { strEt = strEt, strEc = strEc, strPrice = strPrice, strPageIndex = strPageIndex, strLat = strLat, strLong = strLong, strSort = strSort, strDateFilter = strDateFilter });

            try
            {
                if (Session["ReturnUrl"] != null)
                {
                    string[] str = Session["ReturnUrl"].ToString().Split('~');
                    long lEventId = Convert.ToInt32(str[0].ToString());
                    if (lEventId > 0)
                    {
                        Discoversavefavourite(lEventId, strUrl, "FromLogin");
                    }
                }
                else
                {
                    Session["ReturnUrl"] = "0~" + strUrl;
                }
            }
            catch (Exception)
            {
                Session["ReturnUrl"] = "0~" + strUrl;

            }




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

            List<DiscoverEvent> objDiscEvt = GetDiscoverEventListing(strEt, strEc, strPrice, strLat, strLong, strSort, strDateFilter, ref strNearLat, ref strNearLong, strTextSearch);
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




            TempData["SearchText"] = strTextSearch;
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
        public List<DiscoverEvent> GetDiscoverEventListing(string strEventTypeId, string strEventCatId, string strPrice, string strLat, string strLong, string strSort, string strDateFilter, ref string strNearLat, ref string strNearLong, string strTextSearch)
        {

            List<DiscoverEvent> lsDisEvt = new List<DiscoverEvent>();
            EventCreation cs = new EventCreation();
            ValidationMessage vmc = new ValidationMessage();
            using (EventComboEntities db = new EventComboEntities())
            {
                StringBuilder sbQuery = new StringBuilder();
                if (strEventTypeId == null || strEventTypeId == "~") strEventTypeId = string.Empty;
                if (strEventCatId == null || strEventCatId == "~") strEventCatId = string.Empty;
                if (strPrice == null || strPrice == "~") strPrice = "ALL";
                if (strTextSearch == null) strTextSearch = "";
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
                    sbQuery.Append("Select * from Event where EventStatus = 'Live' and rtrim(ltrim(lower(EventPrivacy))) != 'private'"); // No need of Parent Event check,  as we already filter address and address table only have latest event id's
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


                    string strImageUrl = "";

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



                        strImageUrl = cs.GetImages(lEventId).FirstOrDefault();

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
                        objDisEv.EventPrivacy = "Y";
                        if (objEv.EventPrivacy.Trim().ToLower() == "private" && objEv.Private_ShareOnFB.Trim() == "N")
                        {
                            objDisEv.EventPrivacy = "N";
                        }

                        objDisEv.EventCatId = objEv.EventCategoryID;
                        objDisEv.EventTypeId = objEv.EventTypeID;
                        objDisEv.PriceLable = GetPriceLabel(lEventId);
                        objDisEv.EventLike = GetDiscoverEventFavLikes(lEventId, strUserId);
                        var vAddress = objEv.Addresses.FirstOrDefault();
                        if (vAddress != null)
                        {
                            objDisEv.EventDistance = GetDiscoverEventLatLongDis(Convert.ToDouble((strLat != "" ? strLat : "0")), Convert.ToDouble((strLong != "" ? strLong : "0")), Convert.ToDouble((vAddress.Latitude != "" ? vAddress.Latitude : "0")), Convert.ToDouble((vAddress.Longitude != "" ? vAddress.Longitude : "0")));
                            if (vAddress.ConsolidateAddress.Trim() != string.Empty)
                            {
                                objDisEv.EventAddress = vAddress.ConsolidateAddress;
                            }
                            else
                            {
                                objDisEv.EventAddress = vAddress.VenueName.Trim() + " " + vAddress.Address1.Trim() + " " + vAddress.Address2.Trim() + " " + vAddress.City.Trim() + " " + vAddress.Zip;
                            }
                            objDisEv.EventDisplayAddress = objDisEv.EventAddress;
                            if (objDisEv.EventAddress.Length > 140)
                            {
                                objDisEv.EventAddress = objDisEv.EventAddress.Substring(0, 135) + "...";
                            }
                        }
                        else
                        {
                            objDisEv.EventDistance = double.MaxValue;
                            objDisEv.EventAddress = "Online";
                        }

                        if (bflag == true)
                        {
                            strNearLat = vAddress.Latitude;
                            strNearLong = vAddress.Longitude;
                            bflag = false;
                        }
                        var Timezonedetail = (from ev in db.TimeZoneDetails where ev.TimeZone_Id.ToString() == objEv.TimeZone select ev).FirstOrDefault();
                        DateTimeWithZone dtzstart, dzend, dtznewstart, dtzCreated;


                        var vTimings = objEv.EventVenues.FirstOrDefault();
                        if (vTimings != null)
                        {
                            if (Timezonedetail != null)
                            {
                                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Timezonedetail.TimeZone);
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(vTimings.E_Startdate), userTimeZone, true);
                                dzend = new DateTimeWithZone(Convert.ToDateTime(vTimings.E_Enddate), userTimeZone, true);
                            }
                            else
                            {
                                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(vTimings.E_Startdate), userTimeZone, true);
                                dzend = new DateTimeWithZone(Convert.ToDateTime(vTimings.E_Enddate), userTimeZone, true);
                            }

                            objDisEv.EventTimings = dtzstart.LocalTime.ToString("ddd MMM dd, yyyy") + " " + dtzstart.LocalTime.ToString("h:mm tt").ToLower().Trim().Replace(" ", "");
                            objDisEv.EventDate = dtzstart.LocalTime;

                            //objDisEv.EventTimings = Convert.ToDateTime(vTimings.EventStartDate).ToString("ddd MMM dd, yyyy") + " " + vTimings.EventStartTime;
                            //objDisEv.EventDate = (vTimings.EventStartDate != null ? Convert.ToDateTime(vTimings.EventStartDate + " " + vTimings.EventStartTime) : DateTime.Now);
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
                            //objDisEv.EventTimings = strTiming;

                            var vMultiple = objEv.MultipleEvents.FirstOrDefault();
                            if (vMultiple != null)
                            {
                                if (Timezonedetail != null)
                                {
                                    TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Timezonedetail.TimeZone);
                                    dtzstart = new DateTimeWithZone(Convert.ToDateTime(vMultiple.M_Startfrom), userTimeZone, true);
                                    dzend = new DateTimeWithZone(Convert.ToDateTime(vMultiple.M_StartTo), userTimeZone, true);
                                }
                                else
                                {
                                    TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                                    dtzstart = new DateTimeWithZone(Convert.ToDateTime(vMultiple.M_Startfrom), userTimeZone, true);
                                    dzend = new DateTimeWithZone(Convert.ToDateTime(vMultiple.M_StartTo), userTimeZone, true);
                                }
                                objDisEv.EventTimings = dtzstart.LocalTime.ToString("ddd MMM dd, yyyy") + " " + dtzstart.LocalTime.ToString("h:mm tt").ToLower().Trim().Replace(" ", "") + " - " + dzend.LocalTime.ToString("ddd MMM dd, yyyy") + " " + dzend.LocalTime.ToString("h:mm tt").ToLower().Trim().Replace(" ", "");
                            }
                        }

                        lsDisEvt.Add(objDisEv);
                    }
                    if (strSort == "dat") lsDisEvt = lsDisEvt.OrderBy(m => m.EventDate).ToList();
                    else lsDisEvt = lsDisEvt.OrderBy(m => m.EventDistance).ToList();
                    if (strTextSearch.Trim() != string.Empty)
                    {
                        lsDisEvt = lsDisEvt.Where(m => m.EventTitle.ToLower().Contains(strTextSearch.ToLower()) || m.EventCat.ToLower().Contains(strTextSearch.ToLower()) || m.EventType.ToLower().Contains(strTextSearch.ToLower()) || (m.EventDisplayAddress != null ? m.EventDisplayAddress.ToLower().Contains(strTextSearch.ToLower()) : m.EventType.ToLower().Contains(strTextSearch.ToLower()))).ToList();
                        //lsDisEvt = lsDisEvt.Where(m => m.EventTitle.Contains(strTextSearch) || m.EventCat.Contains(strTextSearch) || m.EventDisplayAddress.Contains(strTextSearch)).ToList();
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

            //try
            //{
            //    string strUrl = Url.RouteUrl("EvType", new { strEt = strEt, strEc = strEc, strPrice = strPrice, strPageIndex = strPageIndex, strLat = strLat, strLong = strLong, strSort = strSort, strDateFilter = strDateFilter });
            //    if (Session["ReturnUrl"] != null)
            //    {
            //        string[] str = Session["ReturnUrl"].ToString().Split('~');
            //        long lEventId = Convert.ToInt32(str[0].ToString());
            //        if (lEventId > 0)
            //        {
            //            Discoversavefavourite(lEventId, strUrl);
            //        }
            //    }
            //    else
            //    {
            //        Session["ReturnUrl"] = "0~" + strUrl;
            //    }
            //}
            //catch (Exception)
            //{
            //    Session["ReturnUrl"] = "0~" + strUrl;

            //}
            EventCreation objCEv = new EventCreation();

            ValidationMessage vmc = new ValidationMessage();

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
                    sbQuery.Append("Select * from Event where EventStatus = 'Live' and rtrim(ltrim(lower(EventPrivacy))) != 'private'");
                    sbQuery.Append(" and EventID in (" + strEventIds + ")");

                    var vEventList = db.Events.SqlQuery(sbQuery.ToString()).ToList();

                    string strImageUrl = "";
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

                        objDisEv.EventPrivacy = "Y";
                        if (objEv.EventPrivacy.Trim().ToLower() == "private" && objEv.Private_ShareOnFB.Trim() == "N")
                        {
                            objDisEv.EventPrivacy = "N";
                        }

                        objDisEv.AddressStatus = 0;
                        if (objEv.AddressStatus != null)
                            objDisEv.AddressStatus = (objEv.AddressStatus.ToLower().Trim() == "online" ? 1 : 0);


                        objDisEv.EventCatId = objEv.EventCategoryID;
                        objDisEv.EventTypeId = objEv.EventTypeID;
                        objDisEv.PriceLable = GetPriceLabel(lEventId);
                        objDisEv.EventLike = GetDiscoverEventFavLikes(lEventId, strUserId);
                        objDisEv.EventFeature = int.MaxValue;
                        if (objEv.Feature != null)
                            objDisEv.EventFeature = (objEv.Feature == 0 ? int.MaxValue : Convert.ToInt16(objEv.Feature)); // 10 - becz if feature is null then that event have to show at last according to feature sorting 



                        objDisEv.FeatureDateTime = (objEv.FeatureUpdateDate != null ? Convert.ToDateTime(objEv.FeatureUpdateDate) : DateTime.Now);
                        var vAddress = objEv.Addresses.FirstOrDefault();
                        if (vAddress != null)
                        {
                            objDisEv.EventDistance = GetDiscoverEventLatLongDis(Convert.ToDouble((strLat != "" ? strLat : "0")), Convert.ToDouble((strLong != "" ? strLong : "0")), Convert.ToDouble((vAddress.Latitude != "" ? vAddress.Latitude : "0")), Convert.ToDouble((vAddress.Longitude != "" ? vAddress.Longitude : "0")));
                            if (!String.IsNullOrWhiteSpace(vAddress.ConsolidateAddress))
                            {
                                objDisEv.EventAddress = vAddress.ConsolidateAddress;
                            }
                            else
                            {
                                objDisEv.EventAddress = vAddress.VenueName.Trim() + " " + vAddress.Address1.Trim() + " " + vAddress.Address2.Trim() + " " + vAddress.City.Trim() + " " + vAddress.Zip;
                            }
                            objDisEv.EventDisplayAddress = objDisEv.EventAddress;
                            if (objDisEv.EventAddress.Length > 140)
                            {
                                objDisEv.EventAddress = objDisEv.EventAddress.Substring(0, 135) + "...";
                            }
                        }
                        else
                        {
                            objDisEv.EventDistance = double.MaxValue;
                            objDisEv.EventAddress = "Online";
                        }


                        if (bflag == true)
                        {
                            strNearLat = vAddress.Latitude;
                            strNearLong = vAddress.Longitude;
                            bflag = false;
                        }
                        var Timezonedetail = (from ev in db.TimeZoneDetails where ev.TimeZone_Id.ToString() == objEv.TimeZone select ev).FirstOrDefault();
                        DateTimeWithZone dtzstart, dzend, dtznewstart, dtzCreated;
                        var vTimings = objEv.EventVenues.FirstOrDefault();
                        if (vTimings != null)
                        {
                            if (Timezonedetail != null)
                            {
                                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Timezonedetail.TimeZone);
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(vTimings.E_Startdate), userTimeZone, true);
                                dzend = new DateTimeWithZone(Convert.ToDateTime(vTimings.E_Enddate), userTimeZone, true);
                            }
                            else
                            {
                                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(vTimings.E_Startdate), userTimeZone, true);
                                dzend = new DateTimeWithZone(Convert.ToDateTime(vTimings.E_Enddate), userTimeZone, true);
                            }
                            objDisEv.EventTimings = dtzstart.LocalTime.ToString("ddd MMM dd, yyyy") + " " + dtzstart.LocalTime.ToString("h:mm tt").ToLower().Trim().Replace(" ", "");
                            objDisEv.EventDate = dtzstart.LocalTime;

                            //objDisEv.EventTimings = Convert.ToDateTime(vTimings.EventStartDate).ToString("ddd MMM dd, yyyy") + " " + vTimings.EventStartTime;
                            //objDisEv.EventDate = (vTimings.EventStartDate != null ? Convert.ToDateTime(vTimings.EventStartDate + " " + vTimings.EventStartTime) : DateTime.Now);
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
                            var vMultiple = objEv.MultipleEvents.FirstOrDefault();
                            if (vMultiple != null)
                            {
                                if (Timezonedetail != null)
                                {
                                    TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Timezonedetail.TimeZone);
                                    dtzstart = new DateTimeWithZone(Convert.ToDateTime(vMultiple.M_Startfrom), userTimeZone, true);
                                    dzend = new DateTimeWithZone(Convert.ToDateTime(vMultiple.M_StartTo), userTimeZone, true);
                                }
                                else
                                {
                                    TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                                    dtzstart = new DateTimeWithZone(Convert.ToDateTime(vMultiple.M_Startfrom), userTimeZone, true);
                                    dzend = new DateTimeWithZone(Convert.ToDateTime(vMultiple.M_StartTo), userTimeZone, true);
                                }
                                //objDisEv.EventTimings = strTiming;
                                objDisEv.EventTimings = dtzstart.LocalTime.ToString("ddd MMM dd, yyyy") + " " + dtzstart.LocalTime.ToString("h:mm tt").ToLower().Trim().Replace(" ", "") + " - " + dzend.LocalTime.ToString("ddd MMM dd, yyyy") + " " + dzend.LocalTime.ToString("h:mm tt").ToLower().Trim().Replace(" ", "");
                            }
                        }

                        lsDisEvt.Add(objDisEv);
                    }
                    //lsDisEvt = lsDisEvt.OrderBy(m => m.EventDistance).ToList().OrderBy(m => m.EventFeature).OrderBy(m => m.FeatureDateTime) .ToList();
                    //lsDisEvt = lsDisEvt.OrderBy(m => m.EventDistance).ToList().OrderBy(m => m.EventFeature).ToList().OrderBy(m => m.EventDate).ToList();
                    //lsDisEvt = lsDisEvt.OrderBy(m => m.EventFeature).ToList().OrderBy(m => m.EventDate).ToList().OrderBy(m => m.FeatureDateTime).ToList().OrderBy(m => m.EventDistance).ToList();
                    //lsDisEvt = lsDisEvt.OrderBy(m => m.AddressStatus).ToList();
                    lsDisEvt = lsDisEvt.OrderBy(m => m.EventFeature).ThenBy(m => m.EventDate).ThenBy(m => m.FeatureDateTime).ThenBy(m => m.EventDistance).ToList();



                }
                return lsDisEvt;
            }
        }

        public ActionResult DiscoverEventsTiles()
        {
            MyAccount hmc = new MyAccount();
            if ((Session["AppId"] != null))
            {

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
            return RedirectToAction("Buzz", "Article");
        }
        public ActionResult EventOraganizer()
        {
            MyAccount hmc = new MyAccount();
            if ((Session["AppId"] != null))
            {

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

        public ActionResult Index(string lat, string lng, int? page, string strParm = "")
        {
            //EventCombo.Services.EventStatus obj = new EventCombo.Services.EventStatus();
            //obj.Update();
            Session["Fromname"] = "Home";
            if (Session["AppId"] != null)
            {
                var user = Session["AppId"].ToString();
                using (EventComboEntities db = new EventComboEntities())
                {
                    AspNetUser aspuser = db.AspNetUsers.First(i => i.Id == user);
                    aspuser.LastLoginTime = System.DateTime.UtcNow;
                    db.SaveChanges();
                }
                string var = getusername();
                if (string.IsNullOrEmpty(var))
                {
                    Session["AppId"] = null;
                }
            }

            string strUrl = Url.RouteUrl("Default");

            try
            {
                if (Session["ReturnUrl"] != null)
                {
                    string[] str = Session["ReturnUrl"].ToString().Split('~');
                    long lEventId = Convert.ToInt32(str[0].ToString());
                    if (lEventId > 0)
                    {
                        Discoversavefavourite(lEventId, strUrl, "FromLogin");
                    }
                }
                else
                {
                    Session["ReturnUrl"] = "0~" + strUrl;
                }
            }
            catch (Exception)
            {
                Session["ReturnUrl"] = "0~" + strUrl;

            }



            if (string.IsNullOrEmpty(lat))
            {
                try
                {
                    Ip2Geo ip2Geo = new Ip2Geo();
                    GeoAddress geoAddress = ip2Geo.GetAddress(ClientIPAddress.GetLanIPAddress(Request));
                    lat = geoAddress.latitude;
                    lng = geoAddress.longitude;
                    if (string.IsNullOrEmpty(lat) || lat == "0")
                    {
                        lat = "28.6139";
                        lng = "77.2090";
                    }
                }
                catch (Exception)
                {
                    lat = "28.6139";
                    lng = "77.2090";
                }

            }

            CookieStore.SetCookie("Lat", lat, TimeSpan.FromDays(365));
            CookieStore.SetCookie("Long", lng, TimeSpan.FromDays(365));



            int pageSize = 15;
            int pageNumber = (page ?? 1);

            string strNearLat = "";
            string strNearLong = "";
            List<DiscoverEvent> objDiscEvt = GetHomePageEventListing("", "", "all", lat, lng, "rel", "none", ref strNearLat, ref strNearLong);
            double dPageCount = objDiscEvt.Count;
            double dTotalPages = dPageCount / pageSize;
            int lTotalPages = (objDiscEvt.Count / pageSize);
            if (dTotalPages.ToString().Contains(".") == true)
                lTotalPages = lTotalPages + 1;
            ViewBag.DisEvnt = objDiscEvt.ToPagedList(pageNumber, pageSize);
            ViewBag.lat = lat;
            ViewBag.lng = lng;
            ViewBag.UserOrg = strParm;







            System.Diagnostics.Debug.Print("LAT" + lat + "LNG" + lng);
            return View(objDiscEvt.ToPagedList(pageNumber, pageSize));

        }

        public string UserOrgStatus()
        {
            using (EventComboEntities objEnt = new EventComboEntities())
            {
                string strUserId = "";
                if (Session["AppId"] != null && strUserId == "")
                    strUserId = Session["AppId"].ToString();
                if (strUserId != "")
                {
                    var vUserOrgStatus = (from myUser in objEnt.Profiles where myUser.UserID == strUserId select myUser.Organiser).FirstOrDefault();
                    if (vUserOrgStatus == null) return "N";
                    if (vUserOrgStatus == "Y")
                    {
                        return "Y";
                    }
                    else { return "N"; }
                }
                else
                {
                    return "N";
                }
            }
        }
        public ActionResult HomeEventList(string strPageIndex, string strLat, string strLong)
        {


            return PartialView();



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
                ResetPasswordViewModel model = new ResetPasswordViewModel();
                model.code = code;
                return View(model);

            }


        }
        [HttpPost]
        [AllowAnonymous]

        public async Task<ActionResult> PasswordReset(ResetPasswordViewModel model)
        {
            try
            {
                string code = "";
                var error = "";
                var success = "";
                Session["Fromname"] = "PasswordReset";
                ValidationMessage vmc = new ValidationMessage();

                if (Session["code"] != null)
                {

                    code = Session["code"].ToString();
                    model.code = code;
                }
                else
                {
                    return RedirectToAction("Index", "Home");

                }
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                             .Where(y => y.Count > 0)
                             .ToList();
                    var ee = "";
                    foreach (var item in errors)
                    {
                        if (!string.IsNullOrWhiteSpace(ee))
                        {
                            ee += "\r\n" + item[0].ErrorMessage;
                        }
                        else
                        {
                            ee += item[0].ErrorMessage;
                        }

                    }
                    ViewData["Error"] = ee;
                    return View(model);
                }
                else
                {

                }
                if (!string.IsNullOrWhiteSpace(code))
                {
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
                        return View(model);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Exception during request processing", ex);

            }

            return View(model);

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
            MyAccount ac = new MyAccount();
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
            EmailTag = ac.getTag();

            var Emailtemplate = ac.getEmail("email_lost_pwd");
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
                    from = ConfigurationManager.AppSettings.Get("DefaultEmail"); //ConfigurationManager.AppSettings.Get("UserName");

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
                ac.SendHtmlFormattedEmail(to, from, subjectn, bodyn, cc, bcc, tag, emailname);
            }
            ValidationMessage vmc = new ValidationMessage();
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
            if (user != null)
            {
                message = "F";
                return Json(new { Message = message, Fname = user.FirstName, Lname = user.LastName });
            }
            else
            {
                message = "N";

                return Json(new { Message = message, Fname = "", Lname = "" });
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
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings.Get("DefaultEmail")); //ConfigurationManager.AppSettings["UserName"]);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(toaddress));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["Host"];
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                string s = ConfigurationManager.AppSettings["UserName"];
                if (!String.IsNullOrEmpty(s))
                {
                    NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
                    NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                }
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
            Session["ReturnUrl"] = "CreateEvent~" + Url.Action("CreateEvent", "EventManagement");


            if (Session["AppId"] == null)
            {

                return "Y";

            }
            else
            {
                return "N";
                //if (CommanClasses.UserOrganizerStatus(Session["AppId"].ToString()) == true)
                //{ return "N"; }
                //else
                //{ return "YN"; }

            }
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
            MyAccount ac = new MyAccount();
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
                            Ip2Geo ip2Geo = new Ip2Geo();
                            GeoAddress geoAddress = ip2Geo.GetAddress(ClientIPAddress.GetLanIPAddress(Request));
                            city = geoAddress.cityName;
                            country = geoAddress.countryName;
                            zipcode = geoAddress.zipCode;
                            state = geoAddress.regionName;
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
                        prof.Organiser = "Y";
                        objEntity.Profiles.Add(prof);




                        objEntity.SaveChanges();


                        using (EventComboEntities db = new EventComboEntities())
                        {
                            AspNetUser aspuser = db.AspNetUsers.First(i => i.Id == Userid.Id);

                            aspuser.LoginStatus = "Y";
                            aspuser.LastLoginTime = System.DateTime.UtcNow;
                            db.SaveChanges();

                        }
                        Session["AppId"] = Userid.Id;




                        /// to send email////
                        /// 
                        string to = "", from = "", cc = "", bcc = "", subjectn = "", emailname = "";
                        var bodyn = "";
                        List<Email_Tag> EmailTag = new List<Email_Tag>();
                        EmailTag = ac.getTag();
                        string tag = "UserEmailID:" + model.Email;
                        var Emailtemplate = ac.getEmail("email_welcome");
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
                            from = ConfigurationManager.AppSettings.Get("DefaultEmail"); //ConfigurationManager.AppSettings.Get("UserName");

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
                        ac.SendHtmlFormattedEmail(to, from, subjectn, bodyn, cc, bcc, tag, emailname);


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
        //public void SendHtmlFormattedEmail(string To, string from, string subject, string body, string cc, string bcc, string tags, string emailname)
        //{
        //    using (MailMessage mailMessage = new MailMessage())
        //    {
        //        mailMessage.From = new MailAddress(from, emailname);
        //        string[] arr = tags.Split('¶');
        //        int length = arr.Length;
        //        List<Email_Tag> EmailTag = new List<Email_Tag>();
        //        EmailTag = getTag();

        //        if (!string.IsNullOrEmpty(subject) && subject != null)
        //        {


        //            for (int j = 0; j < length; j++)
        //            {
        //                for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
        //                {
        //                    string[] arrtag = arr[j].Split(':');
        //                    if (arrtag[0] == EmailTag[i].Tag_Name)
        //                    {
        //                        if (subject.Contains(EmailTag[i].Tag_Name))
        //                        {
        //                            subject = subject.Replace("¶¶" + EmailTag[i].Tag_Name + "¶¶", arrtag[1]);
        //                        }
        //                    }
        //                }
        //            }
        //            for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
        //            {
        //                if (subject.Contains(EmailTag[i].Tag_Name))
        //                {
        //                    subject = subject.Replace("¶¶" + EmailTag[i].Tag_Name + "¶¶", "");
        //                }
        //            }



        //        }
        //        if (body != null && !string.IsNullOrEmpty(body))
        //        {
        //            for (int j = 0; j < length; j++)
        //            {
        //                for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
        //                {

        //                    string[] arrtag = arr[j].Split(':');
        //                    if (arrtag[0] == EmailTag[i].Tag_Name)
        //                    {
        //                        if (body.Contains(EmailTag[i].Tag_Name))
        //                        {
        //                            body = body.Replace("¶¶" + EmailTag[i].Tag_Name + "¶¶", arrtag[1]);
        //                        }
        //                    }
        //                }
        //            }
        //            for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
        //            {
        //                if (body.Contains(EmailTag[i].Tag_Name))
        //                {
        //                    body = body.Replace("¶¶" + EmailTag[i].Tag_Name + "¶¶", "");
        //                }
        //            }

        //        }
        //        mailMessage.Subject = subject;
        //        mailMessage.Body = body;
        //        if (!string.IsNullOrEmpty(cc))
        //        {
        //            mailMessage.CC.Add(cc);
        //        }
        //        if (!string.IsNullOrEmpty(bcc))
        //        {
        //            mailMessage.Bcc.Add(bcc);
        //        }
        //        mailMessage.IsBodyHtml = true;
        //        mailMessage.To.Add(new MailAddress(To));
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = ConfigurationManager.AppSettings["Host"];
        //        smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
        //        System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
        //        NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
        //        NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = NetworkCred;
        //        smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
        //        smtp.Send(mailMessage);
        //    }
        //}
        //public void SendHtmlFormattedEmail(string To, string from, string subject, string body, string cc, string bcc, MemoryStream attachment, string emailname, string qrimage, string brcode, List<TicketBearer> GuestList)
        //{
        //    MailMessage mailMessage = new MailMessage();

        //    mailMessage.From = new MailAddress(from, emailname);


        //    mailMessage.Subject = subject;
        //    mailMessage.Body = body;
        //    if (!string.IsNullOrEmpty(cc))
        //    {
        //        mailMessage.CC.Add(cc);
        //    }
        //    if (!string.IsNullOrEmpty(bcc))
        //    {
        //        mailMessage.Bcc.Add(bcc);
        //    }
        //    if (attachment != null)
        //    {
        //        if (attachment.Length != 0)
        //        {
        //            System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
        //            System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(attachment, ct);
        //            attach.ContentDisposition.FileName = "Ticket_EventCombo.pdf";
        //            mailMessage.Attachments.Add(attach);
        //        }
        //    }
        //    mailMessage.IsBodyHtml = true;
        //    //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
        //    //mailMessage.AlternateViews.Add(htmlView);

        //    //Add Image
        //    //LinkedResource theEmailImage = new LinkedResource(ImageMapPath);
        //    //theEmailImage.ContentId = "myeventmapImageID";
        //    //htmlView.LinkedResources.Add(theEmailImage);


        //    ////LinkedResource theQrImage = new LinkedResource(qrimage);

        //    ////theQrImage.ContentId = "myQrcodeImageID";
        //    ////htmlView.LinkedResources.Add(theQrImage);


        //    ////LinkedResource thebarImage = new LinkedResource(brcode);

        //    ////thebarImage.ContentId = "myBarcodeImageID";
        //    ////htmlView.LinkedResources.Add(thebarImage);

        //    //LinkedResource theeventImage = new LinkedResource(Imageevent);

        //    //theeventImage.ContentId = "myeventImageID";
        //    //htmlView.LinkedResources.Add(theeventImage);


        //    mailMessage.To.Add(new MailAddress(To));
        //    if (GuestList != null)
        //    {
        //        foreach (var item in GuestList)
        //        {
        //            mailMessage.To.Add(new MailAddress(item.Email, item.Name));
        //        }
        //    }
        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Host = ConfigurationManager.AppSettings["Host"];
        //    smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
        //    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
        //    NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
        //    NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
        //    smtp.UseDefaultCredentials = true;
        //    smtp.Credentials = NetworkCred;
        //    smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
        //    smtp.Send(mailMessage);

        //}
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

        public void Setheader(string header)
        {

            CookieStore.SetCookie("ckHeader", header, TimeSpan.FromDays(1));
            //Session["Header"] = header;
        }

        public ActionResult Footer()
        {
            Footer footer = new Footer();
            string User = "";
            string UserLatitude = "";
            string UserLongitude = "";

            if (Session["AppId"] != null)
            {
                User = Session["AppId"].ToString();
            }
            var aspuser = db.Profiles.FirstOrDefault(i => i.UserID == User);
            if (aspuser != null)
            {
                var city = db.Cities.FirstOrDefault(c => c.CityName == aspuser.City);
                if (city != null)
                {
                    UserLatitude = city.Latitude;
                    UserLongitude = city.Longitude;
                }
            }
            ViewData["UserLatitude"] = UserLatitude;
            ViewData["UserLongitude"] = UserLongitude;

            var businessPages = db.BusinessPages.Where(x => x.IsOnFooter == true).OrderBy(x => x.PageOrder).ToList();
            if (businessPages != null)
            {
                footer.BusinessPages = businessPages;
            }
            var eventTypes = db.EventTypes.Where(x => x.IsOnFooter == true).OrderBy(x => x.EventType1).ToList();
            if (eventTypes != null)
            {
                footer.EventTypes = eventTypes;
            }
            var cities = db.Cities.Where(x => x.IsOnFooter == true).OrderBy(x => x.CityName).ToList();
            if (cities != null)
            {
                footer.Cities = cities;
            }
            return PartialView("_AngularFooter", footer);
        }

        public void SendEmailToFriend(string strFriendEmail, string strFromname, string strEventTitle, string strEventUrl, long lEvent = 0)
        {
            if (lEvent > 0)
            {
                EventCreation objCE = new EventCreation();
                var EventDetail = objCE.GetEventdetail(lEvent);
                strEventTitle = EventDetail.EventTitle;
                var url = Request.Url;
                var baseurl = url.GetLeftPart(UriPartial.Authority);
                strEventUrl = baseurl + Url.Action("ViewEvent", "ViewEvent", new { strEventDs = System.Text.RegularExpressions.Regex.Replace(strEventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = lEvent.ToString() });
            }
            List<Email_Tag> EmailTag = new List<Email_Tag>();
            MyAccount ac = new MyAccount();
            EmailTag = ac.getTag();
            var Emailtemplate = ac.getEmail("email_friend");
            string to = strFriendEmail, from = "", cc = "", bcc = "", emailname = "", subjectn = "", bodyn = "";
            if (Emailtemplate != null)
            {
                if (!(string.IsNullOrEmpty(Emailtemplate.From)))
                {
                    from = Emailtemplate.From;
                }
                else
                {
                    from = ConfigurationManager.AppSettings.Get("DefaultEmail");
                }
                if (!(string.IsNullOrEmpty(Emailtemplate.CC)))
                {
                    cc = Emailtemplate.CC;
                }
                if (!(string.IsNullOrEmpty(Emailtemplate.Bcc)))
                {
                    bcc = Emailtemplate.Bcc;
                }
                if (strFromname.Trim() != "")
                {
                    emailname = strFromname;
                }
                else
                {
                    if (!(string.IsNullOrEmpty(Emailtemplate.From_Name)))
                    {
                        emailname = Emailtemplate.From_Name;
                    }
                    else
                    {
                        emailname = from;
                    }
                }

                if (!string.IsNullOrEmpty(Emailtemplate.Subject))
                {
                    subjectn = Emailtemplate.Subject;
                    for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
                    {
                        if (subjectn.Contains("¶¶" + EmailTag[i].Tag_Name.Trim() + "¶¶"))
                        {
                            if (EmailTag[i].Tag_Name == "EventTitleId")
                            {
                                subjectn = subjectn.Replace("¶¶EventTitleId¶¶", strEventTitle);
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
                            if (EmailTag[i].Tag_Name == "DiscoverEventurl")
                            {
                                bodyn = bodyn.Replace("¶¶DiscoverEventurl¶¶", strEventUrl);
                            }
                            if (EmailTag[i].Tag_Name == "EventTitleId")
                            {
                                bodyn = bodyn.Replace("¶¶EventTitleId¶¶", strEventTitle);
                            }
                        }
                    }
                }


                ac.SendHtmlFormattedEmail(to, from, subjectn, bodyn, cc, bcc, "", emailname);
            }
        }
    }
}