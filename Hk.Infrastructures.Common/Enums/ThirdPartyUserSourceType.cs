using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hk.Infrastructures.Common.Enums
{
    public enum ThirdPartyUserSourceType
    {
        /// <summary>
        /// 支付宝
        /// </summary>
        Alipay = 1,

        /// <summary>
        /// qq
        /// </summary>
        TencentQQ = 2,

        /// <summary>
        /// 新浪微博
        /// </summary>
        SinaWeibo = 3,

        /// <summary>
        /// 小米黄页
        /// </summary>
        XiaoMiHuangYe = 4,
        /// <summary>
        /// 电话帮
        /// </summary>
        Dianhuabang = 5,
        /// <summary>
        /// 微信
        /// </summary>
        WeiXin = 6,
        /// <summary>
        /// 阿里健康过来的用户
        /// </summary>
        ALiJianKang = 13,
    }
}
