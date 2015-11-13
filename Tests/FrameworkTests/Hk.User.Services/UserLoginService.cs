using System;
using Hk.User.DataContract.Requests;
using Hk.User.DataContract.Responses;

namespace Hk.User.Services
{
   public class UserLoginService:Hk.Infrastructures.Core.Services.BaseService,Hk.User.IServices.IUserLoginService
    {
       public AddUserLoginRespone AddUserLogin(AddUserLoginRequest request)
       {
           return null;
       }
    }
}
