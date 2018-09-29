using Backup2Cloud.Worker;
using System.Collections.Generic;

namespace Backup2Cloud.Conf
{
    /// <summary>
    /// 单条备份配置
    /// </summary>
    public struct SingleConfiguration : IConfigurable
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string name;

        /// <summary>
        /// 需要备份的文件夹或文件路径
        /// </summary>
        public string path;

        /// <summary>
        /// 备份开始时间
        /// </summary>
        public HashSet<string> crontab;

        /// <summary>
        /// 打包备份文件夹或文件之前额外执行的命令。
        /// 可以为空。
        /// </summary>
        public string command;

        /// <summary>
        /// 打包备份文件夹或文件之前额外执行的命令的参数。
        /// 可以为空。
        /// </summary>
        public string commandArgs;

        /// <summary>
        /// 上传设置
        /// </summary>
        public IUploader uploader;

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Tips
        {
            get
            {
                return "name：任务名称；" +
                    "path：需要备份的文件夹或文件在本地的路径；" +
                    "crontab：启动备份的时间集合，可以参考http://cron.qqe2.com/，使用时需要注意时区；" +
                    "command：打包备份文件夹或文件之前额外执行的命令，例如备份数据库等，使用docker容器时须注意执行环境，可以为空；" +
                    "commandArgs：打包备份文件夹或文件之前额外执行的命令的参数，，可以为空；" +
                    "uploader：上传设置";
            }
        }
    }
}
