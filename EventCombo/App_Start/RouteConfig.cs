﻿using System;
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
            //routes.MapMvcAttributeRoutes();
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


            //routes.MapRoute(
            //    name: "ViewEvent",
            //    url: "Event/{strUrlData}",
            //    defaults: new
            //    {
            //        controller = "ViewEvent",
            //        action = "ViewEvent"
            //    }
            //);

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
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            //routes.MapRoute("ViewEvent", "CreateEvent/{strUrlData} ", new { controller = "CreateEvent", action = "ViewEvent", strUrlData = UrlParameter.Optional });
        }
    }
}
