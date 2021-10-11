using Newtonsoft.Json;
using System;
using System.Drawing;

namespace KHLBotSharp.Common.Converter
{
    /// <summary>
    /// 解决开黑啦颜色返回RGBA而不是C#的Color
    /// </summary>
    public class ColorConverter : JsonConverter
    {
        /// <summary>
        /// 内部使用，无需理解
        /// </summary>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((Color)value).ToArgb());
        }
        /// <summary>
        /// 内部使用，无需理解
        /// </summary>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return Color.FromArgb(Convert.ToInt32(reader.Value));
        }
        /// <summary>
        /// 内部使用，无需理解
        /// </summary>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Color);
        }
    }
}
