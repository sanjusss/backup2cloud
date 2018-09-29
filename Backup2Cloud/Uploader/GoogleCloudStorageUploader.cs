using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Backup2Cloud.Worker
{
    /// <summary>
    /// 谷歌云存储上传实现类
    /// </summary>
    [ProviderName("google")]
    public class GoogleCloudStorageUploader : IUploader
    {
        /// <summary>
        /// 服务账号密钥文件位置
        /// </summary>
        public string jsonKeyFile;
        /// <summary>
        /// 存储空间名
        /// </summary>
        public string bucket;
        /// <summary>
        /// 文件在Bucket下的路径，作为上传路径前缀。
        /// </summary>
        public string path;
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Tips
        {
            get
            {
                return "jsonKeyFile：服务账号密钥文件位置" +
                    "bucket：存储空间名；" +
                    "path：文件在Bucket/桶下的路径前缀，例如\"data/some\"，最终会生成类似\"data/some201809092054.zip\"之类的文件";
            }
        }

        /// <summary>
        /// 获取示例实例。
        /// </summary>
        /// <returns>示例配置实例</returns>
        public IUploader GetExample()
        {
            return new GoogleCloudStorageUploader()
            {
                jsonKeyFile = "/conf/xxx.json",
                bucket = "backup",
                path = "data/some"
            };
        }
        
        /// <summary>
        /// 上传指定文件。
        /// </summary>
        /// <param name="file">待上传的文件</param>
        /// <param name="suffix">文件在云空间里的后缀</param>
        /// <exception cref="Exception"/>
        public Task Upload(string file, string suffix)
        {
            using (FileStream stream = new FileStream(file, FileMode.Open))
            {
                GoogleCredential credential = GoogleCredential.FromFile(jsonKeyFile);
                var client = StorageClient.Create(credential);
                client.UploadObject(bucket, path + suffix, "application/x-zip-compressed", stream);
            }

            return Task.CompletedTask;
        }
    }
}
