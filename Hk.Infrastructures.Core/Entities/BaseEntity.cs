using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Hk.Infrastructures.Common.Utility;

namespace Hk.Infrastructures.Core.Entities
{
    [Serializable]
    public class BaseEntity : ICloneable
    {
        /// <summary>
        /// 唯一标识ID
        /// </summary>
        public string IdentityId
        { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateUserId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateDateTime { get; set; }

        /// <summary>
        /// 是否删除 0 正常  1删除
        /// </summary>
        public BaseEntity()
        {
            IdentityId = Identity.GenerateId();
        }
        public bool IsDelete { get; set; }
        public object Clone()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
                bf.Serialize(ms, this);
                ms.Seek(0, SeekOrigin.Begin);
                // 反序列化至另一个对象(即创建了一个原对象的深表副本) 
                object cloneObject = bf.Deserialize(ms);
                // Close stream
                ms.Close();
                return cloneObject;
            }
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as BaseEntity);
        }

        private static bool IsTransient(BaseEntity obj)
        {
            return obj != null && Equals(obj.IdentityId, default(string));
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(BaseEntity other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (!IsTransient(this) &&
                !IsTransient(other) &&
                Equals(IdentityId, other.IdentityId))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                        otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (Equals(IdentityId, default(string)))
                return base.GetHashCode();
            return IdentityId.GetHashCode();
        }

        public static bool operator ==(BaseEntity x, BaseEntity y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(BaseEntity x, BaseEntity y)
        {
            return !(x == y);
        }
    }
}

