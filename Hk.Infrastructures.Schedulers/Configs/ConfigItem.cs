using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Hk.Infrastructures.Config;
namespace Hk.Infrastructures.Schedulers.Configs
{
    [Serializable]
    public class ConfigItem : IConfigItem
    {
        [XmlArray(ElementName = "properties")]
        [XmlArrayItem(ElementName = "property")]
        public List<QuartzPropertyItem> Properties { get; set; }
    }

    public class QuartzPropertyItem
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }
}
