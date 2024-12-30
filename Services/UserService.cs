using System;
using System.Linq;
using AuthApplication.Models;

namespace AuthApplication.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public User RegisterUser(User user)
        {
            user.Password = PasswordHasher.HashPassword(user.Password);
            user.IsActive = false; // Default inactive until activation
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public bool VerifyPassword(string email, string password)
        {
            var user = GetUserByEmail(email);
            if (user == null) return false;

            return PasswordHasher.VerifyPassword(password, user.Password);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public bool ActivateUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null) return false;

            user.IsActive = true;
            _context.SaveChanges();
            return true;
        }

        public bool UpdateLastLogin(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null) return false;

            user.LastLogin = DateTime.Now;
            _context.SaveChanges();
            return true;
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public User UpdateUserInformation(User updatedUser)
        {
            var user = _context.Users.Find(updatedUser.Id);
            if (user == null) return null;

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Address = updatedUser.Address;
            user.BirthDate = updatedUser.BirthDate;

            _context.SaveChanges();
            return user;
        }
    }
}
