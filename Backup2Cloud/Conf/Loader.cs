using Backup2Cloud.Args;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
        public static void Load(Options options)
        {
            if (File.Exists(options.Conf) == false)
            {
                Console.WriteLine(string.Format("没有找到配置文件\"{0}\"！", options.Conf));
                Environment.Exit(1);
                return;
            }
        }
    }
}
