using Newtonsoft.Json;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;

namespace KHLBotSharp.Core.BotHost
{
    /// <summary>
    /// 欢迎词汇输出用，内置自动取消Console的快速选择避免出现Bot被选择相关文字后停止运行以及错误重启，不推荐Webhook使用
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class Welcome
    {
        private const uint ENABLE_QUICK_EDIT = 0x0040;

        // STD_INPUT_HANDLE (DWORD): -10 is the standard input device.
        private const int STD_INPUT_HANDLE = -10;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        protected class Author
        {
            [JsonProperty("login")]
            public string Login { get; set; }

            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("node_id")]
            public string NodeId { get; set; }

            [JsonProperty("avatar_url")]
            public string AvatarUrl { get; set; }

            [JsonProperty("gravatar_id")]
            public string GravatarId { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("html_url")]
            public string HtmlUrl { get; set; }

            [JsonProperty("followers_url")]
            public string FollowersUrl { get; set; }

            [JsonProperty("following_url")]
            public string FollowingUrl { get; set; }

            [JsonProperty("gists_url")]
            public string GistsUrl { get; set; }

            [JsonProperty("starred_url")]
            public string StarredUrl { get; set; }

            [JsonProperty("subscriptions_url")]
            public string SubscriptionsUrl { get; set; }

            [JsonProperty("organizations_url")]
            public string OrganizationsUrl { get; set; }

            [JsonProperty("repos_url")]
            public string ReposUrl { get; set; }

            [JsonProperty("events_url")]
            public string EventsUrl { get; set; }

            [JsonProperty("received_events_url")]
            public string ReceivedEventsUrl { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("site_admin")]
            public bool SiteAdmin { get; set; }
        }

        protected class Uploader
        {
            [JsonProperty("login")]
            public string Login { get; set; }

            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("node_id")]
            public string NodeId { get; set; }

            [JsonProperty("avatar_url")]
            public string AvatarUrl { get; set; }

            [JsonProperty("gravatar_id")]
            public string GravatarId { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("html_url")]
            public string HtmlUrl { get; set; }

            [JsonProperty("followers_url")]
            public string FollowersUrl { get; set; }

            [JsonProperty("following_url")]
            public string FollowingUrl { get; set; }

            [JsonProperty("gists_url")]
            public string GistsUrl { get; set; }

            [JsonProperty("starred_url")]
            public string StarredUrl { get; set; }

            [JsonProperty("subscriptions_url")]
            public string SubscriptionsUrl { get; set; }

            [JsonProperty("organizations_url")]
            public string OrganizationsUrl { get; set; }

            [JsonProperty("repos_url")]
            public string ReposUrl { get; set; }

            [JsonProperty("events_url")]
            public string EventsUrl { get; set; }

            [JsonProperty("received_events_url")]
            public string ReceivedEventsUrl { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("site_admin")]
            public bool SiteAdmin { get; set; }
        }

        protected class Asset
        {
            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("node_id")]
            public string NodeId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("label")]
            public object Label { get; set; }

            [JsonProperty("uploader")]
            public Uploader Uploader { get; set; }

            [JsonProperty("content_type")]
            public string ContentType { get; set; }

            [JsonProperty("state")]
            public string State { get; set; }

            [JsonProperty("size")]
            public int Size { get; set; }

            [JsonProperty("download_count")]
            public int DownloadCount { get; set; }

            [JsonProperty("created_at")]
            public DateTime CreatedAt { get; set; }

            [JsonProperty("updated_at")]
            public DateTime UpdatedAt { get; set; }

            [JsonProperty("browser_download_url")]
            public string BrowserDownloadUrl { get; set; }
        }

        protected class GithubVersion
        {
            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("assets_url")]
            public string AssetsUrl { get; set; }

            [JsonProperty("upload_url")]
            public string UploadUrl { get; set; }

            [JsonProperty("html_url")]
            public string HtmlUrl { get; set; }

            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("author")]
            public Author Author { get; set; }

            [JsonProperty("node_id")]
            public string NodeId { get; set; }

            [JsonProperty("tag_name")]
            public string TagName { get; set; }

            [JsonProperty("target_commitish")]
            public string TargetCommitish { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("draft")]
            public bool Draft { get; set; }

            [JsonProperty("prerelease")]
            public bool Prerelease { get; set; }

            [JsonProperty("created_at")]
            public DateTime CreatedAt { get; set; }

            [JsonProperty("published_at")]
            public DateTime PublishedAt { get; set; }

            [JsonProperty("assets")]
            public List<Asset> Assets { get; set; }

            [JsonProperty("tarball_url")]
            public string TarballUrl { get; set; }

            [JsonProperty("zipball_url")]
            public string ZipballUrl { get; set; }

            [JsonProperty("body")]
            public string Body { get; set; }
        }
        /// <summary>
        /// 输出欢迎以及绑定错误重启以及取消Console快速选择
        /// </summary>
        public static async void Print()
        {
            IntPtr consoleHandle = GetStdHandle(STD_INPUT_HANDLE);

            // get current console mode
            if (!GetConsoleMode(consoleHandle, out uint consoleMode))
            {
                // ERROR: Unable to get console mode.
                // However who cares?
            }

            // Clear the quick edit bit in the mode flags
            consoleMode &= ~ENABLE_QUICK_EDIT;

            // set the new mode
            if (!SetConsoleMode(consoleHandle, consoleMode))
            {
                // ERROR: Unable to set console mode
                // However who cares?
            }
            var currentVer = "v" + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
            try
            {
                HttpClient hc = new HttpClient();
                hc.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("KHLBotSharp.Core", currentVer));
                hc.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("(KHLBotUpdateCheck)"));
                var version = JsonConvert.DeserializeObject<GithubVersion>(await hc.GetStringAsync("https://api.github.com/repos/PoH98/KHLBotSharp/releases/latest"));
                var compareResult = String.Compare(currentVer, version.TagName);
                if (compareResult < 0)
                {
                    AnsiConsole.Write(new Rule("[red]Found Github latest version is " + version.TagName + "[/]"));
                    AnsiConsole.WriteLine();
                    AnsiConsole.Write(new Markup("[underline lime1]Download latest version on " + version.HtmlUrl + "[/]"));
                    AnsiConsole.WriteLine();
                    AnsiConsole.WriteLine();
                    AnsiConsole.WriteLine();
                }
                else if (compareResult == 0)
                {
                    AnsiConsole.Write(new Rule("[lime]You are using latest version of KHLBot.Core[/]"));
                    AnsiConsole.WriteLine();
                }
                else
                {
                    AnsiConsole.Write(new Rule("[red]Found Github latest version is " + version.TagName + "[/]"));
                    AnsiConsole.WriteLine();
                    AnsiConsole.Write(new Markup("[underline cyan1]Wow! You are using future version of KHLBot![/]"));
                    AnsiConsole.WriteLine();
                    AnsiConsole.WriteLine();
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.Write(new Rule("[red]Unable to fetch latest version, " + ex.Message + "[/]"));
                AnsiConsole.WriteLine();
            }
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            AnsiConsole.Write(new FigletText("KHLBot " + currentVer).Centered().Color(Color.Aqua));
            AnsiConsole.WriteLine();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            AnsiConsole.MarkupLine("[grey42][[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "]]: [/][red][[Err]]: [/][underline green1][[Global]][/]: [underline cyan1][[BotService]][/]: [white]" + e.ToString().Replace("[", "[[").Replace("]", "]]") + "[/]");
            File.WriteAllText("error.log", e.ExceptionObject.ToString());
            Process.Start(Process.GetCurrentProcess().MainModule.FileName);
            Environment.Exit(0);
            //Exit and restart
        }
    }
}
