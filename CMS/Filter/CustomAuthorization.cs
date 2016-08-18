using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public class CustomAuthorization : AuthorizeAttribute
{
    EmsEntities context = new EmsEntities();
    private readonly string[] AllowedPermissionIds;
    public CustomAuthorization(params string[] PermissionIds)
    {
        this.AllowedPermissionIds = PermissionIds;
    }
    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        bool authorize = false;
        string UserId = (httpContext.Session["UserID"] != null ? httpContext.Session["UserID"].ToString() : string.Empty);

        foreach (var PermissionId in AllowedPermissionIds)
        {
            var vRole = context.Database.SqlQuery<string>("Select RoleId from AspNetUserRoles where UserId=@UserId", new System.Data.SqlClient.SqlParameter("@UserId", UserId)).SingleOrDefault();
            if (vRole != null && Convert.ToInt32(vRole) == 1)
            {
                authorize = true;
            }
            else
            {
                var vPer = (from c in context.User_Permission_Detail
                            where c.UP_User_Id == UserId && c.UP_Permission_Id.ToString() == PermissionId
                            select c).SingleOrDefault();
                if (vPer != null && vPer.UP_Id > 0)
                    authorize = true;
            }
        }
        return authorize;
    }
    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        if (filterContext.HttpContext.Session["UserID"] == null || filterContext.HttpContext.Session["UserID"].ToString() == string.Empty)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
        else
        {
            filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Permission", action = "AccessDenied" }));
        }
    }
}