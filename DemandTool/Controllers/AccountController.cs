using DemandTool.App_Start;
using DemandTool.Models;
using DemandTool.MVC.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemandTool.Controllers
{
    public class AccountController : Controller
    {
        
            
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        //public ActionResult Login(LoginViewModel login )
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //        var authManager = HttpContext.GetOwinContext().Authentication;

        //        ApplicationUser user = userManager.Find(login.Email, login.Password);
        //        if (user != null)
        //        {
        //            var ident = userManager.CreateIdentity(user,
        //                DefaultAuthenticationTypes.ApplicationCookie);
        //            //use the instance that has been created. 
        //            authManager.SignIn(
        //                new AuthenticationProperties { IsPersistent = false }, ident);
        //            return Redirect(login.ReturnUrl ?? Url.Action("Index", "Demands"));
        //        }
        //    }
        //    ModelState.AddModelError("", "Invalid username or password");
        //    return Redirect(Url.Action("Index", "Home"));
        //}


        //public ActionResult Login(LoginViewModel login)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var user = User. 
    //    }
    }
}