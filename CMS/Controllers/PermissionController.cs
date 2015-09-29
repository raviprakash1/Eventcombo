using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Models;
using CMS.Controllers;
namespace CMS.Controllers
{
    public class PermissionController : Controller
    {
        // GET: Permission
        public ActionResult Index()
        {
            return View();
        }

        public string GetPermission(int iPermissionId)
        {
            string strPer = "N";
            try
            {
                string strUserId = Session["AppId"].ToString();
                EmsEntities objEms = new EmsEntities();
                var vRole = objEms.Database.SqlQuery<string>("Select RoleId from AspNetUserRoles where UserId='" + strUserId + "'").Single();
                if (vRole != null && Convert.ToInt16(vRole) == 1)
                    return "Y";
                
                
                
                //int iGet = 
                var vPer = (from c in objEms.User_Permission_Detail
                            where c.UP_User_Id.Equals(strUserId) && c.UP_Permission_Id.Equals(iPermissionId)
                            select new { c.UP_Id }).SingleOrDefault();

               // vPer = objEms.Database.SqlQuery("SELECT UP_Id FROM User_Permission_Detail WHERE UP_User_Id = '" + strUserId + "' AND UP_Permission_Id = " + iPermissionId)

                if (vPer != null && Convert.ToInt16(vPer) >0) strPer = "Y";

            }
            catch (Exception ex)
            {
                return "N";
            }
            return strPer;
        }

        //<label for="chk_3">Users</label>
        //<label for="chk_4">Events</label>
        //<label for="chk_5">Tickets</label>
        //<label for="chk_6">Emails</label>
        //<label for="chk_7">Messages</label>
    }
}