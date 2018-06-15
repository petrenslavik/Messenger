using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using MessengerServiceData.DbContexts;
using MessengerServiceData.Entities;
using MessengerServices.Core;

namespace MessengerServices.Managers
{
    public class AuthorizationManager:IAuthorizationManager
    {
        private IPasswordHasher _passwordHasher;

        public AuthorizationManager()
        {
            _passwordHasher = new PasswordHasher();
        }

        public string GetUniqueAuthorizationToken(string username, string password, byte[] aesKey)
        {
            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Username == username);

                if (user == null)
                {
                    return null;
                }

                if (!_passwordHasher.VerifyPassword(password, user.PasswordHash))
                {
                    return null;
                }

                return GetUniqueAuthorizationToken(user, aesKey);
            }
        }

        public string GetUniqueAuthorizationToken(User user, byte[] aesKey)
        {
            string guid;

            do
            {
                guid = Guid.NewGuid().ToString();
            } while (MemoryCache.Default.Contains(guid));

            MemoryCache.Default.Set(guid, 
                new Session {AesKey = aesKey, User = user},
                new CacheItemPolicy() {SlidingExpiration = TimeSpan.FromMinutes(10)}
                );

            return guid;
        }

        public Session GetCurrentSession(string token)
        {
            return MemoryCache.Default.Get(token) as Session;
        }
    }
}