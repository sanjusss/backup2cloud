using BaiduBce;
using BaiduBce.Auth;
using BaiduBce.Services.Bos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Backup2Cloud.Worker
{
    /// <summary>
    /// 百度云上传实现类。
    /// </summary>
    [ProviderName("baidu")]
    public class BaiduUploader : IUploader
    {
        /// <summary>
        /// 访问域名（可以在控制台查看）
        /// </summary>
        public string endpoint;
        /// <summary>
        /// 百度云AccessKeyId
        /// </summary>
        public string id;
        /// <summary>
        /// 百度云AccessKeySecret
        /// </summary>
        public string key;
        /// <summary>
        /// 存储空间名
        /// </summary>
        public string bucket;
        /// <summary>
        /// 文件在存储空间下的路径，作为上传路径前缀。
        /// </summary>
        public string path;
        /// <summary>
        /// 上传超时时间，单位毫秒。小于等于0时表示不超时。
        /// </summary>
        public int timeout = 0;
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Tips
        {
            get
            {
                return "endpoint：地域节点北京区域：https://bj.bcebos.com ，广州区域：https://gz.bcebos.com， 苏州区域：https://su.bcebos.com （可以在控制台查看）；" +
                    "id：百度云AccessKeyId（可以在控制台查看）；" +
                    "secret：百度云SecretAccessKey（可以在控制台查看）；" +
                    "bucket：存储空间名" +
                    "path：文件在存储空间下的路径前缀，例如\"data/some\"，最终会生成类似\"data/some201809092054.zip\"之类的文件；" +
                    "timeout：上传超时时间，单位毫秒";
            }
        }
        /// <summary>
        /// 获取百度云示例实例。
        /// </summary>
        /// <returns>百度云示例配置实例</returns>
        public IUploader GetExample()
        {
            return new BaiduUploader()
            {
                endpoint = "https://su.bcebos.com",
                id = "百度云AccessKeyId",
                key = "百度云SecretAccessKey",
                bucket = "backup",
                path = "data/some",
                timeout = 200000
            };
        }

        /// <summary>
        /// 上传指定文件到百度云BOS。
        /// </summary>
        /// <param name="file">待上传的文件</param>
        /// <param name="suffix">文件在云空间里的后缀</param>
        /// <exception cref="Exception"/>
        public Task Upload(string file, string suffix)
        {
            BceClientConfiguration config = new BceClientConfiguration();
            config.Credentials = new DefaultBceCredentials(id, key);
            config.Endpoint = endpoint;
            if (timeout > 0)
            {
                config.ReadWriteTimeoutInMillis = timeout;
            }

            BosClient client = new BosClient(config);
            client.PutObject(bucket, path + suffix, new FileInfo(file));
            return Task.CompletedTask;
        }
    }
}
