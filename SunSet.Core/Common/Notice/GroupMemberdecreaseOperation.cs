using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using SunSet.Core.Milky;

namespace SunSet.Core.Common.Notice;

[CustomEvent("group_member_decrease")]
internal class GroupMemberdecreaseOperation : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if (node.Deserialize<MilkyGroupMemberDecrease>() is { } decrease)
        {
            await bot.Invoke.Call(bot, decrease);
        }
    }
}

public class MilkyGroupMemberDecrease : MilkyBaseData
{
    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }

    [JsonPropertyName("user_id")]
    public uint UserUin { get; init; }

    [JsonPropertyName("operation_id")]
    public uint OperationUin { get; init; }

    public override string ToPreviewString() =>
        $"[{nameof(MilkyGroupMemberDecrease)}] GroupUin: {GroupUin}, UserUin: {UserUin}, OperationUin: {OperationUin}";
}