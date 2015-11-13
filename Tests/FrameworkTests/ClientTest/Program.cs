using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hk.Infrastructures.Validator.Results;
using Hk.User.Domain.Entities;
using Hk.User.Domain.Validators;
using Hk.User.Repositories;

namespace ClientTest
{
    internal class Program
    {
        private static void f(string a, string b)
        {

        }
        /// <summary>
        /// 调用示例 https://github.com/JeremySkinner/FluentValidation/wiki
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            //for (int k = 0; k < 10000; k++)
            //{
            Hk.User.Domain.Entities.UserLogin userLogin = new UserLogin();
            userLogin.Email = "mawei@hk515.com";
            userLogin.IsEmailCertification = false;
            userLogin.IsPhoneCertification = true;
            userLogin.LoginName = "admin";
            userLogin.MobilePhone = "13571111111";
            userLogin.Password = "password";
            userLogin.UserId = userLogin.IdentityId;
            userLogin.UserTypeId = 1;
            UserLoginValidator validator = new UserLoginValidator();
            ValidationResult result = validator.Validate(userLogin);
            if (result.IsValid)
            {

                Hk.User.Repositories.UserLoginRepository userLoginRepository = new UserLoginRepository();
                int i = userLoginRepository.AddUserLogin(userLogin);
            }
            else
            {
                result.Errors.ToList().ForEach(error =>
                {
                    f(error.PropertyName, error.ErrorMessage);
                });

            }
            //}
            Console.Write("完成");
        }
    }
}
