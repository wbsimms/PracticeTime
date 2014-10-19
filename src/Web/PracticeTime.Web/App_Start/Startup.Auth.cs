using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;

namespace PracticeTime.Web
{
    [ExcludeFromCodeCoverage]
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //Uncomment the following lines to enable logging in with third party login providers
            app.UseMicrosoftAccountAuthentication(
                clientId: "0000000048105430 ",
                clientSecret: "/8rj8oj4eFmGDyKew/65oDg67L3xWRbl ");

            app.UseTwitterAuthentication(
               consumerKey: "NcBhP7nE3SjMQHcRhjBQ",
               consumerSecret: "YYZqrESCgp35fPwAGqiNN2XybJuR9mpsA7PwPlRgZEg");

            app.UseFacebookAuthentication(
               appId: "227198527467491",
               appSecret: "3265c528e316ae4f405ecee44e3bae6d");

//            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions());
        }
    }
}