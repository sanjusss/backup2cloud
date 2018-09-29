using Backup2Cloud.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UCloudSDK;
using UCloudSDK.Models;

namespace Backup2Cloud.Worker
{
    /// <summary>
    /// UCloud对象存储上传实现类。
    /// </summary>
    [ProviderName("ucloud")]
    public class UCloudUploader : IUploader
    {
        /// <summary>
        /// 服务商名称
        /// </summary>
        public string Name => "ucloud";
        /// <summary>
        /// UCloud 公钥
        /// </summary>
        public string publicKey;
        /// <summary>
        /// UCloud 私钥
        /// </summary>
        public string privateKey;
        /// <summary>
        /// UCloud 存储空间
        /// </summary>
        public string bucket;
        /// <summary>
        /// UCloud 存储空间域名
        /// </summary>
        public string bucketDomain;
        /// <summary>
        /// 文件在存储空间下的路径，作为上传路径前缀。
        /// </summary>
        public string path;

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Tips
        {
            get
            {
                return "accessKey：UCloud 公钥（可以在控制台-产品服务-API 产品-API密钥 查看）；" +
                    "secretKey：UCloud 私钥（可以在控制台-产品服务-API 产品-API密钥 查看）；" +
                    "bucket：存储空间名；" +
                    "bucketDomain：存储空间域名 类似<bucketname>.cn-sh2.ufileos.com；" +
                    "path：文件在存储空间下的路径前缀，例如\"data/some\"，最终会生成类似\"data/some201809092054.zip\"之类的文件";
            }
        }

        /// <summary>
        /// 获取UCloud示例实例。
        /// </summary>
        /// <returns>UCloud示例配置实例</returns>
        public IUploader GetExample()
        {
            return new UCloudUploader()
            {
                publicKey = "publicKey",
                privateKey = "privateKey",
                bucket = "backup",
                bucketDomain = "backup.cn-sh2.ufileos.com",
                path = "data/some"
            };
        }

        /// <summary>
        /// 上传指定文件到UCloud对象存储。
        /// </summary>
        /// <param name="file">待上传的文件</param>
        /// <param name="suffix">文件在云空间里的后缀</param>
        /// <exception cref="Exception"/>
        public Task Upload(string file, string suffix)
        {
            if (bucketDomain.StartsWith(bucket) == false)
            {
                throw new Exception(string.Format("Bucket\"{0}\"的BucketDomain不是\"{1}\"！", bucket, bucketDomain));
            }

            UFile u = new UFile(publicKey, privateKey);
            u.FileUrl = "http://{0}" + bucketDomain.Substring(bucket.Length);
            var res = u.PutFile(file, path + suffix, bucket);
            if (res.RetCode != 0)
            {
                throw new Exception(string.Format("上传失败。\n\tXSessionId：{0}\n\t错误信息：{1}", res.XSessionId, res.ErrMsg));
            }

            return Task.CompletedTask;
        }
    }
}
