using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
namespace EventCombo.Controllers
{
    public class TicketPaymentController : Controller
    {
        // GET: TicketPayment
        public ActionResult TicketPayment()
        {
            return View();
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
    }
}