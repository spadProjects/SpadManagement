using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;

namespace SpadManagement.Web
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            //#if DEBUG
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {

                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                CookieName = ".spadTransportation",
                ExpireTimeSpan = TimeSpan.FromHours(1),
                LogoutPath = new PathString("/Account/Logout")
            });
            //#else
            //            app.UseCookieAuthentication(new CookieAuthenticationOptions
            //            {

            //                CookieSecure = CookieSecureOption.Always,
            //                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //                LoginPath = new PathString("/Account/Login"),
            //                CookieName = ".exir.irdoc.approval",
            //                    // Enables the application to validate the security stamp when the user logs in.
            //                    // This is a security feature which is used when you change a password or add an external login to your account.
            //                ExpireTimeSpan = TimeSpan.FromHours(1),
            //                LogoutPath = new PathString("/Account/Logout")
            //            });
            //#endif
            // Enable the application to use a cookie to store information for the signed in user

            // Use a cookie to temporarily store information about a user logging in with a third party login provider

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication();
        }
    }
}