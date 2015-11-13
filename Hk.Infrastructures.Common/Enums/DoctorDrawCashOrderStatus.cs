namespace Hk.Infrastructures.Common.Enums
{
    /// <summary>
    /// 医生提现订单状态
    /// </summary>
    public enum DoctorDrawCashOrderStatus
    {
        /// <summary>
        /// 订单创建
        /// </summary>
        Created = 0,

        /// <summary>
        /// 已经处理（客服已经通过银行打款，未确认是否到账）
        /// </summary>
        Handled = 1,

        /// <summary>
        /// 已完成
        /// </summary>
        Completed = 2,

        /// <summary>
        /// 已经取消
        /// </summary>
        Canceled = 3
    }
}
