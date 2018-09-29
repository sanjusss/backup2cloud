using System;
using System.Collections.Generic;
using System.Text;

namespace Backup2Cloud.Conf
{
    /// <summary>
    /// 举例接口。
    /// </summary>
    public interface IExampled
    {
        /// <summary>
        /// 获取示例配置。
        /// </summary>
        /// <returns>示例的配置</returns>
        object GetExample();
    }
}
