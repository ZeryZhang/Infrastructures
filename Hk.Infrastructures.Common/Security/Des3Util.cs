using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Hk.Infrastructures.Common.Extensions;

namespace Hk.Infrastructures.Common.Security
{
    public static class Des3Util
    {
        /// <summary>
        /// DES3 CBC模式加密
        /// </summary>
        /// <param name="data">明文的byte数组</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">IV</param>
        /// <param name="cipherMode">密码模式</param>
        /// <returns>密文的byte数组</returns>
        public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv, CipherMode cipherMode)
        {
            try
            {
                MemoryStream mStream = new MemoryStream();
                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = cipherMode;
                tdsp.Padding = PaddingMode.PKCS7;
                CryptoStream cStream = new CryptoStream(mStream,
                    tdsp.CreateEncryptor(key, iv),
                    CryptoStreamMode.Write);
                cStream.Write(data, 0, data.Length);
                cStream.FlushFinalBlock();
                byte[] ret = mStream.ToArray();
                cStream.Close();
                mStream.Close();
                return ret;
            }
            catch (CryptographicException ex)
            {
                throw;
            }
        }
        public static string Encrypt(string data, CipherMode cipherMode)
        {
            string result = string.Empty;
            if (data.IsNotEmpty())
            {
                System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
                byte[] key = Convert.FromBase64String("HKJjZGVmZ2hpamtsbW5vcHFyc3R1dnQJ");
                byte[] iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                byte[] buffer = utf8.GetBytes(data);
                byte[] str3 = Encrypt(buffer, key, iv, cipherMode);
                result = Convert.ToBase64String(str3);
            }
            return result;
        }
        /// <summary>
        /// DES3 CBC模式解密
        /// </summary>
        /// <param name="data">密文的byte数组</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">IV</param>
        /// <param name="cipherMode">密码模式</param>
        /// <returns>明文的byte数组</returns>
        public static byte[] Decrypt(byte[] data, byte[] key, byte[] iv, CipherMode cipherMode)
        {
            try
            {
                MemoryStream msDecrypt = new MemoryStream(data);

                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = cipherMode;
                tdsp.Padding = PaddingMode.PKCS7;
                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    tdsp.CreateDecryptor(key, iv),
                    CryptoStreamMode.Read);
                byte[] fromEncrypt = new byte[data.Length];
                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);
                return fromEncrypt;
            }
            catch (CryptographicException ex)
            {
                throw;
            }
        }
        public static string Decrypt(string data, CipherMode cipherMode)
        {
            try
            {
                string result = string.Empty;
                if (data.IsNotEmpty())
                {
                    System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
                    byte[] key = Convert.FromBase64String("HKJjZGVmZ2hpamtsbW5vcHFyc3R1dnQJ");
                    byte[] iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                    byte[] buffer = Convert.FromBase64String(data);
                    byte[] str4 = Decrypt(buffer, key, iv, cipherMode);
                    result = utf8.GetString(str4).Replace("\0", "");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
