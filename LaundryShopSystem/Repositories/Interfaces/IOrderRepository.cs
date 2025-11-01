using LaundryShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryShopSystem.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        long AddOrder(Order order);
        void UpdateOrderQRCode(long orderId, string qrPath);
        void AddOrderItems(IEnumerable<OrderItem> items);
        Order GetOrderById(long orderId);
        Order GetOrderByQRCode(string qrCode);
        IEnumerable<Order> GetOrdersByStatus(string status);
        void UpdateStatus(long orderId, string status);
        IEnumerable<Order> GetAllOrders();
    }
}