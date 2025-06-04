using SunSet.Core.Milky;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace SunSet.Core.Common.Notice;

[CustomEvent("group_essence_message_change")]
internal class GroupEssenceMessageChangeOperation : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if (node.Deserialize<MilkyGroupEssenceMessageChange>() is { } change)
        {
            await bot.Invoke.Call(bot, change);
        }
    }
}

public class MilkyGroupEssenceMessageChange : MilkyBaseData
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }
    /// <summary>
    /// 消息ID
    /// </summary>
    [JsonPropertyName("message_seq")]
    public long MessageSeq { get; init; }
    /// <summary>
    /// 是否置顶
    /// </summary>
    [JsonPropertyName("is_set")]
    public bool IsSet { get; init; }

    public override string ToPreviewString() =>
        $"[{nameof(MilkyGroupEssenceMessageChange)}] GroupId: {GroupUin} MessageSeq: {MessageSeq} {(IsSet ? "set" : "unset")}";
}