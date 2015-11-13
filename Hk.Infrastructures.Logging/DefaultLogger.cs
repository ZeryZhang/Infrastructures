using System;
using System.Reflection;
using Hk.Infrastructures.Common.Enums;
using Hk.Infrastructures.Common.Utility;
using Hk.Infrastructures.Mongo.Repository;

namespace Hk.Infrastructures.Logging
{
    internal class DefaultLogger:ILogger
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
        public void Log(int platformType, string module, string version, LogLevel level, Exception exception,
            string format, params object[] args)
        {
            var repo = new MongoRepository<ErrorLog>("LoggingLibrary");
            var newLog = new ErrorLog
            {
                ErrorLogId = Identity.GenerateId(),
                PlatformType = platformType,
                Module=module,
                Version=version,
                LevelValue = (int)level,
                LevelName = level.ToString(),
                ExceptionMessage = exception==null?format:exception.Message,
                ExceptionInformation = exception == null ? format : exception.ToString(),
                CustomMessage = (args==null)?format:string.Format(format, args)
            };
            repo.Add(newLog);
        }
    }
}
