using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Hk.Infrastructures.Config;

namespace Hk.Infrastructures.ElasticSearch.Configs
{
    [Serializable]
    public class ConfigItem : IConfigItem
    {
        [XmlArray(ElementName = "Indexes")]
        [XmlArrayItem(ElementName = "Index")]
        public List<ElasticSearchIndex> ElasticSearchIndexes { get; set; }
    }

    [Serializable]
    public class ElasticSearchIndex
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "hostUrl")]
        public string HostUrl { get; set; }
    }
}

