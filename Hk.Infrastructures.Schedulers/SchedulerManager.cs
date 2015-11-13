using System;
using System.Collections.Generic;
using System.IO;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using System.Collections.Specialized;
using Hk.Infrastructures.Logging;

namespace Hk.Infrastructures.Schedulers
{
     public static class SchedulerManager
    {
        private static IScheduler _scheduler;

        public static IScheduler Scheduler
        {
            get
            {
                return _scheduler;
            }
        }

        public static void Start()
        {
            try
            {
                var config = Configs.Config.GetConfig();
                if (config != null && config.Properties != null && config.Properties.Count>0)
                {
                    var properties = new NameValueCollection();
                    foreach (var item in config.Properties)
                    {
                        if (String.CompareOrdinal(item.Name, "quartz.plugin.xml.fileNames") == 0)
                        {
                            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, item.Value);
                            if (File.Exists(filePath))
                            {
                                properties[item.Name] = filePath;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            properties.Add(item.Name, item.Value);
                        }
                    }

                    properties["quartz.scheduler.instanceId"] = Guid.NewGuid().ToString(); // requires uniqueness
                    ISchedulerFactory schedulerFactory = new StdSchedulerFactory(properties);
                    _scheduler = schedulerFactory.GetScheduler();

                    _scheduler.Start();
                }
            }
            catch (Exception ex)
            {
                LoggerClient.WriteLog().Fatal(-1, "Hk.Infrastructures.Schedulers.SchedulerManager.Start", "V1.0", ex, ex.Message);
            }
        }

        public static void Stop()
        {
            try
            {
                if (_scheduler != null && _scheduler.IsStarted)
                {
                    _scheduler.Shutdown();
                }
            }
            catch (Exception ex)
            {
                LoggerClient.WriteLog().Fatal(-1, "Hk.Infrastructures.Schedulers.SchedulerManager.Stop", "V1.0", ex, ex.Message);
            }
        }

        public static void CreateJob(Action<object> action, DateTime startDateTime, object parameter)
        {
            if (action != null && Scheduler != null)
            {
                IDictionary<string, object> data = new Dictionary<string, object>();
                data.Add("ActionName", action);
                data.Add("Parameter", parameter);

                var job = JobBuilder.Create(typeof(SimpleSchedulerJob)).UsingJobData(new JobDataMap(data)).Build();
                var trigger = TriggerBuilder.Create().StartAt(new DateTimeOffset(startDateTime)).ForJob(job).Build();

                Scheduler.ScheduleJob(job, trigger);
            }
        }

        public static void CreateJob(Action<object> action, DateTime startDateTime, IntervalUnit intervalUnit, object parameter)
        {
            if (action != null && Scheduler != null)
            {
                IDictionary<string, object> data = new Dictionary<string, object>();
                data.Add("ActionName", action);
                data.Add("Parameter", parameter);

                var job = JobBuilder.Create(typeof(SimpleSchedulerJob)).UsingJobData(new JobDataMap(data)).Build();
                var trigger = new CalendarIntervalTriggerImpl(Guid.NewGuid().ToString(), new DateTimeOffset(startDateTime),
                    new DateTimeOffset(DateTime.MaxValue), intervalUnit, 1);
                Scheduler.ScheduleJob(job, trigger);
            }
        }
    }
}
