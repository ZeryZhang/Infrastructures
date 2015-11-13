namespace Hk.Infrastructures.Common.Enums
{
    /// <summary>
    /// App升级模式
    /// </summary>
    public enum AppUpgradeMode
    {
        /// <summary>
        /// 强制升级
        /// </summary>
        Enforcement = 1,
        /// <summary>
        /// 可选
        /// </summary>
        Choose = 2,
        /// <summary>
        /// 弹窗
        /// </summary>
        Pop = 3,
        /// <summary>
        /// 提醒一次
        /// </summary>
        Once = 4
    }
}
