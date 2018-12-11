using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using DemandTool.App_Start;
using DemandTool.Models;
using DemandTool.MVC.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
//[assembly: OwinStartupAttribute(typeof(DemandTool.App_Start.Startup))]

namespace DemandTool
{
    public class StartupAuth
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new DefaultDBContext());
           
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login"),
            });

            
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                SlidingExpiration = true,
                ExpireTimeSpan = new TimeSpan(0, 300, 0),
                CookieName = "DemandToolCookie",
                LoginPath = new PathString("/Account/Login"),
                //Provider = new CookieAuthenticationProvider
                //{
                //    OnResponseSignedIn = ctx =>
                //    {
                //        ctx.Options.SlidingExpiration = true;
                //        ctx.Options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                //    },
                //    OnApplyRedirect = ctx =>
                //    {
                //        if (IsAjaxRequest(ctx.Request))
                //            return;
                //        ctx.Response.Redirect(HttpUtility.HtmlDecode($"{ctx.RedirectUri}"));
                //    },
                //    OnValidateIdentity = MyCustomValidateIdentity
                //}
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
        }
       
    }
}