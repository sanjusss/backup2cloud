using Backup2Cloud.DataSource;
using Backup2Cloud.Uploader;
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
        /// 数据源。
        /// 用来将数据保存到path。
        /// </summary>
        public IDataSource dataSource;

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
                    "dataSource：数据源，可以为空；" +
                    "uploader：上传设置";
            }
        }
    }
}
