using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using MessengerServices.Core;

namespace MessengerServices.Managers
{
    public class PasswordHasher : IPasswordHasher
    {

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return string.Concat(hashValue.Select(item => item.ToString("x2")));
            }
        }

        public bool VerifyPassword(string password, string hash)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var passwordHash = string.Concat(hashValue.Select(item => item.ToString("x2")));
                return hash == passwordHash;
            }
        }
    }
}