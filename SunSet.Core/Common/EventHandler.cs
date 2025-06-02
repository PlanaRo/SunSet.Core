using System.Runtime.CompilerServices;
using SunSet.Core.Common.Message;
using SunSet.Core.Common.Notice;
using SunSet.Core.Log;
using SunSet.Core.Milky;
using SunSet.Core.Operation.Message;

namespace SunSet.Core.Common;

public class EventHandler
{
    public delegate Task OperationHandlerDelegate<T>(BotContext context, T args) where T : MilkyBaseData;

    private Dictionary<Type, Action<MilkyBaseData>> _eventHandlers = [];

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

    public event OperationHandlerDelegate<BotLogEventArgs>? BotLogEvent;

    public EventHandler(BotContext context)
    {

        RegisterEvent((MilkyGroupMessage args) => OnGroupMessageReceived?.Invoke(context, args));
        RegisterEvent((MilkyFriendMessage args) => OnFriendMessageReceived?.Invoke(context, args));
        RegisterEvent((MilkyTempMessage args) => OnTempMessageReceived?.Invoke(context, args));
        RegisterEvent((MilkyFriendFileUpload args) => OnFriendFileUploadReceived?.Invoke(context, args));
        RegisterEvent((MilkyFriendNudge args) => OnFriendNudgeReceived?.Invoke(context, args));
        RegisterEvent((MilkyGroupAdminChange args) => OnGroupAdminChangeReceived?.Invoke(context, args));
        RegisterEvent((MilkyGroupInvitationRequest args) => OnGroupInvitationRequestReceived?.Invoke(context, args));
        RegisterEvent((MilkyGroupInvitedJoinRequest args) => OnGroupInvitedJoinRequestReceived?.Invoke(context, args));
        RegisterEvent((MilkyGroupJoinRequest args) => OnGroupMemberJoinRequestReceived?.Invoke(context, args));
        RegisterEvent((MilkyGroupMemberDecrease args) => OnGroupMemberDecreaseReceived?.Invoke(context, args));
        RegisterEvent((MilkyGroupMemberIncrease args) => OnGroupMemberIncreaseReceived?.Invoke(context, args));
        RegisterEvent((MilkyGroupMessageReaction args) => OnGroupMessageReactionReceived?.Invoke(context, args));
        RegisterEvent((MilkyMessageRecall args) => OnMessageRecallReceived?.Invoke(context, args));
        RegisterEvent((MilkyGroupFileUpload args) => OnGroupFileUploadReceived?.Invoke(context, args));
        RegisterEvent((MilkyGroupEssenceMessageChange args) => OnGroupEssenceMessageChangeReceived?.Invoke(context, args));
        RegisterEvent((MilkyGroupMute args) => OnGroupMuteReceived?.Invoke(context, args));
        RegisterEvent((MilkyGroupWholeMute args) => OnGroupWholeMuteReceived?.Invoke(context, args));
        RegisterEvent((MilkyGroupNameChange args) => OnGroupNameChangeReceived?.Invoke(context, args));
        RegisterEvent((MilkyGroupNudge args) => OnGroupNudgeReceived?.Invoke(context, args));
        RegisterEvent((BotLogEventArgs args) => BotLogEvent?.Invoke(context, args));
    }

    internal void RegisterEvent<T>(Action<T> action) where T : MilkyBaseData => _eventHandlers[typeof(T)] = data => action((T)data);

    internal Task Call(BotContext context, MilkyBaseData args)
    {
        if (_eventHandlers.TryGetValue(args.GetType(), out var handler))
        {
            if(args is not BotLogEventArgs)
            {
                context.Log.LogInformation(args.ToPreviewString());
            }
            handler(args);
        }
        else
        {
            throw new NotSupportedException($"Event type '{args.GetType()}' is not supported.");
        }

        return Task.CompletedTask;
    }
}
