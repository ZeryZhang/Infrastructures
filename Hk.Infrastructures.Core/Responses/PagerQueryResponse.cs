using System;
namespace Hk.Infrastructures.Core.Responses
{
    [Serializable]
    public class PagerQueryResponse : BaseResponse
    {
        public int TotalCount { get; set; }
    }
}
