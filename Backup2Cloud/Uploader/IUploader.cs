using Backup2Cloud.Conf;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Backup2Cloud.Worker
{
    /// <summary>
    /// 上传文件接口，实现此接口需要同时引用ProviderNameAttribute表明服务商。
    /// </summary>
    public interface IUploader : IConfigurable
    {
        /// <summary>
        /// 服务商名称，应该与ProviderNameAttribute相同。
        /// </summary>
        [JsonProperty("name")]
        string Name { get; }

        /// <summary>
        /// 上传文件。
        /// </summary>
        /// <param name="file">待上传的文件</param>
        /// <param name="suffix">文件在云空间里的后缀</param>
        /// <exception cref="Exception"/>
        Task Upload(string file, string suffix);

        /// <summary>
        /// 获取示例的上传配置。
        /// </summary>
        /// <returns>示例的上传配置</returns>
        IUploader GetExample();
    }
}
