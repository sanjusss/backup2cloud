using System;
using System.Collections.Generic;
using System.Text;

namespace Backup2Cloud.Conf
{
    /// <summary>
    /// FTP基本配置。
    /// </summary>
    public abstract class BaseFtpConf
    {
        /// <summary>
        /// 服务器主机名。
        /// 默认为本机。
        /// </summary>
        public string host { get; set; } = "localhost";

        /// <summary>
        /// 服务器的FTP端口。
        /// 默认为21。
        /// </summary>
        public int port { get; set; } = 21;

        /// <summary>
        /// 用户名。
        /// 默认为anonymous。
        /// </summary>
        public string user { get; set; } = "anonymous";

        /// <summary>
        /// 密码。
        /// </summary>
        public string password { get; set; } = string.Empty;

        /// <summary>
        /// 是否使用匿名登陆。
        /// 默认为false。
        /// </summary>
        public bool anonymous { get; set; } = false;

        /// <summary>
        /// 在ftp上的路径。
        /// </summary>
        public string path { get; set; } = "/";
    }
}
