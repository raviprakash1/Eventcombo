using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
      private static Logger logger = LogManager.GetCurrentClassLogger();
      
      protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

      protected void Application_Error(Object sender, EventArgs e)
      {
        var raisedException = Server.GetLastError();
        if (raisedException != null)
          logger.Error(raisedException, "Exception occured.");
      }
    }
}
