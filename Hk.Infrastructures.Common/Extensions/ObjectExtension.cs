using System;
namespace Hk.Infrastructures.Common.Extensions
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 将对象转换为字符串
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的string类型结果</returns>
        public static string ToStringOrEmpty(this object obj)
        {
            if (obj == null)
                return "";
            return obj.ToString().Trim();
        }
        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的DateTime类型结果</returns>
        public static DateTime ToDateTime(this object obj)
        {
            return obj.ToString().ToDateTime();
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的DateTime类型结果</returns>
        public static DateTime ToDateTime(this object obj, DateTime defValue)
        {
            return obj.ToString().ToDateTime(defValue);
        }
    }
}
