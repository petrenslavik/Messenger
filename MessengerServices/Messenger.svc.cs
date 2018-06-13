using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using MessengerServiceData.DbContexts;
using MessengerServiceData.Entities;
using MessengerServices.Core;
using MessengerServices.Managers;
using System.Security.Cryptography;
using System.Security.Permissions;
using CommonLibrary.Security;
using Aes = CommonLibrary.Security.Aes;

namespace MessengerServices
{
    public class MessengerService : IMessenger
    {
        private IPasswordHasher _passwordHasher;
        private IRequestHelper _requestHelper;
        private IAuthorizationManager _authorizationManager;
        private Aes _aes;
        private Rsa _rsa;

        public MessengerService()
        {
            _passwordHasher = new PasswordHasher();
            _requestHelper = new RequestHelper();
            _authorizationManager = new AuthorizationManager();
            _aes = new Aes();
            _rsa = new Rsa();
            ImportRsaKey();
            if (_requestHelper.GetCurrentSession() != null)
            {
                _aes.SetAesKey(_requestHelper.GetCurrentSession().AesKey);
            }

            if(MemoryCache.Default.Contains(_requestHelper.GetClientIp()))
            {
                _aes.SetAesKey(MemoryCache.Default.Get(_requestHelper.GetClientIp()) as byte[]);
            }
        }

        public bool FriendUser(int idFirst, int idSecond)
        {
            using (var db = new UserContext())
            {
                var first = db.Users.Find(idFirst);
                var second = db.Users.Find(idSecond);
                var friendship = new Friendship { FirstUser = first, SecondUser = second, Status = 2 };
                db.Friendships.Add(friendship);
                db.SaveChanges();
            }

            return true;
        }

        public bool IsUniqueUsername(string username)
        {
            using (var db = new UserContext())
            {
                var userRes = db.Users.FirstOrDefault(user => user.Username == username);
                return userRes == null;
            }
        }

        public string Login(string username, string password, byte[] iv)
        {
            var arr = _aes.Decrypt(iv, username, password);
            username = arr[0];
            password = arr[1];

            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Username == username);

                if (user == null)
                    throw new Exception("Wrong username or password");

                if (!_passwordHasher.VerifyPassword(password, user.PasswordHash))
                    throw new Exception("Wrong username or password");

                if (!MemoryCache.Default.Contains(_requestHelper.GetClientIp()))
                    throw new Exception("Your session has been expired");

                byte[] aesKey = MemoryCache.Default.Remove(_requestHelper.GetClientIp()) as byte[];
                return _authorizationManager.GetUniqueAuthorizationToken(user, aesKey);
            }
        }

        public void SetEncryptedSessionKey(byte[] encryptedKey)
        {
            var key = _rsa.Decrypt(encryptedKey);

            if (key.Length != 32)
            {
                throw new Exception("Key's length isn't 256 bit");
            }

            MemoryCache.Default.Add(_requestHelper.GetClientIp(), key, DateTime.Now.AddMinutes(5));
        }

        public string RegisterUser(string name, string username, string password, string email, byte[] iv)
        {
            var arr = _aes.Decrypt(iv, name, username, password, email);
            name = arr[0];
            username = arr[1];
            password = arr[2];
            email = arr[3];
            if (!IsUniqueEmailAndUsername(email, username))
                return null;

            using (var db = new UserContext())
            {
                var user = db.Users.Add(new User()
                {
                    Email = email,
                    EmailConfirmed = false,
                    Username = username,
                    PasswordHash = _passwordHasher.HashPassword(password),
                    Name = name
                });

                db.SaveChanges();

                byte[] aesKey = MemoryCache.Default.Remove(_requestHelper.GetClientIp()) as byte[];
                return _authorizationManager.GetUniqueAuthorizationToken(user, aesKey);
            }
        }

        private bool IsUniqueEmailAndUsername(string email, string username)
        {
            using (var db = new UserContext())
            {
                return null == db.Users.FirstOrDefault(x => x.Email == email || x.Username == username);
            }
        }

        private void ImportRsaKey()
        {
            _rsa.SetKey(ConfigurationManager.AppSettings["RsaXmlPrivateKey"]);
        }
    }
}
