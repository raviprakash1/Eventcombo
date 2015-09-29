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
        EmsEntities db = new EmsEntities();
        public ActionResult Deleteuser(string userid)
        {
            try {
                //User_Permission_Detail userper = db.User_Permission_Detail.Where(i => i.UP_User_Id.Trim() == userid.Trim()).FirstOrDefault();
                //db.User_Permission_Detail.Remove(userper);
                //db.SaveChanges();
                db.Database.ExecuteSqlCommand("Delete from User_Permission_Detail where UP_User_Id='" + userid + "'");
                Profile prof = db.Profiles.Where(i => i.UserID == userid).FirstOrDefault();
                db.Profiles.Remove(prof);
                db.SaveChanges();

                db.Database.ExecuteSqlCommand("Delete from AspNetUserRoles where UserId='" + userid + "'");


                AspNetUser user = db.AspNetUsers.Find(userid);
                db.AspNetUsers.Remove(user);
                db.SaveChanges();

              
                return Content("Deleted");

            }
            catch(Exception ex)
            {
                return Content(ex.Message);

            }

        }

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