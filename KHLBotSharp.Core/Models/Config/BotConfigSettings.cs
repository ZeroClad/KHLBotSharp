using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace KHLBotSharp.Core.Models.Config
{
    public class BotConfigSettings:IBotConfigSettings
    {
        public string BotToken { get; set; }
        public bool Active { get; set; } = true;
        public AppSettings Settings { get; set; } = new AppSettings();
        public int APIVersion { get; set; } = 3;
    }

    public interface IBotConfigSettings
    {
        string BotToken { get; set; }
        bool Active { get; set; }
        AppSettings Settings { get; set; }
        int APIVersion { get; set; }
    }

    public class AppSettings: Dictionary<string, PluginSettings>
    {
        /// <summary>
        /// Try get plugin's settings
        /// </summary>
        /// <param name="pluginName"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public bool TryGet(string pluginName, out PluginSettings settings)
        {
            if (this.ContainsKey(pluginName))
            {
                settings = this[pluginName];
                return true;
            }
            settings = null;
            return false;
        }
    }
    public class PluginSettings: Dictionary<string, object>
    {
        /// <summary>
        /// Try get config value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool TryGet<T>(string key, out T value)
        {
            if (ContainsKey(key))
            {
                var converter = TypeDescriptor.GetConverter(this[key].GetType());
                if (converter.CanConvertTo(typeof(T)))
                {
                    value = (T)converter.ConvertTo(this[key], typeof(T));
                    return true;
                }
                value = default;
                return false;
            }
            value = default;
            return false;
        }
        /// <summary>
        /// Get config value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T GetValue<T>(string key)
        {
            if (ContainsKey(key))
            {
                var converter = TypeDescriptor.GetConverter(this[key].GetType());
                if (converter.CanConvertTo(typeof(T)))
                {
                   return (T)converter.ConvertTo(this[key], typeof(T));
                }
                throw new InvalidCastException("Unable to convert to Type " + typeof(T).Name);
            }
            return default;
        }
    }
}
