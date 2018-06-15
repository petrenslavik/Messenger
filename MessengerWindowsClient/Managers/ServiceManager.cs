using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CommonLibrary;
using CommonLibrary.Security;
using MessengerWindowsClient.Models;
using MessengerWindowsClient.ServiceReference1;
using Aes = CommonLibrary.Security.Aes;

namespace MessengerWindowsClient.Managers
{
    public class ServiceManager
    {
        public static string UniqueToken { get; private set; }

        private MessengerClient _client;
        private Rsa _rsa;
        private Aes _aes;

        public ServiceManager()
        {
            _aes = new Aes();
            _rsa = new Rsa();

            _rsa.SetKey(ConfigurationManager.AppSettings["RsaXmlPublicKey"]);

            byte[] key;
            var encryptedKey = _rsa.GenerateNewAes256EncryptedKey(out key);

            _client = new MessengerClient("httpEndPoint1");
            _client.SetEncryptedSessionKey(encryptedKey);
            _aes.SetAesKey(key);
        }

        public async Task<bool> RegisterUser(string name, string username, string password, string email)
        {
            var iv = _aes.GenerateNewIv();

            _aes.Encrypt(ref name);
            _aes.Encrypt(ref username);
            _aes.Encrypt(ref password);
            _aes.Encrypt(ref email);

            var token = await _client.RegisterUserAsync(name, username, password, email, iv);
            UniqueToken = token;

            return true;
        }

        public async Task<bool> Login(string username, string password)
        {
            var iv = _aes.GenerateNewIv();

            _aes.Encrypt(ref username);
            _aes.Encrypt(ref password);

            var token = await _client.LoginAsync(username, password, iv);
            UniqueToken = token;

            await _client.IsUniqueUsernameAsync(username);
            return true;
        }

        public async Task<List<MessageDTO>> GetConversations()
        {
            return (await _client.GetAllMessagesAsync()).ToList();
        }

        public async Task<string> WriteMessage(string content, string receiverId)
        {
            var iv = _aes.GenerateNewIv();

            _aes.Encrypt(ref content);
            _aes.Encrypt(ref receiverId);

            return await _client.WriteMessageAsync(receiverId, content, iv);
        }

        public async Task<List<UserDTO>> GetPossibleUsers(string str)
        {
            return (await _client.GetPossibleUsersAsync(str)).ToList();
        }

        public async Task<List<MessageDTO>> GetNewMessages(DateTime date)
        {
            return (await _client.GetNewMessagesAsync(date)).ToList();
        }

        public void Dispose()
        {
            _client.Close();
        }
    }
}
