using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LaundryShopSystem.Models;
using LaundryShopSystem.Services.Interfaces;

namespace LaundryShopSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = _userService.Login(username, password);
            if (user != null)
            {
                Session["User"] = user;
                return RedirectToAction("Index", "Orders");
            }
            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }

}