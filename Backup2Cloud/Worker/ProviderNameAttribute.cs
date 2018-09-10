using System;

namespace Backup2Cloud.Worker
{
    /// <summary>
    /// 服务商名称。
    /// 没有此特性的类将不会被解析。
    /// </summary>
    public class ProviderNameAttribute : Attribute
    {
        private readonly string _provider;

        /// <summary>
        /// 服务商名称
        /// </summary>
        public string Provider
        {
            get
            {
                return _provider;
            }
        }

        /// <summary>
        /// 定义服务商的名称。
        /// </summary>
        /// <param name="provider">服务商</param>
        public ProviderNameAttribute(string provider)
        {
            _provider = provider;
        }
    }
}
