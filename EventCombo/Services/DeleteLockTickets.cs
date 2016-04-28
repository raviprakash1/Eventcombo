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
                string strSql = "delete ev set ev.EventStatus = 'Expired' FROM Event ev LEFT Join MultipleEvent me on ev.EventID = me.EventID  where me.M_StartTo < getutcdate() AND LOWER(EventStatus)='live'";
                db.Database.ExecuteSqlCommand(strSql);
                db.SaveChanges();
            }
        }
    }
}