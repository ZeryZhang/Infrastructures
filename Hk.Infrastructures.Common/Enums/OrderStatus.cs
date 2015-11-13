namespace Hk.Infrastructures.Common.Enums
{
    /// <summary>
    /// 订单状态
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// 已创建订单
        /// </summary>
        Created = 0,

        /// <summary>
        /// 未支付，已关闭
        /// </summary>
        Close = 1,

        /// <summary>
        /// 已支付
        /// </summary>
        Paid = 2,

        /// <summary>
        /// 退款中
        /// </summary>
        Refunding = 3,

        /// <summary>
        /// 已退款
        /// </summary>
        Returned = 4,

        /// <summary>
        /// 已完成
        /// </summary>
        Complete = 5
    }
}
