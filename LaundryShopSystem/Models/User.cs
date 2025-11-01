using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryShopSystem.Models
{
    public class User
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // Owner or Staff
        public DateTime CreatedAt { get; set; }
    }
}