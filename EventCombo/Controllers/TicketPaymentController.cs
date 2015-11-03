using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
using Microsoft.AspNet.Identity;

namespace EventCombo.Controllers
{
    public class TicketPaymentController : Controller
    {
        // GET: TicketPayment
        public ActionResult TicketPayment(long Eventid)
        {
            TicketPayment tp = new TicketPayment();
            string defaultCountry = "";
            string Fname = "", Lname = "", Phnnumber = "", Adress = "", Email = "";
            CreateEventController cs = new CreateEventController();
            AccountController AccDetail = new AccountController();
            tp.Imageurl = cs.GetImages(Eventid).FirstOrDefault();
            var eventdetails = cs.GetEventdetail(Eventid);
            tp.Title = eventdetails.EventTitle;
            tp.Tickettype = "Paid";
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
            }

            return View(tp);
        }

        public void ReleaseTickets(string strTTicketIds)
        {
            string strUsers = (Session["AppId"] != null ? Session["AppId"].ToString() : "");

            using (var context = new EventComboEntities())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM Ticket_Locked_Detail WHERE TLD_Id in (" + strTTicketIds + ") and TLD_User_Id ='" + strUsers + "'");
            }
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


        public async System.Threading.Tasks.Task<string> SaveDetails(TicketPayment ps)
        {
            HomeController hm = new HomeController();
            if (!string.IsNullOrEmpty(ps.AccconfirmEmail) && !string.IsNullOrEmpty(ps.Accpassword))
            {
                var user = new ApplicationUser { UserName = ps.Email, Email = ps.Email };
                string userid = await hm.saveuser(user, ps.Accpassword);
                if (!string.IsNullOrEmpty(userid))
                {
                    using (EventComboEntities objEntity = new EventComboEntities())
                    {


                        Profile prof = new Profile();
                        prof.FirstName = ps.AccFname;
                        prof.LastName = ps.AccLname;
                        prof.FirstName = ps.AccFname;
                        prof.MainPhone = ps.Accountphnno;
                        prof.City = ps.AccCity;
                        prof.State = ps.AccState;
                        prof.Zip = ps.Acczip;
                        prof.CountryID =byte.Parse(ps.Acccountry);
                        prof.UserID = userid;

                        objEntity.Profiles.Add(prof);
                        objEntity.SaveChanges();
                    }
                }


            }
            return "";
        } 
      

    }
}