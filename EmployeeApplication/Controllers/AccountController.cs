using EmployeeApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeApplication.Controllers
{
    public class AccountController : Controller
    {
        public static List<AuthUser> a = new List<AuthUser>() { new AuthUser() { FirstName="Admin",LastName="Admin",Email="Admin@gmail.com",Password="Admin",MobileNumber=9898989898} };
        public ViewResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(AuthUser authUser)
        {
            if (authUser != null)
            {
                a.Add(authUser);
                Session["User"] = authUser;
                return RedirectToAction("Login", "Account");
            }
            else {
                ViewBag.Email = authUser.Email;
                return RedirectToAction("Failure", "User");
            }
        }
        public ViewResult Login()
        {
            ViewBag.Name = "Rahul Kondi";
            return View();
        }
        public ViewResult LogOut()
        {
            ViewBag.Name = "Rahul Kondi";
            Session["products"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Validate(AuthUser authUser)
        {
            var email = a.Where(e => e.Email == authUser.Email).FirstOrDefault().Email;
            var password = a.Where(e => e.Password == authUser.Password).FirstOrDefault().Password;
            if (authUser.Email == email && authUser.Password == password)
            {
                ViewBag.Email = authUser.Email;
                return RedirectToAction("Index", "User");
            }
            else
            {
                ViewBag.Email = authUser.Email;
                return RedirectToAction("Failure", "User");
            }
        }
    }
}