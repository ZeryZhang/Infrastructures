using System;
using Hk.Infrastructures.Common.Enums;

namespace Hk.Infrastructures.Logging
{
    /// <summary>
    /// 日志级别
    /// </summary>
    public enum LogLevel
    {
        Debug=1,
        Info,
        Warning,
        Error,
        Fatal
    }
    public interface ILogger
    {
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="platformType">平台类型</param>
        /// <param name="module">模块(类名:方法名)</param>
        /// <param name="version">版本号（内部版）</param>
        /// <param name="level">日志级别</param>
        /// <param name="exception">异常</param>
        /// <param name="format">自定义异常信息</param>
        /// <param name="args">自定义异常信息参数传递</param>
        void Log(int platformType, string module, string version, LogLevel level, Exception exception, string format, params object[] args);
    }
}
