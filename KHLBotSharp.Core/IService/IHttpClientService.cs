using System.IO;
using System.Threading.Tasks;

namespace KHLBotSharp.IService
{
    public interface IHttpClientService
    {
         Task<T> GetAsync<T>(string url);
         Task<T> GetAsync<T>(string url, object data);
         Task<T> PostAsync<T>(string url, object data);
         Task<string> UploadFileAsync(Stream file);
    }
}
