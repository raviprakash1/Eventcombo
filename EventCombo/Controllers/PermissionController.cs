using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
namespace EventCombo.Controllers
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
                string strUserId = (Session["AppId"] != null ? Session["AppId"].ToString() : string.Empty);
                EventComboEntities objEms = new EventComboEntities();
                var vRole = objEms.Database.SqlQuery<string>("Select RoleId from AspNetUserRoles where UserId='" + strUserId + "'").Single();
                if (vRole != null)
                {
                    if (Convert.ToInt16(vRole) == 1  || Convert.ToInt16(vRole) == 2)
                        return "Y";
                }


                var vPer = (from c in objEms.User_Permission_Detail
                            where c.UP_User_Id.Equals(strUserId) && c.UP_Permission_Id.Equals(iPermissionId)
                            select new { c.UP_Id }).SingleOrDefault();

                if (vPer != null && vPer.UP_Id > 0) strPer = "Y";
            }
            catch (Exception ex)
            {
                return "N";
            }
            return strPer;
        }


        public string IsSuperAdmin()
        {
            string strPer = "N";
            try
            {
                string strUserId = (Session["AppId"] != null ? Session["AppId"].ToString() : string.Empty);
                EventComboEntities objEms = new EventComboEntities();
                var vRole = objEms.Database.SqlQuery<string>("Select RoleId from AspNetUserRoles where UserId='" + strUserId + "'").Single();
                if (vRole != null && Convert.ToInt16(vRole) == 1)
                    return "Y";
            }
            catch (Exception ex)
            {
                return "N";
            }
            return strPer;
        }

        public int GetUserRole(string strUserId)
        {
            int iURole = 0;
            try
            {
                EventComboEntities objEms = new EventComboEntities();
                var vRole = objEms.Database.SqlQuery<string>("Select RoleId from AspNetUserRoles where UserId='" + strUserId + "'").Single();
                if (vRole != null)
                    iURole = Convert.ToInt16(vRole);
            }
            catch (Exception ex)
            {
                return 0;
            }
            return iURole;
        }

        public int CurrentUserRole()
        {
            int iURole = 0;
            try
            {
                string strUserId = (Session["AppId"] != null ? Session["AppId"].ToString() : string.Empty);
                if (!strUserId.Equals(string.Empty))
                {
                    EventComboEntities objEms = new EventComboEntities();
                    var vRole = objEms.Database.SqlQuery<string>("Select RoleId from AspNetUserRoles where UserId='" + strUserId + "'").Single();
                    if (vRole != null)
                        iURole = Convert.ToInt16(vRole);
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return iURole;
        }
    }
}