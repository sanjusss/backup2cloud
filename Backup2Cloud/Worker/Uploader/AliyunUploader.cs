using Aliyun.OSS;
using Aliyun.OSS.Common;
using System;
using System.Threading.Tasks;

namespace Backup2Cloud.Worker.Uploader
{
    /// <summary>
    /// 阿里云上传实现类。
    /// </summary>
    [ProviderName("aliyun")]
    public class AliyunUploader : IUploader
    {
        /// <summary>
        /// 访问域名（可以在控制台查看）
        /// </summary>
        public string endpoint;
        /// <summary>
        /// 阿里云AccessKeyId
        /// </summary>
        public string id;
        /// <summary>
        /// 阿里云AccessKeySecret
        /// </summary>
        public string secret;
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
                return "endpoint：地域节点（可以在控制台查看）；" +
                    "id：阿里云AccessKeyId（可以在控制台查看）；" +
                    "secret：阿里云AccessKeySecret（可以在控制台查看）；" +
                    "bucket：存储空间名" +
                    "path：文件在存储空间下的路径前缀，例如\"data/some\"，最终会生成类似\"data/some201809092054.zip\"之类的文件；" +
                    "timeout：上传超时时间，单位毫秒，可以不设置";
            }
        }

        /// <summary>
        /// 获取阿里云示例实例。
        /// </summary>
        /// <returns>阿里云示例配置实例</returns>
        public IUploader GetExample()
        {
            return new AliyunUploader()
            {
                endpoint = "oss-cn-hangzhou.aliyuncs.com",
                id = "阿里云AccessKeyId",
                secret = "阿里云AccessKeySecret",
                bucket = "backup",
                path = "data/some",
                timeout = 200000
            };
        }
        
        /// <summary>
        /// 上传指定文件到阿里云OSS。
        /// </summary>
        /// <param name="file">待上传的文件</param>
        /// <param name="suffix">文件在云空间里的后缀</param>
        /// <exception cref="Exception"/>
        public Task Upload(string file, string suffix)
        {
            OssClient client;
            if (timeout > 0)
            {
                var clientconf = new ClientConfiguration();
                clientconf.ConnectionTimeout = 20000;
                client = new OssClient(endpoint, id, secret, clientconf);
            }
            else
            {
                client = new OssClient(endpoint, id, secret);
            }

            client.PutObject(bucket, path + suffix, file);
            return Task.CompletedTask;
        }
    }
}
