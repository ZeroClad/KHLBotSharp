# KHLBotSharp
开黑啦机器人运行器，支持多机器人以及插件模式运行

## 下载
- [可以到这里浏览所有可下载的zip包](https://github.com/PoH98/KHLBotSharp/releases/latest)
- [Nuget 链接](https://www.nuget.org/packages/KHLBotSharp.Core)

## Websocket使用
如果你的机器人单纯是小型使用，可考虑使用Websocket
- 启动过一次启动器后，在`Profiles\<你的Profile名字>`内会拥有一个config.json
- 还请自行到[开黑啦开发者网页](https://developer.kaiheila.cn/bot/index)注册机器人，选择使用Websocket
- 回到config.json, 把`BotToken`粘贴上
- 保存config.json后，打开启动器即可
- 可开始测试机器人是否运行正常
- 如果需要多个机器人运行，则可使用cmd cd到启动器文件夹内，输入下列指令`KHLBotSharp -c 你的Profile名字`
- 如果需要只单独运行一个特定的Profile，则使用cmd cd到启动器文件夹内，输入下列指令`KHLBotSharp -r 你的Profile名字`
- 暂时不支持Docker以及非Windows，不过如果你想要使用，都可以自行下载源代码Publish

## WebHook使用
如果你的机器人可能会大量用户使用，可考虑使用Webhook
- 启动过一次启动器后，在`Profiles\<你的Profile名字>`内会拥有一个config.json
- 还请自行到[开黑啦开发者网页](https://developer.kaiheila.cn/bot/index)注册机器人，选择使用Webhook
- 回到config.json，把`EncryptKey`和`VerifyToken`粘贴上
- 保存config.json后，打开启动器即可
- 回到[开黑啦开发者网页](https://developer.kaiheila.cn/bot/index)，在你的机器人`Callback Url`下粘贴`http://你的域名/hook?botName=<你的Profile名字>`, 并且上线机器人
- 可开始测试机器人是否运行正常
- 暂时不支持Docker以及非Windows，不过如果你想要使用，都可以自行下载源代码Publish

> 欢迎各路大神fork以及修改代码

## 即将出现的更新:
- 持续优化以及跟进开黑啦文档

## 文档
> 创建插件之前，需要先知道插件监听的事件，框架会自动根据你监听的事件进行自动分类和整合数据。下列是插件可加载的列表:-

|消息事件|解释|
|----|----|
|`IGroupCardMessageHandler`|频道卡片消息事件|
|`IGroupKMarkdownMessageHandler`|频道KMarkdown消息事件|
|`IGroupPictureMessageHandler`|频道图片消息事件|
|`IGroupTextMessageHandler`|频道文字消息事件|
|`IGroupVideoMessageHandler`|频道影片消息事件|
|`IPrivateCardMessageHandler`|私聊卡片消息|
|`IPrivateKMarkdownMessageHandler`|私聊KMarkdown消息事件|
|`IPrivatePictureMessageHandler`|私聊图片消息事件|
|`IPrivateTextMessageHandler`|私聊文字消息事件|
|`IPrivateVideoMessageHandler`|私聊影片消息|

|系统事件|解释|
|----|----|
|`IBotExitServerHandler`|机器人退出服务器事件|
|`IBotJoinServerHandler`|机器人加入服务器事件|
|`ICardMessageButtonClickHandler`|卡片消息按钮点击事件|
|`IChannelCreatedHandler`|频道创建事件|
|`IChannelMessageRemoveHandler`|频道消息撤回事件|
|`IChannelMessageUpdateHandler`|频道消息修改事件|
|`IChannelModifyHandler`|频道修改事件|
|`IChannelPinnedMessageHandler`|频道置顶消息事件|
|`IChannelRemoveHandler`|频道删除事件|
|`IChannelRemovePinMessageHandler`|频道置顶消息移除事件|
|`IChannelUserAddReactionHandler`|频道用户添加表情到消息事件|
|`IChannelUserRemoveReactionHandler`|频道用户从消息删除表情事件|
|`IPrivateMessageAddReactionHandler`|私聊用户添加表情到消息事件|
|`IPrivateMessageModifyHandler`|私聊消息修改事件|
|`IPrivateMessageRemoveHandler`|私聊消息撤回事件|
|`IPrivateMessageRemoveReactionHandler`|私聊用户从消息删除表情事件|
|`IServerBlacklistUserHandler`|服务器添加黑名单事件|
|`IServerMemberModifiedHandler`|服务器成员修改昵称事件|
|`IServerMemberOfflineHandler`|服务器成员下线事件|
|`IServerMemberOnlineHandler`|服务器成员上线事件|
|`IServerNewMemberJoinHandler`|服务器新成员加入事件|
|`IServerRemoveBlacklistUserHandler`|服务器移除黑名单事件|
|`IServerRemoveHandler`|服务器删除事件|
|`IServerRoleAddHandler`|服务器添加角色事件|
|`IServerRoleModifyHandler`|服务器角色修改事件|
|`IServerRoleRemoveHandler`|服务器角色移除事件|
|`IServerUpdateHandler`|服务器修改设置事件|
|`IUserExitVoiceChannelHandler`|用户离开语音频道事件|
|`IUserInfoChangeHandler`|用户个人资料修改事件|
|`IUserJoinVoiceChannelHandler`|用户加入语音频道事件|

---

> 在创建了`class`后，以下是一些你可以在`Ctor`内从`IServiceProvider`获得到的`interface`

|IService|解释|
|----------|----|
|`ILogService`|日志Service|
|`IKHLHttpService`|开黑啦的Http请求指令，例如可发群聊消息，私聊消息等等，属于重要的Service，务必GetService时获取|
|`IBotConfigSettings`|获取在当前Profile内的config.ini设置|

---

### 后台线程
打从`v1.0.0.8`开始，如果你需要有主动发送通知的功能，可以尝试使用`IBackgroundService` interface
所有`IBackgroundService`会在第一次触发指令后开始执行`StartAsync`，作为开始加载数据或者开启新的Task或者Timer在后台运行
`IBackgroundService`使用的Dependency Injector，因此可以享有所有DI功能

---

> 接下来就是自主编写插件，而插件将会使用到的Http请求则可[在这里查看interface代码](https://github.com/PoH98/KHLBotSharp/blob/master/KHLBotSharp.Core/IService/IKHLHttpService.cs)
- 插件需要添加KHLBotSharp.Core作为Dependency，打包后此dll不需要复制
- 当前将会使用`IGroupTextMessageHandler`作为例子
- 点击这里查看[一般插件的例子文件(v1 SDK都支持)](https://github.com/PoH98/KHLBotSharp/blob/master/TestPlugin/PluginSample.cs)，这个插件是复读机
- 点击这里查看[新版本插件的例子文件(v1.0.0.6 SDK 开始支持)](https://github.com/PoH98/KHLBotSharp/blob/master/TestPlugin/NewPluginSample.cs)，这个插件是复读机
- `Ctor`为插件的初始化，在这里你会获得IServiceProvider，源自于Dependency Injection(DI)，因此可以进行`GetService`获取你想使用的东西，但是还请注意，这里并不能让你注册DI，只能获取
- `Handle`则是插件的真正运行位置，当收到特定的Event后将会传输到这，再进行处理即可
- 你可以拥有多个处理相同事件的插件
- 插件完成后，在启动器文件夹内的`Profiles\<你的Profile名字>\Plugins`内创建一个与你插件名字一样的文件夹，并且把插件丢到文件夹内，包含你所有其他的Dependency，无需复制KHLBotSharp.Core
- 打开启动器即可

---
> 如果想要注册自己想在Ctor的时候用的一些Service，可以尝试使用`IServiceRegister` Interface，只需要创建新的class (尽量别跟原本的消息处理事件放一起) 加上interface后，就可以获取个`void Register(IServiceCollection services)`功能进行注册DI

## 启动器选择
- 目前启动器支持俩选择: .NET 5，.NET 6以及.NET Core 3.1, 而插件因KHLBotSharp为.NET Standard 2.0因此可支持.NET Framework 4.6.1 以及.NET Core 2.0 以上甚至是最新的.NET 5和6
- Websocket启动器也可以自主添加或者修改，只需要复制[这个Repos里的所有文件](https://github.com/PoH98/KHLBotSharp/tree/master/KHLBotSharp.NETCore3)并且打包为你想要的.NET版本即可，相同插件可支持
- Webhook启动器也可自主添加或者修改，但是会比较麻烦，需要复制[这个Repos里的所有文件](https://github.com/PoH98/KHLBotSharp/tree/master/KHLBotSharp.WebHook.NetCore3)并且打包为你想要的.NET版本即可，相同插件可支持
