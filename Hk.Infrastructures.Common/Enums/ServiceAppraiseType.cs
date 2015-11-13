using System.ComponentModel;
namespace Hk.Infrastructures.Common.Enums
{
    /// <summary>
    /// 服务评价类型 1 医生咨询服务 2 预约服务
    /// </summary>
    public enum ServiceAppraiseType
    {
        /// <summary>
        /// 医生服务
        /// </summary>
        [Description("医生服务")]
        DoctorConsultantService = 1,

        /// <summary>
        /// 预约服务
        /// </summary>
        [Description("预约服务")]
        AppointmentService = 2
    }
}
