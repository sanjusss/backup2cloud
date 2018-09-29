using Backup2Cloud.Conf;
using System;
using System.Threading.Tasks;

namespace Backup2Cloud.Uploader
{
    /// <summary>
    /// 上传文件接口，实现此接口需要同时引用NameAttribute表明服务商。
    /// </summary>
    public interface IUploader : IConfigurable, IExampled
    {
        /// <summary>
        /// 上传文件。
        /// </summary>
        /// <param name="file">待上传的文件</param>
        /// <param name="suffix">文件在云空间里的后缀</param>
        /// <exception cref="Exception"/>
        Task Upload(string file, string suffix);
    }
}
