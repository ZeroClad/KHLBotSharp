using KHLBotSharp.Core.Models.Config;
using KHLBotSharp.IService;
using KHLBotSharp.Models.MessageHttps.ResponseMessage;
using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data;
using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KHLBotSharp.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient client;
        private readonly ILogService log;
        private readonly IErrorRateService errorRateService;
        private readonly IBotConfigSettings settings;
        private readonly Stopwatch stopwatch = new Stopwatch();
        public HttpClientService(ILogService log, IErrorRateService errorRateService, IBotConfigSettings settings, HttpClient client)
        {
            this.log = log;
            this.errorRateService = errorRateService;
            client.BaseAddress = new Uri("https://www.kaiheila.cn/api/v" + settings.APIVersion + "/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", settings.BotToken);
            client.Timeout = new TimeSpan(0, 0, 2);
            this.client = client;
            this.settings = settings;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<T> GetAsync<T>(string url)
        {
            stopwatch.Reset();
            stopwatch.Start();
            try
            {
                log.Write("GET " + url);
                using (var result = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                {
                    using (var stream = await result.Content.ReadAsStreamAsync())
                    {
                        using (StreamReader r = new StreamReader(stream))
                        {
                            if (result.IsSuccessStatusCode)
                            {
                                using (JsonReader jr = new JsonTextReader(r))
                                {
                                    JsonSerializer s = new JsonSerializer();
                                    var data = s.Deserialize<T>(jr);
                                    result.Dispose();
                                    stopwatch.Stop();
                                    log.Write("GET " + url + " done in " + stopwatch.ElapsedMilliseconds + " ms");
                                    return data;
                                }
                            }
                            else
                            {
                                log.Write(await r.ReadToEndAsync());
                                throw new HttpRequestException(((int)result.StatusCode).ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (e is OperationCanceledException)
                {
                    log.Error("Connection Timeout for " + url);
                }
                else
                {
                    log.Error(e.ToString());
                }
                errorRateService.AddError();
                if (e.Message.Contains("401") || e.Message.Contains("403"))
                {
                    throw new NoPermissionException();
                }
                if (e.Message.Contains("400"))
                {
                    settings.Active = false;
                    log.Error("机器人已被封禁，自动取消Bot运行");
                    settings.Save();
                    Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                    Environment.Exit(0);
                }
                if (e.Message.Contains("429"))
                {
                    throw new RateLimitException();
                }
                return await GetAsync<T>(url);
            }

        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Task<T> GetAsync<T>(string url, object data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(url + "?");
            var proplist = data.GetType().GetProperties();
            foreach (var props in proplist)
            {
                if (Attribute.IsDefined(props, typeof(JsonIgnoreAttribute)))
                {
                    continue;
                }
                var value = props.GetValue(data);
                if (value == default || value == null)
                {
                    continue;
                }
                if (Attribute.IsDefined(props, typeof(JsonPropertyAttribute)))
                {
                    var names = props.GetCustomAttributes(false).Select(x => x as JsonPropertyAttribute).Where(y => y != null);
                    if (names.Count() > 0)
                    {
                        sb.Append(names.First().PropertyName + "=" + value);
                        if (props != proplist.Last())
                        {
                            sb.Append("&");
                        }
                    }
                    else
                    {
                        sb.Append(props.Name + "=" + value);
                        if (props != proplist.Last())
                        {
                            sb.Append("&");
                        }
                    }

                }
                else
                {
                    sb.Append(props.Name + "=" + value);
                    if (props != proplist.Last())
                    {
                        sb.Append("&");
                    }
                }
            }
            var finalurl = sb.ToString();
            if (finalurl.EndsWith("&"))
            {
                finalurl.Remove(finalurl.Length - 2);
            }
            return GetAsync<T>(finalurl);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<KHLResponseMessage<T>> PostAsync<T>(string url, object data) where T : BaseData
        {
            try
            {
                stopwatch.Reset();
                stopwatch.Start();
                var json = JsonConvert.SerializeObject(data);
                log.Write("POST " + url + " : " + json);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, stringContent);
                using (var stream = await result.Content.ReadAsStreamAsync())
                {
                    using (StreamReader r = new StreamReader(stream))
                    {
                        if (result.IsSuccessStatusCode)
                        {
                            using (JsonReader jr = new JsonTextReader(r))
                            {
                                JsonSerializer s = new JsonSerializer();
                                var resultdata = s.Deserialize<KHLResponseMessage<T>>(jr);
                                if (resultdata.Code != 0)
                                {
                                    throw new HttpRequestException(resultdata.Message);
                                }
                                result.Dispose();
                                stopwatch.Stop();
                                log.Write("POST " + url + " done in " + stopwatch.ElapsedMilliseconds + " ms");
                                return resultdata;
                            }
                        }
                        else
                        {
                            var body = await r.ReadToEndAsync();
                            log.Write(body);
                            throw new HttpRequestException(((int)result.StatusCode).ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (e is OperationCanceledException)
                {
                    log.Error("Connection Timeout for " + url);
                }
                else
                {
                    log.Error(e.ToString());
                }
                if (e.Message.Contains("401") || e.Message.Contains("403"))
                {
                    throw new NoPermissionException();
                }
                if (e.Message.Contains("400"))
                {
                    settings.Active = false;
                    log.Error("机器人已被封禁，自动取消Bot运行");
                    settings.Save();
                    Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                    Environment.Exit(0);
                }
                if (e.Message.Contains("429"))
                {
                    throw new RateLimitException();
                }
                errorRateService.AddError();
                return await PostAsync<T>(url, data);
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<string> UploadFileAsync(string file)
        {
            log.Write("UploadFile " + file);
            try
            {
                var requestContent = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(ReadFully(File.OpenRead(file)));
                requestContent.Add(fileContent, "file", file.Substring(file.LastIndexOf("\\")));
                using (var client = new HttpClient
                {
                    BaseAddress = new Uri("https://www.kaiheila.cn/api/v" + settings.APIVersion + "/")
                })
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", settings.BotToken);
                    client.Timeout = new TimeSpan(0, 0, 5);
                    var result = await client.PostAsync("asset/create", requestContent);
                    if (result.IsSuccessStatusCode)
                    {
                        var data = JsonConvert.DeserializeObject<KHLResponseMessage<UploadFileResponse>>(await result.Content.ReadAsStringAsync());
                        result.Dispose();
                        if (data.Data.Url == null)
                        {
                            throw new HttpRequestException("Upload file failed.\n" + data.Message);
                        }
                        return data.Data.Url;
                    }
                    else
                    {
                        log.Write(await result.Content.ReadAsStringAsync());
                        throw new HttpRequestException(((int)result.StatusCode).ToString());
                    }
                }

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
                if (e.Message.Contains("401") || e.Message.Contains("403"))
                {
                    throw new NoPermissionException();
                }
                if (e.Message.Contains("400"))
                {
                    settings.Active = false;
                    log.Error("机器人已被封禁，自动取消Bot运行");
                    settings.Save();
                    Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                    Environment.Exit(0);
                }
                if (e.Message.Contains("429"))
                {
                    throw new RateLimitException();
                }
                errorRateService.AddError();
                return await UploadFileAsync(file);
            }

        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<string> UploadFileAsync(Stream file, string fileName)
        {
            try
            {
                var requestContent = new MultipartFormDataContent();
                var fileContent = new StreamContent(file);
                if (fileName.Contains("\\"))
                {
                    fileName = fileName.Substring(fileName.LastIndexOf("\\"));
                }
                else if (fileName.Contains("/"))
                {
                    fileName = fileName.Substring(fileName.LastIndexOf("/"));
                }
                requestContent.Add(fileContent, "file", fileName);
                using (
                var client = new HttpClient
                {
                    BaseAddress = new Uri("https://www.kaiheila.cn/api/v" + settings.APIVersion + "/")
                })
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", settings.BotToken);
                    client.Timeout = new TimeSpan(0, 0, 5);
                    var result = await client.PostAsync("asset/create", requestContent);
                    if (result.IsSuccessStatusCode)
                    {
                        var data = JsonConvert.DeserializeObject<KHLResponseMessage<UploadFileResponse>>(await result.Content.ReadAsStringAsync());
                        result.Dispose();
                        if (data.Data.Url == null)
                        {
                            throw new HttpRequestException("Upload file failed.\n" + data.Message);
                        }
                        return data.Data.Url;
                    }
                    else
                    {
                        log.Write(await result.Content.ReadAsStringAsync());
                        throw new HttpRequestException(((int)result.StatusCode).ToString());
                    }
                }
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
                if (e.Message.Contains("401") || e.Message.Contains("403"))
                {
                    throw new NoPermissionException();
                }
                if (e.Message.Contains("400"))
                {
                    settings.Active = false;
                    log.Error("机器人已被封禁，自动取消Bot运行");
                    settings.Save();
                    Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                    Environment.Exit(0);
                }
                if (e.Message.Contains("429"))
                {
                    throw new RateLimitException();
                }
                errorRateService.AddError();
                return await UploadFileAsync(file, fileName);
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

        public async Task PostAsync(string url, object data)
        {
            try
            {
                stopwatch.Reset();
                stopwatch.Start();
                var json = JsonConvert.SerializeObject(data);
                log.Write("POST " + url + " : " + json);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, stringContent);
                using (var stream = await result.Content.ReadAsStreamAsync())
                {
                    using (StreamReader r = new StreamReader(stream))
                    {
                        if (result.IsSuccessStatusCode)
                        {
                            using (JsonReader jr = new JsonTextReader(r))
                            {
                                JsonSerializer s = new JsonSerializer();
                                var resultdata = s.Deserialize<KHLResponseMessage>(jr);
                                if (resultdata.Code != 0)
                                {
                                    throw new HttpRequestException(resultdata.Message);
                                }
                                result.Dispose();
                                stopwatch.Stop();
                                log.Write("POST " + url + " done in " + stopwatch.ElapsedMilliseconds + " ms");
                            }
                        }
                        else
                        {
                            var body = await r.ReadToEndAsync();
                            log.Write(body);
                            throw new HttpRequestException(((int)result.StatusCode).ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (e is OperationCanceledException)
                {
                    log.Error("Connection Timeout for " + url);
                }
                else
                {
                    log.Error(e.ToString());
                }
                if (e.Message.Contains("401") || e.Message.Contains("403"))
                {
                    throw new NoPermissionException();
                }
                if (e.Message.Contains("400"))
                {
                    settings.Active = false;
                    log.Error("机器人已被封禁，自动取消Bot运行");
                    settings.Save();
                    Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                    Environment.Exit(0);
                }
                if (e.Message.Contains("429"))
                {
                    throw new RateLimitException();
                }
                errorRateService.AddError();
                await PostAsync(url, data);
            }
        }

        ~HttpClientService()
        {
            client.Dispose();
        }
    }

    public class NoPermissionException : Exception
    {
        public override string Message => "机器人没有权限进行相关操作，请给予相应的权限后再继续使用指令！";
    }

    public class RateLimitException : Exception
    {
        public override string Message => "机器人已被限速！将会自动取消所有正在发送的请求！";
    }

    public class BannedException : Exception
    {
        public override string Message => "机器人已被封禁！请取消机器人的运行！";
    }
}
