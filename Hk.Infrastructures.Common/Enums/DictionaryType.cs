using System.ComponentModel;
namespace Hk.Infrastructures.Common.Enums
{
    /// <summary>
    /// 数据字典类型
    /// </summary>
    public enum DictionaryType
    {
        /// <summary>
        /// 短信供应商
        /// </summary>
        [Description("短信供应商")]
        MessageSupplier = 1,
        /// <summary>
        /// 随访计划提醒时间类型
        /// </summary>
        [Description("随访计划提醒时间类型")]
        ScheduleRemindType = 0,
        /// <summary>
        /// 医生职称
        /// </summary>
        [Description("医生职称")]
        ProfessionalTitle = 2,
        /// <summary>
        /// 身份证枚举
        /// </summary>
        [Description("身份证枚举")]
        CertificateType = 3,

        /// <summary>
        /// 平台类型
        /// </summary>
        [Description("平台类型")]
        PlatformType = 4,

        /// <summary>
        /// 医院类型
        /// </summary>
        [Description("医院类型")]
        HospitalType = 5,
        /// <summary>
        /// 渠道值
        /// </summary>
        [Description("渠道值")]
        CooperationSourceType=6
    }
    /// <summary>
    /// 区域配置类型
    /// </summary>
    public enum AreaConfigurationModuleType
    {
        /// <summary>
        /// 开通预约服务城市
        /// </summary>
        [Description("开通预约服务城市")]
        OpenAppointmentServiceCity = 1

    }
    /// <summary>
    /// 区域级别
    /// </summary>
    public enum AreaLevel
    {
        /// <summary>
        /// 省份
        /// </summary>
        [Description("省份")]
        Province = 0,

        /// <summary>
        /// 城市
        /// </summary>
        [Description("城市")]
        City = 1,

        /// <summary>
        /// 区县
        /// </summary>
        [Description("区县")]
        Region = 2
    }
}
