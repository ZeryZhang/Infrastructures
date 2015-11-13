using System;
using Hk.Infrastructures.Core.Entities;
using Hk.Infrastructures.Validator.Attributes;
using Hk.User.Domain.Validators;

namespace Hk.User.Domain.Entities
{
     [Validator(typeof(UserLoginValidator))]
    [Serializable]
    public class UserLogin:BaseEntity
    {
        public UserLogin()
        { }
        #region Model
        private string _userid;
        private int _usertypeid;
        private string _loginname;
        private string _password;
        private string _email;
        private bool _isemailcertification=false;
        private string _mobilephone;
        private bool _isphonecertification=false;  
        /// <summary>
        /// 
        /// </summary>
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int UserTypeId
        {
            set { _usertypeid = value; }
            get { return _usertypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LoginName
        {
            set { _loginname = value; }
            get { return _loginname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsEmailCertification
        {
            set { _isemailcertification = value; }
            get { return _isemailcertification; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MobilePhone
        {
            set { _mobilephone = value; }
            get { return _mobilephone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsPhoneCertification
        {
            set { _isphonecertification = value; }
            get { return _isphonecertification; }
        }
        #endregion Model
    }
}
