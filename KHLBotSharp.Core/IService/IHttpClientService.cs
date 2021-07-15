using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace KHLBotSharp.IService
{
    public interface IHttpClientService
    {
        void Init(HttpClient client);
         Task<T> GetAsync<T>(string url);
         Task<T> GetAsync<T>(string url, object data);
         Task<T> PostAsync<T>(string url, object data);
         Task<string> UploadFileAsync(Stream file);
    }
}
