using LaundryShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryShopSystem.Services.Interfaces
{
    public interface IUserService
    {
        long CreateUser(User user);
        User Login(string username, string password);
        IEnumerable<User> GetAllUsers();
    }
}