using System.ComponentModel;

namespace Hk.Infrastructures.Common.Enums
{
    /// <summary>
    /// APP产品类型
    /// </summary>
    public enum AppServerType
    {
        /// <summary>
        /// 期刊杂志
        /// </summary>
        [Description("期刊杂志")]
        Magazine = 3,
        /// <summary>
        /// 医客
        /// </summary>
        [Description("医客")]
        DoctorClient = 2,
        /// <summary>
        /// 就医宝
        /// </summary>
        [Description("就医宝")]
        DoctorReservation = 4,
        /// <summary>
        /// web站点[官网]
        /// </summary>
        [Description("web站点")]
        WebSite = 1,

        /// <summary>
        /// 微站
        /// </summary>
        [Description("微站")]
        H5 = 5
    }
}
