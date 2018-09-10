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
    public class ExamplePrinter
    {
        public static void Print(ExampleOptions options)
        {
            var uploaders = UploaderLoader.Load();
            List<SingleConfiguration> configurations = new List<SingleConfiguration>();
            string[] crontab = { "0,30 * * * * ?" };
            foreach (var i in uploaders)
            {
                SingleConfiguration single = new SingleConfiguration()
                {
                    provider = i.Key,
                    uploader = (Activator.CreateInstance(i.Value) as IUploader).GetExample(),
                    name = "上传到 " + i.Key,
                    path = "/data",
                    crontab = new HashSet<string>(crontab)
                };

                configurations.Add(single);
            }

            string json = JsonConvert.SerializeObject(configurations, Formatting.Indented);
            Log.Info("\n\n示例文件：\n");
            Console.WriteLine(json);
            if (string.IsNullOrEmpty(options.Path) == false)
            {
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
