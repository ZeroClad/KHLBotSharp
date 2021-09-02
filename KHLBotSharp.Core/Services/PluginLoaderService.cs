using KHLBotSharp.IService;
using KHLBotSharp.Models.EventsMessage;
using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using KHLBotSharp.Models.MessageHttps.RequestMessage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace KHLBotSharp.Services
{
    public class PluginLoaderService : IPluginLoaderService
    {
        private IEnumerable<IKHLPlugin> plugins;
        private ILogService logService;
        private IKHLHttpService httpService;
        private bool _initialized;
        private IServiceProvider provider;
        private IErrorRateService errorRate;
        public void LoadPlugin(string bot, IServiceCollection services)
        {
            /*加载所有文件夹内的插件文件夹*/
            foreach (var plugin in Directory.GetDirectories(Path.Combine(bot, "Plugins")))
            {
                var pluginDll = plugin.Substring(plugin.LastIndexOf("\\") + 1) + ".dll";
                var pluginFullPath = Path.Combine(plugin, pluginDll);
                var pluginBytes = File.ReadAllBytes(pluginFullPath);
                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Changed += Watcher_Changed;
                watcher.Path = plugin;
                watcher.Filter = "*.dll";
                watcher.EnableRaisingEvents = true;
                AppDomain.CurrentDomain.AssemblyResolve += (sender, e) =>
                {
                    try
                    {
                        var assemblyDetails = e.Name.Split(',').Where(x => !string.IsNullOrEmpty(x));
                        var file = assemblyDetails.First();
                        var dependencyDll = Path.Combine(Environment.CurrentDirectory, plugin, file + ".dll");
                        if (!File.Exists(dependencyDll))
                        {
                            return null;
                        }
                        var result = Assembly.LoadFrom(dependencyDll);
                        return result;
                    }
                    catch (Exception ex)
                    {
                        File.WriteAllText("error.log", ex.ToString());
                        Environment.Exit(1);
                        return null;
                    }

                };
                Assembly pluginAssembly = AppDomain.CurrentDomain.Load(pluginBytes);
                var Iplugins = pluginAssembly.GetTypes().Where(x => typeof(IKHLPlugin).IsAssignableFrom(x) && !x.IsAbstract);
                foreach (var type in Iplugins)
                {
                    var implementedInterfaces = type.GetInterfaces();
                    if (implementedInterfaces.Any())
                    {
                        foreach (var interfaceType in implementedInterfaces)
                        {
                            services.AddScoped(interfaceType, type);
                        }
                    }
                    else
                    {
                        // No implemented interface, register as self
                        services.AddScoped(type);
                    }
                }
                var diregister = pluginAssembly.GetTypes().Where(x => typeof(IServiceRegister).IsAssignableFrom(x) && !x.IsAbstract);
                foreach(var type in diregister)
                {
                    (type as IServiceRegister).Register(services);
                }
            }
        }

        private static void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Process.Start(Process.GetCurrentProcess().MainModule.FileName);
            Environment.Exit(0);
        }

        public virtual IEnumerable<IKHLPlugin> ResolvePlugin()
        {
            this.logService = provider.GetService<ILogService>();
            this.httpService = provider.GetService<IKHLHttpService>();
            if (plugins == null)
            {
                plugins = provider.GetServices<IKHLPlugin>();
            }
            if (!_initialized)
            {
                foreach (var plug in plugins)
                {
                    plug.Ctor(provider);
                }
                _initialized = true;
            }
            return plugins;
        }

        public virtual IEnumerable<T> ResolvePlugin<T>() where T : IKHLPlugin
        {
            var pluginList = ResolvePlugin();
            return pluginList.Where(x => x is T).Select(y => (T)y);
        }

        public virtual async void HandleMessage<T, T2>(EventMessage<T> input, IEnumerable<T2> plugins)
            where T : AbstractExtra
            where T2 : IKHLPlugin<T>
        {
            foreach (var plugin in plugins)
            {
                try
                {
                    if (await plugin.Handle(input))
                    {
                        break;
                    }
                    else
                    {
                        //Slow things down abit
                        await Task.Delay(50);
                    }
                }
                catch (Exception ex)
                {
                    errorRate.AddError();
                    if (input.MessageType.ToString() == "0")
                    {
                        try
                        {
                            await httpService.SendGroupMessage(new SendMessage() { Content = "错误报告: " + ex.Message, TargetId = input.Data.TargetId });
                        }
                        catch
                        {
                            //Ignore send as we can't even send the message out
                        }
                    }
                    logService.Error(plugin.GetType().Name + ":-" + ex.ToString());
                }

            }
        }

        public virtual void HandleMessage<T>(EventMessage<T> input)
            where T : AbstractExtra
        {
            HandleMessage(input, ResolvePlugin<IKHLPlugin<T>>());
        }

        public void Init(IServiceProvider provider)
        {
            this.provider = provider;
            errorRate = provider.GetService<IErrorRateService>();
        }
    }
}
