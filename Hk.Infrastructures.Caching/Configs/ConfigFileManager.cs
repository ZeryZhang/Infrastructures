using System;
using System.IO;
using Hk.Infrastructures.Config;

namespace Hk.Infrastructures.Caching.Configs
{
    public class ConfigFileManager : BaseConfigFileManager
    {
        private static ConfigItem _configItem;

        /// <summary>
        /// 文件修改时间
        /// </summary>
        private static DateTime _fileOldChange;

        /// <summary>
        /// 初始化文件修改时间和对象实例
        /// </summary>
        static ConfigFileManager()
        {
            if (System.IO.File.Exists(ConfigFilePath))
            {
                _fileOldChange = System.IO.File.GetLastWriteTime(ConfigFilePath);
                _configItem = (ConfigItem)BaseConfigFileManager.Deserialize(ConfigFilePath, typeof(ConfigItem));
            }
        }

        /// <summary>
        /// 当前的配置类实例
        /// </summary>
        public new static IConfigItem ConfigItem
        {
            get { return _configItem; }
            set { _configItem = (ConfigItem)value; }
        }

        /// <summary>
        /// 配置文件所在路径
        /// </summary>
        public static string FileName = null;


        /// <summary>
        /// 获取配置文件所在路径
        /// </summary>
        public new static string ConfigFilePath
        {
            get
            {
                if (FileName == null)
                {
                    FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "DataCacheMapping.config");
                }

                return FileName;
            }
        }

        /// <summary>
        /// 返回配置类实例
        /// </summary>
        /// <returns></returns>
        public static ConfigItem LoadConfig()
        {
            if (System.IO.File.Exists(ConfigFilePath))
            {
                ConfigItem = BaseConfigFileManager.LoadConfig(ref _fileOldChange, ConfigFilePath, ConfigItem);
                return ConfigItem as ConfigItem;
            }
            else
                return null;
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <returns></returns>
        public override bool SaveConfig()
        {
            return base.SaveConfig(ConfigFilePath, ConfigItem);
        }
    }
}


