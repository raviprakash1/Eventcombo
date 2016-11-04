using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EventCombo.Service;

namespace EventCombo
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
      HangfireBootstrapper.Instance.Start();
      #if DEBUG
            BundleTable.EnableOptimizations = false;
      #else
            BundleTable.EnableOptimizations = true;
      #endif
    }

    protected void Application_End(object sender, EventArgs e)
    {
      HangfireBootstrapper.Instance.Stop();
    }

    protected void Application_Error(Object sender, EventArgs e)
    {
      var raisedException = Server.GetLastError();
      if (raisedException != null)
      {
        logger.Error(raisedException, "Exception occured.");
        if (raisedException is HttpException)
        {
          var httpException = (HttpException)raisedException;
          Response.StatusCode = httpException.GetHttpCode();
        }
        else
          Response.StatusCode = 500;
      }
    }
  }
}
