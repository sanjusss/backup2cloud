using Backup2Cloud.Conf;

namespace Backup2Cloud.DataSource
{
    /// <summary>
    /// 数据源接口，实现此接口需要同时引用NameAttribute表明数据源类型。
    /// </summary>
    public interface IDataSource : IConfigurable, IExampled
    {
        /// <summary>
        /// 保存数据。
        /// </summary>
        /// <param name="des">保存的路径</param>
        void SaveData(string des);
    }
}
