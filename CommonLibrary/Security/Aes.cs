﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Security
{
    public class Aes
    {
        private AesCryptoServiceProvider _aes = new AesCryptoServiceProvider();

        public Aes()
        {
            _aes.Padding = PaddingMode.PKCS7;
        }

        public Aes(int keySize, CipherMode mode)
        {
            _aes.KeySize = keySize;
            _aes.Mode = mode;
        }

        public byte[] GenerateNewIv()
        {
            _aes.GenerateIV();
            return _aes.IV;
        }

        public string GetKey()
        {
            return Convert.ToBase64String(_aes.Key);
        }

        public void SetAesKey(byte[] key)
        {
            if (key == null || key.Length == 0)
                return;
            _aes.Key = key;
        }

        public void SetAesIv(byte[] iv)
        {
            if (iv == null || iv.Length == 0)
                return;
            _aes.IV = iv;
        }

        public string Encrypt(string methodParameter)
        {
            if (_aes.Key == null)
                throw new Exception("Key is null");
            using (var encryptor = _aes.CreateEncryptor())
            {
                using (var targetStream = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(targetStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (var streamWriter = new StreamWriter(csEncrypt) { AutoFlush = true })
                        {
                            streamWriter.Write(methodParameter);
                            csEncrypt.FlushFinalBlock();
                        }
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        return Convert.ToBase64String(targetStream.ToArray());
                    }
                }
            }
        }

        public void Encrypt(ref string methodParameter)
        {
            if (_aes.Key == null)
                throw new Exception("Key is null");
            using (var encryptor = _aes.CreateEncryptor())
            {
                using (var targetStream = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(targetStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (var streamWriter = new StreamWriter(csEncrypt))
                        {
                            streamWriter.Write(methodParameter);
                            streamWriter.Flush();
                            csEncrypt.FlushFinalBlock();
                        }

                        methodParameter = Convert.ToBase64String(targetStream.ToArray());
                    }
                }
            }
        }

        public string Decrypt(string methodParameters)
        {
            if (_aes.Key == null)
                throw new Exception("Key is null");

            using (var decryptor = _aes.CreateDecryptor())
            {
                using (var encryptedStream = new MemoryStream(Convert.FromBase64String(methodParameters)))
                {
                    using (var csEncrypt = new CryptoStream(encryptedStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (var streamReader = new StreamReader(csEncrypt))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public string[] Decrypt(byte[] aesIv, params string[] methodParameters)
        {
            if (_aes.Key == null)
                throw new Exception("Key is null");
            _aes.IV = aesIv;
            for (int i = 0; i < methodParameters.Length; i++)
            {
                using (var decryptor = _aes.CreateDecryptor())
                {
                    using (var encryptedStream = new MemoryStream(Convert.FromBase64String(methodParameters[i])))
                    {
                        using (var csEncrypt = new CryptoStream(encryptedStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (var streamReader = new StreamReader(csEncrypt))
                            {
                                methodParameters[i] = streamReader.ReadToEnd();
                            }
                        }
                    }
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            return methodParameters;
        }
    }
}
