using System;
using Hk.Infrastructures.Common.Utility;

namespace Hk.Infrastructures.Core.Requests
{
    [Serializable]
    public class BaseRequest
    {
        public BaseRequest()
        {
            RequestId = Identity.GenerateId();
            CreateDateTime = DateTime.Now;
            RequestName = this.GetType().Name;
        }
        /// <summary>
        /// 请求Id
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// 请求名称
        /// </summary>
        public string RequestName { get; set; }
        /// <summary>
        /// 请求类型
        /// </summary>
        public string RequestType { get; set; }
        /// <summary>
        /// 是否异步
        /// </summary>
        public bool IsAsync { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}

