using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Hk.Infrastructures.Caching.Configs;
using Hk.Infrastructures.Common.Extensions;
using Hk.Infrastructures.Logging;
using Hk.Infrastructures.Redis;
using Newtonsoft.Json;

namespace Hk.Infrastructures.Caching
{
    public class CacheRepository
    {
        #region Fields

        private const string CacheTagPrefix = "Hk_Cache_Tag_{0}";
        private static  ConcurrentDictionary<string, MappingItem> _mappingContainer=new ConcurrentDictionary<string, MappingItem>();
        private static Hk.Infrastructures.Redis.ICache _cacheClient=new StackExchangeRedisCache();    
        #endregion

        #region Ctor

        public CacheRepository(string groupName)
        {
            InitMappingContainer(groupName);
        }

        #endregion

        #region public Methods
        public MappingItem GetCacheConfig(string methodName)
        {
            MappingItem result = null;
            if (!string.IsNullOrWhiteSpace(methodName) && _mappingContainer.IsNotNull())
            {
                if (_mappingContainer.ContainsKey(methodName))
                {
                    result = _mappingContainer[methodName];
                }
            }
            return result;
        }

        public void AddDataToCache<T>(string methodName, T data, params object[] cacheKeyParams)
        {
            var config = this.GetCacheConfig(methodName);
            if (config == null || !config.IsEnable || data == null)
            {
                return;
            }

            var cacheKey = (cacheKeyParams != null && cacheKeyParams.Length > 0) ? string.Format(config.CacheKey, cacheKeyParams) : config.CacheKey;
            this.AddDataToCache(config, cacheKey, data);
        }


        public void RemoveDataByMethodName(string methodName, params object[] param)
        {
            var config = this.GetCacheConfig(methodName);
            if (config != null)
            {
                if (config.IsEnable)
                {
                    var cacheKey = (param != null && param.Length > 0) ? string.Format(config.CacheKey, param) : config.CacheKey;
                    _cacheClient.Remove(cacheKey);  
                }
            }
        }

        public void RemoveDataFromCache(string[] cacheKeys = null, params string[] cacheTags)
        {
            if (cacheKeys.IsNotNull())
            {
                _cacheClient.Remove(cacheKeys.ToList());
            }

            if (cacheTags.IsNotNull())
            {
                Parallel.ForEach(cacheTags, tag =>
                {
                    var cacheKeysOfTag = _cacheClient.Get<HashSet<string>>(string.Format(CacheTagPrefix, tag));
                    if (cacheKeysOfTag != null)
                    {
                        _cacheClient.Remove(cacheKeysOfTag.ToList());
                    }
                });
            }
        }

        /// <summary>
        /// 模糊匹配清除缓存数据
        /// </summary>
        /// <param name="pattern"></param>
        public void RemoveDataFromCache(string pattern = "")
        {
            _cacheClient.RemoveByPattern(pattern);
        }

        /// <summary>
        /// 查询缓存中的数据（如果没有则查询数据库中的数据）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbAccess"></param>
        /// <param name="cacheTags"></param>
        /// <param name="keyParams"></param>
        /// <returns></returns>
        public T GetCacheData<T>(Func<T> dbAccess, string[] cacheTags = null, params object[] keyParams)
        {
            T result = default(T);

            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            if (stackFrame != null)
            {
                MethodBase methodBase = stackFrame.GetMethod();

                var config = this.GetCacheConfig(methodBase.Name);
                if (config == null)
                {
                    result = TryCatchFunction<T>(dbAccess);
                }
                else
                {
                    if (config.IsEnable)
                    {
                        var cacheKey = keyParams == null ? config.CacheKey : string.Format(config.CacheKey, keyParams);
                        var cacheData = GetDataFromCache<T>(config, cacheKey);

                        if (cacheData != null)
                        {
                            result = cacheData;
                        }
                        else
                        {
                            result = TryCatchFunction<T>(dbAccess);
                            AddDataToCache<T>(config, cacheKey, result);
                            AddCacheKeyWithTags(config, cacheTags, cacheKey);
                        }
                    }
                    else
                    {
                        result = TryCatchFunction<T>(dbAccess);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 查询缓存中的数据（如果没有则查询数据库中的数据）
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="methodName">配置名</param>
        /// <param name="isNotNull">判断已从缓存取到数据</param>
        /// <param name="doing">方法执行体</param>
        /// <param name="args">配置参数</param>
        /// <returns></returns>
        public T GetCacheData<T>(string methodName, Func<T, bool> isNotNull, Func<T> doing, params object[] args)
        {
            var config = this.GetCacheConfig(methodName);
            if (config == null || !config.IsEnable)
            {
                return doing();
            }

            var cacheKey = string.Format(config.CacheKey, args);

            var cacheData = GetDataFromCache<T>(config, cacheKey);

            if (isNotNull(cacheData))
            {
                return cacheData;
            }
            else
            {
                var result = doing();
                AddDataToCache<T>(config, cacheKey, result);
                return result;
            }
        }

        #endregion

        #region private Methods

        private void AddCacheKeyWithTags(MappingItem config, string[] cacheTags, string cacheKey)
        {
            if (config != null && !string.IsNullOrWhiteSpace(cacheKey) && config.IsEnable && cacheTags.IsNotNull())
            {
                foreach (var tag in cacheTags)
                {
                    var tagCacheKey = string.Format(CacheTagPrefix, tag);
                    var cacheData = _cacheClient.Get<HashSet<string>>(tagCacheKey);
                    if (cacheData != null)
                    {
                        if (!cacheData.Contains(cacheKey))
                        {
                            cacheData.Add(cacheKey);
                            _cacheClient.Set(tagCacheKey, cacheData, DateTime.MaxValue);
                        }
                    }
                    else
                    {
                        cacheData = new HashSet<string>();
                        cacheData.Add(cacheKey);
                        _cacheClient.Set(tagCacheKey, cacheData, DateTime.MaxValue);
                    }
                }

            }
        }
        private void AddDataToCache<T>(MappingItem config, string cacheKey, T data)
        {
            if (config != null && data != null && !string.IsNullOrWhiteSpace(cacheKey))
            {
                if (data is ICollection)
                {
                    var dataCollection = (ICollection)data;
                    if (dataCollection != null)
                    {
                        _cacheClient.Set(cacheKey, JsonConvert.SerializeObject(data), DateTime.Now.AddMinutes(config.CacheTime));
                    }
                }
                else
                {
                    _cacheClient.Set(cacheKey, JsonConvert.SerializeObject(data), DateTime.Now.AddMinutes(config.CacheTime));
                }
            }
        }
        private T GetDataFromCache<T>(MappingItem config, string cacheKey)
        {
            T result = default(T);
            if (config != null && !string.IsNullOrWhiteSpace(cacheKey))
            {
                var jsonResult = _cacheClient.Get<string>(cacheKey);
                if (jsonResult != null)
                {
                    result = JsonConvert.DeserializeObject<T>(jsonResult);
                }
            }
            return result;
        }
        private T TryCatchFunction<T>(Func<T> function)
        {
            try { return function(); }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void InitMappingContainer(string groupName)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(groupName))
                {
                    var configInfo = Configs.Config.GetConfig();
                    if (configInfo != null && configInfo.MappingGroups.IsNotNull())
                    {
                        var groupInfo =
                            configInfo.MappingGroups.FirstOrDefault(
                                u => String.CompareOrdinal(u.GroupName, groupName) == 0);
                        if (groupInfo != null && groupInfo.Mappings.Count > 0)
                        {
                            foreach (var mapping in groupInfo.Mappings)
                            {
                                if (mapping != null && !_mappingContainer.ContainsKey(mapping.MethodName) &&
                                    !string.IsNullOrWhiteSpace(mapping.CacheKey))
                                {
                                    _mappingContainer.TryAdd(mapping.MethodName, mapping);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerClient.WriteLog()
                    .Error(-1, "Hk.Infrastructures.Caching.CacheRepository.InitMappingContainer", "V1.0", ex, ex.Message);
            }
        }

        #endregion

    }
}
