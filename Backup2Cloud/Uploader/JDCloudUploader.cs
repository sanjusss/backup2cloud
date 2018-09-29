using Amazon.S3;
using Amazon.S3.Model;
using Backup2Cloud.Conf;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Backup2Cloud.Uploader
{
    /// <summary>
    /// 京东云对象存储上传实现类。
    /// </summary>
    [Name("jdcloud")]
    public class JDCloudUploader : IUploader
    {
        /// <summary>
        /// 对象存储空间的外网访问域名
        /// </summary>
        public string domain;
        /// <summary>
        /// 京东云 Access Key ID
        /// </summary>
        public string accessKeyId;
        /// <summary>
        /// 京东云 Secret Access Key
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
                return "domain：对象存储空间的外网访问域名，在 对象存储-空间管理-空间信息 里查看，例如backup.oss.cn-east-2.jcloudcs.com" +
                    "accessKeyId：Access Key ID；" +
                    "secretAccessKey：Secret Access Key；" +
                    "path：文件在Bucket/桶下的路径前缀，例如\"data/some\"，最终会生成类似\"data/some201809092054.zip\"之类的文件";
            }
        }
        /// <summary>
        /// 获取示例实例。
        /// </summary>
        /// <returns>示例配置实例</returns>
        public object GetExample()
        {
            return new JDCloudUploader()
            {
                domain = "backup.oss.cn-east-2.jcloudcs.com",
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
            string pattern = @"^([^\.]+)\.oss\.([^\.]+)\.jcloudcs\.com$";
            var matches = Regex.Matches(domain.Trim(), pattern);
            if (matches.Count != 1)
            {
                throw new Exception(string.Format("域名{0}不符合京东云对象存储空间的外网访问域名的格式。", domain));
            }

            var groups = matches[0].Groups;
            string bucket = groups[1].Value;
            string systemName = groups[2].Value;
            string serverUrl = "http://s3." + systemName + ".jcloudcs.com";
            AmazonS3Config config = new AmazonS3Config()
            {
                UseHttp = true,
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
                        InputStream = stream,
                        UseChunkEncoding = false
                    };

                    await client.PutObjectAsync(request);
                }
            }
        }
    }
}
