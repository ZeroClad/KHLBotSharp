using KHLBotSharp.Core.IService;
using KHLBotSharp.Core.Models.Config;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace KHLBotSharp.Core.Services
{
    public class AudioChannelService : IAudioChannelService
    {
        private readonly string token;
        private Process player;
        public AudioChannelService(IBotConfigSettings config)
        {
            token = config.BotToken;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Task Init()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process p = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = "/c ffmpeg -v",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        WorkingDirectory = Environment.CurrentDirectory
                    }
                };
                p.Start();
                string ffmpegcheck = p.StandardOutput.ReadToEnd();
                if (ffmpegcheck.Contains("not recognized"))
                {
                    //no install ffmpeg
                    throw new FileNotFoundException("ffmpeg not found! Please download it at https://www.gyan.dev/ffmpeg/builds/ffmpeg-release-full.7z and setup environment path!");
                }
                if (!File.Exists("khl-voice.exe"))
                {
                    var zipArchive = new ZipArchive(new MemoryStream(Resource.khl_voice_exe));
                    zipArchive.ExtractToDirectory(Environment.CurrentDirectory);
                }
            }
            else
                if (!File.Exists("khl-voice"))
                {
                var zipArchive = new ZipArchive(new MemoryStream(Resource.khl_voice));
                zipArchive.ExtractToDirectory(Environment.CurrentDirectory);
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Task Play(string fileName, string channelId, bool repeat  = false)
        {
            return Task.Run(() =>
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    player = new Process()
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "khl-voice.exe",
                            Arguments = "-i " + fileName + " -t " + token + " -c " + channelId,
                            CreateNoWindow = true,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            WorkingDirectory = Environment.CurrentDirectory
                        }
                    };
                }
                else
                {
                    player = new Process()
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "khl-voice",
                            Arguments = "-i " + fileName + " -t " + token + " -c " + channelId,
                            CreateNoWindow = true,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            WorkingDirectory = Environment.CurrentDirectory
                        }
                    };
                }
                player.Start();
            });
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Task Stop()
        {
            player.Kill();
            return Task.CompletedTask;
        }
    }
}
