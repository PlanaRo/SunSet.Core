using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using SunSet.Core.Milky;

namespace SunSet.Core.Common.Notice;

[CustomEvent("group_message_reaction")]
internal class GroupMessageReactionOperation : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if (node.Deserialize<MilkyGroupMessageReaction>() is { } reaction)
        {
            await bot.Invoke.Call(bot, reaction);
        }
    }
}

public class MilkyGroupMessageReaction : MilkyBaseData
{
    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }

    [JsonPropertyName("message_seq")]
    public long MessageId { get; init; }

    [JsonPropertyName("face_id")]
    public string FaceId { get; init; } = string.Empty;

    [JsonPropertyName("user_id")]
    public uint UserUin { get; init; }

    [JsonPropertyName("is_add")]
    public bool IsAdd { get; init; }

    public override string ToPreviewString() =>
        $"[{nameof(MilkyGroupMessageReaction)}] Group: {GroupUin}, MessageId: {MessageId}, User: {UserUin}, Reaction: {(IsAdd ? "Added" : "Removed")} {FaceId}";
}