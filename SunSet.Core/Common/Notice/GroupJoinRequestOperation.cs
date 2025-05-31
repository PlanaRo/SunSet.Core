using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using SunSet.Core.Milky;

namespace SunSet.Core.Common.Notice;

[CustomEvent("group_join_request")]
internal class GroupJoinRequestOperation : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if (node.Deserialize<MilkyGroupJoinRequest>() is { } request)
        {
            await bot.Invoke.Call(bot, request);
        }
    }
}

public class MilkyGroupJoinRequest : MilkyBaseData
{
    [JsonPropertyName("request_id")]
    public string RequestId { get; init; } = string.Empty;

    [JsonPropertyName("operator_id")]
    public uint OperatorId { get; init; }

    [JsonPropertyName("comment")]
    public string Comment { get; init; } = string.Empty;

    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }

    public override string ToPreviewString() =>
        $"[{nameof(MilkyGroupJoinRequest)}] RequestId: {RequestId}, OperatorId: {OperatorId}, GroupUin: {GroupUin}, Comment: {Comment}";
}
