using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public class CustomAuthorization : AuthorizeAttribute
{
    EmsEntities context = new EmsEntities();
    private readonly string[] allowedroles;
    public CustomAuthorization(params string[] roles)
    {
        this.allowedroles = roles;
    }
    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        bool authorize = false;
        string UserId = (httpContext.Session["UserID"] != null ? httpContext.Session["UserID"].ToString() : string.Empty);

        foreach (var role in allowedroles)
        {
            var vRole = context.Database.SqlQuery<string>("Select RoleId from AspNetUserRoles where UserId='" + UserId + "'").Single();
            if (vRole != null && Convert.ToInt16(vRole) == 1)
            {
                authorize = true;
            }
            else
            {
                var vPer = (from c in context.User_Permission_Detail
                            where c.UP_User_Id.Equals(UserId) && c.UP_Permission_Id.Equals(role)
                            select new { c.UP_Id }).SingleOrDefault();
                if (vPer != null && Convert.ToInt16(vPer.UP_Id) > 0)
                    authorize = true;
            }
        }
        return authorize;
    }
    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Permission", action = "AccessDenied" }));
    }
}