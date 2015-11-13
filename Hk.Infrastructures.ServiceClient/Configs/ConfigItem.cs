using System;
using System.Xml.Serialization;
using Hk.Infrastructures.Config;

namespace Hk.Infrastructures.ServiceClient.Configs
{
   [Serializable]
    public class ConfigItem : IConfigItem
    {
        [XmlAttribute(AttributeName = "timeOut")]
        public int TimeOut { get; set; }

        [XmlAttribute(AttributeName = "retryCount")]
        public int RetryCount { get; set; }

        [XmlAttribute(AttributeName = "isEnabled")]
        public bool IsEnabled { get; set; }

        [XmlAttribute(AttributeName = "url")]
        public string Url { get; set; }
    }
}


