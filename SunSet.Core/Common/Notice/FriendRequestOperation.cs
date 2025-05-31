using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using SunSet.Core.Milky;

namespace SunSet.Core.Common.Notice;

[CustomEvent("friend_request")]
internal class FriendRequestOperation : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if (node.Deserialize<MilkyFriendRequest>() is { } request)
        {
            await bot.Invoke.Call(bot, request, token);
        }
    }
}

public class MilkyFriendRequest : MilkyBaseData
{
    [JsonPropertyName("request_id")]
    public string RequestId { get; init; } = string.Empty;

    [JsonPropertyName("operator_id")]
    public uint OperatorId { get; init; }

    [JsonPropertyName("comment")]
    public string Comment { get; init; } = string.Empty;

    [JsonPropertyName("via")]
    public string Via { get; init; } = string.Empty;
}
