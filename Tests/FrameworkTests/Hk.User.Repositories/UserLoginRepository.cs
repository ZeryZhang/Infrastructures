using System;
using Hk.Infrastructures.Repositories;
using Hk.Infrastructures.Sql;

namespace Hk.User.Repositories
{
   public class UserLoginRepository : Repository
    {
        public UserLoginRepository()
            : base("User")
        {
        }

        //public long AddUserLogin(Hk.User.Domain.Entities.UserLogin userLoginEntity)
        //{
        //    long result = 0;
        //    using (var conn = CreateWriteDbConnection())
        //    {
        //        result=conn.Insert(userLoginEntity);
        //    }
        //    return result;
        //}

        public int AddUserLogin(Hk.User.Domain.Entities.UserLogin userLoginEntity)
        {
            int result = 0;
            using (var conn = CreateWriteDbConnection())
            {
                string sql = "INSERT INTO UserLogin ( UserId ,UserTypeID ,LoginName ,Password ,Email ,IsEmailCertification ,MobilePhone ,IsPhoneCertification ) VALUES (?UserId ,?UserTypeID ,?LoginName ,?Password ,?Email ,?IsEmailCertification ,?MobilePhone ,?IsPhoneCertification)";
               result= conn.Execute(sql, userLoginEntity);
            }
            return result;
        }
        public bool DeleteUserLoginByUserId(string userId)
        {
            using (var conn = CreateWriteDbConnection())
            {
                conn.Open();
                //开户事务
                var trans = conn.BeginTransaction();
                var rows = conn.Execute("delete from UserLogin where UserId=@UserId", new { UserId = userId }, trans);
                if (rows > 0)
                {
                    rows = conn.Execute("delete from XXX where XXXX=@XXXX", new { XXXX = userId }, trans);
                    if (rows > 0)
                    {
                        trans.Commit();
                    }
                    else
                    {
                        trans.Rollback();
                    }
                }
                else
                {
                    trans.Rollback();
                }
                conn.Close();
                return rows > 0;
            }
        }
    }
}
