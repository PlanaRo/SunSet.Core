using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SunSet.Core.Segments.Entity;

[EntityType("reply")]
public class ReplyEntity : BaseEntity
{
    [JsonPropertyName("message_seq")]
    public long MessageSeq { get; init; } = 0;

    public override string ToString() =>
        $"[Reply: {MessageSeq}]";
}
