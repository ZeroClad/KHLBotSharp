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

namespace KHLBotSharp.Services
{
    public class PluginLoaderService : IPluginLoaderService
    {
        private IServiceProvider provider;
        private IErrorRateService errorRate;
        private bool _Inited = false;
        public bool Inited => _Inited;
        private string currentPlugin;
        public void LoadPlugin(string bot, IServiceCollection services)
        {
            if (!Directory.Exists(Path.Combine(bot, "Plugins")))
            {
                Directory.CreateDirectory(Path.Combine(bot, "Plugins"));
            }
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Changed += Watcher_Changed;
            watcher.Path = Path.Combine(bot, "Plugins");
            watcher.Filter = "*.dll";
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            watcher.EnableRaisingEvents = true;
            watcher.IncludeSubdirectories = true;
            /*加载所有文件夹内的插件文件夹*/
            foreach (var plugin in Directory.GetDirectories(Path.Combine(bot, "Plugins")))
            {
                currentPlugin = plugin;
                var pluginDll = plugin.Substring(plugin.LastIndexOf("\\") + 1) + ".dll";
                var pluginFullPath = Path.Combine(plugin, pluginDll);
                var pluginBytes = File.ReadAllBytes(pluginFullPath);
                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
                Assembly pluginAssembly = AppDomain.CurrentDomain.Load(pluginBytes);
                var diregister = pluginAssembly.GetTypes().Where(x => typeof(IServiceRegister).IsAssignableFrom(x) && !x.IsAbstract);
                foreach (var type in diregister)
                {
                    var instance = Activator.CreateInstance(type, null);
                    if (instance is IServiceRegister)
                    {
                        (instance as IServiceRegister).Register(services);
                    }
                    else
                    {
                        File.WriteAllText("error.log", type.FullName + " DI failed");
                    }
                }
                var Iplugins = pluginAssembly.GetTypes().Where(x => typeof(IKHLPlugin).IsAssignableFrom(x) && !x.IsAbstract);
                foreach (var type in Iplugins)
                {
                    var implementedInterfaces = type.GetInterfaces();
                    if (implementedInterfaces.Any())
                    {
                        RegisterType? regType = null;
                        if (implementedInterfaces.Any(x => x is IPluginType))
                        {
                            var instance = Activator.CreateInstance(type, null);
                            regType = (instance as IPluginType).RegisterType;
                        }
                        foreach (var interfaceType in implementedInterfaces)
                        {
                            if (regType.HasValue)
                            {
                                if (regType == RegisterType.Singleton)
                                {
                                    services.AddSingleton(interfaceType, type);
                                }
                                else
                                {
                                    services.AddTransient(interfaceType, type);
                                }
                            }
                            else
                            {
                                services.AddScoped(interfaceType, type);
                            }
                        }
                    }
                    else
                    {
                        // No implemented interface, register as self
                        services.AddScoped(type);
                    }
                }
            }
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                var assemblyDetails = args.Name.Split(',').Where(x => !string.IsNullOrEmpty(x));
                var file = assemblyDetails.First();
                var dependencyDll = Path.Combine(Environment.CurrentDirectory, currentPlugin, file + ".dll");
                if (!File.Exists(dependencyDll))
                {
                    return null;
                }
                var result = Assembly.LoadFrom(dependencyDll);
                return result;
            }
            catch (Exception ex)
            {
                File.WriteAllText("error.log", currentPlugin + ex.ToString());
                Environment.Exit(1);
                return null;
            }
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Process.Start(Process.GetCurrentProcess().MainModule.FileName);
            Environment.Exit(0);
        }

        public virtual IEnumerable<IKHLPlugin> ResolvePlugin()
        {
            return provider.GetServices<IKHLPlugin>();
        }

        public virtual IEnumerable<T> ResolvePlugin<T>() where T : IKHLPlugin
        {
            return provider.GetServices<T>();
        }

        public virtual async void HandleMessage<T, T2>(EventMessage<T> input, IEnumerable<T2> plugins)
            where T : Extra
            where T2 : IKHLPlugin<T>
        {
            Stopwatch speedTest = Stopwatch.StartNew();
            var logService = provider.GetService<ILogService>();
            foreach (var plugin in plugins)
            {
                try
                {
                    Stopwatch pluginExecuteTime = Stopwatch.StartNew();
                    await plugin.Ctor(provider);
                    var completed = await plugin.Handle(input);
                    pluginExecuteTime.Stop();
                    if (pluginExecuteTime.ElapsedMilliseconds >= 1500)
                    {
                        var httpService = provider.GetService<IKHLHttpService>();
                        //The plugin is too low performance!
                        logService.Warning(plugin.GetType().FullName + " is too slow! Used " + pluginExecuteTime.ElapsedMilliseconds + " ms to process a single fucking message?");
                        await httpService.SendGroupMessage(new SendMessage() { Content = "警告: \n" + plugin.GetType().FullName + "运行速度太过缓慢，已使用" + pluginExecuteTime.ElapsedMilliseconds + "ms处理一条消息，请确保联络插件开发者禁止运行大量操作，并且善用IServiceRegister注册需要长时间加载大量数据的Service为Singleton!", TargetId = input.Data.TargetId });
                    }
                    if (plugin is IDisposable)
                    {
                        if (plugin is IPluginType)
                        {
                            if ((plugin as IPluginType).RegisterType != RegisterType.Singleton)
                            {
                                (plugin as IDisposable).Dispose();
                            }
                        }
                        else
                        {
                            (plugin as IDisposable).Dispose();
                        }
                    }
                    if (completed)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    var httpService = provider.GetService<IKHLHttpService>();
                    errorRate.AddError();
                    if (input.MessageType.ToString() == "0")
                    {
                        try
                        {
                            await httpService.SendGroupMessage(new SendMessage() { Content = "错误报告: \n" + plugin.GetType().FullName + "触发了" + ex.Message, TargetId = input.Data.TargetId });
                        }
                        catch
                        {
                            //Ignore send as we can't even send the message out
                        }
                    }
                    logService.Error(plugin.GetType().FullName + ":-" + ex.ToString());
                }
            }
            speedTest.Stop();
            logService.Debug("Plugin process success in " + speedTest.ElapsedMilliseconds + " ms");
        }

        public virtual void HandleMessage<T>(EventMessage<T> input)
            where T : Extra
        {
            HandleMessage(input, ResolvePlugin<IKHLPlugin<T>>());
        }

        public void Init(IServiceProvider provider)
        {
            this.provider = provider;
            if (errorRate == null)
            {
                errorRate = provider.GetService<IErrorRateService>();
            }
            _Inited = true;
        }
    }
}
