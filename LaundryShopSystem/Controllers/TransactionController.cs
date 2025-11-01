using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LaundryShopSystem.Models;
using LaundryShopSystem.Services.Interfaces;
using QRCoder;
using System.IO;

namespace LaundryShopSystem.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IOrderService _orderService;

        public TransactionController(ITransactionService transactionService, IOrderService orderService)
        {
            _transactionService = transactionService;
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult Add(long orderId)
        {
            ViewBag.Order = _orderService.GetOrderById(orderId);
            return View();
        }

        [HttpPost]
        public ActionResult Add(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                // Save transaction first
                _transactionService.RecordPayment(transaction);

                // Update order status to Completed
                _orderService.UpdateOrderStatus(transaction.OrderId, "Completed");

                // ✅ Generate QR Code with transaction details
                string qrData = $"Transaction ID: {transaction.TransactionId}\n" +
                                $"Order ID: {transaction.OrderId}\n" +
                                $"Amount Paid: ₱{transaction.AmountPaid}\n" +
                                $"Date: {DateTime.Now:yyyy-MM-dd HH:mm}";

                using (var qrGenerator = new QRCodeGenerator())
                using (var qrCodeData = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q))
                using (var qrCode = new PngByteQRCode(qrCodeData))
                {
                    var qrBytes = qrCode.GetGraphic(20);
                    string base64Qr = Convert.ToBase64String(qrBytes);
                    transaction.QRCodeUrl = $"data:image/png;base64,{base64Qr}";
                }

                // ✅ Optional: Update transaction record with QR code URL (if supported in service)
                _transactionService.UpdateQRCode(transaction.TransactionId, transaction.QRCodeUrl);

                // Redirect to receipt view after payment
                return RedirectToAction("Receipt", new { id = transaction.TransactionId });
            }

            return View(transaction);
        }

        // ✅ Receipt View to Display QR
        [HttpGet]
        public ActionResult Receipt(long id)
        {
            var transaction = _transactionService.GetTransactionById(id);
            if (transaction == null)
                return HttpNotFound();

            return View(transaction);
        }
    }
}