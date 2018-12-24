using Backup2Cloud.Conf;
using Backup2Cloud.Logging;
using FluentFTP;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Backup2Cloud.DataSource
{
    /// <summary>
    /// FTP数据源。从远端FTP获取数据。
    /// </summary>
    [Name("ftp")]
    public class FtpDataSource : BaseFtpConf, IDataSource
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
            return new FtpDataSource();
        }

        public void SaveData(string des)
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
                if (client.DirectoryExists(path))
                {
                    int result = DownloadDirectory(path, "/", des, client);
                    Log.Print($"从 ftp://{ host }:{ port }{ path } 成功下载 { result } 个文件到 { des } 。");
                }
                else
                {
                    if (client.DownloadFile(des, path) == false)
                    {
                        throw new IOException($"下载 ftp://{ host }:{ port }{ path } 失败。");
                    }

                    Log.Print($"成功下载 ftp://{ host }:{ port }{ path } 到 { des } 。");
                }

                client.Disconnect();
            }
        }

        private int DownloadDirectory(string baseSrc, string dir, string des, FtpClient client)
        {
            string target = FormatDirectoryName(des + dir);
            if (Directory.Exists(target))
            {
                Directory.Delete(target, true);
            }

            Directory.CreateDirectory(target);
            var list = client.GetListing(FormatDirectoryName(baseSrc + dir));
            List<string> dirs = new List<string>();
            int result = 0;
            foreach (var i in list)
            {
                switch (i.Type)
                {
                    case FtpFileSystemObjectType.File:
                        if (client.DownloadFile(target + i.Name, i.FullName))
                        {
                            ++result;
                        }

                        break;

                    case FtpFileSystemObjectType.Directory:
                        dirs.Add(i.Name);
                        break;

                    case FtpFileSystemObjectType.Link:
                        break;

                    default:
                        break;
                }
            }

            foreach (var i in dirs)
            {
                result += DownloadDirectory(baseSrc, dir + i + "/", des + i + "/", client);
            }

            return result;
        }

        private string FormatDirectoryName(string name)
        {
            name = name.Replace("\\", "/");
            name = name.Replace("//", "/");
            if (name.EndsWith("/") == false)
            {
                name += "/";
            }

            return name;
        }
    }
}
