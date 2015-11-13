using System.ComponentModel;
namespace Hk.Infrastructures.Common.Enums
{
    /// <summary>
    /// 医生提现账号类型
    /// </summary>
    public enum DoctorPaymentAccountType
    {
        /// <summary>
        /// 所有类型提现账号
        /// </summary>
        [Description("所有提现账号")]
        AllAccount = 0,

        /// <summary>
        /// 传统银行账号
        /// </summary>
        [Description("银行提现账号")]
        BankAccount = 1,

        /// <summary>
        /// 网络支付平台账号
        /// </summary>
        [Description("网络提现账号")]
        NetAccount = 2
    }
}
