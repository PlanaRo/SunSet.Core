using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using SunSet.Core.Milky;

namespace SunSet.Core.Common.Message;

[CustomEvent("message_recall")]
public class MessageRecallOperationl : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if (node.Deserialize<MilkyMessageRecall>() is { } msg)
        {
            await bot.Invoke.Call(bot, msg);
        }
    }
}

public class MilkyMessageRecall : MilkyBaseData
{
    [JsonPropertyName("message_scene")]
    public string MessageScene { get; init; } = string.Empty;

    [JsonPropertyName("peer_id")]
    public uint GroupUin { get; init; }

    [JsonPropertyName("message_seq")]
    public long MessageSeq { get; init; }

    [JsonPropertyName("sender_id")]
    public uint SenderUin { get; init; }

    [JsonPropertyName("operator_id")]
    public uint OperatorUin { get; init; }

    public override string ToPreviewString() =>
        $"[{nameof(MilkyMessageRecall)}] MessageScene: {MessageScene}, GroupUin: {GroupUin}, MessageSeq: {MessageSeq}, SenderUin: {SenderUin}, OperatorUin: {OperatorUin}";
}
