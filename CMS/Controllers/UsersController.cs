using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Models;
using System.Data;
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
                                     select new UsersTemplate
                                     {
                                         EMail = UserTemp.Email,
                                         UserName = UserTemp.UserName,
                                         Id = UserTemp.Id
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

        public void SavePermisions(string strUserId,string strPermission, string strRole)
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
                objEnt.SaveChanges();
                strResult = "Y";
            }
            catch (Exception)
            {
                strResult = "N";
            }
        }
    }
}