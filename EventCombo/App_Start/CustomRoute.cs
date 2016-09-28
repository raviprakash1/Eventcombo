using EventCombo.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace EventCombo
{
    public class CustomRoute : RouteBase
    {

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var url = httpContext.Request.Headers["HOST"].Split('.');

            if (url.Length < 2)
                return null;

            var subDomain = url[0];
            var domain = url[1];

            string ReserveSubDomains = "," + ConfigurationManager.AppSettings["ReserveSubDomains"] + ",";
            if (ReserveSubDomains.Contains("," + subDomain + ","))
            {
                return null;
            }
            IEventService _eService = new EventService(
                                new DAL.EntityFrameworkUnitOfWorkFactory(new DAL.EventComboContextFactory()),
                                AutomapperConfig.Config.CreateMapper());
            var eventId = _eService.GetEventBySubDomain(subDomain).EventID; ;
            if (eventId > 0)
            {
                var routeData = new RouteData(this, new System.Web.Mvc.MvcRouteHandler());

                routeData.Values.Add("controller", "EventViewCustom");
                routeData.Values.Add("action", "ViewEvent");
                routeData.Values.Add("EventId", eventId);
                routeData.Values.Add("Domain", domain);
                return routeData;
            }

            return null;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}