using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backup2Cloud.Conf
{
    /// <summary>
    /// 转换所有有NameAttribute特性的对象为Json字符串。
    /// </summary>
    public class NameConverter : JsonConverter
    {
        /// <summary>
        /// 禁止读操作。
        /// </summary>
        public override bool CanRead => false;

        /// <summary>
        /// 判断是否可以进行类型转换。
        /// </summary>
        /// <param name="objectType">目标类型</param>
        /// <returns>是否可以进行类型转换</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType.GetCustomAttributes(typeof(NameAttribute), true).Length == 1;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 为对象增加name属性。
        /// </summary>
        /// <param name="writer">Json写入器</param>
        /// <param name="value">对象实例</param>
        /// <param name="serializer">序列化方式</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var obj = JObject.FromObject(value);
            if (obj.Type == JTokenType.Object)
            {
                Type type = value.GetType();
                var attrs = type.GetCustomAttributes(typeof(NameAttribute), true);
                if (attrs.Length != 1)
                {
                    throw new InvalidCastException(string.Format("{0}不包含唯一特性{1}", type, typeof(NameAttribute)));
                }

                var attr = attrs[0] as NameAttribute;
                obj.AddFirst(new JProperty("name", attr.Name));
            }

            obj.WriteTo(writer);
        }
    }
}
