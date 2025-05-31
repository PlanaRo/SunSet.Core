using SunSet.Core.Common.Message;
using SunSet.Core.Common.Notice;
using SunSet.Core.Milky;
using SunSet.Core.Operation.Message;

namespace SunSet.Core.Common;

public class EventHandler
{
    public delegate Task OperationHandlerDelegate<T>(BotContext context, T args, CancellationToken token) where T : MilkyBaseData;

    public event OperationHandlerDelegate<MilkyGroupMessage>? OnGroupMessageReceived;

    public event OperationHandlerDelegate<MilkyFriendMessage>? OnFriendMessageReceived;

    public event OperationHandlerDelegate<MilkyTempMessage>? OnTempMessageReceived;

    public event OperationHandlerDelegate<MilkyFriendFileUpload>? OnFriendFileUploadReceived;

    public event OperationHandlerDelegate<MilkyFriendNudge>? OnFriendNudgeReceived;

    public event OperationHandlerDelegate<MilkyGroupAdminChange>? OnGroupAdminChangeReceived;

    public event OperationHandlerDelegate<MilkyGroupInvitationRequest>? OnGroupInvitationRequestReceived;

    public event OperationHandlerDelegate<MilkyGroupInvitedJoinRequest>? OnGroupInvitedJoinRequestReceived;

    public event OperationHandlerDelegate<MilkyGroupJoinRequest>? OnGroupMemberJoinRequestReceived;

    public event OperationHandlerDelegate<MilkyGroupMemberDecrease>? OnGroupMemberDecreaseReceived;

    public event OperationHandlerDelegate<MilkyGroupMemberIncrease>? OnGroupMemberIncreaseReceived;

    public event OperationHandlerDelegate<MilkyGroupMessageReaction>? OnGroupMessageReactionReceived;

    public event OperationHandlerDelegate<MilkyMessageRecall>? OnMessageRecallReceived;

    public event OperationHandlerDelegate<MilkyGroupFileUpload>? OnGroupFileUploadReceived;

    public event OperationHandlerDelegate<MilkyGroupEssenceMessageChange>? OnGroupEssenceMessageChangeReceived;

    public event OperationHandlerDelegate<MilkyGroupMute>? OnGroupMuteReceived;

    public event OperationHandlerDelegate<MilkyGroupWholeMute>? OnGroupWholeMuteReceived;

    public event OperationHandlerDelegate<MilkyGroupNameChange>? OnGroupNameChangeReceived;

    public event OperationHandlerDelegate<MilkyGroupNudge>? OnGroupNudgeReceived;

    public Task Call(BotContext context, MilkyBaseData args, CancellationToken token)
    {
        return args switch
        {
            MilkyGroupMessage msg => OnGroupMessageReceived?.Invoke(context, msg, token),
            MilkyFriendMessage msg => OnFriendMessageReceived?.Invoke(context, msg, token),
            MilkyTempMessage msg => OnTempMessageReceived?.Invoke(context, msg, token),
            MilkyFriendFileUpload upload => OnFriendFileUploadReceived?.Invoke(context, upload, token),
            MilkyFriendNudge nudge => OnFriendNudgeReceived?.Invoke(context, nudge, token),
            MilkyGroupAdminChange adminChange => OnGroupAdminChangeReceived?.Invoke(context, adminChange, token),
            MilkyGroupInvitationRequest invitationRequest => OnGroupInvitationRequestReceived?.Invoke(context, invitationRequest, token),
            MilkyGroupInvitedJoinRequest invitedJoinRequest => OnGroupInvitedJoinRequestReceived?.Invoke(context, invitedJoinRequest, token),
            MilkyGroupJoinRequest joinRequest => OnGroupMemberJoinRequestReceived?.Invoke(context, joinRequest, token),
            MilkyGroupMemberDecrease memberDecrease => OnGroupMemberDecreaseReceived?.Invoke(context, memberDecrease, token),
            MilkyGroupMemberIncrease memberIncrease => OnGroupMemberIncreaseReceived?.Invoke(context, memberIncrease, token),
            MilkyGroupMessageReaction reaction => OnGroupMessageReactionReceived?.Invoke(context, reaction, token),
            MilkyMessageRecall recall => OnMessageRecallReceived?.Invoke(context, recall, token),
            MilkyGroupFileUpload groupFileUpload => OnGroupFileUploadReceived?.Invoke(context, groupFileUpload, token),
            MilkyGroupEssenceMessageChange essenceChange => OnGroupEssenceMessageChangeReceived?.Invoke(context, essenceChange, token),
            MilkyGroupMute groupMute => OnGroupMuteReceived?.Invoke(context, groupMute, token),
            MilkyGroupWholeMute groupWholeMute => OnGroupWholeMuteReceived?.Invoke(context, groupWholeMute, token),
            MilkyGroupNameChange groupNameChange => OnGroupNameChangeReceived?.Invoke(context, groupNameChange, token),
            MilkyGroupNudge groupNudge => OnGroupNudgeReceived?.Invoke(context, groupNudge, token),
            _ => null
        } ?? Task.CompletedTask;
    }
}
