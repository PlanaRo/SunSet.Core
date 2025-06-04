using SunSet.Core.Milky;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace SunSet.Core.Common.Notice;

[CustomEvent("group_nudge")]
internal class GroupNudgeOperation : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if (node.Deserialize<MilkyGroupNudge>() is { } nudge)
        {
            await bot.Invoke.Call(bot, nudge);
        }
    }
}

public class MilkyGroupNudge : MilkyBaseData
{
    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }

    [JsonPropertyName("sender_id")]
    public uint SenderUin { get; init; }

    [JsonPropertyName("receiver_id")]
    public uint ReceiverUin { get; init; }

    public override string ToPreviewString() =>
        $"[{nameof(MilkyGroupNudge)}] GroupUin: {GroupUin}, SenderUin: {SenderUin}, ReceiverUin: {ReceiverUin}";
}