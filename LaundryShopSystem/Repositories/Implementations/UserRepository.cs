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
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _db;

        public UserRepository(IDbConnectionFactory db)
        {
            _db = db;
        }

        public long CreateUser(User user)
        {
            using (var conn = _db.CreateConnection())
            {
                string sql = @"INSERT INTO Users (Username, PasswordHash, Role)
                               VALUES (@Username, @PasswordHash, @Role);
                               SELECT LAST_INSERT_ID();";
                return conn.ExecuteScalar<long>(sql, user);
            }
        }

        public User GetByUsername(string username)
        {
            using (var conn = _db.CreateConnection())
            {
                string sql = "SELECT * FROM Users WHERE Username = @Username;";
                return conn.QueryFirstOrDefault<User>(sql, new { Username = username });
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            using (var conn = _db.CreateConnection())
            {
                string sql = "SELECT * FROM Users ORDER BY CreatedAt DESC;";
                return conn.Query<User>(sql).ToList();
            }
        }
    }
}