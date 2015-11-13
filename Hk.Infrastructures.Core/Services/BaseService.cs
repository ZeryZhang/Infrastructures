using System;
using Hk.Infrastructures.Core.Responses;
namespace Hk.Infrastructures.Core.Services
{
    public abstract class BaseService : IService
    {
        public T Success<T>(T response = null)
            where T : BaseResponse, new()
        {
            if (response == null)
            {
                response = new T();
            }
            response.IsSuccess = true;
            return response;
        }

        public T Fail<T>(string messageCode)
            where T : BaseResponse, new()
        {
            return Fail<T>(messageCode, null);
        }

        public T Fail<T>(string messageCode, string content)
            where T : BaseResponse, new()
        {
            var response = new T();

            response.IsSuccess = false;
            response.MessageCode = messageCode;
            response.MessageContent = content;

            return response;
        }
    }
}
