namespace Backup2Cloud.Conf
{
    /// <summary>
    /// 表明可以由用户配置的结构/类的接口。
    /// </summary>
    public interface IConfigurable
    {
        /// <summary>
        /// 提示信息
        /// </summary>
        string Tips { get; }
    }
}
