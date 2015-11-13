using System;
using System.Text.RegularExpressions;

namespace Hk.Infrastructures.Common.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// 判断不为空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(this string value)
        {
            return !String.IsNullOrWhiteSpace(value);
        }
        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsEmail(this string strEmail)
        {
            Regex regex = new Regex(@"^[\w\.]+([-]\w+)*@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]", RegexOptions.Compiled |
            RegexOptions.IgnoreCase);
            bool result = false;
            if (strEmail.IsNotEmpty())
            {
                result = regex.IsMatch(strEmail);
            }
            return result;
        }
        /// <summary>
        /// 检测是否是正确的Url
        /// </summary>
        /// <param name="strUrl">要验证的Url</param>
        /// <returns>判断结果</returns>
        public static bool IsUrl(this string strUrl)
        {
            Regex regex = new Regex(@"^http(s)?://([\w-]+\.)+[\w-]+(:\d+)?(/[\w- ./?%&=]*)?$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);
            bool result = false;
            if (strUrl.IsNotEmpty())
            {
                result = regex.IsMatch(strUrl);
            }
            return result;
        }

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIp(this string ip)
        {
            Regex regex = new Regex(@"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            bool result = false;
            if (ip.IsNotEmpty())
            {
                result = regex.IsMatch(ip);
            }
            return result;
        }

        /// <summary>
        /// 判断是否是电话号码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(this string value)
        {
            Regex regex = new Regex(@"^1[3,4,5,7,8][0-9]\d{8}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            bool result = false;
            if (value.IsNotEmpty())
            {
                result = regex.IsMatch(value);
            }
            return result;
        }

        /// <summary>
        /// 检查是否是身份证号码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsIdentityCardNumber(this string value)
        {
            Regex regex = new Regex(@"^\d{17}[\d|X]|\d{15}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            bool result = false;
            if (value.IsNotEmpty())
            {
                result = regex.IsMatch(value);
            }
            return result;
        }
        public static int ToSafeInt(this string value)
        {
            int result = 0;
            if (value.IsNotEmpty())
            {
                Int32.TryParse(value, out result);
            }
            return result;
        }

        public static DateTime ToSafeDateTime(this string value)
        {
            DateTime result = DateTime.MinValue;
            if (value.IsNotEmpty())
            {
                DateTime.TryParse(value, out result);
            }
            return result;
        }


        public static long ToSafeLong(this string value)
        {
            long result = 0;
            if (value.IsNotEmpty())
            {
                Int64.TryParse(value, out result);
            }
            return result;
        }

        #region 数据转换处理
        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        public static bool IsInt32(this string str)
        {
            if (str != null)
            {
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') ||
                        (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 是否为Double类型
        /// </summary>
        public static bool IsDouble(this string str)
        {
            if (str != null)
            {
                return Regex.IsMatch(str, @"^([0-9])[0-9]*(\.\w*)?$");
            }
            return false;
        }

        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="defaultValue">缺省值</param>   
        public static bool ToBool(this string str, bool defaultValue)
        {
            if (str != null)
            {
                if (String.Compare(str, "true", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return true;
                }
                else if (String.Compare(str, "false", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return false;
                }
                else
                {
                    return defaultValue;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 将字符串转换为Int32类型
        /// </summary>
        /// <param name="defaultValue">缺省值</param>
        public static int ToInt32(this string str, int defaultValue)
        {
            if (String.IsNullOrEmpty(str) || str.Trim().Length >= 11 ||
                !Regex.IsMatch(str.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
            {
                return defaultValue;
            }
            int rv;
            if (Int32.TryParse(str, out rv))
            {
                return rv;
            }
            return Convert.ToInt32(ToFloat(str, defaultValue));
        }

        /// <summary>
        /// string型转换为decimal型
        /// </summary>
        /// <param name="defaultValue">缺省值</param>
        public static decimal ToDecimal(this string str, decimal defaultValue)
        {
            if ((str == null) || (str.Length > 10))
            {
                return defaultValue;
            }
            decimal intValue = defaultValue;
            if (str != null)
            {
                bool isDecimal = Regex.IsMatch(str, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (isDecimal)
                {
                    Decimal.TryParse(str, out intValue);
                }
            }
            return intValue;
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="defaultValue">缺省值</param>
        public static float ToFloat(this string str, float defaultValue)
        {
            if ((str == null) || (str.Length > 10))
            {
                return defaultValue;
            }
            float floatValue = defaultValue;
            if (str != null)
            {
                bool isFloat = Regex.IsMatch(str, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (isFloat)
                {
                    Single.TryParse(str, out floatValue);
                }
            }
            return floatValue;
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="defaultValue">缺省值</param>
        public static DateTime ToDateTime(this string str, DateTime defaultValue)
        {
            if (!String.IsNullOrEmpty(str))
            {
                DateTime dateTime;
                if (DateTime.TryParse(str, out dateTime))
                {
                    return dateTime;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        public static DateTime ToDateTime(this string str)
        {
            return str.ToDateTime(DateTime.Now);
        }
        #endregion
    }
}
