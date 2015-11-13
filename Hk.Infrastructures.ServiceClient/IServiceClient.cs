using System;
using System.Collections.Specialized;
using Hk.Infrastructures.Core.Requests;
using Hk.Infrastructures.Core.Responses;
using RestSharp;

namespace Hk.Infrastructures.ServiceClient
{
    public interface IServiceClient
    {
        K ProcessRequest<T, K>(T request, string remoteAddress, Method method, DataFormat dataFormat, NameValueCollection requestHeaderParameters = null, NameValueCollection requestparameters = null)
            where K : BaseResponse, new()
            where T : BaseRequest;
    }
}
