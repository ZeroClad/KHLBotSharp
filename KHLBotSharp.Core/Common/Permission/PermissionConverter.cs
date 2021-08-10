using System;
using System.Collections;
using System.Collections.Generic;

namespace KHLBotSharp.Common.Permission
{
    public static class PermissionConverter
    {
        public static IList<Permission> ParsePermissions(this uint num)
        {
            List<Permission> permissions = new List<Permission>();
            var mbit = new BitArray(BitConverter.GetBytes(num));
            for (int x = 0; x < 28; x++)
            {
                if (mbit[x])
                {
                    permissions.Add((Permission)x);
                }
            }
            return permissions;
        }
    }

    public enum Permission
    {
        Admin,
        ManageServer,
        ViewManageLog,
        CreateServerInvitation,
        ManageInvitation,
        ManageChannels,
        KickUser,
        BanUser,
        ManageCustomEmoji,
        ModifyServerName,
        ManageRoles,
        ViewTextOrVoiceChannel,
        SendMessage,
        ManageMessage,
        UploadFile,
        VoiceLink,
        VoiceManage,
        AtAll,
        AddReaction,
        AddExistingReaction,
        ConnectVoice,
        OnlyPressSpeak,
        FreeMic,
        Talk,
        ServerMute,
        ServerOffMic,
        ModifyOthersNick,
        PlayMusicInVoiceChannel
    }
}
