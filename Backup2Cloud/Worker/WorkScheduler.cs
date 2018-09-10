using Backup2Cloud.Conf;
using Backup2Cloud.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Backup2Cloud.Worker
{
    /// <summary>
    /// 工作调度类。
    /// </summary>
    public class WorkScheduler
    {
        /// <summary>
        /// 配置在JobDataMap中的key名称。
        /// </summary>
        public static string ConfKey => "conf";
        /// <summary>
        /// 工作调度实例。
        /// </summary>
        private readonly IScheduler _scheduler = null;
        /// <summary>
        /// 组名。
        /// </summary>
        private readonly string _groupName = "group_" + Guid.NewGuid().ToString("N");
        /// <summary>
        /// 已经创建的工作名的集合。
        /// </summary>
        private HashSet<string> _jobNames = new HashSet<string>();

        /// <summary>
        /// 构造函数。
        /// </summary>
        public WorkScheduler()
        {
            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            _scheduler = factory.GetScheduler().Result;
            _scheduler.Start().Wait();
        }

        /// <summary>
        /// 开始工作。
        /// </summary>
        /// <param name="configuration">工作相关配置</param>
        public void StartWork(SingleConfiguration configuration)
        {
            if (_jobNames.Contains(configuration.name))
            {
                string newName = configuration.name + Guid.NewGuid().ToString("N");
                Log.Warn(string.Format("已存在名为\"{0}\"的任务，当前任务将改名为\"{1}\"。", configuration.name, newName));
                configuration.name = newName;
            }

            _jobNames.Add(configuration.name);
            if (configuration.crontab == null || configuration.crontab.Count == 0)
            {
                throw new ArgumentException(string.Format("任务\"{0}\"的crontab字段为空。", configuration.name));
            }

            JobDataMap map = new JobDataMap
            {
                [ConfKey] = configuration
            };
            IJobDetail job = JobBuilder.Create<BackupWorker>()
                .WithIdentity("job_" + configuration.name, _groupName)
                .UsingJobData(map)
                .Build();

            TriggerBuilder triggerBuilder = TriggerBuilder.Create()
                .WithIdentity("trigger_" + configuration.name, _groupName)
                .StartNow();
            foreach (var i in configuration.crontab)
            {
                triggerBuilder.WithCronSchedule(i);
            }

            ITrigger trigger = triggerBuilder.Build();
            _scheduler.ScheduleJob(job, trigger);
        }
    }
}
