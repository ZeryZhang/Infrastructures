using System;
namespace Hk.Infrastructures.Data.Configs
{
   public class Config
    {
        private static System.Timers.Timer _configTimer = new System.Timers.Timer(1200000); //间隔为10分钟

        private static ConfigItem _configItem;

        static Config()
        {
            _configItem = ConfigFileManager.LoadConfig();
            _configTimer.AutoReset = true;
            _configTimer.Enabled = true;
            _configTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            _configTimer.Start();
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ResetConfig();
        }


        /// <summary>
        /// 重设配置类实例
        /// </summary>
        public static void ResetConfig()
        {
            _configItem = ConfigFileManager.LoadConfig();
        }

        /// <summary>
        /// 获取配置类实例
        /// </summary>
        /// <returns></returns>
        public static ConfigItem GetConfig()
        {
            return _configItem;
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <param name="configItem"></param>
        /// <returns></returns>
        public static bool SaveConfig(ConfigItem configItem)
        {
            ConfigFileManager rcfm = new ConfigFileManager();
            ConfigFileManager.ConfigItem = configItem;
            return rcfm.SaveConfig();
        }
    }
}
