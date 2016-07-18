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
        private static Logger logger = LogManager.GetCurrentClassLogger();

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

            logger.Trace(filterContext.Exception, "Exception occured.");
        }
    }
}