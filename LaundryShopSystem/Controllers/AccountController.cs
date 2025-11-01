using System;
using System.Web.Mvc;
using LaundryShopSystem.Services.Interfaces;
using LaundryShopSystem.Services.Implementations;
using LaundryShopSystem.Repositories.Interfaces;
using LaundryShopSystem.Repositories.Implementations;
using LaundryShopSystem.Models;
using LaundryShopSystem.Data;

namespace LaundryShopSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        // Parameterless constructor for MVC
        public AccountController()
        {
            IDbConnectionFactory dbFactory = new DbConnectionFactory("server=localhost;database=laundry_shop_db;uid=root;pwd=kaisuan;");
            IUserRepository userRepository = new UserRepository(dbFactory);
            _userService = new UserService(userRepository);
        }

        // Constructor for future DI (optional)
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: /Account/Login
        public ActionResult Login() => View();

        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = _userService.Login(username, password);
            if (user != null)
            {
                // Store user info in session
                Session["UserId"] = user.UserId;
                Session["Role"] = user.Role;

                // Redirect based on role or fixed dashboard
                return RedirectToAction("Dashboard", "Admin");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        // GET: /Account/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
