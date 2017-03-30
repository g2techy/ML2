using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace G2.Frameworks.Core
{
    public static class Encryption
    {
        private static string m_strEncryptionKey = "DaTeNcKey";
        
        public static string EncryptionKey
        {
            get
            {
                return m_strEncryptionKey;
            }
            set
            {
                m_strEncryptionKey = value;
            }
        }

        private static byte[] SALT
        {
            get
            {
                return Encoding.ASCII.GetBytes(EncryptionKey.Length.ToString());
            }
        }

        public static string Encrypt(string inputText)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] plainText = Encoding.Unicode.GetBytes(inputText);
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(EncryptionKey, SALT);

            using (ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16)))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainText, 0, plainText.Length);
                        cryptoStream.FlushFinalBlock();
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }

        public static string Decrypt(string inputText)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            string _dataToDecrypt = inputText.Replace(" ", "+");
            if (_dataToDecrypt.Length % 4 > 0)
            {
                _dataToDecrypt = _dataToDecrypt.PadRight(_dataToDecrypt.Length + 4 - _dataToDecrypt.Length % 4, '=');
            }
            byte[] encryptedData = Convert.FromBase64String(_dataToDecrypt);
            PasswordDeriveBytes secretKey = new PasswordDeriveBytes(EncryptionKey, SALT);

            using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
            {
                using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] plainText = new byte[encryptedData.Length];
                        int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                        return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
                    }
                }
            }
        }
    }
}
