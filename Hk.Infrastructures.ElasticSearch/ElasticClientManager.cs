using System;
using System.Collections.Concurrent;
using System.Linq;
using Nest;

namespace Hk.Infrastructures.ElasticSearch
{
    public static class ElasticClientManager
    {
        private static ConcurrentDictionary<string, IElasticClient> _elasticClientContainer =
            new ConcurrentDictionary<string, IElasticClient>();

        public static IElasticClient GetElasticClient(string indexName, string hostUrl = null)
        {
            IElasticClient result = null;
            if (!string.IsNullOrWhiteSpace(indexName))
            {
                if (_elasticClientContainer.ContainsKey(indexName))
                {
                    result = _elasticClientContainer[indexName];
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(hostUrl))
                    {
                        var config = Configs.Config.GetConfig();
                        if (config != null
                            && config.ElasticSearchIndexes != null
                            && config.ElasticSearchIndexes.Count > 0)
                        {
                            var indexInfo = config.ElasticSearchIndexes.FirstOrDefault(u => u.Name == indexName);
                            if (indexInfo != null)
                            {
                                hostUrl = indexInfo.HostUrl;
                            }
                        }
                    }
                    result = new ElasticClient(Settings(hostUrl,indexName));
                    _elasticClientContainer.TryAdd(indexName, result);
                }

            }
            return result;
        }

        private static ConnectionSettings Settings(string hostUrl,string indexName)
        {
            return new ConnectionSettings(new Uri(hostUrl), indexName.ToLower());
        }
    }
}
