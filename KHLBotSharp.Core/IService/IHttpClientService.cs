using KHLBotSharp.Models.MessageHttps.ResponseMessage;
using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace KHLBotSharp.IService
{
    /// <summary>
    /// 基础HttpClient，包装过，正常情况下无需使用这个玩意
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IHttpClientService
    {
        /// <summary>
        /// Get request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<T> GetCustomAsync<T>(string url);
        /// <summary>
        /// 发送Get请求，返回T类型数据
        /// </summary>
        /// <returns></returns>
        Task<KHLResponseMessage<T>> GetAsync<T>(string url) where T : BaseData;
        /// <summary>
        /// 发送Get请求，并且附带数据，返回T类型请求
        /// </summary>
        /// <returns></returns>
        Task<KHLResponseMessage<T>> GetAsync<T>(string url, object data) where T : BaseData;
        /// <summary>
        /// 发送Post请求，并且附带数据，返回T类型请求
        /// </summary>
        /// <returns></returns>
        Task<KHLResponseMessage<T>> PostAsync<T>(string url, object data) where T : BaseData;
        /// <summary>
        /// 发送Post请求，并且附带数据，返回T类型请求
        /// </summary>
        /// <returns></returns>
        Task PostAsync(string url, object data);
        /// <summary>
        /// 上传Stream数据作为文件到开黑服务器
        /// </summary>
        /// <returns></returns>
        Task<string> UploadFileAsync(Stream file, string fileName);
        /// <summary>
        /// 上传路径中的文件到开黑服务器，可支持http链接文件
        /// </summary>
        /// <returns></returns>
        Task<string> UploadFileAsync(string file);
    }
}
