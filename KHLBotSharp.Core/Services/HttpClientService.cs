using KHLBotSharp.IService;
using KHLBotSharp.Models.MessageHttps.ResponseMessage;
using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KHLBotSharp.Services
{
    public class HttpClientService : IHttpClientService
    {
        private HttpClient client;
        public HttpClientService(HttpClient client)
        {
            this.client = client;
        }
        public async Task<T> GetAsync<T>(string url)
        {
            return JsonConvert.DeserializeObject<T>(await (await client.GetAsync(url)).Content.ReadAsStringAsync());
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
            if (finalurl.EndsWith("&"))
            {
                finalurl.Remove(finalurl.Length - 2);
            }
            return GetAsync<T>(finalurl);
        }

        public async Task<T> PostAsync<T>(string url, object data)
        {
            var stringContent = new StringContent(JObject.FromObject(data).ToString(), Encoding.UTF8, "application/json");
            var result = await client.PostAsync(url, stringContent);
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<string> UploadFileAsync(Stream file)
        {
            var requestContent = new MultipartFormDataContent();
            var fileContent = new ByteArrayContent(ReadFully(file));
            requestContent.Add(fileContent, "file");
            var result = await client.PostAsync("asset/create", requestContent);
            return JsonConvert.DeserializeObject<KHLResponseMessage<UploadFileResponse>>(await result.Content.ReadAsStringAsync()).Data.Url;
        }

        private byte[] ReadFully(Stream input)
        {
            if(input is MemoryStream)
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
    }
}
