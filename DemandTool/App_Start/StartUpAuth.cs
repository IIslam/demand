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
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<RoleManager<ApplicationRole>>((options, context) =>
                new RoleManager<ApplicationRole>(
                    new RoleStore<ApplicationRole>(context.Get<DefaultDBContext>())));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login"),
            });

            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

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
        //public void Configuration(IAppBuilder app)
        //{
        //    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        //    ConfigureAuth(app);
        //}
        //private static bool IsAjaxRequest(IOwinRequest request)
        //{

        //    var headers = request.Headers;
        //    return ((headers != null) && (headers["X-Requested-With"] == "XMLHttpRequest"));
        //}

        //private static Task MyCustomValidateIdentity(CookieValidateIdentityContext context)
        //{
        //    var expireUtc = context.Properties.ExpiresUtc;
        //    var claimType = "myExpireUtc";
        //    var identity = context.Identity;
        //    if (identity.HasClaim(c => c.Type == claimType))
        //    {
        //        var existingClaim = identity.FindFirst(claimType);
        //        identity.RemoveClaim(existingClaim);
        //    }
        //    if (expireUtc != null)
        //    {
        //        var newClaim = new Claim(claimType, expireUtc.Value.UtcTicks.ToString());
        //        context.Identity.AddClaim(newClaim);
        //    }

        //    return Task.FromResult(0);
        //}

    }
}