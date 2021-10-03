using KHLBotSharp.Core.Models.Config;
using KHLBotSharp.IService;
using KHLBotSharp.Models.EventsMessage;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace KHLBotSharp.WebHook.NetCore3.Helper
{
    public static class ParseMessage
    {
        
        public static Task ParseEvent(this JObject eventMsg, IPluginLoaderService pluginLoader, IBotConfigSettings settings, ILogService logService)
        {
            var channelType = eventMsg.Value<JToken>("d").Value<string>("channel_type");
            if (channelType == "GROUP")
            {
                switch (eventMsg.Value<JToken>("d").Value<int>("type"))
                {
                    case 1:
                        var groupText = eventMsg.ToObject<EventMessage<GroupTextMessageEvent>>();
                        if (settings.ProcessChar.Any(x => groupText.Data.Content.StartsWith(x)))
                        {
                            if (!groupText.Data.Extra.Author.IsBot)
                            {
                                logService.Debug("Received Group Text Event, Triggering Plugins");
                                pluginLoader.HandleMessage(groupText);
                            }
                        }
                        break;
                    case 2:
                        logService.Debug("Received Group Image Event, Triggering Plugins");
                        pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<GroupPictureMessageEvent>>());
                        break;
                    case 3:
                        logService.Debug("Received Group Video Event, Triggering Plugins");
                        pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<GroupVideoMessageEvent>>());
                        break;
                    case 9:
                        logService.Debug("Received Group Markdown Event, Triggering Plugins");
                        pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<GroupKMarkdownMessageEvent>>());
                        break;
                    case 10:
                        logService.Debug("Received Group Card Event, Triggering Plugins");
                        pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<GroupCardMessageEvent>>());
                        break;
                    case 255:
                        logService.Debug("Received Group System Event, Triggering Plugins");
                        var extra = eventMsg.Value<JToken>("d").Value<JToken>("extra").Value<string>("type");
                        switch (extra)
                        {
                            case "added_reaction":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ChannelUserAddReactionEvent>>>());
                                break;
                            case "deleted_reaction":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ChannelUserRemoveReactionEvent>>>());
                                break;
                            case "updated_message":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ChannelMessageUpdateEvent>>>());
                                break;
                            case "deleted_message":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ChannelMessageRemoveEvent>>>());
                                break;
                            case "added_channel":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ChannelCreatedEvent>>>());
                                break;
                            case "updated_channel":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ChannelModifyEvent>>>());
                                break;
                            case "deleted_channel":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ChannelRemovedEvent>>>());
                                break;
                            case "pinned_message":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ChannelPinnedMessageEvent>>>());
                                break;
                            case "unpinned_message":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ChannelRemovePinMessageEvent>>>());
                                break;
                            case "joined_guild":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerNewMemberJoinEvent>>>());
                                break;
                            case "exited_guild":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerMemberExitEvent>>>());
                                break;
                            case "updated_guild_member":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerMemberModifiedEvent>>>());
                                break;
                            case "guild_member_online":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerMemberOnlineEvent>>>());
                                break;
                            case "guild_member_offline":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerMemberOfflineEvent>>>());
                                break;
                            case "added_role":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerRoleAddEvent>>>());
                                break;
                            case "deleted_role":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerRoleRemoveEvent>>>());
                                break;
                            case "updated_role":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerRoleModifyEvent>>>());
                                break;
                            case "updated_guild":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerUpdateEvent>>>());
                                break;
                            case "deleted_guild":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerRemoveEvent>>>());
                                break;
                            case "added_block_list":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerBlacklistUserEvent>>>());
                                break;
                            case "deleted_block_list":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerRemoveBlacklistUserEvent>>>());
                                break;
                            case "joined_channel":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<UserJoinVoiceChannelEvent>>>());
                                break;
                            case "exited_channel":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<UserExitVoiceChannelEvent>>>());
                                break;
                        }
                        break;
                }
            }
            else if (channelType == "BROADCAST")
            {
                logService.Info("Received system message broadcast. Skipping data process");
            }
            else
            {
                switch (eventMsg.Value<JToken>("d").Value<int>("type"))
                {
                    case 1:
                        logService.Debug("Received Private Text Event, Triggering Plugins");
                        pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<PrivateTextMessageEvent>>());
                        break;
                    case 2:
                        logService.Debug("Received Private Image Event, Triggering Plugins");
                        pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<PrivatePictureMessageEvent>>());
                        break;
                    case 3:
                        logService.Debug("Received Private Video Event, Triggering Plugins");
                        pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<PrivateVideoMessageEvent>>());
                        break;
                    case 9:
                        logService.Debug("Received Private Markdown Event, Triggering Plugins");
                        pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<PrivateKMarkdownMessageEvent>>());
                        break;
                    case 10:
                        logService.Debug("Received Private Card Event, Triggering Plugins");
                        pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<PrivateCardMessageEvent>>());
                        break;
                    case 255:
                        logService.Debug("Received Private System Event, Triggering Plugins");
                        var extra = eventMsg.Value<JToken>("d").Value<JToken>("extra").Value<string>("type");
                        switch (extra)
                        {
                            case "updated_private_message":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<PrivateMessageModifyEvent>>>());
                                break;
                            case "deleted_private_message":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<PrivateMessageRemoveEvent>>>());
                                break;
                            case "private_added_reaction":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<PrivateMessageAddReactionEvent>>>());
                                break;
                            case "private_deleted_reaction":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<PrivateMessageRemoveReactionEvent>>>());
                                break;
                            case "user_updated":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<UserInfoChangeEvent>>>());
                                break;
                            case "message_btn_click":
                                pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<CardMessageButtonClickEvent>>>());
                                break;
                        }
                        break;
                }
            }
            return Task.CompletedTask;
        }
    }
}
