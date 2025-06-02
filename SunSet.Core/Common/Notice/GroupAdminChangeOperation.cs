using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using SunSet.Core.Milky;

namespace SunSet.Core.Common.Notice;

[CustomEvent("group_admin_change")]
internal class GroupAdminChangeOperation : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if (node.Deserialize<MilkyGroupAdminChange>() is { } change)
        {
            await bot.Invoke.Call(bot, change);
        }
    }
}

public class MilkyGroupAdminChange : MilkyBaseData
{
    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }

    [JsonPropertyName("user_id")]
    public uint UserUin { get; init; }

    [JsonPropertyName("is_set")]
    public bool IsSet { get; init; }

    public override string ToPreviewString() =>
        $"[{nameof(MilkyGroupAdminChange)}] Group: {GroupUin}, User: {UserUin}, Action: {(IsSet ? "Set Admin" : "Removed Admin")}";
}