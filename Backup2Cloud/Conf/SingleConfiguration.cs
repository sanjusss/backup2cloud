using Backup2Cloud.Worker;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
        /// 服务商
        /// </summary>
        public string provider;

        /// <summary>
        /// 需要备份的文件夹或文件路径
        /// </summary>
        public string path;

        /// <summary>
        /// 备份开始时间
        /// </summary>
        public HashSet<string> crontab;

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
                    "provider：上传服务提供商；" +
                    "path：需要备份的文件夹或文件在本地的路径；" +
                    "crontab：启动备份的时间集合，可以参考http://cron.qqe2.com/，使用时需要注意时区；" +
                    "uploader：上传设置";
            }
        }
    }
}
