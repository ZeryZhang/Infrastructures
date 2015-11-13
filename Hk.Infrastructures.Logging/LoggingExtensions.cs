using System;
using Hk.Infrastructures.Common.Enums;

namespace Hk.Infrastructures.Logging
{
    public static class LoggingExtensions
    {
        public static void Debug(this ILogger logger, int platformType, string module, string version, string message)
        {
            WriteLog(logger, platformType, module, version, LogLevel.Debug, null, message, null);
        }

        public static void Info(this ILogger logger, int platformType, string module, string version, string message)
        {
            WriteLog(logger, platformType, module, version, LogLevel.Info, null, message, null);
        }

        public static void Warning(this ILogger logger, int platformType, string module, string version, string message)
        {
            WriteLog(logger, platformType, module, version, LogLevel.Warning, null, message, null);
        }

        public static void Error(this ILogger logger, int platformType, string module, string version, string message)
        {
            WriteLog(logger, platformType, module, version, LogLevel.Error, null, message, null);
        }

        public static void Fatal(this ILogger logger, int platformType, string module, string version, string message)
        {
            WriteLog(logger, platformType, module, version, LogLevel.Fatal, null, message, null);
        }

        public static void Debug(this ILogger logger, int platformType, string module, string version, Exception exception, string message)
        {
            WriteLog(logger, platformType, module, version, LogLevel.Debug, exception, message, null);
        }

        public static void Info(this ILogger logger, int platformType, string module, string version, Exception exception, string message)
        {
            WriteLog(logger, platformType, module, version, LogLevel.Info, exception, message, null);
        }

        public static void Warning(this ILogger logger, int platformType, string module, string version, Exception exception, string message)
        {
            WriteLog(logger, platformType, module, version, LogLevel.Warning, exception, message, null);
        }

        public static void Error(this ILogger logger, int platformType, string module, string version, Exception exception, string message)
        {
            WriteLog(logger, platformType, module, version, LogLevel.Error, exception, message, null);
        }

        public static void Fatal(this ILogger logger, int platformType, string module, string version, Exception exception, string message)
        {
            WriteLog(logger, platformType, module, version, LogLevel.Fatal, exception, message, null);
        }

        public static void Debug(this ILogger logger, int platformType, string module, string version, string format, params object[] args)
        {
            WriteLog(logger, platformType, module, version, LogLevel.Debug, null, format, args);
        }

        public static void Info(this ILogger logger, int platformType, string module, string version, string format, params object[] args)
        {
            WriteLog(logger, platformType, module, version, LogLevel.Info, null, format, args);
        }

        public static void Warning(this ILogger logger, int platformType, string module, string version, string format, params object[] args)
        {
            WriteLog(logger, platformType, module, version, LogLevel.Warning, null, format, args);
        }

        public static void Error(this ILogger logger, int platformType, string module, string version, string format, params object[] args)
        {
            WriteLog(logger, platformType, module, version, LogLevel.Error, null, format, args);
        }

        public static void Fatal(this ILogger logger, int platformType, string module, string version, string format, params object[] args)
        {
            WriteLog(logger, platformType, module, version, LogLevel.Fatal, null, format, args);
        }

        public static void Debug(this ILogger logger, int platformType, string module, string version, Exception exception, string format, params object[] args)
        {
            WriteLog(logger, platformType, module, version, LogLevel.Debug, exception, format, args);
        }

        public static void Info(this ILogger logger, int platformType, string module, string version, Exception exception, string format, params object[] args)
        {
            WriteLog(logger, platformType, module, version, LogLevel.Info, exception, format, args);
        }

        public static void Warning(this ILogger logger, int platformType, string module, string version, Exception exception, string format, params object[] args)
        {
            WriteLog(logger, platformType, module, version, LogLevel.Warning, exception, format, args);
        }

        public static void Error(this ILogger logger, int platformType, string module, string version, Exception exception, string format, params object[] args)
        {
            WriteLog(logger, platformType, module, version, LogLevel.Error, exception, format, args);
        }

        public static void Fatal(this ILogger logger, int platformType, string module, string version, Exception exception, string format, params object[] args)
        {
            WriteLog(logger, platformType, module, version, LogLevel.Fatal, exception, format, args);
        }

        private static void WriteLog(ILogger logger, int platformType, string module, string version, LogLevel level, Exception exception, string format,
            object[] objects)
        {
            logger.Log(platformType, module,version, level, exception, format, objects);
        }
    }
}
