using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Hk.Infrastructures.Config;

namespace Hk.Infrastructures.MessageBus.Configs
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

        [XmlArray(ElementName = "Subscribers")]
        [XmlArrayItem(ElementName = "subscriber")]
        public List<Subscriber> Subscribers { get; set; }

        public Publisher Publisher { get; set; }
    }

    public class Subscriber
    {
        [XmlAttribute(AttributeName = "exchange")]
        public string Exchange { get; set; }

        [XmlAttribute(AttributeName = "queue")]
        public string Queue { get; set; }

        [XmlAttribute(AttributeName = "topic")]
        public string Topic { get; set; }

        [XmlAttribute(AttributeName = "publisher")]
        public string Publisher { get; set; }
    }

    public class Publisher
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlArray(ElementName = "connectionProperties")]
        [XmlArrayItem(ElementName = "property")]
        public List<PublisherConnectionProperty> ConnectionProperties { get; set; }
    }

    public class PublisherConnectionProperty
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }
}


