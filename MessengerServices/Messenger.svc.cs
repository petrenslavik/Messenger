using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Caching;
using MessengerServiceData.DbContexts;
using MessengerServiceData.Entities;
using MessengerServices.Core;
using MessengerServices.Managers;
using CommonLibrary;
using CommonLibrary.Security;
using Aes = CommonLibrary.Security.Aes;

namespace MessengerServices
{
    public class MessengerService : IMessenger
    {
        private IPasswordHasher _passwordHasher;
        private IRequestHelper _requestHelper;
        private IAuthorizationManager _authorizationManager;
        private Rsa _rsa;
        private Session _session;

        public MessengerService()
        {
            _passwordHasher = new PasswordHasher();
            _requestHelper = new RequestHelper();
            _authorizationManager = new AuthorizationManager();
            _rsa = new Rsa();
            _session = _requestHelper.GetCurrentSession();
            ImportRsaKey();
        }

        public List<UserDTO> GetPossibleUsers(string str)
        {
            using (var db = new UserContext())
            {
                return db.Users.Where(x => x.Username.StartsWith(str)).ToList()
                    .Select(x => new UserDTO() {UserId = x.Id.ToString(), Username = x.Username, Name = x.Name})
                    .ToList();
            }
        }

        public List<MessageDTO> GetNewMessages(DateTime updateDate)
        {
            if (_session == null)
                return null;

            using (var db = new UserContext())
            {
                var user = db.Users.First(x => x.Username == _session.User.Username);
                var messages = db.Messages.Where(x => x.ReceiverId == user.Id && x.SendDate > updateDate).ToList();

                return messages.Select(x=>new MessageDTO()
                {
                    Content = x.MessageText,
                    Id = x.Id.ToString(),
                    IsAuthor = false,
                    Name = x.Sender.Name,
                    SendDate = x.SendDate,
                    UserId = x.SenderId.ToString(),
                    Username = x.Sender.Username,
                }).ToList();
            }
        }

        public List<MessageDTO> GetAllMessages()
        {
            if (_session == null)
                return null;

            using (var db = new UserContext())
            {
                var user = db.Users.First(x => x.Username == _session.User.Username);
                var messages = new List<MessageDTO>();

                if (user.SentMessages.Count>0)
                {
                    messages.AddRange(user.SentMessages.Select(x => new MessageDTO()
                    {
                        Content = x.MessageText,
                        Id = x.Id.ToString(),
                        IsAuthor = true,
                        Name = x.Receiver.Name,
                        Username = x.Receiver.Username,
                        UserId = x.Receiver.Id.ToString(),
                        SendDate = x.SendDate,
                    }));
                }

                if (user.ReceivedMessages.Count > 0)
                {
                    messages.AddRange(user.ReceivedMessages.Select(x => new MessageDTO()
                    {
                        Content = x.MessageText,
                        Id = x.Id.ToString(),
                        IsAuthor = false,
                        Name = x.Sender.Name,
                        Username = x.Sender.Username,
                        UserId = x.Sender.Id.ToString(),
                        SendDate = x.SendDate,
                    }));
                }

                return messages;
            }
        }

        public string WriteMessage(string receiverId, string content)
        {
            if (_session == null)
                return null;

            int id = int.Parse(receiverId);

            using (var db = new UserContext())
            {
                var receiver = db.Users.FirstOrDefault(x => x.Id == id);
                var user = db.Users.First(x => x.Username == _session.User.Username);

                if (receiver == null)
                    return null;

                var message = db.Messages.Add(new Message()
                {
                    Sender = user,
                    MessageText = content,
                    Receiver = receiver,
                    SendDate = DateTime.Now,
                });

                db.SaveChanges();

                return message.Id.ToString();
            }
        }

        public bool IsUniqueUsername(string username)
        {
            using (var db = new UserContext())
            {
                var userRes = db.Users.FirstOrDefault(user => user.Username == username);
                return userRes == null;
            }
        }

        public string Login(string username, string password)
        {
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

        public string RegisterUser(string name, string username, string password, string email)
        {
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
