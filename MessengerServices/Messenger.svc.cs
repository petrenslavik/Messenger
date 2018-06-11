using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
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
using Aes = CommonLibrary.Security.Aes;

namespace MessengerServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class MessengerService : IMessenger
    {
        private IPasswordHasher _passwordHasher;
        private IRequestHelper _requestHelper;
        private Aes _aes;

        public MessengerService()
        {
            _passwordHasher = new PasswordHasher();
            _requestHelper = new RequestHelper();
            _aes = new Aes();
            _aes.SetAesKey(SessionManager.GetKeyForCurrentClient());
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

        public bool Login(string username, string password, byte[] iv)
        {
            _aes.Decrypt(iv, username, password);
            password = _passwordHasher.HashPassword(password);
            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Username == username && x.PasswordHash == password);
                if (user == null)
                    return false;
                db.Sessions.Add(new Session()
                {
                    Holder = user,
                    Ip = _requestHelper.GetClientIp(),
                    Key = SessionManager.PopKeyForCurrentClient()
                });
                return true;
            }
        }

        public byte[] GetEncryptedSessionKey(byte[] exponent, byte[] modulus)
        {
            var rsa = new RSACryptoServiceProvider(2376);
            rsa.ImportParameters(new RSAParameters()
            {
                Exponent = exponent,
                Modulus = modulus
            });

            using (var rng = new RNGCryptoServiceProvider())
            {
                var sessionKey = new byte[32];
                rng.GetBytes(sessionKey);
                var encryptedKey = rsa.Encrypt(sessionKey, false);
                SessionManager.AddKeyForCurrentClient(sessionKey);
                return encryptedKey;
            }
        }

        public bool RegisterUser(string name, string username, string password, string email, byte[] iv)
        {
            var arr = _aes.Decrypt(iv, name, username, password, email);
            name = arr[0];
            username = arr[1];
            password = arr[2];
            email = arr[3];
            if (!IsUniqueEmailAndUsername(email, username))
                return false;

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
                var session = db.Sessions.Add(new Session()
                {
                    Holder = user,
                    Ip = _requestHelper.GetClientIp(),
                    Key = SessionManager.PopKeyForCurrentClient(),
                });
                db.SaveChanges();
            }

            return true;
        }

        private bool IsUniqueEmailAndUsername(string email, string username)
        {
            using (var db = new UserContext())
            {
                return null == db.Users.FirstOrDefault(x => x.Email == email || x.Username == username);
            }
        }
    }
}
