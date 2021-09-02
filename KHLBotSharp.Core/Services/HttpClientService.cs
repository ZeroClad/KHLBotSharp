using KHLBotSharp.Core.Models.Config;
using KHLBotSharp.IService;
using KHLBotSharp.Models.MessageHttps.ResponseMessage;
using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KHLBotSharp.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient client;
        private readonly ILogService log;
        private readonly IErrorRateService errorRateService;
        public HttpClientService(ILogService log, IErrorRateService errorRateService, IBotConfigSettings settings)
        {
            this.log = log;
            this.errorRateService = errorRateService;
            client = new HttpClient
            {
                BaseAddress = new Uri("https://www.kaiheila.cn/api/v" + settings.APIVersion + "/")
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", settings.BotToken);
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.Add("Keep-Alive", "600");
        }
        public async Task<T> GetAsync<T>(string url)
        {
            log.Write("GET " + url);
            var result = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var json = await result.Content.ReadAsStringAsync();
            result.Dispose();
            var data = JsonConvert.DeserializeObject<T>(json);
            return data;
        }

        public Task<T> GetAsync<T>(string url, object data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(url + "?");
            var proplist = data.GetType().GetProperties();
            foreach (var props in proplist)
            {
                var value = props.GetValue(data);
                if (value == default)
                {
                    continue;
                }
                sb.Append(props.Name + "=" + value);
                if (props != proplist.Last())
                {
                    sb.Append("&");
                }
            }
            var finalurl = sb.ToString();
            log.Write("GET "+finalurl);
            if (finalurl.EndsWith("&"))
            {
                finalurl.Remove(finalurl.Length - 2);
            }
            return GetAsync<T>(finalurl);
        }

        public async Task<T> PostAsync<T>(string url, object data)
        {
            try
            {
                var json = JObject.FromObject(data).ToString();
                log.Write("POST " + url + "\n" + json);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, stringContent);
                result.EnsureSuccessStatusCode();
                var content = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch(Exception e)
            {
                if(e is OperationCanceledException)
                {
                    log.Error("Connection Timeout for " + url);
                }
                else
                {
                    log.Error(e.ToString());
                }
                errorRateService.AddError();
                return await PostAsync<T>(url, data);
            }
        }

        public async Task<string> UploadFileAsync(string file)
        {
            log.Write("UploadFile " + file);
            try
            {
                var requestContent = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(ReadFully(File.OpenRead(file)));
                requestContent.Add(fileContent, "file", file.Substring(file.LastIndexOf("\\")));
                var result = await client.PostAsync("asset/create", requestContent);
                if (!result.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(result.StatusCode.ToString());
                }
                var data = JsonConvert.DeserializeObject<KHLResponseMessage<UploadFileResponse>>(await result.Content.ReadAsStringAsync());
                if (data.Data.Url == null)
                {
                    throw new HttpRequestException("Upload file failed.\n" + data.Message);
                }
                return data.Data.Url;
            }
            catch(Exception e)
            {
                if (e is OperationCanceledException)
                {
                    log.Error("Connection Timeout for " + client.BaseAddress + "asset/create");
                }
                else
                {
                    log.Error(e.ToString());
                }
                if (e is FileNotFoundException)
                {
                    throw;
                }
                errorRateService.AddError();
                return await UploadFileAsync(file);
            }

        }

        public async Task<string> UploadFileAsync(Stream file)
        {
            try
            {
                var requestContent = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(ReadFully(file));
                requestContent.Add(fileContent, "file");
                var result = await client.PostAsync("asset/create", requestContent);
                if (!result.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(result.StatusCode.ToString());
                }
                var data = JsonConvert.DeserializeObject<KHLResponseMessage<UploadFileResponse>>(await result.Content.ReadAsStringAsync());
                if (data.Data.Url == null)
                {
                    throw new HttpRequestException("Upload file failed.\n" + data.Message);
                }
                return data.Data.Url;
            }
            catch (Exception e)
            {
                if (e is OperationCanceledException)
                {
                    log.Error("Connection Timeout for " + client.BaseAddress + "asset/create");
                }
                else
                {
                    log.Error(e.ToString());
                }
                if (e is FileNotFoundException)
                {
                    throw;
                }
                errorRateService.AddError();
                return await UploadFileAsync(file);
            }
        }

        private byte[] ReadFully(Stream input)
        {
            if (input is MemoryStream)
            {
                return (input as MemoryStream).ToArray();
            }
            else
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    input.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }

        ~HttpClientService()
        {
            client.Dispose();
        }
    }
}
