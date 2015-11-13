using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Hk.Infrastructures.Common.Extensions;
using Hk.Infrastructures.Redis.Configs;
using Hk.Infrastructures.Redis.Serializer;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Hk.Infrastructures.Redis
{
    public class StackExchangeRedisQueue
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _db;
        private readonly ISerializer _serializer;
        private static readonly Encoding encoding = Encoding.UTF8;

        /// <summary>
        /// Initializes a new instance of the <see cref="StackExchangeRedisQueue"/> class.
        /// </summary>
        public StackExchangeRedisQueue()
        {
            var config = Configs.Config.GetConfig();
            if (config == null)
            {
                throw new ConfigurationErrorsException(
                    "Unable to locate <ConfigItem> section into your configuration file.");
            }
            ConfigurationOptions options = new ConfigurationOptions
            {
                Ssl = config.Ssl,
                AllowAdmin = config.AllowAdmin
            };

            foreach (Host host in config.Hosts)
            {
                options.EndPoints.Add(host.Ip, host.Port);
            }

            this._connectionMultiplexer = ConnectionMultiplexer.Connect(options);
            _db = _connectionMultiplexer.GetDatabase(config.Database);
            this._serializer = new NewtonsoftSerializer();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StackExchangeRedisCache"/> class.
        /// </summary>
        /// <param name="serializer">The serializer.</param>
        public StackExchangeRedisQueue(ISerializer serializer)
        {
            if (serializer == null)
            {
                throw new ArgumentNullException("serializer");
            }
            var config = Configs.Config.GetConfig();
            if (config == null)
            {
                throw new ConfigurationErrorsException(
                    "Unable to locate <ConfigItem> section into your configuration file.");
            }
            ConfigurationOptions options = new ConfigurationOptions
            {
                Ssl = config.Ssl,
                AllowAdmin = config.AllowAdmin
            };

            foreach (Host host in config.Hosts)
            {
                options.EndPoints.Add(host.Ip, host.Port);
            }

            this._connectionMultiplexer = ConnectionMultiplexer.Connect(options);
            _db = _connectionMultiplexer.GetDatabase(config.Database);
            this._serializer = serializer;
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queueName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Enqueue<T>(string queueName, T data) 
        {
            bool result = false;
            if (!string.IsNullOrWhiteSpace(queueName))
            {
                var value = _serializer.Serialize(data);
                result = _db.ListRightPush(queueName, value, StackExchange.Redis.When.Always,
                    StackExchange.Redis.CommandFlags.PreferMaster) > 0;
            }
            return result;
        }

        /// <summary>
        /// 出对
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public T Dequeue<T>(string queueName) where T : class
        {
            T result = default(T);

            if (!string.IsNullOrWhiteSpace(queueName))
            {
                var value = _db.ListLeftPop(queueName);
                result = _serializer.Deserialize<T>(value);
            }
            return result;
        }

        public List<T> Peek<T>(string queueName, int startFrom, int endAt) 
        {
            List<T> result = new List<T>();

            if (!string.IsNullOrWhiteSpace(queueName))
            {
                var jsonDataList = _db.ListRange(queueName, startFrom, endAt);
                if (jsonDataList.IsNotNull())
                {
                    result.AddRange(jsonDataList.Select(jsonData => JsonConvert.DeserializeObject<T>(jsonData)));
                }
            }
            return result;
        }

        public long GetLength(string queueName)
        {
            long result = 0;
            if (!string.IsNullOrWhiteSpace(queueName))
            {
                result = _db.ListLength(queueName);
            }
            return result;
        }

        public void Remove<T>(string queueName, List<T> items)
        {
            if (!string.IsNullOrWhiteSpace(queueName) && items.IsNotNull())
            {
                foreach (var item in items)
                {
                    _db.ListRemove(queueName, JsonConvert.SerializeObject(item));
                }
            }
        }

        public void Clear(string queueName)
        {
            if (!string.IsNullOrWhiteSpace(queueName))
            {
                _db.KeyDeleteAsync(queueName);
            }
        }
    }
}
