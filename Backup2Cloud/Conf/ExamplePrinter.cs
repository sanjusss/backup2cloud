using Backup2Cloud.Args;
using Backup2Cloud.Logging;
using Backup2Cloud.Worker;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Backup2Cloud.Conf
{
    /// <summary>
    /// 示例打印类。
    /// </summary>
    public class ExamplePrinter
    {
        /// <summary>
        /// 根据命令行参数打印示例。
        /// </summary>
        /// <param name="options">命令行参数</param>
        public static void Print(ExampleOptions options)
        {
            var uploaders = UploaderLoader.Load();
            List<SingleConfiguration> configurations = new List<SingleConfiguration>();
            string[] crontab = { "0,30 * * * * ?" };
            foreach (var i in uploaders)
            {
                SingleConfiguration single = new SingleConfiguration()
                {
                    uploader = (Activator.CreateInstance(i.Value) as IUploader).GetExample(),
                    name = "上传到 " + i.Key,
                    path = "/data",
                    crontab = new HashSet<string>(crontab),
                    command = "echo",
                    commandArgs = "Hello World!"
                };

                configurations.Add(single);
            }

            string json = JsonConvert.SerializeObject(configurations, Formatting.Indented);
            Log.Info("\n\n示例文件：\n");
            Console.WriteLine(json);
            if (string.IsNullOrEmpty(options.Path) == false)
            {
                //保存到文件。
                using (StreamWriter stream = new StreamWriter(options.Path, false, Encoding.UTF8))
                {
                    stream.Write(json);
                }

                Log.Info(string.Format("示例文件已经保存到\"{0}\"。", options.Path));
            }

#if !DEBUG
            Environment.Exit(0);
#endif
        }
    }
}
