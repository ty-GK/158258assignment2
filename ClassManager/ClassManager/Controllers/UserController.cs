using ClassManager.Data;
using ClassManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace ClassManager.Controllers
{
    public class UserController : Controller
    {
        //Registration Action
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        //Registration POST Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(User user)
        {
            bool Status = false;
            string message = "";

            if (ModelState.IsValid)
            {
                //UserName is already Exit
                var isExist = IsUserNameExist(user.UserName);

                if (isExist)
                {
                    message = "Username already exist!";
                }
                else
                {
                    using (ClassManagerContext dc = new ClassManagerContext())
                    {
                        dc.Users.Add(user);
                        dc.SaveChanges();

                        message = "Registration successfully Done. Welcome " + user.UserName;
                        Status = true;
                    }
                }
            }
            else
            {
                message = "Invalid Request";
            }
            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }

        //Login
        [HttpGet]
        public ActionResult Login()
        {
            if(Session["Username"] != null)
            {
                ViewBag.logged = true;
            }
            else
            {
                ViewBag.logged = false;
            }
            return View();
        }

        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login)
        {
            string message = "";

            using (ClassManagerContext dc = new ClassManagerContext())
            {
                var v = dc.Users.Where(a => a.UserName == login.UserName).FirstOrDefault();

                if (v != null && string.Compare(login.Password, v.Password) == 0)
                {
                    int timeout = login.RememberMe ? 525600 : 3600;

                    var ticket = new FormsAuthenticationTicket(login.UserName, login.RememberMe, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = DateTime.Now.AddMinutes(timeout);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);

                    Session["Username"] = login.UserName;

                    return RedirectToAction("Index", "Students");
                }
                else
                {
                    message = "Invalid username or password";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["Username"] = null;
            return RedirectToAction("Login", "User");
        }

        [NonAction]
        private bool IsUserNameExist(string username)
        {
            using (ClassManagerContext dc = new ClassManagerContext())
            {
                var v = dc.Users.Where(a => a.UserName == username).FirstOrDefault();
                return v != null;
            }
        }
    }
}