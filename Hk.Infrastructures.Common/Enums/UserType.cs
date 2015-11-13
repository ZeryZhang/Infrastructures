using System.ComponentModel;

namespace Hk.Infrastructures.Common.Enums
{
    /// <summary>
    /// 用户类型的枚举
    /// </summary>
    public enum UserType
    {
        [Description("医生")]
        Doctor = 1,

        [Description("患者")]
        Patient = 2,

        [Description("医生+患者")]
        DoctorAndPatient = 3,
    }

    /// <summary>
    /// 患者用户状态
    /// </summary>
    public enum PatientStatus
    {
        /// <summary>
        /// 注册
        /// </summary>
        IsRegedit=0,
        /// <summary>
        /// 认证
        /// </summary>
        IsCertification=1,
        /// <summary>
        /// 禁用
        /// </summary>
        Disabled = 4
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum SexType
    {

        /// <summary>
        /// 男
        /// </summary>
        Male = 0,
        /// <summary>
        /// 女
        /// </summary>
        Female = 1,
        /// <summary>
        /// 保密
        /// </summary>
        Unknown = 3
    }
    /// <summary>
    /// 医生状态
    /// </summary>
    public enum DoctorStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 审核中
        /// </summary>
        Verifying = 2,
        /// <summary>
        /// 已认证
        /// </summary>
        Approved = 3,
        /// <summary>
        /// 禁用
        /// </summary>
        Disabled = 4,
        /// <summary>
        /// 删除
        /// </summary>
        Deleted = 5,
        /// <summary>
        /// 黑名单
        /// </summary>
        Blacklist = 6,
        /// <summary>
        /// 未审核通过
        /// </summary>
        Rejected = 7,

    }

    /// <summary>
    /// 患者类型
    /// </summary>
    public enum PatientType
    {
        Adult=1,//成人
        Child=2//儿童
    }

}
