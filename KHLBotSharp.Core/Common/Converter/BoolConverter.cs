using Newtonsoft.Json;
using System;

namespace KHLBotSharp.Common.Converter
{
    /// <summary>
    /// 解决开黑啦经常文档会作出bool和int的修改（俗称boolint)
    /// </summary>
    public class BoolConverter : JsonConverter
    {
        /// <summary>
        /// 内部使用，无需理解
        /// </summary>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((bool)value) ? 1 : 0);
        }
        /// <summary>
        /// 内部使用，无需理解
        /// </summary>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return false;
            }
            return reader.Value.ToString() == "1";
        }
        /// <summary>
        /// 内部使用，无需理解
        /// </summary>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }
    }
}
