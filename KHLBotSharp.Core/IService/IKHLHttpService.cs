using KHLBotSharp.Models.MessageHttps.RequestMessage;
using KHLBotSharp.Models.MessageHttps.ResponseMessage;
using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data;
using KHLBotSharp.Models.Objects;
using System.IO;
using System.Threading.Tasks;

namespace KHLBotSharp.IService
{
    public interface IKHLHttpService
    {
        Task<KHLResponseMessage<GetServerList>> GetServers();
        Task<KHLResponseMessage<Guild>> GetGuild(string GuildId);
        Task<KHLResponseMessage<GetGuildMemberList>> GetGuildUser(GetServerMember Request);
        Task ChangeNick(string GuildId, string UserId, string NickName = null);
        Task LeaveGuild(string GuildId);
        Task Kick(string GuildId, string TargetId);
        Task<KHLResponseMessage<GetGuildMuteListDetail>> GetMute(string GuildId);
        Task SetMute(SetServerMute Request);
        Task UnMute(SetServerMute Request);
        Task<KHLResponseMessage<GetChannelList>> GetChannels(string GuildId);
        Task<KHLResponseMessage<Channel>> GetChannelDetail(string ChannelId);
        Task<KHLResponseMessage<Channel>> CreateChannel(CreateChannel Request);
        Task DeleteChannel(string ChannelId);
        Task MoveUserVoiceChannel(string ChannelId, params string[] UserIds);
        Task<KHLResponseMessage<GetChannelRoleList>> GetChannelRoles(string ChannelId);
        Task AddChannelRoles(string ChannelId, bool IsUser, string UserIdOrRoleId);
        Task<string> SendGroupMessage(SendMessage Request);
        Task UpdateGroupMessage(UpdateMessage Request);
        Task RemoveGroupMessage(string MsgId);
        Task GroupMessageAddReaction(string MsgId, string Emoji);
        Task GroupMessageRemoveReaction(string MsgId, string Emoji, string UserId = null);
        Task<KHLResponseMessage<PrivateMessageList>> GetPrivateMessageList();
        Task<KHLResponseMessage<PrivateMessageDetail>> GetPrivateChatDetail(string ChatCode);
        Task<KHLResponseMessage<PrivateMessageDetail>> CreatePrivateMessage(string TargetId);
        Task RemovePrivateMessage(string ChatCode);
        Task PrivateMessageAddReaction(string MsgId, string Emoji);
        Task PrivateMessageRemoveReaction(string MsgId, string Emoji);
        Task<string> UploadFile(string filePath);
        Task<string> UploadFile(Stream stream);
        Task<KHLResponseMessage<GetServerRoleList>> GetServerRoleList(string GuildId);
        Task<KHLResponseMessage<ServerRole>> CreateServerRole(string GuildId, string Name = null);
        Task<KHLResponseMessage<ServerRole>> UpdateServerRole(UpdateServerRole Request);
        Task DeleteServerRole(string GuildId, uint RoleId);
        Task GrantServerRole(string GuildId, string UserId, uint RoleId);
        Task RevokeServerRole(string GuildId, string UserId, uint RoleId);
        Task<KHLResponseMessage<User>> GetMyself();
    }
}
