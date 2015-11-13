using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Hk.Infrastructures.Common.Security
{
    /// <summary>
    /// DES密码处理
    /// </summary>
    public static class DesUtil
    {
        public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            MemoryStream ms = new MemoryStream(); 
            Rijndael alg = Rijndael.Create();
            alg.Key = key;
            alg.IV = iv;     
            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);         
            cs.Write(data, 0, data.Length);
            cs.Close();        
            byte[] encryptedData = ms.ToArray();
            return encryptedData;
        }

        public static string Encrypt(string data, string password)
        {
            if (!string.IsNullOrWhiteSpace(data) && !string.IsNullOrWhiteSpace(password))
            {
                byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(data);
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                byte[] encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
                return Convert.ToBase64String(encryptedData);
            }
            else
            {
                return string.Empty;
            }
        }

        public static byte[] Encrypt(byte[] data, string password)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password,
                new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            return Encrypt(data, pdb.GetBytes(32), pdb.GetBytes(16));
        }

        public static byte[] Decrypt(byte[] cipherData, byte[] key, byte[] iv)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = key;
            alg.IV = iv;
            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cipherData, 0, cipherData.Length);
            cs.Close();
            byte[] decryptedData = ms.ToArray();
            return decryptedData;
        }

        public static string Decrypt(string data, string password)
        {
            if (!string.IsNullOrWhiteSpace(data) && !string.IsNullOrWhiteSpace(password))
            {
                byte[] cipherBytes = Convert.FromBase64String(data);
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(password,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                byte[] decryptedData = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));
                return System.Text.Encoding.Unicode.GetString(decryptedData);
            }
            else
            {
                return string.Empty;
            }
        }
        public static byte[] Decrypt(byte[] data, string password)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            return Decrypt(data, pdb.GetBytes(32), pdb.GetBytes(16));
        }
    }
}
