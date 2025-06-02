using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using SunSet.Core.Milky;

namespace SunSet.Core.Common.Notice;

[CustomEvent("group_whole_mute")]
internal class GroupWholeMuteOperation : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if (node.Deserialize<MilkyGroupWholeMute>() is { } mute)
        {
            await bot.Invoke.Call(bot, mute);
        }
    }
}

public class MilkyGroupWholeMute : MilkyBaseData
{
    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }

    [JsonPropertyName("operator_id")]
    public uint OperatorUin { get; init; }

    [JsonPropertyName("is_mute")]
    public bool IsMute { get; init; }

    public override string ToPreviewString() =>
        $"[{nameof(MilkyGroupWholeMute)}] GroupUin: {GroupUin}, OperatorUin: {OperatorUin}, IsMute: {IsMute}";
}