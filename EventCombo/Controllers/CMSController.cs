using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCombo.Models;
namespace EventCombo.Controllers
{
    public class CMSController : Controller
    {
        // GET: CMS
        //public async System.Threading.Tasks.Task<ActionResult> Index(string e)
        //{
        //    EventComboEntities db = new EventComboEntities();
        //    Session["ReturnUrl"] = Url.Action("EventList", "EventList");
        //    LoginViewModel obj = new LoginViewModel();
        //    var userEmail = db.AspNetUsers.Where(x => x.Id == e).Select(y => y.Email).SingleOrDefault();
        //    obj.Email = userEmail;
        //    obj.Password = "";
        //    AccountController objAC = new AccountController();
        //    objAC.ControllerContext = new ControllerContext(this.Request.RequestContext, objAC);
        //    Session["AppId"] = e;
        //    //return RedirectToAction("Login", "Account", new { model = obj, returnUrl = "" });
        //    return await objAC.Login(obj, "Admin");
        //}

        public async System.Threading.Tasks.Task<ActionResult> Index(string e, string id)
        {
            EventComboEntities db = new EventComboEntities();
            if (id != null && id.Trim() != string.Empty)
                Session["ReturnUrl"] = Url.Action("ModifyEvent", "EditEvent", new { Eventid = id });
            else
                Session["ReturnUrl"] = Url.Action("EventList", "EventList");

            LoginViewModel obj = new LoginViewModel();
            var userEmail = db.AspNetUsers.Where(x => x.Id == e).Select(y => y.Email).SingleOrDefault();
            obj.Email = userEmail;
            obj.Password = "";
            AccountController objAC = new AccountController();
            objAC.ControllerContext = new ControllerContext(this.Request.RequestContext, objAC);
            Session["AppId"] = e;
            //return RedirectToAction("Login", "Account", new { model = obj, returnUrl = "" });
            return await objAC.Login(obj, "Admin");
        }


    }
}