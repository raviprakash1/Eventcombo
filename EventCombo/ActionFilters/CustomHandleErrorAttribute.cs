using EventCombo.Utils;
using NLog;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventCombo.ActionFilters
{ 
    public class CustomHandleErrorAttribute : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }
            Email.SendErrorReport(
                "email_error",
                filterContext.HttpContext.Request.UrlReferrer == null ? "" : filterContext.HttpContext.Request.UrlReferrer.AbsoluteUri,
                filterContext.HttpContext.Request.Url.AbsoluteUri,
                filterContext.Exception.Message);
        }
    }
}