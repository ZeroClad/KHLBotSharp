using KHLBotSharp.Core.Models.Config;
using KHLBotSharp.IService;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KHLBotSharp.Services
{
    /// <summary>
    /// Webhook解析encrypt用
    /// </summary>
    public class DecoderService : IDecoderService
    {
        private readonly IBotConfigSettings config;
        private readonly ILogService log;
        public DecoderService(IBotConfigSettings config, ILogService log)
        {
            this.config = config;
            this.log = log;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<JObject> DecodeEncrypt(JToken code)
        {
            if (code is JObject)
            {
                var obj = code as JObject;
                if (obj.ContainsKey("encrypt"))
                {
                    try
                    {
                        //decode start
                        byte[] data = Convert.FromBase64String(obj.Value<string>("encrypt"));
                        string decoded = Encoding.UTF8.GetString(data);
                        string iv = decoded.Substring(0, 16);
                        var aesEncrypted = decoded.Substring(16);
                        var key = config.EncryptKey.PadRight(32, '\0');
                        var result = await Decrypt(aesEncrypted, key, iv);
                        result = result.Substring(0, result.LastIndexOf("}") + 1);
                        return JObject.Parse(result);
                    }
                    catch
                    {
                        log.Error("Decode failed, received " + code.ToString());
                    }

                }
                //not encrypted
                return obj;
            }
            throw new ArgumentException("Invalid json received. Expected Object get Array");
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Task<string> GetEventType(JToken code)
        {
            return Task.Run(() =>
            {
                if (code is JObject)
                {
                    var obj = code.ToObject<JObject>();
                    if (obj.TryGetValue("d", out JToken data))
                    {
                        if (data is JObject)
                        {
                            //No Encrypt Challenge
                            if ((data as JObject).ContainsKey("challenge"))
                            {
                                log.Debug("Challenge Received");
                                return "Challenge";
                            }
                        }
                        //Default Type
                        return data.Value<string>("type");
                    }
                    else
                    {
                        //Encrypted Challenge
                        if (obj.ContainsKey("encrypt"))
                        {
                            log.Debug("Challenge Received");
                            return "Challenge";
                        }
                    }
                }
                return null;
            });
        }

        private async Task<string> Decrypt(string cipherData, string keyString, string ivString)
        {
            byte[] key = Encoding.UTF8.GetBytes(keyString);
            byte[] iv = Encoding.UTF8.GetBytes(ivString);

            try
            {
                using (var aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(keyString);
                    aes.IV = Encoding.UTF8.GetBytes(ivString);
                    aes.KeySize = 256;
                    aes.Padding = PaddingMode.Zeros;
                    aes.Mode = CipherMode.CBC;
                    using (var memoryStream = new MemoryStream(Convert.FromBase64String(cipherData)))
                    using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                    using (var s = new StreamReader(cryptoStream))
                    {
                        return await s.ReadToEndAsync();
                    }
                }
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.ToString());
                return null;
            }
            // You may want to catch more exceptions here...
        }
    }
}
