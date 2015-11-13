using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hk.Infrastructures.Common.Enums
{
    public enum PaymentChannel
    {
        None=0,//无
        Alipay=1,//支付宝支付
        WeChat=2,//微信支付
        Unionpay=3,//银联支付
        OfflinePay=4 //线下支付
    }
}
