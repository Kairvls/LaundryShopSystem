using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LaundryShopSystem.Models;
using LaundryShopSystem.Services.Interfaces;

namespace LaundryShopSystem.Controllers
{
    public class IncidentController : Controller
    {
        private readonly IIncidentService _incidentService;
        private readonly IOrderService _orderService;

        public IncidentController(IIncidentService incidentService, IOrderService orderService)
        {
            _incidentService = incidentService;
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult Report(long orderId)
        {
            ViewBag.Order = _orderService.GetOrderById(orderId);
            return View();
        }

        [HttpPost]
        public ActionResult Report(Incident incident)
        {
            if (ModelState.IsValid)
            {
                _incidentService.ReportIncident(incident);
                return RedirectToAction("Index", "Orders");
            }
            return View(incident);
        }

        [HttpPost]
        public ActionResult Resolve(long id)
        {
            _incidentService.ResolveIncident(id, true);
            return RedirectToAction("Index", "Orders");
        }
    }
}