using DemandTool.App_Start;
using DemandTool.Models;
using DemandTool.MVC.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;


namespace DemandTool.MVC.Controllers
{
    public class AccountController : Controller
    {

        private DefaultDBContext db = new DefaultDBContext();

        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Demands");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

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

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = db.Users.FirstOrDefault(s => s.Email == model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            if (user.Password != model.Password)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            Authenticate(user, false);
            return RedirectToAction("Index", "Demands");
        }

        [HttpPost]

        public ActionResult LogOff()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }

        private List<Claim> GetClaims(User user)
        {
            var userCms = new List<Claim>
            {
                //new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                //new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim (ClaimTypes.Name , user.FullName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            return userCms;
        }
        private void Authenticate(User user, bool rememberMe)
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;

            var identity = new ClaimsIdentity(GetClaims(user), DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignIn
                (new AuthenticationProperties
                {
                    IsPersistent = rememberMe
                }, identity);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Changepassword(UpdatePassword model)
        {
            var id = ((ClaimsIdentity)User.Identity).FindFirstValue(ClaimTypes.NameIdentifier);

          var  user = db.Users.FirstOrDefault(u => u.Id.ToString() == id);
            if (user!= null && user.Password == model.OldPassword)
            {

                user.Password = model.NewPassword;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Demands");
        }

    }
}

