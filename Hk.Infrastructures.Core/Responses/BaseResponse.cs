using System;

namespace Hk.Infrastructures.Core.Responses
{
    [Serializable]
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string MessageCode { get; set; }
        public string MessageContent { get; set; }
    }
}


