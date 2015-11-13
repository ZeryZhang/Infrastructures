using System;
using System.Xml.Serialization;

namespace Hk.Infrastructures.Config
{
    public class KeyValueItem
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }
}
