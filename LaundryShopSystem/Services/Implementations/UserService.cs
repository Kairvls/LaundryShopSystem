using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaundryShopSystem.Models;
using LaundryShopSystem.Repositories.Interfaces;
using LaundryShopSystem.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace LaundryShopSystem.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public long CreateUser(User user)
        {
            // Hash the password before saving
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            return _userRepository.CreateUser(user);
        }

        public User Login(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);
            if (user == null)
                return null;

            // Verify hashed password
            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash) ? user : null;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }
    }
}