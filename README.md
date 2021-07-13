# KHLBotSharp
开黑啦机器人运行器，支持多机器人以及插件模式运行

## 插件开发
- 插件需要添加KHLBotSharp.Core作为Dependency，打包后此dll不需要复制
- 当前只测试过了IGroupTextMessageHandler，因此将会使用这个作为例子
- 点击这里查看[例子文件](https://github.com/PoH98/KHLBotSharp/blob/master/TestPlugin/Class1.cs)
- Ctor为插件的初始化，在这里你会获得IServiceProvider，源自于Dependency Injection(DI)，因此可以进行GetService获取你想使用的东西，但是还请注意，这里并不能让你注册DI，只能获取
- Handle则是插件的真正运行位置，当收到特定的Event后将会传输到这，再进行处理即可
- 你可以拥有多个处理相同事件的插件
- 插件完成后，在启动器文件夹内的Profiles\<你的Profile名字>\Plugins内创建一个与你插件名字一样的文件夹，并且把插件丢到文件夹内，包含你所有其他的Dependency，无需复制KHLBotSharp.Core
- 打开软件即可

## 启动器选择
- 目前启动器支持俩选择: .NET 6以及.NET Core 3.1, 而插件因KHLBotSharp为.NET Standard 2.0因此可支持.NET Framework 4.6.1 以及.NET Core 2.0 以上甚至是最新的.NET 5和6
- 启动器也可以自主添加或者修改，只需要复制[这个Repos里的所有文件](https://github.com/PoH98/KHLBotSharp/tree/master/KHLBotSharp.NETCore3)并且打包为你想要的.NET版本即可，相同插件可支持的范围内

### 下载
- [.NET Core 3.1 启动器 + KHLBotSharp.Core](https://github.com/PoH98/KHLBotSharp/releases/download/v0.1/netcore3.1.zip)
- [.NET 6 启动器 + KHLBotSharp.Core](https://github.com/PoH98/KHLBotSharp/releases/download/v0.1/net6.zip)