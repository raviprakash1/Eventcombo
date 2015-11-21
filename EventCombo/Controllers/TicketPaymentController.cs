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
            TicketPayment tp = new TicketPayment();
            string defaultCountry = "";
            string Fname = "", Lname = "", Phnnumber = "", Adress = "", Email = "";
            var url = Url.Action("TicketPayment", "TicketPayment") + "?Eventid=" + Eventid;
            Session["ReturnUrl"] = "TicketPayment~" + url;
            CreateEventController cs = new CreateEventController();
            AccountController AccDetail = new AccountController();
            tp.Imageurl = cs.GetImages(Eventid).FirstOrDefault();
            var eventdetails = cs.GetEventdetail(Eventid);
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
            if (!string.IsNullOrEmpty(model.AccconfirmEmail) && !string.IsNullOrEmpty(model.Accpassword))
            {

                string userid = await saveuser(model.AccEmail, model.Accpassword);

                if (!string.IsNullOrEmpty(userid))
                {
                    using (EventComboEntities objEntity = new EventComboEntities())
                    {

                        Profile prof = new Profile();
                        prof.FirstName = model.AccFname;
                        prof.LastName = model.AccLname;
                        prof.FirstName = model.AccFname;
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
                    decimal dAmt = 0;
                    foreach (Ticket_Locked_Detail_List TLD in objLockedTic)
                    {
                        objTPD = new Ticket_Purchased_Detail();
                        objTPD.TPD_Amount = dAmt;
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


                        //ApiLoginID = "354v9ZufxM6";
                        //ApiTransactionKey = "68Et2R3KcV62rJ27";
                        //strCardNo = model.cardno;
                        //strExpDate = model.expirydate;
                        //strCvvCode = model.cvv;
                        //dAmount = (strGrandTotal != "" ? Convert.ToDecimal(strGrandTotal) : 0);
                        //  PaymentProcess.CheckCreditCard(ApiLoginID, ApiTransactionKey, strCardNo, strExpDate, strCvvCode, dAmount);

                    }

                    ApiLoginID = "354v9ZufxM6";
                    ApiTransactionKey = "68Et2R3KcV62rJ27";
                    strCardNo = model.cardno;
                    strExpDate = model.expirydate;
                    strCvvCode = model.cvv;
                    dAmount = (strGrandTotal != "" ? Convert.ToDecimal(strGrandTotal) : 0);
                    //PaymentProcess.CheckCreditCard(ApiLoginID, ApiTransactionKey, strCardNo, strExpDate, strCvvCode, dAmount);
                    objEntity.SaveChanges();
                    return strOrderNo;

                }
            }
            else
            {

                return "Some Error Comes.";

            }
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
                                        TLD_Donate = TLD.TLD_Donate
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
            Session["Fromname"] = "ViewEvent";
            CreateEventController cs = new CreateEventController();
            PaymentConfirmation ps = new PaymentConfirmation();
            var Eventdetails = cs.GetEventdetail(Eventid);
            ps.imgurl = cs.GetImages(Eventid).FirstOrDefault();
            ps.Tilte = Eventdetails.EventTitle;
            ps.description = Eventdetails.EventDescription;
            AccountController ac = new AccountController();
            string strUsers = (Session["AppId"] != null ? Session["AppId"].ToString() : "");
            var acountdedtails = ac.GetLoginDetails(strUsers);
            ps.sendlatestdetails = acountdedtails.SendLatestdetails;
            ps.Username = acountdedtails.Firstname + " " + acountdedtails.Lastname;
            var url = Request.Url;
            var baseurl = url.GetLeftPart(UriPartial.Authority);
            ps.url = baseurl+ Url.Action("ViewEvent", "CreateEvent") + "?EventId=" + Eventdetails.EventID + "&eventTitle=a" ;
            return View(ps);
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

                    var OrderNo = (from TPD in objEnt.Ticket_Purchased_Detail
                                   where TPD.TPD_GUID == strGuid
                                   select TPD.TPD_Order_Id
                                       ).FirstOrDefault();

                    var OrderAmt = (from OD in objEnt.Order_Detail_T
                                    where OD.O_Order_Id == OrderNo
                                    select OD.O_TotalAmount
                                    ).SingleOrDefault();


                    strResult = OrderNo + "~" + TicketCount.totalOrder + "~" + OrderAmt;
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