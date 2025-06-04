using SunSet.Core.Milky;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace SunSet.Core.Common.Notice;

[CustomEvent("group_name_change")]
internal class GroupNameChangeOperation : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if (node.Deserialize<MilkyGroupNameChange>() is { } change)
        {
            await bot.Invoke.Call(bot, change);
        }
    }
}

public class MilkyGroupNameChange : MilkyBaseData
{
    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("operator_id")]
    public uint OperatorUin { get; init; }

    public override string ToPreviewString() =>
        $"[{nameof(MilkyGroupNameChange)}] GroupUin: {GroupUin}, Name: {Name}, OperatorUin: {OperatorUin}";
}