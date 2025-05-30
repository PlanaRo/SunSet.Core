using System.Text.Json.Serialization;
using SunSet.Core.Segments.Entity;

namespace SunSet.Core.Entity;

[JsonConverter(typeof(EntityConverter))]
public class MilkySegment
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public BaseEntity Entity { get; set; } = new();
}
