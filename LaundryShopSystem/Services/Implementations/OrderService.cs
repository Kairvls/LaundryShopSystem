using LaundryShopSystem.ViewModels;
using LaundryShopSystem.Models;
using LaundryShopSystem.Repositories.Interfaces;
using LaundryShopSystem.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using QRCoder;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace LaundryShopSystem.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;

        public OrderService(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }



        public long CreateOrder(Order order, IEnumerable<OrderItem> items)
        {
            // Compute total and default values
            order.TotalAmount = items.Sum(i => i.Subtotal);
            order.Status = "Pending";
            order.QRCode = ""; // will be filled after creation

            // Step 1: Save order
            var orderId = _orderRepo.AddOrder(order);

            // Step 2: Save items with OrderId
            foreach (var item in items)
                item.OrderId = orderId;
            _orderRepo.AddOrderItems(items);

            // Step 3: Generate QR code (use unique info)
            string qrContent = $"Order-{orderId}"; // could also be order.OrderCode if you have one
            var qrImagePath = GenerateQRCode(qrContent);

            // Step 4: Update DB with QR path
            _orderRepo.UpdateOrderQRCode(orderId, qrImagePath);

            return orderId;
        }

        /// <summary>
        /// Generates a QR code image for the given text and returns the relative path.
        /// </summary>
        private string GenerateQRCode(string text)
        {
            // Physical folder path (inside wwwroot)
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "qrcodes");

            // Ensure folder exists
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // Build file path
            var fileName = $"{text}.png";
            var filePath = Path.Combine(folderPath, fileName);

            // Generate and save QR image
            using (var qrGen = new QRCodeGenerator())
            using (var qrData = qrGen.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q))
            using (var qrCode = new QRCode(qrData))
            using (var bitmap = qrCode.GetGraphic(20))
            {
                bitmap.Save(filePath, ImageFormat.Png);
            }

            // Return relative web path for <img src="">
            return $"/qrcodes/{fileName}";
        }

        public void UpdateOrderStatus(long orderId, string status)
        {
            _orderRepo.UpdateStatus(orderId, status);
        }

        public Order GetOrderByQRCode(string qrCode)
        {
            return _orderRepo.GetOrderByQRCode(qrCode);
        }

        public Order GetOrderById(long orderId)
        {
            return _orderRepo.GetOrderById(orderId);
        }

        public IEnumerable<Order> GetOrdersByStatus(string status)
        {
            return _orderRepo.GetOrdersByStatus(status);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orderRepo.GetAllOrders();
        }
    }
}
