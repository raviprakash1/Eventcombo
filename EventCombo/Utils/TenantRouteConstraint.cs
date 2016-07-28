using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace EventCombo.Utils
{
    public class TenantRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var fullAddress = httpContext.Request.Headers["Host"].Split('.');
            if (fullAddress.Length < 2)
            {
                return false;
            }
            var tenantSubdomain = fullAddress[0];
            //var tenantId = ... // Lookup tenant id (preferably use a cache) 
            if (!values.ContainsKey("tenant"))
            {
                values.Add("tenant", tenantSubdomain);
            }
            return true;
        }
    }
}