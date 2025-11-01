using LaundryShopSystem.Models;
using LaundryShopSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LaundryShopSystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var customers = _customerService.GetAllCustomers();
            return View(customers);
        }

        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                long id = _customerService.RegisterCustomer(customer);
                return RedirectToAction("Details", new { id });
            }
            return View(customer);
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            var customer = _customerService.GetAllCustomers();
            return View(customer);
        }
    }
}