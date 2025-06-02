using System.Text.Json.Serialization;

namespace SunSet.Core.Common.ApiResultArgs;

public class GroupMessageResult
{
    [JsonPropertyName("group_id")]
    public uint GroupUin { get; init; }

    [JsonPropertyName("message_seq")]
    public long MessageSeq { get; init; }

    public override string ToString() =>
        $"[{nameof(GroupMessageResult)}] GroupUin: {GroupUin}, MessageSeq: {MessageSeq}";
}
