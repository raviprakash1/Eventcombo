using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using Owin.Security.Providers.LinkedIn;
using EventCombo.Models;
using System.Configuration;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Net.Http;

namespace EventCombo
{
  public partial class Startup
  {
    const string XmlSchemaString = "http://www.w3.org/2001/XMLSchema#string";
    // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
    public void ConfigureAuth(IAppBuilder app)
    {
      // Configure the db context, user manager and signin manager to use a single instance per request
      app.CreatePerOwinContext(ApplicationDbContext.Create);
      app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
      app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

      // Enable the application to use a cookie to store information for the signed in user
      // and to use a cookie to temporarily store information about a user logging in with a third party login provider
      // Configure the sign in cookie
      app.UseCookieAuthentication(new CookieAuthenticationOptions
      {
        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        LoginPath = new PathString("/Home/Index"),
        Provider = new CookieAuthenticationProvider
        {
          // Enables the application to validate the security stamp when the user logs in.
          // This is a security feature which is used when you change a password or add an external login to your account.  
          OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
              validateInterval: TimeSpan.FromMinutes(30),
              regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
        }
      });
      app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

      // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
      app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

      // Enables the application to remember the second login verification factor such as phone or email.
      // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
      // This is similar to the RememberMe option when you log in.
      app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

      // Uncomment the following lines to enable logging in with third party login providers
      //app.UseMicrosoftAccountAuthentication(
      //    clientId: "",
      //    clientSecret: "");

      //app.UseTwitterAuthentication(
      //   consumerKey: "",
      //   consumerSecret: "");

      if (ConfigurationManager.AppSettings.Get("FacebookAppId").Length > 0)
      {
        var facebookOptions = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions()
        {
          AppId = ConfigurationManager.AppSettings.Get("FacebookAppId"),
          AppSecret = ConfigurationManager.AppSettings.Get("FacebookAppSecret"),
          BackchannelHttpHandler = new FacebookBackChannelHandler(),
          UserInformationEndpoint = "https://graph.facebook.com/v2.6/me?fields=id,name,email,first_name,last_name",
          Provider = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationProvider()
          {
            OnAuthenticated = (context) =>
            {
              context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:access_token", context.AccessToken, XmlSchemaString, "Facebook"));
              foreach (var claim in context.User)
              {
                var claimType = string.Format("urn:facebook:{0}", claim.Key);
                string claimValue = claim.Value.ToString();
                if (!context.Identity.HasClaim(claimType, claimValue))
                  context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, XmlSchemaString, "Facebook"));
              }
              return Task.FromResult(0);
            }
          }
        };
        facebookOptions.Scope.Add("email");
        app.UseFacebookAuthentication(facebookOptions);
      }

      var googleOptions = new GoogleOAuth2AuthenticationOptions()
      {
        ClientId = ConfigurationManager.AppSettings.Get("GoogleID"),
        ClientSecret = ConfigurationManager.AppSettings.Get("GoogleSecret"),
      };
      googleOptions.Scope.Add("profile");
      googleOptions.Scope.Add("https://www.googleapis.com/auth/plus.login");
      googleOptions.Scope.Add("https://www.googleapis.com/auth/userinfo.email");
      googleOptions.Provider = new GoogleOAuth2AuthenticationProvider()
      {
        OnAuthenticated = (context) =>
        {
          string claimType; bool bAddClaim = false;
          foreach (var claim in context.User)
          {

            claimType = string.Empty;
            bAddClaim = false;
            switch (claim.Key)
            {
              case "given_name":
                claimType = "FirstName";
                bAddClaim = true;
                break;
              case "family_name":
                claimType = "LastName";
                bAddClaim = true;
                break;
              case "gender":
                claimType = "gender";
                bAddClaim = true;
                break;
            }

            if (bAddClaim)
            {
              string claimValue = claim.Value.ToString();
              if (!context.Identity.HasClaim(claimType, claimValue))
                context.Identity.AddClaim(new Claim(claimType, claimValue, "XmlSchemaString", "Google"));
            }
          }
          return Task.FromResult(0);
        }
      };
      app.UseGoogleAuthentication(googleOptions);


      if (ConfigurationManager.AppSettings.Get("LinkedInAppId").Length > 0)
      {
        var linkedInOptions = new LinkedInAuthenticationOptions()
        {
          ClientId = ConfigurationManager.AppSettings.Get("LinkedInAppId"),
          ClientSecret = ConfigurationManager.AppSettings.Get("LinkedInAppSecret"),
          Provider = new LinkedInAuthenticationProvider()
          {
            OnAuthenticated = (context) =>
            {
              context.Identity.AddClaim(new System.Security.Claims.Claim("urn:linkedin:access_token", context.AccessToken, XmlSchemaString, "LinkedIn"));
              context.Identity.AddClaim(new System.Security.Claims.Claim("urn:linkedin:firstname", context.GivenName, XmlSchemaString, "LinkedIn"));
              context.Identity.AddClaim(new System.Security.Claims.Claim("urn:linkedin:lastname", context.FamilyName, XmlSchemaString, "LinkedIn"));
              context.Identity.AddClaim(new System.Security.Claims.Claim("urn:linkedin:email", context.Email, XmlSchemaString, "LinkedIn"));
              return Task.FromResult(0);
            }
          }

        };
        app.UseLinkedInAuthentication(linkedInOptions);
      }
    }
  }

  public class FacebookBackChannelHandler : HttpClientHandler
  {
    protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
    {
      // Replace the RequestUri so it's not malformed
      if (!request.RequestUri.AbsolutePath.Contains("/oauth"))
      {
        request.RequestUri = new Uri(request.RequestUri.AbsoluteUri.Replace("?access_token", "&access_token"));
      }

      return await base.SendAsync(request, cancellationToken);
    }
  }
}