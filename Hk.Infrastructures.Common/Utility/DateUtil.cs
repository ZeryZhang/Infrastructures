using System;
using System.Text;
namespace Hk.Infrastructures.Common.Utility
{
   public class DateUtil
    {
        #region 返回两个时间差的天数 +DateDiff(DateTime dateTime1, DateTime dateTime2)
        /// <summary>
        /// 返回两个时间差的天数
        /// </summary>
        /// <param name="dateTime1">起始时间</param>
        /// <param name="dateTime2">截止时间</param>
        /// <returns></returns>
        public static int DateDiff(DateTime dateTime1, DateTime dateTime2)
        {
            int dateDiff = 0;

            TimeSpan ts1 = new TimeSpan(dateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(dateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            dateDiff = ts.Days;//"天"
            return dateDiff;
        }
        #endregion

        #region 返回两个时间差的小时 +DateDiffHours(DateTime dateTime1, DateTime dateTime2)
        /// <summary>
        /// 返回两个时间差的小时
        /// </summary>
        /// <param name="dateTime1">起始时间</param>
        /// <param name="dateTime2">截止时间</param>
        /// <returns></returns>
        public static int DateDiffHours(DateTime dateTime1, DateTime dateTime2)
        {
            int dateDiff = 0;

            TimeSpan ts1 = new TimeSpan(dateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(dateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            dateDiff = ts.Hours;//"小时"
            return dateDiff;
        }
        #endregion

        #region 秒数转为时间 +DisplayDateTimeCountBySecond(long pSecond)
        /// <summary>
        /// 秒数转为时间
        /// </summary>
        /// <param name="pSecond">秒数</param>
        /// <returns>时间数</returns>
        public static string DisplayDateTimeCountBySecond(long pSecond)
        {
            StringBuilder oStringBuilder = new StringBuilder();

            //大于一天，显示天
            if (pSecond >= 60 * 60 * 24)
            {
                oStringBuilder.Append(string.Format("{0:F0}", pSecond / 86400));
                oStringBuilder.Append("天");
                pSecond = pSecond % (86400);
            }

            //一天内，显示小时
            if (pSecond > 3600 || pSecond == 0)
            {
                oStringBuilder.Append(string.Format("{0:F0}", pSecond / 3600));
                oStringBuilder.Append("小时");
                pSecond = pSecond % (3600);
            }

            //一小时内，显示分
            if (pSecond > 60 || pSecond == 0)
            {
                oStringBuilder.Append(string.Format("{0:F0}", pSecond / 60));
                oStringBuilder.Append("分");
                pSecond = pSecond % (60);
            }

            //一分钟内，显示秒
            if (pSecond < 60)
            {
                oStringBuilder.Append(pSecond.ToString());
                oStringBuilder.Append("秒");
            }

            return oStringBuilder.ToString();
        }
        #endregion

        #region 毫秒数转为时间 +DisplayDateTimeCountByMillisecond(long pMillisecond)
        /// <summary>
        /// 秒数转为时间
        /// </summary>
        /// <param name="pMillisecond">秒数</param>
        /// <returns>时间数</returns>
        public static string DisplayDateTimeCountByMillisecond(long pMillisecond)
        {
            StringBuilder oStringBuilder = new StringBuilder();

            pMillisecond = int.Parse(string.Format("{0:F0}", pMillisecond / 1000));

            //大于一天，显示天
            if (pMillisecond >= 60 * 60 * 24)
            {
                oStringBuilder.Append(string.Format("{0:F0}", pMillisecond / 86400));
                oStringBuilder.Append("天");
                pMillisecond = pMillisecond % (86400);
            }

            //一天内，显示小时
            if (pMillisecond > 3600 || pMillisecond == 0)
            {
                oStringBuilder.Append(string.Format("{0:F0}", pMillisecond / 3600));
                oStringBuilder.Append("小时");
                pMillisecond = pMillisecond % (3600);
            }

            //一小时内，显示分
            if (pMillisecond > 60 || pMillisecond == 0)
            {
                oStringBuilder.Append(string.Format("{0:F0}", pMillisecond / 60));
                oStringBuilder.Append("分");
                pMillisecond = pMillisecond % (60);
            }

            //一分钟内，显示秒
            if (pMillisecond < 60)
            {
                oStringBuilder.Append(pMillisecond.ToString());
                oStringBuilder.Append("秒");
            }

            return oStringBuilder.ToString();
        }
        #endregion
    }
}
