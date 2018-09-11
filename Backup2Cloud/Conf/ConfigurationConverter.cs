using Backup2Cloud.Worker;
using Backup2Cloud.Worker.Uploader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Backup2Cloud.Conf
{
    /// <summary>
    /// 将Json转为SingleConfiguration类型的转换器。
    /// 自动实例化uploader。
    /// </summary>
    public class ConfigurationConverter : JsonConverter
    {
        /// <summary>
        /// 所有支持的uploader类型。
        /// key表示上传服务提供商。
        /// </summary>
        private readonly Dictionary<string, Type> supportTypes = null;
        /// <summary>
        /// 初始化
        /// </summary>
        public ConfigurationConverter()
        {
            supportTypes = UploaderLoader.Load();
        }

        /// <summary>
        /// 禁止写操作。
        /// </summary>
        public override bool CanWrite => false;

        /// <summary>
        /// 判断是否可以进行类型转换。
        /// </summary>
        /// <param name="objectType">目标类型</param>
        /// <returns>是否可以进行类型转换</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(SingleConfiguration);
        }

        /// <summary>
        /// 将Json转为SingleConfiguration类型。
        /// </summary>
        /// <param name="reader">Json读取器</param>
        /// <param name="objectType">目标类型</param>
        /// <param name="existingValue">已存在的值</param>
        /// <param name="serializer">序列化生成器</param>
        /// <returns>SingleConfiguration实例</returns>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            SingleConfiguration target = new SingleConfiguration()
            {
                name = jsonObject["name"].ToString(),
                provider = jsonObject["provider"].ToString(),
                path = jsonObject["path"].ToString(),
                crontab = jsonObject["crontab"].ToObject<HashSet<string>>()
            };
            if (supportTypes.ContainsKey(target.provider) == false)
            {
                throw new ArgumentOutOfRangeException(string.Format("没有找到\"{0}\"的上传实现。", target.provider));
            }

            target.uploader = jsonObject["uploader"].ToObject(supportTypes[target.provider]) as IUploader;
            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
