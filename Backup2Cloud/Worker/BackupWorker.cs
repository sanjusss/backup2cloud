﻿using Backup2Cloud.Conf;
using Backup2Cloud.Logging;
using Quartz;
using SharpCompress.Archives;
using SharpCompress.Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Backup2Cloud.Worker
{
    /// <summary>
    /// 执行备份的工作类。
    /// </summary>
    public class BackupWorker : IJob
    {
        /// <summary>
        /// 执行备份。
        /// </summary>
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var conf = (SingleConfiguration)(context.MergedJobDataMap[WorkScheduler.ConfKey]);
                string file = null;
                try
                {
                    DateTime start = DateTime.Now;
                    Log.Info(string.Format("开始执行任务\"{0}\"。", conf.name), conf.name);

                    if (conf.dataSource != null)
                    {
                        conf.dataSource.SaveData(conf.path);
                        Log.Info("成功为数据源执行保存命令。", conf.name);
                    }

                    string suffix = start.ToString("yyyyMMddHHmmss") + ".zip";
                    file = Compress(conf.path);
                    Log.Info("成功打包备份文件。", conf.name);
                    await conf.uploader.Upload(file, suffix);
                    Log.Info("成功上传文件。", conf.name);

                    DateTime end = DateTime.Now;
                    Log.Info($"任务\"{ conf.name }\"执行成功，用时{ (end - start).TotalSeconds }秒。",  conf.name);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message, conf.name);//此处只显示基本异常信息。
                }
                finally
                {
                    if (string.IsNullOrEmpty(file) == false &&
                        File.Exists(file))
                    {
                        File.Delete(file);//删除压缩包
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }

        /// <summary>
        /// 压缩文件或文件夹为zip文件。
        /// </summary>
        /// <param name="path">待压缩的路径</param>
        /// <returns>zip路径</returns>
        private string Compress(string path)
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory + "/tmp";
            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
            }

            string zip = dir + "/" + Guid.NewGuid().ToString("N") + ".zip";
            if (File.Exists(zip))
            {
                File.Delete(zip);
            }

            if (File.Exists(path))
            {
                // 是文件
                FileInfo fi = new FileInfo(path);
                if (fi.FullName.EndsWith(".zip"))//如果是zip文件，就直接复制，而不是压缩。
                {
                    fi.CopyTo(zip, true);
                }
                else
                {
                    using (var archive = SharpCompress.Archives.Zip.ZipArchive.Create())
                    {
                        archive.AddEntry(fi.Name, fi);
                        archive.SaveTo(zip, CompressionType.Deflate);
                    }
                }
            }
            else if (Directory.Exists(path))
            {
                // 是文件夹
                using (var archive = SharpCompress.Archives.Zip.ZipArchive.Create())
                {
                    archive.AddAllFromDirectory(path);
                    archive.SaveTo(zip, CompressionType.Deflate);
                }
            }
            else
            {
                // 都不是
                throw new FileNotFoundException($"没有找到文件或目录\"{ path }\"。");
            }

            return zip;
        }
    }
}
