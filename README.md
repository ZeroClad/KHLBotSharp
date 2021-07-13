# KHLBotSharp
开黑啦机器人运行器，支持多机器人以及插件模式运行

## 插件开发
- 当前只测试过了IGroupTextMessageHandler，因此将会使用这个作为例子
- 点击这里查看[例子文件](https://github.com/PoH98/KHLBotSharp/blob/master/TestPlugin/Class1.cs)
- Ctor为插件的初始化，在这里你会获得IServiceProvider，源自于Dependency Injection(DI)，因此可以进行GetService获取你想使用的东西，但是还请注意，这里并不能让你注册DI，只能获取
- Handle则是插件的真正运行位置，当收到特定的Event后将会传输到这，再进行处理即可
- 你可以拥有多个处理相同事件的插件
- 插件完成后，在启动器文件夹内的Profiles\<你的Profile名字>\Plugins内创建一个与你插件名字一样的文件夹，并且把插件丢到文件夹内，包含你所有其他的Dependency
- 打开软件即可
