using Backup2Cloud.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backup2Cloud.Conf
{
    /// <summary>
    /// 已命名的接口实现类的载入类。
    /// </summary>
    public static class NamedInterfaceLoader
    {
        /// <summary>
        /// 遍历所有上传接口的实现类，返回集合。
        /// </summary>
        /// <returns>返回上传接口实现类的集合。key是上传服务的提供商名称，value是上传接口的实现类类型。</returns>
        public static Dictionary<string, Type> Load(Type targetInterface)
        {
            Dictionary<string, Type> supportTypes = new Dictionary<string, Type>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    try
                    {
                        if (type.IsClass == false ||
                            type.IsAbstract ||
                            targetInterface.IsAssignableFrom(type) == false)
                        {
                            continue;
                        }

                        var attrs = type.GetCustomAttributes(typeof(NameAttribute), true);
                        if (attrs.Length == 0)
                        {
                            Log.Debug(string.Format("类{0}没有定义{1}特性，跳过。", type.ToString(), typeof(NameAttribute).ToString()));
                            continue;
                        }

                        if (attrs.Length > 1)
                        {
                            Log.Warn(string.Format("类{0}定义了多个{1}特性，只取第一个。", type.ToString(), typeof(NameAttribute).ToString()));
                        }

                        var attr = attrs[0] as NameAttribute;
                        if (supportTypes.ContainsKey(attr.Name))
                        {
                            Log.Warn(string.Format("类{0}的命名与{1}相同，{0}将被跳过。", type.ToString(), supportTypes[attr.Name].ToString()));
                            continue;
                        }
                        else
                        {
                            supportTypes[attr.Name] = type;
                            Log.Info(string.Format("初始化类{0}（{1}）完成。", type.ToString(), attr.Name));
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error(string.Format("反射类{0}时出现错误，跳过。", type.ToString()));
                        Log.Error(e.ToString());
                    }
                }
            }

            return supportTypes;
        }
    }
}
