using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LaundryShopSystem.Data;
using LaundryShopSystem.Repositories.Implementations;
using LaundryShopSystem.Services.Implementations;
using LaundryShopSystem.Services.Interfaces;

namespace LaundryShopSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IReportService _reportService;

        public AdminController()
        {
            // Your DB connection string
            var connectionString = "server=localhost;database=laundry_shop_db;uid=root;pwd=kaisuan;";
            var dbFactory = new DbConnectionFactory(connectionString);

            // Create repositories
            var orderRepo = new OrderRepository(dbFactory);
            var reportRepo = new ReportRepository(dbFactory);

            // Create services
            _orderService = new OrderService(orderRepo);
            _reportService = new ReportService(reportRepo);
        }

        public ActionResult Dashboard()
        {
            var stats = _reportService.GetDashboardStats();
            return View(stats);
        }

        public ActionResult Reports()
        {
            var reports = _reportService.GetMonthlyReport();
            return View(reports);
        }
    }
}