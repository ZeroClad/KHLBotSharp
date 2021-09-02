using System;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace KHLBotSharp.IService
{
    public interface IHttpClientService
    {
        Task<T> GetAsync<T>(string url);
        Task<T> GetAsync<T>(string url, object data);
        Task<T> PostAsync<T>(string url, object data);
        Task<string> UploadFileAsync(Stream file);
        Task<string> UploadFileAsync(string file);
    }
}
