using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Models;
using System.Data;
using System.Text;

namespace CMS.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Users()
        {
           


            List<UsersTemplate> objuser = GetAllUsers();
           // List<Permissions> objPerm = GetPermission("APP");
            UsersTemplate objU = new UsersTemplate();
            objU.objPermissions = GetPermission("APP");
            objuser.Add(objU);
            return View(objuser);
        }
        public List<UsersTemplate> GetAllUsers()
        {
            using (EmsEntities objEntity = new EmsEntities())
            {
                var modelUserTemp = (from UserTemp in objEntity.AspNetUsers
                                     join Pr in objEntity.Profiles on UserTemp.Id equals Pr.UserID
                                     select new UsersTemplate
                                     {
                                         EMail = UserTemp.Email,
                                         UserName = UserTemp.UserName,
                                         Id = UserTemp.Id,
                                         Organiser = Pr.Organiser.Trim(),
                                         Merchant = Pr.Merchant.Trim(),
                                         UserStatus = Pr.UserStatus.Trim()
                                     }
                                    );
                return modelUserTemp.ToList();
            }
        }




        public ActionResult Permission()
        {
            return View(GetPermission("APP"));
        }

        public List<Permissions> GetPermission(string strCategory)
        {

            using (EmsEntities objEntity = new EmsEntities())
            {
                var modelPerm = (from Perm in objEntity.Permission_Detail
                                 where Perm.Permission_Category.Equals(strCategory)
                                 select new Permissions
                                 {
                                     Permission_Id = Perm.Permission_Id,
                                     Permission_Desc = Perm.Permission_Desc,
                                     Permission_Category = Perm.Permission_Category
                                 }
                                    );


               return  modelPerm.ToList();

            }

        }

        public string SavePermisions(string strUserId,string strPermission, string strRole)
        {
            string strResult = "";
            try
            {


                string[] strAry = strPermission.Split('#');
                EmsEntities objEnt = new EmsEntities();
                User_Permission_Detail upd = new User_Permission_Detail();
                foreach (string str in strAry)
                {
                    if (!str.Trim().Equals(string.Empty))
                    {
                        upd = new User_Permission_Detail();
                        upd.UP_Permission_Id = Convert.ToInt16(str);
                        upd.UP_User_Id = strUserId;
                        objEnt.User_Permission_Detail.Add(upd);
                    }
                }
                string strRoleId = objEnt.GetSetUserRole(strUserId, "SET", strRole).Single();
                objEnt.User_Permission_Detail.RemoveRange(objEnt.User_Permission_Detail.Where(x => x.UP_User_Id == strUserId));
                objEnt.SaveChanges();


                //var vRoleId =objEnt.Database.SqlQuery<int>("Select RoleId from AspNetUserRoles where UserId='" + strUserId + "'").Single();
                //var vRoleId = objEnt.Database.SqlQuery("GetSetUserRole",new { "@user_Id: " });



                strResult = "Y";
            }
            catch (Exception ex)
            {
                strResult = "N";
            }
            return strResult;
        }


        public string SaveOtherInfo(string strField, string strvalue,string strUserId)
        {
            string strResult = "";
            try
            {
                EmsEntities objEnt = new EmsEntities();
                objEnt.Database.ExecuteSqlCommand("Update Profile set " + strField +  " = '" + strvalue + "' Where UserId = '" + strUserId + "'");
                strResult = "Y";
            }
            catch (Exception ex)
            {
                strResult = "N";
            }
            return strResult;
        }
        public string GetUserPermission(string strUserId)
        {
            StringBuilder strResult = new StringBuilder();
            try
            {
                EmsEntities objEms = new EmsEntities();
                List<User_Permission_Detail> objUpList = objEms.User_Permission_Detail.Where(x => x.UP_User_Id == strUserId).ToList();
                foreach(User_Permission_Detail upd in objUpList)
                    strResult.Append(upd.UP_Permission_Id.ToString() + "^");

                string strRoleId = objEms.GetSetUserRole(strUserId, "GET", "").Single();

                strResult.Append(strRoleId);
            }
            catch (Exception ex)
            {
                strResult.Clear();
                strResult.Append("Error");
            }
            return strResult.ToString();
        }
    }
}