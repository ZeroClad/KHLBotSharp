﻿using KHLBotSharp.IService;
using KHLBotSharp.Models.MessageHttps.ResponseMessage;
using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KHLBotSharp.Services
{
    public class HttpClientService : IHttpClientService
    {
        private HttpClient client;
        private bool InitState;
        private readonly ILogService log;
        public HttpClientService(ILogService log)
        {
            this.log = log;
        }
        public async Task<T> GetAsync<T>(string url)
        {
            log.Write("GET " + url);
            var result = await client.GetAsync(url);
            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException(result.StatusCode.ToString());
            }
            var json = await result.Content.ReadAsStringAsync();
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

        [Obsolete("Don't call in any plugin! This should be the bot host to init, not you!")]
        public void Init(HttpClient client, [CallerMemberName] string caller = null)
        {
            if (InitState)
            {
                throw new InvalidOperationException("This cannot be called in plugin!");
            }
            this.InitState = true;
            this.client = client;
        }

        public async Task<T> PostAsync<T>(string url, object data)
        {
            var json = JObject.FromObject(data).ToString();
            log.Write("POST "+ url + "\n" +json);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(url, stringContent);
            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException(result.StatusCode.ToString());
            }
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<string> UploadFileAsync(string file)
        {
            log.Write("UploadFile " + file);
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

        public async Task<string> UploadFileAsync(Stream file)
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
