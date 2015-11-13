using System;
using Hk.Infrastructures.Common.Serializer;
namespace Hk.Infrastructures.Config
{
    /// <summary>
    /// 文件配置管理基类
    /// </summary>
    public class BaseConfigFileManager
    {

        public static event EventHandler<ConfigChangedEventArgs> ConfigChanged;

        /// <summary>
        /// 文件所在路径变量
        /// </summary>
        private static string _configfilepath;

        /// <summary>
        /// 临时配置对象变量
        /// </summary>
        private static IConfigItem _configItem = null;

        /// <summary>
        /// 锁对象
        /// </summary>
        private static object _lockObject = new object();

        /// <summary>
        /// 文件所在路径
        /// </summary>
        public static string ConfigFilePath
        {
            get { return _configfilepath; }
            set { _configfilepath = value; }
        }

        /// <summary>
        /// 临时配置对象
        /// </summary>
        public static IConfigItem ConfigItem
        {
            get { return _configItem; }
            set { _configItem = value; }
        }

        /// <summary>
        /// 加载(反序列化)指定对象类型的配置对象
        /// </summary>
        /// <param name="fileoldchange">文件加载时间</param>
        /// <param name="configFilePath">配置文件所在路径</param>
        /// <param name="configItem">相应的变量 注:该参数主要用于设置m_configinfo变量 和 获取类型.GetType()</param>
        /// <returns></returns>
        protected static IConfigItem LoadConfig(ref DateTime fileoldchange, string configFilePath, IConfigItem configItem)
        {
            return LoadConfig(ref fileoldchange, configFilePath, configItem, true);
        }


        /// <summary>
        /// 加载(反序列化)指定对象类型的配置对象
        /// </summary>
        /// <param name="fileoldchange">文件加载时间</param>
        /// <param name="configFilePath">配置文件所在路径(包括文件名)</param>
        /// <param name="configItem">相应的变量 注:该参数主要用于设置m_configinfo变量 和 获取类型.GetType()</param>
        /// <param name="checkTime">是否检查并更新传递进来的"文件加载时间"变量</param>
        /// <returns></returns>
        protected static IConfigItem LoadConfig(ref DateTime fileoldchange, string configFilePath, IConfigItem configItem, bool checkTime)
        {
            lock (_lockObject)
            {
                _configfilepath = configFilePath;
                _configItem = configItem;

                if (checkTime)
                {
                    DateTime fileNewChange = System.IO.File.GetLastWriteTime(configFilePath);

                    //当程序运行中config文件发生变化时则对config重新赋值
                    if (fileoldchange != fileNewChange)
                    {
                        fileoldchange = fileNewChange;
                        _configItem = Deserialize(configFilePath, configItem.GetType());
                        OnConfigChanged(new ConfigChangedEventArgs());

                    }
                }
                else
                {
                    _configItem = Deserialize(configFilePath, configItem.GetType());
                }

                return _configItem;
            }
        }


        /// <summary>
        /// 反序列化指定的类
        /// </summary>
        /// <param name="configfilepath">config 文件的路径</param>
        /// <param name="configtype">相应的类型</param>
        /// <returns></returns>
        public static IConfigItem Deserialize(string configfilepath, Type configtype)
        {
            ObjectSerializerClient serializerClient=new ObjectSerializerClient(SerializerType.Xml);
            return (IConfigItem)serializerClient.GetObjectSerializer.Deserialize(configtype,configfilepath);
        }

        /// <summary>
        /// 保存配置实例(虚方法需继承)
        /// </summary>
        /// <returns></returns>
        public virtual bool SaveConfig()
        {
            return true;
        }

        /// <summary>
        /// 保存(序列化)指定路径下的配置文件
        /// </summary>
        /// <param name="configFilePath">指定的配置文件所在的路径(包括文件名)</param>
        /// <param name="configItem">被保存(序列化)的对象</param>
        /// <returns></returns>
        public bool SaveConfig(string configFilePath, IConfigItem configItem)
        {
            ObjectSerializerClient serializerClient = new ObjectSerializerClient(SerializerType.Xml);
            return serializerClient.GetObjectSerializer.Serialize(configItem, configFilePath);
        }

        private static void OnConfigChanged(ConfigChangedEventArgs e)
        {
            #region 本地事件通知

            if (ConfigChanged != null)
            {
                ConfigChanged(null, e);
            }

            #endregion
        }
    }
}

