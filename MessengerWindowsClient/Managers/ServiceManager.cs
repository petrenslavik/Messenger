using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using CommonLibrary;
using CommonLibrary.Security;
using MessengerWindowsClient.Models;
using MessengerWindowsClient.MessengerService;
using Aes = CommonLibrary.Security.Aes;

namespace MessengerWindowsClient.Managers
{
    public class ServiceManager
    {
        public static string UniqueToken { get; private set; }
        public bool IsConnected { get; set; }
        public static Aes Aes { get; private set; }

        private MessengerClient _client;
        private Rsa _rsa;
        private Aes _aes;
        private DispatcherTimer _timer;

        public ServiceManager()
        {
            _aes = new Aes();
            _rsa = new Rsa();
            _rsa.SetKey(ConfigurationManager.AppSettings["RsaXmlPublicKey"]);
            var _timer = new DispatcherTimer();
            _timer.Tick += Reconnect;
            _timer.Interval = TimeSpan.FromSeconds(2);
            _client = new MessengerClient();
            Connect();
        }

        private void Reconnect(object sender, EventArgs e)
        {
            Connect();
            if(IsConnected)
                _timer.Stop();
        }

        private void Connect()
        {
            try
            {
                byte[] key;
                var encryptedKey = _rsa.GenerateNewAes256EncryptedKey(out key);
                _client.SetEncryptedSessionKey(encryptedKey);
                _aes.SetAesKey(key);
                Aes = _aes;
                IsConnected = true;
            }
            catch
            {
                return;
            }
        }

        public async Task<bool> RegisterUser(string name, string username, string password, string email)
        {
            try
            {
                var token = await _client.RegisterUserAsync(name, username, password, email);
                if (token == null)
                    return false;
                UniqueToken = token;
                return true;
            }
            catch
            {
                IsConnected = false;
                _timer.Start();
            }

            return false;
        }

        public async Task<bool> Login(string username, string password)
        {
            try
            {
                var token = await _client.LoginAsync(username, password);
                if (token == null)
                    return false;
                UniqueToken = token;
                return true;
            }
            catch
            {
                IsConnected = false;
                _timer.Start();
            }

            return false;
        }

        public async Task<MessageDTO[]> GetConversations()
        {
            try
            {
                var messages = await _client.GetAllMessagesAsync();
                return messages;
            }
            catch
            {
                IsConnected = false;
                _timer.Start();
            }

            return null;
        }

        public async Task<string> WriteMessage(string content, string receiverId)
        {
            try
            {
                return await _client.WriteMessageAsync(receiverId, content);
            }
            catch
            {
                IsConnected = false;
                _timer.Start();
            }

            return null;
        }

        public async Task<List<UserDTO>> GetPossibleUsers(string str)
        {
            try
            {
                return _client.GetPossibleUsers(str).ToList();
            }
            catch
            {
                IsConnected = false;
                _timer.Start();
            }

            return null;
        }

        public async Task<List<MessageDTO>> GetNewMessages(DateTime date)
        {
            try
            {
                return (_client.GetNewMessages(date)).ToList();
            }
            catch
            {
                IsConnected = false;
                _timer.Start();
            }

            return null;
        }

        public bool IsValidUsername(string username)
        {
            try
            {
                return _client.IsUniqueUsername(username);
            }
            catch
            {
                IsConnected = false;
                _timer.Start();
            }

            return false;
        }

        public void Dispose()
        {
            _client.Close();
        }
    }
}
