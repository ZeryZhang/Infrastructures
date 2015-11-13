using System;
using Hk.User.DataContract.Requests;
using Hk.User.DataContract.Responses;

namespace Hk.User.IServices
{
   public interface IUserLoginService:Hk.Infrastructures.Core.Services.IService
   {
       AddUserLoginRespone AddUserLogin(AddUserLoginRequest request);
   }
}
