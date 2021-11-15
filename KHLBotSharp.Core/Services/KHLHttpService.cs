using KHLBotSharp.IService;
using KHLBotSharp.Models.MessageHttps.RequestMessage;
using KHLBotSharp.Models.MessageHttps.ResponseMessage;
using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data;
using KHLBotSharp.Models.Objects;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace KHLBotSharp.Common.Request
{
    public class KHLHttpService : IKHLHttpService
    {
        private readonly IHttpClientService httpApi;
        private readonly IMemoryCache cache;

        public KHLHttpService(IHttpClientService httpApiBaseRequestService, IMemoryCache memoryCache)
        {
            httpApi = httpApiBaseRequestService;
            cache = memoryCache;
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

        public async Task<KHLResponseMessage<Channel>> GetChannelDetail(string ChannelId)
        {
            if (!cache.TryGetValue("channel/view/" + ChannelId, out KHLResponseMessage<Channel> result))
            {
                result = await httpApi.PostAsync<KHLResponseMessage<Channel>>("channel/view", new { target_id = ChannelId });
                cache.Set("channel/view/" + ChannelId, result, DateTimeOffset.Now.AddHours(1));
            }
            return result;
        }

        public async Task<KHLResponseMessage<GetChannelRoleList>> GetChannelRoles(string ChannelId)
        {
            if (!cache.TryGetValue("channel/role/view/" + ChannelId, out KHLResponseMessage<GetChannelRoleList> result))
            {
                result = await httpApi.GetAsync<KHLResponseMessage<GetChannelRoleList>>("channel-role/index?channel_id=" + ChannelId);
                cache.Set("channel/role/view/" + ChannelId, result, DateTimeOffset.Now.AddHours(1));
            }
            return result;
        }

        public async Task<KHLResponseMessage<GetChannelList>> GetChannels(string GuildId)
        {
            if (!cache.TryGetValue("channel/list/" + GuildId, out KHLResponseMessage<GetChannelList> result))
            {
                result = await httpApi.GetAsync<KHLResponseMessage<GetChannelList>>("channel/list?guild_id=" + GuildId);
                cache.Set("channel/list/" + GuildId, result, DateTimeOffset.Now.AddHours(1));
            }
            return result;
        }

        public Task<KHLResponseMessage<User>> GetUserDetail(string UserId, string GuildId)
        {
            //考虑可能会太多数据因此不缓存
            return httpApi.GetAsync<KHLResponseMessage<User>>("user/view?user_id=" + UserId + "&guild_id=" + GuildId);
        }

        public async Task<KHLResponseMessage<EmojiList>> GetEmojiList(string GuildId)
        {
            if (!cache.TryGetValue("emoji/list/" + GuildId, out KHLResponseMessage<EmojiList> result))
            {
                result = await httpApi.GetAsync<KHLResponseMessage<EmojiList>>("guild-emoji/list?guild_id=" + GuildId);
                cache.Set("emoji/list/" + GuildId, result, DateTimeOffset.Now.AddHours(1));
            }
            return result;
        }

        public async Task<KHLResponseMessage<Guild>> GetGuild(string GuildId)
        {
            if (!cache.TryGetValue("guild/view/" + GuildId, out KHLResponseMessage<Guild> result))
            {
                result = await httpApi.GetAsync<KHLResponseMessage<Guild>>("guild/view?guild_id=" + GuildId);
                cache.Set("guild/view/" + GuildId, result, DateTimeOffset.Now.AddHours(1));
            }
            return result;
        }

        public Task<KHLResponseMessage<GetGuildMemberList>> GetGuildUser(GetServerMember Request)
        {
            //考虑可能会太多数据因此不缓存
            return httpApi.GetAsync<KHLResponseMessage<GetGuildMemberList>>("guild/user-list", Request);
        }

        public Task<KHLResponseMessage<GetGuildMuteListDetail>> GetMute(string GuildId)
        {
            return httpApi.GetAsync<KHLResponseMessage<GetGuildMuteListDetail>>("guild-mute/list", new { guild_id = GuildId });
        }

        public async Task<KHLResponseMessage<User>> GetMyself()
        {
            if (!cache.TryGetValue("me", out KHLResponseMessage<User> result))
            {
                result = await httpApi.GetAsync<KHLResponseMessage<User>>("user/me");
                cache.Set("me", result, DateTimeOffset.Now.AddHours(1));
            }
            return result;
        }

        public async Task<KHLResponseMessage<PrivateMessageDetail>> GetPrivateChatDetail(string ChatCode)
        {
            if (!cache.TryGetValue("private/view/" + ChatCode, out KHLResponseMessage<PrivateMessageDetail> result))
            {
                result = await httpApi.GetAsync<KHLResponseMessage<PrivateMessageDetail>>("user-chat/view", new { chat_code = ChatCode });
                cache.Set("private/view/" + ChatCode, result, DateTimeOffset.Now.AddHours(1));
            }
            return result;
        }

        public Task<KHLResponseMessage<PrivateMessageList>> GetPrivateMessageList()
        {
            //考虑到可能会一直频繁更改，因此不作缓存
            return httpApi.GetAsync<KHLResponseMessage<PrivateMessageList>>("user-chat/list");
        }

        public Task<KHLResponseMessage<GetServerRoleList>> GetServerRoleList(string GuildId)
        {
            return httpApi.GetAsync<KHLResponseMessage<GetServerRoleList>>("guild-role/list", new { guild_id = GuildId });
        }

        public async Task<KHLResponseMessage<GetServerList>> GetServers()
        {
            if (!cache.TryGetValue("servers/list/", out KHLResponseMessage<GetServerList> result))
            {
                result = await httpApi.GetAsync<KHLResponseMessage<GetServerList>>("guild/list");
                cache.Set("servers/list/", result, DateTimeOffset.Now.AddHours(1));
            }
            return result;
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
            return httpApi.PostAsync<object>("guild-mute/create", Request);
        }

        public Task UnMute(SetServerMute Request)
        {
            return httpApi.PostAsync<object>("guild-mute/delete", Request);
        }

        public Task UpdateGroupMessage(UpdateMessage Request)
        {
            return httpApi.PostAsync<KHLResponseMessage<ServerRole>>("message/update", Request);
        }

        public Task<KHLResponseMessage<ServerRole>> UpdateServerRole(UpdateServerRole Request)
        {
            return httpApi.PostAsync<KHLResponseMessage<ServerRole>>("guild-role/update", Request);
        }

        public async Task<string> UploadFile(string filePath)
        {
            if (filePath.StartsWith("http"))
            {
                var wc = new HttpClient();
                var ms = new MemoryStream(await wc.GetByteArrayAsync(new Uri(filePath)));
                return await httpApi.UploadFileAsync(ms, filePath);
            }
            else
            {
                return await httpApi.UploadFileAsync(filePath);
            }
        }

        public Task<string> UploadFile(Stream stream, string fileName)
        {
            return httpApi.UploadFileAsync(stream, fileName);
        }

        public async Task OfflineBot()
        {
            await httpApi.PostAsync<dynamic>("user/offline", new { });
        }

        public Task<string> SendPrivateMessage(SendMessage Request)
        {
            return SendGroupMessage(Request);
        }
    }
}
