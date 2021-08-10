using KHLBotSharp.IService;
using KHLBotSharp.Models.MessageHttps.RequestMessage;
using KHLBotSharp.Models.MessageHttps.ResponseMessage;
using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data;
using KHLBotSharp.Models.Objects;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace KHLBotSharp.Common.Request
{
    public class KHLHttpService : IKHLHttpService
    {
        private readonly IHttpClientService httpApi;
        public KHLHttpService(IHttpClientService httpApiBaseRequestService)
        {
            httpApi = httpApiBaseRequestService;
        }

        public Task AddChannelRoles(string ChannelId, bool IsUser, string UserIdOrRoleId)
        {
            return httpApi.PostAsync<object>("channel-role/create", new { channel_id = ChannelId, type = IsUser ? "user_id" : "role_id", value = UserIdOrRoleId });
        }

        public Task ChangeNick(string GuildId, string UserId, string NickName = null)
        {
            return httpApi.PostAsync<object>("guild/nickname", new { guild_id = GuildId, nickname = NickName, user_id = UserId });
        }

        public Task<KHLResponseMessage<Channel>> CreateChannel(CreateChannel Request)
        {
            return httpApi.PostAsync<KHLResponseMessage<Channel>>("channel/create", Request);
        }

        public Task<KHLResponseMessage<PrivateMessageDetail>> CreatePrivateMessage(string TargetId)
        {
            return httpApi.PostAsync<KHLResponseMessage<PrivateMessageDetail>>("user-chat/create", new { target_id = TargetId });
        }

        public Task<KHLResponseMessage<ServerRole>> CreateServerRole(string GuildId, string Name = null)
        {
            return httpApi.PostAsync<KHLResponseMessage<ServerRole>>("guild-role/create", new { guild_id = GuildId, name = Name });
        }

        public Task DeleteChannel(string ChannelId)
        {
            return httpApi.PostAsync<object>("channel/delete", new { channel_id = ChannelId });
        }

        public Task DeleteServerRole(string GuildId, uint RoleId)
        {
            return httpApi.PostAsync<object>("guild-role/delete", new { guild_id = GuildId, role_id = RoleId });
        }

        public Task<KHLResponseMessage<Channel>> GetChannelDetail(string ChannelId)
        {
            return httpApi.PostAsync<KHLResponseMessage<Channel>>("channel/view", new { target_id = ChannelId });
        }

        public Task<KHLResponseMessage<GetChannelRoleList>> GetChannelRoles(string ChannelId)
        {
            return httpApi.GetAsync<KHLResponseMessage<GetChannelRoleList>>("channel-role/index?channel_id=" + ChannelId);
        }

        public Task<KHLResponseMessage<GetChannelList>> GetChannels(string GuildId)
        {
            return httpApi.GetAsync<KHLResponseMessage<GetChannelList>>("channel/list?guild_id=" + GuildId);
        }

        public Task<KHLResponseMessage<User>> GetDetail(string UserId, string GuildId)
        {
            return httpApi.GetAsync<KHLResponseMessage<User>>("user/view?user_id=" + UserId + "&guild_id=" + GuildId);
        }

        public Task<KHLResponseMessage<Guild>> GetGuild(string GuildId)
        {
            return httpApi.GetAsync<KHLResponseMessage<Guild>>("guild/view?guild_id=" + GuildId);
        }

        public Task<KHLResponseMessage<GetGuildMemberList>> GetGuildUser(GetServerMember Request)
        {
            return httpApi.GetAsync<KHLResponseMessage<GetGuildMemberList>>("guild/user-list", Request);
        }

        public Task<KHLResponseMessage<GetGuildMuteListDetail>> GetMute(string GuildId)
        {
            return httpApi.GetAsync<KHLResponseMessage<GetGuildMuteListDetail>>("guild-mute/list", new { guild_id = GuildId });
        }

        public Task<KHLResponseMessage<User>> GetMyself()
        {
            return httpApi.GetAsync<KHLResponseMessage<User>>("user/me");
        }

        public Task<KHLResponseMessage<PrivateMessageDetail>> GetPrivateChatDetail(string ChatCode)
        {
            return httpApi.GetAsync<KHLResponseMessage<PrivateMessageDetail>>("user-chat/view", new { chat_code = ChatCode });
        }

        public Task<KHLResponseMessage<PrivateMessageList>> GetPrivateMessageList()
        {
            return httpApi.GetAsync<KHLResponseMessage<PrivateMessageList>>("user-chat/list");
        }

        public Task<KHLResponseMessage<GetServerRoleList>> GetServerRoleList(string GuildId)
        {
            return httpApi.GetAsync<KHLResponseMessage<GetServerRoleList>>("guild-role/list", new { guild_id = GuildId });
        }

        public Task<KHLResponseMessage<GetServerList>> GetServers()
        {
            return httpApi.GetAsync<KHLResponseMessage<GetServerList>>("guild/list");
        }

        public Task GrantServerRole(string GuildId, string UserId, uint RoleId)
        {
            return httpApi.PostAsync<object>("guild-role/grant", new { guild_id = GuildId, user_id = UserId, role_id = RoleId });
        }

        public Task GroupMessageAddReaction(string MsgId, string Emoji)
        {
            return httpApi.PostAsync<object>("message/add-reaction", new { msg_id = MsgId, emoji = Emoji });
        }

        public Task GroupMessageRemoveReaction(string MsgId, string Emoji, string UserId = null)
        {
            return httpApi.PostAsync<object>("message/add-reaction", new { msg_id = MsgId, emoji = Emoji, user_id = UserId });
        }

        public Task Kick(string GuildId, string TargetId)
        {
            return httpApi.PostAsync<object>("guild/kickout", new { guild_id = GuildId, target_id = TargetId });
        }

        public Task LeaveGuild(string GuildId)
        {
            return httpApi.PostAsync<object>("guild/leave", new { guild_id = GuildId });
        }

        public Task MoveUserVoiceChannel(string ChannelId, params string[] UserIds)
        {
            return httpApi.PostAsync<object>("channel/move-user", new { target_id = ChannelId, user_ids = UserIds });
        }

        public Task PrivateMessageAddReaction(string MsgId, string Emoji)
        {
            return httpApi.PostAsync<object>("direct-message/add-reaction", new { msg_id = MsgId, emoji = Emoji });
        }

        public Task PrivateMessageRemoveReaction(string MsgId, string Emoji)
        {
            return httpApi.PostAsync<object>("direct-message/delete-reaction", new { msg_id = MsgId, emoji = Emoji });
        }

        public Task RemoveGroupMessage(string MsgId)
        {
            return httpApi.PostAsync<object>("message/delete", new { msg_id = MsgId });
        }

        public Task RemovePrivateMessage(string ChatCode)
        {
            throw new NotImplementedException();
        }

        public Task RevokeServerRole(string GuildId, string UserId, uint RoleId)
        {
            return httpApi.PostAsync<object>("guild-role/revoke", new { guild_id = GuildId, user_id = UserId, role_id = RoleId });
        }

        public async Task<string> SendGroupMessage(SendMessage Request)
        {
            return (await httpApi.PostAsync<KHLResponseMessage<SendChannelMessageReply>>("message/create", Request)).Data.MessageId;
        }

        public Task SetMute(SetServerMute Request)
        {
            throw new NotImplementedException();
        }

        public Task UnMute(SetServerMute Request)
        {
            throw new NotImplementedException();
        }

        public Task UpdateGroupMessage(UpdateMessage Request)
        {
            throw new NotImplementedException();
        }

        public Task<KHLResponseMessage<ServerRole>> UpdateServerRole(UpdateServerRole Request)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadFile(string filePath)
        {
            if (filePath.StartsWith("http"))
            {
                var wc = new HttpClient();
                var ms = new MemoryStream(await wc.GetByteArrayAsync(filePath));
                return await httpApi.UploadFileAsync(ms);
            }
            else
            {
                return await httpApi.UploadFileAsync(filePath);
            }
        }

        public Task<string> UploadFile(Stream stream)
        {
            return httpApi.UploadFileAsync(stream);
        }
    }
}
