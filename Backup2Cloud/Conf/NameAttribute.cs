using System;

namespace Backup2Cloud.Conf
{
    /// <summary>
    /// 名称特性。
    /// </summary>
    public class NameAttribute : Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// 定义名称。
        /// </summary>
        /// <param name="name">名称</param>
        public NameAttribute(string name)
        {
            _name = name;
        }
    }
}
