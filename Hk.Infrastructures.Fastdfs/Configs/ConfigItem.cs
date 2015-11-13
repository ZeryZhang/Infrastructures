using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Hk.Infrastructures.Config;
namespace Hk.Infrastructures.Fastdfs.Configs
{
    [Serializable]
    public class ConfigItem : IConfigItem
    {
        [XmlArrayItem(ElementName = "FileGroup")]
        public List<FileGroup> FileGroups { get; set; }
        [XmlArrayItem(ElementName = "TrackerServer")]
        public List<TrackerServer> TrackerServers { get; set; }
    }
    [Serializable]
    public class FileGroup
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlArrayItem(ElementName = "StorageServer")]
        public List<StorageServer> StorageServers { get; set; }
    }
    [Serializable]
    public class StorageServer
    {
        [XmlAttribute(AttributeName = "domainName")]
        public string DomainName { get; set; }
        [XmlAttribute(AttributeName = "address")]
        public string Address { get; set; }
    }
    [Serializable]
    public class TrackerServer
    {
        [XmlAttribute(AttributeName = "domainName")]
        public string DomainName { get; set; }
        [XmlAttribute(AttributeName = "address")]
        public string Address { get; set; }
        [XmlAttribute(AttributeName = "port")]
        public int Port { get; set; }
    }
}

