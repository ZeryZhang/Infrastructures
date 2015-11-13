using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hk.Infrastructures.Common.Enums
{
    /// <summary>
    /// 收费项目
    /// </summary>
    public enum ChargeModuleType
    {
        /// <summary>
        /// 医生服务
        /// </summary>
        DoctorService = 1,

        /// <summary>
        /// 预约挂号
        /// </summary>
        Appointment = 2,

        /// <summary>
        /// 门诊缴费
        /// </summary>
        OutpatientService = 3,

        /// <summary>
        /// 住院缴费
        /// </summary>
        Hospitalized = 4
    }
}
