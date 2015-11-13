using System.ComponentModel;

namespace Hk.Infrastructures.Common.Enums
{
    /// <summary>
    /// 医生服务模式
    /// </summary>
    public enum ServiceChargeMode
    {
        /// <summary>
        /// 免费咨询次数
        /// </summary>
        [Description("免费咨询次数")]
        FreeConsultationsLimited = 1,

        /// <summary>
        /// 按次咨询
        /// </summary>
        [Description("按次咨询")]
        OneTimeServiceSetting = 2,

        /// <summary>
        /// 按次咨询
        /// </summary>
        [Description("按月咨询")]
        MonthlyServiceSetting = 3
    }
}
