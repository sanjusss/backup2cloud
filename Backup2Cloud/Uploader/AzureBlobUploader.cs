using Backup2Cloud.Conf;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Threading.Tasks;

namespace Backup2Cloud.Uploader
{
    /// <summary>
    /// Azure Blob上传实现类。
    /// </summary>
    [Name("azure")]
    public class AzureBlobUploader : IUploader
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string connectionString;
        /// <summary>
        /// 存储容器名
        /// </summary>
        public string container;
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
                return "connectionString：连接字符串；" +
                    "container：存储容器名；" +
                    "path：文件在Bucket下的路径，作为上传路径前缀";
            }
        }
        
        /// <summary>
        /// 获取示例实例。
        /// </summary>
        /// <returns>示例配置实例</returns>
        public object GetExample()
        {
            return new AzureBlobUploader()
            {
                connectionString = "connectionStringxxx",
                container = "backup",
                path = "data/some"
            };
        }

        /// <summary>
        /// 上传指定文件。
        /// </summary>
        /// <param name="file">待上传的文件</param>
        /// <param name="suffix">文件在云空间里的后缀</param>
        /// <exception cref="Exception"/>
        public async Task Upload(string file, string suffix)
        {
            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudBlobClient();
            var rc = client.GetContainerReference(container);
            var blob = rc.GetBlockBlobReference(path + suffix);
            await blob.UploadFromFileAsync(file);
        }
    }
}
