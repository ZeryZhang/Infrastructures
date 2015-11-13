using System;

namespace Hk.Infrastructures.Common.Serializer
{
    public class ObjectSerializerClient
    {
        public IObjectSerializer GetObjectSerializer=null;

        public ObjectSerializerClient(SerializerType serializerType)
        {
            GetObjectSerializer = CreateObjectSerializer(serializerType);
        }

        private IObjectSerializer CreateObjectSerializer(SerializerType serializerType)
        {
            IObjectSerializer serializer = null;
            switch (serializerType)
            {
                case SerializerType.Xml:
                    serializer=new ObjectXmlSerializer();
                    break;
                default:
                    throw new Exception("不存在该SerializerType");
            }
            return serializer;
        }
    }

    public enum SerializerType
    {
        Xml = 1,
        Json = 2
    }
}
