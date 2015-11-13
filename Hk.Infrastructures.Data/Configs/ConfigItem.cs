using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Hk.Infrastructures.Config;
namespace Hk.Infrastructures.Data.Configs
{
     [Serializable]
    public class ConfigItem : IConfigItem
    {
        [XmlArray(ElementName = "ConnectionStringGroups")]
        [XmlArrayItem(ElementName = "ConnectionStringGroup")]
         public List<ConnectionStringGroup> ConnectionStringGroups { get; set; }   
    }

    [Serializable]
     public class ConnectionStringGroup
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "WriteConnectionString")]
        public ConnectionStringItem WriteConnectionStringItem { get; set; }
        [XmlElement(ElementName = "ReadConnectionString")]
        public ConnectionStringItem ReadConnectionStringItem { get; set; }
    }

    [Serializable]
    public class ConnectionStringItem
    {
        [XmlAttribute(AttributeName = "value")]
        public string ConnectionString { get; set; }

        [XmlAttribute(AttributeName = "dbType")]
        public SqlDbType SqlDbType { get; set; }
    }


}

