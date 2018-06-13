using System;
using System.Configuration;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CommonLibrary.Security;
using MessengerWindowsClient.MessengerService;
using Aes = CommonLibrary.Security.Aes;

namespace MessengerWindowsClient.Managers
{
    public class ServiceManager
    {
        public static string UniqueToken { get; private set; }

        private MessengerClient _client;
        private Rsa _rsa;
        private Aes _aes;

        private bool EasyCertCheck(object sender, X509Certificate cert,
            X509Chain chain, System.Net.Security.SslPolicyErrors error)
        {
            return true;
        }

        public ServiceManager()
        {
            ServicePointManager.ServerCertificateValidationCallback += EasyCertCheck;
            _aes = new Aes();
            _rsa = new Rsa();

            //_rsa.SetKey(new RSAParameters()
            //{
            //    Modulus = Convert.FromBase64String(ConfigurationManager.AppSettings["RsaModulus"]),
            //    Exponent = Convert.FromBase64String(ConfigurationManager.AppSettings["RsaExponent"]),
            //});

            _rsa.SetKey(ConfigurationManager.AppSettings["RsaXmlPublicKey"]);

            byte[] key;
            var encryptedKey = _rsa.GenerateNewAes256EncryptedKey(out key);

            _client = new MessengerClient("httpEndPoint");
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

        public void Dispose()
        {
            _client.Close();
        }
    }
}
