using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Aes = CommonLibrary.Security.Aes;

namespace MessengerWindowsClient
{
    public class ServiceManager
    {
        private MessengerService.MessengerClient _client;
        private RSACryptoServiceProvider _rsa;
        private Aes aes;

        public ServiceManager()
        {
            aes = new Aes();
            _rsa = new RSACryptoServiceProvider(2376);
            _client = new MessengerService.MessengerClient();
            var parameters = _rsa.ExportParameters(false);
            var encryptedkey = _client.GetEncryptedSessionKey(parameters.Exponent, parameters.Modulus);
            var key = _rsa.Decrypt(encryptedkey, false);
            aes.SetAesKey(key);
        }

        public Task<bool> RegisterUser(string name, string username, string password, string email)
        {
            var iv = aes.GenerateNewIv();
            aes.Encrypt(ref name);
            aes.Encrypt(ref username);
            aes.Encrypt(ref password);
            aes.Encrypt(ref email);
            aes.Decrypt(iv, name, username, password, email);
            return _client.RegisterUserAsync(name, username, password, email, iv);
        }

        public void Dispose()
        {
            _client.Close();
        }
    }
}
