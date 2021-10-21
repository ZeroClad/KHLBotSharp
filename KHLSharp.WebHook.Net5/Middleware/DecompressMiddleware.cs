using KHLBotSharp.IService;
using KHLBotSharp.WebHook.Net5.Helper;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace KHLBotSharp.WebHook.Net5.Middleware
{
    //Copy from https://github.com/kaiheila-community/kaiheila-dotnet/blob/master/src/core/Client/WebHook/WebHookClient.cs
    public class DecompressMiddleware
    {
        private readonly RequestDelegate _next;
        private const string CompressKey = "compress";
        public DecompressMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, ILogService logService)
        {
            Stopwatch s = Stopwatch.StartNew();
            if (context.Request.Query.ContainsKey(CompressKey) && context.Request.Query[CompressKey] == "0")
            {
                context.Items["Content"] = await context.Request.Body.GetJson();
                await _next(context);
                s.Stop();
                logService.Write("Message processed in " + s.ElapsedMilliseconds + " ms");
                return;
            }

            // Decompress Deflate
            MemoryStream stream = new();
            await context.Request.Body.CopyToAsync(stream);
            await context.Request.Body.DisposeAsync();

            // Magic headers of zlib:
            // 78 01 - No Compression/low
            // 78 9C - Default Compression
            // 78 DA - Best Compression
            stream.Position = 2;

            DeflateStream deflateStream = new(stream, CompressionMode.Decompress, true);
            MemoryStream resultStream = new();
            await deflateStream.CopyToAsync(resultStream);
            await deflateStream.DisposeAsync();
            await stream.DisposeAsync();

            // Rewind
            resultStream.Position = 0;

            StreamReader reader = new(resultStream);
            string result = await reader.ReadToEndAsync();
            await resultStream.DisposeAsync();

            context.Items["Content"] = result;
            reader.Dispose();

            await _next(context);
            s.Stop();
            logService.Write("Message processed in " + s.ElapsedMilliseconds + " ms");
        }
    }
}
