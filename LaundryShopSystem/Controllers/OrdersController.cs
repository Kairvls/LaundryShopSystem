using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LaundryShopSystem.Data;
using LaundryShopSystem.Models;
using LaundryShopSystem.Repositories.Implementations;
using LaundryShopSystem.Services.Implementations;

namespace LaundryShopSystem.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderService _orderService;
        private readonly CustomerService _customerService;

        // Parameterless constructor
        public OrdersController()
        {
            // Create DB connection factory
            var connectionString = "server=localhost;database=laundry_shop_db;uid=root;pwd=kaisuan;"; // adjust your connection string
            var dbFactory = new DbConnectionFactory(connectionString);

            // Create repositories
            var orderRepo = new OrderRepository(dbFactory);
            var customerRepo = new CustomerRepository(dbFactory);

            // Create services
            _orderService = new OrderService(orderRepo);
            _customerService = new CustomerService(customerRepo);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var orders = _orderService.GetAllOrders();
            return View(orders);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string fullName, string phone, string serviceMode, List<OrderItem> items)
        {
            var customer = _customerService.GetCustomerByPhone(phone);
            if (customer == null)
            {
                customer = new Customer
                {
                    FullName = fullName,
                    Phone = phone,
                    CreatedAt = DateTime.Now
                };
                customer.CustomerId = _customerService.RegisterCustomer(customer);
            }

            var order = new Order
            {
                CustomerId = customer.CustomerId,
                ServiceMode = serviceMode,
                Status = "Queued",
                CreatedAt = DateTime.Now
            };

            var orderId = _orderService.CreateOrder(order, items);
            return RedirectToAction("Receipt", new { id = orderId });
        }

        [HttpGet]
        public ActionResult Receipt(long id)
        {
            var order = _orderService.GetOrderById(id);
            return View(order);
        }

        [HttpGet]
        public ActionResult Track(string qr)
        {
            var order = _orderService.GetOrderByQRCode(qr);
            if (order == null)
                return HttpNotFound();

            return View(order);
        }

        [HttpPost]
        public ActionResult UpdateStatus(long orderId, string status)
        {
            _orderService.UpdateOrderStatus(orderId, status);
            return RedirectToAction("Index");
        }
    }
}
