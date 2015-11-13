using System.ComponentModel;
namespace Hk.Infrastructures.Common.Enums
{
    /// <summary>
    /// 患者用户收藏类型
    /// </summary>
    public enum UserCollectedType
    {
        /// <summary>
        /// 医生
        /// </summary>
        [Description("医生")]
        HKDoctor = 1,

        /// <summary>
        /// 医院
        /// </summary>
        [Description("医院")]
        Hospital = 2,

        [Description("预约挂号医生")]
        AppointmentDoctor=3
    }
}
