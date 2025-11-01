using LaundryShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryShopSystem.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        IEnumerable<OrderItem> GetByOrderId(long orderId);
        void Add(OrderItem item);
        void Delete(long id);
    }
}