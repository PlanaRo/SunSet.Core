using System.Text.Json.Serialization;

namespace SunSet.Core.Segments.Entity;

[EntityType("reply")]
public class ReplyEntity : BaseEntity
{
    [JsonPropertyName("message_seq")]
    public long MessageSeq { get; init; } = 0;

    public override string ToString() =>
        $"[Reply: {MessageSeq}]";
}
