using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using LaundryShopSystem.Models;
using LaundryShopSystem.Repositories.Interfaces;
using LaundryShopSystem.Data;

namespace LaundryShopSystem.Repositories.Implementations
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IDbConnectionFactory _db;
        public TransactionRepository(IDbConnectionFactory db) => _db = db;

        public long AddTransaction(Transaction transaction)
        {
            using (var conn = _db.CreateConnection())
            {
                var sql = @"INSERT INTO Transactions (OrderId, AmountPaid, PaymentMethod, PaidAt)
                            VALUES (@OrderId, @AmountPaid, @PaymentMethod, NOW());
                            SELECT LAST_INSERT_ID();";
                return conn.ExecuteScalar<long>(sql, transaction);
            }
        }

        public IEnumerable<Transaction> GetTransactionsByOrder(long orderId)
        {
            using (var conn = _db.CreateConnection())
                return conn.Query<Transaction>("SELECT * FROM Transactions WHERE OrderId = @oid", new { oid = orderId });
        }

        // Get a single transaction by Id
        public Transaction GetTransactionById(long transactionId)
        {
            using (var conn = _db.CreateConnection())
                return conn.QueryFirstOrDefault<Transaction>("SELECT * FROM Transactions WHERE Id = @id", new { id = transactionId });
        }

        // Update transaction (e.g., QRCodeUrl or other fields)
        public void UpdateTransaction(Transaction transaction)
        {
            using (var conn = _db.CreateConnection())
            {
                var sql = @"UPDATE Transactions 
                            SET OrderId = @OrderId, AmountPaid = @AmountPaid, PaymentMethod = @PaymentMethod, QRCodeUrl = @QRCodeUrl
                            WHERE Id = @Id";
                conn.Execute(sql, transaction);
            }
        }
    }
}
