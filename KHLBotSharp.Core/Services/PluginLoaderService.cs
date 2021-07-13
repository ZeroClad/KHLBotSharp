using KHLBotSharp.IService;
using KHLBotSharp.Models.EventsMessage;
using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace KHLBotSharp.Services
{
    public class PluginLoaderService: IPluginLoaderService
    {
        private IEnumerable<IKHLPlugin> plugins;
        private bool _initialized;
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
                        var assemblyDetails = e.Name.Split(", ", StringSplitOptions.RemoveEmptyEntries);
                        var file = assemblyDetails.First();
                        var dependencyDll = Path.Combine(Environment.CurrentDirectory, plugin, file + ".dll");
                        var result = Assembly.LoadFrom(dependencyDll);
                        return result;
                    }
                    catch(Exception ex)
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
                            services.AddSingleton(interfaceType, type);
                        }
                    }
                    else
                    {
                        // No implemented interface, register as self
                        services.AddSingleton(type);
                    }
                }
            }
        }

        private static void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Process.Start(Process.GetCurrentProcess().MainModule.FileName);
            Environment.Exit(0);
        }

        public virtual IEnumerable<IKHLPlugin> ResolvePlugin(IServiceProvider provider)
        {
            if(plugins == null)
            {
                plugins = provider.GetServices<IKHLPlugin>();
            }
            if (!_initialized)
            {
                foreach(var plug in plugins)
                {
                    plug.Ctor(provider);
                }
                _initialized = true;
            }
            return plugins;
        }

        public virtual IEnumerable<T> ResolvePlugin<T>(IServiceProvider provider) where T : IKHLPlugin
        {
            var pluginList = ResolvePlugin(provider);
            return pluginList.Where(x => x is T).Select(y => (T)y);
        }

        public virtual async void HandleMessage<T, T2>(EventMessage<T> input, IEnumerable<T2> plugins)
            where T : AbstractExtra
            where T2 : IKHLPlugin<T>
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach (var plugin in plugins)
            {
                if (await plugin.Handle(input))
                {
                    break;
                }
            }
            Console.WriteLine(stopwatch.ElapsedMilliseconds + " ms");
        }

        public virtual void HandleMessage<T>(EventMessage<T> input, IServiceProvider provider)
            where T : AbstractExtra
        {
            HandleMessage(input, ResolvePlugin<IKHLPlugin<T>>(provider));
        }
    }
}
