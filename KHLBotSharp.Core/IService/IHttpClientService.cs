using System.IO;
using System.Threading.Tasks;

namespace KHLBotSharp.IService
{
    public interface IHttpClientService
    {
        public Task<T> GetAsync<T>(string url);
        public Task<T> GetAsync<T>(string url, object data);
        public Task<T> PostAsync<T>(string url, object data);
        public Task<string> UploadFileAsync(Stream file);
    }
}
