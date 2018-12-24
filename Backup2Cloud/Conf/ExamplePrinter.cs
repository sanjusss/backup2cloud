using Backup2Cloud.Args;
using Backup2Cloud.DataSource;
using Backup2Cloud.Logging;
using Backup2Cloud.Uploader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

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
            var uploaders = NamedInterfaceLoader.Load(typeof(IUploader));
            var dataSources = NamedInterfaceLoader.Load(typeof(IDataSource));
            string content = string.Empty;
            if (string.IsNullOrEmpty(options.List) == false)
            {
                if (options.List == "datasource")
                {
                    content = string.Join('\n', dataSources.Keys);
                    Console.WriteLine("已支持以下数据源：");
                }
                else if (options.List == "uploader")
                {
                    content = string.Join('\n', uploaders.Keys);
                    Console.WriteLine("已支持以下上传类：");
                }
                else
                {
                    Console.WriteLine("只能列出 datasource 或 uploader");
                }
            }
            else if (string.IsNullOrEmpty(options.Uploader) == false)
            {
                if (uploaders.ContainsKey(options.Uploader))
                {
                    content = JsonConvert.SerializeObject((Activator.CreateInstance(uploaders[options.Uploader]) as IExampled).GetExample(),
                        Formatting.Indented,
                        new NameConverter());
                }
                else
                {
                    Console.WriteLine($"不存在uploader类： { options.Uploader }");
                }
            }
            else if (string.IsNullOrEmpty(options.DataSource) == false)
            {
                if (dataSources.ContainsKey(options.DataSource))
                {
                    content = JsonConvert.SerializeObject((Activator.CreateInstance(dataSources[options.DataSource]) as IExampled).GetExample(),
                        Formatting.Indented,
                        new NameConverter());
                }
                else
                {
                    Console.WriteLine($"不存在datasource类： { options.DataSource }");
                }
            }
            else
            {
                content = GetExampleConfig(uploaders, dataSources);
            }

            if (string.IsNullOrEmpty(content) == false)
            {
                Console.WriteLine(content);
                if (string.IsNullOrEmpty(options.Path) == false)
                {
                    //保存到文件。
                    using (StreamWriter stream = new StreamWriter(options.Path, false, Encoding.UTF8))
                    {
                        stream.Write(content);
                    }

                    Console.WriteLine("\n\n");
                    Log.Info(string.Format("文件已经保存到\"{0}\"。", options.Path));
                }
            }

#if !DEBUG
            Environment.Exit(0);
#endif
        }

        private static string GetExampleConfig(Dictionary<string, Type> uploaders, Dictionary<string, Type> dataSources)
        {
            string[] crontab = { "0,30 * * * * ?" };
            List<SingleConfiguration> configurations = new List<SingleConfiguration>();
            SingleConfiguration single = new SingleConfiguration()
            {
                name = "上传到 ",
                path = "/data",
                crontab = new HashSet<string>(crontab)
            };

            if (uploaders.Count > 0)
            {
                var i = uploaders.First();
                single.uploader = (Activator.CreateInstance(i.Value) as IUploader).GetExample() as IUploader;
                single.name += i.Key;
            }

            if (dataSources.Count > 0)
            {
                var i = dataSources.First();
                single.dataSource = (Activator.CreateInstance(i.Value) as IDataSource).GetExample() as IDataSource;
            }

            configurations.Add(single);
            return JsonConvert.SerializeObject(configurations, Formatting.Indented, new NameConverter());
        }
    }
}
