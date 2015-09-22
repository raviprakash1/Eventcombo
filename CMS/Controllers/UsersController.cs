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
            //UsersContext objUCon = new UsersContext();
            //UsersTemplate objUTemp = objUCon.UserTemplates;

            //  List<>
            //DataTable dt = new DataTable();
            //dt.Columns.Add("UserName", typeof(string));
            //dt.Columns.Add("Email", typeof(string));
            //DataRow dr = dt.NewRow();
            //dr["UserName"] = "Satname ";
            //dr["Email"] = "def@gmail.com";
            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["UserName"] = "Ji";
            //dr["Email"] = "def@gmail.com";
            //dt.Rows.Add(dr);


            //ViewData.Model = dt.AsEnumerable();

            //UsersTemplate objUser = new UsersTemplate();
            //  objUser.UserName = "Satnam Waheguru";
            //objUser.EMail = "abc@yahoo.com";


            //List<UsersTemplate> objUserList = new List<UsersTemplate>();


            //objUserList.Add(objUser);
            //objUser = new UsersTemplate();
            //objUser.UserName = "Waheguru ji";
            //objUser.EMail = "cde@yahoo.com";
            //objUserList.Add(objUser);
            //    ViewBag.UserList = objUserList;

            return View(GetAllUsers());
        }
        public List<UsersTemplate> GetAllUsers()
        {

            using (EmsEntities objEntity = new EmsEntities())
            {
                var modelUserTemp = (from UserTemp in objEntity.AspNetUsers
                                    select new UsersTemplate
                                    {
                                        EMail = UserTemp.Email,
                                        UserName = UserTemp.UserName
                                    }
                                    );


                return modelUserTemp.ToList();

            }



        }
    }
}