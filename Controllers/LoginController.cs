using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHLSystem1.Models;

namespace DHLSystem1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public List<UserModel> Putvalue()
        {
            var user = new List<UserModel>
            {
                new UserModel{Id = 1, username="Fatima",password="246812" },
                new UserModel{Id = 2, username="Iman", password = "221200"}

            };

            return user;
        }

        [HttpPost]
        public ActionResult Verify(UserModel usr)
        {
            var us = Putvalue();

            var u = us.Where(um => um.username.Equals(usr.username));
            var up = u.Where(p => p.password.Equals(usr.password));

            if (up.Count() == 1)
            {
                ViewBag.message = "Login Success";
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.message = "Login Failed. Please try again.";
                return View("Login");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            System.Web.Security.FormsAuthentication.SignOut();
            this.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.Response.Cache.SetNoStore();

            return RedirectToAction("Login", "Login");
        }
    }
}