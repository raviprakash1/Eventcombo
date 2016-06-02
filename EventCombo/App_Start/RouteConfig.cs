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

            routes.MapRoute(
            "EventViewCustom",
                "ev/{strCustomUrl}",
                new { controller = "EventViewCustom", action = "Index", strCustomUrl = "" }
                );

            routes.MapRoute(
                name: "EvType",
                namespaces: new[] { "EventCombo.Controllers" },
                url: "et/{strEt}/{strEc}/{strPrice}/{strPageIndex}/{strLat}/{strLong}/{strSort}/{strDateFilter}/{strTextSearch}",
                defaults: new { controller = "Home", action = "DiscoverEvents",strEt = UrlParameter.Optional, strEc = UrlParameter.Optional, strPrice= UrlParameter.Optional, strPageIndex = UrlParameter.Optional , strLat = UrlParameter.Optional, strLong = UrlParameter.Optional, strSort = UrlParameter.Optional, strDateFilter = UrlParameter.Optional, strTextSearch = UrlParameter.Optional }
            );

            routes.MapRoute(
                "ShowArticle",
                "a/{articleId}/{articleTitle}",
                new { controller = "Article", action = "ShowArticle", articleId = "", articleTitle = ""}
              );


            routes.MapRoute(
                name: "Default",
                namespaces: new[] { "EventCombo.Controllers" },
                url: "{controller}/{action}",
                defaults: new { Controller = "Home", action = "Index"}
            );
            //routes.MapRoute("ViewEvent", "CreateEvent/{strUrlData} ", new { controller = "CreateEvent", action = "ViewEvent", strUrlData = UrlParameter.Optional });
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}
