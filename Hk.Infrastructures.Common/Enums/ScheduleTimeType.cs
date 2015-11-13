using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hk.Infrastructures.Common.Enums
{
    /// <summary>
    /// 排班时段类型
    /// </summary>
    public enum ScheduleTimeType
    {
        None=0,
        Forenoon=1,
        Afternoon=2,
        Night=3,
        DayTime=4,
        AllDay=5
    }
}
