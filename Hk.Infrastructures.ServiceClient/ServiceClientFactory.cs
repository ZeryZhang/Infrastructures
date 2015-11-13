using System;
using Hk.Infrastructures.Core.Requests;
using Hk.Infrastructures.ServiceClient.HttpClient;
namespace Hk.Infrastructures.ServiceClient
{
    internal static class ServiceClientFactory
    {
        public static IServiceClient GetServiceClient<T>(T request, string remoteAddress)
            where T : BaseRequest
        {
            IServiceClient client = null;
            if (!string.IsNullOrWhiteSpace(remoteAddress))
            {
                if (remoteAddress.StartsWith("http://") || remoteAddress.StartsWith("https://"))
                {
                    if (request.IsAsync)
                    {
                        client = new HttpAsynchronizedServiceClient();
                    }
                    else
                    {
                        client = new HttpSynchronizedServiceClient();
                    }
                }               
            }
            return client ?? new HttpAsynchronizedServiceClient();
        }
    }
}
