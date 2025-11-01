using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using LaundryShopSystem.Models;

namespace LaundryShopSystem.Services.Interfaces
{
    public interface IOrderService
    {
        long CreateOrder(Order order, IEnumerable<OrderItem> items);
        void UpdateOrderStatus(long orderId, string status);
        Order GetOrderByQRCode(string qrCode);
        Order GetOrderById(long orderId);
        IEnumerable<Order> GetOrdersByStatus(string status);
        IEnumerable<Order> GetAllOrders();
    }
}