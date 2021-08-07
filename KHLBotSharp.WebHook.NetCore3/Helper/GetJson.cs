using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KHLBotSharp.WebHook.NetCore3.Helper
{
    public static class Helper
    {
        public static async Task<string> GetJson(this Stream stream)
        {
            using (var ms = new MemoryStream(8192))
            {
                await stream.CopyToAsync(ms);
                int decompressedLength = 0;
                byte[] decompressedData = new byte[40960];
                using (InflaterInputStream inflater = new InflaterInputStream(ms))
                    decompressedLength = inflater.Read(decompressedData, 0, decompressedData.Length);
                var json = Encoding.UTF8.GetString(decompressedData, 0, decompressedLength);
                return json;
            }
        }
    }
}
