using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Backup2Cloud.Worker.Uploader
{
    /// <summary>
    /// AWS S3上传实现类。
    /// </summary>
    [ProviderName("aws")]
    public class AwsUploader : IUploader
    {
        /// <summary>
        /// 区域的系统名
        /// </summary>
        public string regionSystemName;
        /// <summary>
        /// AWS Access Key ID
        /// </summary>
        public string awsAccessKeyId;
        /// <summary>
        /// AWS Secret Access Key
        /// </summary>
        public string awsSecretAccessKey;
        /// <summary>
        /// AWS S3 Bucket
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
                return "regionSystemName：区域的系统名，例如 us-west-1" +
                    "awsAccessKeyId：AWS Access Key ID；" +
                    "awsSecretAccessKey：AWS Secret Access Key；" +
                    "bucket：存储空间名；" +
                    "path：文件在存储空间下的路径前缀，例如\"data/some\"，最终会生成类似\"data/some201809092054.zip\"之类的文件";
            }
        }
        /// <summary>
        /// 获取示例实例。
        /// </summary>
        /// <returns>示例配置实例</returns>
        public IUploader GetExample()
        {
            return new AwsUploader()
            {
                regionSystemName = "us-west-1",
                awsAccessKeyId = "xxxx",
                awsSecretAccessKey = "yyy",
                bucket = "backup",
                path = "data/file"
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
            var region = RegionEndpoint.GetBySystemName(regionSystemName);
            using (FileStream stream = new FileStream(file, FileMode.Open))
            {
                using (AmazonS3Client client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, region))
                {
                    PutObjectRequest request = new PutObjectRequest()
                    {
                        BucketName = bucket,
                        Key = path + suffix,
                        InputStream = stream
                    };

                    await client.PutObjectAsync(request);
                }
            }
        }
    }
}
