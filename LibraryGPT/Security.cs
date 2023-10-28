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

    }
}
