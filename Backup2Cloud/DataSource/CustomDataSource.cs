using Backup2Cloud.Conf;
using Backup2Cloud.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Backup2Cloud.DataSource
{
    /// <summary>
    /// 自定义数据源。
    /// </summary>
    [Name("custom")]
    public class CustomDataSource : IDataSource
    {
        /// <summary>
        /// 自定义命令。
        /// </summary>
        [JsonProperty("command")]
        public virtual string Command { get; set; }
        /// <summary>
        /// 自定义命令参数。
        /// 可以为空。
        /// </summary>
        [JsonProperty("params")]
        public virtual string Params { get; set; }

        /// <summary>
        /// 数据源提示信息。
        /// </summary>
        public virtual string Tips
        {
            get
            {
                return "command：自定义命令；" +
                    "params：自定义命令参数，可以为空。";
            }
        }

        /// <summary>
        /// 获取示例。
        /// </summary>
        /// <returns>示例</returns>
        public virtual object GetExample()
        {
            return new CustomDataSource()
            {
                Command = "echo",
                Params = "Hello"
            };
        }

        /// <summary>
        /// 运行自定义命令。
        /// </summary>
        /// <exception cref="OperationCanceledException"/>
        public void SaveData()
        {
            Process process = string.IsNullOrWhiteSpace(Params) ?
                                    Process.Start(Command) :
                                    Process.Start(Command, Params);
#if DEBUG
            process.OutputDataReceived += (sender, e) => Log.Info(e.Data);
            process.BeginOutputReadLine();
#endif
            process.WaitForExit();
            if (process.ExitCode == 0)
            {
                throw new OperationCanceledException(string.Format("自定义命令返回了错误码 {0}。", process.ExitCode));
            }
        }
    }
}
