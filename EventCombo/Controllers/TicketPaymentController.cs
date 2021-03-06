﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
using EventCombo.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using NReco.PdfGenerator;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using EventCombo.ViewModels;
using System.Globalization;
using System.Configuration;
using NLog;

namespace EventCombo.Controllers
{

    //[RoutePrefix("Payment")]

    public class TicketPaymentController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        EventComboEntities db = new EventComboEntities();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        // GET: TicketPayment

        //[Route("Payment", Name = "TPayment", Order=2)]
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        //[Route("",Name ="Payment"),HttpGet] 
        //[RequireHttps]  
        public ActionResult TicketPayment()

        {
            if (Session["token"] != null && Session["PayerID"] != null)
            {
                string strtoken = Session["token"].ToString();
                string strPayerID = Session["PayerID"].ToString();


            }
            EncryptDecrypt EDcode = new EncryptDecrypt();
            EventCreation cs = new EventCreation();
            MyAccount AccDetail = new MyAccount();
            //ValidationMessageController vmc = new ValidationMessageController();
            if ((Session["AppId"] != null))
            {
                string LgUser = Session["AppId"].ToString();
                using (EventComboEntities db = new EventComboEntities())
                {
                    AspNetUser aspuser = db.AspNetUsers.First(i => i.Id == LgUser);

                    aspuser.LastLoginTime = System.DateTime.UtcNow;
                    db.SaveChanges();


                }
                string usernme = AccDetail.getusername();
                if (string.IsNullOrEmpty(usernme))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            long Eventid = 0;
            if (Session["TicketLockedId"] != null)
            {
                Event objMyEvent = new Event();
                objMyEvent = cs.GetSelectedEventDetail(Session["TicketLockedId"].ToString());
                if (objMyEvent == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                Eventid = objMyEvent.EventID;
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }

            Eventid = cs.GetLatestEventId(Eventid);

            TicketPayment tp = new TicketPayment();
            string defaultCountry = "";
            string Fname = "", Lname = "", Phnnumber = "", Adress = "", Email = "", City = "", State = "", Country = "", Zip = "";

            var url = Url.Action("TicketPayment", "TicketPayment");
            //Session["ReturnUrl"] = "TicketPayment~" + url;
            //var url = Url.RouteUrl("Payment");
            Session["ReturnUrl"] = "TicketPayment~" + url;


            var tt = cs.GetImages(Eventid).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(tt))
            {
                tp.Imageurl = tt;
            }
            else
            {
                tp.Imageurl = "/Images/default_event_image.jpg"; 
            }

            var eventdetails = cs.GetEventdetail(Eventid);
            tp.Ticketdeliveraddress = eventdetails.Ticket_DAdress;
            tp.Title = eventdetails.EventTitle;
            tp.URLTitle = Regex.Replace(eventdetails.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", "");
            tp.Tickettype = "Paid";
            var address = (from a in db.Addresses where a.EventId == Eventid select a).FirstOrDefault();


            //datetime
            DateTime datetime = new DateTime();
            DateTimeWithZone dtzstart;
            string day = "", Sdate = "", time = "";
            var td = DateTimeWithZone.Timezonedetail(eventdetails.EventID);
            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td);
            if (eventdetails.AddressStatus == "Single")
            {
                var ev = db.EventVenues.FirstOrDefault(i => i.EventID == Eventid);
                dtzstart = new DateTimeWithZone(Convert.ToDateTime(ev.E_Startdate), userTimeZone, true);
                datetime = dtzstart.LocalTime;
                day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(datetime).ToString();
                Sdate = datetime.ToString("MMM dd, yyyy");
                time = datetime.ToString("h:mm tt").ToLower().Trim().Replace(" ", "");

            }
            //


            ViewData["Type"] = tp.Tickettype;
            if (Session["AppId"] != null)
            {
                string Userid = Session["AppId"].ToString();
                var accountdetail = AccDetail.GetLoginDetails(Userid);
                Fname = accountdetail.Firstname;
                Lname = accountdetail.Lastname;
                Phnnumber = accountdetail.MainPhone;
                Adress = accountdetail.StreetAddress1 + "," + accountdetail.StreeAddress2 + "," + accountdetail.City + "," + accountdetail.State + "," + accountdetail.Zip + "," + accountdetail.Country;
                Email = accountdetail.Email;
                City = accountdetail.City;
                State = accountdetail.State;
                Country = accountdetail.Country;
                Zip = accountdetail.Zip;


            }

            tp.Email = Email;
            tp.FName = Fname;
            tp.LName = Lname;
            tp.PhnNo = Phnnumber;
            tp.Address = Adress;
            tp.EventId = Eventid;
            tp.AccState = State;
            tp.AccCity = City;
            tp.Acczip = Zip;
            TempData["address"] = !string.IsNullOrEmpty(address.ConsolidateAddress) ? address.ConsolidateAddress : "";
            TempData["eventdatetime"] = day + "," + Sdate + " " + time;

            using (EventComboEntities db = new EventComboEntities())
            {
                var countryQuery = (from c in db.Countries
                                    orderby c.Country1 ascending
                                    select c).Distinct();
                List<SelectListItem> countryList = new List<SelectListItem>();
                defaultCountry = "United States";
                foreach (var item in countryQuery)
                {
                    countryList.Add(new SelectListItem()
                    {
                        Text = item.Country1,
                        Value = item.CountryID.ToString(),
                        Selected = (item.CountryID.ToString().Trim() == defaultCountry.Trim() ? true : false)
                    });
                }

                ViewBag.CountryID = countryList;

            }
            tp.tickebox = LoadTickets(Eventid.ToString());
            TicketPayment TicketPayment = new TicketPayment();
            TicketPayment = (TicketPayment)Session["TicketDatamodel"];
            if (TicketPayment != null)
            {
                TempData["accfname"] = TicketPayment.AccFname.ToString();
                TempData["accLname"] = TicketPayment.AccLname.ToString();
                TempData["AccEmail"] = TicketPayment.AccEmail.ToString();
                if (TicketPayment.AccconfirmEmail != null)
                    TempData["AccConfirmEmail"] = TicketPayment.AccconfirmEmail.ToString();
                else
                    TempData["AccConfirmEmail"] = "";

                TempData["ReqFrom"] = "PP";
                StringBuilder strHTML = new StringBuilder();
                StringBuilder strdll = new StringBuilder();
                List<TicketBearer> objTB = TicketPayment.NameList;

                int i = 0;
                if (objTB != null)
                {
                    foreach (TicketBearer tb in objTB)
                    {
                        strHTML.Append("<tr>");
                        strHTML.Append("<td style='display:none' width='92%'>");
                        strHTML.Append(i.ToString());
                        strHTML.Append("</td>");
                        strHTML.Append("<td width='92 %'><label id=TicketName_" + i.ToString() + ">" + tb.Name + "</label></td>");
                        strHTML.Append("<td style='display: none'><label id=TicketEmail_" + i.ToString() + ">" + tb.Email + "</label></td>");
                        strHTML.Append("</tr>");
                        strdll.Append("<option value=" + i.ToString() + " id=" + i.ToString() + ">" + tb.Name + "</option>");
                    }
                }
                TempData["GuestList"] = strHTML.ToString();
                TempData["GuestListOption"] = strdll.ToString();
            }
            else
            {
                TempData["ReqFrom"] = "B";
                TempData["GuestList"] = "";
                TempData["GuestListOption"] = "";
            }
            return View(tp);
        }

        public string ReleaseTickets()
        {
            string strResult = "";
            try
            {
                string strLockedId = (Session["TicketLockedId"] != null ? Session["TicketLockedId"].ToString() : "");
                CommanClasses objC = new CommanClasses();
                using (var context = new EventComboEntities())
                {
                    context.Database.ExecuteSqlCommand("DELETE FROM Ticket_Locked_Detail WHERE TLD_GUID ='" + strLockedId + "'");
                    strResult = objC.GetMessage("TicketPayment", "TenMinWindowExpires");
                }
            }
            catch (Exception ex)
            {
                strResult = "There is some Problem.";
                logger.Error("Exception during request processing", ex);
            }
            return strResult;
        }

        public void LockTickets(Ticket_Locked_Detail objTicketIds)
        {
            //string[] strTIds = strTicketIds.Split(',');
            string strUsers = (Session["AppId"] != null ? Session["AppId"].ToString() : "");
            using (var context = new EventComboEntities())
            {
                Ticket_Locked_Detail objTLD = new Ticket_Locked_Detail();
                foreach (Ticket_Locked_Detail objModel in objTicketIds.TLD_List)
                {
                    objTLD.TLD_Locked_Qty = objModel.TLD_Locked_Qty;
                    // objTLD.TLD_Ticket_Id = objModel.TLD_Ticket_Id;
                    objTLD.TLD_User_Id = strUsers;
                    context.Ticket_Locked_Detail.Add(objTLD);
                }
                context.SaveChanges();
            }
        }


        public string CalculatePromoCode(string strTicketId, string strCode, long lEventId)
        {
            string strResult = "";
            DateTimeWithZone dtzstart, dzend, dtzCreated, dzstartpromocode, dzendpromocode;
            string strUsers = (Session["AppId"] != null ? Session["AppId"].ToString() : "");
            string strGUID = (Session["TicketLockedId"] != null ? Session["TicketLockedId"].ToString() : "");
            try
            {
                using (var context = new EventComboEntities())
                {
                    var Event = (from myEnt in db.Events where myEnt.EventID == lEventId select myEnt).FirstOrDefault();
                    int timeZoneID = Int32.Parse(Event.TimeZone);
                    var td = DateTimeWithZone.Timezonedetail(lEventId);
                    DateTime dtStartDate = new DateTime();
                    DateTime dtEndDate = new DateTime();
                    DateTime dtendpromocodedate = new DateTime();
                    DateTime dtNow = DateTime.Now;
                    var vEventVenue = (from Ev in db.EventVenues where Ev.EventID == lEventId select Ev).FirstOrDefault();
                    if (vEventVenue == null)
                    {

                        var vMultiEv = (from Ev in db.MultipleEvents where Ev.EventID == lEventId select Ev).FirstOrDefault();


                        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td);
                        dtzstart = new DateTimeWithZone(Convert.ToDateTime(vMultiEv.M_Startfrom), userTimeZone, true);
                        dzend = new DateTimeWithZone(Convert.ToDateTime(vMultiEv.M_StartTo), userTimeZone, true);
                        dtzCreated = new DateTimeWithZone(DateTime.Now, userTimeZone, false);

                        dtStartDate = dtzstart.LocalTime;
                        dtEndDate = dzend.LocalTime;
                    }
                    else
                    {

                        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td);
                        dtzstart = new DateTimeWithZone(Convert.ToDateTime(vEventVenue.E_Startdate), userTimeZone, true);
                        dzend = new DateTimeWithZone(Convert.ToDateTime(vEventVenue.E_Enddate), userTimeZone, true);
                        dtzCreated = new DateTimeWithZone(DateTime.Now, userTimeZone, false);

                        dtStartDate = dtzstart.LocalTime;
                        dtEndDate = dzend.LocalTime;
                    }
                    dtNow = dtzCreated.LocalTime;
                    string[] strTAry = strTicketId.Split(',');
                    var PromoCode = (from pc in db.Promo_Code where pc.PC_Code == strCode && pc.PC_Eventid == lEventId select pc).FirstOrDefault();
                    if (PromoCode == null) { return "INV"; }
                    var vPCount = (from pcnt in db.Order_Detail_T where pcnt.O_PromoCodeId == PromoCode.PC_id select pcnt).Count();
                    var vLockCount = (from lck in db.Ticket_Locked_Detail where lck.TLD_PromoCodeId == PromoCode.PC_id && lck.TLD_GUID != strGUID select lck).ToList().Select(x => x.TLD_GUID).Count();
                    if (PromoCode.PC_Uses != null)
                    {
                        long lTotalCount = vPCount + vLockCount;
                        long lUserCount = (PromoCode.PC_Uses.Trim() != string.Empty ? Convert.ToInt32(PromoCode.PC_Uses) : 0);
                        if (lTotalCount >= lUserCount)
                        {
                            return "OL";
                        }
                    }

                    if (PromoCode.PC_Startdatetype != null)
                    {
                        if (PromoCode.PC_Startdatetype.Trim() == "0")
                        {




                            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td);
                            dzstartpromocode = new DateTimeWithZone(Convert.ToDateTime(PromoCode.P_Startdate), userTimeZone, true);




                            dtStartDate = dzstartpromocode.LocalTime;
                            if (dtNow < dtStartDate)
                            {
                                return "FDI";
                            }
                        }
                        else if (PromoCode.PC_Startdatetype.Trim() == "1")
                        {

                            string text = PromoCode.PC_Start;// "10 Days 24 Hrs 45 Min";
                            Regex pattern = new Regex(@"(?:(?<days>\d+) Days )?(?:(?<hrs>\d+) Hrs )?(?:(?<mins>\d+) Min)?");
                            Match match = pattern.Match(text);
                            string days = match.Groups["days"].Value == "" ? "0" : match.Groups["days"].Value;
                            string hrs = match.Groups["hrs"].Value == "" ? "0" : match.Groups["hrs"].Value;
                            string mins = match.Groups["mins"].Value == "" ? "0" : match.Groups["mins"].Value;

                            //DateTime TodayDate = new DateTime();
                            //TodayDate = DateTime.Now;
                            dtStartDate = dtStartDate.AddDays(-int.Parse(days));
                            dtStartDate = dtStartDate.AddHours(-int.Parse(hrs));
                            dtStartDate = dtStartDate.AddMinutes(-int.Parse(mins));

                            if (dtNow < dtStartDate)
                            {
                                return "FDI";
                            }

                        }


                        if (PromoCode.Pc_Enddatetype.Trim() == "0")
                        {

                            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td);
                            dzendpromocode = new DateTimeWithZone(Convert.ToDateTime(PromoCode.P_Enddate), userTimeZone, true);


                            dtEndDate = dzendpromocode.LocalTime;

                            if (dtNow > dtEndDate)
                            {
                                return "EDI";
                            }
                        }
                        else if (PromoCode.Pc_Enddatetype.Trim() == "1")
                        {



                            string text = PromoCode.PC_End;// "10 Days 24 Hrs 45 Min";
                            Regex pattern = new Regex(@"(?:(?<days>\d+) Days )?(?:(?<hrs>\d+) Hrs )?(?:(?<mins>\d+) Min)?");
                            Match match = pattern.Match(text);
                            string days = match.Groups["days"].Value == "" ? "0" : match.Groups["days"].Value;
                            string hrs = match.Groups["hrs"].Value == "" ? "0" : match.Groups["hrs"].Value;
                            string mins = match.Groups["mins"].Value == "" ? "0" : match.Groups["mins"].Value;

                            //DateTime TodayDate = new DateTime();
                            //TodayDate = DateTime.Now;
                            dtEndDate = dtEndDate.AddDays(-int.Parse(days));
                            dtEndDate = dtEndDate.AddHours(-int.Parse(hrs));
                            dtEndDate = dtEndDate.AddMinutes(-int.Parse(mins));

                            if (dtNow > dtEndDate)
                            {
                                return "EDI";
                            }
                        }

                        if (PromoCode != null && PromoCode.PC_Apply != null)
                        {
                            strResult = PromoCode.PC_Apply.Trim();

                            if (PromoCode.PC_Amount != null && PromoCode.PC_Amount > 0)
                            {
                                strResult = strResult + "~" + "AMT";
                                strResult = strResult + "~" + PromoCode.PC_Amount.ToString();
                            }
                            else if (PromoCode.PC_Percentage != null && PromoCode.PC_Percentage > 0)
                            {
                                strResult = strResult + "~" + "P";
                                strResult = strResult + "~" + PromoCode.PC_Percentage.ToString();
                            }
                            else
                            {
                                strResult = strResult + "~F~" + "0";
                            }

                            //List<Ticket_Locked_Detail> objTLD = (from TLD in context.Ticket_Locked_Detail where TLD.TLD_GUID == strGUID select TLD).ToList();
                            //foreach (Ticket_Locked_Detail tl in objTLD)
                            //{
                            //    tl.TLD_PromoCodeId = PromoCode.PC_id;
                            //    context.SaveChanges();

                            //}

                        }
                        //for (int i =0;i<strTAry.Length;i++)
                        //{

                        //}
                    }
                }
            }
            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
            }
            return strResult;
        }


        public string ValidatePromoCode(long lEventId, string strCode)
        {
            string strResult = "Y";
            string strGUID = (Session["TicketLockedId"] != null ? Session["TicketLockedId"].ToString() : "");
            try
            {
                using (var context = new EventComboEntities())
                {
                    var PromoCode = (from pc in db.Promo_Code where pc.PC_Code == strCode && pc.PC_Eventid == lEventId select pc).FirstOrDefault();
                    var vPCount = (from pcnt in db.Order_Detail_T where pcnt.O_PromoCodeId == PromoCode.PC_id select pcnt).Count();
                    var vLockCount = (from lck in db.Ticket_Locked_Detail where lck.TLD_PromoCodeId == PromoCode.PC_id && lck.TLD_GUID != strGUID select lck).ToList().Select(x => x.TLD_GUID).Count();
                    if (PromoCode.PC_Uses != null)
                    {
                        long lTotalCount = vPCount + vLockCount;
                        long lUserCount = (PromoCode.PC_Uses.Trim() != string.Empty ? Convert.ToInt32(PromoCode.PC_Uses) : 0);
                        if (lTotalCount >= lUserCount)
                        {
                            strResult = "OL";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
            }
            return strResult;
        }

        public string LockPromoCode(decimal dAmt, long lTQDId, long lEventId, string strCode)
        {
            try
            {
                string strGUID = (Session["TicketLockedId"] != null ? Session["TicketLockedId"].ToString() : "");
                using (var context = new EventComboEntities())
                {
                    var vTicket = (from Tic in context.Tickets where Tic.E_Id == lEventId select Tic).ToList();
                    bool bflag = false;
                    bool bisPaid = false;
                    foreach (Ticket objTic in vTicket)
                    {
                        if (objTic.TicketTypeID == 2)
                        {
                            bisPaid = true;
                            break;
                        }
                    }
                    if (bisPaid == false)
                    {
                        var vPromoCode = (from pc in db.Promo_Code where pc.PC_Code == strCode && pc.PC_Eventid == lEventId select pc.PC_id).FirstOrDefault();
                        Ticket_Locked_Detail objTLD = (from TLD in context.Ticket_Locked_Detail where TLD.TLD_GUID == strGUID && TLD.TLD_TQD_Id == lTQDId select TLD).FirstOrDefault();
                        objTLD.TLD_PromoCodeId = vPromoCode;
                        objTLD.TLD_PromoCodeAmount = dAmt;
                        context.SaveChanges();
                    }
                    else
                    {
                        Ticket_Locked_Detail objTLD = (from TLD in context.Ticket_Locked_Detail where TLD.TLD_GUID == strGUID && TLD.TLD_TQD_Id == lTQDId select TLD).FirstOrDefault();
                        var vTicketId = (from TQD in context.Ticket_Quantity_Detail where TQD.TQD_Id == lTQDId select TQD.TQD_Ticket_Id).FirstOrDefault();
                        var vTicketType = (from Tic in context.Tickets where Tic.T_Id == vTicketId select Tic.TicketTypeID).FirstOrDefault();
                        if (vTicketType == 2)
                        {
                            var vPromoCode = (from pc in db.Promo_Code where pc.PC_Code == strCode && pc.PC_Eventid == lEventId select pc.PC_id).FirstOrDefault();
                            objTLD.TLD_PromoCodeId = vPromoCode;
                            objTLD.TLD_PromoCodeAmount = dAmt;
                            context.SaveChanges();
                        }
                    }
                }
                return "Y";
            }
            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
              return "N";
            }
        }
        public string UnLockPromoCode()
        {
            try
            {
                string strGUID = (Session["TicketLockedId"] != null ? Session["TicketLockedId"].ToString() : "");
                using (var context = new EventComboEntities())
                {
                    Ticket_Locked_Detail objTLD = (from TLD in context.Ticket_Locked_Detail where TLD.TLD_GUID == strGUID select TLD).FirstOrDefault();
                    objTLD.TLD_PromoCodeId = 0;
                    objTLD.TLD_PromoCodeAmount = 0;
                    context.SaveChanges();
                }
                return "Y";
            }
            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
              return "N";
            }
        }
        public void setsession(string id, long Eventid)
        {

            Session["ReturnUrl"] = Url.Action("TicketPayment", "TicketPayment", new { Eventid = Eventid });

        }

        public string returncardetail(string cardid)
        {
            EncryptDecrypt Ecode = new EncryptDecrypt();
            var Carddetails = (from ev in db.CardDetails where ev.CardId.ToString() == cardid select ev).FirstOrDefault();

            return Ecode.DecryptText(Carddetails.CardNumber) + "*" + Ecode.DecryptText(Carddetails.ExpirationDate) + "*" + Ecode.DecryptText(Carddetails.Cvv);

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
        public async Task<string> saveuser(string Email, string password)
        {

            var user = new ApplicationUser { UserName = Email, Email = Email };
            var result = await UserManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                var Userid = UserManager.FindByEmail(user.Email);
                await this.UserManager.AddToRoleAsync(Userid.Id, "Member");
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
                }
                return Userid.Id;
            }
            else
            {
                return "";

            }
        }

        public string SendPaypaldetail(TicketPayment model)
        {
            string strResult = "";
            try
            {
                strResult = "Y";
                Session["TicketDatamodel"] = model;
            }
            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
              strResult = "N";
            }
            return strResult;
        }

        public async Task<JsonResult> SaveDetails(TicketPayment model, string strOrderTotal, string strGrandTotal, string strPromId, string strVarChanges, string strVarId, string strPaymentType, string strTranId = null, string strPayerId = null, string strTokenNo = null)
        {
            try
            {
                string strOrderNo = "";
                string ApiLoginID; string ApiTransactionKey; string strCardNo; string strExpDate; string strCvvCode; decimal dAmount;
                ApiLoginID = ""; ApiTransactionKey = ""; strCardNo = ""; strExpDate = ""; strCvvCode = ""; dAmount = 0;
                var grandtotal = double.Parse(strGrandTotal);
                string Userid = "", transactionhash = "", transactionid = "";
                var usertype = 0;
                var useridnew = "";
                string message = "";

                if (Session["AppId"] != null)
                {
                    Userid = Session["AppId"].ToString();
                }
                else
                {
                    var userdetail = (from v in db.AspNetUsers where v.Email == model.AccEmail select v).FirstOrDefault();
                    if (userdetail == null)
                    {
                        usertype = 1;
                        string userid = await saveuser(model.AccEmail, model.Accpassword);
                        Userid = userid;
                        if (!string.IsNullOrEmpty(userid))
                        {
                            using (EventComboEntities objEntity = new EventComboEntities())
                            {

                                Profile prof = new Profile();
                                prof.FirstName = model.AccFname;
                                prof.Email = model.AccEmail;
                                prof.LastName = model.AccLname;
                                prof.UserID = Userid;
                                prof.UserStatus = "y";
                                objEntity.Profiles.Add(prof);

                                await this.UserManager.AddToRoleAsync(Userid, "Member");

                                User_Permission_Detail permdetail = new User_Permission_Detail();
                                for (int i = 1; i < 3; i++)
                                {

                                    permdetail.UP_Permission_Id = i;
                                    permdetail.UP_User_Id = Userid.ToString();
                                    objEntity.User_Permission_Detail.Add(permdetail);
                                    objEntity.SaveChanges();
                                }
                            }
                            //Session["AppId"] = userid;
                        }



                    }
                    else
                    {
                        Userid = userdetail.Id;
                    }

                }
                List<Ticket_Locked_Detail_List> objLockedTic = new List<Ticket_Locked_Detail_List>();
                objLockedTic = GetLockTickets();
                Ticket_Purchased_Detail objTPD;
                long lPromoId = 0;
                foreach (Ticket_Locked_Detail_List TLD in objLockedTic)
                {
                    lPromoId = (TLD.TLD_PromoCodeId != null ? Convert.ToInt32(TLD.TLD_PromoCodeId) : 0);
                    break;
                }
                using (EventComboEntities objEntity = new EventComboEntities())
                {
                    Order_Detail_T objOdr = new Order_Detail_T();
                    objOdr.O_Order_Id = "";
                    objOdr.O_TotalAmount = CommanClasses.ConvertToNumeric(strGrandTotal); ;
                    objOdr.O_User_Id = Userid;
                    objOdr.O_OrderAmount = CommanClasses.ConvertToNumeric(strOrderTotal);
                    objOdr.O_VariableId = strVarId;
                    objOdr.O_VariableAmount = CommanClasses.ConvertToNumeric(strVarChanges);
                    objOdr.O_PromoCodeId = lPromoId;
                    objOdr.O_OrderDateTime = DateTime.Now;
                    objOdr.O_PayPal_PayerId = strPayerId;
                    objOdr.O_PayPal_TokenId = strTokenNo;
                    objOdr.O_PayPal_TrancId = strTranId;
                    objOdr.O_Email = model.AccEmail;
                    objOdr.O_First_Name = model.AccFname;
                    objOdr.O_Last_Name = model.AccLname;
                    objOdr.O_Card_TransHash = "";
                    objOdr.O_Card_TransId = "";
                    objEntity.Order_Detail_T.Add(objOdr);
                    objEntity.SaveChanges();

                    strOrderNo = GetOrderNo();
                }
                EncryptDecrypt EDcode = new EncryptDecrypt();


                using (var transaction = db.Database.BeginTransaction())
                {
                    if (grandtotal > 0)
                    {
                        if (strPaymentType == "A")
                        {


                            ApiLoginID = ConfigurationManager.AppSettings.Get("AuthorzieNetApiLoginID");
                            ApiTransactionKey = ConfigurationManager.AppSettings.Get("AuthorzieNetApiTransactionKey");
                            strCardNo = model.cardno;
                            strExpDate = model.expirydate;
                            strCvvCode = model.cvv;
                            dAmount = (strGrandTotal != "" ? Convert.ToDecimal(strGrandTotal) : 0);
                            var cardtransaction = PaymentProcess.CheckCreditCard(ApiLoginID, ApiTransactionKey, strCardNo, strExpDate, strCvvCode, dAmount,strOrderNo, model, Userid);
                            message = cardtransaction.message;
                            transactionhash = cardtransaction.Transactionhash;
                            transactionid = cardtransaction.TransactionId;

                        }
                    }
                    else
                    {
                        message = "O";
                    }
                    if (strPaymentType == "P" || message == "O")
                    {
                        //if (usertype == 0)
                        //{

                        //    Userid = Userid;
                        //}
                        //else
                        //{
                        //    Userid = useridnew;
                        //}
                        string guid = Session["TicketLockedId"].ToString();
                        using (EventComboEntities objEntity = new EventComboEntities())
                        {
                        //List<Ticket_Locked_Detail> objLockedTic = new List<Ticket_Locked_Detail>();
                            foreach (Ticket_Locked_Detail_List TLD in objLockedTic)
                            {
                                objTPD = new Ticket_Purchased_Detail();
                                objTPD.TPD_Amount = TLD.TicketAmount;
                                objTPD.TPD_Donate = TLD.TLD_Donate;
                                objTPD.TPD_Event_Id = TLD.TLD_Event_Id;
                                objTPD.TPD_Order_Id = strOrderNo;
                                objTPD.TPD_Purchased_Qty = TLD.TLD_Locked_Qty;
                                objTPD.TPD_TQD_Id = TLD.TLD_TQD_Id;
                                objTPD.TPD_GUID = TLD.TLD_GUID;
                                objTPD.TPD_User_Id = Userid;
                                if (objTPD.TPD_Amount > 0)
                                {
                                  var fees = GetCurrentECFee(TLD.TLD_TQD_Id);
                                  objTPD.TPD_EC_Fee = fees.Item1;
                                  objTPD.Customer_Fee = fees.Item2;
                                }
                                else
                                {
                                    objTPD.TPD_EC_Fee = 0;
                                    objTPD.Customer_Fee = 0;
                                }
                                objTPD.TPD_PromoCodeID = TLD.TLD_PromoCodeId;
                                objTPD.TPD_PromoCodeAmount = TLD.TLD_PromoCodeAmount;
                                objEntity.Ticket_Purchased_Detail.Add(objTPD);
                            }


                            if (grandtotal > 0)
                            {
                                if (model.strPaymentType != "P")
                                {
                                    if (model.Savecarddetail != "N")
                                    {
                                        var objcarddetail = (from objdetail in objEntity.CardDetails where objdetail.UserId == Userid && objdetail.CardNumber == model.cardno select objdetail).Any();
                                        if (!objcarddetail)
                                        {
                                            CardDetail card = new CardDetail();
                                            card.CardNumber = EDcode.EncryptText("XXXXXXXXXXXX" + model.cardno.Substring(model.cardno.Length - 4));
                                            card.ExpirationDate = EDcode.EncryptText("Unknown");
                                            card.Cvv = EDcode.EncryptText("Unknown");
                                            card.UserId = Userid;
                                            card.Guid = guid;
                                            card.card_type = EDcode.EncryptText(model.card_type);
                                            objEntity.CardDetails.Add(card);
                                        }
                                    }

                                    if (strTranId == "" || strTranId == null)
                                    {
                                        BillingAddress badd = new BillingAddress();
                                        badd.Fname = model.billfname;
                                        badd.Lname = model.billLname;
                                        badd.Address1 = model.billaddress1;
                                        badd.Address2 = model.billaddress2;
                                        badd.City = model.billcity;
                                        badd.State = model.billstate;
                                        badd.Zip = model.billzip;
                                        badd.Country = model.billcountry;
                                        badd.Phone_Number = model.billingphno;
                                        badd.UserId = Userid;
                                        badd.Guid = guid;
                                        badd.OrderId = strOrderNo;
                                        badd.PaymentType = "C";
                                        badd.CardId = EDcode.EncryptText("XXXXXXXXXXXX" + model.cardno.Substring(model.cardno.Length - 4));
                                        badd.card_type = EDcode.EncryptText(model.card_type);
                                        badd.Cvv = EDcode.EncryptText("Unknown");
                                        badd.ExpirationDate = EDcode.EncryptText("Unknown");
                                        objEntity.BillingAddresses.Add(badd);
                                    }
                                    if (model.Saveshipdetail != "N")
                                    {
                                        ShippingAddress shipadd = new ShippingAddress();

                                        shipadd.Fname = model.shipfname;
                                        shipadd.Lname = model.shipLname;
                                        shipadd.Address1 = model.shipaddress1;
                                        shipadd.Address2 = model.shipaddress2;
                                        shipadd.City = model.shipcity;
                                        shipadd.State = model.shipstate;
                                        shipadd.Zip = model.shipzip;
                                        shipadd.Country = model.shipcountry;
                                        shipadd.Phone_Number = model.shipphno;
                                        shipadd.UserId = Userid;
                                        shipadd.Guid = guid;
                                        shipadd.OrderId = strOrderNo;
                                        objEntity.ShippingAddresses.Add(shipadd);
                                    }
                                    if (model.sameshipbilldetail == "Y")
                                    {
                                        ShippingAddress shipadd = new ShippingAddress();
                                        shipadd.Fname = model.billfname;
                                        shipadd.Lname = model.billLname;
                                        shipadd.Address1 = model.billaddress1;
                                        shipadd.Address2 = model.billaddress2;
                                        shipadd.City = model.billcity;
                                        shipadd.State = model.billstate;
                                        shipadd.Zip = model.billzip;
                                        shipadd.Country = model.billcountry;
                                        shipadd.Phone_Number = model.billingphno;
                                        shipadd.UserId = Userid;
                                        shipadd.Guid = guid;
                                        shipadd.OrderId = strOrderNo;
                                        objEntity.ShippingAddresses.Add(shipadd);
                                    }
                                }
                                if (model.NameList != null)
                                {
                                    TicketBearer ObjAdd = new TicketBearer();
                                    foreach (TicketBearer objA in model.NameList)
                                    {

                                        ObjAdd = new TicketBearer();
                                        ObjAdd.UserId = Userid;
                                        ObjAdd.Guid = guid;
                                        ObjAdd.Email = objA.Email;
                                        ObjAdd.Name = objA.Name;
                                        ObjAdd.OrderId = strOrderNo;
                                        objEntity.TicketBearers.Add(ObjAdd);
                                    }
                                }
                                // -------------------------------------------------- Payment Transfer Card detail -----------------------------------------
                            }
                            try
                            {
                                objEntity.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();

                                logger.Error("Exception during request processing", ex);
                            }

                            //else if (strPaymentType == "P")
                            //{
                            //    RedirectToAction("Pay", "Cart");
                            //}






                            //Session["AppId"] = Userid;
                            Session["TicketLockedId"] = guid;

                            return Json(new { Message = "S", Order = strOrderNo }); ;
                        }


                    }
                    else
                    {
                        if (message == "E")
                        {
                            message = "Not able to proceed with payment contact administrator";
                        }
                        return Json(new { Message = message, Order = "" }); ;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("Exp");
                logger.Error("Exception during request processing", ex);
                return Json(new { Message = ex.Message, Order = "" }); ;

            }

        }

        public Tuple<decimal, decimal> GetCurrentECFee(long? lTQDId)
        {
            Tuple<decimal, decimal> dResult = new Tuple<decimal,decimal>(0, 0);
            try
            {
                using (EventComboEntities objECE = new EventComboEntities())
                {
                    var vTicketId = (from myRow in objECE.Ticket_Quantity_Detail
                                     where myRow.TQD_Id == lTQDId
                                     select myRow.TQD_Ticket_Id).FirstOrDefault();

                    var ticket = objECE.Tickets.Where(t => t.T_Id == vTicketId).FirstOrDefault();

                  if (ticket != null)
                    dResult = new Tuple<decimal, decimal>(ticket.EC_Fee ?? 0, ticket.Customer_Fee ?? 0);
                }
            }
            catch (Exception ex)
            {
              dResult = new Tuple<decimal, decimal>(0, 0);
              logger.Error("Exception during request processing", ex);
            }


            return dResult;
        }
        public void generateQR(string qrdata, string qrImgPath)
        {
            WebClient wc = new WebClient();
            string url = "http://chart.apis.google.com/chart?cht=qr&chs=150x150&chl=" + qrdata;
            byte[] qrImage = wc.DownloadData(url);
            MemoryStream ms = new MemoryStream(qrImage);
            Image img = Image.FromStream(ms);
            img.Save(qrImgPath, ImageFormat.Png);
            img.Dispose();
            ms.Close();
        }

        public void generateBarCode(string strBarCodeData, string strqrBarImgPath)
        {
            WebClient wc = new WebClient();
            string url = "https://www.barcodesinc.com/generator/image.php?code=" + strBarCodeData + "&style=196&type=C128B&width=250&height=70&xres=1&font=3";
            byte[] barImage = wc.DownloadData(url);
            MemoryStream mms = new MemoryStream(barImage);
            Image img = Image.FromStream(mms);
            img.Save(strqrBarImgPath, ImageFormat.Png);
            mms.Close();
        }

        private string createxml(string Ticketnumber, long eventid, string guid, Ticket_Quantity_Detail TQtydetail, Ticket_Purchased_Detail TPurchasedetail)
        {


            string tprice = "", Tdiscount = "", totalprice = "", tickettype = "", addressdetail = "";
            //var TQtydetail = (from tQty in db.Ticket_Quantity_Detail
            //                    where tQty.TQD_Id.ToString() == (from t in db.TicketOrderDetails
            //                                                     where t.T_Id == Ticketnumber
            //                                                     select t.T_TQD_Id).FirstOrDefault().ToString()
            //                    select tQty
            //                     ).FirstOrDefault();

            //var TPurchasedetail= (from tQty in db.Ticket_Purchased_Detail
            //                           where tQty.TPD_TQD_Id.ToString() == (from t in db.TicketOrderDetails
            //                                                            where t.T_Id == Ticketnumber
            //                                                            select t.T_TQD_Id).FirstOrDefault().ToString()
            //                                                            && tQty.TPD_GUID== guid
            //                           select tQty
            //                     ).FirstOrDefault();


            var TicketDetail = (from t in db.Tickets where t.T_Id == TQtydetail.TQD_Ticket_Id select t).FirstOrDefault();
            var eDetails = (from e in db.Events where e.EventID == eventid select e).FirstOrDefault();
            var address = (from a in db.Addresses where a.AddressID == TQtydetail.TQD_AddressId select a).FirstOrDefault();
            var username = (from prof in db.Profiles where prof.UserID == TPurchasedetail.TPD_User_Id select prof.FirstName + " " + prof.LastName).FirstOrDefault();
            if (address != null)
            {
                addressdetail = address.ConsolidateAddress;
            }
            var tickettypeid = TicketDetail.TicketTypeID;
            if (tickettypeid == 1)
            {
                tprice = "$ 0.00";
                Tdiscount = "$ 0.00";
                totalprice = "$ 0.00";
                tickettype = "Free";
            }
            if (tickettypeid == 2)
            {
                var t = TPurchasedetail.TPD_Amount / TPurchasedetail.TPD_Purchased_Qty;
                tprice = "$ " + TicketDetail.TotalPrice;
                Tdiscount = "$ " + TicketDetail.T_Discount;
                totalprice = "$ " + t;
                tickettype = "Paid";
            }
            if (tickettypeid == 3)
            {
                tprice = "$ " + TPurchasedetail.TPD_Donate;
                Tdiscount = "$ 0.00";
                totalprice = "$ " + TPurchasedetail.TPD_Donate;
                tickettype = "Donate";
            }

            StringBuilder strInfo = new StringBuilder();

            strInfo.Append("UniqueOrderNumber:" + Ticketnumber);
            strInfo.Append("TicketTypeName:" + TicketDetail.T_name);
            strInfo.Append("TotalTicketQuantityPerOrder:" + 1);
            strInfo.Append("TicketPrice:" + tprice);
            strInfo.Append("TicketDiscountAmount:" + Tdiscount);
            strInfo.Append("TotalTicketPrice:" + totalprice);
            strInfo.Append("TicketType:" + tickettype);
            strInfo.Append("CustomerName:" + username);
            strInfo.Append("EventName:" + eDetails.EventTitle);
            strInfo.Append("EventStartDate:" + TQtydetail.TQD_StartDate + " " + TQtydetail.TQD_StartTime);
            strInfo.Append("EventVenueName:" + addressdetail);

            return strInfo.ToString();

        }
        public void pdf(long eventid, string guid)
        {
            EncryptDecrypt Ecode = new EncryptDecrypt();
            //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            WebClient wc = new WebClient();
            MemoryStream mms = new MemoryStream();
            string htmlText = "";
            string htmlPath = Server.MapPath("..");
            try
            {
                var TicketDetail = (from Ord in db.TicketOrderDetails
                                    where Ord.T_Guid == guid
                                    select Ord).ToList();
                if (TicketDetail != null)
                {
                    foreach (var item in TicketDetail)
                    {
                        string barImgPath = Server.MapPath("..") + "/Images/br_" + item.T_Id + ".Png";

                        string qrImgPath = Server.MapPath("..") + "/Images/QR_" + item.T_Id + ".Png";

                        // Ticket and event details
                        var TQtydetail = (from tQty in db.Ticket_Quantity_Detail
                                          where tQty.TQD_Id.ToString() == (from t in db.TicketOrderDetails
                                                                           where t.T_Id == item.T_Id
                                                                           select t.T_TQD_Id).FirstOrDefault().ToString()
                                          select tQty
                                  ).FirstOrDefault();

                        var TPurchasedetail = (from tQty in db.Ticket_Purchased_Detail
                                               where tQty.TPD_TQD_Id.ToString() == (from t in db.TicketOrderDetails
                                                                                    where t.T_Id == item.T_Id
                                                                                    select t.T_TQD_Id).FirstOrDefault().ToString()
                                                                                && tQty.TPD_GUID == guid
                                               select tQty
                                             ).FirstOrDefault();


                        var Ticketorderdetail = (from f in db.Order_Detail_T where f.O_Order_Id == item.T_Order_Id select f).FirstOrDefault();
                        var userdetail = (from prof in db.Profiles where prof.UserID == TPurchasedetail.TPD_User_Id select prof).FirstOrDefault();
                        var Edtails = (from p in db.Events
                                       join user in db.Profiles on p.UserID equals user.UserID
                                       join org in db.Event_Orgnizer_Detail on p.EventID equals org.Orgnizer_Event_Id
                                       join orgprof in db.Profiles on org.UserId equals orgprof.UserID
                                       where p.EventID == eventid && org.DefaultOrg == "Y"
                                       select new
                                       {
                                           EventTitle = p.EventTitle,
                                           UserName = user.FirstName + " " + user.LastName,
                                           Organizername = orgprof.FirstName,
                                           OrganiserEmail = orgprof.Email,
                                           Addresstatus = p.AddressStatus,
                                       }).ToList().Distinct().FirstOrDefault();
                        var Organizerdetail = (from p in db.Event_Orgnizer_Detail join k in db.Organizer_Master on p.OrganizerMaster_Id equals k.Orgnizer_Id where p.Orgnizer_Event_Id == eventid && p.DefaultOrg == "Y" select k).FirstOrDefault();
                        var usernmae = !string.IsNullOrEmpty(Ticketorderdetail.O_First_Name + " " + Ticketorderdetail.O_Last_Name) ? Ticketorderdetail.O_First_Name + " " + Ticketorderdetail.O_Last_Name : Edtails.UserName;
                        var datetime = DateTime.Parse(TQtydetail.TQD_StartDate);
                        var day = datetime.DayOfWeek.ToString();
                        var Sdate = datetime.ToString("MMM dd, yyyy");
                        string Eventtype = "", Etype = "", add = "";

                        var addresslist = (from a in db.Addresses where a.AddressID == TQtydetail.TQD_AddressId select a).FirstOrDefault();
                        if ((addresslist) != null)
                        {
                            add = addresslist.ConsolidateAddress;
                        }
                        var time = TQtydetail.TQD_StartTime;

                        if (Edtails.Addresstatus == "PastLocation")
                        {
                            Eventtype = "Single";
                        }
                        else
                        {
                            Eventtype = Edtails.Addresstatus;
                        }
                        if (Edtails.Addresstatus == "Multiple")
                        {
                            Etype = "*This event has multiple venues";
                            add = add + "*";
                        }
                        else
                        {
                            Etype = "";
                        }

                        //

                        string xel = createxml(item.T_Order_Id, eventid, guid, TQtydetail, TPurchasedetail);

                        generateBarCode(item.T_Id, barImgPath);
                        generateQR(xel.ToString(), qrImgPath);


                        string Qrcode = "<img style = 'width:150px;height:150px' src ='" + qrImgPath + "' alt = 'QRCode' />";
                        string barcode = "<img  src ='" + barImgPath + "' alt = 'BarCode' >";
                        string Imagelogo = Server.MapPath("..") + "/Images/logo_vertical.png";
                        string logoImage = "<img style='width:57px;height:375px' src ='" + Imagelogo + "' alt = 'Logo' >";
                        CreateEventController ccEvent = new CreateEventController();
                        var Images = ccEvent.GetImages(eventid).FirstOrDefault();
                        string Imageevent = "";
                        if (string.IsNullOrEmpty(Images))
                        {
                            Imageevent = Server.MapPath("..") + "/Images/default_event_image.jpg";
                        }
                        else
                        {
                            Imageevent = Server.MapPath("..") + Images;
                        }
                        string Imagevent = "<img style='width:200px' src ='" + Imageevent + "' alt = 'Image' >";

                        //Order Details
                        //Order Details
                        StringBuilder strHTML = new StringBuilder();
                        var itemQuery = from TqtId in db.Ticket_Purchased_Detail

                                        where TqtId.TPD_GUID == guid && TqtId.TPD_Event_Id == eventid

                                        select TqtId.TPD_TQD_Id;
                        var myOrderId = (from p in db.Ticket_Purchased_Detail where p.TPD_GUID == guid && p.TPD_Event_Id == eventid select p.TPD_Order_Id).ToList().Distinct().FirstOrDefault();
                        var myOrderDetails = (from p in db.Order_Detail_T where p.O_Order_Id == myOrderId select p).FirstOrDefault();

                        var myAddress = (from p in db.Ticket_Quantity_Detail where itemQuery.Contains(p.TQD_Id) select p.TQD_AddressId).ToList().Distinct();
                        var myaddress = (from p in db.Ticket_Quantity_Detail where itemQuery.Contains(p.TQD_Id) select p).ToList();

                        strHTML.Append("<table style='width: 100 %;'>");
                        var eventtype = "";
                        var eventname = "";
                        var startdate = "";
                        var enddate = "";
                        var Event = (from myEnt in db.Events where myEnt.EventID == eventid select myEnt).FirstOrDefault();

                        int timeZoneID = Int32.Parse(Event.TimeZone);
                        TimeZoneDetail td = db.TimeZoneDetails.First(x => x.TimeZone_Id == timeZoneID);
                        DateTimeWithZone dtzstart, dzend;
                        DateTimeWithZone dtzcreated;


                        var singledate = (from date in db.EventVenues where date.EventID == eventid select date).FirstOrDefault();
                        if (singledate != null)
                        {
                            if (td != null)
                            {
                                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td.TimeZone);
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(singledate.E_Startdate), userTimeZone, true);
                                dzend = new DateTimeWithZone(Convert.ToDateTime(singledate.E_Enddate), userTimeZone, true);
                            }
                            else
                            {
                                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(singledate.E_Startdate), userTimeZone, true);
                                dzend = new DateTimeWithZone(Convert.ToDateTime(singledate.E_Enddate), userTimeZone, true);
                            }


                        }
                        else
                        {
                            var muldate = (from date in db.MultipleEvents where date.EventID == eventid select date).FirstOrDefault();
                            if (td != null)
                            {
                                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td.TimeZone);
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(muldate.M_Startfrom), userTimeZone, true);
                                dzend = new DateTimeWithZone(Convert.ToDateTime(muldate.M_StartTo), userTimeZone, true);
                            }
                            else
                            {
                                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(muldate.M_Startfrom), userTimeZone, true);
                                dzend = new DateTimeWithZone(Convert.ToDateTime(muldate.M_StartTo), userTimeZone, true);
                            }


                        }
                        startdate = dtzstart.LocalTime.ToLongDateString() + " " + dtzstart.LocalTime.ToLongTimeString();
                        enddate = dzend.LocalTime.ToLongDateString() + " " + dzend.LocalTime.ToLongTimeString();

                        if (Edtails.Addresstatus == "Online")
                        {
                            eventname = "Online";

                        }
                        else if (Edtails.Addresstatus == "PastLocation")
                        {


                            var Address = (from address in db.Addresses
                                           where address.AddressID == (from fb in db.Events where fb.EventID == eventid select fb.LastLocationAddress).FirstOrDefault()
                                           select address).FirstOrDefault();

                            eventname = Address.ConsolidateAddress;
                            if (Address != null)
                            {
                                eventname = Address.ConsolidateAddress;
                            }
                            else
                            {
                                eventname = "";
                            }

                        }
                        else
                        {
                            var Address = (from address in db.Addresses
                                           where address.EventId == eventid
                                           select address).FirstOrDefault();
                            if (Address != null)
                            {
                                eventname = Address.ConsolidateAddress;
                            }
                            else
                            {
                                eventname = "";
                            }
                        }
                        if (myAddress.Count() == 1)
                        {

                            foreach (var item1 in myAddress)
                            {
                                if (item1 == 0)
                                {


                                    var myDatescnt = (from p in db.Ticket_Quantity_Detail where itemQuery.Contains(p.TQD_Id) select p.TQD_StartDate).ToList().Distinct();

                                    foreach (var vdate in myDatescnt)
                                    {
                                        //strHTML.Append("<tr> <p style='margin:0px;padding:0px;margin-bottom:5px;font-size:13px;color:#aaaaaa;'><span style='float:right;'>" + Convert.ToDateTime(vdate).ToString("MMM dd, yyyy") + " </span></p ></tr > ");
                                        strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                        strHTML.Append("<th style='font-weight:normal;padding:10px5px;border-bottom:1px dashed #ccc;'>Name</th>");
                                        strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Type </th>");
                                        strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Quantity </th>");
                                        strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Price </th>");
                                        strHTML.Append("</tr>");
                                        var itemtoadd = (from p in db.Ticket_Quantity_Detail
                                                         join TP in db.Ticket_Purchased_Detail on p.TQD_Id equals TP.TPD_TQD_Id
                                                         join l in db.Profiles on TP.TPD_User_Id equals l.UserID
                                                         join t in db.Tickets on p.TQD_Ticket_Id equals t.T_Id
                                                         where p.TQD_StartDate == vdate && TP.TPD_GUID == guid && TP.TPD_Event_Id == eventid
                                                         select new
                                                         {
                                                             eventid = TP.TPD_Event_Id,
                                                             Promocode = TP.TPD_PromoCodeID,
                                                             Promocodeamt = TP.TPD_PromoCodeAmount,
                                                             username = l.FirstName,
                                                             Ticketname = t.T_name,
                                                             Quantity = TP.TPD_Purchased_Qty,
                                                             Price = TP.TPD_Amount > 0 ? "$ " + TP.TPD_Amount.ToString() : TP.TPD_Donate > 0 ? "$ " + TP.TPD_Donate.ToString() : "Free"

                                                         }).ToList();


                                        foreach (var qty in itemtoadd)
                                        {

                                            if (qty.Promocode != null && qty.Promocode != 0)
                                            {
                                                strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + (!string.IsNullOrEmpty(usernmae) ? usernmae : qty.username) + "</td>");
                                                strHTML.Append("<td style='width:30%;font-size:15px; padding: 10px 5px;'>" + qty.Ticketname + "</td>");
                                                strHTML.Append("<td style='width:10%font-size:15px; padding: 10px 5px;'>" + qty.Quantity + "</td>");
                                                strHTML.Append("<td style='width:30%;font-size:15px; padding: 10px 5px;'>" + qty.Price + "</td>");
                                                strHTML.Append("</tr>");
                                                var promocode = (from v in db.Promo_Code where v.PC_Eventid == qty.eventid && v.PC_id == qty.Promocode select v.PC_Code).FirstOrDefault();
                                                strHTML.Append("<tr align='left'>");
                                                strHTML.Append("<td colspan='3' style='font-size:15px; padding:0px 5px 10px 5px;color: green; border-bottom:1px dashed #ccc;'>" + promocode + "</td>");
                                                var promoprice = qty.Promocodeamt * qty.Quantity;
                                                strHTML.Append("<td colspan='1' style='font-size:15px; color: green;padding:0px 5px 10px 5px; border-bottom:1px dashed #ccc;'>-$" + promoprice + "</td>");
                                                strHTML.Append("</tr>");
                                            }
                                            else
                                            {
                                                strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + (!string.IsNullOrEmpty(usernmae) ? usernmae : qty.username) + "</td>");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Ticketname + "</td>");
                                                strHTML.Append("<td style='width:10%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Quantity + "</td>");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Price + "</td>");
                                                strHTML.Append("</tr>");
                                            }



                                        }



                                    }
                                }
                                else
                                {
                                    var addressdetail = (from p in db.Addresses where p.AddressID == item1 select p.ConsolidateAddress).FirstOrDefault();
                                    //strHTML.Append("<tr> <p style='margin:0px;padding:0px;margin-bottom:5px;font-size:13px;color:#aaaaaa;'>" + addressdetail + "</p ></tr > ");
                                    var myDatescnt = (from p in db.Ticket_Quantity_Detail where itemQuery.Contains(p.TQD_Id) select p.TQD_StartDate).ToList().Distinct();
                                    foreach (var vdate in myDatescnt)
                                    {
                                        var itemtoadd = (from p in db.Ticket_Quantity_Detail
                                                         join TP in db.Ticket_Purchased_Detail on p.TQD_Id equals TP.TPD_TQD_Id
                                                         join l in db.Profiles on TP.TPD_User_Id equals l.UserID
                                                         join t in db.Tickets on p.TQD_Ticket_Id equals t.T_Id
                                                         where p.TQD_StartDate == vdate && TP.TPD_GUID == guid && TP.TPD_Event_Id == eventid
                                                         select new
                                                         {
                                                             eventid = TP.TPD_Event_Id,
                                                             Promocode = TP.TPD_PromoCodeID,
                                                             Promocodeamt = TP.TPD_PromoCodeAmount,
                                                             username = l.FirstName,
                                                             Ticketname = t.T_name,
                                                             Quantity = TP.TPD_Purchased_Qty,
                                                             Price = TP.TPD_Amount > 0 ? "$ " + TP.TPD_Amount.ToString() : TP.TPD_Donate > 0 ? "$ " + TP.TPD_Donate.ToString() : "Free"

                                                         }).ToList();

                                        //strHTML.Append("<tr> <p style='margin:0px;padding:0px;margin-bottom:5px;font-size:13px;color:#aaaaaa;'><span style='float:right;'>" + Convert.ToDateTime(vdate).ToString("MMM dd, yyyy") + " </span></p ></tr > ");
                                        strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                        strHTML.Append("<th style='font-weight:normal;padding:10px5px;border-bottom:1px dashed #ccc;'>Name</th>");
                                        strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Type </th>");
                                        strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Quantity </th>");
                                        strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Price </th>");
                                        strHTML.Append("</tr>");
                                        foreach (var qty in itemtoadd)
                                        {

                                            if (qty.Promocode != null && qty.Promocode != 0)
                                            {
                                                strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + (!string.IsNullOrEmpty(usernmae) ? usernmae : qty.username) + "</td>");
                                                strHTML.Append("<td style='width:30%;font-size:15px; padding: 10px 5px;'>" + qty.Ticketname + "</td>");
                                                strHTML.Append("<td style='width:10%font-size:15px; padding: 10px 5px;'>" + qty.Quantity + "</td>");
                                                strHTML.Append("<td style='width:30%;font-size:15px; padding: 10px 5px;'>" + qty.Price + "</td>");
                                                strHTML.Append("</tr>");
                                                var promocode = (from v in db.Promo_Code where v.PC_Eventid == qty.eventid && v.PC_id == qty.Promocode select v.PC_Code).FirstOrDefault();
                                                strHTML.Append("<tr align='left'>");
                                                strHTML.Append("<td colspan='3' style='font-size:15px; padding:0px 5px 10px 5px;color: green; border-bottom:1px dashed #ccc;'>" + promocode + "</td>");
                                                var promoprice = qty.Promocodeamt * qty.Quantity;
                                                strHTML.Append("<td colspan='1' style='font-size:15px; color: green;padding:0px 5px 10px 5px; border-bottom:1px dashed #ccc;'>-$" + promoprice + "</td>");
                                                strHTML.Append("</tr>");
                                            }
                                            else
                                            {
                                                strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + (!string.IsNullOrEmpty(usernmae) ? usernmae : qty.username) + "</td>");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Ticketname + "</td>");
                                                strHTML.Append("<td style='width:10%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Quantity + "</td>");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Price + "</td>");
                                                strHTML.Append("</tr>");
                                            }



                                        }



                                    }




                                }

                            }



                        }
                        else
                        {

                            foreach (var item1 in myAddress)
                            {

                                var itemtoadd = (from p in db.Ticket_Quantity_Detail
                                                 join TP in db.Ticket_Purchased_Detail on p.TQD_Id equals TP.TPD_TQD_Id
                                                 join l in db.Profiles on TP.TPD_User_Id equals l.UserID
                                                 join t in db.Tickets on p.TQD_Ticket_Id equals t.T_Id
                                                 where p.TQD_AddressId == item1 && TP.TPD_GUID == guid && TP.TPD_Event_Id == eventid
                                                 select new
                                                 {
                                                     eventid = TP.TPD_Event_Id,
                                                     Promocode = TP.TPD_PromoCodeID,
                                                     Promocodeamt = TP.TPD_PromoCodeAmount,
                                                     username = l.FirstName,
                                                     Ticketname = t.T_name,
                                                     Quantity = TP.TPD_Purchased_Qty,
                                                     Price = TP.TPD_Amount > 0 ? "$ " + TP.TPD_Amount.ToString() : TP.TPD_Donate > 0 ? "$ " + TP.TPD_Donate.ToString() : "Free"

                                                 }).ToList();

                                var dateofaddress = (from p in db.Ticket_Quantity_Detail
                                                     join TP in db.Ticket_Purchased_Detail on p.TQD_Id equals TP.TPD_TQD_Id
                                                     where p.TQD_AddressId == item1 && TP.TPD_GUID == guid && TP.TPD_Event_Id == eventid
                                                     select p.TQD_StartDate).ToList().Distinct().FirstOrDefault();
                                var addressdetail = (from p in db.Addresses where p.AddressID == item1 select p.ConsolidateAddress).FirstOrDefault();

                                strHTML.Append("<tr> <p style='margin:0px;padding:0px;margin-bottom:5px;font-size:13px;color:#aaaaaa;'>" + addressdetail + "<span style='float:right;'>" + Convert.ToDateTime(dateofaddress).ToString("MMM dd, yyyy") + " </span></p ></tr > ");
                                strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                strHTML.Append("<th style='font-weight:normal;padding:10px5px;border-bottom:1px dashed #ccc;'>Name</th>");
                                strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;' > Type </th >");
                                strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Quantity </th>");
                                strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Price </ th >");
                                strHTML.Append("</tr>");
                                foreach (var qty in itemtoadd)
                                {

                                    if (qty.Promocode != null && qty.Promocode != 0)
                                    {
                                        strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + (!string.IsNullOrEmpty(usernmae) ? usernmae : qty.username) + "</td>");
                                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + qty.Ticketname + "</td>");
                                        strHTML.Append("<td style='width:10%; font-size:15px; padding: 10px 5px;'>" + qty.Quantity + "</td>");
                                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + qty.Price + "</td>");
                                        strHTML.Append("</tr>");
                                        var promocode = (from v in db.Promo_Code where v.PC_Eventid == qty.eventid && v.PC_id == qty.Promocode select v.PC_Code).FirstOrDefault();
                                        strHTML.Append("<tr align='left'>");
                                        strHTML.Append("<td colspan='3' style='font-size:15px; padding:0px 5px 10px 5px;color: green; border-bottom:1px dashed #ccc;'>" + promocode + "</td>");
                                        var promoprice = qty.Promocodeamt * qty.Quantity;
                                        strHTML.Append("<td colspan='1' style='font-size:15px; color: green;padding:0px 5px 10px 5px; border-bottom:1px dashed #ccc;'>-$" + promoprice + "</td>");
                                        strHTML.Append("</tr>");
                                    }
                                    else
                                    {
                                        strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + (!string.IsNullOrEmpty(usernmae) ? usernmae : qty.username) + "</td>");
                                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Ticketname + "</td>");
                                        strHTML.Append("<td style='width:10%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Quantity + "</td>");
                                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Price + "</td>");
                                        strHTML.Append("</tr>");
                                    }
                                }

                            }


                        }

                        var variabledescid = myOrderDetails.O_VariableId;
                        if (!string.IsNullOrWhiteSpace(variabledescid))
                        {
                            if (variabledescid.Contains(','))
                            {
                                string[] words = variabledescid.Split(',');
                                foreach (string word in words)
                                {
                                    var vardesc = (from p in db.Event_VariableDesc
                                                   where p.Variable_Id.ToString() == word && p.Event_Id == eventid
                                                   select p).FirstOrDefault();
                                    strHTML.Append("<tr align='right'> ");
                                    strHTML.Append("<td colspan='4' style='font-size:15px;font-weight:bold;padding:10px 5px;border-bottom:1px solid #ccc;'>" + vardesc.VariableDesc + ":$ " + vardesc.Price + " </td>");
                                    strHTML.Append("</tr> ");
                                }
                            }
                            else
                            {
                                var vardesc = (from p in db.Event_VariableDesc
                                               where p.Variable_Id.ToString() == variabledescid && p.Event_Id == eventid
                                               select p).FirstOrDefault();

                                if (vardesc != null)
                                {
                                    strHTML.Append("<tr align='right'> ");
                                    strHTML.Append("<td colspan='4' style='font-size:15px;font-weight:bold;padding:10px 5px;border-bottom:1px solid #ccc;'>" + vardesc.VariableDesc + ":$ " + vardesc.Price + " </td>");
                                    strHTML.Append("</tr> ");
                                }
                            }
                        }
                        strHTML.Append("<tr align='right'> ");
                        strHTML.Append("<td colspan='4' style='font-size:15px;font-weight:bold;padding:10px 5px;border-bottom:1px solid #ccc;'>Total :$" + myOrderDetails.O_TotalAmount + " </td></tr>");
                        if (Edtails.Addresstatus == "Multiple")
                        {
                            eventtype = "* This event has multiple venues ";
                        }
                        else
                        {
                            eventtype = "";
                        }


                        var myBillingdeatils = (from p in db.BillingAddresses where p.Guid == guid && p.OrderId == myOrderId select p).ToList().Distinct().FirstOrDefault();

                        var cardtext = "";


                        if (myBillingdeatils != null)
                        {
                            var Scardnumber = Ecode.DecryptText(myBillingdeatils.CardId).Trim();
                            var Icardlength = Scardnumber.Length;
                            var WrVS = "";
                            int k = 1;
                            for (int i = 0; i < Icardlength; i++)
                            {

                                WrVS += "X";
                                if (k == 4)
                                {
                                    WrVS += "-";
                                    k = 0;
                                }


                                k++;

                            }
                            if (WrVS.EndsWith("-"))
                            {
                                WrVS = WrVS.Substring(0, WrVS.LastIndexOf("-"));
                            }
                            var rvrs = WrVS.Substring(0, WrVS.LastIndexOf("-") + 1);
                            var rvrsd = rvrs.Replace("-", "");
                            var chrlength = Icardlength - rvrsd.Length;
                            var result = Scardnumber.Substring(Icardlength - Math.Min(chrlength, Icardlength));
                            var finalstr = rvrs + result;

                            var touper = "";

                            if (!string.IsNullOrWhiteSpace(myBillingdeatils.card_type))
                            {
                                var cardtype = Ecode.DecryptText(myBillingdeatils.card_type);
                                touper = char.ToUpper(cardtype[0]) + cardtype.Substring(1);
                            }
                            else
                            {
                                touper = "";
                            }

                            cardtext = "Charge to :" + touper + "  " + finalstr;
                        }
                        else
                        {
                            cardtext = "Charge to : Paypal";
                        }
                        if (myOrderDetails.O_TotalAmount <= 0)
                        {
                            cardtext = "";
                        }
                        strHTML.Append("<tr align='center'> ");
                        strHTML.Append("<td colspan='4' style='font-size:15px; padding:10px 5px;'>" + cardtext + " </td></tr>");
                        strHTML.Append("<tr align='center'><td colspan='4' style='font-size:15px;'>");
                        strHTML.Append("<p style='background:#fff9cf; padding:10px 15px; display:inline-block; margin:0px;'>This charge will appear on your card statement as Eventcombo * { " + Edtails.EventTitle + "}</p>");
                        strHTML.Append("<p style='color:#696564;' >This order is subject to Eventcombo '");
                        strHTML.Append("<a href='#' style='color:#0f90ba;'>Terms of Service </a> , <a style='color:#0f90ba;' href='#'>Privacy Policy </a> and <a style='color:#0f90ba;' href='#'>Cookie Policy </a></p>");
                        strHTML.Append("</td></tr></table > ");
                        //var query = (from p in db.Ticket_Purchased_Detail
                        //             join o in db.Ticket_Quantity_Detail on p.TPD_TQD_Id equals o.TQD_Id
                        //             join t in db.Tickets on o.TQD_Ticket_Id equals t.T_Id
                        //             where p.TPD_GUID == guid && p.TPD_Event_Id == eventid
                        //             group new { p.TPD_Event_Id, p.TPD_PromoCodeAmount, p.TPD_PromoCodeID, t.T_Id, p.TPD_Purchased_Qty, t.TotalPrice, t.T_Discount, t.T_name, t.TicketTypeID, p.TPD_Donate }
                        //             by new { p.TPD_Event_Id, p.TPD_PromoCodeAmount, p.TPD_PromoCodeID, t.T_Id, p.TPD_Purchased_Qty, t.TotalPrice, t.T_Discount, t.T_name, t.TicketTypeID, p.TPD_Donate } into g
                        //             select new
                        //             {
                        //                 Qty = g.Sum(x => x.TPD_Purchased_Qty),
                        //                 amount = (g.Key.TotalPrice == null ? 0 : g.Key.TotalPrice) - (g.Key.T_Discount == null ? 0 : g.Key.T_Discount),
                        //                 Tname = g.Key.T_name,
                        //                 TicketType = g.Key.TicketTypeID,
                        //                 donate = g.Sum(x => x.TPD_Donate),
                        //                 ticketid = g.Key.T_Id,
                        //                 Promocodeamt = g.Key.TPD_PromoCodeAmount,
                        //                 Promocode = g.Key.TPD_PromoCodeID,
                        //                 eventid = g.Key.TPD_Event_Id
                        //             }).ToList();
                        //string orderdet = "", freetype = "", paidtype = "", donatertype = "";
                        //orderdet = " " + Ticketorderdetail.O_First_Name != null ? Ticketorderdetail.O_First_Name : userdetail.FirstName + " has  ";
                        //foreach (var i in query)
                        //{
                        //    var promocode = (from v in db.Promo_Code where v.PC_Eventid == i.eventid && v.PC_id == i.Promocode select v.PC_Code).FirstOrDefault();
                        //    if (i.TicketType == 1)
                        //    {
                        //        if (!string.IsNullOrEmpty(freetype))
                        //        {
                        //            freetype = freetype + " , ";
                        //        }
                        //        freetype = freetype + " " + i.Qty + " * " + i.Tname + " free type for $0.00 ";
                        //        if (i.Promocode != null && i.Promocode != 0)
                        //        {

                        //            freetype = freetype + " after applying promotion code " + promocode + "  per ticket ";
                        //        }
                        //    }
                        //    if (i.TicketType == 2)
                        //    {
                        //        if (!string.IsNullOrEmpty(paidtype))
                        //        {
                        //            paidtype = paidtype + " , ";
                        //        }
                        //        paidtype = paidtype + " " + i.Qty + " * " + i.Tname + " paid type for $ " + i.amount;
                        //        if (i.Promocode != null && i.Promocode != 0)
                        //        {

                        //            paidtype = paidtype + " after applying promotion code " + promocode + "  per ticket ";
                        //        }

                        //    }
                        //    if (i.TicketType == 3)
                        //    {
                        //        if (!string.IsNullOrEmpty(donatertype))
                        //        {
                        //            donatertype = donatertype + " , ";
                        //        }
                        //        donatertype = donatertype + "  Donated for " + i.Tname + " type for $ " + i.donate;
                        //        if (i.Promocode != null && i.Promocode != 0)
                        //        {

                        //            donatertype = donatertype + " after applying promotion code " + promocode + "  per ticket ";
                        //        }
                        //    }

                        //}
                        //if (!string.IsNullOrEmpty(paidtype) && !string.IsNullOrEmpty(freetype) && !string.IsNullOrEmpty(donatertype))
                        //{
                        //    orderdet = orderdet + paidtype + " , " + freetype + " and " + donatertype;
                        //}
                        //if (!string.IsNullOrEmpty(paidtype) && !string.IsNullOrEmpty(freetype) && string.IsNullOrEmpty(donatertype))
                        //{
                        //    orderdet = orderdet + paidtype + " and " + freetype;
                        //}
                        //if (string.IsNullOrEmpty(paidtype) && !string.IsNullOrEmpty(freetype) && !string.IsNullOrEmpty(donatertype))
                        //{
                        //    orderdet = orderdet + freetype + " and " + donatertype;
                        //}
                        //if (string.IsNullOrEmpty(paidtype) && !string.IsNullOrEmpty(freetype) && string.IsNullOrEmpty(donatertype))
                        //{
                        //    orderdet = orderdet + freetype;
                        //}
                        //if (!string.IsNullOrEmpty(paidtype) && string.IsNullOrEmpty(freetype) && !string.IsNullOrEmpty(donatertype))
                        //{
                        //    orderdet = orderdet + paidtype + " and " + donatertype;
                        //}
                        //if (!string.IsNullOrEmpty(paidtype) && string.IsNullOrEmpty(freetype) && string.IsNullOrEmpty(donatertype))
                        //{
                        //    orderdet = orderdet + paidtype;
                        //}
                        //if (string.IsNullOrEmpty(paidtype) && string.IsNullOrEmpty(freetype) && !string.IsNullOrEmpty(donatertype))
                        //{
                        //    orderdet = orderdet + donatertype;
                        //}
                        //var totorder = (from o in db.Order_Detail_T where o.O_Order_Id == TPurchasedetail.TPD_Order_Id select o.O_TotalAmount).FirstOrDefault();

                        //orderdet = orderdet + " for a total of $ " + totorder;
                        //Order Details

                        htmlText += wc.DownloadString(htmlPath + "/email.html");

                        htmlText = htmlText.Replace("¶¶EventTitleId¶¶", Edtails.EventTitle);
                        htmlText = htmlText.Replace("¶¶EventStartDateID¶¶", Sdate);
                        htmlText = htmlText.Replace("¶¶EventVenueID¶¶", add);
                        htmlText = htmlText.Replace("¶¶EventOrderNO¶¶", item.T_Order_Id);
                        htmlText = htmlText.Replace("¶¶UserFirstNameID¶¶", usernmae);
                        //htmlText = htmlText.Replace("¶¶UserLastNameID¶¶", userdetail.LastName);
                        htmlText = htmlText.Replace("¶¶TicketOrderDateId¶¶", System.DateTime.Now.ToLongDateString());
                        htmlText = htmlText.Replace("¶¶EventStartTimeID¶¶", time);
                        htmlText = htmlText.Replace("¶¶EventBarcodeId¶¶", barcode);
                        htmlText = htmlText.Replace("¶¶EventQrCode¶¶", Qrcode);
                        htmlText = htmlText.Replace("¶¶EventImage¶¶", Imagevent);
                        htmlText = htmlText.Replace("¶¶EventdayId¶¶", day);
                        htmlText = htmlText.Replace("¶¶EventLogo¶¶", logoImage);
                        htmlText = htmlText.Replace("¶¶Eventtype¶¶", Eventtype);
                        htmlText = htmlText.Replace("¶¶EventDescription¶¶", "");
                        htmlText = htmlText.Replace("¶¶Eventtypedetail¶¶", Etype);
                        htmlText = htmlText.Replace("¶¶OrderDetail¶¶", strHTML.ToString());
                        htmlText = htmlText.Replace("¶¶EventOrganiserName¶¶", !string.IsNullOrEmpty(Organizerdetail.Orgnizer_Name) ? Organizerdetail.Orgnizer_Name : Edtails.Organizername);
                        htmlText = htmlText.Replace("¶¶EventOrganiserEmail¶¶", !string.IsNullOrEmpty(Organizerdetail.Organizer_Email) ? Organizerdetail.Organizer_Email : Edtails.OrganiserEmail);

                    }



                    var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                    var pdfBytes = htmlToPdf.GeneratePdf(htmlText);
                    mms = new MemoryStream(pdfBytes);
                }
                byte[] byteInfo = mms.ToArray();
                mms.Write(byteInfo, 0, byteInfo.Length);
                mms.Position = 0;
                Response.Buffer = true;
                //Response.AddHeader("content-disposition","inline;filename=" + "output.pdf");
                Response.AddHeader("Content-Disposition", "attachment; filename= " + Server.HtmlEncode("abc.pdf"));
                Response.ContentType = "APPLICATION/pdf";
                Response.BinaryWrite(byteInfo);
                mms.Close();
                Response.End();
            }
            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
            }
        }
        public MemoryStream generateTicketPDF(string guid, long eventid, List<Email_Tag> emailtag, string fname, string htmlPath)
        {

            // System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            WebClient wc = new WebClient();
            MemoryStream mms = new MemoryStream();
            EncryptDecrypt Ecode = new EncryptDecrypt();
            string htmlText = "";
            try
            {
                var TicketDetail = (from Ord in db.TicketOrderDetails
                                    where Ord.T_Guid == guid
                                    select Ord).ToList();
                if (TicketDetail != null)
                {
                    var count = 0;
                    var lastcount = TicketDetail.Count();
                    foreach (var item in TicketDetail)
                    {
                        count = count + 1;

                        string barImgPath = htmlPath + "/Images/br_" + item.T_Id + ".Png";

                        string qrImgPath = htmlPath + "/Images/QR_" + item.T_Id + ".Png";

                        // Ticket and event details
                        var TQtydetail = (from tQty in db.Ticket_Quantity_Detail
                                          where tQty.TQD_Id.ToString() == (from t in db.TicketOrderDetails
                                                                           where t.T_Id == item.T_Id
                                                                           select t.T_TQD_Id).FirstOrDefault().ToString()
                                          select tQty
                                  ).FirstOrDefault();

                        var TPurchasedetail = (from tQty in db.Ticket_Purchased_Detail
                                               where tQty.TPD_TQD_Id.ToString() == (from t in db.TicketOrderDetails
                                                                                    where t.T_Id == item.T_Id
                                                                                    select t.T_TQD_Id).FirstOrDefault().ToString()
                                                                                && tQty.TPD_GUID == guid
                                               select tQty
                                             ).FirstOrDefault();


                        var Ticketorderdetail = (from f in db.Order_Detail_T where f.O_Order_Id == item.T_Order_Id select f).FirstOrDefault();
                        var userdetail = (from prof in db.Profiles where prof.UserID == TPurchasedetail.TPD_User_Id select prof).FirstOrDefault();
                        var Edtails = (from p in db.Events
                                       join user in db.Profiles on p.UserID equals user.UserID
                                       join org in db.Event_Orgnizer_Detail on p.EventID equals org.Orgnizer_Event_Id
                                       join orgprof in db.Profiles on org.UserId equals orgprof.UserID
                                       where p.EventID == eventid && org.DefaultOrg == "Y"
                                       select new
                                       {
                                           EventTitle = p.EventTitle,
                                           UserName = user.FirstName,
                                           Organizername = orgprof.FirstName,
                                           OrganiserEmail = orgprof.Email,
                                           Addresstatus = p.AddressStatus,
                                       }).ToList().Distinct().FirstOrDefault();
                        var Organizerdetail = (from p in db.Event_Orgnizer_Detail join k in db.Organizer_Master on p.OrganizerMaster_Id equals k.Orgnizer_Id where p.Orgnizer_Event_Id == eventid && p.DefaultOrg == "Y" select k).FirstOrDefault();

                        var usernmae = !string.IsNullOrEmpty(Ticketorderdetail.O_First_Name + " " + Ticketorderdetail.O_Last_Name) ? Ticketorderdetail.O_First_Name + " " + Ticketorderdetail.O_Last_Name : Edtails.UserName;
                        var datetime = DateTime.Parse(TQtydetail.TQD_StartDate);
                        var day = datetime.DayOfWeek.ToString();
                        var Sdate = datetime.ToString("MMM dd, yyyy");
                        string Eventtype = "", Etype = "", add = "";

                        var addresslist = (from a in db.Addresses where a.AddressID == TQtydetail.TQD_AddressId select a).FirstOrDefault();
                        if ((addresslist) != null)
                        {
                            add = addresslist.ConsolidateAddress;
                        }
                        var time = TQtydetail.TQD_StartTime;

                        if (Edtails.Addresstatus == "PastLocation")
                        {
                            Eventtype = "Single";
                        }
                        else
                        {
                            Eventtype = Edtails.Addresstatus;
                        }
                        if (Edtails.Addresstatus == "Multiple")
                        {
                            Etype = "*This event has multiple venues";
                            add = add + "*";
                        }
                        else
                        {
                            Etype = "";
                        }

                        //

                        string xel = createxml(item.T_Order_Id, eventid, guid, TQtydetail, TPurchasedetail);

                        generateBarCode(item.T_Id, barImgPath);
                        generateQR(xel.ToString(), qrImgPath);


                        string Qrcode = "<img style = 'width:150px;height:150px' src ='" + qrImgPath + "' alt = 'QRCode' />";
                        string barcode = "<img  src ='" + barImgPath + "' alt = 'BarCode' >";
                        string Imagelogo = htmlPath + "/Images/logo_vertical.png";
                        string logoImage = "<img style='width:57px;height:375px' src ='" + Imagelogo + "' alt = 'Logo' >";
                        EventCreation ccEvent = new EventCreation();
                        var Images = ccEvent.GetImages(eventid).FirstOrDefault();
                        string Imageevent = "";
                        if (string.IsNullOrEmpty(Images))
                        {
                            Imageevent = htmlPath + "/Images/default_event_image.jpg";
                        }
                        else
                        {
                            Imageevent = htmlPath + Images;
                        }
                        string Imagevent = "<img style='width:200px;height:200px;' src ='" + Imageevent + "' alt = 'Image' >";

                        //Order Details
                        StringBuilder strHTML = new StringBuilder();
                        var itemQuery = from TqtId in db.Ticket_Purchased_Detail

                                        where TqtId.TPD_GUID == guid && TqtId.TPD_Event_Id == eventid

                                        select TqtId.TPD_TQD_Id;
                        var myOrderId = (from p in db.Ticket_Purchased_Detail where p.TPD_GUID == guid && p.TPD_Event_Id == eventid select p.TPD_Order_Id).ToList().Distinct().FirstOrDefault();
                        var myOrderDetails = (from p in db.Order_Detail_T where p.O_Order_Id == myOrderId select p).FirstOrDefault();

                        var myAddress = (from p in db.Ticket_Quantity_Detail where itemQuery.Contains(p.TQD_Id) select p.TQD_AddressId).ToList().Distinct();
                        var myaddress = (from p in db.Ticket_Quantity_Detail where itemQuery.Contains(p.TQD_Id) select p).ToList();

                        strHTML.Append("<table style='width: 100 %;'>");
                        var eventtype = "";
                        var eventname = "";
                        var startdate = "";
                        var enddate = "";
                        var Event = (from myEnt in db.Events where myEnt.EventID == eventid select myEnt).FirstOrDefault();

                        int timeZoneID = Int32.Parse(Event.TimeZone);
                        TimeZoneDetail td = db.TimeZoneDetails.First(x => x.TimeZone_Id == timeZoneID);
                        DateTimeWithZone dtzstart, dzend;
                        DateTimeWithZone dtzcreated;


                        var singledate = (from date in db.EventVenues where date.EventID == eventid select date).FirstOrDefault();
                        if (singledate != null)
                        {
                            if (td != null)
                            {
                                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td.TimeZone);
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(singledate.E_Startdate), userTimeZone, true);
                                dzend = new DateTimeWithZone(Convert.ToDateTime(singledate.E_Enddate), userTimeZone, true);
                            }
                            else
                            {
                                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(singledate.E_Startdate), userTimeZone, true);
                                dzend = new DateTimeWithZone(Convert.ToDateTime(singledate.E_Enddate), userTimeZone, true);
                            }


                        }
                        else
                        {
                            var muldate = (from date in db.MultipleEvents where date.EventID == eventid select date).FirstOrDefault();
                            if (td != null)
                            {
                                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td.TimeZone);
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(muldate.M_Startfrom), userTimeZone, true);
                                dzend = new DateTimeWithZone(Convert.ToDateTime(muldate.M_StartTo), userTimeZone, true);
                            }
                            else
                            {
                                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(muldate.M_Startfrom), userTimeZone, true);
                                dzend = new DateTimeWithZone(Convert.ToDateTime(muldate.M_StartTo), userTimeZone, true);
                            }


                        }
                        startdate = dtzstart.LocalTime.ToLongDateString() + " " + dtzstart.LocalTime.ToLongTimeString();
                        enddate = dzend.LocalTime.ToLongDateString() + " " + dzend.LocalTime.ToLongTimeString();

                        if (Edtails.Addresstatus == "Online")
                        {
                            eventname = "Online";

                        }
                        else if (Edtails.Addresstatus == "PastLocation")
                        {


                            var Address = (from address in db.Addresses
                                           where address.AddressID == (from fb in db.Events where fb.EventID == eventid select fb.LastLocationAddress).FirstOrDefault()
                                           select address).FirstOrDefault();

                            eventname = Address.ConsolidateAddress;
                            if (Address != null)
                            {
                                eventname = Address.ConsolidateAddress;
                            }
                            else
                            {
                                eventname = "";
                            }

                        }
                        else
                        {
                            var Address = (from address in db.Addresses
                                           where address.EventId == eventid
                                           select address).FirstOrDefault();
                            if (Address != null)
                            {
                                eventname = Address.ConsolidateAddress;
                            }
                            else
                            {
                                eventname = "";
                            }
                        }
                        if (myAddress.Count() == 1)
                        {

                            foreach (var item1 in myAddress)
                            {
                                if (item1 == 0)
                                {


                                    var myDatescnt = (from p in db.Ticket_Quantity_Detail where itemQuery.Contains(p.TQD_Id) select p.TQD_StartDate).ToList().Distinct();

                                    foreach (var vdate in myDatescnt)
                                    {
                                        //strHTML.Append("<tr> <p style='margin:0px;padding:0px;margin-bottom:5px;font-size:13px;color:#aaaaaa;'><span style='float:right;'>" + Convert.ToDateTime(vdate).ToString("MMM dd, yyyy") + " </span></p ></tr > ");
                                        strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                        strHTML.Append("<th style='font-weight:normal;padding:10px5px;border-bottom:1px dashed #ccc;'>Name</th>");
                                        strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Type </th>");
                                        strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Quantity </th>");
                                        strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Price </th>");
                                        strHTML.Append("</tr>");
                                        var itemtoadd = (from p in db.Ticket_Quantity_Detail
                                                         join TP in db.Ticket_Purchased_Detail on p.TQD_Id equals TP.TPD_TQD_Id
                                                         join l in db.Profiles on TP.TPD_User_Id equals l.UserID
                                                         join t in db.Tickets on p.TQD_Ticket_Id equals t.T_Id
                                                         where p.TQD_StartDate == vdate && TP.TPD_GUID == guid && TP.TPD_Event_Id == eventid
                                                         select new
                                                         {
                                                             eventid = TP.TPD_Event_Id,
                                                             Promocode = TP.TPD_PromoCodeID,
                                                             Promocodeamt = TP.TPD_PromoCodeAmount,
                                                             username = l.FirstName,
                                                             Ticketname = t.T_name,
                                                             Quantity = TP.TPD_Purchased_Qty,
                                                             Price = TP.TPD_Amount > 0 ? "$ " + TP.TPD_Amount.ToString() : TP.TPD_Donate > 0 ? "$ " + TP.TPD_Donate.ToString() : "Free"

                                                         }).ToList();


                                        foreach (var qty in itemtoadd)
                                        {

                                            if (qty.Promocode != null && qty.Promocode != 0)
                                            {
                                                strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + (!string.IsNullOrEmpty(fname) ? fname : qty.username) + "</td>");
                                                strHTML.Append("<td style='width:30%;font-size:15px; padding: 10px 5px;'>" + qty.Ticketname + "</td>");
                                                strHTML.Append("<td style='width:10%font-size:15px; padding: 10px 5px;'>" + qty.Quantity + "</td>");
                                                strHTML.Append("<td style='width:30%;font-size:15px; padding: 10px 5px;'>" + qty.Price + "</td>");
                                                strHTML.Append("</tr>");
                                                var promocode = (from v in db.Promo_Code where v.PC_Eventid == qty.eventid && v.PC_id == qty.Promocode select v.PC_Code).FirstOrDefault();
                                                strHTML.Append("<tr align='left'>");
                                                strHTML.Append("<td colspan='3' style='font-size:15px; padding:0px 5px 10px 5px;color: green; border-bottom:1px dashed #ccc;'>" + promocode + "</td>");
                                                var promoprice = qty.Promocodeamt * qty.Quantity;
                                                strHTML.Append("<td colspan='1' style='font-size:15px; color: green;padding:0px 5px 10px 5px; border-bottom:1px dashed #ccc;'>-$" + promoprice + "</td>");
                                                strHTML.Append("</tr>");
                                            }
                                            else
                                            {
                                                strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + (!string.IsNullOrEmpty(fname) ? fname : qty.username) + "</td>");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Ticketname + "</td>");
                                                strHTML.Append("<td style='width:10%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Quantity + "</td>");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Price + "</td>");
                                                strHTML.Append("</tr>");
                                            }



                                        }



                                    }
                                }
                                else
                                {
                                    var addressdetail = (from p in db.Addresses where p.AddressID == item1 select p.ConsolidateAddress).FirstOrDefault();
                                    //strHTML.Append("<tr> <p style='margin:0px;padding:0px;margin-bottom:5px;font-size:13px;color:#aaaaaa;'>" + addressdetail + "</p ></tr > ");
                                    var myDatescnt = (from p in db.Ticket_Quantity_Detail where itemQuery.Contains(p.TQD_Id) select p.TQD_StartDate).ToList().Distinct();
                                    foreach (var vdate in myDatescnt)
                                    {
                                        var itemtoadd = (from p in db.Ticket_Quantity_Detail
                                                         join TP in db.Ticket_Purchased_Detail on p.TQD_Id equals TP.TPD_TQD_Id
                                                         join l in db.Profiles on TP.TPD_User_Id equals l.UserID
                                                         join t in db.Tickets on p.TQD_Ticket_Id equals t.T_Id
                                                         where p.TQD_StartDate == vdate && TP.TPD_GUID == guid && TP.TPD_Event_Id == eventid
                                                         select new
                                                         {
                                                             eventid = TP.TPD_Event_Id,
                                                             Promocode = TP.TPD_PromoCodeID,
                                                             Promocodeamt = TP.TPD_PromoCodeAmount,
                                                             username = l.FirstName,
                                                             Ticketname = t.T_name,
                                                             Quantity = TP.TPD_Purchased_Qty,
                                                             Price = TP.TPD_Amount > 0 ? "$ " + TP.TPD_Amount.ToString() : TP.TPD_Donate > 0 ? "$ " + TP.TPD_Donate.ToString() : "Free"

                                                         }).ToList();

                                        //strHTML.Append("<tr> <p style='margin:0px;padding:0px;margin-bottom:5px;font-size:13px;color:#aaaaaa;'><span style='float:right;'>" + Convert.ToDateTime(vdate).ToString("MMM dd, yyyy") + " </span></p ></tr > ");
                                        strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                        strHTML.Append("<th style='font-weight:normal;padding:10px5px;border-bottom:1px dashed #ccc;'>Name</th>");
                                        strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Type </th>");
                                        strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Quantity </th>");
                                        strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Price </th>");
                                        strHTML.Append("</tr>");
                                        foreach (var qty in itemtoadd)
                                        {

                                            if (qty.Promocode != null && qty.Promocode != 0)
                                            {
                                                strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + (!string.IsNullOrEmpty(fname) ? fname : qty.username) + "</td>");
                                                strHTML.Append("<td style='width:30%;font-size:15px; padding: 10px 5px;'>" + qty.Ticketname + "</td>");
                                                strHTML.Append("<td style='width:10%font-size:15px; padding: 10px 5px;'>" + qty.Quantity + "</td>");
                                                strHTML.Append("<td style='width:30%;font-size:15px; padding: 10px 5px;'>" + qty.Price + "</td>");
                                                strHTML.Append("</tr>");
                                                var promocode = (from v in db.Promo_Code where v.PC_Eventid == qty.eventid && v.PC_id == qty.Promocode select v.PC_Code).FirstOrDefault();
                                                strHTML.Append("<tr align='left'>");
                                                strHTML.Append("<td colspan='3' style='font-size:15px; padding:0px 5px 10px 5px;color: green; border-bottom:1px dashed #ccc;'>" + promocode + "</td>");
                                                var promoprice = qty.Promocodeamt * qty.Quantity;
                                                strHTML.Append("<td colspan='1' style='font-size:15px; color: green;padding:0px 5px 10px 5px; border-bottom:1px dashed #ccc;'>-$" + promoprice + "</td>");
                                                strHTML.Append("</tr>");
                                            }
                                            else
                                            {
                                                strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + (!string.IsNullOrEmpty(fname) ? fname : qty.username) + "</td>");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Ticketname + "</td>");
                                                strHTML.Append("<td style='width:10%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Quantity + "</td>");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Price + "</td>");
                                                strHTML.Append("</tr>");
                                            }



                                        }



                                    }




                                }

                            }



                        }
                        else
                        {

                            foreach (var item1 in myAddress)
                            {

                                var itemtoadd = (from p in db.Ticket_Quantity_Detail
                                                 join TP in db.Ticket_Purchased_Detail on p.TQD_Id equals TP.TPD_TQD_Id
                                                 join l in db.Profiles on TP.TPD_User_Id equals l.UserID
                                                 join t in db.Tickets on p.TQD_Ticket_Id equals t.T_Id
                                                 where p.TQD_AddressId == item1 && TP.TPD_GUID == guid && TP.TPD_Event_Id == eventid
                                                 select new
                                                 {
                                                     eventid = TP.TPD_Event_Id,
                                                     Promocode = TP.TPD_PromoCodeID,
                                                     Promocodeamt = TP.TPD_PromoCodeAmount,
                                                     username = l.FirstName,
                                                     Ticketname = t.T_name,
                                                     Quantity = TP.TPD_Purchased_Qty,
                                                     Price = TP.TPD_Amount > 0 ? "$ " + TP.TPD_Amount.ToString() : TP.TPD_Donate > 0 ? "$ " + TP.TPD_Donate.ToString() : "Free"

                                                 }).ToList();

                                var dateofaddress = (from p in db.Ticket_Quantity_Detail
                                                     join TP in db.Ticket_Purchased_Detail on p.TQD_Id equals TP.TPD_TQD_Id
                                                     where p.TQD_AddressId == item1 && TP.TPD_GUID == guid && TP.TPD_Event_Id == eventid
                                                     select p.TQD_StartDate).ToList().Distinct().FirstOrDefault();
                                var addressdetail = (from p in db.Addresses where p.AddressID == item1 select p.ConsolidateAddress).FirstOrDefault();

                                strHTML.Append("<tr> <p style='margin:0px;padding:0px;margin-bottom:5px;font-size:13px;color:#aaaaaa;'>" + addressdetail + "<span style='float:right;'>" + Convert.ToDateTime(dateofaddress).ToString("MMM dd, yyyy") + " </span></p ></tr > ");
                                strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                strHTML.Append("<th style='font-weight:normal;padding:10px5px;border-bottom:1px dashed #ccc;'>Name</th>");
                                strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;' > Type </th >");
                                strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Quantity </th>");
                                strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Price </ th >");
                                strHTML.Append("</tr>");
                                foreach (var qty in itemtoadd)
                                {

                                    if (qty.Promocode != null && qty.Promocode != 0)
                                    {
                                        strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + (!string.IsNullOrEmpty(fname) ? fname : qty.username) + "</td>");
                                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + qty.Ticketname + "</td>");
                                        strHTML.Append("<td style='width:10%; font-size:15px; padding: 10px 5px;'>" + qty.Quantity + "</td>");
                                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + qty.Price + "</td>");
                                        strHTML.Append("</tr>");
                                        var promocode = (from v in db.Promo_Code where v.PC_Eventid == qty.eventid && v.PC_id == qty.Promocode select v.PC_Code).FirstOrDefault();
                                        strHTML.Append("<tr align='left'>");
                                        strHTML.Append("<td colspan='3' style='font-size:15px; padding:0px 5px 10px 5px;color: green; border-bottom:1px dashed #ccc;'>" + promocode + "</td>");
                                        var promoprice = qty.Promocodeamt * qty.Quantity;
                                        strHTML.Append("<td colspan='1' style='font-size:15px; color: green;padding:0px 5px 10px 5px; border-bottom:1px dashed #ccc;'>-$" + promoprice + "</td>");
                                        strHTML.Append("</tr>");
                                    }
                                    else
                                    {
                                        strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + (!string.IsNullOrEmpty(fname) ? fname : qty.username) + "</td>");
                                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Ticketname + "</td>");
                                        strHTML.Append("<td style='width:10%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Quantity + "</td>");
                                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Price + "</td>");
                                        strHTML.Append("</tr>");
                                    }
                                }

                            }


                        }

                        var variabledescid = myOrderDetails.O_VariableId;
                        if (!string.IsNullOrWhiteSpace(variabledescid))
                        {
                            if (variabledescid.Contains(','))
                            {
                                string[] words = variabledescid.Split(',');
                                foreach (string word in words)
                                {
                                    var vardesc = (from p in db.Event_VariableDesc
                                                   where p.Variable_Id.ToString() == word && p.Event_Id == eventid
                                                   select p).FirstOrDefault();
                                    strHTML.Append("<tr align='right'> ");
                                    strHTML.Append("<td colspan='4' style='font-size:15px;font-weight:bold;padding:10px 5px;border-bottom:1px solid #ccc;'>" + vardesc.VariableDesc + ":$ " + vardesc.Price + " </td>");
                                    strHTML.Append("</tr> ");
                                }
                            }
                            else
                            {
                                var vardesc = (from p in db.Event_VariableDesc
                                               where p.Variable_Id.ToString() == variabledescid && p.Event_Id == eventid
                                               select p).FirstOrDefault();

                                if (vardesc != null)
                                {
                                    strHTML.Append("<tr align='right'> ");
                                    strHTML.Append("<td colspan='4' style='font-size:15px;font-weight:bold;padding:10px 5px;border-bottom:1px solid #ccc;'>" + vardesc.VariableDesc + ":$ " + vardesc.Price + " </td>");
                                    strHTML.Append("</tr> ");
                                }
                            }
                        }
                        strHTML.Append("<tr align='right'> ");
                        strHTML.Append("<td colspan='4' style='font-size:15px;font-weight:bold;padding:10px 5px;border-bottom:1px solid #ccc;'>Total :$" + myOrderDetails.O_TotalAmount + " </td></tr>");
                        if (Edtails.Addresstatus == "Multiple")
                        {
                            eventtype = "* This event has multiple venues ";
                        }
                        else
                        {
                            eventtype = "";
                        }


                        var myBillingdeatils = (from p in db.BillingAddresses where p.Guid == guid && p.OrderId == myOrderId select p).ToList().Distinct().FirstOrDefault();

                        var cardtext = "";


                        if (myBillingdeatils != null)
                        {
                            var Scardnumber = Ecode.DecryptText(myBillingdeatils.CardId).Trim();
                            var Icardlength = Scardnumber.Length;
                            var WrVS = "";
                            int k = 1;
                            for (int i = 0; i < Icardlength; i++)
                            {

                                WrVS += "X";
                                if (k == 4)
                                {
                                    WrVS += "-";
                                    k = 0;
                                }


                                k++;

                            }
                            if (WrVS.EndsWith("-"))
                            {
                                WrVS = WrVS.Substring(0, WrVS.LastIndexOf("-"));
                            }
                            var rvrs = WrVS.Substring(0, WrVS.LastIndexOf("-") + 1);
                            var rvrsd = rvrs.Replace("-", "");
                            var chrlength = Icardlength - rvrsd.Length;
                            var result = Scardnumber.Substring(Icardlength - Math.Min(chrlength, Icardlength));
                            var finalstr = rvrs + result;

                            var touper = "";

                            if (!string.IsNullOrWhiteSpace(myBillingdeatils.card_type))
                            {
                                var cardtype = Ecode.DecryptText(myBillingdeatils.card_type);
                                touper = char.ToUpper(cardtype[0]) + cardtype.Substring(1);
                            }
                            else
                            {
                                touper = "";
                            }

                            cardtext = "Charge to :" + touper + "  " + finalstr;
                        }
                        else
                        {
                            cardtext = "Charge to : Paypal";
                        }
                        if (myOrderDetails.O_TotalAmount <= 0)
                        {
                            cardtext = "";
                        }
                        strHTML.Append("<tr align='center'> ");
                        strHTML.Append("<td colspan='4' style='font-size:15px; padding:10px 5px;'>" + cardtext + " </td></tr>");
                        strHTML.Append("<tr align='center'><td colspan='4' style='font-size:15px;'>");
                        strHTML.Append("<p style='background:#fff9cf; padding:10px 15px; display:inline-block; margin:0px;'>This charge will appear on your card statement as Eventcombo * { " + Edtails.EventTitle + "}</p>");
                        strHTML.Append("<p style='color:#696564;' >This order is subject to Eventcombo '");
                        strHTML.Append("<a href='#' style='color:#0f90ba;'>Terms of Service </a> , <a style='color:#0f90ba;' href='#'>Privacy Policy </a> and <a style='color:#0f90ba;' href='#'>Cookie Policy </a></p>");
                        strHTML.Append("</td></tr></table > ");



                        //var query = (from p in db.Ticket_Purchased_Detail
                        //             join o in db.Ticket_Quantity_Detail on p.TPD_TQD_Id equals o.TQD_Id
                        //             join t in db.Tickets on o.TQD_Ticket_Id equals t.T_Id
                        //             where p.TPD_GUID == guid && p.TPD_Event_Id == eventid
                        //             group new {p.TPD_Event_Id,p.TPD_PromoCodeAmount,p.TPD_PromoCodeID, t.T_Id, p.TPD_Purchased_Qty, t.TotalPrice, t.T_Discount, t.T_name, t.TicketTypeID, p.TPD_Donate }
                        //             by new { p.TPD_Event_Id, p.TPD_PromoCodeAmount, p.TPD_PromoCodeID, t.T_Id, p.TPD_Purchased_Qty, t.TotalPrice, t.T_Discount, t.T_name, t.TicketTypeID, p.TPD_Donate } into g
                        //             select new
                        //             {
                        //                 Qty = g.Sum(x => x.TPD_Purchased_Qty),
                        //                 amount = (g.Key.TotalPrice == null ? 0 : g.Key.TotalPrice) - (g.Key.T_Discount == null ? 0 : g.Key.T_Discount),
                        //                 Tname = g.Key.T_name,
                        //                 TicketType = g.Key.TicketTypeID,
                        //                 donate = g.Sum(x => x.TPD_Donate),
                        //                 ticketid = g.Key.T_Id,
                        //                 Promocodeamt=g.Key.TPD_PromoCodeAmount,
                        //                 Promocode=g.Key.TPD_PromoCodeID,
                        //                 eventid=g.Key.TPD_Event_Id
                        //             }).ToList();
                        //string orderdet = "", freetype = "", paidtype = "", donatertype = "";
                        //orderdet = " " + Ticketorderdetail.O_First_Name!=null? Ticketorderdetail.O_First_Name:userdetail.FirstName + " has  ";
                        //foreach (var i in query)
                        //{
                        //    var promocode = (from v in db.Promo_Code where v.PC_Eventid == i.eventid && v.PC_id == i.Promocode select v.PC_Code).FirstOrDefault();
                        //    if (i.TicketType == 1)
                        //    {
                        //        if(!string.IsNullOrEmpty(freetype))
                        //        {
                        //            freetype = freetype + " , ";
                        //        }
                        //        freetype = freetype + " " + i.Qty + " * " + i.Tname + " free type for $0.00 ";
                        //        if(i.Promocode != null && i.Promocode!=0)
                        //        {

                        //            freetype = freetype + " after applying promotion code "+ promocode+"  per ticket ";
                        //        }
                        //    }
                        //    if (i.TicketType == 2)
                        //    {
                        //        if(!string.IsNullOrEmpty(paidtype))
                        //        {
                        //            paidtype = paidtype + " , ";
                        //        }
                        //        paidtype = paidtype+ " " + i.Qty + " * " + i.Tname + " paid type for $ " + i.amount;
                        //        if (i.Promocode != null && i.Promocode != 0)
                        //        {

                        //            paidtype = paidtype + " after applying promotion code " + promocode + "  per ticket ";
                        //        }

                        //    }
                        //    if (i.TicketType == 3)
                        //    {
                        //        if (!string.IsNullOrEmpty(donatertype))
                        //        {
                        //            donatertype = donatertype + " , ";
                        //        }
                        //        donatertype = donatertype+ "  Donated for " + i.Tname + " type for $ " + i.donate;
                        //        if (i.Promocode != null && i.Promocode != 0)
                        //        {

                        //            donatertype = donatertype + " after applying promotion code " + promocode + "  per ticket ";
                        //        }
                        //    }

                        //}
                        //if (!string.IsNullOrEmpty(paidtype) && !string.IsNullOrEmpty(freetype) && !string.IsNullOrEmpty(donatertype))
                        //{
                        //    orderdet = orderdet + paidtype + " , " + freetype + " and " + donatertype;
                        //}
                        //if (!string.IsNullOrEmpty(paidtype) && !string.IsNullOrEmpty(freetype) && string.IsNullOrEmpty(donatertype))
                        //{
                        //    orderdet = orderdet + paidtype + " and " + freetype;
                        //}
                        //if (string.IsNullOrEmpty(paidtype) && !string.IsNullOrEmpty(freetype) && !string.IsNullOrEmpty(donatertype))
                        //{
                        //    orderdet = orderdet + freetype + " and " + donatertype;
                        //}
                        //if (string.IsNullOrEmpty(paidtype) && !string.IsNullOrEmpty(freetype) && string.IsNullOrEmpty(donatertype))
                        //{
                        //    orderdet = orderdet + freetype;
                        //}
                        //if (!string.IsNullOrEmpty(paidtype) && string.IsNullOrEmpty(freetype) && !string.IsNullOrEmpty(donatertype))
                        //{
                        //    orderdet = orderdet + paidtype + " and " + donatertype;
                        //}
                        //if (!string.IsNullOrEmpty(paidtype) && string.IsNullOrEmpty(freetype) && string.IsNullOrEmpty(donatertype))
                        //{
                        //    orderdet = orderdet + paidtype;
                        //}
                        //if (string.IsNullOrEmpty(paidtype) && string.IsNullOrEmpty(freetype) && !string.IsNullOrEmpty(donatertype))
                        //{
                        //    orderdet = orderdet + donatertype;
                        //}
                        //var totorder = (from o in db.Order_Detail_T where o.O_Order_Id == TPurchasedetail.TPD_Order_Id select o.O_TotalAmount).FirstOrDefault();

                        //orderdet = orderdet + " for a total of $ " + totorder;
                        //Order Details

                        htmlText += wc.DownloadString(htmlPath + "/email.html");

                        htmlText = htmlText.Replace("¶¶EventTitleId¶¶", Edtails.EventTitle);
                        htmlText = htmlText.Replace("¶¶EventStartDateID¶¶", Sdate);
                        htmlText = htmlText.Replace("¶¶EventVenueID¶¶", add);
                        htmlText = htmlText.Replace("¶¶EventOrderNO¶¶", item.T_Order_Id);
                        htmlText = htmlText.Replace("¶¶UserFirstNameID¶¶", fname);
                        //htmlText = htmlText.Replace("¶¶UserLastNameID¶¶", userdetail.LastName);
                        htmlText = htmlText.Replace("¶¶TicketOrderDateId¶¶", System.DateTime.Now.ToLongDateString());
                        htmlText = htmlText.Replace("¶¶EventStartTimeID¶¶", time);
                        htmlText = htmlText.Replace("¶¶EventBarcodeId¶¶", barcode);
                        htmlText = htmlText.Replace("¶¶EventQrCode¶¶", Qrcode);
                        htmlText = htmlText.Replace("¶¶EventImage¶¶", Imagevent);
                        htmlText = htmlText.Replace("¶¶EventdayId¶¶", day);
                        htmlText = htmlText.Replace("¶¶EventLogo¶¶", logoImage);
                        htmlText = htmlText.Replace("¶¶Eventtype¶¶", Eventtype);
                        htmlText = htmlText.Replace("¶¶EventDescription¶¶", "");
                        htmlText = htmlText.Replace("¶¶Eventtypedetail¶¶", Etype);
                        htmlText = htmlText.Replace("¶¶OrderDetail¶¶", strHTML.ToString());
                        htmlText = htmlText.Replace("¶¶EventOrganiserName¶¶", !string.IsNullOrEmpty(Organizerdetail.Orgnizer_Name) ? Organizerdetail.Orgnizer_Name : Edtails.Organizername);
                        htmlText = htmlText.Replace("¶¶EventOrganiserEmail¶¶", !string.IsNullOrEmpty(Organizerdetail.Organizer_Email) ? Organizerdetail.Organizer_Email : Edtails.OrganiserEmail);
                        if (count == lastcount)
                        {
                            htmlText = htmlText.Replace("¶¶Linebreak¶¶", "");
                        }
                        else
                        {
                            htmlText = htmlText.Replace("¶¶Linebreak¶¶", "<div style='page-break-before: always;width:100% text - align: center></div>");
                        }
                    }



                    var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                    var pdfBytes = htmlToPdf.GeneratePdf(htmlText);
                    mms = new MemoryStream(pdfBytes);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Exception during request processing", ex);
            }
            return mms;

        }
        public MemoryStream generateTicketPDFManualOrder(string guid, long eventid, List<Email_Tag> emailtag, string fname, string htmlPath)
        {

            WebClient wc = new WebClient();
            MemoryStream mms = new MemoryStream();
            EncryptDecrypt Ecode = new EncryptDecrypt();
            string htmlText = "";
            try
            {
                var TicketDetail = (from Ord in db.TicketOrderDetails
                                    where Ord.T_Guid == guid
                                    select Ord).ToList();
                if (TicketDetail != null)
                {
                    var count = 0;
                    var lastcount = TicketDetail.Count();
                    List<string> AddedAutoIds = new List<string>();
                    List<TicketAttendeeWithSingleQty> ticketAttendeeWithSingleQtys = new List<TicketAttendeeWithSingleQty>();
                    TicketAttendeeWithSingleQty ticketAttendeeWithSingleQty;
                    var OrderId = TicketDetail.FirstOrDefault().T_Order_Id;
                    var ticketAttendees = db.TicketAttendee_View.Where(ta => ta.OrderId == OrderId);
                    int tempNumber = 0;
                    foreach (var item in ticketAttendees)
                    {
                        tempNumber = 0;
                        for (int i = 1; i <= item.Quantity; i++)
                        {
                            tempNumber += 1;
                            ticketAttendeeWithSingleQty = new TicketAttendeeWithSingleQty();
                            ticketAttendeeWithSingleQty.TicketAttendeeAutoId = item.TicketBearerId + "_" + tempNumber;
                            ticketAttendeeWithSingleQty.TicketBearerId = item.TicketBearerId;
                            ticketAttendeeWithSingleQty.OrderId = item.OrderId;
                            ticketAttendeeWithSingleQty.PurchasedTicketId = item.PurchasedTicketId;
                            ticketAttendeeWithSingleQty.Name = item.Name;
                            ticketAttendeeWithSingleQtys.Add(ticketAttendeeWithSingleQty);
                        }
                    }
                    foreach (var item in TicketDetail)
                    {
                        count = count + 1;

                        string barImgPath = htmlPath + "/Images/br_" + item.T_Id + ".Png";

                        string qrImgPath = htmlPath + "/Images/QR_" + item.T_Id + ".Png";

                        var TQtydetail = (from tQty in db.Ticket_Quantity_Detail
                                          where tQty.TQD_Id.ToString() == (from t in db.TicketOrderDetails
                                                                           where t.T_Id == item.T_Id
                                                                           select t.T_TQD_Id).FirstOrDefault().ToString()
                                          select tQty
                                  ).FirstOrDefault();

                        var TPurchasedetail = (from tQty in db.Ticket_Purchased_Detail
                                               where tQty.TPD_TQD_Id.ToString() == (from t in db.TicketOrderDetails
                                                                                    where t.T_Id == item.T_Id
                                                                                    select t.T_TQD_Id).FirstOrDefault().ToString()
                                                                                && tQty.TPD_GUID == guid
                                               select tQty
                                             ).FirstOrDefault();


                        var Ticketorderdetail = (from f in db.Order_Detail_T where f.O_Order_Id == item.T_Order_Id select f).FirstOrDefault();
                        var userdetail = (from prof in db.Profiles where prof.UserID == TPurchasedetail.TPD_User_Id select prof).FirstOrDefault();
                        var Edtails = (from p in db.Events
                                       join user in db.Profiles on p.UserID equals user.UserID
                                       join org in db.Event_Orgnizer_Detail on p.EventID equals org.Orgnizer_Event_Id
                                       join orgprof in db.Profiles on org.UserId equals orgprof.UserID
                                       where p.EventID == eventid && org.DefaultOrg == "Y"
                                       select new
                                       {
                                           EventTitle = p.EventTitle,
                                           UserName = user.FirstName,
                                           Organizername = orgprof.FirstName,
                                           OrganiserEmail = orgprof.Email,
                                           Addresstatus = p.AddressStatus,
                                       }).ToList().Distinct().FirstOrDefault();
                        var Organizerdetail = (from p in db.Event_Orgnizer_Detail join k in db.Organizer_Master on p.OrganizerMaster_Id equals k.Orgnizer_Id where p.Orgnizer_Event_Id == eventid && p.DefaultOrg == "Y" select k).FirstOrDefault();

                        var usernmae = !string.IsNullOrEmpty(Ticketorderdetail.O_First_Name + " " + Ticketorderdetail.O_Last_Name) ? Ticketorderdetail.O_First_Name + " " + Ticketorderdetail.O_Last_Name : Edtails.UserName;
                        var datetime = DateTime.Parse(TQtydetail.TQD_StartDate);
                        var day = datetime.DayOfWeek.ToString();
                        var Sdate = datetime.ToString("MMM dd, yyyy");
                        string Eventtype = "", Etype = "", add = "";

                        var addresslist = (from a in db.Addresses where a.AddressID == TQtydetail.TQD_AddressId select a).FirstOrDefault();
                        if ((addresslist) != null)
                        {
                            add = addresslist.ConsolidateAddress;
                        }
                        var time = TQtydetail.TQD_StartTime;

                        if (Edtails.Addresstatus == "PastLocation")
                        {
                            Eventtype = "Single";
                        }
                        else
                        {
                            Eventtype = Edtails.Addresstatus;
                        }
                        if (Edtails.Addresstatus == "Multiple")
                        {
                            Etype = "*This event has multiple venues";
                            add = add + "*";
                        }
                        else
                        {
                            Etype = "";
                        }

                        //

                        string xel = createxml(item.T_Order_Id, eventid, guid, TQtydetail, TPurchasedetail);

                        generateBarCode(item.T_Id, barImgPath);
                        generateQR(xel.ToString(), qrImgPath);


                        string Qrcode = "<img style = 'width:150px;height:150px' src ='" + qrImgPath + "' alt = 'QRCode' />";
                        string barcode = "<img  src ='" + barImgPath + "' alt = 'BarCode' >";
                        string Imagelogo = htmlPath + "/Images/logo_vertical.png";
                        string logoImage = "<img style='width:57px;height:375px' src ='" + Imagelogo + "' alt = 'Logo' >";
                        EventCreation ccEvent = new EventCreation();
                        var Images = ccEvent.GetImages(eventid).FirstOrDefault();
                        string Imageevent = "";
                        if (string.IsNullOrEmpty(Images))
                        {
                            Imageevent = htmlPath + "/Images/default_event_image.jpg";
                        }
                        else
                        {
                            Imageevent = htmlPath + Images;
                        }
                        string Imagevent = "<img style='width:200px;height:200px;' src ='" + Imageevent + "' alt = 'Image' >";

                        //Order Details
                        StringBuilder strHTML = new StringBuilder();
                        var itemQuery = from TqtId in db.Ticket_Purchased_Detail

                                        where TqtId.TPD_GUID == guid && TqtId.TPD_Event_Id == eventid

                                        select TqtId.TPD_TQD_Id;
                        var myOrderId = (from p in db.Ticket_Purchased_Detail where p.TPD_GUID == guid && p.TPD_Event_Id == eventid select p.TPD_Order_Id).ToList().Distinct().FirstOrDefault();
                        var myOrderDetails = (from p in db.Order_Detail_T where p.O_Order_Id == myOrderId select p).FirstOrDefault();

                        var myAddress = (from p in db.Ticket_Quantity_Detail where itemQuery.Contains(p.TQD_Id) select p.TQD_AddressId).ToList().Distinct();
                        var myaddress = (from p in db.Ticket_Quantity_Detail where itemQuery.Contains(p.TQD_Id) select p).ToList();

                        var ticketAttendeeWithSingle = ticketAttendeeWithSingleQtys.Where(ta => ta.OrderId == item.T_Order_Id && ta.PurchasedTicketId == TPurchasedetail.TPD_Id && !AddedAutoIds.Contains(ta.TicketAttendeeAutoId)).FirstOrDefault();
                        if (ticketAttendeeWithSingle != null)
                        {
                            fname = ticketAttendeeWithSingle.Name;
                            AddedAutoIds.Add(ticketAttendeeWithSingle.TicketAttendeeAutoId);
                        }

                        strHTML.Append("<table style='width: 100 %;'>");
                        var eventtype = "";
                        var eventname = "";
                        var startdate = "";
                        var enddate = "";
                        var Event = (from myEnt in db.Events where myEnt.EventID == eventid select myEnt).FirstOrDefault();

                        int timeZoneID = Int32.Parse(Event.TimeZone);
                        TimeZoneDetail td = db.TimeZoneDetails.First(x => x.TimeZone_Id == timeZoneID);
                        DateTimeWithZone dtzstart, dzend;
                        DateTimeWithZone dtzcreated;


                        var singledate = (from date in db.EventVenues where date.EventID == eventid select date).FirstOrDefault();
                        if (singledate != null)
                        {
                            if (td != null)
                            {
                                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td.TimeZone);
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(singledate.E_Startdate), userTimeZone, true);
                                dzend = new DateTimeWithZone(Convert.ToDateTime(singledate.E_Enddate), userTimeZone, true);
                            }
                            else
                            {
                                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(singledate.E_Startdate), userTimeZone, true);
                                dzend = new DateTimeWithZone(Convert.ToDateTime(singledate.E_Enddate), userTimeZone, true);
                            }


                        }
                        else
                        {
                            var muldate = (from date in db.MultipleEvents where date.EventID == eventid select date).FirstOrDefault();
                            if (td != null)
                            {
                                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td.TimeZone);
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(muldate.M_Startfrom), userTimeZone, true);
                                dzend = new DateTimeWithZone(Convert.ToDateTime(muldate.M_StartTo), userTimeZone, true);
                            }
                            else
                            {
                                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(muldate.M_Startfrom), userTimeZone, true);
                                dzend = new DateTimeWithZone(Convert.ToDateTime(muldate.M_StartTo), userTimeZone, true);
                            }


                        }
                        startdate = dtzstart.LocalTime.ToLongDateString() + " " + dtzstart.LocalTime.ToLongTimeString();
                        enddate = dzend.LocalTime.ToLongDateString() + " " + dzend.LocalTime.ToLongTimeString();

                        if (Edtails.Addresstatus == "Online")
                        {
                            eventname = "Online";

                        }
                        else if (Edtails.Addresstatus == "PastLocation")
                        {


                            var Address = (from address in db.Addresses
                                           where address.AddressID == (from fb in db.Events where fb.EventID == eventid select fb.LastLocationAddress).FirstOrDefault()
                                           select address).FirstOrDefault();

                            eventname = Address.ConsolidateAddress;
                            if (Address != null)
                            {
                                eventname = Address.ConsolidateAddress;
                            }
                            else
                            {
                                eventname = "";
                            }

                        }
                        else
                        {
                            var Address = (from address in db.Addresses
                                           where address.EventId == eventid
                                           select address).FirstOrDefault();
                            if (Address != null)
                            {
                                eventname = Address.ConsolidateAddress;
                            }
                            else
                            {
                                eventname = "";
                            }
                        }

                        if (myAddress.Count() == 1)
                        {

                            foreach (var item1 in myAddress)
                            {
                                if (item1 == 0)
                                {


                                    var myDatescnt = (from p in db.Ticket_Quantity_Detail where itemQuery.Contains(p.TQD_Id) select p.TQD_StartDate).ToList().Distinct();

                                    foreach (var vdate in myDatescnt)
                                    {
                                        //strHTML.Append("<tr> <p style='margin:0px;padding:0px;margin-bottom:5px;font-size:13px;color:#aaaaaa;'><span style='float:right;'>" + Convert.ToDateTime(vdate).ToString("MMM dd, yyyy") + " </span></p ></tr > ");
                                        strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                        strHTML.Append("<th style='font-weight:normal;padding:10px5px;border-bottom:1px dashed #ccc;'>Name</th>");
                                        strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Type </th>");
                                        strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Quantity </th>");
                                        strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Price </th>");
                                        strHTML.Append("</tr>");
                                        var itemtoadd = (from p in db.Ticket_Quantity_Detail
                                                         join TP in db.Ticket_Purchased_Detail on p.TQD_Id equals TP.TPD_TQD_Id
                                                         join ta in db.TicketAttendees on TP.TPD_Id equals ta.PurchasedTicketId
                                                         join tb in db.TicketBearers on ta.TicketBearerId equals tb.TicketbearerId
                                                         join t in db.Tickets on p.TQD_Ticket_Id equals t.T_Id
                                                         where p.TQD_StartDate == vdate && TP.TPD_GUID == guid && TP.TPD_Event_Id == eventid
                                                         select new
                                                         {
                                                             eventid = TP.TPD_Event_Id,
                                                             Promocode = TP.TPD_PromoCodeID,
                                                             Promocodeamt = TP.TPD_PromoCodeAmount,
                                                             attendeeName = tb.Name,
                                                             Ticketname = t.T_name,
                                                             Quantity = ta.Quantity,
                                                             TPD_Amount = TP.TPD_Amount,
                                                             TPD_Purchased_Qty = TP.TPD_Purchased_Qty,
                                                             TPD_Donate = TP.TPD_Donate

                                                         }).ToList();


                                        foreach (var qty in itemtoadd)
                                        {
                                            if (qty.Promocode != null && qty.Promocode != 0)
                                            {
                                                strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + qty.attendeeName + "</td>");
                                                strHTML.Append("<td style='width:30%;font-size:15px; padding: 10px 5px;'>" + qty.Ticketname + "</td>");
                                                strHTML.Append("<td style='width:10%font-size:15px; padding: 10px 5px;'>" + qty.Quantity + "</td>");
                                                strHTML.Append("<td style='width:30%;font-size:15px; padding: 10px 5px;'>" + (qty.TPD_Amount > 0 ? "$ " + decimal.Truncate((qty.Quantity * qty.TPD_Amount / qty.TPD_Purchased_Qty ?? 0) * 100) / 100 : qty.TPD_Donate > 0 ? "$ " + decimal.Truncate((qty.Quantity * qty.TPD_Donate / qty.TPD_Purchased_Qty ?? 0) * 100) / 100 : "Free") + "</td>");
                                                strHTML.Append("</tr>");
                                                var promocode = (from v in db.Promo_Code where v.PC_Eventid == qty.eventid && v.PC_id == qty.Promocode select v.PC_Code).FirstOrDefault();
                                                strHTML.Append("<tr align='left'>");
                                                strHTML.Append("<td colspan='3' style='font-size:15px; padding:0px 5px 10px 5px;color: green; border-bottom:1px dashed #ccc;'>" + promocode + "</td>");
                                                var promoprice = qty.Promocodeamt * qty.Quantity;
                                                strHTML.Append("<td colspan='1' style='font-size:15px; color: green;padding:0px 5px 10px 5px; border-bottom:1px dashed #ccc;'>-$" + promoprice + "</td>");
                                                strHTML.Append("</tr>");
                                            }
                                            else
                                            {
                                                strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.attendeeName + "</td>");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Ticketname + "</td>");
                                                strHTML.Append("<td style='width:10%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Quantity + "</td>");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + (qty.TPD_Amount > 0 ? "$ " + decimal.Truncate((qty.Quantity * qty.TPD_Amount / qty.TPD_Purchased_Qty ?? 0) * 100) / 100 : qty.TPD_Donate > 0 ? "$ " + decimal.Truncate((qty.Quantity * qty.TPD_Donate / qty.TPD_Purchased_Qty ?? 0) * 100) / 100 : "Free") + "</td>");
                                                strHTML.Append("</tr>");
                                            }



                                        }



                                    }
                                }
                                else
                                {
                                    var addressdetail = (from p in db.Addresses where p.AddressID == item1 select p.ConsolidateAddress).FirstOrDefault();
                                    //strHTML.Append("<tr> <p style='margin:0px;padding:0px;margin-bottom:5px;font-size:13px;color:#aaaaaa;'>" + addressdetail + "</p ></tr > ");
                                    var myDatescnt = (from p in db.Ticket_Quantity_Detail where itemQuery.Contains(p.TQD_Id) select p.TQD_StartDate).ToList().Distinct();
                                    foreach (var vdate in myDatescnt)
                                    {
                                        var itemtoadd = (from p in db.Ticket_Quantity_Detail
                                                         join TP in db.Ticket_Purchased_Detail on p.TQD_Id equals TP.TPD_TQD_Id
                                                         join ta in db.TicketAttendees on TP.TPD_Id equals ta.PurchasedTicketId
                                                         join tb in db.TicketBearers on ta.TicketBearerId equals tb.TicketbearerId
                                                         join t in db.Tickets on p.TQD_Ticket_Id equals t.T_Id
                                                         where p.TQD_StartDate == vdate && TP.TPD_GUID == guid && TP.TPD_Event_Id == eventid
                                                         select new
                                                         {
                                                             eventid = TP.TPD_Event_Id,
                                                             Promocode = TP.TPD_PromoCodeID,
                                                             Promocodeamt = TP.TPD_PromoCodeAmount,
                                                             attendeeName = tb.Name,
                                                             Ticketname = t.T_name,
                                                             Quantity = ta.Quantity,
                                                             TPD_Amount = TP.TPD_Amount,
                                                             TPD_Purchased_Qty = TP.TPD_Purchased_Qty,
                                                             TPD_Donate = TP.TPD_Donate                                                            

                                                         }).ToList();

                                        //strHTML.Append("<tr> <p style='margin:0px;padding:0px;margin-bottom:5px;font-size:13px;color:#aaaaaa;'><span style='float:right;'>" + Convert.ToDateTime(vdate).ToString("MMM dd, yyyy") + " </span></p ></tr > ");
                                        strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                        strHTML.Append("<th style='font-weight:normal;padding:10px5px;border-bottom:1px dashed #ccc;'>Name</th>");
                                        strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Type </th>");
                                        strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Quantity </th>");
                                        strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Price </th>");
                                        strHTML.Append("</tr>");
                                        foreach (var qty in itemtoadd)
                                        {

                                            if (qty.Promocode != null && qty.Promocode != 0)
                                            {
                                                strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + qty.attendeeName + "</td>");
                                                strHTML.Append("<td style='width:30%;font-size:15px; padding: 10px 5px;'>" + qty.Ticketname + "</td>");
                                                strHTML.Append("<td style='width:10%font-size:15px; padding: 10px 5px;'>" + qty.Quantity + "</td>");
                                                strHTML.Append("<td style='width:30%;font-size:15px; padding: 10px 5px;'>" + (qty.TPD_Amount > 0 ? "$ " + decimal.Truncate((qty.Quantity * qty.TPD_Amount / qty.TPD_Purchased_Qty ?? 0) * 100) / 100 : qty.TPD_Donate > 0 ? "$ " + decimal.Truncate((qty.Quantity * qty.TPD_Donate / qty.TPD_Purchased_Qty ?? 0) * 100) / 100 : "Free") + "</td>");
                                                strHTML.Append("</tr>");
                                                var promocode = (from v in db.Promo_Code where v.PC_Eventid == qty.eventid && v.PC_id == qty.Promocode select v.PC_Code).FirstOrDefault();
                                                strHTML.Append("<tr align='left'>");
                                                strHTML.Append("<td colspan='3' style='font-size:15px; padding:0px 5px 10px 5px;color: green; border-bottom:1px dashed #ccc;'>" + promocode + "</td>");
                                                var promoprice = qty.Promocodeamt * qty.Quantity;
                                                strHTML.Append("<td colspan='1' style='font-size:15px; color: green;padding:0px 5px 10px 5px; border-bottom:1px dashed #ccc;'>-$" + promoprice + "</td>");
                                                strHTML.Append("</tr>");
                                            }
                                            else
                                            {
                                                strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.attendeeName + "</td>");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Ticketname + "</td>");
                                                strHTML.Append("<td style='width:10%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Quantity + "</td>");
                                                strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + (qty.TPD_Amount > 0 ? "$ " + decimal.Truncate((qty.Quantity * qty.TPD_Amount / qty.TPD_Purchased_Qty ?? 0) * 100) / 100 : qty.TPD_Donate > 0 ? "$ " + decimal.Truncate((qty.Quantity * qty.TPD_Donate / qty.TPD_Purchased_Qty ?? 0) * 100) / 100 : "Free") + "</td>");
                                                strHTML.Append("</tr>");
                                            }



                                        }



                                    }




                                }

                            }



                        }
                        else
                        {

                            foreach (var item1 in myAddress)
                            {

                                var itemtoadd = (from p in db.Ticket_Quantity_Detail
                                                 join TP in db.Ticket_Purchased_Detail on p.TQD_Id equals TP.TPD_TQD_Id
                                                 join ta in db.TicketAttendees on TP.TPD_Id equals ta.PurchasedTicketId
                                                 join tb in db.TicketBearers on ta.TicketBearerId equals tb.TicketbearerId
                                                 join t in db.Tickets on p.TQD_Ticket_Id equals t.T_Id
                                                 where p.TQD_AddressId == item1 && TP.TPD_GUID == guid && TP.TPD_Event_Id == eventid
                                                 select new
                                                 {
                                                     eventid = TP.TPD_Event_Id,
                                                     Promocode = TP.TPD_PromoCodeID,
                                                     Promocodeamt = TP.TPD_PromoCodeAmount,
                                                     attendeeName = tb.Name,
                                                     Ticketname = t.T_name,
                                                     Quantity = ta.Quantity,
                                                     TPD_Amount = TP.TPD_Amount,
                                                     TPD_Purchased_Qty = TP.TPD_Purchased_Qty,
                                                     TPD_Donate = TP.TPD_Donate

                                                 }).ToList();

                                var dateofaddress = (from p in db.Ticket_Quantity_Detail
                                                     join TP in db.Ticket_Purchased_Detail on p.TQD_Id equals TP.TPD_TQD_Id
                                                     where p.TQD_AddressId == item1 && TP.TPD_GUID == guid && TP.TPD_Event_Id == eventid
                                                     select p.TQD_StartDate).ToList().Distinct().FirstOrDefault();
                                var addressdetail = (from p in db.Addresses where p.AddressID == item1 select p.ConsolidateAddress).FirstOrDefault();

                                strHTML.Append("<tr> <p style='margin:0px;padding:0px;margin-bottom:5px;font-size:13px;color:#aaaaaa;'>" + addressdetail + "<span style='float:right;'>" + Convert.ToDateTime(dateofaddress).ToString("MMM dd, yyyy") + " </span></p ></tr > ");
                                strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                strHTML.Append("<th style='font-weight:normal;padding:10px5px;border-bottom:1px dashed #ccc;'>Name</th>");
                                strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;' > Type </th >");
                                strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Quantity </th>");
                                strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Price </ th >");
                                strHTML.Append("</tr>");
                                foreach (var qty in itemtoadd)
                                {

                                    if (qty.Promocode != null && qty.Promocode != 0)
                                    {
                                        strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + qty.attendeeName + "</td>");
                                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + qty.Ticketname + "</td>");
                                        strHTML.Append("<td style='width:10%; font-size:15px; padding: 10px 5px;'>" + qty.Quantity + "</td>");
                                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + (qty.TPD_Amount > 0 ? "$ " + decimal.Truncate((qty.Quantity * qty.TPD_Amount / qty.TPD_Purchased_Qty ?? 0) * 100) / 100 : qty.TPD_Donate > 0 ? "$ " + decimal.Truncate((qty.Quantity * qty.TPD_Donate / qty.TPD_Purchased_Qty ?? 0) * 100) / 100 : "Free") + "</td>");
                                        strHTML.Append("</tr>");
                                        var promocode = (from v in db.Promo_Code where v.PC_Eventid == qty.eventid && v.PC_id == qty.Promocode select v.PC_Code).FirstOrDefault();
                                        strHTML.Append("<tr align='left'>");
                                        strHTML.Append("<td colspan='3' style='font-size:15px; padding:0px 5px 10px 5px;color: green; border-bottom:1px dashed #ccc;'>" + promocode + "</td>");
                                        var promoprice = qty.Promocodeamt * qty.Quantity;
                                        strHTML.Append("<td colspan='1' style='font-size:15px; color: green;padding:0px 5px 10px 5px; border-bottom:1px dashed #ccc;'>-$" + promoprice + "</td>");
                                        strHTML.Append("</tr>");
                                    }
                                    else
                                    {
                                        strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.attendeeName + "</td>");
                                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Ticketname + "</td>");
                                        strHTML.Append("<td style='width:10%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Quantity + "</td>");
                                        strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + (qty.TPD_Amount > 0 ? "$ " + decimal.Truncate((qty.Quantity * qty.TPD_Amount / qty.TPD_Purchased_Qty ?? 0) * 100) / 100 : qty.TPD_Donate > 0 ? "$ " + decimal.Truncate((qty.Quantity * qty.TPD_Donate / qty.TPD_Purchased_Qty ?? 0) * 100) / 100 : "Free") + "</td>");
                                        strHTML.Append("</tr>");
                                    }
                                }

                            }


                        }

                        var variabledescid = myOrderDetails.O_VariableId;
                        if (!string.IsNullOrWhiteSpace(variabledescid))
                        {
                            if (variabledescid.Contains(','))
                            {
                                string[] words = variabledescid.Split(',');
                                foreach (string word in words)
                                {
                                    var vardesc = (from p in db.Event_VariableDesc
                                                   where p.Variable_Id.ToString() == word && p.Event_Id == eventid
                                                   select p).FirstOrDefault();
                                    strHTML.Append("<tr align='right'> ");
                                    strHTML.Append("<td colspan='4' style='font-size:15px;font-weight:bold;padding:10px 5px;border-bottom:1px solid #ccc;'>" + vardesc.VariableDesc + ":$ " + vardesc.Price + " </td>");
                                    strHTML.Append("</tr> ");
                                }
                            }
                            else
                            {
                                var vardesc = (from p in db.Event_VariableDesc
                                               where p.Variable_Id.ToString() == variabledescid && p.Event_Id == eventid
                                               select p).FirstOrDefault();

                                if (vardesc != null)
                                {
                                    strHTML.Append("<tr align='right'> ");
                                    strHTML.Append("<td colspan='4' style='font-size:15px;font-weight:bold;padding:10px 5px;border-bottom:1px solid #ccc;'>" + vardesc.VariableDesc + ":$ " + vardesc.Price + " </td>");
                                    strHTML.Append("</tr> ");
                                }
                            }
                        }
                        strHTML.Append("<tr align='right'> ");
                        strHTML.Append("<td colspan='4' style='font-size:15px;font-weight:bold;padding:10px 5px;border-bottom:1px solid #ccc;'>Total :$" + myOrderDetails.O_TotalAmount + " </td></tr>");
                        if (Edtails.Addresstatus == "Multiple")
                        {
                            eventtype = "* This event has multiple venues ";
                        }
                        else
                        {
                            eventtype = "";
                        }


                        var myBillingdeatils = (from p in db.BillingAddresses where p.Guid == guid && p.OrderId == myOrderId select p).ToList().Distinct().FirstOrDefault();

                        var cardtext = "";

                        var paymentType = db.PaymentTypes.Where(p => p.PaymentTypeId == Ticketorderdetail.PaymentTypeId).FirstOrDefault();
                        
                        if (myBillingdeatils != null)
                        {
                            EncryptDecrypt encryptor = new EncryptDecrypt();
                            string cardtype = encryptor.DecryptText(myBillingdeatils.card_type);
                            string cardId = encryptor.DecryptText(myBillingdeatils.CardId);
                            cardtext = " - " + cardtype.First().ToString().ToUpper() + cardtype.Substring(1) + " XXXX-XXXX-XXXX-" + cardId.Substring(cardId.Length - 4);
                        }
                        if (paymentType != null)
                        {
                            cardtext = "Charge to : " + paymentType.PaymentTypeName + "" + cardtext;
                        }
                        strHTML.Append("<tr align='center'> ");
                        strHTML.Append("<td colspan='4' style='font-size:15px; padding:10px 5px;'>" + cardtext + " </td></tr>");
                        strHTML.Append("<tr align='center'><td colspan='4' style='font-size:15px;'>");
                        strHTML.Append("<p style='background:#fff9cf; padding:10px 15px; display:inline-block; margin:0px;'>This charge will appear on your card statement as Eventcombo * { " + Edtails.EventTitle + "}</p>");
                        strHTML.Append("</td></tr></table > ");

                        htmlText += wc.DownloadString(htmlPath + "/email.html");

                        htmlText = htmlText.Replace("¶¶EventTitleId¶¶", Edtails.EventTitle);
                        htmlText = htmlText.Replace("¶¶EventStartDateID¶¶", Sdate);
                        htmlText = htmlText.Replace("¶¶EventVenueID¶¶", add);
                        htmlText = htmlText.Replace("¶¶EventOrderNO¶¶", item.T_Order_Id);
                        htmlText = htmlText.Replace("¶¶UserFirstNameID¶¶", fname);
                        //htmlText = htmlText.Replace("¶¶UserLastNameID¶¶", userdetail.LastName);
                        htmlText = htmlText.Replace("¶¶TicketOrderDateId¶¶", System.DateTime.Now.ToLongDateString());
                        htmlText = htmlText.Replace("¶¶EventStartTimeID¶¶", time);
                        htmlText = htmlText.Replace("¶¶EventBarcodeId¶¶", barcode);
                        htmlText = htmlText.Replace("¶¶EventQrCode¶¶", Qrcode);
                        htmlText = htmlText.Replace("¶¶EventImage¶¶", Imagevent);
                        htmlText = htmlText.Replace("¶¶EventdayId¶¶", day);
                        htmlText = htmlText.Replace("¶¶EventLogo¶¶", logoImage);
                        htmlText = htmlText.Replace("¶¶Eventtype¶¶", Eventtype);
                        htmlText = htmlText.Replace("¶¶EventDescription¶¶", "");
                        htmlText = htmlText.Replace("¶¶Eventtypedetail¶¶", Etype);
                        htmlText = htmlText.Replace("¶¶OrderDetail¶¶", strHTML.ToString());
                        htmlText = htmlText.Replace("¶¶EventOrganiserName¶¶", !string.IsNullOrEmpty(Organizerdetail.Orgnizer_Name) ? Organizerdetail.Orgnizer_Name : Edtails.Organizername);
                        htmlText = htmlText.Replace("¶¶EventOrganiserEmail¶¶", !string.IsNullOrEmpty(Organizerdetail.Organizer_Email) ? Organizerdetail.Organizer_Email : Edtails.OrganiserEmail);
                        if (count == lastcount)
                        {
                            htmlText = htmlText.Replace("¶¶Linebreak¶¶", "");
                        }
                        else
                        {
                            htmlText = htmlText.Replace("¶¶Linebreak¶¶", "<div style='page-break-before: always;width:100% text - align: center></div>");
                        }
                    }



                    var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                    var pdfBytes = htmlToPdf.GeneratePdf(htmlText);
                    mms = new MemoryStream(pdfBytes);
                }
            }
            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
            }
            return mms;

        }
        public string GetOrderNo()
        {
            string strOrderNo = "";
            if (Session["TicketLockedId"] != null)
            {
                using (EventComboEntities objECE = new EventComboEntities())
                {
                    try
                    {
                        long lMax = (from Ord in objECE.Order_Detail_T
                                     where (Ord.O_Order_Id ?? string.Empty) != string.Empty
                                     select Ord.O_Id
                                      ).Max();

                        strOrderNo = (from Ord in objECE.Order_Detail_T
                                      where Ord.O_Id == lMax
                                      select Ord.O_Order_Id).SingleOrDefault();
                    }
                    catch (Exception ex)
                    {
                      logger.Error("Exception during request processing", ex);
                    }


                }
            }


            return strOrderNo;


        }

        public List<Ticket_Locked_Detail_List> GetLockTickets()
        {
            try
            {
                string strGuid = "";
                strGuid = (Session["TicketLockedId"] != null ? Session["TicketLockedId"].ToString() : "");
                using (EventComboEntities objEntity = new EventComboEntities())
                {
                    var modelTLD = (from TLD in objEntity.Ticket_Locked_Detail
                                    where TLD.TLD_GUID == strGuid
                                    select new Ticket_Locked_Detail_List
                                    {
                                        TLD_Locked_Qty = TLD.TLD_Locked_Qty,
                                        TLD_Event_Id = TLD.TLD_Event_Id,
                                        TLD_TQD_Id = TLD.TLD_TQD_Id,
                                        Locktime = TLD.Locktime,
                                        TLD_GUID = TLD.TLD_GUID,
                                        TLD_Donate = TLD.TLD_Donate,
                                        TicketAmount = TLD.TicketAmount,
                                        TLD_PromoCodeId = TLD.TLD_PromoCodeId,
                                        TLD_PromoCodeAmount = TLD.TLD_PromoCodeAmount
                                    }
                                        );
                    return modelTLD.ToList();
                }
            }
            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
              throw;
            }


        }
        public void Nullsession()
        {
            Session["AppId"] = null;

        }


        #region LoadTickets
        public string LoadTickets(string strEvent)
        {
            string strGetSelectedTickets = "";
            string strGuid = (Session["TicketLockedId"] != null ? Session["TicketLockedId"].ToString() : "");
            if (strGuid != "")
            {
                using (var context = new EventComboEntities())
                {
                    strGetSelectedTickets = context.GetSelectedTicketListing(strGuid, (strEvent != "" ? Convert.ToInt64(strEvent) : 0)).Single();
                }
            }

            return strGetSelectedTickets;
        }


        #endregion

        public async Task<JsonResult> SaveDetailsForPaypal(string strTranId, string strPayerId, string strTokenNo)
        {
            TicketPayment objTP = (Session["TicketDatamodel"] != null ? (TicketPayment)Session["TicketDatamodel"] : null);

            JsonResult strResult = await SaveDetails(objTP, objTP.strOrderTotal, objTP.strGrandTotal, objTP.strPromId, objTP.strVarChanges, objTP.strVarId, objTP.strPaymentType, strTranId, strPayerId, strTokenNo);
            return strResult;
        }

        public ActionResult PaymentConfirmation()

        {
            if (Session["TicketLockedId"] != null)
            {
                PaymentConfirmation ps = new PaymentConfirmation();

                MyAccount ac = new MyAccount();

                EventCreation cs = new EventCreation();

                string strGUID = (Session["TicketLockedId"] != null ? Session["TicketLockedId"].ToString() : "");
                try
                {
                    Session["TicketDatamodel"] = null;
                    string body = "";
                    string to = "", from = "", cc = "", bcc = "", subjectn = "";

                    var currenturl = Url.Action("PaymentConfirmation", "TicketPayment");
                    //Session["ReturnUrl"] = "TicketPayment~" + url;
                    //var url = Url.RouteUrl("Payment");
                    Session["ReturnUrl"] = "PaymentConfirmation~" + currenturl;
                    var Emailtemplate = ac.getEmail("eticket");
                    List<paymentdate> Dateofevent = new List<paymentdate>();

                    List<Email_Tag> EmailTag = new List<Email_Tag>();
                    EventComboEntities objContent = new EventComboEntities();
                    var EvtOrDetail = (from Order in objContent.Ticket_Purchased_Detail where Order.TPD_GUID == strGUID select Order).FirstOrDefault();
                    var Orderdetail = (from order in objContent.Order_Detail_T where order.O_Order_Id == EvtOrDetail.TPD_Order_Id select order).FirstOrDefault();
                    long Eventid = (long)EvtOrDetail.TPD_Event_Id;


                    Session["Fromname"] = "events";
                    Session["logo"] = "events";
                    //Send mail
                    //var Userid = Session["AppId"].ToString();

                    var guid = Session["TicketLockedId"].ToString();

                    string strUsers = (Session["AppId"] != null ? Session["AppId"].ToString() : EvtOrDetail.TPD_User_Id);
                    if (Session["AppId"] != null)
                    {
                        string LgUser = Session["AppId"].ToString();
                        using (EventComboEntities db = new EventComboEntities())
                        {
                            AspNetUser aspuser = db.AspNetUsers.First(i => i.Id == LgUser);

                            aspuser.LastLoginTime = System.DateTime.UtcNow;
                            db.SaveChanges();


                        }
                    }
                    var acountdedtails = ac.GetLoginDetails(strUsers);


                    var email = acountdedtails.Email;
                    var username = (!String.IsNullOrEmpty(Orderdetail.O_First_Name) ? Orderdetail.O_First_Name : acountdedtails.Firstname) + " " + (!String.IsNullOrEmpty(Orderdetail.O_First_Name) ? Orderdetail.O_Last_Name : acountdedtails.Lastname);

                    string emailnames = "", emailonpayment = "";
                    string Organisername = "", Organiseremail = "";


                    // Organiserdetail
                    var OrganiserDetail = (from ev in db.Event_Orgnizer_Detail join pfd in db.Organizer_Master on ev.OrganizerMaster_Id equals pfd.Orgnizer_Id where ev.Orgnizer_Event_Id == Eventid && ev.DefaultOrg == "Y" select pfd).FirstOrDefault();


                    var Organiserdetail = db.Profiles.FirstOrDefault(i => i.UserID == OrganiserDetail.UserId);
                    if (Organiserdetail != null)
                    {
                        Organisername = !String.IsNullOrEmpty(OrganiserDetail.Orgnizer_Name) ? OrganiserDetail.Orgnizer_Name : Organiserdetail.FirstName != null ? Organiserdetail.FirstName : "";
                        Organiseremail = !String.IsNullOrEmpty(OrganiserDetail.Organizer_Email) ? OrganiserDetail.Organizer_Email : Organiserdetail.Email != null ? Organiserdetail.Email : "";
                    }

                    //

                    //type of event
                    var etype = "";
                    var typeid = db.EventVenues.Any(i => i.EventID == Eventid);
                    if (typeid)
                    {
                        etype = "Single";
                    }
                    else
                    {
                        var tyid = db.MultipleEvents.FirstOrDefault(i => i.EventID == Eventid);
                        etype = tyid.Frequency;
                    }
                    //
                    var TicketPurchasedDetail = db.Ticket_Purchased_Detail.Where(i => i.TPD_GUID == strGUID && i.TPD_Event_Id == Eventid).ToList();
                    long sumqty = 0;
                    decimal sumAmount = 0;
                    var bodyn = "";
                    var ticketP = "";
                    var eventdetail = db.Events.FirstOrDefault(i => i.EventID == Eventid);
                    DateTimeWithZone dtzstart;
                    //Get Email tags
                    EmailTag = ac.getTag();
                    DateTime datetime = new DateTime();
                    DayOfWeek day = new DayOfWeek();
                    string Sdate = "", time = "";
                    var addresstemp = db.Addresses.FirstOrDefault(i => i.EventId == eventdetail.EventID);
                    TempData["address"] = (!string.IsNullOrEmpty(addresstemp.ConsolidateAddress)) ? addresstemp.ConsolidateAddress : ""; 
                    //Get Email tags
                    foreach (var item in TicketPurchasedDetail)
                    {
                        //Detail to send on page
                        paymentdate pdate = new paymentdate();
                        var tQntydetail = db.Ticket_Quantity_Detail.FirstOrDefault(i => i.TQD_Id == item.TPD_TQD_Id);
                        var address = db.Addresses.FirstOrDefault(i => i.AddressID == tQntydetail.TQD_AddressId);
                        sumqty = sumqty + (item.TPD_Purchased_Qty??0);
                        sumAmount = (item.TPD_Amount ?? 0)+ (item.TPD_Donate ?? 0);

                        if (tQntydetail.TQD_StartDate != null)
                        {
                            datetime = DateTime.Parse(tQntydetail.TQD_StartDate);
                            day = datetime.DayOfWeek;
                            Sdate = datetime.ToString("MMM dd, yyyy");

                            time = tQntydetail.TQD_StartTime;
                        }
                        else
                        {
                            var td = DateTimeWithZone.Timezonedetail(eventdetail.EventID);
                            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td);
                            if (etype == "Single")
                            {
                                var ev = db.EventVenues.FirstOrDefault(i => i.EventID == Eventid);
                                dtzstart = new DateTimeWithZone(Convert.ToDateTime(ev.E_Startdate), userTimeZone, true);
                                datetime = dtzstart.LocalTime;
                                day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(datetime);
                                Sdate = datetime.ToString("MMM dd, yyyy");
                                time = datetime.ToString("h:mm tt").ToLower().Trim().Replace(" ", ""); ;

                            }


                        }
                        var addresslist = "";

                        if (address != null)
                        {
                            addresslist = (!string.IsNullOrEmpty(address.ConsolidateAddress)) ? address.ConsolidateAddress : "";
                        }
                        else
                        {
                            addresslist = "";
                        }
                        var timefinal = day.ToString() + "~" + Sdate.ToString() + "~" + time + "~" + addresslist;
                        pdate.id = timefinal;
                        pdate.Address = addresslist;
                        pdate.Datetime = day.ToString() + " " + Sdate.ToString() + " " + time;
                        var matchingvalues = Dateofevent.Where(x => x.id.Contains(timefinal)).FirstOrDefault();
                        if (matchingvalues == null)
                        {
                            Dateofevent.Add(pdate);
                        }


                        //Detail to send on page
                    }
                    var emailname = "";
                    //email bearer
                    var Emailbearer = (from x in db.TicketBearers where x.Guid == strGUID && (x.Email ?? "") != "" select x).Distinct().ToList();
                    var Emailbearernames = (from x in db.TicketBearers where x.Guid == strGUID && (x.Name ?? "") != "" select x).Distinct().ToList();

                    foreach (var item in Emailbearernames)
                    {
                        //MemoryStream attachment1 = new MemoryStream();
                        //attachment1= generateTicketPDF(strGUID, Eventid, EmailTag, username);
                        if (string.IsNullOrEmpty(emailnames))
                        {
                            emailnames += item.Name;
                        }
                        else
                        {
                            emailnames += "," + item.Name;
                        }

                    }
                    if (string.IsNullOrEmpty(emailnames))
                    {
                        emailonpayment = "";
                    }
                    else
                    {
                        emailonpayment = "Your invitation(s) have been sent to  " + emailnames;

                    }

                    //email bearer
                    MemoryStream attachment = generateTicketPDF(strGUID, Eventid, EmailTag, username, Server.MapPath(".."));

                    var emailorder = Orderdetail.O_Email != null ? Orderdetail.O_Email : email;
                    if (Emailtemplate != null)
                    {
                        if (!string.IsNullOrEmpty(Emailtemplate.To))
                        {


                            to = Emailtemplate.To;
                            if (to.Contains("¶¶UserEmailID¶¶"))
                            {
                                to = to.Replace("¶¶UserEmailID¶¶", emailorder);

                            }
                        }
                        if (!(string.IsNullOrEmpty(Emailtemplate.From)))
                        {
                            from = Emailtemplate.From;
                            if (from.Contains("¶¶UserEmailID¶¶"))
                            {
                                from = from.Replace("¶¶UserEmailID¶¶", emailorder);

                            }
                        }
                        else
                        {
                            from = ConfigurationManager.AppSettings.Get("DefaultEmail"); //ConfigurationManager.AppSettings.Get("UserName");

                        }
                        if (!(string.IsNullOrEmpty(Emailtemplate.CC)))
                        {
                            cc = Emailtemplate.CC;
                            if (cc.Contains("¶¶UserEmailID¶¶"))
                            {
                                cc = cc.Replace("¶¶UserEmailID¶¶", emailorder);

                            }
                        }
                        if (!(string.IsNullOrEmpty(Emailtemplate.Bcc)))
                        {
                            bcc = Emailtemplate.Bcc;
                            if (bcc.Contains("¶¶UserEmailID¶¶"))
                            {
                                bcc = bcc.Replace("¶¶UserEmailID¶¶", emailorder);

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
                            subjectn = modifysubject(subjectn, emailorder, username, eventdetail.EventTitle, DateTime.Now.ToString(), EvtOrDetail.TPD_Order_Id, EmailTag);


                        }

                        if (!string.IsNullOrEmpty(Emailtemplate.TemplateHtml))
                        {
                            bodyn = new MvcHtmlString(HttpUtility.HtmlDecode(Emailtemplate.TemplateHtml)).ToHtmlString();
                            body = ModifyEmailBody(bodyn, strGUID, Eventid, EmailTag, username);

                        }

                        // ImageMapPath = Server.MapPath("..") + "/Images/Imagemap_"+EvtOrDetail.TPD_Order_Id+ ".png";
                        //Mail 
                        ac.SendHtmlFormattedEmail(to, from, subjectn, body, cc, bcc, attachment, emailname, "", "", Emailbearer);
                        //Mail 



                    }
                    var url = Request.Url;
                    var baseurl = url.GetLeftPart(UriPartial.Authority);


                    //send email to organizer

                    Emailtemplate = ac.getEmail("email_ticket_purchase");

                    if (Emailtemplate != null)
                    {
                        if (!string.IsNullOrEmpty(Emailtemplate.To))
                        {


                            to = Emailtemplate.To;
                            if (to.Contains("¶¶EventOrganiserEmail¶¶"))
                            {
                                to = to.Replace("¶¶EventOrganiserEmail¶¶", Organiseremail);

                            }
                        }
                        if (!(string.IsNullOrEmpty(Emailtemplate.From)))
                        {
                            from = Emailtemplate.From;
                            if (from.Contains("¶¶UserEmailID¶¶"))
                            {
                                from = from.Replace("¶¶UserEmailID¶¶", emailorder);

                            }
                            if (from.Contains("¶¶EventOrganiserEmail¶¶"))
                            {
                                from = from.Replace("¶¶EventOrganiserEmail¶¶", Organiseremail);

                            }
                        }
                        else
                        {
                            from = ConfigurationManager.AppSettings.Get("DefaultEmail"); //ConfigurationManager.AppSettings.Get("UserName");

                        }
                        if (!(string.IsNullOrEmpty(Emailtemplate.CC)))
                        {
                            cc = Emailtemplate.CC;
                            if (cc.Contains("¶¶UserEmailID¶¶"))
                            {
                                cc = cc.Replace("¶¶UserEmailID¶¶", emailorder);

                            }
                        }
                        if (!(string.IsNullOrEmpty(Emailtemplate.Bcc)))
                        {
                            bcc = Emailtemplate.Bcc;
                            if (bcc.Contains("¶¶UserEmailID¶¶"))
                            {
                                bcc = bcc.Replace("¶¶UserEmailID¶¶", emailorder);

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
                                    if (EmailTag[i].Tag_Name == "TicketQty")
                                    {
                                        subjectn = subjectn.Replace("¶¶TicketQty¶¶", sumqty.ToString ());

                                    }
                                    if (EmailTag[i].Tag_Name == "TicketPrice")
                                    {
                                        subjectn = subjectn.Replace("¶¶TicketPrice¶¶", sumAmount.ToString());

                                    }
                                    if (EmailTag[i].Tag_Name == "EventTitleId")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventTitleId¶¶", eventdetail.EventTitle);

                                    }
                                    if (EmailTag[i].Tag_Name == "UserFirstNameID")
                                    {
                                        subjectn = subjectn.Replace("¶¶UserFirstNameID¶¶", username);

                                    }

                                    // All tags




                                }

                            }
                        }

                     
                        string createevent = baseurl + Url.Action("Index", "Home");
                        if (!string.IsNullOrEmpty(Emailtemplate.TemplateHtml))
                        {
                            bodyn = new MvcHtmlString(HttpUtility.HtmlDecode(Emailtemplate.TemplateHtml)).ToHtmlString();
                            for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
                            {

                                if (bodyn.Contains("¶¶" + EmailTag[i].Tag_Name.Trim() + "¶¶"))
                                {
                                    if (EmailTag[i].Tag_Name == "TicketQty")
                                    {
                                        bodyn = bodyn.Replace("¶¶TicketQty¶¶", sumqty.ToString());

                                    }
                                    if (EmailTag[i].Tag_Name == "TicketPrice")
                                    {
                                        bodyn = bodyn.Replace("¶¶TicketPrice¶¶", sumAmount.ToString());

                                    }
                                    if (EmailTag[i].Tag_Name == "EventTitleId")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventTitleId¶¶", eventdetail.EventTitle);

                                    }
                                    if (EmailTag[i].Tag_Name == "UserFirstNameID")
                                    {
                                        bodyn = bodyn.Replace("¶¶UserFirstNameID¶¶", username);

                                    }
                                    if (EmailTag[i].Tag_Name == "ClickHere")
                                    {
                                        bodyn = bodyn.Replace("¶¶ClickHere¶¶", createevent);

                                    }

                                }

                            }
                        }

                        // ImageMapPath = Server.MapPath("..") + "/Images/Imagemap_"+EvtOrDetail.TPD_Order_Id+ ".png";
                        //Mail 
                        ac.SendHtmlFormattedEmail(to, from, subjectn, bodyn, cc, bcc, "", emailname);
                        //Mail 



                    }


                    //


                    //Send mail




                    var Eventdetails = cs.GetEventdetail(Eventid);
                    ps.imgurl = (!string.IsNullOrEmpty(cs.GetImages(Eventid).FirstOrDefault()) ? cs.GetImages(Eventid).FirstOrDefault() : "/Images/default_event_image.jpg");
                    ps.Tilte = Eventdetails.EventTitle;
                    ps.description = Eventdetails.EventDescription;
                    ps.Eventid = Eventdetails.EventID.ToString();


                    ps.Organiserid = OrganiserDetail.Orgnizer_Id.ToString();

                    ps.sendlatestdetails = acountdedtails.SendLatestdetails;
                    ps.Username = username;
                    ps.Email = emailorder;
                    ps.url = baseurl+Url.RouteUrl("ViewEvent", new { strEventDs = Regex.Replace(Eventdetails.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", ""), strEventId = Eventdetails.EventID.ToString() });

                    //ps.url = baseurl + Url.Action("ViewEvent", "ViewEvent") + "?strEventDs="+ Eventid + "&strEventId";
                    ps.Guestlist = emailonpayment;
                    ps.EventPrivacy = Eventdetails.EventPrivacy;
                    ps.Shareonfb = Eventdetails.Private_ShareOnFB;
                    ps.Orderdetail = GetOrderDetailForConfirmation(strGUID);
                    ViewBag.Timecal = Dateofevent;
                    //System.GC.Collect();
                    //System.GC.WaitForPendingFinalizers();


                    //if (System.IO.File.Exists(ImageMapPath))
                    //{
                    //    Image image2 = Image.FromFile(ImageMapPath);
                    //    image2.Dispose();
                    //    System.IO.File.Delete(ImageMapPath);

                    //}

                    var ticketdet = (from t in db.TicketOrderDetails where t.T_Guid == EvtOrDetail.TPD_GUID select t).ToList();
                    if (ticketdet != null)
                    {
                        foreach (var item in ticketdet)
                        {
                            string barImgPath = Server.MapPath("..") + "/Images/br_" + item.T_Id + ".Png";

                            string qrImgPath = Server.MapPath("..") + "/Images/QR_" + item.T_Id + ".Png";
                            if (System.IO.File.Exists(barImgPath))
                            {
                                Image image2 = Image.FromFile(barImgPath);
                                image2.Dispose();
                                System.IO.File.Delete(barImgPath);

                            }
                            if (System.IO.File.Exists(qrImgPath))
                            {
                                Image image2 = Image.FromFile(qrImgPath);
                                image2.Dispose();
                                System.IO.File.Delete(qrImgPath);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                  logger.Error("Exception during request processing", ex);
                }
                Session["TicketLockedId"] = strGUID;
                return View(ps);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private string modifysubject(string subjectn, string email, string username, string eventTitle, string datenow, string Ordernum, List<Email_Tag> Emailtag)
        {
            for (int i = 0; i < Emailtag.Count; i++) // Loop with for.
            {

                if (subjectn.Contains("¶¶" + Emailtag[i].Tag_Name.Trim() + "¶¶"))
                {
                    if (Emailtag[i].Tag_Name == "UserEmailID")
                    {
                        subjectn = subjectn.Replace("¶¶UserEmailID¶¶", email);

                    }
                    if (Emailtag[i].Tag_Name == "UserFirstNameID")
                    {
                        subjectn = subjectn.Replace("¶¶UserFirstNameID¶¶", username);

                    }
                    if (Emailtag[i].Tag_Name == "EventTitleId")
                    {
                        subjectn = subjectn.Replace("¶¶EventTitleId¶¶", eventTitle);

                    }
                    if (Emailtag[i].Tag_Name == "EventOrderNO")
                    {
                        subjectn = subjectn.Replace("¶¶EventOrderNO¶¶", Ordernum);

                    }
                    if (Emailtag[i].Tag_Name == "EventStartDateID")
                    {
                        subjectn = subjectn.Replace("¶¶EventStartDateID¶¶", "");

                    }
                    if (Emailtag[i].Tag_Name == "EventVenueID")
                    {
                        subjectn = subjectn.Replace("¶¶EventVenueID¶¶", "");

                    }
                    if (Emailtag[i].Tag_Name == "Tickettype")
                    {
                        subjectn = subjectn.Replace("¶¶Tickettype¶¶", "");

                    }
                    if (Emailtag[i].Tag_Name == "TicketOrderDateId")
                    {
                        subjectn = subjectn.Replace("¶¶TicketOrderDateId¶¶", datenow);

                    }
                    if (Emailtag[i].Tag_Name == "EventImageId")
                    {
                        subjectn = subjectn.Replace("¶¶EventImageId¶¶", "");

                    }
                    if (Emailtag[i].Tag_Name == "EventStartTimeID")
                    {
                        subjectn = subjectn.Replace("¶¶EventStartTimeID¶¶", "");

                    }
                    if (Emailtag[i].Tag_Name == "TicketPrice")
                    {
                        subjectn = subjectn.Replace("¶¶TicketPrice¶¶", "");

                    }


                }

            }
            return subjectn;
        }

        private void generatemapimage(string url, string orderid)
        {
            string filename = "";
            WebClient client = new WebClient();
            filename = "Imagemap_" + orderid + ".png";
            client.DownloadFile(url, Server.MapPath("~/Images/" + filename));

        }

        private string ModifyEmailBody(string bodyn, string gUID, long Eventid, List<Email_Tag> Emailtag, string username)
        {
            EncryptDecrypt Ecode = new EncryptDecrypt();

            var Edtails = (from p in db.Events
                           join user in db.Profiles on p.UserID equals user.UserID
                           join org in db.Event_Orgnizer_Detail on p.EventID equals org.Orgnizer_Event_Id
                           join orgprof in db.Profiles on org.UserId equals orgprof.UserID
                           where p.EventID == Eventid && org.DefaultOrg == "Y"
                           select new
                           {
                               EventTitle = p.EventTitle,
                               UserName = user.FirstName,
                               Organizername = orgprof.FirstName,
                               OrganiserEmail = orgprof.Email,
                               Addresstatus = p.AddressStatus,
                           }).ToList().Distinct().FirstOrDefault();
            var Organizerdetail = (from p in db.Event_Orgnizer_Detail join k in db.Organizer_Master on p.OrganizerMaster_Id equals k.Orgnizer_Id where p.Orgnizer_Event_Id == Eventid && p.DefaultOrg == "Y" select k).FirstOrDefault();
            var url = Request.Url;
            var baseurl = url.GetLeftPart(UriPartial.Authority);
            string createevent = baseurl + Url.Action("Index", "Home");
            string discoverevents = baseurl + Url.Action("discoverevents", "Home");
            string Downloadurl = baseurl + Url.Action("pdf", "TicketPayment") + "?eventid=" + Eventid + "&guid=" + gUID;


            var itemQuery = from TqtId in db.Ticket_Purchased_Detail

                            where TqtId.TPD_GUID == gUID && TqtId.TPD_Event_Id == Eventid

                            select TqtId.TPD_TQD_Id;
            var myOrderId = (from p in db.Ticket_Purchased_Detail where p.TPD_GUID == gUID && p.TPD_Event_Id == Eventid select p.TPD_Order_Id).ToList().Distinct().FirstOrDefault();
            var myOrderDetails = (from p in db.Order_Detail_T where p.O_Order_Id == myOrderId select p).FirstOrDefault();

            var myAddress = (from p in db.Ticket_Quantity_Detail where itemQuery.Contains(p.TQD_Id) select p.TQD_AddressId).ToList().Distinct();
            var myaddress = (from p in db.Ticket_Quantity_Detail where itemQuery.Contains(p.TQD_Id) select p).ToList();
            StringBuilder strHTML = new StringBuilder();

            strHTML.Append("<table style='width: 100 %;'><tr><h1 style='font-size:22px;margin-bottom:5px;margin-top:0px;font-weight:normal;'> Order Summary </h1>");
            strHTML.Append("<p style='margin:0px;padding:0px;margin-bottom:5px;font-size:15px;'>Order#:" + myOrderId + "</p></tr>");
            var eventtype = "";
            var eventname = "";
            var startdate = "";
            var enddate = "";
            var Event = (from myEnt in db.Events where myEnt.EventID == Eventid select myEnt).FirstOrDefault();

            int timeZoneID = Int32.Parse(Event.TimeZone);
            TimeZoneDetail td = db.TimeZoneDetails.First(x => x.TimeZone_Id == timeZoneID);
            DateTimeWithZone dtzstart, dzend;
            DateTimeWithZone dtzcreated;


            var singledate = (from date in db.EventVenues where date.EventID == Eventid select date).FirstOrDefault();
            if (singledate != null)
            {
                if (td != null)
                {
                    TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td.TimeZone);
                    dtzstart = new DateTimeWithZone(Convert.ToDateTime(singledate.E_Startdate), userTimeZone, true);
                    dzend = new DateTimeWithZone(Convert.ToDateTime(singledate.E_Enddate), userTimeZone, true);
                }
                else
                {
                    TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    dtzstart = new DateTimeWithZone(Convert.ToDateTime(singledate.E_Startdate), userTimeZone, true);
                    dzend = new DateTimeWithZone(Convert.ToDateTime(singledate.E_Enddate), userTimeZone, true);
                }


            }
            else
            {
                var muldate = (from date in db.MultipleEvents where date.EventID == Eventid select date).FirstOrDefault();
                if (td != null)
                {
                    TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(td.TimeZone);
                    dtzstart = new DateTimeWithZone(Convert.ToDateTime(muldate.M_Startfrom), userTimeZone, true);
                    dzend = new DateTimeWithZone(Convert.ToDateTime(muldate.M_StartTo), userTimeZone, true);
                }
                else
                {
                    TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    dtzstart = new DateTimeWithZone(Convert.ToDateTime(muldate.M_Startfrom), userTimeZone, true);
                    dzend = new DateTimeWithZone(Convert.ToDateTime(muldate.M_StartTo), userTimeZone, true);
                }


            }
            startdate = dtzstart.LocalTime.ToLongDateString() + " " + dtzstart.LocalTime.ToLongTimeString();
            enddate = dzend.LocalTime.ToLongDateString() + " " + dzend.LocalTime.ToLongTimeString();

            if (Edtails.Addresstatus == "Online")
            {
                eventname = "Online";

            }
            else if (Edtails.Addresstatus == "PastLocation")
            {


                var Address = (from add in db.Addresses
                               where add.AddressID == (from fb in db.Events where fb.EventID == Eventid select fb.LastLocationAddress).FirstOrDefault()
                               select add).FirstOrDefault();

                eventname = Address.ConsolidateAddress;
                if (Address != null)
                {
                    eventname = Address.ConsolidateAddress;
                }
                else
                {
                    eventname = "";
                }

            }
            else
            {
                var Address = (from add in db.Addresses
                               where add.EventId == Eventid
                               select add).FirstOrDefault();
                if (Address != null)
                {
                    eventname = Address.ConsolidateAddress;
                }
                else
                {
                    eventname = "";
                }
            }
            if (myAddress.Count() == 1)
            {

                foreach (var item in myAddress)
                {
                    if (item == 0)
                    {


                        var myDatescnt = (from p in db.Ticket_Quantity_Detail where itemQuery.Contains(p.TQD_Id) select p.TQD_StartDate).ToList().Distinct();

                        foreach (var vdate in myDatescnt)
                        {
                            strHTML.Append("<tr> <p style='margin:0px;padding:0px;margin-bottom:5px;font-size:13px;color:#aaaaaa;'><span style='float:right;'>" + Convert.ToDateTime(vdate).ToString("MMM dd, yyyy") + " </span></p ></tr > ");
                            strHTML.Append("<tr align='left' style='color:#696564;'> ");
                            strHTML.Append("<th style='font-weight:normal;padding:10px5px;border-bottom:1px dashed #ccc;'>Name</th>");
                            strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Type </th>");
                            strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Quantity </th>");
                            strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Price </th>");
                            strHTML.Append("</tr>");
                            var itemtoadd = (from p in db.Ticket_Quantity_Detail
                                             join TP in db.Ticket_Purchased_Detail on p.TQD_Id equals TP.TPD_TQD_Id
                                             join l in db.Profiles on TP.TPD_User_Id equals l.UserID
                                             join t in db.Tickets on p.TQD_Ticket_Id equals t.T_Id
                                             where p.TQD_StartDate == vdate && TP.TPD_GUID == gUID && TP.TPD_Event_Id == Eventid
                                             select new
                                             {
                                                 eventid = TP.TPD_Event_Id,
                                                 Promocode = TP.TPD_PromoCodeID,
                                                 Promocodeamt = TP.TPD_PromoCodeAmount,
                                                 username = l.FirstName,
                                                 Ticketname = t.T_name,
                                                 Quantity = TP.TPD_Purchased_Qty,
                                                 Price = TP.TPD_Amount > 0 ? "$ " + TP.TPD_Amount.ToString() : TP.TPD_Donate > 0 ? "$ " + TP.TPD_Donate.ToString() : "Free"

                                             }).ToList();


                            foreach (var qty in itemtoadd)
                            {

                                if (qty.Promocode != null && qty.Promocode != 0)
                                {
                                    strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                    strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + (!string.IsNullOrEmpty(username) ? username : qty.username) + "</td>");
                                    strHTML.Append("<td style='width:30%;font-size:15px; padding: 10px 5px;'>" + qty.Ticketname + "</td>");
                                    strHTML.Append("<td style='width:10%font-size:15px; padding: 10px 5px;'>" + qty.Quantity + "</td>");
                                    strHTML.Append("<td style='width:30%;font-size:15px; padding: 10px 5px;'>" + qty.Price + "</td>");
                                    strHTML.Append("</tr>");
                                    var promocode = (from v in db.Promo_Code where v.PC_Eventid == qty.eventid && v.PC_id == qty.Promocode select v.PC_Code).FirstOrDefault();
                                    strHTML.Append("<tr align='left'>");
                                    strHTML.Append("<td colspan='3' style='font-size:15px; padding:0px 5px 10px 5px;color: green; border-bottom:1px dashed #ccc;'>" + promocode + "</td>");
                                    var promoprice = qty.Promocodeamt * qty.Quantity;
                                    strHTML.Append("<td colspan='1' style='font-size:15px; color: green;padding:0px 5px 10px 5px; border-bottom:1px dashed #ccc;'>-$" + promoprice + "</td>");
                                    strHTML.Append("</tr>");
                                }
                                else
                                {
                                    strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                    strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + (!string.IsNullOrEmpty(username) ? username : qty.username) + "</td>");
                                    strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Ticketname + "</td>");
                                    strHTML.Append("<td style='width:10%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Quantity + "</td>");
                                    strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Price + "</td>");
                                    strHTML.Append("</tr>");
                                }



                            }



                        }
                    }
                    else
                    {
                        var addressdetail = (from p in db.Addresses where p.AddressID == item select p.ConsolidateAddress).FirstOrDefault();
                        strHTML.Append("<tr> <p style='margin:0px;padding:0px;margin-bottom:5px;font-size:13px;color:#aaaaaa;'>" + addressdetail + "</p ></tr > ");
                        var myDatescnt = (from p in db.Ticket_Quantity_Detail where itemQuery.Contains(p.TQD_Id) select p.TQD_StartDate).ToList().Distinct();
                        foreach (var vdate in myDatescnt)
                        {
                            var itemtoadd = (from p in db.Ticket_Quantity_Detail
                                             join TP in db.Ticket_Purchased_Detail on p.TQD_Id equals TP.TPD_TQD_Id
                                             join l in db.Profiles on TP.TPD_User_Id equals l.UserID
                                             join t in db.Tickets on p.TQD_Ticket_Id equals t.T_Id
                                             where p.TQD_StartDate == vdate && TP.TPD_GUID == gUID && TP.TPD_Event_Id == Eventid
                                             select new
                                             {
                                                 eventid = TP.TPD_Event_Id,
                                                 Promocode = TP.TPD_PromoCodeID,
                                                 Promocodeamt = TP.TPD_PromoCodeAmount,
                                                 username = l.FirstName,
                                                 Ticketname = t.T_name,
                                                 Quantity = TP.TPD_Purchased_Qty,
                                                 Price = TP.TPD_Amount > 0 ? "$ " + TP.TPD_Amount.ToString() : TP.TPD_Donate > 0 ? "$ " + TP.TPD_Donate.ToString() : "Free"

                                             }).ToList();

                            strHTML.Append("<tr> <p style='margin:0px;padding:0px;margin-bottom:5px;font-size:13px;color:#aaaaaa;'><span style='float:right;'>" + Convert.ToDateTime(vdate).ToString("MMM dd, yyyy") + " </span></p ></tr > ");
                            strHTML.Append("<tr align='left' style='color:#696564;'> ");
                            strHTML.Append("<th style='font-weight:normal;padding:10px5px;border-bottom:1px dashed #ccc;'>Name</th>");
                            strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Type </th>");
                            strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Quantity </th>");
                            strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Price </th>");
                            strHTML.Append("</tr>");
                            foreach (var qty in itemtoadd)
                            {

                                if (qty.Promocode != null && qty.Promocode != 0)
                                {
                                    strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                    strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + (!string.IsNullOrEmpty(username) ? username : qty.username) + "</td>");
                                    strHTML.Append("<td style='width:30%;font-size:15px; padding: 10px 5px;'>" + qty.Ticketname + "</td>");
                                    strHTML.Append("<td style='width:10%font-size:15px; padding: 10px 5px;'>" + qty.Quantity + "</td>");
                                    strHTML.Append("<td style='width:30%;font-size:15px; padding: 10px 5px;'>" + qty.Price + "</td>");
                                    strHTML.Append("</tr>");
                                    var promocode = (from v in db.Promo_Code where v.PC_Eventid == qty.eventid && v.PC_id == qty.Promocode select v.PC_Code).FirstOrDefault();
                                    strHTML.Append("<tr align='left'>");
                                    strHTML.Append("<td colspan='3' style='font-size:15px; padding:0px 5px 10px 5px;color: green; border-bottom:1px dashed #ccc;'>" + promocode + "</td>");
                                    var promoprice = qty.Promocodeamt * qty.Quantity;
                                    strHTML.Append("<td colspan='1' style='font-size:15px; color: green;padding:0px 5px 10px 5px; border-bottom:1px dashed #ccc;'>-$" + promoprice + "</td>");
                                    strHTML.Append("</tr>");
                                }
                                else
                                {
                                    strHTML.Append("<tr align='left' style='color:#696564;'> ");
                                    strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + (!string.IsNullOrEmpty(username) ? username : qty.username) + "</td>");
                                    strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Ticketname + "</td>");
                                    strHTML.Append("<td style='width:10%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Quantity + "</td>");
                                    strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Price + "</td>");
                                    strHTML.Append("</tr>");
                                }



                            }



                        }




                    }

                }



            }
            else
            {

                foreach (var item in myAddress)
                {

                    var itemtoadd = (from p in db.Ticket_Quantity_Detail
                                     join TP in db.Ticket_Purchased_Detail on p.TQD_Id equals TP.TPD_TQD_Id
                                     join l in db.Profiles on TP.TPD_User_Id equals l.UserID
                                     join t in db.Tickets on p.TQD_Ticket_Id equals t.T_Id
                                     where p.TQD_AddressId == item && TP.TPD_GUID == gUID && TP.TPD_Event_Id == Eventid && p.TQD_AddressId == item
                                     select new
                                     {
                                         eventid = TP.TPD_Event_Id,
                                         Promocode = TP.TPD_PromoCodeID,
                                         Promocodeamt = TP.TPD_PromoCodeAmount,
                                         username = l.FirstName,
                                         Ticketname = t.T_name,
                                         Quantity = TP.TPD_Purchased_Qty,
                                         Price = TP.TPD_Amount > 0 ? "$ " + TP.TPD_Amount.ToString() : TP.TPD_Donate > 0 ? "$ " + TP.TPD_Donate.ToString() : "Free"

                                     }).ToList();

                    var dateofaddress = (from p in db.Ticket_Quantity_Detail
                                         join TP in db.Ticket_Purchased_Detail on p.TQD_Id equals TP.TPD_TQD_Id
                                         where p.TQD_AddressId == item && TP.TPD_GUID == gUID && TP.TPD_Event_Id == Eventid
                                         select p.TQD_StartDate).ToList().Distinct().FirstOrDefault();
                    var addressdetail = (from p in db.Addresses where p.AddressID == item select p.ConsolidateAddress).FirstOrDefault();

                    strHTML.Append("<tr> <p style='margin:0px;padding:0px;margin-bottom:5px;font-size:13px;color:#aaaaaa;'>" + addressdetail + "<span style='float:right;'>" + Convert.ToDateTime(dateofaddress).ToString("MMM dd, yyyy") + " </span></p ></tr > ");
                    strHTML.Append("<tr align='left' style='color:#696564;'> ");
                    strHTML.Append("<th style='font-weight:normal;padding:10px5px;border-bottom:1px dashed #ccc;'>Name</th>");
                    strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;' > Type </th >");
                    strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Quantity </th>");
                    strHTML.Append("<th style='font-weight:normal; padding:10px 5px; border-bottom:1px dashed #ccc;'> Price </ th >");
                    strHTML.Append("</tr>");
                    foreach (var qty in itemtoadd)
                    {

                        if (qty.Promocode != null && qty.Promocode != 0)
                        {
                            strHTML.Append("<tr align='left' style='color:#696564;'> ");
                            strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + (!string.IsNullOrEmpty(username) ? username : qty.username) + "</td>");
                            strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + qty.Ticketname + "</td>");
                            strHTML.Append("<td style='width:10%; font-size:15px; padding: 10px 5px;'>" + qty.Quantity + "</td>");
                            strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px;'>" + qty.Price + "</td>");
                            strHTML.Append("</tr>");
                            var promocode = (from v in db.Promo_Code where v.PC_Eventid == qty.eventid && v.PC_id == qty.Promocode select v.PC_Code).FirstOrDefault();
                            strHTML.Append("<tr align='left'>");
                            strHTML.Append("<td colspan='3' style='font-size:15px; padding:0px 5px 10px 5px;color: green; border-bottom:1px dashed #ccc;'>" + promocode + "</td>");
                            var promoprice = qty.Promocodeamt * qty.Quantity;
                            strHTML.Append("<td colspan='1' style='font-size:15px; color: green;padding:0px 5px 10px 5px; border-bottom:1px dashed #ccc;'>-$" + promoprice + "</td>");
                            strHTML.Append("</tr>");
                        }
                        else
                        {
                            strHTML.Append("<tr align='left' style='color:#696564;'> ");
                            strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + (!string.IsNullOrEmpty(username) ? username : qty.username) + "</td>");
                            strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Ticketname + "</td>");
                            strHTML.Append("<td style='width:10%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Quantity + "</td>");
                            strHTML.Append("<td style='width:30%; font-size:15px; padding: 10px 5px; border-bottom:1px dashed #ccc;'>" + qty.Price + "</td>");
                            strHTML.Append("</tr>");
                        }
                    }

                }


            }

            var variabledescid = myOrderDetails.O_VariableId;
            if (!string.IsNullOrWhiteSpace(variabledescid))
            {
                if (variabledescid.Contains(','))
                {
                    string[] words = variabledescid.Split(',');
                    foreach (string word in words)
                    {
                        var vardesc = (from p in db.Event_VariableDesc
                                       where p.Variable_Id.ToString() == word && p.Event_Id == Eventid
                                       select p).FirstOrDefault();
                        strHTML.Append("<tr align='right'> ");
                        strHTML.Append("<td colspan='4' style='font-size:15px;font-weight:bold;padding:10px 5px;border-bottom:1px solid #ccc;'>" + vardesc.VariableDesc + ":$ " + vardesc.Price + " </td>");
                        strHTML.Append("</tr> ");
                    }
                }
                else
                {
                    var vardesc = (from p in db.Event_VariableDesc
                                   where p.Variable_Id.ToString() == variabledescid && p.Event_Id == Eventid
                                   select p).FirstOrDefault();

                    if (vardesc != null)
                    {
                        strHTML.Append("<tr align='right'> ");
                        strHTML.Append("<td colspan='4' style='font-size:15px;font-weight:bold;padding:10px 5px;border-bottom:1px solid #ccc;'>" + vardesc.VariableDesc + ":$ " + vardesc.Price + " </td>");
                        strHTML.Append("</tr> ");
                    }
                }
            }
            strHTML.Append("<tr align='right'> ");
            strHTML.Append("<td colspan='4' style='font-size:15px;font-weight:bold;padding:10px 5px;border-bottom:1px solid #ccc;'>Total :$" + myOrderDetails.O_TotalAmount + " </td></tr>");
            if (Edtails.Addresstatus == "Multiple")
            {
                eventtype = "* This event has multiple venues ";
            }
            else
            {
                eventtype = "";
            }


            var myBillingdeatils = (from p in db.BillingAddresses where p.Guid == gUID && p.OrderId == myOrderId select p).ToList().Distinct().FirstOrDefault();

            var cardtext = "";


            if (myBillingdeatils != null)
            {
                var Scardnumber = Ecode.DecryptText(myBillingdeatils.CardId).Trim();
                var Icardlength = Scardnumber.Length;
                var WrVS = "";
                int k = 1;
                for (int i = 0; i < Icardlength; i++)
                {

                    WrVS += "X";
                    if (k == 4)
                    {
                        WrVS += "-";
                        k = 0;
                    }


                    k++;

                }
                if (WrVS.EndsWith("-"))
                {
                    WrVS = WrVS.Substring(0, WrVS.LastIndexOf("-"));
                }
                var rvrs = WrVS.Substring(0, WrVS.LastIndexOf("-") + 1);
                var rvrsd = rvrs.Replace("-", "");
                var chrlength = Icardlength - rvrsd.Length;
                var result = Scardnumber.Substring(Icardlength - Math.Min(chrlength, Icardlength));
                var finalstr = rvrs + result;

                var touper = "";

                if (!string.IsNullOrWhiteSpace(myBillingdeatils.card_type))
                {
                    var cardtype = Ecode.DecryptText(myBillingdeatils.card_type);
                    touper = char.ToUpper(cardtype[0]) + cardtype.Substring(1);
                }
                else
                {
                    touper = "";
                }

                cardtext = "Charge to :" + touper + "  " + finalstr;
            }
            else
            {
                cardtext = "Charge to : Paypal";
            }
            if (myOrderDetails.O_TotalAmount <= 0)
            {
                cardtext = "";
            }
            strHTML.Append("<tr align='center'> ");
            strHTML.Append("<td colspan='4' style='font-size:15px; padding:10px 5px;'>" + cardtext + " </td></tr>");
            strHTML.Append("<tr align='center'><td colspan='4' style='font-size:15px;'>");
            strHTML.Append("<p style='background:#fff9cf; padding:10px 15px; display:inline-block; margin:0px;'>This charge will appear on your card statement as Eventcombo * { " + Edtails.EventTitle + "}</p>");
            strHTML.Append("<p style='color:#696564;' >This order is subject to Eventcombo '");
            strHTML.Append("<a href='#' style='color:#0f90ba;'>Terms of Service </a> , <a style='color:#0f90ba;' href='#'>Privacy Policy </a> and <a style='color:#0f90ba;' href='#'>Cookie Policy </a></p>");
            strHTML.Append("</td></tr></table > ");

            //var imagepath = "<img src=https://maps.googleapis.com/maps/api/staticmap?center='"+eventname+"'&zoom=13&size=400x400&maptype=roadmap&markers=color:red%7Clabel:C%7C'"+ eventname + "' style='width:100%' />";

            //generatemapimage("http://maps.google.com/maps/api/staticmap?center="+ eventname + "&zoom=14&size=400x400&maptype=roadmap&markers=color:red|color:red|label:C|"+ eventname + "&sensor=false", myOrderId);
            var url1 = Request.Url;
            var baseurl1 = url.GetLeftPart(UriPartial.Authority);
            //string ImageMapPath = "http://eventcombonew-qa.kiwireader.com/Images/Imagemap_"+myOrderId+".png";
            string Imagecode = "<img style = 'width:200px;height:200px;' src ='http://maps.google.com/maps/api/staticmap?center=" + eventname + "&zoom=14&size=400x400&maptype=roadmap&markers=color:red|color:red|label:C|" + eventname + "&sensor=false' alt = 'Map Image' />";
            //string Imageeventimg = "<img style='width:200px;height:200px;' src = cid:myeventmapImageID alt = 'Image' >";
            if (!string.IsNullOrEmpty(bodyn))
            {




                for (int i = 0; i < Emailtag.Count; i++) // Loop with for.
                {

                    if (bodyn.Contains("¶¶" + Emailtag[i].Tag_Name.Trim() + "¶¶"))
                    {

                        if (Emailtag[i].Tag_Name == "UserFirstNameID")
                        {
                            bodyn = bodyn.Replace("¶¶UserFirstNameID¶¶", username);

                        }
                        if (Emailtag[i].Tag_Name == "EventTitleId")
                        {
                            bodyn = bodyn.Replace("¶¶EventTitleId¶¶", Edtails.EventTitle);

                        }

                        if (Emailtag[i].Tag_Name == "EventOrganiserName")
                        {
                            bodyn = bodyn.Replace("¶¶EventOrganiserName¶¶", !string.IsNullOrEmpty(Organizerdetail.Orgnizer_Name) ? Organizerdetail.Orgnizer_Name : Edtails.Organizername);

                        }
                        if (Emailtag[i].Tag_Name == "EventOrganiserEmail")
                        {
                            bodyn = bodyn.Replace("¶¶EventOrganiserEmail¶¶", !string.IsNullOrEmpty(Organizerdetail.Organizer_Email) ? Organizerdetail.Organizer_Email : Edtails.OrganiserEmail);

                        }
                        if (Emailtag[i].Tag_Name == "EventDynamicTable")
                        {
                            bodyn = bodyn.Replace("¶¶EventDynamicTable¶¶", strHTML.ToString());

                        }
                        if (Emailtag[i].Tag_Name == "CreateEventurl")
                        {
                            bodyn = bodyn.Replace("¶¶CreateEventurl¶¶", createevent);
                        }
                        if (Emailtag[i].Tag_Name == "DiscoverEventurl")
                        {
                            bodyn = bodyn.Replace("¶¶DiscoverEventurl¶¶", discoverevents);
                        }
                        if (Emailtag[i].Tag_Name == "EventLogin")
                        {
                            bodyn = bodyn.Replace("¶¶EventLogin¶¶", createevent);
                        }
                        if (Emailtag[i].Tag_Name == "Eventtype")
                        {
                            bodyn = bodyn.Replace("¶¶Eventtype¶¶", eventtype);
                        }
                        if (Emailtag[i].Tag_Name == "EventStartDateID")
                        {
                            bodyn = bodyn.Replace("¶¶EventStartDateID¶¶", startdate);
                        }
                        if (Emailtag[i].Tag_Name == "EventEndDateID")
                        {
                            bodyn = bodyn.Replace("¶¶EventEndDateID¶¶", enddate);
                        }
                        if (Emailtag[i].Tag_Name == "EventVenueID")
                        {
                            bodyn = bodyn.Replace("¶¶EventVenueID¶¶", eventname);
                        }
                        if (Emailtag[i].Tag_Name == "EventMapImage")
                        {
                            bodyn = bodyn.Replace("¶¶EventMapImage¶¶", Imagecode);
                        }
                        if (Emailtag[i].Tag_Name == "Downloadurl")
                        {
                            bodyn = bodyn.Replace("¶¶Downloadurl¶¶", Downloadurl);
                        }

                    }

                }
            }




            return bodyn;
        }

        public string GetOrderDetailForConfirmation(string guid)
        {
            string strResult = "";
            try
            {
                string strGuid = (guid != null ? guid : "");
                if (strGuid != "")
                {
                    using (var objEnt = new EventComboEntities())
                    {
                        var TicketCount = (from TPD in objEnt.Ticket_Purchased_Detail
                                           where TPD.TPD_GUID == strGuid
                                           group TPD by new { TPD.TPD_GUID } into TPDgrp
                                           select new
                                           {
                                               totalOrder = TPDgrp.Sum(s => s.TPD_Purchased_Qty)
                                           }
                                           ).FirstOrDefault();

                        var PurchaseDetail = (from TPD in objEnt.Ticket_Purchased_Detail
                                              where TPD.TPD_GUID == strGuid
                                              select TPD
                                          ).ToList();

                        long? iPaidCount = 0; long? iFreeCount = 0;
                        foreach (Ticket_Purchased_Detail TPD in PurchaseDetail)
                        {
                            var vTId = (from TQD in objEnt.Ticket_Quantity_Detail
                                        where TQD.TQD_Id == TPD.TPD_TQD_Id
                                        select TQD.TQD_Ticket_Id).FirstOrDefault();
                            var vTType = (from Tkt in objEnt.Tickets
                                          where Tkt.T_Id == vTId
                                          select Tkt.TicketTypeID).FirstOrDefault();

                            if (vTType == 2) iPaidCount = iPaidCount + TPD.TPD_Purchased_Qty;
                            if (vTType == 1) iFreeCount = iFreeCount + TPD.TPD_Purchased_Qty;
                        }


                        var OrderNo = (from TPD in objEnt.Ticket_Purchased_Detail
                                       where TPD.TPD_GUID == strGuid
                                       select TPD.TPD_Order_Id
                                           ).FirstOrDefault();

                        var OrderAmt = (from OD in objEnt.Order_Detail_T
                                        where OD.O_Order_Id == OrderNo
                                        select OD.O_TotalAmount
                                        ).FirstOrDefault();
                        var Promocode = (from p in objEnt.Order_Detail_T join k in objEnt.Promo_Code on p.O_PromoCodeId equals k.PC_id where p.O_Order_Id == OrderNo select k.PC_Code).FirstOrDefault();

                        if (iPaidCount == 1)
                        {
                            strResult = "Order " + OrderNo.ToString() + " , " + TicketCount.totalOrder.ToString() + " ticket for $" + OrderAmt.ToString();
                        }
                        else if (iPaidCount > 1)
                        {
                            strResult = "Order " + OrderNo.ToString() + " , " + TicketCount.totalOrder.ToString() + " tickets for $" + OrderAmt.ToString();
                        }
                        else if (iFreeCount == 1)
                        {
                            strResult = "Order " + OrderNo.ToString() + " , " + TicketCount.totalOrder.ToString() + " ticket";
                        }
                        else if (iFreeCount > 1)
                        {
                            strResult = "Order " + OrderNo.ToString() + " , " + TicketCount.totalOrder.ToString() + " tickets";
                        }
                        else
                        {
                            OrderAmt = (OrderAmt != null ? OrderAmt : 0);
                            strResult = "Order " + OrderNo.ToString() + " , " + TicketCount.totalOrder.ToString() + " ticket for $" + OrderAmt.ToString();
                        }
                        if (!string.IsNullOrWhiteSpace(Promocode))
                        {
                            strResult = strResult + "." + Environment.NewLine + "You have applied " + Promocode + " as promo code for this transaction";
                        }

                        //strResult = OrderNo + "~" + TicketCount.totalOrder + "~" + OrderAmt;
                    }
                }
            }
            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
            }
            return strResult;

        }


        public void getlatestsetting(string get)
        {
            try
            {
                string strUsers = (Session["AppId"] != null ? Session["AppId"].ToString() : "");
                using (EventComboEntities db = new EventComboEntities())
                {
                    Profile prof = db.Profiles.Where(x => x.UserID == strUsers).FirstOrDefault();
                    if (prof != null)
                    {
                        prof.SendCur_EventDetail = get;
                        db.SaveChanges();
                    }

                }
            }
            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
            }


        }


        public string GetURL()
        {

            string URL = "";

            try
            {

                string Port = Request.ServerVariables["SERVER_PORT"];

                if ((Port == null) | Port == "80" | Port == "443")
                {
                    Port = "";
                }

                else
                {
                    Port = ":" + Port;
                }

                string Protocol = Request.ServerVariables["SERVER_PORT_SECURE"];

                if ((Protocol == null) | Protocol == "0")
                {
                    Protocol = "http://";
                }

                else
                {
                    Protocol = "https://";
                }
                URL = Protocol + Request.ServerVariables["SERVER_NAME"] + Port + "";
            }

            catch (Exception ex)
            {
              logger.Error("Exception during request processing", ex);
            }

            return URL;
        }

        public PartialViewResult LoadTicket(long id)
        {
            ticketbox t = new ticketbox();
            t.ticketinfo = LoadTickets(id.ToString());
            return PartialView("LoadTicketsView", t);
        }

    }






}
