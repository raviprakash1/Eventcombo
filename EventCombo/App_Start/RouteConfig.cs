using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EventCombo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
           // routes.MapMvcAttributeRoutes();
            routes.LowercaseUrls = true;
            //AreaRegistration.RegisterAllAreas();
            //AreaRegistration.RegisterAllAreas();
            //routes.MapRoute(
            //name: "ViewEvent",
            //url: "CreateEvent",
            //defaults: new
            // {
            //     controller = "CreateEvent",
            //     action = "ViewEvent",
            //    strUrlData = UrlParameter.Optional
            // }
            //);


            routes.MapRoute(
                name: "ViewEvent",
                url: "e/{strEventDs}-{strEventId}",
                defaults: new
                {
                    controller = "ViewEvent",
                    action = "ViewEvent"
                }
            );

            //    routes.MapRoute(
            //    name: "TPayment",
            //    url: "Payment/{strUrl}",
            //    defaults: new
            //    {
            //        controller = "TicketPayment",
            //        action = "TicketPayment"
            //    }
            //);


            routes.MapRoute(
                name: "Default",
                namespaces: new[] { "EventCombo.Controllers" },
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index"}
            );
            //routes.MapRoute("ViewEvent", "CreateEvent/{strUrlData} ", new { controller = "CreateEvent", action = "ViewEvent", strUrlData = UrlParameter.Optional });
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}
