using System;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace KHLBotSharp.IService
{
    public interface IHttpClientService
    {
        [Obsolete("Do not call this function in any Plugin!")]
        void Init(HttpClient client, [CallerMemberName] string caller = null);
        Task<T> GetAsync<T>(string url);
        Task<T> GetAsync<T>(string url, object data);
        Task<T> PostAsync<T>(string url, object data);
        Task<string> UploadFileAsync(Stream file);
        Task<string> UploadFileAsync(string file);
    }
}
