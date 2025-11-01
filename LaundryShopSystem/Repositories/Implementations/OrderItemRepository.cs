using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using LaundryShopSystem.Data;
using LaundryShopSystem.Models;
using LaundryShopSystem.Repositories.Interfaces;

namespace LaundryShopSystem.Repositories.Implementations
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public OrderItemRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<OrderItem> GetByOrderId(long orderId)
        {
            using (var conn = _connectionFactory.CreateConnection())
            {
                conn.Open();
                string query = "SELECT * FROM OrderItems WHERE OrderId = @OrderId";
                return conn.Query<OrderItem>(query, new { OrderId = orderId }).ToList();
            }
        }

        public void Add(OrderItem item)
        {
            using (var conn = _connectionFactory.CreateConnection())
            {
                conn.Open();
                string query = @"
                    INSERT INTO OrderItems 
                    (OrderId, ItemType, FabricOrCareNotes, Quantity, UnitType, ServiceType, PricePerUnit, Subtotal)
                    VALUES 
                    (@OrderId, @ItemType, @FabricOrCareNotes, @Quantity, @UnitType, @ServiceType, @PricePerUnit, @Subtotal)";

                conn.Execute(query, item);
            }
        }

        public void Delete(long id)
        {
            using (var conn = _connectionFactory.CreateConnection())
            {
                conn.Open();
                string query = "DELETE FROM OrderItems WHERE OrderItemId = @Id";
                conn.Execute(query, new { Id = id });
            }
        }
    }
}