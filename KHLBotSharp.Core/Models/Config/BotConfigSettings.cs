using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace KHLBotSharp.Core.Models.Config
{
    public class BotConfigSettings : IBotConfigSettings
    {
        [JsonIgnore]
        protected string BotName { get; set; }
        public string BotToken { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string EncryptKey { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string VerifyToken { get; set; }
        public bool Active { get; set; } = true;
        public AppSettings Settings { get; set; } = new AppSettings();
        public int APIVersion { get; set; } = 3;
        public bool AtMe { get; set; } = true;
        public string[] ProcessChar { get; set; } = new string[] { ".", "。" };
        public bool Debug { get; set; } = false;
        public string DisableBotCommand { get; set; } = "";
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Save()
        {
            if (!BotName.Contains("Profiles"))
            {
                BotName = Path.Combine("Profiles", BotName);
            }
            File.WriteAllText(Path.Combine(BotName, "config.json"), JsonConvert.SerializeObject(this));
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Load(string botName = null)
        {
            if (botName != null)
            {
                BotName = botName;
            }
            if (!BotName.Contains("Profiles"))
            {
                BotName = Path.Combine("Profiles", BotName);
            }
            var settings = JsonConvert.DeserializeObject<BotConfigSettings>(File.ReadAllText(Path.Combine(BotName, "config.json")));
            var t = GetType();
            foreach (var prop in t.GetProperties())
            {
                prop.SetValue(this, prop.GetValue(settings));
            }
        }
    }
    /// <summary>
    /// 机器人config
    /// </summary>
    public interface IBotConfigSettings
    {
        string BotToken { get; set; }
        string EncryptKey { get; set; }
        string VerifyToken { get; set; }
        bool Active { get; set; }
        AppSettings Settings { get; set; }
        int APIVersion { get; set; }
        bool AtMe { get; set; }
        string[] ProcessChar { get; set; }
        bool Debug { get; set; }
        string DisableBotCommand { get; set; }
        /// <summary>
        /// 保存BotCOnfig，作出特定修改后可以直接保存起来下次打开Bot自动加载使用
        /// </summary>
        void Save();
        /// <summary>
        /// 重载BotConfig，botName留空就会自动加载正确的config因此无需担心
        /// </summary>
        /// <param name="botName"></param>
        void Load(string botName = null);
    }

    public class AppSettings : Dictionary<string, PluginSettings>
    {
        /// <summary>
        /// Try get plugin's settings
        /// </summary>
        /// <param name="pluginName"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public bool TryGet(string pluginName, out PluginSettings settings)
        {
            if (ContainsKey(pluginName))
            {
                settings = this[pluginName];
                return true;
            }
            settings = null;
            return false;
        }
    }
    public class PluginSettings : Dictionary<string, object>
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
