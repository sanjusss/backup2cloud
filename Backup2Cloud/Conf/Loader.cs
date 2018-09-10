using Backup2Cloud.Args;
using Backup2Cloud.Logging;
using Backup2Cloud.Worker;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Backup2Cloud.Conf
{
    /// <summary>
    /// 配置加载器
    /// </summary>
    public static class Loader
    {
        /// <summary>
        /// 载入设置
        /// </summary>
        /// <param name="options">相关设置</param>
        public static void Load(NormalOptions options)
        {
            if (File.Exists(options.Conf) == false)
            {
                Log.Warn(string.Format("没有找到配置文件\"{0}\"！", options.Conf));
                Environment.Exit(1);
                return;
            }

            string content;
            using (StreamReader reader = new StreamReader(options.Conf, Encoding.UTF8))
            {
                content = reader.ReadToEnd();
            }

            Log.Info("成功获取配置文件，正在加载……");
            List<SingleConfiguration> configurations = JsonConvert.DeserializeObject<List<SingleConfiguration>>(content, new ConfigurationConverter());
            Log.Info(string.Format("成功加载配置文件，共有{0}条配置。", configurations.Count));
            if (configurations.Count == 0)
            {
                Environment.Exit(0);
            }

            WorkScheduler scheduler = new WorkScheduler();
            foreach (var i in configurations)
            {
                scheduler.StartWork(i);
                Log.Info(string.Format("成功启动任务\"{0}\"。", i.name));
            }

            Log.Info("成功启动所有备份任务。");
#if !DEBUG
            Thread.Sleep(Timeout.Infinite);//加载完成后主线程休眠。
#endif
        }
    }
}
