using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Hk.Infrastructures.Config;

namespace Hk.Infrastructures.Localization.Configs
{
    [Serializable]
    public class ConfigItem : IConfigItem
    {
        [XmlArray(ElementName = "MessageCodes")]
        [XmlArrayItem(ElementName = "MessageCode")]
        public List<MessageCodeItem> MessageCodes { get; set; }
    }

    public class MessageCodeItem
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }
}


