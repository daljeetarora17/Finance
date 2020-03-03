using NDFinance.API.Helpers;
using NDFinance.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NDFinance.API.Models;

namespace NDFinance.API.Services
{
    public interface IUserService
    {
        public User Authenticate(string UserName, string Password);
        public List<User> GetAll();
        public bool CreateUser(UserVM userVM);
    }

    public class UserService : IUserService
    {

        public List<User> GetAll()
        {
            using (FinanceDBContext _context = new FinanceDBContext())
            {
                return _context.User.ToList<User>();
            }
        }

        public User Authenticate(string UserName, string Password)
        {

            using (FinanceDBContext _context = new FinanceDBContext())
            {
                return _context.User.FirstOrDefault(x => x.Password == Password && x.UserName == UserName);
            }
        }

        public bool CreateUser(UserVM userVM)
        {
            using (FinanceDBContext _context = new FinanceDBContext())
            {
                User newUser = new User
                {
                    Email = userVM.Email,
                    FirstName = userVM.FirstName,
                    LastName = userVM.LastName,
                    Id = 0,
                    Password = userVM.Password,
                    PhoneNo = userVM.PhoneNo,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    ModifiedDate = DateTime.Now,
                    UserName = userVM.Email
                };

                _context.User.Add(newUser);
                _context.SaveChanges();
                return true;
            }
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
