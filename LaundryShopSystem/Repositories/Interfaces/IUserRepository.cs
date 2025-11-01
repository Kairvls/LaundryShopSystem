using LaundryShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryShopSystem.Repositories.Interfaces
{
    public interface IUserRepository
    {
        long CreateUser(User user);
        User GetByUsername(string username);
        IEnumerable<User> GetAllUsers();
    }
}