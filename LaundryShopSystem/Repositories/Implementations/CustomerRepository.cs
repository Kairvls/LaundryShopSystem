using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaundryShopSystem.Models;
using LaundryShopSystem.Repositories.Interfaces;
using LaundryShopSystem.Data;
using Dapper;

namespace LaundryShopSystem.Repositories.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnectionFactory _db;

        public CustomerRepository(IDbConnectionFactory db) => _db = db;

        public long AddCustomer(Customer customer)
        {
            using (var conn = _db.CreateConnection())
            {
                var sql = @"INSERT INTO Customers (FullName, Phone, Email, Address)
                            VALUES (@FullName, @Phone, @Email, @Address);
                            SELECT LAST_INSERT_ID();";
                return conn.ExecuteScalar<long>(sql, customer);
            }
        }

        public Customer GetCustomerById(long id)
        {
            using (var conn = _db.CreateConnection())
                return conn.QuerySingleOrDefault<Customer>("SELECT * FROM Customers WHERE CustomerId = @id", new { id });
        }

        public Customer GetCustomerByPhone(string phone)
        {
            using (var conn = _db.CreateConnection())
                return conn.QuerySingleOrDefault<Customer>("SELECT * FROM Customers WHERE Phone = @phone", new { phone });
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            using (var conn = _db.CreateConnection())
                return conn.Query<Customer>("SELECT * FROM Customers ORDER BY CreatedAt DESC");
        }
    }
}