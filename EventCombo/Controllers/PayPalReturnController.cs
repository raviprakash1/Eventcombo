using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
namespace EventCombo.Controllers
{
    public class PayPalReturnController : Controller
    {
        // GET: PayPalReturn
        public async System.Threading.Tasks.Task<ActionResult> PayPalReturn(string token, string PayerID)
        {
            // will also save token and payer id in database;
            Session["token"] = token;
            Session["PayerID"] = PayerID;
            TicketPayment objTP = (Session["TicketDatamodel"] != null ? (TicketPayment)Session["TicketDatamodel"] : null);
            if (objTP != null)
            {
                

                TicketPaymentController objTc = new TicketPaymentController();
                objTc.ControllerContext = new ControllerContext(this.Request.RequestContext, objTc);
                string strResult = await objTc.SaveDetails(objTP, objTP.strOrderTotal, objTP.strGrandTotal, objTP.strPromId, objTP.strVarChanges, objTP.strVarId, objTP.strPaymentType);
            }
            return View();
        }



        //public string SaveDetails(TicketPayment model, string strOrderTotal, string strGrandTotal, string strPromId, string strVarChanges, string strVarId, string strPaymentType)
        //{
        //    string ApiLoginID; string ApiTransactionKey; string strCardNo; string strExpDate; string strCvvCode; decimal dAmount;
        //    ApiLoginID = ""; ApiTransactionKey = ""; strCardNo = ""; strExpDate = ""; strCvvCode = ""; dAmount = 0;



        //    HomeController hm = new HomeController();
        //    hm.ControllerContext = new ControllerContext(this.Request.RequestContext, hm);
        //    if (!string.IsNullOrEmpty(model.AccconfirmEmail) && !string.IsNullOrEmpty(model.Accpassword))
        //    {
        //        string userid = await saveuser(model.AccEmail, model.Accpassword);
        //        if (!string.IsNullOrEmpty(userid))
        //        {
        //            using (EventComboEntities objEntity = new EventComboEntities())
        //            {

        //                Profile prof = new Profile();
        //                prof.FirstName = model.AccFname;
        //                prof.Email = model.AccEmail;
        //                prof.LastName = model.AccLname;
        //                prof.MainPhone = model.Accountphnno;
        //                prof.City = model.AccCity;
        //                prof.State = model.AccState;
        //                prof.Zip = model.Acczip;
        //                prof.CountryID = byte.Parse(model.Acccountry);
        //                prof.UserID = userid;

        //                objEntity.Profiles.Add(prof);
        //                objEntity.SaveChanges();
        //            }
        //            Session["AppId"] = userid;
        //        }
        //        else
        //        {
        //            Session["AppId"] = null;
        //        }


        //    }
        //    if (Session["AppId"] != null)
        //    {
        //        string Userid = Session["AppId"].ToString();
        //        string guid = Session["TicketLockedId"].ToString();
        //        using (EventComboEntities objEntity = new EventComboEntities())
        //        {
        //            Profile prof = objEntity.Profiles.First(i => i.UserID == Userid);
        //            prof.FirstName = model.AccFname;
        //            if (!string.IsNullOrEmpty(model.AccLname))
        //            {
        //                prof.LastName = model.AccLname;
        //            }
        //            if (!string.IsNullOrEmpty(model.Accountphnno))
        //            {
        //                prof.MainPhone = model.Accountphnno;
        //            }
        //            if (!string.IsNullOrEmpty(model.AccCity))
        //            {
        //                prof.City = model.AccCity;
        //            }
        //            if (!string.IsNullOrEmpty(model.AccState))
        //            {
        //                prof.State = model.AccState;
        //            }
        //            if (!string.IsNullOrEmpty(model.Acczip))
        //            {
        //                prof.Zip = model.Acczip;
        //            }

        //            prof.CountryID = byte.Parse(model.Acccountry);





        //            Order_Detail_T objOdr = new Order_Detail_T();
        //            objOdr.O_Order_Id = "";
        //            objOdr.O_TotalAmount = CommanClasses.ConvertToNumeric(strGrandTotal); ;
        //            objOdr.O_User_Id = Userid;
        //            objOdr.O_OrderAmount = CommanClasses.ConvertToNumeric(strOrderTotal);
        //            objOdr.O_VariableId = CommanClasses.ConvertToLong(strVarId);
        //            objOdr.O_VariableAmount = CommanClasses.ConvertToNumeric(strVarChanges);
        //            objOdr.O_PromoCodeId = CommanClasses.ConvertToLong(strPromId);

        //            objEntity.Order_Detail_T.Add(objOdr);
        //            objEntity.SaveChanges();
        //            string strOrderNo = GetOrderNo();

        //            //List<Ticket_Locked_Detail> objLockedTic = new List<Ticket_Locked_Detail>();
        //            List<Ticket_Locked_Detail_List> objLockedTic = new List<Ticket_Locked_Detail_List>();
        //            objLockedTic = GetLockTickets();
        //            Ticket_Purchased_Detail objTPD;

        //            foreach (Ticket_Locked_Detail_List TLD in objLockedTic)
        //            {
        //                objTPD = new Ticket_Purchased_Detail();
        //                objTPD.TPD_Amount = TLD.TicketAmount;
        //                objTPD.TPD_Donate = TLD.TLD_Donate;
        //                objTPD.TPD_Event_Id = TLD.TLD_Event_Id;
        //                objTPD.TPD_Order_Id = strOrderNo;
        //                objTPD.TPD_Purchased_Qty = TLD.TLD_Locked_Qty;
        //                objTPD.TPD_TQD_Id = TLD.TLD_TQD_Id;
        //                objTPD.TPD_GUID = TLD.TLD_GUID;
        //                objTPD.TPD_User_Id = Userid;
        //                objEntity.Ticket_Purchased_Detail.Add(objTPD);
        //            }


        //            if (model.Ticketname == "Paid")
        //            {

        //                if (model.Savecarddetail != "N")
        //                {
        //                    var objcarddetail = (from objdetail in objEntity.CardDetails where objdetail.UserId == Userid && objdetail.CardNumber == model.cardno select objdetail).Any();
        //                    if (!objcarddetail)
        //                    {
        //                        CardDetail card = new CardDetail();
        //                        card.OrderId = strOrderNo;
        //                        card.CardNumber = model.cardno;
        //                        card.ExpirationDate = model.expirydate;
        //                        card.Cvv = model.cvv;
        //                        card.UserId = Userid;
        //                        card.Guid = guid;
        //                        card.card_type = model.card_type;
        //                        objEntity.CardDetails.Add(card);
        //                    }


        //                }


        //                BillingAddress badd = new BillingAddress();

        //                badd.Fname = model.billfname;
        //                badd.Lname = model.billLname;
        //                badd.Address1 = model.billaddress1;
        //                badd.Address2 = model.billaddress2;
        //                badd.City = model.billcity;
        //                badd.State = model.billstate;
        //                badd.Zip = model.billzip;
        //                badd.Country = model.billcountry;
        //                badd.Phone_Number = model.billingphno;
        //                badd.UserId = Userid;
        //                badd.Guid = guid;
        //                badd.OrderId = strOrderNo;
        //                objEntity.BillingAddresses.Add(badd);
        //                if (model.Saveshipdetail != "N")
        //                {
        //                    ShippingAddress shipadd = new ShippingAddress();

        //                    shipadd.Fname = model.shipfname;
        //                    shipadd.Lname = model.shipLname;
        //                    shipadd.Address1 = model.shipaddress1;
        //                    shipadd.Address2 = model.shipaddress2;
        //                    shipadd.City = model.shipcity;
        //                    shipadd.State = model.shipstate;
        //                    shipadd.Zip = model.shipzip;
        //                    shipadd.Country = model.shipcountry;
        //                    shipadd.Phone_Number = model.shipphno;
        //                    shipadd.UserId = Userid;
        //                    shipadd.Guid = guid;
        //                    shipadd.OrderId = strOrderNo;
        //                    objEntity.ShippingAddresses.Add(shipadd);
        //                }
        //                if (model.sameshipbilldetail == "Y")
        //                {
        //                    ShippingAddress shipadd = new ShippingAddress();
        //                    shipadd.Fname = model.billfname;
        //                    shipadd.Lname = model.billLname;
        //                    shipadd.Address1 = model.billaddress1;
        //                    shipadd.Address2 = model.billaddress2;
        //                    shipadd.City = model.billcity;
        //                    shipadd.State = model.billstate;
        //                    shipadd.Zip = model.billzip;
        //                    shipadd.Country = model.billcountry;
        //                    shipadd.Phone_Number = model.billingphno;
        //                    shipadd.UserId = Userid;
        //                    shipadd.Guid = guid;
        //                    shipadd.OrderId = strOrderNo;
        //                    objEntity.ShippingAddresses.Add(shipadd);
        //                }
        //                if (model.NameList != null)
        //                {
        //                    TicketBearer ObjAdd = new TicketBearer();
        //                    foreach (TicketBearer objA in model.NameList)
        //                    {

        //                        ObjAdd = new TicketBearer();
        //                        ObjAdd.UserId = Userid;
        //                        ObjAdd.Guid = guid;
        //                        ObjAdd.Email = objA.Email;
        //                        ObjAdd.Name = objA.Name;
        //                        ObjAdd.OrderId = strOrderNo;
        //                        objEntity.TicketBearers.Add(ObjAdd);


        //                    }
        //                }


        //                // -------------------------------------------------- Payment Transfer Card detail -----------------------------------------

        //            }
        //            objEntity.SaveChanges();
        //            if (strPaymentType == "A")
        //            {
        //                ApiLoginID = "354v9ZufxM6";
        //                ApiTransactionKey = "68Et2R3KcV62rJ27";
        //                strCardNo = model.cardno;
        //                strExpDate = model.expirydate;
        //                strCvvCode = model.cvv;
        //                dAmount = (strGrandTotal != "" ? Convert.ToDecimal(strGrandTotal) : 0);
        //                //  PaymentProcess.CheckCreditCard(ApiLoginID, ApiTransactionKey, strCardNo, strExpDate, strCvvCode, dAmount);
        //            }
        //            //else if (strPaymentType == "P")
        //            //{
        //            //    RedirectToAction("Pay", "Cart");
        //            //}






        //            Session["AppId"] = Userid;
        //            Session["TicketLockedId"] = guid;

        //            return strOrderNo;


        //        }
        //    }
        //    else
        //    {

        //        return "Some Error Comes.";

        //    }
        //}



        //public string saveuser(string Email, string password)
        //{

        //    //var user = new ApplicationUser { UserName = Email, Email = Email };
        //    //var result = await UserManager.CreateAsync(user, password);
        //    //if (result.Succeeded)
        //    //{
        //    //    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //    //    var Userid = UserManager.FindByEmail(user.Email);
        //    //    await this.UserManager.AddToRoleAsync(Userid.Id, "Member");
        //    //    using (EventComboEntities objEntity = new EventComboEntities())
        //    //    {
        //    //        User_Permission_Detail permdetail = new User_Permission_Detail();
        //    //        for (int i = 1; i < 3; i++)
        //    //        {

        //    //            permdetail.UP_Permission_Id = i;
        //    //            permdetail.UP_User_Id = Userid.Id.ToString();
        //    //            objEntity.User_Permission_Detail.Add(permdetail);
        //    //            objEntity.SaveChanges();
        //    //        }
        //    //    }
        //    //    return Userid.Id;
        //    //}
        //    //else
        //    //{
        //    //    return "";

        //    //}
        //}

    }
}