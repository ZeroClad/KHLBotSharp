using KHLBotSharp.Models.MessageHttps.RequestMessage;
using KHLBotSharp.Models.MessageHttps.ResponseMessage;
using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data;
using KHLBotSharp.Models.Objects;
using System.IO;
using System.Threading.Tasks;

namespace KHLBotSharp.IService
{
    /// <summary>
    /// 大部分开黑的HTTP接口，可以直接在这里执行大部分操作
    /// </summary>
    public interface IKHLHttpService
    {
        /// <summary>
        /// 获取机器人当前加入的服务器列表
        /// </summary>
        /// <returns></returns>
        Task<KHLResponseMessage<GetServerList>> GetServers();
        /// <summary>
        /// 获取服务器资料
        /// </summary>
        /// <param name="GuildId"></param>
        /// <returns></returns>
        Task<KHLResponseMessage<Guild>> GetGuild(string GuildId);
        /// <summary>
        /// 获取服务器用户
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        Task<KHLResponseMessage<GetGuildMemberList>> GetGuildUser(GetServerMember Request);
        /// <summary>
        /// 修改昵称
        /// </summary>
        /// <param name="GuildId"></param>
        /// <param name="UserId"></param>
        /// <param name="NickName"></param>
        /// <returns></returns>
        Task ChangeNick(string GuildId, string UserId, string NickName = null);
        /// <summary>
        /// 退出服务器
        /// </summary>
        /// <param name="GuildId"></param>
        /// <returns></returns>
        Task LeaveGuild(string GuildId);
        /// <summary>
        /// 把用户踢出服务器
        /// </summary>
        /// <param name="GuildId"></param>
        /// <param name="TargetId"></param>
        /// <returns></returns>
        Task Kick(string GuildId, string TargetId);
        /// <summary>
        /// 获取服务器禁麦数据
        /// </summary>
        /// <param name="GuildId"></param>
        /// <returns></returns>
        Task<KHLResponseMessage<GetGuildMuteListDetail>> GetMute(string GuildId);
        /// <summary>
        /// 禁麦
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        Task SetMute(SetServerMute Request);
        /// <summary>
        /// 取消禁麦
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        Task UnMute(SetServerMute Request);
        /// <summary>
        /// 获得服务器频道列表
        /// </summary>
        /// <param name="GuildId"></param>
        /// <returns></returns>
        Task<KHLResponseMessage<GetChannelList>> GetChannels(string GuildId);
        /// <summary>
        /// 获取频道资料
        /// </summary>
        /// <param name="ChannelId"></param>
        /// <returns></returns>
        Task<KHLResponseMessage<Channel>> GetChannelDetail(string ChannelId);
        /// <summary>
        /// 创建频道
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        Task<KHLResponseMessage<Channel>> CreateChannel(CreateChannel Request);
        /// <summary>
        /// 删除频道
        /// </summary>
        /// <param name="ChannelId"></param>
        /// <returns></returns>
        Task DeleteChannel(string ChannelId);
        /// <summary>
        /// 把用户移动到语音频道
        /// </summary>
        /// <param name="ChannelId"></param>
        /// <param name="UserIds"></param>
        /// <returns></returns>
        Task MoveUserVoiceChannel(string ChannelId, params string[] UserIds);
        /// <summary>
        /// 获取频道角色权限列表
        /// </summary>
        /// <param name="ChannelId"></param>
        /// <returns></returns>
        Task<KHLResponseMessage<GetChannelRoleList>> GetChannelRoles(string ChannelId);
        /// <summary>
        /// 添加频道角色权限
        /// </summary>
        /// <param name="ChannelId"></param>
        /// <param name="IsUser"></param>
        /// <param name="UserIdOrRoleId"></param>
        /// <returns></returns>
        Task AddChannelRoles(string ChannelId, bool IsUser, string UserIdOrRoleId);
        /// <summary>
        /// 发送群消息
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        Task<string> SendGroupMessage(SendMessage Request);
        /// <summary>
        /// 修改群消息
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        Task UpdateGroupMessage(UpdateMessage Request);
        /// <summary>
        /// 撤回群消息
        /// </summary>
        /// <param name="MsgId"></param>
        /// <returns></returns>
        Task RemoveGroupMessage(string MsgId);
        /// <summary>
        /// 给群消息添加表情
        /// </summary>
        /// <param name="MsgId"></param>
        /// <param name="Emoji"></param>
        /// <returns></returns>
        Task GroupMessageAddReaction(string MsgId, string Emoji);
        /// <summary>
        /// 给群消息移除表情
        /// </summary>
        /// <param name="MsgId"></param>
        /// <param name="Emoji"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task GroupMessageRemoveReaction(string MsgId, string Emoji, string UserId = null);
        /// <summary>
        /// 获取私聊列表
        /// </summary>
        /// <returns></returns>
        Task<KHLResponseMessage<PrivateMessageList>> GetPrivateMessageList();
        /// <summary>
        /// 获取私聊详情
        /// </summary>
        /// <param name="ChatCode"></param>
        /// <returns></returns>
        Task<KHLResponseMessage<PrivateMessageDetail>> GetPrivateChatDetail(string ChatCode);
        /// <summary>
        /// 发送私聊消息
        /// </summary>
        /// <param name="TargetId"></param>
        /// <returns></returns>
        Task<KHLResponseMessage<PrivateMessageDetail>> CreatePrivateMessage(string TargetId);
        /// <summary>
        /// 撤回私聊消息
        /// </summary>
        /// <param name="ChatCode"></param>
        /// <returns></returns>
        Task RemovePrivateMessage(string ChatCode);
        /// <summary>
        /// 私聊消息添加表情
        /// </summary>
        /// <param name="MsgId"></param>
        /// <param name="Emoji"></param>
        /// <returns></returns>
        Task PrivateMessageAddReaction(string MsgId, string Emoji);
        /// <summary>
        /// 私聊消息移除表情
        /// </summary>
        /// <param name="MsgId"></param>
        /// <param name="Emoji"></param>
        /// <returns></returns>
        Task PrivateMessageRemoveReaction(string MsgId, string Emoji);
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Task<string> UploadFile(string filePath);
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        Task<string> UploadFile(Stream stream);
        /// <summary>
        /// 获取服务器角色权限列表
        /// </summary>
        /// <param name="GuildId"></param>
        /// <returns></returns>
        Task<KHLResponseMessage<GetServerRoleList>> GetServerRoleList(string GuildId);
        /// <summary>
        /// 创建服务器角色权限
        /// </summary>
        /// <param name="GuildId"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        Task<KHLResponseMessage<ServerRole>> CreateServerRole(string GuildId, string Name = null);
        /// <summary>
        /// 更新服务器角色权限
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        Task<KHLResponseMessage<ServerRole>> UpdateServerRole(UpdateServerRole Request);
        /// <summary>
        /// 删除服务器权限
        /// </summary>
        /// <param name="GuildId"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        Task DeleteServerRole(string GuildId, uint RoleId);
        /// <summary>
        /// 给予服务器角色权限到某成员上
        /// </summary>
        /// <param name="GuildId"></param>
        /// <param name="UserId"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        Task GrantServerRole(string GuildId, string UserId, uint RoleId);
        /// <summary>
        /// 撤回已经给予的角色权限
        /// </summary>
        /// <param name="GuildId"></param>
        /// <param name="UserId"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        Task RevokeServerRole(string GuildId, string UserId, uint RoleId);
        /// <summary>
        /// 获取机器人自己的资料
        /// </summary>
        /// <returns></returns>
        Task<KHLResponseMessage<User>> GetMyself();
        /// <summary>
        /// 获取指定用户的资料
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="GuildId"></param>
        /// <returns></returns>
        Task<KHLResponseMessage<User>> GetUserDetail(string UserId, string GuildId);
        /// <summary>
        /// 获取服务器的表情列表
        /// </summary>
        /// <param name="GuildId"></param>
        /// <returns></returns>
        Task<KHLResponseMessage<EmojiList>> GetEmojiList(string GuildId);
    }
}
