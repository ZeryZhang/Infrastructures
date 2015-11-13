using System;
using Hk.Infrastructures.Logging;
using Quartz;

namespace Hk.Infrastructures.Schedulers
{
    public abstract class BaseSchedulerJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                //LoggerClient.WriteLog().Info(-1, this.GetType().FullName + ".Execute", "V1.0", "Start Executing");
                ExecuteCore(context);
                //LoggerClient.WriteLog().Info(-1, this.GetType().FullName + ".Execute", "V1.0", "Execute Finished");
            }
            catch (Exception ex)
            {
                LoggerClient.WriteLog().Fatal(-1, this.GetType().FullName + ".Execute", "V1.0", ex, ex.Message);
            }
        }

        protected abstract void ExecuteCore(IJobExecutionContext context);
    }
}
