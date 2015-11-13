using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hk.Infrastructures.Redis;

namespace Hk.Infrastructures.RedisTests
{
    class Program
    {
        private static void TestMessageQueueManager()
        {
            Hk.Infrastructures.Redis.StackExchangeRedisQueue queue = new StackExchangeRedisQueue();
            var queueName = "person";
            var p1 = new Person { Username = "p1" };
            queue.Enqueue<Person>(queueName, p1);
            var p2 = new Person { Username = "p2" };
            queue.Enqueue<Person>(queueName, p2);
            var length1 = queue.GetLength(queueName);

            //  var pData = queue.Peek<Person>(queueName,0,0);
            var dData = queue.Dequeue<Person>(queueName);
            var length2 = queue.GetLength(queueName);

            queue.Remove<Person>(queueName, new List<Person> { p2 });
            var length3 = queue.GetLength(queueName);

            queue.Clear(queueName);
        }
        static void Main(string[] args)
        {
            //TestMessageQueueManager();
            Hk.Infrastructures.Redis.ICache cache = new StackExchangeRedisCache();
            string str = cache.Get<string>("workinfo_city230100_hospitaladsfybjy_departmentfckek");
            //for (int i = 0; i < 4; i++)
            //{            
            //    //cache.Set("key" + i.ToString(), "Value" + i.ToString());
            //}
            //cache.RemoveByPattern("");
            //List<string> list = cache.GetCacheKeys("*").ToList();
            //Parallel.For(0, 5000, (i) =>
            //{
            //    cache.Set("key" + i.ToString(), "Value" + i.ToString());
            //    string str = cache.Get<string>("key" + i.ToString());
            //});
            //List<string> list1 = cache.GetCacheKeys("*").ToList();
            //Console.Write(list1.Count);
            //cache.RemoveByPattern("");
            ////cache.Remove("key");
            ////cache.Remove("111key1");
            ////cache.Remove("key1");
            ////cache.Remove("key2");
            //List<string> list1 = cache.GetCacheKeys("").ToList();
            //cache.Get<string>("111key1");
            //List<User> list = new List<User>();
            //list = new TestCacheRepository().GetAllUser();
            //int i = list.Count;
        }
    }

    [Serializable]
    public class Person
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Profile Profile { get; set; }
    }

    [Serializable]
    public class Profile
    {
        public int Age { get; set; }
    }
}
