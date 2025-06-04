using SunSet.Core.Milky;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace SunSet.Core.Common.Notice;

[CustomEvent("group_member_increase")]
internal class GroupMemberIncreaseOperation : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if (node.Deserialize<MilkyGroupMemberIncrease>() is { } increase)
        {
            await bot.Invoke.Call(bot, increase);
        }
    }
}

public class MilkyGroupMemberIncrease : MilkyBaseData
{
    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }

    [JsonPropertyName("user_id")]
    public uint UserUin { get; init; }

    [JsonPropertyName("operator_id")]
    public uint OperatorUin { get; init; }

    [JsonPropertyName("invitor_id")]
    public uint InvitorUin { get; init; }

    public override string ToPreviewString() =>
        $"[{nameof(MilkyGroupMemberIncrease)}] GroupUin: {GroupUin}, UserUin: {UserUin}, OperatorUin: {OperatorUin}, InvitorUin: {InvitorUin}";
}