using Backup2Cloud.Conf;
using FluentFTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Backup2Cloud.Uploader
{
    /// <summary>
    /// FTP上传实现类。
    /// </summary>
    [Name("ftp")]
    public class FtpUploader : BaseFtpConf, IUploader
    {
        public string Tips
        {
            get
            {
                return "host:服务器主机名；" +
                    "port：服务器的FTP端口；" +
                    "user：用户名；" +
                    "password：密码；" +
                    "anonymous：是否使用匿名登陆；" +
                    "path：在ftp上的路径前缀。";
            }
        }

        public object GetExample()
        {
            return new FtpUploader();
        }

        public Task Upload(string file, string suffix)
        {
            using (FtpClient client = new FtpClient(host))
            {
                client.Port = port;
                client.Encoding = Encoding.UTF8;
                if (anonymous == false)
                {
                    client.Credentials = new NetworkCredential(user, password);
                }

                client.Connect();
                if (client.UploadFile(file, path + suffix) == false)
                {
                    throw new IOException($"上传 ftp://{ host }:{ port }{ path }{ suffix } 失败。");
                }

                client.Disconnect();
            }

            return Task.CompletedTask;
        }
    }
}
