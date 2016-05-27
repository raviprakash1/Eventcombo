using EventCombo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Services
{
    public class LastLoginStatus
    {

        public void changeloginstatus()
        {
            using (EventComboEntities db = new EventComboEntities())
            {
                string strSql = "update AspNetUsers set LoginStatus='N' WHERE (DATEDIFF(MINUTE,LastLoginTime ,getutcdate()) > 45 and (LoginStatus='Y' Or LoginStatus='y')) OR LastLoginTime is null";
                db.Database.ExecuteSqlCommand(strSql);
                db.SaveChanges();
            }
        }
    }
}