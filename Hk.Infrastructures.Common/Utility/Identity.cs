using System;
namespace Hk.Infrastructures.Common.Utility
{
    public class Identity
    {
        private static IdWorker _idWorker = new IdWorker(2,1);
        public static string GenerateOrderId()
        {
            return _idWorker.NextId().ToString();
        }

        public static string GenerateId()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return System.DateTime.Now.ToString("yyMMddHHmmssffff") + BitConverter.ToInt64(buffer, 0).ToString();
        }
        public static string GeneratePayOrderId()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return System.DateTime.Now.ToString("yyyyMMdd") + _idWorker.NextId().ToString();
        }
    }
}
