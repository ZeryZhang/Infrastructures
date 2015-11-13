using System;
using System.IO;
using System.Xml.Serialization;
namespace Hk.Infrastructures.Common.Serializer
{
    /// <summary>
    /// Represents the Json serializer.
    /// </summary>
    internal class ObjectXmlSerializer : IObjectSerializer
    {
        #region IObjectSerializer Members

        /// <summary>
        /// Serializes an object into a byte stream.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="obj">The object to be serialized.</param>
        /// <returns>The byte stream which contains the serialized data.</returns>
        public virtual byte[] Serialize<TObject>(TObject obj)
        {
            byte[] ret = null;
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                XmlSerializer xmlSerializer =new XmlSerializer(obj.GetType());
                xmlSerializer.Serialize(ms, obj);
                ret = ms.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                }
            }
            return ret;
        }

        public virtual bool Serialize(object obj, string filename)
        {
            bool success = false;
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
                xmlSerializer.Serialize(fs, obj);
                success = true;
            }
            catch (Exception ex)
            {              
                throw ex;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return success;
        }

        /// <summary>
        /// Deserializes an object from the given byte stream.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="stream">The byte stream which contains the serialized data of the object.</param>
        /// <returns>The deserialized object.</returns>
        public virtual TObject Deserialize<TObject>(byte[] stream)
        {
            MemoryStream ms = null;
            TObject ret;
            try
            {
                ms = new MemoryStream(stream);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof (TObject));
                ret = (TObject) xmlSerializer.Deserialize(ms);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                }
            }
            return ret;
        }

        public virtual TObject Deserialize<TObject>(string filename)
        {
            FileStream fs = null;
            TObject ret;
            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof (TObject));               
                ret = (TObject) xmlSerializer.Deserialize(fs);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return ret;
        }

        public virtual object Deserialize(Type type, string filename)
        {
            FileStream fs = null;
            object ret = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type);
                ret= serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return ret;
        }
        #endregion
    }
}
