using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace LibraryGPT
{
    public static class Security
    {
        public static class SecureCredentialManager
        {
            public static string EncryptString(string input)
            {
                byte[] encryptedData = ProtectedData.Protect(
                    Encoding.Unicode.GetBytes(input),
                    null,
                    DataProtectionScope.CurrentUser);

                return Convert.ToBase64String(encryptedData);
            }

            public static string DecryptString(string encryptedData)
            {
                byte[] decryptedData = ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedData),
                    null,
                    DataProtectionScope.CurrentUser);

                return Encoding.Unicode.GetString(decryptedData);
            }
        }

        /// <summary>
        /// Encrypts text using a specified key.
        /// </summary>
        /// <param name="text">The text to encrypt.</param>
        /// <param name="key">The encryption key.</param>
        /// <returns>The encrypted text in Base64 format.</returns>
        private static string EncryptText(string text, string key)
        {
            // Check arguments.
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            string encrypted;
            // Create an Aes object with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key.PadRight(32)); // Ensure the key is long enough, AES requires 256 bits (32 bytes) for the key
                aesAlg.IV = new byte[16]; // AES requires a 128-bit (16 bytes) IV, using zeros for simplicity

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using MemoryStream msEncrypt = new();
                using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);
                using (StreamWriter swEncrypt = new(csEncrypt))
                {
                    //Write all data to the stream.
                    swEncrypt.Write(text);
                }
                encrypted = Convert.ToBase64String(msEncrypt.ToArray());
            }

            // Return the encrypted bytes from the memory stream in Base64 format.
            return encrypted;
        }
    }
}
