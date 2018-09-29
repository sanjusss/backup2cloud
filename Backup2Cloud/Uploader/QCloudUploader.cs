using QCloudSDK.COS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Backup2Cloud.Worker
{
    /// <summary>
    /// 腾讯云COS上传实现类。
    /// </summary>
    [ProviderName("qcloud")]
    public class QCloudUploader : IUploader
    {
        /// <summary>
        /// 服务商名称
        /// </summary>
        public string Name => "qcloud";
        /// <summary>
        /// 腾讯云 APPID
        /// </summary>
        public string appId;
        /// <summary>
        /// 腾讯云 SecretId
        /// </summary>
        public string secretId;
        /// <summary>
        /// 腾讯云 SecretKey
        /// </summary>
        public string secretKey;
        /// <summary>
        /// 文件绝对路径，作为前缀使用。
        /// </summary>
        public string url;
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Tips
        {
            get
            {
                return "appId：腾讯云 APPID（可以在控制台查看）；" +
                    "secretId：腾讯云 SecretId（可以在控制台查看）；" +
                    "secretKey：腾讯云 SecretKey（可以在控制台查看）；" +
                    "url：绝对路径，例如\"https://backup-123456.cos.ap-chengdu.myqcloud.com/test\"，" +
                    "最终会生成类似\"https://backup-123456.cos.ap-chengdu.myqcloud.com/test201809092054.zip\"之类的文件，" +
                    "可以在 控制台-对象存储-存储桶列表-点击实际的桶名称-基础设置-访问域名 中找到url的前部分，后部分是文件相对于桶的路径";
            }
        }

        /// <summary>
        /// 获取腾讯云示例实例。
        /// </summary>
        /// <returns>腾讯云示例配置实例</returns>
        public IUploader GetExample()
        {
            return new QCloudUploader()
            {
                appId = "123456",
                secretId = "xxxx",
                secretKey = "yyyy",
                url = "https://backup-123456.cos.ap-chengdu.myqcloud.com/test"
            };
        }

        /// <summary>
        /// 上传指定文件到腾讯云COS。
        /// </summary>
        /// <param name="file">待上传的文件</param>
        /// <param name="suffix">文件在云空间里的后缀</param>
        /// <exception cref="Exception"/>
        public async Task Upload(string file, string suffix)
        {
            Client client = new Client(new Configuration()
            {
                AppId = appId,
                SecretId = secretId,
                SecretKey = secretKey
            });
            using (FileStream stream = new FileStream(file, FileMode.Open))
            {
                await client.PutObjectAsync(url + suffix, stream);
            }
        }
    }
}
