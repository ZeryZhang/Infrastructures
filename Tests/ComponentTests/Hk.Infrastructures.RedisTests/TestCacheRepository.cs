using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hk.Infrastructures.Caching;

namespace Hk.Infrastructures.RedisTests
{
    public class TestCacheRepository : CacheRepository
    {
        public TestCacheRepository()
            : base("User")
        {
        }

        public List<User> GetAllUser()
        {
            return GetCacheData(GetAllUserData);
        }

        private List<User> GetAllUserData()
        {
            List<User> list=new List<User>();
            for (int i = 0; i < 10; i++)
            {
                User user=new User();
                user.Username = "GetAllUserData" + i.ToString();
                user.Password = "xxxxxx";
                list.Add(user);
            }
            return list;
        }
    }
       [Serializable]
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

       
    }
}
