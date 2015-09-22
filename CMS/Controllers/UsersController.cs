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
            DataTable dt = new DataTable();
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            DataRow dr = dt.NewRow();
            dr["UserName"] = "Satname ";
            dr["Email"] = "def@gmail.com";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["UserName"] = "Ji";
            dr["Email"] = "def@gmail.com";
            dt.Rows.Add(dr);


            ViewData.Model = dt.AsEnumerable();

            UsersTemplate objUser = new UsersTemplate();
            //AspNetUser UserEntity = new AspNetUser();
            objUser.UserName = "Satnam";
            List<UsersTemplate> objUserList = new List<UsersTemplate>();

            objUserList.Add(objUser);
            objUser = new UsersTemplate();
            objUser.UserName = "Waheguru";
            objUserList.Add(objUser);
            ViewBag.UserList = objUserList; 
            return View();
        }
    }
}