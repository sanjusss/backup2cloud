using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Backup2Cloud.Worker.Uploader
{
    /// <summary>
    /// 青云对象存储上传实现类。
    /// </summary>
    [ProviderName("qinstor")]
    public class QingStorUploader : IUploader
    {
        /// <summary>
        /// 对象存储空间的外网访问域名
        /// </summary>
        public string url;
        /// <summary>
        /// 青云 Access Key ID
        /// </summary>
        public string accessKeyId;
        /// <summary>
        /// 青云 Secret Access Key
        /// </summary>
        public string secretAccessKey;
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
                return "url：Url (API访问用)，在 对象存储 里查看，例如 http://backup.pek3b.qingstor.com" +
                    "accessKeyId：Access Key ID；" +
                    "secretAccessKey：Secret Access Key；" +
                    "path：文件在Bucket/桶下的路径前缀，例如\"data/some\"，最终会生成类似\"data/some201809092054.zip\"之类的文件";
            }
        }
        /// <summary>
        /// 获取示例实例。
        /// </summary>
        /// <returns>示例配置实例</returns>
        public IUploader GetExample()
        {
            return new QingStorUploader()
            {
                url = "http://backup.pek3b.qingstor.com",
                accessKeyId = "xxxx",
                secretAccessKey = "yyy",
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
            string pattern = @"^http://([^\.]+)\.([^\.]+)\.qingstor\.com$";
            var matches = Regex.Matches(url.Trim(), pattern);
            if (matches.Count != 1)
            {
                throw new Exception(string.Format("域名{0}不符合青云对象存储空间的api url的格式。", url));
            }

            var groups = matches[0].Groups;
            string bucket = groups[1].Value;
            string systemName = groups[2].Value;
            string serverUrl = string.Format("https://s3.{0}.qingstor.com", systemName, bucket);
            AmazonS3Config config = new AmazonS3Config()
            {
                ServiceURL = serverUrl,
                SignatureVersion = "v4"
            };

            using (FileStream stream = new FileStream(file, FileMode.Open))
            {
                using (AmazonS3Client client = new AmazonS3Client(accessKeyId, secretAccessKey, config))
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
