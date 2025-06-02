using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using SunSet.Core.Milky;

namespace SunSet.Core.Common.Notice;

[CustomEvent("group_invited_join_request")]
internal class GroupInvitedJoinRequestOperation : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if (node.Deserialize<MilkyGroupInvitedJoinRequest>() is { } request)
        {
            await bot.Invoke.Call(bot, request);
        }
    }
}


public class MilkyGroupInvitedJoinRequest : MilkyBaseData
{
    [JsonPropertyName("request_id")]
    public string RequestId { get; init; } = string.Empty;

    [JsonPropertyName("operator_id")]
    public uint OperatorId { get; init; }

    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }

    [JsonPropertyName("invitee_id")]
    public uint InviteeUin { get; init; }

    public override string ToPreviewString() =>
        $"[{nameof(MilkyGroupInvitedJoinRequest)}] RequestId: {RequestId}, OperatorId: {OperatorId}, GroupUin: {GroupUin}, InviteeUin: {InviteeUin}";
}