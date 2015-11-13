using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Hk.Infrastructures.Common.Utility
{
    /// <summary>
    /// 身份证号码
    /// </summary>
    public class IdentityCardNumberHelper
    {
        /// <summary>
        /// 18位身份证号码
        /// </summary>
        private static readonly Regex MatchIdentityNumber18 =
            new Regex(@"^[1-9]\d{5}(?<date>[1-9]\d{3}((0\d)|(1[0-2]))((0[1-9])|([1-2]\d)|3[0-1]))\d{3}[\dXx]$");

        /// <summary>
        /// 18位验证码验证系数
        /// </summary>
        private static readonly int[] Coefficient = {7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2};

        private static readonly string[] ResultValidate = {"1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2"};

        /// <summary>
        /// 15位身份证号码
        /// </summary>
        private static readonly Regex MatchIdentityNumber15=
            new Regex(@"^[1-9]\d{5}(?<date>\d{2}((0\d)|(1[0-2]))((0[1-9])|([1-2]\d)|3[0-1]))\d{3}$");

        /// <summary>
        /// 检查是不是合法身份证号码
        /// </summary>
        /// <param name="identityCardNumber"></param>
        /// <returns></returns>
        public static bool IsAvailable(string identityCardNumber)
        {
            switch (identityCardNumber.Length)
            {
                case 15:
                    return Check15IdentityNumber(identityCardNumber);
                case 18:
                    return Check18IdentityNumber(identityCardNumber);
                default:
                    return false;
            }
        }

        /// <summary>
        /// 检查18位
        /// </summary>
        /// <returns></returns>
        private static bool Check18IdentityNumber(string identityCardNumber)
        {
            var m = MatchIdentityNumber18.Match(identityCardNumber);
            if (!m.Success)
                return false;

            DateTime d;

            if (DateTime.TryParseExact(m.Groups["date"].Value, "yyyyMMdd", new CultureInfo("zh-CN", true),
                DateTimeStyles.None, out d) && d < DateTime.Now)
            {

                char[] cs = identityCardNumber.ToCharArray();

                int sum = 0;

                for (int i = 0; i < 17; i++)
                {
                    int temp = Convert.ToInt32(cs[i].ToString());
                    sum += temp*Coefficient[i];
                }

                int mod = sum%11;

                if (cs[17].ToString().ToUpper() == ResultValidate[mod])
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 检查15位
        /// </summary>
        /// <returns></returns>
        private static bool Check15IdentityNumber(string identityCardNumber)
        {
            var m = MatchIdentityNumber15.Match(identityCardNumber);
            if (!m.Success)
                return false;

            DateTime d;

            if (DateTime.TryParseExact("19"+m.Groups["date"].Value, "yyyyMMdd", new CultureInfo("zh-CN", true),
                DateTimeStyles.None, out d) && d < DateTime.Now)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 通过身份证号码获取出生日期
        /// </summary>
        /// <param name="identityCardNumber"></param>
        /// <returns></returns>
        public static DateTime GetBirthdate(string identityCardNumber)
        {
            if(!IsAvailable(identityCardNumber))
                throw new FormatException("非法身份证号码");

            return BirthdateParse(identityCardNumber);
        }

        /// <summary>
        /// 通过身份证号码获取出生日期
        /// </summary>
        /// <param name="identityCardNumber"></param>
        /// <param name="birthdate"></param>
        /// <returns></returns>
        public static bool TryGetBirthdate(string identityCardNumber,out DateTime birthdate)
        {
            if (!IsAvailable(identityCardNumber))
            {
                birthdate = DateTime.MinValue;
                return false;
            }
            birthdate = BirthdateParse(identityCardNumber);
            return true;
        }

        /// <summary>
        /// 转换出生日期
        /// </summary>
        /// <param name="validatedIdentityCardNumber">合法的身份证号码</param>
        /// <returns></returns>
        private static DateTime BirthdateParse(string validatedIdentityCardNumber)
        {
            string dateString = String.Empty;

            switch (validatedIdentityCardNumber.Length)
            {
                case 15:
                    dateString = "19" + validatedIdentityCardNumber.Substring(6, 6);
                    break;
                case 18:
                    dateString = validatedIdentityCardNumber.Substring(6, 8);
                    break;
            }

            return DateTime.ParseExact(dateString, "yyyyMMdd", new CultureInfo("zh-CN", true));
        }
    }
}
