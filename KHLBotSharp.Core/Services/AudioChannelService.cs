using KHLBotSharp.Core.IService;
using KHLBotSharp.Core.Models.Config;
using KHLBotSharp.IService;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace KHLBotSharp.Core.Services
{
    public class AudioChannelService : IAudioChannelService
    {
        private readonly string token;
        private Process player;
        private ILogService logService;
        public AudioChannelService(IBotConfigSettings config, ILogService logService)
        {
            token = config.BotToken;
            this.logService = logService;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual async Task Init()
        {
            logService.Info("Initing KHL-Voice...");
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
                    HttpClient hc = new HttpClient();
                    hc.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36");
                    var result = await hc.GetAsync("https://img.kaiheila.cn/attachments/2021-12/21/61c194328d749");
                    using (var stream = await result.Content.ReadAsStreamAsync())
                    {
                        using (var fs = File.Create("khl-voice.exe"))
                        {
                            stream.CopyTo(fs);
                        }
                    }
                }
            }
            else
                if (!File.Exists("khl-voice"))
                {
                    HttpClient hc = new HttpClient();
                    hc.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36");
                    var result = await hc.GetAsync("https://img.kaiheila.cn/attachments/2021-12/21/61c193d6efffb");
                    using (var stream = await result.Content.ReadAsStreamAsync())
                    {
                        using (var fs = File.Create("khl-voice"))
                        {
                            stream.CopyTo(fs);
                        }
                    }
                }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual Task Play(string fileName, string channelId, bool repeat  = false)
        {
            return Task.Run(() =>
            {
                logService.Info("Playing "+fileName+" at " + channelId);
                if(player != null && !player.HasExited)
                {
                    player.StandardInput.WriteLine("-i " + fileName + " -t " + token + " -c " + channelId);
                }
                else
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
                                WorkingDirectory = Environment.CurrentDirectory,
                                RedirectStandardInput = true,
                                UseShellExecute = false
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
                                WorkingDirectory = Environment.CurrentDirectory,
                                RedirectStandardInput = true,
                                UseShellExecute = false
                            }
                        };
                    }
                    player.Start();
                }

            });
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual Task Stop()
        {
            player.Kill();
            return Task.CompletedTask;
        }
    }
}
