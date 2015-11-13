using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Hk.Infrastructures.Config;
namespace Hk.Infrastructures.Mongo.Configs
{
    [Serializable]
    public class ConfigItem : IConfigItem
    {
        [XmlArray(ElementName = "ConnectionStrings")]
        [XmlArrayItem(ElementName = "ConnectionString")]
        public List<ConnectionString> ConnectionStrings { get; set; }
    }
    [Serializable]
    public class ConnectionString
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "value")]
        public string ConnectionStringValue { get; set; }
    }
}

