using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Hk.Infrastructures.Config;
namespace Hk.Infrastructures.Caching.Configs
{
    /// <summary>
    /// 业务配置信息类文件
    /// </summary>
    [Serializable]
    public class ConfigItem : IConfigItem
    {
        [XmlArray(ElementName = "MappingGroups")]
        [XmlArrayItem(ElementName = "Group")]
        public List<MappingGroup> MappingGroups { get; set; }
    }

    [Serializable]
    public class MappingGroup
    {
        [XmlAttribute(AttributeName = "name")]
        public string GroupName { get; set; }

        [XmlArray(ElementName = "Mappings")]
        [XmlArrayItem(ElementName = "Mapping")]
        public List<MappingItem> Mappings { get; set; }
    }

    [Serializable]
    public class MappingItem
    {
        [XmlAttribute(AttributeName = "methodName")]
        public string MethodName { get; set; }

        [XmlAttribute(AttributeName = "cacheKey")]
        public string CacheKey { get; set; }

        [XmlAttribute(AttributeName = "cacheTime")]
        public int CacheTime { get; set; }

        [XmlAttribute(AttributeName = "isEnabled")]
        public bool IsEnable { get; set; }
    }
}
