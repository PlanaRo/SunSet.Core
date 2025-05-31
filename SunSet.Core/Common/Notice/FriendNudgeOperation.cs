using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using SunSet.Core.Milky;

namespace SunSet.Core.Common.Notice;

[CustomEvent("friend_nudge")]
internal class FriendNudgeOperation : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if (node.Deserialize<MilkyFriendNudge>() is { } nudge)
        {
            await bot.Invoke.Call(bot, nudge, token);
        }
    }
}

public class MilkyFriendNudge : MilkyBaseData
{

    [JsonPropertyName("user_id")]
    public uint UserUin { get; init; }

    [JsonPropertyName("is_self_send")]
    public bool IsSelfSend { get; init; }

    [JsonPropertyName("is_self_receive")]
    public bool IsSelfReceive { get; init; }
}
