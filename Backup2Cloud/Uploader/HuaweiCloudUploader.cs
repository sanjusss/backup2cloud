using Backup2Cloud.Conf;
using OBS;
using OBS.Model;
using System;
using System.Threading.Tasks;

namespace Backup2Cloud.Uploader
{
    /// <summary>
    /// 华为云上传实现类。
    /// </summary>
    [Name("huawei")]
    public class HuaweiCloudUploader : IUploader
    {
        /// <summary>
        /// 访问域名（可以在控制台查看）
        /// </summary>
        public string endpoint;
        /// <summary>
        /// 华为云 Access Key Id
        /// </summary>
        public string accessKeyId;
        /// <summary>
        /// 华为云 Secret Access Key
        /// </summary>
        public string secretAccessKey;
        /// <summary>
        /// 桶名称
        /// </summary>
        public string bucket;
        /// <summary>
        /// 文件在存储空间下的路径，作为上传路径前缀。
        /// </summary>
        public string path;
        /// <summary>
        /// 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
        /// </summary>
        public int? deleteAfterDays;
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Tips
        {
            get
            {
                return "endpoint：地域节点（可以在控制台查看）；" +
                    "accessKeyId：华为云 Access Key Id（可以在控制台查看）；" +
                    "secretAccessKey：华为云 Secret Access Key（可以在控制台查看）；" +
                    "bucket：桶名称" +
                    "path：文件在存储空间下的路径前缀，例如\"data/some\"，最终会生成类似\"data/some201809092054.zip\"之类的文件；" +
                    "deleteAfterDays：多少天后自动删除，可以不设置";
            }
        }

        /// <summary>
        /// 获取华为云示例实例。
        /// </summary>
        /// <returns>华为云示例配置实例</returns>
        public object GetExample()
        {
            return new HuaweiCloudUploader()
            {
                endpoint = "obs.cn-north-1.myhwclouds.com",
                accessKeyId = "xxxx",
                secretAccessKey = "yyyy",
                bucket = "backup",
                path = "data/some",
                deleteAfterDays = 20
            };
        }

        /// <summary>
        /// 上传指定文件到华为云BOS。
        /// </summary>
        /// <param name="file">待上传的文件</param>
        /// <param name="suffix">文件在云空间里的后缀</param>
        /// <exception cref="Exception"/>
        public Task Upload(string file, string suffix)
        {
            var client = new ObsClient(accessKeyId, secretAccessKey, endpoint);
            PutObjectRequest request = new PutObjectRequest()
            {
                BucketName = bucket,
                ObjectKey = path + suffix,
                FilePath = file,
                Expires = deleteAfterDays
            };
            client.PutObject(request);
            return Task.CompletedTask;
        }
    }
}
