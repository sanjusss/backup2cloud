using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.Util;
using System;
using System.Threading.Tasks;

namespace Backup2Cloud.Worker
{
    /// <summary>
    /// 七牛云对象存储上传实现类。
    /// </summary>
    [ProviderName("qiniu")]
    public class QiniuUploader : IUploader
    {
        /// <summary>
        /// 服务商名称
        /// </summary>
        public string Name => "qiniu";
        /// <summary>
        /// 七牛云AccessKey
        /// </summary>
        public string accessKey;
        /// <summary>
        /// 七牛云SecretKey
        /// </summary>
        public string secretKey;
        /// <summary>
        /// 七牛云存储空间
        /// </summary>
        public string bucket;
        /// <summary>
        /// 文件在存储空间下的路径，作为上传路径前缀。
        /// </summary>
        public string path;
        /// <summary>
        /// 上传超时时间，单位毫秒    
        /// </summary>
        public int? timeout;
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
                return "accessKey：七牛云AccessKey（可以在控制台查看）；" +
                    "secretKey：七牛云SecretKey（可以在控制台查看）；" +
                    "bucket：存储空间名" +
                    "path：文件在存储空间下的路径前缀，例如\"data/some\"，最终会生成类似\"data/some201809092054.zip\"之类的文件；" +
                    "timeout：上传超时时间，单位毫秒，可以不设置；" +
                    "deleteAfterDays：多少天后自动删除，可以不设置";
            }
        }

        /// <summary>
        /// 获取七牛云示例实例。
        /// </summary>
        /// <returns>七牛云示例配置实例</returns>
        public IUploader GetExample()
        {
            return new QiniuUploader()
            {
                accessKey = "accessKey",
                secretKey = "secretKey",
                bucket = "backup",
                path = "data/some",
                timeout = 200 * 1000,
                deleteAfterDays = 5
            };
        }

        /// <summary>
        /// 上传指定文件到七牛云对象存储。
        /// </summary>
        /// <param name="file">待上传的文件</param>
        /// <param name="suffix">文件在云空间里的后缀</param>
        /// <exception cref="Exception"/>
        public async Task Upload(string file, string suffix)
        {
            string key = path + suffix;
            Mac mac = new Mac(accessKey, secretKey);

            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope = bucket + ":" + key;
            putPolicy.Scope = bucket;
            if (timeout.HasValue && timeout.Value > 0)
            {
                putPolicy.SetExpires(timeout.Value / 1000);
            }

            if (deleteAfterDays.HasValue && deleteAfterDays.Value > 0)
            {
                putPolicy.DeleteAfterDays = deleteAfterDays;
            }   
            
            string json = putPolicy.ToJsonString();
            string token = Auth.CreateUploadToken(mac, json);
            UploadManager um = new UploadManager();
            var result = await um.UploadFileAsync(file, key, token);
            if (result.Code != 200)
            {
                throw new Exception("上传失败，服务器返回错误信息：" + result.Text);
            }
        }
    }
}
