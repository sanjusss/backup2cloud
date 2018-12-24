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
    public class FtpUploader : BaseFtpConf, IUploader
    {
        public string Tips
        {
            get
            {
                return string.Empty;
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
