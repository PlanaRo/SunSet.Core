﻿using SunSet.Core.Milky;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace SunSet.Core.Common.Notice;

[CustomEvent("group_invitation_request")]
internal class GroupInvitationRequestOperation : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if (node.Deserialize<MilkyGroupInvitationRequest>() is { } request)
        {
            await bot.Invoke.Call(bot, request);
        }
    }
}

public class MilkyGroupInvitationRequest : MilkyBaseData
{
    [JsonPropertyName("request_id")]
    public string RequestId { get; init; } = string.Empty;

    [JsonPropertyName("operator_id")]
    public uint OperatorId { get; init; }

    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }

    public override string ToPreviewString() =>
        $"[{nameof(MilkyGroupInvitationRequest)}] RequestId: {RequestId}, OperatorId: {OperatorId}, GroupUin: {GroupUin}";
}