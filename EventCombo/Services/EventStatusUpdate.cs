using EventCombo.Models;
using EventCombo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Services
{
    public class EventStatusUpdate
    {
        public void Update()
        {
            //Do the logic/SQL Query here to update the status of event
            using (EventComboEntities db = new EventComboEntities())
            {
                string strSql = "update ev set ev.EventStatus = 'Expired' FROM Event ev LEFT Join MultipleEvent me on ev.EventID = me.EventID  where me.M_StartTo < getutcdate() AND LOWER(EventStatus)='live'";
                db.Database.ExecuteSqlCommand(strSql);
                db.SaveChanges();

                strSql = "update ev set ev.EventStatus='Expired' FROM Event ev LEFT Join EventVenue me on ev.EventID = me.EventID where me.E_Enddate < getutcdate() AND LOWER(EventStatus)='live'";
                db.Database.ExecuteSqlCommand(strSql);
                db.SaveChanges();

            }
        }

    }
}