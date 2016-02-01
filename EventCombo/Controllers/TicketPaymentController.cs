﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
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
namespace EventCombo.Controllers
{

    //[RoutePrefix("Payment")]
    public class TicketPaymentController : Controller
    {
        EventComboEntities db = new EventComboEntities();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        // GET: TicketPayment

        //[Route("Payment", Name = "TPayment", Order=2)]
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        //[Route("",Name ="Payment"),HttpGet]
        public ActionResult TicketPayment()
        {
            if (Session["token"] != null && Session["PayerID"] != null)
            {
                string strtoken = Session["token"].ToString();
                string strPayerID = Session["PayerID"].ToString();


            }


            ValidationMessageController vmc = new ValidationMessageController();
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
            long Eventid = 0;
            if (Session["TicketLockedId"] != null)
            {
                Event objMyEvent = new Event();
                objMyEvent = vmc.GetSelectedEventDetail(Session["TicketLockedId"].ToString());
                Eventid = objMyEvent.EventID;

            }

            Eventid = vmc.GetLatestEventId(Eventid);

            TicketPayment tp = new TicketPayment();
            string defaultCountry = "";
            string Fname = "", Lname = "", Phnnumber = "", Adress = "", Email = "", City = "", State = "", Country = "", Zip = "";

            var url = Url.Action("TicketPayment", "TicketPayment");
            //Session["ReturnUrl"] = "TicketPayment~" + url;
            //var url = Url.RouteUrl("Payment");
            Session["ReturnUrl"] = "TicketPayment~" + url;

            CreateEventController cs = new CreateEventController();
            AccountController AccDetail = new AccountController();
            tp.Imageurl = cs.GetImages(Eventid).FirstOrDefault();
            var eventdetails = cs.GetEventdetail(Eventid);
            tp.Ticketdeliveraddress = eventdetails.Ticket_DAdress;
            tp.Title = eventdetails.EventTitle;
            tp.URLTitle = Regex.Replace(eventdetails.EventTitle.Replace(" ", "-"), "[^a-zA-Z0-9_-]+", "");
            tp.Tickettype = "Paid";
            ViewData["Type"] = tp.Tickettype;
            List<Cardview> Detailscard = new List<Cardview>();
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

            using (EventComboEntities db = new EventComboEntities())
            {
                var countryQuery = (from c in db.Countries
                                    orderby c.Country1 ascending
                                    select c).Distinct();
                List<SelectListItem> countryList = new List<SelectListItem>();
                if (!string.IsNullOrEmpty(Country))
                {
                    defaultCountry = Country;
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
                ViewBag.CountryID = countryList;
                Cardview card = new Cardview();
                card.value = "-1";
                card.text = "Select a Card";
                Detailscard.Add(card);
                if (Session["AppId"] != null)
                {
                    var userid = Session["AppId"].ToString();
                    var carddetails = db.CardDetails.Where(x => x.UserId == userid).ToList();

                    if (carddetails != null)
                    {
                        foreach (var item in carddetails)
                        {
                            Cardview card1 = new Cardview();
                            var Scardnumber = item.CardNumber.Trim();
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
                            card1.value = item.CardId.ToString();
                            var touper = "";
                            if (!string.IsNullOrWhiteSpace(item.card_type))
                            {
                                touper = char.ToUpper(item.card_type[0]) + item.card_type.Substring(1);
                            }
                            else
                            {
                                touper = "";
                            }
                            card1.text = "Payment Method:Customer-" + Fname + "  " + touper + "  " + finalstr;
                            Detailscard.Add(card1);


                        }

                    }

                }
                Cardview card2 = new Cardview();
                card2.value = "A";
                card2.text = "Add a new Card";
                Detailscard.Add(card2);

                Cardview card3 = new Cardview();
                card3.value = "P";
                card3.text = "Use Paypal";
                Detailscard.Add(card3);
                ViewBag.Detailscard = Detailscard;
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
            catch (Exception)
            {
                strResult = "There is some Problem.";
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


        public void setsession(string id, long Eventid)
        {

            Session["ReturnUrl"] = Url.Action("TicketPayment", "TicketPayment", new { Eventid = Eventid });

        }

        public string returncardetail(string cardid)
        {

            var Carddetails = (from ev in db.CardDetails where ev.CardId.ToString() == cardid select ev).FirstOrDefault();

            return Carddetails.CardNumber + "*" + Carddetails.ExpirationDate + "*" + Carddetails.Cvv;

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
            catch (Exception)
            {
                strResult = "N";
            }
            return strResult;
        }

        public async Task<string> SaveDetails(TicketPayment model, string strOrderTotal, string strGrandTotal, string strPromId, string strVarChanges, string strVarId, string strPaymentType)
        {
            string ApiLoginID; string ApiTransactionKey; string strCardNo; string strExpDate; string strCvvCode; decimal dAmount;
            ApiLoginID = ""; ApiTransactionKey = ""; strCardNo = ""; strExpDate = ""; strCvvCode = ""; dAmount = 0;



            HomeController hm = new HomeController();
            hm.ControllerContext = new ControllerContext(this.Request.RequestContext, hm);
            if (!string.IsNullOrEmpty(model.AccconfirmEmail) && !string.IsNullOrEmpty(model.Accpassword))
            {
                string userid = await saveuser(model.AccEmail, model.Accpassword);
                if (!string.IsNullOrEmpty(userid))
                {
                    using (EventComboEntities objEntity = new EventComboEntities())
                    {

                        Profile prof = new Profile();
                        prof.FirstName = model.AccFname;
                        prof.Email = model.AccEmail;
                        prof.LastName = model.AccLname;
                        prof.MainPhone = model.Accountphnno;
                        prof.City = model.AccCity;
                        prof.State = model.AccState;
                        prof.Zip = model.Acczip;
                        prof.CountryID = byte.Parse(model.Acccountry);
                        prof.UserID = userid;

                        objEntity.Profiles.Add(prof);
                        objEntity.SaveChanges();
                    }
                    Session["AppId"] = userid;
                }
                else
                {
                    Session["AppId"] = null;
                }


            }
            if (Session["AppId"] != null)
            {
                string Userid = Session["AppId"].ToString();
                string guid = Session["TicketLockedId"].ToString();
                using (EventComboEntities objEntity = new EventComboEntities())
                {
                    Profile prof = objEntity.Profiles.First(i => i.UserID == Userid);
                    prof.FirstName = model.AccFname;
                    if (!string.IsNullOrEmpty(model.AccLname))
                    {
                        prof.LastName = model.AccLname;
                    }
                    if (!string.IsNullOrEmpty(model.Accountphnno))
                    {
                        prof.MainPhone = model.Accountphnno;
                    }
                    if (!string.IsNullOrEmpty(model.AccCity))
                    {
                        prof.City = model.AccCity;
                    }
                    if (!string.IsNullOrEmpty(model.AccState))
                    {
                        prof.State = model.AccState;
                    }
                    if (!string.IsNullOrEmpty(model.Acczip))
                    {
                        prof.Zip = model.Acczip;
                    }

                    prof.CountryID = byte.Parse(model.Acccountry);





                    Order_Detail_T objOdr = new Order_Detail_T();
                    objOdr.O_Order_Id = "";
                    objOdr.O_TotalAmount = CommanClasses.ConvertToNumeric(strGrandTotal); ;
                    objOdr.O_User_Id = Userid;
                    objOdr.O_OrderAmount = CommanClasses.ConvertToNumeric(strOrderTotal);
                    objOdr.O_VariableId = CommanClasses.ConvertToLong(strVarId);
                    objOdr.O_VariableAmount = CommanClasses.ConvertToNumeric(strVarChanges);
                    objOdr.O_PromoCodeId = CommanClasses.ConvertToLong(strPromId);
                    objOdr.O_OrderDateTime = DateTime.Now;

                    objEntity.Order_Detail_T.Add(objOdr);
                    objEntity.SaveChanges();
                    string strOrderNo = GetOrderNo();

                    //List<Ticket_Locked_Detail> objLockedTic = new List<Ticket_Locked_Detail>();
                    List<Ticket_Locked_Detail_List> objLockedTic = new List<Ticket_Locked_Detail_List>();
                    objLockedTic = GetLockTickets();
                    Ticket_Purchased_Detail objTPD;

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
                        objTPD.TPD_EC_Fee = GetCurrentECFee(TLD.TLD_TQD_Id);
                        objEntity.Ticket_Purchased_Detail.Add(objTPD);
                    }


                    if (model.Ticketname == "Paid")
                    {

                        if (model.Savecarddetail != "N")
                        {
                            var objcarddetail = (from objdetail in objEntity.CardDetails where objdetail.UserId == Userid && objdetail.CardNumber == model.cardno select objdetail).Any();
                            if (!objcarddetail)
                            {
                                CardDetail card = new CardDetail();
                                card.OrderId = strOrderNo;
                                card.CardNumber = model.cardno;
                                card.ExpirationDate = model.expirydate;
                                card.Cvv = model.cvv;
                                card.UserId = Userid;
                                card.Guid = guid;
                                card.card_type = model.card_type;
                                objEntity.CardDetails.Add(card);
                            }


                        }


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
                        objEntity.BillingAddresses.Add(badd);
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
                    objEntity.SaveChanges();
                    if (strPaymentType == "A")
                    {
                        ApiLoginID = "354v9ZufxM6";
                        ApiTransactionKey = "68Et2R3KcV62rJ27";
                        strCardNo = model.cardno;
                        strExpDate = model.expirydate;
                        strCvvCode = model.cvv;
                        dAmount = (strGrandTotal != "" ? Convert.ToDecimal(strGrandTotal) : 0);
                        //  PaymentProcess.CheckCreditCard(ApiLoginID, ApiTransactionKey, strCardNo, strExpDate, strCvvCode, dAmount);
                    }
                    //else if (strPaymentType == "P")
                    //{
                    //    RedirectToAction("Pay", "Cart");
                    //}






                    Session["AppId"] = Userid;
                    Session["TicketLockedId"] = guid;

                    return strOrderNo;


                }
            }
            else
            {

                return "Some Error Comes.";

            }
        }

        public decimal GetCurrentECFee(long? lTQDId)
        {
            decimal dResult = 0;
            try
            {
                using (EventComboEntities objECE = new EventComboEntities())
                {
                    var vTicketId = (from myRow in objECE.Ticket_Quantity_Detail
                                     where myRow.TQD_Id == lTQDId
                                     select myRow.TQD_Ticket_Id).FirstOrDefault();

                    var vECFee = (from myRow in objECE.Tickets
                                  where myRow.T_Id == vTicketId
                                  select myRow.EC_Fee).FirstOrDefault();

                    dResult = (vECFee != null ? Convert.ToDecimal(vECFee) : 0);
                }
            }
            catch (Exception)
            {
                dResult = 0;
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

        private string createxml(string Orderno, string ticketname, string tqty, string tprice, string fee, string discount, string tickettype, string customername, string eventname, string eventdate, string eventime, string venue, string organisername, string organiseremail)
        {
            XElement Ticketinfo = new XElement("Ticketinfo",
  new XElement("UniqueOrderNumber", Orderno),
  new XElement("TicketTypeName", ticketname),
  new XElement("TotalTicketQuantityPerOrder", tqty),
 new XElement("TicketPrice", tprice),
      new XElement("TicketFeeAmount", fee),
        new XElement("TicketDiscountAmount", discount),
        new XElement("TicketType", tickettype),
              new XElement("CustomerName", customername),
               new XElement("EventName", eventname),
                new XElement("EventStartDate", eventdate),
                 new XElement("EventVenueName", venue)

);

            StringBuilder strInfo = new StringBuilder();

            strInfo.Append("UniqueOrderNumber:" + Orderno);
            strInfo.Append("TicketTypeName:" + ticketname);
            strInfo.Append("TotalTicketQuantityPerOrder:" + tqty);
            strInfo.Append("TicketPrice:" + tprice);
            strInfo.Append("TicketDiscountAmount:" + discount);
            strInfo.Append("TicketType:" + tickettype);
            strInfo.Append("CustomerName:" + customername);
            strInfo.Append("EventName:" + eventname);
            strInfo.Append("EventStartDate:" + eventdate);
            strInfo.Append("EventVenueName:" + venue);

            return strInfo.ToString();

        }

        public MemoryStream generateTicketPDF(Pdfgeneration pdf, string barcode1)
        {
            WebClient wc = new WebClient();
            string htmlPath = Server.MapPath("..");
            string htmlText = wc.DownloadString(htmlPath + "/email.html");
            htmlText = htmlText.Replace("¶¶Email¶¶", "");
            htmlText = htmlText.Replace("¶¶EventTitleId¶¶", pdf.EventTitleId);
            htmlText = htmlText.Replace("¶¶EventStartDateID¶¶", pdf.EventStartDateID);
            htmlText = htmlText.Replace("¶¶EventVenueID¶¶", pdf.EventVenueID);
            htmlText = htmlText.Replace("¶¶EventOrderNO¶¶", pdf.EventOrderNO);
            htmlText = htmlText.Replace("¶¶UserFirstNameID¶¶", pdf.UserFirstNameID);
            htmlText = htmlText.Replace("¶¶UserLastNameID¶¶", pdf.UserLastNameID);
            htmlText = htmlText.Replace("¶¶TicketOrderDateId¶¶", pdf.TicketOrderDateId);
            htmlText = htmlText.Replace("¶¶EventStartTimeID¶¶", pdf.EventStartTimeID);
            htmlText = htmlText.Replace("¶¶Ticketname¶¶", "");
            htmlText = htmlText.Replace("¶¶Tickettype¶¶", pdf.Tickettype);
            htmlText = htmlText.Replace("¶¶EventBarcodeId¶¶", pdf.EventBarcodeId);
            htmlText = htmlText.Replace("¶¶EventQrCode¶¶", pdf.EventQrCode);
            htmlText = htmlText.Replace("¶¶EventImage¶¶", pdf.EventImage);
            htmlText = htmlText.Replace("¶¶EventdayId¶¶", pdf.EventdayId);
            htmlText = htmlText.Replace("¶¶EventLogo¶¶", pdf.EventLogo);
            htmlText = htmlText.Replace("¶¶Eventtype¶¶", pdf.Eventtype);
            htmlText = htmlText.Replace("¶¶EventDescription¶¶", pdf.EventDescription);
            htmlText = htmlText.Replace("¶¶TicketPrice¶¶", pdf.TicketPrice);
            htmlText = htmlText.Replace("¶¶TicketQty¶¶", pdf.Purchased_Qty);
            htmlText = htmlText.Replace("¶¶EventOrganiserName¶¶", pdf.EventOrganiserName);
            htmlText = htmlText.Replace("¶¶EventOrganiserEmail¶¶", pdf.EventOrganiserEmail);

            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
            var pdfBytes = htmlToPdf.GeneratePdf(htmlText);
            MemoryStream mms = new MemoryStream(pdfBytes);
            return mms;

        }
        public string GetOrderNo()
        {
            string strOrderNo = "";
            if (Session["TicketLockedId"] != null)
            {
                using (EventComboEntities objECE = new EventComboEntities())
                {
                    long lMax = (from Ord in objECE.Order_Detail_T
                                 select Ord.O_Id
                                  ).Max();

                    strOrderNo = (from Ord in objECE.Order_Detail_T
                                  where Ord.O_Id == lMax
                                  select Ord.O_Order_Id).SingleOrDefault();


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
                                        TicketAmount = TLD.TicketAmount
                                    }
                                        );
                    return modelTLD.ToList();
                }
            }
            catch (Exception ex)
            {

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

        public async Task<string> SaveDetailsForPaypal()
        {
            TicketPayment objTP = (Session["TicketDatamodel"] != null ? (TicketPayment)Session["TicketDatamodel"] : null);
            string strResult = await SaveDetails(objTP, objTP.strOrderTotal, objTP.strGrandTotal, objTP.strPromId, objTP.strVarChanges, objTP.strVarId, objTP.strPaymentType);
            return strResult;
        }

        public ActionResult PaymentConfirmation()
        {
            if (Session["AppId"] != null)
            {
                Session["TicketDatamodel"] = null;
                List<paymentdate> Dateofevent = new List<paymentdate>();
                string strGUID = (Session["TicketLockedId"] != null ? Session["TicketLockedId"].ToString() : "");
                List<Email_Tag> EmailTag = new List<Email_Tag>();
                EventComboEntities objContent = new EventComboEntities();
                var EvtOrDetail = (from Order in objContent.Ticket_Purchased_Detail where Order.TPD_GUID == strGUID select Order).FirstOrDefault();
                long Eventid = (long)EvtOrDetail.TPD_Event_Id;
                HomeController hmc = new HomeController();
                hmc.ControllerContext = new ControllerContext(this.Request.RequestContext, hmc);
                string usernme = hmc.getusername();
                if (string.IsNullOrEmpty(usernme))
                {
                    return RedirectToAction("Index", "Home");
                }
                Session["Fromname"] = "events";
                //Send mail
                var Userid = Session["AppId"].ToString();

                var userdetail = db.AspNetUsers.FirstOrDefault(i => i.Id == Userid);
                var profiledetail = db.Profiles.FirstOrDefault(i => i.UserID == Userid);

                var email = userdetail.Email;
                var username = profiledetail.FirstName;
                var lastname = profiledetail.LastName;
                // Organiserdetail


                var Organiser = db.Event_Orgnizer_Detail.FirstOrDefault(i => i.Orgnizer_Event_Id == Eventid && i.DefaultOrg == "Y");
                var Organiserdetail = db.Profiles.FirstOrDefault(i => i.UserID == Organiser.UserId);
                var Organisername = Organiserdetail.FirstName;
                var Organiseremail = Organiserdetail.Email;

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
                foreach (var item in TicketPurchasedDetail)
                {
                    //Detail to send on page
                    paymentdate pdate = new paymentdate();
                    var tQntydetail = db.Ticket_Quantity_Detail.FirstOrDefault(i => i.TQD_Id == item.TPD_TQD_Id);

                    var tickets = db.Tickets.FirstOrDefault(i => i.T_Id == tQntydetail.TQD_Ticket_Id);
                    var address = db.Addresses.FirstOrDefault(i => i.AddressID == tQntydetail.TQD_AddressId);
                    var eventdetail = db.Events.FirstOrDefault(i => i.EventID == tQntydetail.TQD_Event_Id);
                    var eventid = tQntydetail.TQD_Event_Id;
                    var datetime = DateTime.Parse(tQntydetail.TQD_StartDate);
                    var day = datetime.DayOfWeek;
                    var Sdate = datetime.ToString("MMM dd, yyyy");
                    var addresslist = "";
                    var time = tQntydetail.TQD_StartTime;
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
                    Dateofevent.Add(pdate);
                    //Detail to send on page


                    string barcode1 = "<img src =https://www.barcodesinc.com/generator/image.php?code=" + item.TPD_Order_Id + "&style=196&type=C128B&width=219&height=50&xres=1&font=3 alt = 'BarCode' >";


                    string to = "", from = "", cc = "", bcc = "", subjectn = "";
                    var bodyn = "";
                    var ticketP = "";

                    //Get Email tags
                    EmailTag = hmc.getTag();
                    //Get Email tags
                    var tickettype = "";
                    var fee = "";
                    if (tickets.TicketTypeID == 1)
                    {
                        tickettype = "Free";
                        ticketP = "$0.00";
                        fee = "$0.00";
                    }
                    if (tickets.TicketTypeID == 2)
                    {
                        var ticketprice = item.TPD_Purchased_Qty * tickets.TotalPrice;
                        ticketP = "$" + ticketprice.ToString();
                        tickettype = "Paid";
                        if (tickets.Fees_Type == "1")
                        {
                            fee = tickets.EC_Fee.ToString();
                        }
                        else
                        {
                            fee = tickets.Customer_Fee.ToString();
                        }

                    }
                    if (tickets.TicketTypeID == 3)
                    {
                        ticketP = "$" + item.TPD_Donate.ToString();
                        tickettype = "Donate";
                        fee = "";
                    }
                    string xel = createxml(item.TPD_Order_Id, tickets.T_name, item.TPD_Purchased_Qty.ToString(), ticketP, fee, tickets.T_Discount.ToString(), tickettype, username, eventdetail.EventTitle, tQntydetail.TQD_StartDate, tQntydetail.TQD_StartTime, addresslist, "", "");

                    string qrImgPath = Server.MapPath("..") + "/Images/QR_Image.Png";
                    string barImgPath = Server.MapPath("..") + "/Images/Bar_Image.Png";
                    string Imagelogo = Server.MapPath("..") + "/Images/logo_vertical.png";
                    generateQR(xel.ToString(), qrImgPath);
                    generateBarCode(item.TPD_Order_Id, barImgPath);
                    string Qrcode = "<img style = 'width:150px;height:150px' src ='" + qrImgPath + "' alt = 'QRCode' />";
                    string QrcodeImg = "<img  src = 'http://eventcombonew-qa.kiwireader.com/Images/QR_Image.png' alt = 'QrCode' >";
                    string barcode = "<img  src ='" + barImgPath + "' alt = 'BarCode' >";
                    string barcodeimg = "<img  src = 'http://eventcombonew-qa.kiwireader.com/Images/Bar_Image.png' alt = 'BarCode' >";
                    string logoImage = "<img style='width:57px;height:375px' src ='" + Imagelogo + "' alt = 'Logo' >";
                    string logoImageemail = "<img style='width:57px;height:375px' src = cid:myImageID alt = 'Logo' >";
                    CreateEventController ccEvent = new CreateEventController();

                    var Images = ccEvent.GetImages(eventdetail.EventID).FirstOrDefault();
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
                    string Imageeventimg = "<img style='width:200px' src = cid:myeventImageID alt = 'Image' >";
                    Pdfgeneration pdf = new Pdfgeneration();
                    pdf.EventBarcodeId = barcode;
                    pdf.EventQrCode = Qrcode;
                    pdf.EventLogo = logoImage;
                    pdf.EventOrderNO = item.TPD_Order_Id;
                    pdf.UserFirstNameID = username;
                    pdf.UserLastNameID = lastname;
                    pdf.EventImage = Imagevent;
                    pdf.EventTitleId = eventdetail.EventTitle;
                    pdf.EventdayId = day.ToString();
                    pdf.EventStartDateID = tQntydetail.TQD_StartDate;
                    pdf.EventStartTimeID = tQntydetail.TQD_StartTime;
                    pdf.Eventtype = etype;
                    pdf.EventVenueID = addresslist;
                    pdf.EventOrganiserName = Organisername;
                    pdf.EventOrganiserEmail = Organiseremail;
                    pdf.EventDescription = eventdetail.EventDescription;
                    pdf.Tickettype = tickettype;
                    pdf.TicketOrderDateId = DateTime.Now.ToString();
                    pdf.TicketPrice = ticketP;
                    pdf.Purchased_Qty = item.TPD_Purchased_Qty.ToString();


                    MemoryStream attachment = generateTicketPDF(pdf, barcode1);

                    var Emailtemplate = hmc.getEmail("eticket");
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

                            for (int i = 0; i < EmailTag.Count; i++) // Loop with for.
                            {

                                if (subjectn.Contains("¶¶" + EmailTag[i].Tag_Name.Trim() + "¶¶"))
                                {
                                    if (EmailTag[i].Tag_Name == "UserEmailID")
                                    {
                                        subjectn = subjectn.Replace("¶¶UserEmailID¶¶", email);

                                    }
                                    if (EmailTag[i].Tag_Name == "UserFirstNameID")
                                    {
                                        subjectn = subjectn.Replace("¶¶UserFirstNameID¶¶", username);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventTitleId")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventTitleId¶¶", eventdetail.EventTitle);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventOrderNO")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventOrderNO¶¶", item.TPD_Order_Id);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventStartDateID")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventStartDateID¶¶", tQntydetail.TQD_StartDate);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventVenueID")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventVenueID¶¶", addresslist);

                                    }
                                    if (EmailTag[i].Tag_Name == "Tickettype")
                                    {
                                        subjectn = subjectn.Replace("¶¶Tickettype¶¶", tickets.T_name);

                                    }
                                    if (EmailTag[i].Tag_Name == "TicketOrderDateId")
                                    {
                                        subjectn = subjectn.Replace("¶¶TicketOrderDateId¶¶", DateTime.Now.ToString());

                                    }
                                    if (EmailTag[i].Tag_Name == "EventImageId")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventImageId¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "EventStartTimeID")
                                    {
                                        subjectn = subjectn.Replace("¶¶EventStartTimeID¶¶", tQntydetail.TQD_StartTime);

                                    }
                                    if (EmailTag[i].Tag_Name == "TicketPrice")
                                    {
                                        subjectn = subjectn.Replace("¶¶TicketPrice¶¶", ticketP);

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



                                    if (EmailTag[i].Tag_Name == "EventTitleId")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventTitleId¶¶", pdf.EventTitleId);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventStartDateID")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventStartDateID¶¶", pdf.EventStartDateID);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventVenueID")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventVenueID¶¶", pdf.EventVenueID);

                                    }
                                    if (EmailTag[i].Tag_Name == "UserFirstNameID")
                                    {
                                        bodyn = bodyn.Replace("¶¶UserFirstNameID¶¶", pdf.UserFirstNameID);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventOrderNO")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventOrderNO¶¶", pdf.EventOrderNO);

                                    }
                                    if (EmailTag[i].Tag_Name == "UserLastNameID")
                                    {
                                        bodyn = bodyn.Replace("¶¶UserLastNameID¶¶", pdf.UserLastNameID);

                                    }
                                    if (EmailTag[i].Tag_Name == "TicketOrderDateId")
                                    {
                                        bodyn = bodyn.Replace("¶¶TicketOrderDateId¶¶", pdf.TicketOrderDateId);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventStartTimeID")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventStartTimeID¶¶", pdf.EventStartTimeID);

                                    }
                                    if (EmailTag[i].Tag_Name == "Ticketname")
                                    {
                                        bodyn = bodyn.Replace("¶¶Ticketname¶¶", "");

                                    }
                                    if (EmailTag[i].Tag_Name == "Tickettype")
                                    {
                                        bodyn = bodyn.Replace("¶¶Tickettype¶¶", pdf.Tickettype);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventBarcodeId")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventBarcodeId¶¶", barcodeimg);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventQrCode")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventQrCode¶¶", QrcodeImg);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventImage")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventImage¶¶", Imageeventimg);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventdayId")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventdayId¶¶", pdf.EventdayId);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventLogo")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventLogo¶¶", logoImageemail);

                                    }
                                    if (EmailTag[i].Tag_Name == "Eventtype")
                                    {
                                        bodyn = bodyn.Replace("¶¶Eventtype¶¶", pdf.Eventtype);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventDescription")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventDescription¶¶", pdf.EventDescription);

                                    }
                                    if (EmailTag[i].Tag_Name == "TicketPrice")
                                    {
                                        bodyn = bodyn.Replace("¶¶TicketPrice¶¶", pdf.TicketPrice);

                                    }
                                    if (EmailTag[i].Tag_Name == "TicketQty")
                                    {
                                        bodyn = bodyn.Replace("¶¶TicketQty¶¶", pdf.Purchased_Qty);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventOrganiserName")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventOrganiserName¶¶", pdf.EventOrganiserName);

                                    }
                                    if (EmailTag[i].Tag_Name == "EventOrganiserEmail")
                                    {
                                        bodyn = bodyn.Replace("¶¶EventOrganiserEmail¶¶", pdf.EventOrganiserEmail);

                                    }
                                    if (EmailTag[i].Tag_Name == "UserEmailID")
                                    {
                                        bodyn = bodyn.Replace("¶¶UserEmailID¶¶", email);

                                    }
                                    ////////////////////



                                }

                            }
                        }
                        //Mail 
                        hmc.SendHtmlFormattedEmail(to, from, subjectn, bodyn, cc, bcc, attachment, Imageevent, qrImgPath, barImgPath);
                        //Mail 
                    }
                }

                //Send mail

                CreateEventController cs = new CreateEventController();
                PaymentConfirmation ps = new PaymentConfirmation();
                var Eventdetails = cs.GetEventdetail(Eventid);
                ps.imgurl = cs.GetImages(Eventid).FirstOrDefault();
                ps.Tilte = Eventdetails.EventTitle;
                ps.description = Eventdetails.EventDescription;
                ps.Eventid = Eventdetails.EventID.ToString();

                var OrganiserDetail = (from ev in db.Event_Orgnizer_Detail where ev.Orgnizer_Event_Id == Eventid && ev.DefaultOrg == "Y" select ev).FirstOrDefault();

                ps.Organiserid = OrganiserDetail.Orgnizer_Id.ToString();
                var guid = Session["TicketLockedId"].ToString();
                AccountController ac = new AccountController();
                string strUsers = (Session["AppId"] != null ? Session["AppId"].ToString() : "");
                var acountdedtails = ac.GetLoginDetails(strUsers);
                ps.sendlatestdetails = acountdedtails.SendLatestdetails;
                ps.Username = acountdedtails.Firstname + " " + acountdedtails.Lastname;
                ps.Email = acountdedtails.Email;
                var url = Request.Url;
                var baseurl = url.GetLeftPart(UriPartial.Authority);
                ps.url = baseurl + Url.Action("ViewEvent", "CreateEvent") + "?strUrlData=" + Eventdetails.EventTitle.Trim() + "౼" + Eventid + "౼N";


                ViewBag.Timecal = Dateofevent;
                return View(ps);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public string GetOrderDetailForConfirmation()
        {
            string strResult = "";
            string strGuid = (Session["TicketLockedId"] != null ? Session["TicketLockedId"].ToString() : "");
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
                                       ).SingleOrDefault();

                    var PurchaseDetail = (from TPD in objEnt.Ticket_Purchased_Detail
                                          where TPD.TPD_GUID == strGuid
                                          select TPD
                                      ).ToList();

                    long? iPaidCount = 0; long? iFreeCount = 0;
                    foreach (Ticket_Purchased_Detail TPD in PurchaseDetail)
                    {
                        var vTId = (from TQD in objEnt.Ticket_Quantity_Detail
                                    where TQD.TQD_Id == TPD.TPD_TQD_Id
                                    select TQD.TQD_Ticket_Id).SingleOrDefault();
                        var vTType = (from Tkt in objEnt.Tickets
                                      where Tkt.T_Id == vTId
                                      select Tkt.TicketTypeID).SingleOrDefault();

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
                                    ).SingleOrDefault();

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


                    //strResult = OrderNo + "~" + TicketCount.totalOrder + "~" + OrderAmt;
                }
            }

            return strResult;

        }


        public void getlatestsetting(string get)
        {
            string strUsers = (Session["AppId"] != null ? Session["AppId"].ToString() : "");
            using (EventComboEntities db = new EventComboEntities())
            {
                Profile prof = db.Profiles.Where(x => x.UserID == strUsers).FirstOrDefault();
                prof.SendCur_EventDetail = get;
                db.SaveChanges();

            }

        }






    }






}