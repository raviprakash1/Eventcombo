using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCombo.Models
{
    public class Authenticationclass: FacebookAuthenticationProvider
    {
        public override void ApplyRedirect(FacebookApplyRedirectContext context)
        {
            context.Response.Redirect(context.RedirectUri + "&display=popup");
        }

    }
    public class GoogleAuthentication : GoogleOAuth2AuthenticationProvider
    {
        public override void ApplyRedirect(GoogleOAuth2ApplyRedirectContext context)
        {
            context.Response.Redirect(context.RedirectUri + "&display=popup");
        }

    }
}