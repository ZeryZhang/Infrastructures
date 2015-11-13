namespace Hk.Infrastructures.Common.Enums
{
    public enum AppointmentRecordStatus
    {
        //状态：状态：1待处理、2预约成功、3预约失败、4待付款、5付款失败、6待就诊、7已取消、8待退款、9退款成功、10退款失败、11爽约、12已就诊、13已过期不爽约、14已停诊
        /// <summary>
        /// 待处理
        /// </summary>
        Waitprocessed = 1,
        /// <summary>
        /// 预约成功
        /// </summary>
        AppointmentSuccess = 2,
        /// <summary>
        /// 预约失败
        /// </summary>
        AppointmentFailure = 3,
        /// <summary>
        /// 待付款
        /// </summary>
        WaitPayment = 4,

        /// <summary>
        /// 付款失败
        /// </summary>
        PaymentFailure = 5,
        /// <summary>
        /// 待就诊
        /// </summary>
        WaitSeeDoctor = 6,
        /// <summary>
        /// 已取消
        /// </summary>
        Cancelled = 7,
        /// <summary>
        /// 待退款
        /// </summary>
        WaitRefund = 8,
        /// <summary>
        /// 退款成功
        /// </summary>
        RefundSuccess = 9,
        /// <summary>
        /// 退款失败
        /// </summary>
        RefundFailure = 10,
        ///// <summary>
        ///// 待评价
        ///// </summary>
        //WaitEvaluation = 11,

        ///// <summary>
        ///// 评价待审
        ///// </summary>
        //EvaluationForAudit = 12,

        ///// <summary>
        ///// 评价成功
        ///// </summary>
        //EvaluationSuccess = 13,
        /// <summary>
        /// 爽约
        /// </summary>
        BreakPromise = 11,
        /// <summary>
        /// 已就诊
        /// </summary>
        AlreadySeeDoctor = 12,
        /// <summary>
        /// 已过期不爽约
        /// </summary>
        ExpiredNotMiss=13,
        /// <summary>
        /// 停诊
        /// </summary>
        StopSchedule=14,

    }
}