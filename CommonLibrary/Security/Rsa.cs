using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Security
{
    public class Rsa
    {
        private RSACryptoServiceProvider _rsa;

        public Rsa()
        {
            _rsa = new RSACryptoServiceProvider(2048);
        }

        public void SetKey(RSAParameters parameters)
        {
            _rsa.ImportParameters(parameters);
        }

        public byte[] GenerateNewAes256EncryptedKey(out byte[] key)
        {
            key = new byte[32];
            using (var gen = new RNGCryptoServiceProvider())
            {
                gen.GetBytes(key);
                var encryptedKey = _rsa.Encrypt(key, false);
                return encryptedKey;
            }
        }

        public void SetKey(string xmlKey)
        {
            _rsa.FromXmlString(xmlKey);
        }

        public byte[] Encrypt(byte[] data)
        {
            return _rsa.Encrypt(data, false);
        }

        public byte[] Decrypt(byte[] data)
        {
            return _rsa.Decrypt(data, false);
        }
    }
}
