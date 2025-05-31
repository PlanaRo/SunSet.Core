using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using SunSet.Core.Milky;

namespace SunSet.Core.Common.Notice;

[CustomEvent("group_mute")]
internal class GroupMuteOperation : IOperationProcessor
{
    public async Task HandleOperationAsync(BotContext bot, JsonNode node, CancellationToken token)
    {
        if (node.Deserialize<MilkyGroupMute>() is { } mute)
        {
            await bot.Invoke.Call(bot, mute);
        }
    }
}

public class MilkyGroupMute : MilkyBaseData
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }

    [JsonPropertyName("user_id")]
    public uint UserUin { get; init; }

    [JsonPropertyName("duration")]
    public int Duration { get; init; }

    [JsonPropertyName("operator_id")]
    public uint OperatorUin { get; init; }

    public override string ToPreviewString() =>
        $"[{nameof(MilkyGroupMute)}] GroupUin: {GroupUin}, UserUin: {UserUin}, Duration: {Duration}, OperatorUin: {OperatorUin}";
}