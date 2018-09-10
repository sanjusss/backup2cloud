using Backup2Cloud.Logging;
using Backup2Cloud.Worker;
using System;
using System.Collections.Generic;

namespace Backup2Cloud.Conf
{
    /// <summary>
    /// 上传接口载入类。
    /// </summary>
    public static class UploaderLoader
    {
        /// <summary>
        /// 遍历所有上传接口的实现类，返回集合。
        /// </summary>
        /// <returns>返回上传接口实现类的集合。key是上传服务的提供商名称，value是上传接口的实现类类型。</returns>
        public static Dictionary<string, Type> Load()
        {
            Dictionary<string, Type> supportTypes = new Dictionary<string, Type>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Type workerType = typeof(IUploader);
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    try
                    {
                        if (type.IsClass == false ||
                            type.IsAbstract ||
                            workerType.IsAssignableFrom(type) == false)
                        {
                            continue;
                        }

                        var attrs = type.GetCustomAttributes(typeof(ProviderNameAttribute), true);
                        if (attrs.Length == 0)
                        {
                            Log.Debug(string.Format("上传类{0}没有定义{1}特性，跳过。", type.ToString(), typeof(ProviderNameAttribute).ToString()));
                            continue;
                        }

                        if (attrs.Length > 1)
                        {
                            Log.Warn(string.Format("上传类{0}定义了多个服务商，只取第一个。", type.ToString()));
                        }

                        var attr = attrs[0] as ProviderNameAttribute;
                        if (supportTypes.ContainsKey(attr.Provider))
                        {
                            Log.Warn(string.Format("上传类{0}的服务商与{1}相同，{0}将被跳过。", type.ToString(), supportTypes[attr.Provider].ToString()));
                            continue;
                        }
                        else
                        {
                            supportTypes[attr.Provider] = type;
                            Log.Info(string.Format("初始化上传类{0}（{1}）完成。", type.ToString(), attr.Provider));
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
