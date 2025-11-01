using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using LaundryShopSystem.Models;
using LaundryShopSystem.Repositories.Interfaces;
using LaundryShopSystem.Data;

namespace LaundryShopSystem.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbConnectionFactory _db;
        public OrderRepository(IDbConnectionFactory db) => _db = db;

        public long AddOrder(Order order)
        {
            using (var conn = _db.CreateConnection())
            {
                var sql = @"
                    INSERT INTO Orders 
                    (CustomerId, ServiceMode, Status, TotalAmount, QRCode, DropoffAt, Notes, CreatedAt)
                    VALUES (@CustomerId, @ServiceMode, @Status, @TotalAmount, @QRCode, @DropoffAt, @Notes, NOW());
                    SELECT LAST_INSERT_ID();";
                return conn.ExecuteScalar<long>(sql, order);
            }
        }

        public void AddOrderItems(IEnumerable<OrderItem> items)
        {
            using (var conn = _db.CreateConnection())
            {
                var sql = @"
                    INSERT INTO OrderItems 
                    (OrderId, ItemType, FabricOrCareNotes, Quantity, UnitType, ServiceType, PricePerUnit, Subtotal)
                    VALUES (@OrderId, @ItemType, @FabricOrCareNotes, @Quantity, @UnitType, @ServiceType, @PricePerUnit, @Subtotal)";
                conn.Execute(sql, items);
            }
        }

        public void UpdateOrderQRCode(long orderId, string qrPath)
        {
            using (var conn = _db.CreateConnection())
            {
                var sql = "UPDATE Orders SET QRCode = @qr, UpdatedAt = NOW() WHERE OrderId = @id";
                conn.Execute(sql, new { qr = qrPath, id = orderId });
            }
        }

        public void UpdateStatus(long orderId, string status)
        {
            using (var conn = _db.CreateConnection())
            {
                var sql = "UPDATE Orders SET Status=@status, UpdatedAt=NOW() WHERE OrderId=@id";
                conn.Execute(sql, new { status, id = orderId });

                // Automatically tag ReadyAt time if completed
                if (status == "ReadyForPickup" || status == "Completed")
                {
                    conn.Execute("UPDATE Orders SET ReadyAt = NOW() WHERE OrderId = @id", new { id = orderId });
                }
            }
        }

        public Order GetOrderById(long orderId)
        {
            using (var conn = _db.CreateConnection())
            {
                var order = conn.QuerySingleOrDefault<Order>("SELECT * FROM Orders WHERE OrderId = @id", new { id = orderId });
                if (order != null)
                {
                    order.OrderItems = conn.Query<OrderItem>(
                        "SELECT * FROM OrderItems WHERE OrderId = @oid", new { oid = orderId }).ToList();
                }
                return order;
            }
        }

        public Order GetOrderByQRCode(string qrCode)
        {
            using (var conn = _db.CreateConnection())
            {
                // Fetch using QRCode field
                var order = conn.QuerySingleOrDefault<Order>(
                    "SELECT * FROM Orders WHERE QRCode = @qr", new { qr = qrCode });

                if (order != null)
                {
                    order.OrderItems = conn.Query<OrderItem>(
                        "SELECT * FROM OrderItems WHERE OrderId = @oid", new { oid = order.OrderId }).ToList();
                }
                return order;
            }
        }

        public IEnumerable<Order> GetOrdersByStatus(string status)
        {
            using (var conn = _db.CreateConnection())
            {
                return conn.Query<Order>(
                    "SELECT * FROM Orders WHERE Status = @status ORDER BY DropoffAt DESC", new { status });
            }
        }

        public IEnumerable<Order> GetAllOrders()
        {
            using (var conn = _db.CreateConnection())
            {
                return conn.Query<Order>("SELECT * FROM Orders ORDER BY CreatedAt DESC");
            }
        }
    }
}
