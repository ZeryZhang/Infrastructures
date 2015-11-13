using System;
using System.Collections.Specialized;
using System.Linq;
using RestSharp;
using System.Net;
using Hk.Infrastructures.Core.Requests;
using Hk.Infrastructures.Core.Responses;
using Newtonsoft.Json;

namespace Hk.Infrastructures.ServiceClient.HttpClient
{
    public class HttpSynchronizedServiceClient : IServiceClient
    {
        public K ProcessRequest<T, K>(T request, string remoteAddress, Method method, DataFormat dataFormat, NameValueCollection requestHeaderParameters = null, NameValueCollection requestParameters = null)
            where K : BaseResponse, new()
            where T : BaseRequest
        {
            K response = default(K);

            Uri requestUri;

            if (Uri.TryCreate(remoteAddress, UriKind.RelativeOrAbsolute, out requestUri))
            {
                var httpClient = new RestClient(remoteAddress);
                RestRequest httpRequest = null;
                if (!string.IsNullOrWhiteSpace(request.RequestType))
                {
                    httpRequest = new RestRequest(string.Format("{0}/{1}", request.RequestType, request.GetType().Name), method);
                }
                else
                {
                    httpRequest = new RestRequest(method);
                }
                if (requestHeaderParameters != null && requestHeaderParameters.AllKeys.Any())
                {
                    var keys = requestHeaderParameters.AllKeys;
                    foreach (var key in keys)
                    {
                        httpRequest.AddHeader(key, requestHeaderParameters.Get(key));
                    }
                }
                if (requestParameters != null && requestParameters.AllKeys.Any())
                {
                    var keys = requestParameters.AllKeys;
                    foreach (var key in keys)
                    {
                        httpRequest.AddParameter(key, requestParameters.Get(key));
                    }
                }
                httpRequest.RequestFormat = dataFormat;
                //httpRequest.AddBody(request);

                if (dataFormat == DataFormat.Json)
                {
                    string applicationJson = JsonConvert.SerializeObject(request);
                    httpRequest.AddParameter("application/json", applicationJson, ParameterType.RequestBody);
                }
                else
                {
                    httpRequest.AddBody(request);
                }

                var httpResponse = httpClient.Execute(httpRequest);

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    response = JsonConvert.DeserializeObject<K>(httpResponse.Content);
                }
            }
            return response ?? new K();
        }
    }
}

