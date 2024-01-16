using System.Security.Cryptography;
using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.Design;
using MediaLibrary.WebApp.Common;
using MediaLibrary.WebApp.Common.Helpers.Contracts;
namespace MediaLibrary.WebApp.Common.Helpers
{
    public class Helpers : IHelpers
    {
        private static readonly byte[] Salt = Encoding.ASCII.GetBytes("AgenteSalt");
        private const int Iterations = 1000; // Puede ser ajustado

        private readonly IConfiguration _configuration;
        private readonly byte[] _encryptionKey;
        private readonly byte[] _salt;
        private readonly int _iterations;

        public Helpers(IConfiguration configuration)
        {
            _configuration = configuration;
            //var base64Key = _configuration["Encryption:Key"]
            var base64Key = _configuration["Encryption:Key"]
                .Replace('_', '/')
                .Replace('-', '+');

            _encryptionKey = Convert.FromBase64String(base64Key);
            _salt = Encoding.UTF8.GetBytes("AgenteSalt");
            _iterations = 1000; // Número de iteraciones para PBKDF2
        }
        public string Encrypt(string plainText, string salt = null)
        {
            if (string.IsNullOrEmpty(plainText))
                return null;

            byte[] saltBytes = _salt;
            if (!string.IsNullOrEmpty(salt))
            {
                saltBytes = Encoding.UTF8.GetBytes(salt);
            }

            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(_encryptionKey, saltBytes, _iterations))
            {
                byte[] key = rfc2898DeriveBytes.GetBytes(32); // 256 bits para la clave
                byte[] iv = rfc2898DeriveBytes.GetBytes(16);  // 128 bits para el IV

                using (var aes = Aes.Create())
                {
                    aes.KeySize = 256;
                    aes.BlockSize = 128;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = key;
                    aes.GenerateIV(); // Generar un IV aleatorio
                    iv = aes.IV;


                    using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    using (var ms = new MemoryStream())
                    using (var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                        cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                        cryptoStream.FlushFinalBlock();

                        byte[] encrypted = ms.ToArray();
                        // El IV debe ser enviado junto con el texto cifrado
                        byte[] encryptedAndIv = new byte[iv.Length + encrypted.Length];
                        Buffer.BlockCopy(iv, 0, encryptedAndIv, 0, iv.Length);
                        Buffer.BlockCopy(encrypted, 0, encryptedAndIv, iv.Length, encrypted.Length);


                        return Convert.ToBase64String(encryptedAndIv);
                    }
                }
            }
        }

        public string? Decrypt(string cipherText, string? salt = null)
        {
            try
            {
                byte[] saltAux = Salt;
                if (salt != null)
                    saltAux = Encoding.UTF8.GetBytes(salt);

                if (string.IsNullOrEmpty(cipherText))
                    return null;

                var aes = new RijndaelManaged
                {
                    KeySize = 256,
                    BlockSize = 128,
                    Padding = PaddingMode.PKCS7
                };

                var pdb = new Rfc2898DeriveBytes(_encryptionKey, saltAux, Iterations);
                aes.Key = pdb.GetBytes(aes.KeySize / 8);

                var bytes = Convert.FromBase64String(cipherText);

                // Extract the IV from the beginning of the encrypted data
                byte[] iv = new byte[aes.BlockSize / 8];
                Buffer.BlockCopy(bytes, 0, iv, 0, iv.Length);
                aes.IV = iv;

                // Decrypt the remaining part of the data
                using var ms = new MemoryStream(bytes, iv.Length, bytes.Length - iv.Length);
                using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
                using var sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while decrypting: {e.Message}");
                return null;
            }
        }
    }


}
