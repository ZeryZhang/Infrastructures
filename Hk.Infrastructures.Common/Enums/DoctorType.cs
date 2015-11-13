using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hk.Infrastructures.Common.Enums
{
    public enum DoctorType
    {
        /// <summary>
        /// 华康医生
        /// </summary>
        [Description("华康医生")]
        HKDoctor = 1,
        
        /// <summary>
        /// 预约挂号医生
        /// </summary>
        [Description("预约挂号医生")]
        AppointmentDoctor = 2
    }
}
