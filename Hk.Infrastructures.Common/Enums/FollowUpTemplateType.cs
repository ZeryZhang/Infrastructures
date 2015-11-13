using System.ComponentModel;
namespace Hk.Infrastructures.Common.Enums
{
    /// <summary>
    /// 随访模板类型
    /// </summary>
    public enum FollowUpTemplateType
    {
        /// <summary>
        /// 系统模板
        /// </summary>
        SystemTemplate = 0,

        /// <summary>
        /// 医生模板
        /// </summary>
        DoctorUserTemplate = 1
    }
    /// <summary>
    /// 随访模板周期日期类型
    /// </summary>
    public enum FollowUpTemplateItemDateType
    {
        /// <summary>
        /// 天
        /// </summary>
        [Description("天")]
        Day = 1,
        /// <summary>
        /// 周
        /// </summary>
        [Description("周")]
        Week = 2,
        /// <summary>
        /// 月
        /// </summary>
        [Description("月")]
        Month = 3
    }

}
