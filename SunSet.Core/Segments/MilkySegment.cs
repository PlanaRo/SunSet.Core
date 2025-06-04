using SunSet.Core.Segments.Entity;
using System.Text.Json.Serialization;

namespace SunSet.Core.Entity;

[JsonConverter(typeof(EntityConverter))]
public class MilkySegment
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public BaseEntity Entity { get; set; } = new();

    public override string ToString() => Entity.ToString()!;
}
