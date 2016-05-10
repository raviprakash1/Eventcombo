using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventCombo.Models;
namespace EventCombo.Services
{
    public class DeleteLockTickets
    {
        public void Delete()
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                string strSql = "DELETE FROM Ticket_Locked_Detail WHERE DATEDIFF(MINUTE,Locktime ,getutcdate()) > 10";
                db.Database.ExecuteSqlCommand(strSql);
                db.SaveChanges();
            }
        }
    }
}