﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Hk.Infrastructures.Common.Extensions;
using Hk.Infrastructures.Redis.Configs;
using Hk.Infrastructures.Redis.Serializer;
using StackExchange.Redis;

namespace Hk.Infrastructures.Redis
{
    public class StackExchangeRedisCache : ICache
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _db;
        private readonly ISerializer _serializer;
        private static readonly Encoding encoding = Encoding.UTF8;
        /// <summary>
        /// Initializes a new instance of the <see cref="StackExchangeRedisCache"/> class.
        /// </summary>
        public StackExchangeRedisCache()
        {
            var config = Configs.Config.GetConfig();
            if (config == null)
            {
                throw new ConfigurationErrorsException("Unable to locate <ConfigItem> section into your configuration file.");
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
        public StackExchangeRedisCache(ISerializer serializer)
        {
            if (serializer == null)
            {
                throw new ArgumentNullException("serializer");
            }
            var config = Configs.Config.GetConfig();
            if (config == null)
            {
                throw new ConfigurationErrorsException("Unable to locate <ConfigItem> section into your configuration file.");
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
        /// Initializes a new instance of the <see cref="StackExchangeRedisCache" /> class.
        /// </summary>
        /// <param name="serializer">The serializer.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="database">The database.</param>
        /// <exception cref="System.ArgumentNullException">serializer</exception>
        public StackExchangeRedisCache(ISerializer serializer, string connectionString, int database = 0)
        {
            if (serializer == null)
            {
                throw new ArgumentNullException("serializer");
            }

            this._serializer = serializer;
            this._connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
            _db = _connectionMultiplexer.GetDatabase(database);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StackExchangeRedisCache" /> class.
        /// </summary>
        /// <param name="connectionMultiplexer">The connection multiplexer.</param>
        /// <param name="serializer">The serializer.</param>
        /// <param name="database">The database.</param>
        /// <exception cref="System.ArgumentNullException">
        /// connectionMultiplexer
        /// or
        /// serializer
        /// </exception>
        public StackExchangeRedisCache(ConnectionMultiplexer connectionMultiplexer, ISerializer serializer, int database = 0)
        {
            if (connectionMultiplexer == null)
            {
                throw new ArgumentNullException("connectionMultiplexer");
            }

            if (serializer == null)
            {
                throw new ArgumentNullException("serializer");
            }

            this._serializer = serializer;
            this._connectionMultiplexer = connectionMultiplexer;

            _db = connectionMultiplexer.GetDatabase(database);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _connectionMultiplexer.Dispose();
        }

        /// <summary>
        /// Return the instance of <see cref="StackExchange.Redis.IDatabase" /> used be ICacheClient implementation
        /// </summary>
        public IDatabase Database
        {
            get { return _db; }
        }

        /// <summary>
        /// Gets the serializer.
        /// </summary>
        /// <value>
        /// The serializer.
        /// </value>
        public ISerializer Serializer
        {
            get { return this._serializer; }
        }

        /// <summary>
        /// Verify that the specified cache key exists
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <returns>
        /// True if the key is present into Redis. Othwerwise False
        /// </returns>
        public bool Exists(string key)
        {
            return _db.KeyExists(key);
        }

        /// <summary>
        /// Verify that the specified cache key exists
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <returns>
        /// True if the key is present into Redis. Othwerwise False
        /// </returns>
        public Task<bool> ExistsAsync(string key)
        {
            return _db.KeyExistsAsync(key);
        }

        /// <summary>
        /// Removes the specified key from Redis Database
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// True if the key has removed. Othwerwise False
        /// </returns>
        public bool Remove(string key)
        {
            return _db.KeyDelete(key);
        }

        /// <summary>
        /// Removes the specified key from Redis Database
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// True if the key has removed. Othwerwise False
        /// </returns>
        public Task<bool> RemoveAsync(string key)
        {
            return _db.KeyDeleteAsync(key);
        }

        /// <summary>
        /// Removes all specified keys from Redis Database
        /// </summary>
        /// <param name="keys">The key.</param>
        public void Remove(IEnumerable<string> keys)
        {
            keys.ForEach(x => Remove(x));
        }

        /// <summary>
        /// Removes all specified keys from Redis Database
        /// </summary>
        /// <param name="keys">The key.</param>
        /// <returns></returns>
        public Task RemoveAsync(IEnumerable<string> keys)
        {
            return keys.ForEachAsync(RemoveAsync);
        }
        public void RemoveByPattern(string pattern = "")
        {
            var allKeys = GetCacheKeys(pattern);
            Remove(allKeys);
        }

        /// <summary>
        /// Get the object with the specified key from Redis database
        /// </summary>
        /// <typeparam name="T">The type of the expected object</typeparam>
        /// <param name="key">The cache key.</param>
        /// <returns>
        /// Null if not present, otherwise the instance of T.
        /// </returns>
        public T Get<T>(string key) where T : class
        {
            var valueBytes = _db.StringGet(key);

            if (!valueBytes.HasValue)
            {
                return default(T);
            }

            return _serializer.Deserialize<T>(valueBytes);
        }

        /// <summary>
        /// Get the object with the specified key from Redis database
        /// </summary>
        /// <typeparam name="T">The type of the expected object</typeparam>
        /// <param name="key">The cache key.</param>
        /// <returns>
        /// Null if not present, otherwise the instance of T.
        /// </returns>
        public async Task<T> GetAsync<T>(string key) where T : class
        {
            var valueBytes = await _db.StringGetAsync(key);

            if (!valueBytes.HasValue)
            {
                return default(T);
            }

            return await _serializer.DeserializeAsync<T>(valueBytes);
        }

        /// <summary>
        /// Adds the specified instance to the Redis database.
        /// </summary>
        /// <typeparam name="T">The type of the class to add to Redis</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The instance of T.</param>
        /// <returns>
        /// True if the object has been added. Otherwise false
        /// </returns>
        public bool Set<T>(string key, T value) where T : class
        {
            var entryBytes = _serializer.Serialize(value);

            return _db.StringSet(key, entryBytes);
        }

        /// <summary>
        /// Adds the specified instance to the Redis database.
        /// </summary>
        /// <typeparam name="T">The type of the class to add to Redis</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The instance of T.</param>
        /// <returns>
        /// True if the object has been added. Otherwise false
        /// </returns>
        public async Task<bool> SetAsync<T>(string key, T value) where T : class
        {
            var entryBytes = await _serializer.SerializeAsync(value);

            return await _db.StringSetAsync(key, entryBytes);
        }

        /// <summary>
        /// Replaces the object with specified key into Redis database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The instance of T</param>
        /// <returns>
        /// True if the object has been added. Otherwise false
        /// </returns>
        public bool Replace<T>(string key, T value) where T : class
        {
            return Set(key, value);
        }

        /// <summary>
        /// Replaces the object with specified key into Redis database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The instance of T</param>
        /// <returns>
        /// True if the object has been added. Otherwise false
        /// </returns>
        public Task<bool> ReplaceAsync<T>(string key, T value) where T : class
        {
            return SetAsync(key, value);
        }

        /// <summary>
        /// Adds the specified instance to the Redis database.
        /// </summary>
        /// <typeparam name="T">The type of the class to add to Redis</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The instance of T.</param>
        /// <param name="expiresAt">Expiration time.</param>
        /// <returns>
        /// True if the object has been added. Otherwise false
        /// </returns>
        public bool Set<T>(string key, T value, DateTimeOffset expiresAt) where T : class
        {
            var entryBytes = _serializer.Serialize(value);
            var expiration = expiresAt.Subtract(DateTimeOffset.Now);

            return _db.StringSet(key, entryBytes, expiration);
        }

        /// <summary>
        /// Adds the specified instance to the Redis database.
        /// </summary>
        /// <typeparam name="T">The type of the class to add to Redis</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The instance of T.</param>
        /// <param name="expiresAt">Expiration time.</param>
        /// <returns>
        /// True if the object has been added. Otherwise false
        /// </returns>
        public async Task<bool> SetAsync<T>(string key, T value, DateTimeOffset expiresAt) where T : class
        {
            var entryBytes = await _serializer.SerializeAsync(value);
            var expiration = expiresAt.Subtract(DateTimeOffset.Now);

            return await _db.StringSetAsync(key, entryBytes, expiration);
        }

        /// <summary>
        /// Replaces the object with specified key into Redis database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The instance of T</param>
        /// <param name="expiresAt">Expiration time.</param>
        /// <returns>
        /// True if the object has been added. Otherwise false
        /// </returns>
        public bool Replace<T>(string key, T value, DateTimeOffset expiresAt) where T : class
        {
            return Set(key, value, expiresAt);
        }

        /// <summary>
        /// Replaces the object with specified key into Redis database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The instance of T</param>
        /// <param name="expiresAt">Expiration time.</param>
        /// <returns>
        /// True if the object has been added. Otherwise false
        /// </returns>
        public Task<bool> ReplaceAsync<T>(string key, T value, DateTimeOffset expiresAt) where T : class
        {
            return SetAsync(key, value, expiresAt);
        }

        /// <summary>
        /// Adds the specified instance to the Redis database.
        /// </summary>
        /// <typeparam name="T">The type of the class to add to Redis</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The instance of T.</param>
        /// <param name="expiresIn">The duration of the cache using Timespan.</param>
        /// <returns>
        /// True if the object has been added. Otherwise false
        /// </returns>
        public bool Set<T>(string key, T value, TimeSpan expiresIn) where T : class
        {
            var entryBytes = _serializer.Serialize(value);

            return _db.StringSet(key, entryBytes, expiresIn);
        }

        /// <summary>
        /// Adds the specified instance to the Redis database.
        /// </summary>
        /// <typeparam name="T">The type of the class to add to Redis</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The instance of T.</param>
        /// <param name="expiresIn">The duration of the cache using Timespan.</param>
        /// <returns>
        /// True if the object has been added. Otherwise false
        /// </returns>
        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan expiresIn) where T : class
        {
            var entryBytes = await _serializer.SerializeAsync(value);

            return await _db.StringSetAsync(key, entryBytes, expiresIn);
        }

        /// <summary>
        /// Replaces the object with specified key into Redis database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The instance of T</param>
        /// <param name="expiresIn">The duration of the cache using Timespan.</param>
        /// <returns>
        /// True if the object has been added. Otherwise false
        /// </returns>
        public bool Replace<T>(string key, T value, TimeSpan expiresIn) where T : class
        {
            return Set(key, value, expiresIn);
        }

        /// <summary>
        /// Replaces the object with specified key into Redis database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The instance of T</param>
        /// <param name="expiresIn">The duration of the cache using Timespan.</param>
        /// <returns>
        /// True if the object has been added. Otherwise false
        /// </returns>
        public Task<bool> ReplaceAsync<T>(string key, T value, TimeSpan expiresIn) where T : class
        {
            return SetAsync(key, value, expiresIn);
        }
        /// <summary>
        /// Adds the specified instance to the Redis database.
        /// </summary>
        /// <typeparam name="T">The type of the class to add to Redis</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The instance of T.</param>
        /// <param name="expiration">The duration of the cache using DateTime.</param>
        /// <returns>
        /// True if the object has been added. Otherwise false
        /// </returns>
        public bool Set<T>(string key, T value, DateTime expiration) where T : class
        {
            var entryBytes = _serializer.Serialize(value);

            return _db.StringSet(key, entryBytes, expiration.Subtract(DateTime.Now));
        }

        /// <summary>
        /// Adds the specified instance to the Redis database.
        /// </summary>
        /// <typeparam name="T">The type of the class to add to Redis</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The instance of T.</param>
        /// <param name="expiration">The duration of the cache using DateTime.</param>
        /// <returns>
        /// True if the object has been added. Otherwise false
        /// </returns>
        public async Task<bool> SetAsync<T>(string key, T value, DateTime expiration) where T : class
        {
            var entryBytes = await _serializer.SerializeAsync(value);
            return await _db.StringSetAsync(key, entryBytes, expiration.Subtract(DateTime.Now));
        }

        /// <summary>
        /// Replaces the object with specified key into Redis database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The instance of T</param>
        /// <param name="expiration">The duration of the cache using DateTime.</param>
        /// <returns>
        /// True if the object has been added. Otherwise false
        /// </returns>
        public bool Replace<T>(string key, T value, DateTime expiration) where T : class
        {
            return Set(key, value, expiration);
        }

        /// <summary>
        /// Replaces the object with specified key into Redis database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The instance of T</param>
        /// <param name="expiration">The duration of the cache using DateTime.</param>
        /// <returns>
        /// True if the object has been added. Otherwise false
        /// </returns>
        public Task<bool> ReplaceAsync<T>(string key, T value, DateTime expiration) where T : class
        {
            return SetAsync(key, value, expiration);
        }
        /// <summary>
        /// Get the objects with the specified keys from Redis database with one roundtrip
        /// </summary>
        /// <typeparam name="T">The type of the expected object</typeparam>
        /// <param name="keys">The keys.</param>
        /// <returns>
        /// Empty list if there are no results, otherwise the instance of T.
        /// If a cache key is not present on Redis the specified object into the returned Dictionary will be null
        /// </returns>
        public IDictionary<string, T> GetAll<T>(IEnumerable<string> keys) where T : class
        {
            var keysList = keys.ToList();
            var redisKeys = new RedisKey[keysList.Count];
            var sb = CreateLuaScriptForMget(redisKeys, keysList);

            RedisResult[] redisResults = (RedisResult[])_db.ScriptEvaluate(sb, redisKeys);

            var result = new Dictionary<string, T>();

            for (var i = 0; i < redisResults.Count(); i++)
            {
                var obj = default(T);

                if (!redisResults[i].IsNull)
                {
                    //TODO: (byte[])redisResults[i]
                    obj = _serializer.Deserialize<T>(encoding.GetBytes(redisResults[i].ToString()));
                }
                result.Add(keysList[i], obj);
            }

            return result;
        }

        /// <summary>
        /// Get the objects with the specified keys from Redis database with one roundtrip
        /// </summary>
        /// <typeparam name="T">The type of the expected object</typeparam>
        /// <param name="keys">The keys.</param>
        /// <returns>
        /// Empty list if there are no results, otherwise the instance of T.
        /// If a cache key is not present on Redis the specified object into the returned Dictionary will be null
        /// </returns>
        public async Task<IDictionary<string, T>> GetAllAsync<T>(IEnumerable<string> keys) where T : class
        {
            var keysList = keys.ToList();
            RedisKey[] redisKeys = new RedisKey[keysList.Count];
            var sb = CreateLuaScriptForMget(redisKeys, keysList);

            var redisResults = (RedisResult[])await _db.ScriptEvaluateAsync(sb, redisKeys);

            var result = new Dictionary<string, T>();

            for (var i = 0; i < redisResults.Count(); i++)
            {
                var obj = default(T);

                if (!redisResults[i].IsNull)
                {
                    obj = await _serializer.DeserializeAsync<T>((byte[])redisResults[i]);
                }
                result.Add(keysList[i], obj);
            }

            return result;
        }

        /// <summary>
        /// Adds all.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        public bool SetAll<T>(IList<Tuple<string, T>> items) where T : class
        {
            RedisKey[] redisKeys = new RedisKey[items.Count];
            RedisValue[] redisValues = new RedisValue[items.Count];
            var sb = CreateLuaScriptForMset(redisKeys, redisValues, items);

            var redisResults = _db.ScriptEvaluate(sb, redisKeys, redisValues);

            return redisResults.ToString() == "OK";
        }

        /// <summary>
        /// Adds all asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public async Task<bool> SetAllAsync<T>(IList<Tuple<string, T>> items) where T : class
        {
            RedisKey[] redisKeys = new RedisKey[items.Count];
            RedisValue[] redisValues = new RedisValue[items.Count];
            var sb = CreateLuaScriptForMset(redisKeys, redisValues, items);

            var redisResults = await _db.ScriptEvaluateAsync(sb, redisKeys, redisValues);

            return redisResults.ToString() == "OK";
        }

        /// <summary>
        /// Run SADD command <see cref="http://redis.io/commands/sadd" />
        /// </summary>
        /// <param name="memberName">Name of the member.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool SetAdd(string memberName, string key)
        {
            return _db.SetAdd(memberName, key);
        }

        /// <summary>
        /// Run SADD command <see cref="http://redis.io/commands/sadd" />
        /// </summary>
        /// <param name="memberName">Name of the member.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public Task<bool> SetAddAsync(string memberName, string key)
        {
            return _db.SetAddAsync(memberName, key);
        }

        /// <summary>
        /// Run SMEMBERS command <see cref="http://redis.io/commands/SMEMBERS" />
        /// </summary>
        /// <param name="memberName">Name of the member.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public string[] SetMember(string memberName)
        {
            return _db.SetMembers(memberName).Select(x => x.ToString()).ToArray();
        }

        /// <summary>
        /// Run SMEMBERS command <see cref="http://redis.io/commands/SMEMBERS" />
        /// </summary>
        /// <param name="memberName">Name of the member.</param>
        /// <returns></returns>
        public async Task<string[]> SetMemberAsync(string memberName)
        {
            return (await _db.SetMembersAsync(memberName)).Select(x => x.ToString()).ToArray();
        }

        /// <summary>
        /// Searches the keys from Redis database
        /// </summary>
        /// <remarks>
        /// Consider this as a command that should only be used in production environments with extreme care. It may ruin performance when it is executed against large databases
        /// </remarks>
        /// <param name="pattern">The pattern.</param>
        /// <example>
        ///		if you want to return all keys that start with "myCacheKey" uses "myCacheKey*"
        ///		if you want to return all keys that contain with "myCacheKey" uses "*myCacheKey*"
        ///		if you want to return all keys that end with "myCacheKey" uses "*myCacheKey"
        /// </example>
        /// <returns>A list of cache keys retrieved from Redis database</returns>
        public IEnumerable<string> GetCacheKeys(string pattern)
        {
            var keys = new HashSet<RedisKey>();

            var endPoints = _db.Multiplexer.GetEndPoints();

            foreach (var endpoint in endPoints)
            {
                var dbKeys = _db.Multiplexer.GetServer(endpoint).Keys(pattern: pattern);

                foreach (var dbKey in dbKeys)
                {
                    if (!keys.Contains(dbKey))
                    {
                        keys.Add(dbKey);
                    }
                }
            }

            return keys.Select(x => (string)x);
        }

        /// <summary>
        /// Searches the keys from Redis database
        /// </summary>
        /// <remarks>
        /// Consider this as a command that should only be used in production environments with extreme care. It may ruin performance when it is executed against large databases
        /// </remarks>
        /// <param name="pattern">The pattern.</param>
        /// <example>
        ///		if you want to return all keys that start with "myCacheKey" uses "myCacheKey*"
        ///		if you want to return all keys that contain with "myCacheKey" uses "*myCacheKey*"
        ///		if you want to return all keys that end with "myCacheKey" uses "*myCacheKey"
        /// </example>
        /// <returns>A list of cache keys retrieved from Redis database</returns>
        public Task<IEnumerable<string>> GetCacheKeysAsync(string pattern)
        {
            return Task.Factory.StartNew(() => GetCacheKeys(pattern));
        }

        public void FlushDb()
        {
            var endPoints = _db.Multiplexer.GetEndPoints();

            foreach (var endpoint in endPoints)
            {
                _db.Multiplexer.GetServer(endpoint).FlushDatabase(_db.Database);
            }
        }

        public async Task FlushDbAsync()
        {
            var endPoints = _db.Multiplexer.GetEndPoints();

            foreach (var endpoint in endPoints)
            {
                await _db.Multiplexer.GetServer(endpoint).FlushDatabaseAsync(_db.Database);
            }
        }

        public void Save(SaveType saveType)
        {
            var endPoints = _db.Multiplexer.GetEndPoints();

            foreach (var endpoint in endPoints)
            {
                _db.Multiplexer.GetServer(endpoint).Save(saveType);
            }
        }

        public async void SaveAsync(SaveType saveType)
        {
            var endPoints = _db.Multiplexer.GetEndPoints();

            foreach (var endpoint in endPoints)
            {
                await _db.Multiplexer.GetServer(endpoint).SaveAsync(saveType);
            }
        }

        public Dictionary<string, string> GetInfo()
        {
            var info = _db.ScriptEvaluate("return redis.call('INFO')").ToString();

            return ParseInfo(info);
        }

        public async Task<Dictionary<string, string>> GetInfoAsync()
        {
            var info = (await _db.ScriptEvaluateAsync("return redis.call('INFO')")).ToString();

            return ParseInfo(info);
        }

        private string CreateLuaScriptForMset<T>(RedisKey[] redisKeys, RedisValue[] redisValues, IList<Tuple<string, T>> objects)
        {
            var sb = new StringBuilder("return redis.call('mset',");

            for (var i = 0; i < objects.Count; i++)
            {
                redisKeys[i] = objects[i].Item1;
                redisValues[i] = this._serializer.Serialize(objects[i].Item2);

                sb.AppendFormat("KEYS[{0}],ARGV[{0}]", i + 1);

                if (i < objects.Count - 1)
                {
                    sb.Append(",");
                }
            }

            sb.Append(")");

            return sb.ToString();
        }

        private string CreateLuaScriptForMget(RedisKey[] redisKeys, List<string> keysList)
        {
            var sb = new StringBuilder("return redis.call('mget',");

            for (var i = 0; i < keysList.Count; i++)
            {
                redisKeys[i] = keysList[i];
                sb.AppendFormat("KEYS[{0}]", i + 1);

                if (i < keysList.Count - 1)
                {
                    sb.Append(",");
                }
            }

            sb.Append(")");

            return sb.ToString();
        }

        private Dictionary<string, string> ParseInfo(string info)
        {
            string[] lines = info.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var data = new Dictionary<string, string>();
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                if (string.IsNullOrEmpty(line) || line[0] == '#')
                {
                    // 2.6+ can have empty lines, and comment lines
                    continue;
                }

                int idx = line.IndexOf(':');
                if (idx > 0) // double check this line looks about right
                {
                    var key = line.Substring(0, idx);
                    var infoValue = line.Substring(idx + 1).Trim();

                    data.Add(key, infoValue);
                }
            }

            return data;
        }
    }
}

