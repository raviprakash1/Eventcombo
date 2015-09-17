using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using EventCombo.Models;
using System.Configuration;
using System.Threading.Tasks;
using System.Security.Claims;

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
                LoginPath = new PathString("/Account/Login"),
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

            app.UseTwitterAuthentication(
                  consumerKey: "TbXK9kFhcutcM68oszSRdhM1G",
                  consumerSecret: "ykRwaPib1lIbt2MSG936292OBRXvZwbUMJ6B7gSPspQkelvPsB");



            if (ConfigurationManager.AppSettings.Get("FacebookAppId").Length > 0)
            {
                var facebookOptions = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions()
                {
                    AppId = ConfigurationManager.AppSettings.Get("FacebookAppId"),
                    AppSecret = ConfigurationManager.AppSettings.Get("FacebookAppSecret"),
                    Provider = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationProvider()
                    {
                        OnAuthenticated = (context) =>
                        {
                            context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:access_token", context.AccessToken, XmlSchemaString, "Facebook"));
                            //context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:email", context.Email, XmlSchemaString, "Facebook"));
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
                Provider = new GoogleOAuth2AuthenticationProvider()
                {
                    OnAuthenticated = context =>
                    {
                        var userDetail = context.User;
                        context.Identity.AddClaim(new Claim(ClaimTypes.Name, context.Identity.FindFirstValue(ClaimTypes.Name)));
                        context.Identity.AddClaim(new Claim(ClaimTypes.Email, context.Identity.FindFirstValue(ClaimTypes.Email)));

                        var gender = userDetail.Value<string>("gender");
                        context.Identity.AddClaim(new Claim(ClaimTypes.Gender, gender));

                        //var picture = userDetail.Value<string>("picture");
                        //context.Identity.AddClaim(new Claim("picture", picture));

                        return Task.FromResult(0);
                    },
                },
            };
            googleOptions.Scope.Add("https://www.googleapis.com/auth/plus.login");
            googleOptions.Scope.Add("https://www.googleapis.com/auth/userinfo.email");
            app.UseGoogleAuthentication(googleOptions);
            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "626183259569-cl5n7nrdlnapj9a6g4u2ish8fakr41fo.apps.googleusercontent.com",
            //    ClientSecret = "BACAx6NA6qVHJ-4nywYEBW7-"
            //});
        }
    }
}