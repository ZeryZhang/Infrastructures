using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Hk.Infrastructures.Config;
namespace Hk.Infrastructures.Redis.Configs
{
    [Serializable]
    public class ConfigItem : IConfigItem
    {
        [XmlAttribute(AttributeName = "allowAdmin")]
        public bool AllowAdmin { get; set; }

        [XmlAttribute(AttributeName = "ssl")]
        public bool Ssl { get; set; }

        [XmlAttribute(AttributeName = "connectTimeout")]
        public int ConnectTimeout { get; set; }

        [XmlAttribute(AttributeName = "database")]
        public int Database { get; set; }      

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlArray(ElementName = "Hosts")]
        [XmlArrayItem(ElementName = "Host")]
        public List<Host> Hosts { get; set; }      
    }
    public class Host
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "ip")]
        public string Ip { get; set; }
        [XmlAttribute(AttributeName = "port")]
        public int Port { get; set; }
    }
}

