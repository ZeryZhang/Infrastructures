using System;
using Quartz;
namespace Hk.Infrastructures.Schedulers
{
    public class SimpleSchedulerJob : BaseSchedulerJob
    {
        protected override void ExecuteCore(IJobExecutionContext context)
        {
            if (context.JobDetail != null && context.JobDetail.JobDataMap != null)
            {
                var action = (Action<object>) context.JobDetail.JobDataMap.Get("ActionName");
                var parameter = context.JobDetail.JobDataMap.Get("Parameter");
                if (action != null)
                {
                    action.Invoke(parameter);
                }
            }
        }
    }
}

