using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Hk.Infrastructures.Common.Security
{
    /// <summary>
    /// MD5密码处理
    /// </summary>
    public class Md5Util
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns></returns>
        public static string Encrypt(string input)
        {
            return Encrypt(input, new UTF8Encoding());
        }


        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <param name="encode">字符的编码</param>
        /// <returns></returns>
        public static string Encrypt(string input, Encoding encode)
        {

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] buffer = md5.ComputeHash(encode.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in buffer)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }



        /// <summary>
        /// MD5对文件流加密
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string Encrypt(Stream stream)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] buffer = md5.ComputeHash(stream);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in buffer)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// MD5加密(返回16位加密串)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encode"></param>
        /// <param name="encryptDigit">只能够是8,16,32位</param>
        /// <returns></returns>
        public static string Encrypt(string input, Encoding encode, int encryptDigit)
        {

            MD5 md5 = new MD5CryptoServiceProvider();
            string result = string.Empty;
            switch (encryptDigit)
            {
                case 8:
                    result = BitConverter.ToString(md5.ComputeHash(encode.GetBytes(input)), 0, 4);
                    break;
                case 16:
                    result = BitConverter.ToString(md5.ComputeHash(encode.GetBytes(input)), 0, 8);
                    break;
                case 32:
                    result = BitConverter.ToString(md5.ComputeHash(encode.GetBytes(input)));
                    break;
            }
            result = result.Replace("-", "").ToLower();
            return result;
        }
    }
}
