using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
using Microsoft.AspNet.Identity;
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

namespace EventCombo.Controllers
{
    public class TicketPaymentController : Controller
    {
        EventComboEntities db = new EventComboEntities();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        // GET: TicketPayment
        public ActionResult TicketPayment(long Eventid)
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
            TicketPayment tp = new TicketPayment();
            string defaultCountry = "";
            string Fname = "", Lname = "", Phnnumber = "", Adress = "", Email = "";
            var url = Url.Action("TicketPayment", "TicketPayment") + "?Eventid=" + Eventid;
            Session["ReturnUrl"] = "TicketPayment~" + url;
            CreateEventController cs = new CreateEventController();
            AccountController AccDetail = new AccountController();
            tp.Imageurl = cs.GetImages(Eventid).FirstOrDefault();
            var eventdetails = cs.GetEventdetail(Eventid);
            tp.Ticketdeliveraddress = eventdetails.Ticket_DAdress;
            tp.Title = eventdetails.EventTitle;
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


            }

            tp.Email = Email;
            tp.FName = Fname;
            tp.LName = Lname;
            tp.PhnNo = Phnnumber;
            tp.Address = Adress;
            tp.EventId = Eventid;

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
                            card1.value = item.CardId.ToString();
                            card1.text = Fname + "-" + item.CardNumber;
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

            return Carddetails.CardNumber + "*" + Carddetails.Cvv + "*" + Carddetails.ExpirationDate;

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

        public async Task<string> SaveDetails(TicketPayment model, string strOrderTotal, string strGrandTotal, string strPromId, string strVarChanges, string strVarId)
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
                        objEntity.Ticket_Purchased_Detail.Add(objTPD);
                    }


                    if (model.Ticketname == "Paid")
                    {

                        if (model.Savecarddetail != "N")
                        {
                            CardDetail card = new CardDetail();
                            card.OrderId = strOrderNo;
                            card.CardNumber = model.cardno;
                            card.ExpirationDate = model.expirydate;
                            card.Cvv = model.cvv;
                            card.UserId = Userid;
                            card.Guid = guid;
                            objEntity.CardDetails.Add(card);


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
                    ApiLoginID = "354v9ZufxM6";
                    ApiTransactionKey = "68Et2R3KcV62rJ27";
                    strCardNo = model.cardno;
                    strExpDate = model.expirydate;
                    strCvvCode = model.cvv;
                    dAmount = (strGrandTotal != "" ? Convert.ToDecimal(strGrandTotal) : 0);
                    //  PaymentProcess.CheckCreditCard(ApiLoginID, ApiTransactionKey, strCardNo, strExpDate, strCvvCode, dAmount);
                    objEntity.SaveChanges();


                    //SendMail

                    var userdetail = db.AspNetUsers.First(i => i.Id == Userid);
                    var profiledetail = db.Profiles.FirstOrDefault(i => i.UserID == Userid);

                    var email = userdetail.Email;
                    var username = profiledetail.FirstName;

                    var TicketPurchasedDetail = db.Ticket_Purchased_Detail.Where(i => i.TPD_GUID == guid).ToList();
                    foreach (var item in TicketPurchasedDetail)
                    {
                        var tQntydetail = db.Ticket_Quantity_Detail.FirstOrDefault(i => i.TQD_Id == item.TPD_TQD_Id);

                        var tickets = db.Tickets.FirstOrDefault(i => i.T_Id == tQntydetail.TQD_Ticket_Id);
                        var address = db.Addresses.FirstOrDefault(i => i.AddressID == tQntydetail.TQD_AddressId);
                        var eventdetail = db.Events.FirstOrDefault(i => i.EventID == tQntydetail.TQD_Event_Id);
                      string barcode1="<img src =https://www.barcodesinc.com/generator/image.php?code="+strOrderNo+"&style=196&type=C128B&width=219&height=50&xres=1&font=3 alt = 'BarCode' >";
                           

                        string to = "", from = "", cc = "", bcc = "", subjectn = "";
                        var bodyn = "";
                        var ticketP = "";
                        HomeController hmc = new HomeController();
                        List<Email_Tag> EmailTag = new List<Email_Tag>();
                        EmailTag = hmc.getTag();
                        var tickettype = "";
                        var fee = "";
                        if(tickets.TicketTypeID==1)
                        {
                            tickettype = "Free";
                            ticketP = "";
                            fee = "";
                        }
                        if (tickets.TicketTypeID == 2)
                        {
                           var  ticketprice = item.TPD_Purchased_Qty * tickets.TotalPrice;
                            ticketP = "$" + ticketprice.ToString();
                            tickettype = "Paid";
                            if(tickets.Fees_Type=="1")
                            {
                                fee = tickets.EC_Fee.ToString();
                            }else
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
                        string xel = createxml(strOrderNo, tickets.T_name, item.TPD_Purchased_Qty.ToString(), ticketP, fee, tickets.T_Discount.ToString(), tickettype, username, eventdetail.EventTitle, tQntydetail.TQD_StartDate, tQntydetail.TQD_StartTime, address.ConsolidateAddress, "", "");

                        string qrImgPath = Server.MapPath("..") + "/Images/QR_Image.Png";
                        string barImgPath = Server.MapPath("..") + "/Images/Bar_Image.Png";
                        generateQR(xel.ToString(), qrImgPath);
                        generateBarCode(strOrderNo, barImgPath);
                        string Qrcode = "<img style = 'width:100px;height:100px' src =" + qrImgPath + " alt = 'QRCode' />";
                        string barcode = "<img src =" + barImgPath + " alt = 'BarCode' >";
                        MemoryStream attachment = generateTicketPDF(email, username, DateTime.Now.ToString(), strOrderNo, eventdetail.EventTitle, tQntydetail.TQD_StartDate, address.ConsolidateAddress,tickets.T_name, tickettype, eventdetail.TimeZone, tQntydetail.TQD_StartTime, barcode, Qrcode, barcode1);
                       // MemoryStream attachment = new MemoryStream(attach);
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
                                            subjectn = subjectn.Replace("¶¶EventOrderNO¶¶", strOrderNo);

                                        }
                                        if (EmailTag[i].Tag_Name == "EventStartDateID")
                                        {
                                            subjectn = subjectn.Replace("¶¶EventStartDateID¶¶", tQntydetail.TQD_StartDate);

                                        }
                                        if (EmailTag[i].Tag_Name == "EventVenueID")
                                        {
                                            subjectn = subjectn.Replace("¶¶EventVenueID¶¶", address.ConsolidateAddress);

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


                                    if (EmailTag[i].Tag_Name == "TicketOrderDateId")
                                    {
                                        bodyn = bodyn.Replace("¶¶TicketOrderDateId¶¶", DateTime.Now.ToString());

                                    }
                                    if (bodyn.Contains("¶¶" + EmailTag[i].Tag_Name.Trim() + "¶¶"))
                                    {
                                        if (EmailTag[i].Tag_Name == "UserEmailID")
                                        {
                                            bodyn = bodyn.Replace("¶¶UserEmailID¶¶", email);

                                        }
                                        if (EmailTag[i].Tag_Name == "UserFirstNameID")
                                        {
                                            bodyn = bodyn.Replace("¶¶UserFirstNameID¶¶", username);

                                        }
                                        if (EmailTag[i].Tag_Name == "EventOrderNO")
                                        {
                                            bodyn = bodyn.Replace("¶¶EventOrderNO¶¶", strOrderNo);

                                        }
                                      
                                        if (EmailTag[i].Tag_Name == "EventBarcodeId")
                                        {
                                            bodyn = bodyn.Replace("¶¶EventBarcodeId¶¶", barcode1);

                                        }
                                        if (EmailTag[i].Tag_Name == "EventTitleId")
                                        {
                                            bodyn = bodyn.Replace("¶¶EventTitleId¶¶", eventdetail.EventTitle);

                                        }
                                        if (EmailTag[i].Tag_Name == "EventStartDateID")
                                        {
                                            bodyn = bodyn.Replace("¶¶EventStartDateID¶¶", tQntydetail.TQD_StartDate);

                                        }
                                        if (EmailTag[i].Tag_Name == "EventVenueID")
                                        {
                                            bodyn = bodyn.Replace("¶¶EventVenueID¶¶", address.ConsolidateAddress);

                                        }
                                        if (EmailTag[i].Tag_Name == "Tickettype")
                                        {
                                            bodyn = bodyn.Replace("¶¶Tickettype¶¶", tickets.T_name);

                                        }
                                        if (EmailTag[i].Tag_Name == "TicketOrderDateId")
                                        {
                                            bodyn = bodyn.Replace("¶¶TicketOrderDateId¶¶", DateTime.Now.ToString());

                                        }
                                        if (EmailTag[i].Tag_Name == "EventImageId")
                                        {
                                            bodyn = bodyn.Replace("¶¶EventImageId¶¶", "");

                                        }
                                        if (EmailTag[i].Tag_Name == "EventStartTimeID")
                                        {
                                            bodyn = bodyn.Replace("¶¶EventStartTimeID¶¶", tQntydetail.TQD_StartTime);

                                        }
                                        if (EmailTag[i].Tag_Name == "TicketPrice")
                                        {
                                            bodyn = bodyn.Replace("¶¶TicketPrice¶¶", ticketP);

                                        }
                                        
                                    }

                                }
                            }
                            hmc.SendHtmlFormattedEmail(to, from, subjectn, bodyn,cc,bcc, attachment);
                        }
                    }

                    Session["AppId"] = Userid;
                    Session["TicketLockedId"]= guid;

                    return strOrderNo;


                }
            }
            else
            {

                return "Some Error Comes.";

            }
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
            string url = "https://www.barcodesinc.com/generator/image.php?code=" + strBarCodeData + "&style=196&type=C128B&width=219&height=50&xres=1&font=3";
            byte[] barImage = wc.DownloadData(url);
            MemoryStream mms = new MemoryStream(barImage);
            Image img = Image.FromStream(mms);
            img.Save(strqrBarImgPath, ImageFormat.Png);
            mms.Close();
        }

        private string createxml(string Orderno,string ticketname,string tqty,string tprice,string fee,string discount,string tickettype,string customername,string eventname,string eventdate,string eventime,string venue,string organisername,string organiseremail)
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

        public MemoryStream generateTicketPDF(string email, string username, string TicketOrderdate, string OrderNo, string EventTitle, string TQD_StartDate, string ConsolidateAddress, string T_name, string tickettype, string TimeZone, string TQD_StartTime,string barcode,string qrcode,string barcode1)
        {
            WebClient wc = new WebClient();
            string htmlPath = Server.MapPath("..");
            string htmlText = wc.DownloadString(htmlPath + "/EventTicket_HTML.html");
            htmlText = htmlText.Replace("¶¶Email¶¶", email);
            htmlText = htmlText.Replace("¶¶EventTitleId¶¶", EventTitle);
            htmlText = htmlText.Replace("¶¶EventStartDateID¶¶", TQD_StartDate);
            htmlText = htmlText.Replace("¶¶EventVenueID¶¶", ConsolidateAddress);
            htmlText = htmlText.Replace("¶¶EventOrderNO¶¶", OrderNo);
            htmlText = htmlText.Replace("¶¶UserFirstNameID¶¶", username);
            htmlText = htmlText.Replace("¶¶TicketOrderDateId¶¶", TicketOrderdate);
            htmlText = htmlText.Replace("¶¶EventStartTimeID¶¶", TQD_StartTime);
            htmlText = htmlText.Replace("¶¶EventTimeZone¶¶", TimeZone);
            htmlText = htmlText.Replace("¶¶EventTimeZone¶¶", TimeZone);
            htmlText = htmlText.Replace("¶¶Ticketname¶¶", T_name);
            htmlText = htmlText.Replace("¶¶Tickettype¶¶", tickettype);
            htmlText = htmlText.Replace("¶¶EventBarcodeId¶¶", barcode1);
            htmlText = htmlText.Replace("¶¶EventQrCode¶¶", qrcode);
            //Byte[] res = null;
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    HtmlToPdfConverter nRecohtmltoPdfObj = new HtmlToPdfConverter();
            //    nRecohtmltoPdfObj.Orientation = PageOrientation.Portrait;
            //    //nRecohtmltoPdfObj.PageFooterHtml = CreatePDFFooter();

            //    res = nRecohtmltoPdfObj.GeneratePdf(htmlText);

            //    //var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(htmlText, PdfSharp.PageSize.A4);
            //    //pdf.Save(ms);
            //    //res = ms.ToArray();
            //}
            //return res;

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

       

        public ActionResult PaymentConfirmation(long Eventid)

        {
            if (Session["AppId"] != null)
            {
                HomeController hmc = new HomeController();
                hmc.ControllerContext = new ControllerContext(this.Request.RequestContext, hmc);
                string usernme = hmc.getusername();
                if (string.IsNullOrEmpty(usernme))
                {
                    return RedirectToAction("Index", "Home");
                }
                Session["Fromname"] = "ViewEvent";
                List<paymentdate> Dateofevent = new List<paymentdate>();
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
                var TicketPurchasedDetail = db.Ticket_Purchased_Detail.Where(i => i.TPD_GUID == guid).ToList();
                foreach (var item in TicketPurchasedDetail)
                {
                    paymentdate pdate = new paymentdate();
                    var tQntydetail = db.Ticket_Quantity_Detail.FirstOrDefault(i => i.TQD_Id == item.TPD_TQD_Id);

                    var tickets = db.Tickets.FirstOrDefault(i => i.T_Id == tQntydetail.TQD_Ticket_Id);
                    var address = db.Addresses.FirstOrDefault(i => i.AddressID == tQntydetail.TQD_AddressId);
                    var eventdetail = db.Events.FirstOrDefault(i => i.EventID == tQntydetail.TQD_Event_Id);

                    var datetime = DateTime.Parse(tQntydetail.TQD_StartDate);
                    var day = datetime.DayOfWeek;
                    var Sdate = datetime.ToString("MMM dd, yyyy");
                    var time = tQntydetail.TQD_StartTime;

                    var addresslist = (!string.IsNullOrEmpty(address.ConsolidateAddress)) ? address.ConsolidateAddress : "";
                    var timefinal = day.ToString() + "~" + Sdate.ToString() + "~" + time + "~" + addresslist;
                    pdate.id = timefinal;
                    pdate.Address = addresslist;
                    pdate.Datetime = day.ToString() + " " + Sdate.ToString() + " " + time;
                    Dateofevent.Add(pdate);

                }
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
                                       where TPD.TPD_GUID == strGuid group TPD by new { TPD.TPD_GUID } into TPDgrp
                                       select new {
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
                                      where Tkt.T_Id  == vTId
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

                    if (iPaidCount==1)
                    {
                        strResult = "Order " + OrderNo.ToString() + " , " + TicketCount.totalOrder.ToString() + " ticket for $" + OrderAmt.ToString();
                    }
                    else if (iPaidCount>1)
                    {
                        strResult = "Order " + OrderNo.ToString() + " , " + TicketCount.totalOrder.ToString() + " tickets for $" + OrderAmt.ToString();
                    }
                    else if (iFreeCount ==1)
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


        public void  getlatestsetting(string get)
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