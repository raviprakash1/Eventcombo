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