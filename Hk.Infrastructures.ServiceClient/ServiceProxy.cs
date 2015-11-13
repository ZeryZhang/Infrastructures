using System;
using System.Collections.Specialized;
using System.IO;
using Hk.Infrastructures.Common.Extensions;
using Hk.Infrastructures.Core.Requests;
using Hk.Infrastructures.Core.Responses;
using Hk.Infrastructures.Core.ServicePlugins;
using RestSharp;

namespace Hk.Infrastructures.ServiceClient
{
    public static class ServiceProxy
    {
        private static ServicePluginsManager _plugInManager;

        static ServiceProxy()
        {
            _plugInManager = new ServicePluginsManager();
            _plugInManager.PlugInFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ServicePlugins");
            _plugInManager.LoadPlugIns();
        }
        /// <summary>
        /// 固定地址请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public static K ProcessRequest<T, K>(T request)
            where K : BaseResponse, new()
            where T : BaseRequest
        {
            try
            {
                var busConfig = Configs.Config.GetConfig();
                NameValueCollection requestHeaderParameters = new NameValueCollection
                {
                    {"Content-Type", "application/json; charset=utf-8"},
                    {"RequestType", request.RequestType}
                };
                return ProcessRequest<T, K>(request, busConfig.Url, Method.POST, DataFormat.Json,
                    requestHeaderParameters, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 动态地址请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="request">请求数据</param>
        /// <param name="remoteAddress">请求地址</param>
        /// <param name="method">请求方式</param>
        /// <param name="dataFormat"></param>
        /// <param name="requestHeaderParameters"></param>
        /// <param name="requestparameters"></param>
        /// <returns></returns>
        public static K ProcessRequest<T, K>(T request, string remoteAddress, Method method, DataFormat dataFormat,
            NameValueCollection requestHeaderParameters = null, NameValueCollection requestparameters = null)
            where K : BaseResponse, new()
            where T : BaseRequest
        {
            var result = new K();
            try
            {
                if (!string.IsNullOrWhiteSpace(remoteAddress))
                {
                    var client = ServiceClientFactory.GetServiceClient<T>(request, remoteAddress);
                    if (client != null)
                    {
                        var serviceSteps = _plugInManager.PlugIns;
                        if (serviceSteps.IsNotNull())
                        {
                            var propertyBag = new PropertyBag();
                            propertyBag["Request"] = request;

                            foreach (var step in serviceSteps)
                            {
                                step.PlugInProxy.Process(propertyBag);
                            }
                        }

                        result = client.ProcessRequest<T, K>(request, remoteAddress, method, dataFormat,
                            requestHeaderParameters, requestparameters);
                        if (!request.IsAsync)
                        {
                            request = null;
                        }
                    }
                }
                else
                {
                    var response = new K
                    {
                        MessageCode = "10002" //没有注册远程请求地址
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static void ProcessRequest<T>(T request)
            where T : BaseRequest
        {
            ProcessRequest<T, BaseResponse>(request);
        }

        public static void ProcessRequest<T>(T request, string remoteAddress, Method method, DataFormat dataFormat,
            NameValueCollection requestHeaderParameters = null, NameValueCollection requestparameters = null)
            where T : BaseRequest
        {
            ProcessRequest<T, BaseResponse>(request, remoteAddress, method, dataFormat, requestHeaderParameters,
                requestparameters);
        }
    }
}
